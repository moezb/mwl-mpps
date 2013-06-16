using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;
using ClearCanvas.Common;
using ClearCanvas.Common.Utilities;
using ClearCanvas.Dicom;
using ClearCanvas.Dicom.Network;
using ClearCanvas.Dicom.Network.Scp;
using ClearCanvas.Dicom.Utilities.Xml;
using ClearCanvas.Dicom.Iod.Iods;
using System.Net;
using System.Collections;
//using ClearCanvas.Enterprise.Core;
using ClearCanvas.Dicom.Audit;
using System.Collections.Specialized;
using System.Xml;
using Edgeteks.MWLServer.DBBroker;


namespace Edgeteks.MWLServer
{

    /// <summary>
    /// This Class represent a Dicom server that Perform the MPPS SCP role. 
    /// </summary>
    public class MPPSScp : IDicomServerHandler
    {
        #region private static members
        // session dumper
        //private static SessionDebugInfo _sessionDebug = new SessionDebugInfo();
        private static bool _started = false;
        private static ServerAssociationParameters _staticAssocParameters;
        private ServerAssociationParameters _assocParameters;
        // cache for processing mpps entities
        private static Dictionary<int, ListDictionary> _mppsCache = new Dictionary<int, ListDictionary>();
        #endregion        

        #region Constructors
        private MPPSScp(ServerAssociationParameters assoc)
        {
            _assocParameters = assoc;
        }
        #endregion

        #region Public Properties
        public static bool Started
        {
            get { return _started; }
        }
        #endregion

        #region Private Methods
        private static void AddPresentationContexts(ServerAssociationParameters assoc)
        {
            byte pcid = assoc.AddPresentationContext(SopClass.VerificationSopClass);
            assoc.AddTransferSyntax(pcid, TransferSyntax.ExplicitVrLittleEndian);
            assoc.AddTransferSyntax(pcid, TransferSyntax.ImplicitVrLittleEndian);

            pcid = assoc.AddPresentationContext(SopClass.ModalityPerformedProcedureStepSopClass);
            assoc.AddTransferSyntax(pcid, TransferSyntax.ExplicitVrLittleEndian);
            assoc.AddTransferSyntax(pcid, TransferSyntax.ImplicitVrLittleEndian);

        }
        #endregion

        #region Static Public Methods
        public static void StartListening(string aeTitle, int port)
        {
            if (_started)
                return;

            _staticAssocParameters = new ServerAssociationParameters(aeTitle, new IPEndPoint(IPAddress.Any, port));

            Platform.Log(LogLevel.Info, "MPPS Server Started");

            AddPresentationContexts(_staticAssocParameters);

            DicomServer.StartListening(_staticAssocParameters,
                delegate(DicomServer server, ServerAssociationParameters assoc)
                {
                    return new MPPSScp(assoc);
                });

            _started = true;
        }

        public static void StopListening(int port)
        {
            if (_started)
            {
                Platform.Log(LogLevel.Info, "Stopping The MPPS server...");
                DicomServer.StopListening(_staticAssocParameters);
                _started = false;
                Platform.Log(LogLevel.Info, "MPPS Server Stopped.");
            }
        }
        #endregion

        #region MPPS SCP processing member methods

        private bool ProcessNSetRequestForCompleted(DicomServer server, byte presentationID, DicomMessage message, XmlElement performedSeriesSQ)
        {
            return DBBroker.ExamsScheduled.SetModalityPerformedProcedureStep(message.DataSet[DicomTags.PerformedProcedureStepId].GetInt32(0, 0),
                                                                             performedSeriesSQ);
        }

        private bool ProcessNCreateRequest(DicomServer server, byte presentationID, DicomMessage message)
        {
            return ExamsScheduled.CreateModalityPerformedProcedureStep(message.DataSet[DicomTags.PerformedProcedureStepId].GetInt32(0, 0));
        }

        private bool ProcessNSetRequestForDiscontinued(DicomServer server, byte presentationID, DicomMessage message,XmlElement performedSeriesSQ)
        {
            return ExamsScheduled.DiscontinueModalityPerformedProcedureStep(message.DataSet[DicomTags.PerformedProcedureStepId].GetInt32(0, 0),
                                                                            performedSeriesSQ);
        }



        #endregion

        #region IDicomServerHandler Member

        void IDicomServerHandler.OnReceiveAssociateRequest(DicomServer server, ServerAssociationParameters association)
        {
            //_sessionDebug.SetAssociationDumpString(association);
            server.SendAssociateAccept(association);
            Platform.Log(LogLevel.Info,"Accepting association between {0} and {1}.",
                             association.CallingAE, association.CalledAE);

        }

        void IDicomServerHandler.OnReceiveRequestMessage(DicomServer server, ServerAssociationParameters association, byte presentationID, DicomMessage message)
        {
            //_sessionDebug.SetAssociationDumpString(association);
            //_sessionDebug._request = message.Dump();

            #region CEcho request
            if (message.CommandField == DicomCommandField.CEchoRequest)
            {
                server.SendCEchoResponse(presentationID, message.MessageId, DicomStatuses.Success);
                Platform.Log(LogLevel.Info, "Received ECHO-RQ message from {0}.", association.CallingAE);
                return;
            }
            #endregion

            #region MPPS NCreate Request
            if (message.CommandField == DicomCommandField.NCreateRequest)
            {
                // june -1st-2009 :
                // Unlike the "ModalityWorklistIod"  class, the 'partially' implemented ModalityPerformedProcedureStepIod class could 
                // be usefull here.
                
                ModalityPerformedProcedureStepIod mppsIod = new ModalityPerformedProcedureStepIod(message.DataSet);

                Platform.Log(LogLevel.Info, "Message Dumped :\n" + message.Dump("", DicomDumpOptions.KeepGroupLengthElements));

                // checking message for error and anomalies

                bool conform = CheckNCreateDataSetConformance(server, association, presentationID, mppsIod, true);

                if (!conform)
                {
                    server.SendNCreateResponse(presentationID, message.MessageId, new DicomMessage(), DicomStatuses.InvalidAttributeValue);
                    Platform.Log(LogLevel.Error, "Sending Invalid Attributes Response.");
                    return;
                }

                // wrong status
                if (mppsIod.PerformedProcedureStepInformation.PerformedProcedureStepStatus != ClearCanvas.Dicom.Iod.Modules.PerformedProcedureStepStatus.InProgress)
                {
                    server.SendNCreateResponse(presentationID, message.MessageId, new DicomMessage(), DicomStatuses.InvalidAttributeValue);
                    Platform.Log(LogLevel.Error, "Recieved N-Create Request with bad status.");
                    return;
                }
                // pps already in cache (duplicated step) 
                string cacheKeyId = message.DataSet[DicomTags.AffectedSopInstanceUid].GetString(0, string.Empty);
                bool alreadyCached;
                MPPSScp.CacheMppsEntity(association, cacheKeyId, mppsIod, out alreadyCached);
                if (alreadyCached)
                {
                    server.SendNCreateResponse(presentationID, message.MessageId, new DicomMessage(), DicomStatuses.DuplicateSOPInstance);
                    Platform.Log(LogLevel.Error, "Recieved duplicated N-Create Request.");
                    return;
                }
                if (!ProcessNCreateRequest(server, presentationID, message))
                {
                    server.SendNCreateResponse(presentationID, message.MessageId, new DicomMessage(), DicomStatuses.ProcessingFailure);
                    Platform.Log(LogLevel.Error, "Sending Processing due to NCreate request Failure.");
                    return;
                }
                server.SendNCreateResponse(presentationID, message.MessageId, new DicomMessage(), DicomStatuses.Success);                
                return;

            }
            #endregion

            #region MPPS NSet Request
            if (message.CommandField == DicomCommandField.NSetRequest)
            {
                // june -1st-2009 :
                // Unlike the "ModalityWorklistIod"  class, the ModalityPerformedProcedureStepIod is fully implemented
                // we can use it here. 
                ModalityPerformedProcedureStepIod mppsIod = new ModalityPerformedProcedureStepIod(message.DataSet);

                Platform.Log(LogLevel.Info, "Message Dumped :\n" + message.Dump("", DicomDumpOptions.KeepGroupLengthElements));

                // check if pps already in cache (duplicated step) 
                string cacheKeyId = message.DataSet[DicomTags.AffectedSopInstanceUid].GetString(0, string.Empty);

                if (!IsMppsEntitycached(association, cacheKeyId))
                {
                    server.SendNCreateResponse(presentationID, message.MessageId, new DicomMessage(), DicomStatuses.NoSuchObjectInstance);
                    Platform.Log(LogLevel.Error, "Received Unknown NSset SOP.");
                    return;
                }

                // status diffrent from in progress
                if (mppsIod.PerformedProcedureStepInformation.PerformedProcedureStepStatus == ClearCanvas.Dicom.Iod.Modules.PerformedProcedureStepStatus.InProgress)
                {
                    server.SendNCreateResponse(presentationID, message.MessageId, new DicomMessage(), DicomStatuses.InvalidAttributeValue);
                    Platform.Log(LogLevel.Error, "Recieved N-Set Request with In Progress status.");
                    return;
                }

                // checking the received mppsiod against cached one
                ModalityPerformedProcedureStepIod cachedMppsIod = GetCachedMppsIod(association, cacheKeyId);
                //assuming cachedMppsIod not null.
                bool conform = CheckNSetDataSetConformance(server, association, presentationID, cachedMppsIod, mppsIod, true);

                if (!conform)
                {
                    server.SendNCreateResponse(presentationID, message.MessageId, new DicomMessage(), DicomStatuses.InvalidAttributeValue);
                    Platform.Log(LogLevel.Error, "Sending Failure Response.");
                    return;
                }

                string studyInstanceUID = mppsIod.
                                          PerformedProcedureStepRelationship.
                                          DicomAttributeProvider[DicomTags.StudyInstanceUid].
                                          GetString(0,"");

                XmlElement performedSeriesSQ = GenerateXmlForPerformedSeriesSQ(message,studyInstanceUID);

                bool success = true;

                if (mppsIod.PerformedProcedureStepInformation.PerformedProcedureStepStatus 
                            == ClearCanvas.Dicom.Iod.Modules.PerformedProcedureStepStatus.Completed)
                {
                    success = ProcessNSetRequestForCompleted(server, presentationID, message, performedSeriesSQ);
                }

                if (mppsIod.PerformedProcedureStepInformation.PerformedProcedureStepStatus 
                            == ClearCanvas.Dicom.Iod.Modules.PerformedProcedureStepStatus.Discontinued)
                {
                    success = ProcessNSetRequestForDiscontinued(server, presentationID, message, performedSeriesSQ);
                }

                if (success)
                   server.SendNSetResponse(presentationID, message.MessageId, new DicomMessage(), DicomStatuses.Success); 
                else
                   server.SendNSetResponse(presentationID, message.MessageId, new DicomMessage(), DicomStatuses.ProcessingFailure);
            
                MPPSScp.RemoveMppsEntityFromCache(association, cacheKeyId);
                
                return;
            }

            #endregion

            //ignore all unsupported request
            server.SendAssociateAbort(DicomAbortSource.ServiceProvider, DicomAbortReason.UnexpectedPDU);
            Platform.Log(LogLevel.Info, "Unexpected Command. Send Associate Abort message from server to {0}.", association.CallingAE);
            return;

        }
        /// <summary>
        /// This methid generate an XmlElement that contains the data necessary
        /// to create Performed Series Sequences.
        /// If modality return
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private static XmlElement GenerateXmlForPerformedSeriesSQ(DicomMessage message,string studyInstanceUID)
        {
            #region Sample Generated Node
            /* 
             * <Root>
             * <Series>               
             *    <Images>
             *    <Image/>             *   
             * </Series>              
             * <Series>               
             *    <Images>
             *    <Image/>             *   
             * </Series>  
             * </Root>
            */
            #endregion

            XmlDocument dom = new XmlDocument();
            XmlElement result = dom.CreateElement("Root");

            dom.AppendChild(result);

            //get StudyIsntanceUID
            DicomAttribute attribute = message.DataSet[DicomTags.PerformedSeriesSequence];

            DicomAttributeSQ seriesSequence = attribute as DicomAttributeSQ;
            //sequence.Count   should be at least 1.
            
            for( int i =0; i < seriesSequence.Count;i++)
            {
                DicomSequenceItem seriesSqItem = seriesSequence[i];
                System.Xml.XmlNode newSeries;
                newSeries = dom.CreateElement("Series");
                //Set Attributes
                newSeries.Attributes.Append(dom.CreateAttribute("StudyInstanceUID")).Value = 
                                                                seriesSqItem[DicomTags.StudyInstanceUid].GetString(0,"");
                newSeries.Attributes.Append(dom.CreateAttribute("PerformingPhysicianName")).Value =
                                                                seriesSqItem[DicomTags.PerformingPhysiciansName].GetString(0, "");                
                string seriesInstanceUID = seriesSqItem[DicomTags.SeriesInstanceUid].GetString(0,DicomUid.GenerateUid().UID);
                newSeries.Attributes.Append(dom.CreateAttribute("SeriesInstanceUID")).Value =seriesInstanceUID;

                newSeries.Attributes.Append(dom.CreateAttribute("OperatorName")).Value =
                                                                seriesSqItem[DicomTags.OperatorsName].GetString(0, "");
                newSeries.Attributes.Append(dom.CreateAttribute("ProtocolName")).Value =
                                                                seriesSqItem[DicomTags.ProtocolName].GetString(0, "");
                newSeries.Attributes.Append(dom.CreateAttribute("SeriesDescription")).Value =
                                                                seriesSqItem[DicomTags.SeriesDescription].GetString(0, "");
                newSeries.Attributes.Append(dom.CreateAttribute("RetrieveAET")).Value =
                                                                seriesSqItem[DicomTags.RetrieveAeTitle].GetString(0, "");

                #region loop over Referenced Image Sequence
                //DicomAttribute referencedImageSQ = seriesSqItem.DataSet[DicomTags.ReferencedImageSequence];

                #region ReferencedImageSequence
                DicomAttributeSQ imagesSequence = (seriesSqItem[DicomTags.ReferencedImageSequence]) as DicomAttributeSQ;
                
                for(int j=0;j<imagesSequence.Count;j++)
                {                    
                    DicomSequenceItem imagesSeqItem = imagesSequence[j];
                    System.Xml.XmlNode newImage;
                    newImage = dom.CreateElement("Image");
                    //Set Attributes 
                    newImage.Attributes.Append(dom.CreateAttribute("SeriesInstanceUID")).Value = seriesInstanceUID;
                    newImage.Attributes.Append(dom.CreateAttribute("ReferencedImageSopClassUID")).Value =
                                                                    imagesSeqItem[DicomTags.ReferencedSopClassUid].GetString(0, "");
                    newImage.Attributes.Append(dom.CreateAttribute("ReferencedImageSopInstanceUID")).Value =
                                                                    imagesSeqItem[DicomTags.ReferencedSopInstanceUid].GetString(0, DicomUid.GenerateUid().UID);
                    newSeries.AppendChild(newImage);
                }
                #endregion

                #region ReferencedNonImageCompositeSopInstanceSequence
                DicomAttributeSQ nonImageCompositeSequence = (seriesSqItem[DicomTags.ReferencedNonImageCompositeSopInstanceSequence]) as DicomAttributeSQ;
                for(int k=0;k<imagesSequence.Count;k++)                    
                {
                    DicomSequenceItem nonImagesCompositeSeqItem = nonImageCompositeSequence[k];
                    System.Xml.XmlNode newNonImageComposite;
                    newNonImageComposite = dom.CreateElement("Image");
                    //Set Attributes 
                    newNonImageComposite.Attributes.Append(dom.CreateAttribute("SeriesInstanceUID")).Value = seriesInstanceUID;
                    newNonImageComposite.Attributes.Append(dom.CreateAttribute("ReferencedImageSopClassUID")).Value =
                                                                    nonImagesCompositeSeqItem[DicomTags.ReferencedSopClassUid].GetString(0, "");
                    newNonImageComposite.Attributes.Append(dom.CreateAttribute("ReferencedImageSopInstanceUID")).Value =
                                                                    nonImagesCompositeSeqItem[DicomTags.ReferencedSopInstanceUid].GetString(0, DicomUid.GenerateUid().UID);
                    newSeries.AppendChild(newNonImageComposite);
                }
                #endregion

                #endregion
                result.AppendChild(newSeries);
            }

            return result;

        }

        #region Mpps Entities Caching methods
        private static bool IsMppsEntitycached(ServerAssociationParameters association, string key)
        {
            ListDictionary mppsAssociationList;
            if (_mppsCache.TryGetValue(association.GetHashCode(), out mppsAssociationList))
                return false;
            else
                return mppsAssociationList.Contains(key);
        }

        private static void CacheMppsEntity(ServerAssociationParameters association,
                                            string key,
                                            ModalityPerformedProcedureStepIod ie,
                                            out bool alreadyCached)
        {
            alreadyCached = false;
            ListDictionary mppsAssociationList;
            if (_mppsCache.TryGetValue(association.GetHashCode(), out mppsAssociationList))
            {
                if (mppsAssociationList.Contains(key))
                    alreadyCached = true;
                else
                    mppsAssociationList.Add(key, ie);
            }
            else
            {
                ListDictionary newList = new ListDictionary();
                newList.Add(key, ie);
                _mppsCache.Add(association.GetHashCode(), newList);
            }

        }

        private static void RemoveMppsEntityFromCache(ServerAssociationParameters association, string key)
        {
            ListDictionary mppsAssociationList;
            if (_mppsCache.TryGetValue(association.GetHashCode(), out mppsAssociationList))
            {
                if (mppsAssociationList.Contains(key))
                    mppsAssociationList.Remove(key);
            }

        }

        private static void RemoveAllAssociationMppsFromCache(ServerAssociationParameters association)
        {
            ListDictionary mppsAssociationList;
            if (_mppsCache.TryGetValue(association.GetHashCode(), out mppsAssociationList))
            {
                mppsAssociationList.Clear();
                _mppsCache.Remove(association.GetHashCode());
            }
        }

        private static ModalityPerformedProcedureStepIod GetCachedMppsIod(ServerAssociationParameters association ,string cacheKeyId)
        {
            ListDictionary mppsAssociationList;
            if (_mppsCache.TryGetValue(association.GetHashCode(), out mppsAssociationList))
            {
                 if (mppsAssociationList.Contains(cacheKeyId))
                     return mppsAssociationList[cacheKeyId] as ModalityPerformedProcedureStepIod;
                 else 
                     return null;
            }
            return null;
        }

        #endregion

        void IDicomServerHandler.OnReceiveResponseMessage(DicomServer server, ServerAssociationParameters association, byte presentationID, DicomMessage message)
        {
            Platform.Log(LogLevel.Error, "Unexpectedly received response mess on server.");
            server.SendAssociateAbort(DicomAbortSource.ServiceUser, DicomAbortReason.UnrecognizedPDU);
            //_sessionDebug.DumpSession();            
        }

        void IDicomServerHandler.OnReceiveReleaseRequest(DicomServer server, ServerAssociationParameters association)
        {
            Platform.Log(LogLevel.Info, "Received association release request from  {0}.", association.CallingAE);
            //_sessionDebug.DumpSession();
            //RemoveAllAssociationMppsFromCache(association);
        }

        void IDicomServerHandler.OnReceiveAbort(DicomServer server, ServerAssociationParameters association, DicomAbortSource source, DicomAbortReason reason)
        {
            Platform.Log(LogLevel.Info, "Unexpected association abort received.");
            //_sessionDebug.DumpSession();
            RemoveAllAssociationMppsFromCache(association);
        }

        void IDicomServerHandler.OnNetworkError(DicomServer server, ServerAssociationParameters association, Exception e)
        {
            Platform.Log(LogLevel.Error, e, string.Format("Unexpected network error over association from {0}.", association.CallingAE));
            //_sessionDebug.DumpSession();
            RemoveAllAssociationMppsFromCache(association);

        }

        void IDicomServerHandler.OnDimseTimeout(DicomServer server, ServerAssociationParameters association)
        {

            Platform.Log(LogLevel.Info, "Received DIMSE Timeout, continuing listening for messages");
            //_sessionDebug.DumpSession();
            RemoveAllAssociationMppsFromCache(association);
        }

        protected void LogAssociationStatistics(ServerAssociationParameters association)
        {

        }

        #endregion

        #region MPPS MessageChecking methods
        private bool CheckNCreateDataSetConformance(DicomServer server, ServerAssociationParameters association, byte presentationID, 
                                         ModalityPerformedProcedureStepIod mppsIod,
                                         bool logFirstAnomalyOnly)
        {            
            bool anomaly = false;
            string comments = "";
            try
            {
                do
                {

                    #region Checking Type 1 Attributes (existance and values)
                    if (mppsIod.PerformedProcedureStepRelationship == null)
                    {
                        anomaly = true;
                        comments += "Performed Procedure Step Relationship Sequence Absent.\n";
                        if (logFirstAnomalyOnly) break;
                    }
                    if (mppsIod.PerformedProcedureStepRelationship.ScheduledStepAttributesSequenceList == null)
                    {
                        anomaly = true;
                        comments += "Scheduled Step Attributes Sequence Absent.\n";
                        if (logFirstAnomalyOnly) break;
                    }
                    if (mppsIod.PerformedProcedureStepRelationship.ScheduledStepAttributesSequenceList.FirstSequenceItem == null)
                    {
                        anomaly = true;
                        comments += "Scheduled Step Attributes Sequence Empty.\n";
                        if (logFirstAnomalyOnly) break;
                    }

                    if (string.IsNullOrEmpty(mppsIod.PerformedProcedureStepRelationship.
                                                     ScheduledStepAttributesSequenceList.
                                                     FirstSequenceItem.StudyInstanceUid.Trim()))
                    {// we  can add more control here (to check the id root for exmaple).
                        anomaly = true;
                        comments += "Invalid Study Instance Uid.\n";
                        if (logFirstAnomalyOnly) break;
                    }

                    if (mppsIod.PerformedProcedureStepInformation == null)
                    {
                        anomaly = true;
                        comments += "Performed Procedure Step Information module missing.\n";
                        if (logFirstAnomalyOnly) break;
                    }
                    if (mppsIod.PerformedProcedureStepInformation.PerformedProcedureStepId == null)
                    {
                        anomaly = true;
                        comments += "Performed Procedure StepId missing.\n";
                        if (logFirstAnomalyOnly) break;
                    }
                    if (string.IsNullOrEmpty(mppsIod.PerformedProcedureStepInformation.PerformedProcedureStepId))
                    {
                        anomaly = true;
                        comments += "Performed Procedure Step Information attribute invalid value.\n";
                        if (logFirstAnomalyOnly) break;
                    }

                    if (mppsIod.PerformedProcedureStepInformation.PerformedStationAeTitle == null)
                    {
                        anomaly = true;
                        comments += "Performed Station Ae Title missing.\n";
                        if (logFirstAnomalyOnly) break;
                    }
                    if (string.IsNullOrEmpty(mppsIod.PerformedProcedureStepInformation.PerformedStationAeTitle))
                    {
                        anomaly = true;
                        comments += "Performed Station Ae Title attribute invalid value.\n";
                        if (logFirstAnomalyOnly) break;
                    }


                    if (mppsIod.PerformedProcedureStepInformation.PerformedProcedureStepStartDate == null)
                    {
                        anomaly = true;
                        comments += "Performed Procedure Step Start Date missing.\n";
                        if (logFirstAnomalyOnly) break;
                    }

                    if (mppsIod.DicomAttributeProvider[DicomTags.PerformedProcedureStepStartDate].GetDateTime(0).HasValue)
                    {
                        anomaly = true;
                        comments += "Performed Procedure Step Start Date invalid value.\n";
                        if (logFirstAnomalyOnly) break;
                    }

                    if (mppsIod.DicomAttributeProvider[DicomTags.PerformedProcedureStepStartTime] == null)
                    {
                        anomaly = true;
                        comments += "Performed Procedure Step Start Time missing.\n";
                        if (logFirstAnomalyOnly) break;
                    }
                    if (mppsIod.DicomAttributeProvider[DicomTags.PerformedProcedureStepStartTime].GetDateTime(0).HasValue)
                    {
                        anomaly = true;
                        comments += "Performed Procedure Step Start Time invalid value.\n";
                        if (logFirstAnomalyOnly) break;
                    }
                    //checking only the status existance
                    if (mppsIod.DicomAttributeProvider[DicomTags.PerformedProcedureStepStatus]== null)
                    {
                        anomaly = true;
                        comments += "Performed Procedure Step Status Missing.\n";
                        if (logFirstAnomalyOnly) break;
                    }

                    if (mppsIod.DicomAttributeProvider[DicomTags.Modality] == null)
                    {
                        anomaly = true;
                        comments += "Modlity Missing.\n";
                        if (logFirstAnomalyOnly) break;
                    }

                    if (mppsIod.DicomAttributeProvider[DicomTags.Modality].GetString(0,"") =="")
                    {
                        anomaly = true;
                        comments += "Modlity invalid value.\n";
                        if (logFirstAnomalyOnly) break;
                    }
                    #endregion

                    #region Check Type 2 attributes
                    // TODO to check all other attributes.

                    if (mppsIod.PerformedProcedureStepRelationship.
                                                     ScheduledStepAttributesSequenceList.
                                                     FirstSequenceItem.DicomSequenceItem[DicomTags.ReferencedStudySequence] == null)
                    {// we  can add more control here
                        anomaly = true;
                        comments += "Referenced Study Sequence Abscent.\n";
                        if (logFirstAnomalyOnly) break;
                    }
                    //TODO :  ReferencedStudySequence FirstItem checking
                    { }
                    if (mppsIod.PerformedProcedureStepRelationship.PatientsName == null)
                    {
                        anomaly = true;
                        comments += "Referenced Study Sequence Abscent.\n";
                        if (logFirstAnomalyOnly) break;
                    }
                    //etc..
                    #endregion


                } while (false);

                if (anomaly)
                {
                    Platform.Log(LogLevel.Warn, "Invalid Modality Performed Procedute Step SOP N-CREATE data received from {0} .",
                                                 server.AssociationParams.CallingAE);
                    Platform.Log(LogLevel.Warn, "-- Details : {0}," + comments);

                }
                return anomaly;
            }
            catch (Exception ex)
            {
                Platform.Log(LogLevel.Error, ex, "Exception Occured while checking the mppsIod", mppsIod);
                
                anomaly = true;
                return anomaly;
            }
       }
        private bool CheckNSetDataSetConformance(DicomServer server, ServerAssociationParameters association, byte presentationID,
                                      ModalityPerformedProcedureStepIod cachedMppsIod,
                                      ModalityPerformedProcedureStepIod receivedMppsIod, bool logFirstAnomalyOnly)
        {
            bool anomaly = false;
            string comments = "";
            try
            {
                do
                {
                    // We are not going to verify sop instance UID neither status - those 2 tests will be done in main processing.

                    #region Character Set 
                    if (cachedMppsIod.SopCommon.SpecificCharacterSet != null)
                    {
                        if (receivedMppsIod.SopCommon.SpecificCharacterSet == null)
                        {
                            anomaly = true;
                            comments += string.Format("Received MPPS Sop instance has no SpecificCharacterSet while it was cached with {0}.\n",
                                                      cachedMppsIod.SopCommon.SpecificCharacterSet);
                            if (logFirstAnomalyOnly) break;
                        }
                        if (cachedMppsIod.SopCommon.SpecificCharacterSet != receivedMppsIod.SopCommon.SpecificCharacterSet)
                        {
                            anomaly = true;
                            comments += "Received MPPS Sop instance has a different SpecificCharacterSet from the one cached .\n";
                            if (logFirstAnomalyOnly) break;
                        }
                    }
                    else
                    {
                        if (receivedMppsIod.SopCommon.SpecificCharacterSet != null)
                        {
                            anomaly = true;
                            comments += "Received MPPS Sop instance has a SpecificCharacterSet while it was cached without.\n";
                            if (logFirstAnomalyOnly) break;
                        }
                    }
                    #endregion 


                } while (false);

                if (anomaly)
                {
                    Platform.Log(LogLevel.Warn, "Invalid Modality Performed Procedute Step SOP N-SET data received from {0} .",
                                                 server.AssociationParams.CallingAE);
                    Platform.Log(LogLevel.Warn, "-- Details : {0}," + comments);
                }
                return anomaly;
            }
            catch (Exception ex)
            {
                Platform.Log(LogLevel.Error, ex, "Exception Occured while checking the the SOPN-SET data  received from {0} .",
                                                   server.AssociationParams.CallingAE);
                anomaly = true;
                return anomaly;
            }            
           
        }


        #endregion
    }
}
