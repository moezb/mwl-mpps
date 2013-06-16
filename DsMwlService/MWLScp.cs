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
using System.Collections.Specialized;
using Edgeteks.MWLServer.DBBroker;


namespace Edgeteks.MWLServer
{
    
    /// <summary>
    /// This Class represent a Dicom server that Perform the MWL SCP role. 
    /// </summary>
    public class MWLScp : IDicomServerHandler
    {  

        #region private static members
        // session dumper
        //private static SessionDebugInfo _sessionDebug = new SessionDebugInfo();
        private static bool _started = false;
        private static ServerAssociationParameters _staticAssocParameters;
        private ServerAssociationParameters _assocParameters;        
        // settings
        private static int _bufferedQueryResponses = 50;
        private static int _maxQueryResponses = 100;
        #endregion          
  
        #region Private members
        private bool _cancelReceived = false;      
        private Queue<DicomMessage> _responseQueue = new Queue<DicomMessage>(_bufferedQueryResponses);        
        #endregion
        
        #region Constructors
        private MWLScp(ServerAssociationParameters assoc)
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

            pcid = assoc.AddPresentationContext(SopClass.ModalityWorklistInformationModelFind);
            assoc.AddTransferSyntax(pcid, TransferSyntax.ExplicitVrLittleEndian);
            assoc.AddTransferSyntax(pcid, TransferSyntax.ImplicitVrLittleEndian);
        }
        #endregion

        #region Static Public Methods
        
        public static void StartListening(string aeTitle, int port, int bufferedQueryResponses,int maxQueryResponses)
        {

            _bufferedQueryResponses = bufferedQueryResponses;
            _maxQueryResponses = maxQueryResponses;
            
            if (_started)
                return;

            _staticAssocParameters = new ServerAssociationParameters(aeTitle, new IPEndPoint(IPAddress.Any, port));

            Platform.Log(LogLevel.Info, "MWL Server Started");

            AddPresentationContexts(_staticAssocParameters);

            DicomServer.StartListening(_staticAssocParameters,
                delegate(DicomServer server, ServerAssociationParameters assoc)
                {
                    return new MWLScp(assoc);
                });

            _started = true;
        }
        public static void StopListening(int port)
        {
            if (_started)
            {
                Platform.Log(LogLevel.Info, "Stopping The MWL server...");
                DicomServer.StopListening(_staticAssocParameters);
                _started = false;
                Platform.Log(LogLevel.Info, "MWL Server Stopped.");
            }
        }

        #endregion

        #region MWL SCP processing member Methods
        /// <summary>
        /// Populate data from a <see cref="ExamsScheduledBroker"/> entity into a DICOM C-FIND-RSP message.
        /// </summary>
        /// <param name="response">The response message to populate with results.</param>
        /// <param name="tagList">The list of tags to populate.</param>
        /// <param name="row">The <see cref="Patient"/> table to populate from.</param>
        private void PopulateResponse(DicomMessage response, IList<uint> tagList, ExamsScheduled row,string specificCharacterSet)
        {
            DicomAttributeCollection dataSet = response.DataSet;

            dataSet[DicomTags.RetrieveAeTitle].SetStringValue(this._assocParameters.CalledAE);
            dataSet[DicomTags.SopClassUid].SetStringValue(SopClass.ModalityWorklistInformationModelFindUid);
            dataSet[DicomTags.SopInstanceUid].SetStringValue(DicomUid.GenerateUid().UID);


            if (tagList.Contains(DicomTags.SpecificCharacterSet))
            {
                dataSet[DicomTags.SpecificCharacterSet].SetStringValue(specificCharacterSet);
                dataSet.SpecificCharacterSet = specificCharacterSet; // this will ensure the data is encoded using the specified character set
            }

            foreach (uint tag in tagList)
            {
                try
                {
                    switch (tag)
                    {
                        case DicomTags.QueryRetrieveLevel:
                            dataSet[DicomTags.QueryRetrieveLevel].SetStringValue("WORKLIST");
                            break;

                        //Scheduled Procedure Step module required retrun keys
                        case DicomTags.ScheduledProcedureStepSequence:
                            PopulateScheduledProcedureStepSequence(dataSet[DicomTags.ScheduledProcedureStepSequence] as DicomAttributeSQ, row);
                            break;

                        // Requested Procedure
                        case DicomTags.RequestedProcedureId:
                            dataSet[DicomTags.RequestedProcedureId].SetStringValue(string.Format("{0}",row.ProcedureID));
                            break;
                        case DicomTags.RequestedProcedureDescription:
                            dataSet[DicomTags.RequestedProcedureDescription].SetStringValue(row.ExamDescription);
                            break;
                        case DicomTags.StudyInstanceUid:
                            // The mwl is supposed to generate the StudyInstanceUID;
                            // and may update back (or not the modality worklist)
                            dataSet[DicomTags.StudyInstanceUid].SetStringValue(DicomUid.GenerateUid().UID);
                            break;
                        case DicomTags.ReferencedStudySequence:
                            dataSet[DicomTags.ReferencedStudySequence].SetNullValue();
                            break;
                        case DicomTags.RequestedProcedurePriority:
                            dataSet[DicomTags.RequestedProcedurePriority].SetStringValue("N/A");
                            break;
                        case DicomTags.PatientTransportArrangements:
                            dataSet[DicomTags.PatientTransportArrangements].SetStringValue("N/A");
                            break;
                        //Imaging Service Request
                        case DicomTags.AccessionNumber:                           
                            dataSet[DicomTags.AccessionNumber].SetStringValue(string.Format("{0}",row.AccessionNumber));
                            break;
                        case DicomTags.RequestingPhysician:
                            dataSet[DicomTags.RequestingPhysician].SetStringValue("");
                            break;
                        case DicomTags.ReferringPhysiciansName:
                            dataSet[DicomTags.ReferringPhysiciansName].SetStringValue(row.ReferringPhysician);
                            break;
                        //Visit Identification
                        case DicomTags.AdmissionId:
                            dataSet[DicomTags.AdmissionId].SetStringValue("");
                            break;
                        //Visit Status
                        case DicomTags.CurrentPatientLocation:
                            dataSet[DicomTags.CurrentPatientLocation].SetStringValue("");
                            break;

                        //Visit Relationship required retrun keys
                        case DicomTags.ReferencedSopClassUid:
                            dataSet[DicomTags.ReferencedSopClassUid].SetStringValue(SopClass.ModalityWorklistInformationModelFindUid);
                            break;
                        case DicomTags.ReferencedSopInstanceUid:
                            dataSet[DicomTags.ReferencedSopInstanceUid].SetStringValue(response.AffectedSopInstanceUid);
                            break;
                        case DicomTags.ReferencedPatientSequence:
                            dataSet[DicomTags.ReferencedPatientSequence].SetNullValue();
                            break;

                        //Patient Identification
                        case DicomTags.PatientsName:
                            dataSet[DicomTags.PatientsName].SetStringValue(row.FullName);
                            break;
                        case DicomTags.PatientId:
                            dataSet[DicomTags.PatientId].SetStringValue(string.Format("{0}",row.PatientID));
                            break;
                        //Patient Demographic
                        case DicomTags.PatientsBirthDate:
                            dataSet[DicomTags.PatientsBirthDate].SetStringValue("");
                            break;
                        case DicomTags.PatientsSex:
                            dataSet[DicomTags.PatientsSex].SetStringValue(row.Sex);
                            break;

                        case DicomTags.ConfidentialityConstraintOnPatientDataDescription:
                            dataSet[DicomTags.ConfidentialityConstraintOnPatientDataDescription].SetStringValue("");
                            break;
                        case DicomTags.PatientsWeight:
                            dataSet[DicomTags.PatientsWeight].SetStringValue("");
                            break;
                        //Patient Medical
                        case DicomTags.PatientState:
                            dataSet[DicomTags.PatientState].SetStringValue("");
                            break;
                        case DicomTags.PregnancyStatus:
                            dataSet[DicomTags.PregnancyStatus].SetStringValue("");
                            break;
                        case DicomTags.MedicalAlerts:
                            dataSet[DicomTags.MedicalAlerts].SetStringValue("");
                            break;
                        case DicomTags.Allergies:
                            dataSet[DicomTags.Allergies].SetStringValue("");
                            break;
                        case DicomTags.SpecialNeeds:
                            dataSet[DicomTags.SpecialNeeds].SetStringValue("");
                            break;
                        default:
                            dataSet[tag].SetNullValue();
                            break;
                    }
                }
                catch (Exception e)
                {
                    Platform.Log(LogLevel.Warn, e, "Unexpected error setting tag {0} in C-FIND-RSP",
                                 dataSet[tag].Tag.ToString());
                    dataSet[tag].SetNullValue();
                }
            }
        }

        private void PopulateScheduledProcedureStepSequence(DicomAttributeSQ dicomAttribute, ExamsScheduled row)
        {
            DicomSequenceItem item;

            item = new DicomSequenceItem();

            item[DicomTags.ScheduledStationAeTitle].SetStringValue(row.ScheduledAET);
            item[DicomTags.ScheduledProcedureStepStartDate].SetStringValue(GetDateStringOnly(row.ExamScheduledDateAndTime));
            item[DicomTags.ScheduledProcedureStepStartTime].SetStringValue(GetTimeStringOnly(row.ExamScheduledDateAndTime));
            item[DicomTags.Modality].SetStringValue(row.Modality);
            item[DicomTags.PerformedProcedureStepId].SetStringValue(string.Format("{0}",row.ProcedureStepID));//O M K supported
            item[DicomTags.ScheduledProcedureStepLocation].SetStringValue(row.ExamRoom);//return type 2 -O M K supported
            item[DicomTags.ScheduledProcedureStepDescription].SetStringValue(row.ExamDescription);//return type 2 -O M K supported
            dicomAttribute.AddSequenceItem(item);            
        }

        private string GetTimeStringOnly(DateTime dateTime)
        {
            return string.Format("{0}{1}{2}", dateTime.Year, dateTime.Month, dateTime.Day);
        }

        private string GetDateStringOnly(DateTime dateTime)
        {
            return string.Format("{0}{1}{2}", dateTime.Hour, dateTime.Minute, dateTime.Second);
        }   

        private void SendBufferedResponses(DicomServer server, byte presentationId, DicomMessage requestMessage,DicomStatus pendingStatus)
        {
            while (_responseQueue.Count > 0)
            {
                DicomMessage response = _responseQueue.Dequeue();
                //_sessionDebug._responses.Add(response.Dump());
                server.SendCFindResponse(presentationId, requestMessage.MessageId, response,
                         pendingStatus);
                Platform.Log(LogLevel.Info, "Sending a Worklist Response ");
                if (_cancelReceived)
                    throw new DicomException("DICOM C-Cancel Received");
            }
        }         

        private void OnReceiveMWLQuery(DicomServer server, byte presentationID, DicomMessage message)
        {
            #region Non conformance checking    - Unsopported Optional matching key checking
            
            if (CheckForMissingRequiredMatchingKey(server, presentationID, message)) 
                return; //this mean sending failure message response.

            DicomStatus pendingStatus = DicomStatuses.Pending;
            
            bool optionalMatchingKeyPresentButNotSupported =
                        CheckForUnSupportedOptionalMatchingKey(server, presentationID, message, false);
            
            if (optionalMatchingKeyPresentButNotSupported)
            {
                pendingStatus = DicomStatuses.QueryRetrieveOptionalKeysNotSupported;  
            }
            #endregion
           
            List<uint> tagList = new List<uint>();           
            
            string sqlWhereClause = " where 1=1 ";            

            DicomAttributeCollection data = message.DataSet;

            string specificCharacterSet="";

            foreach (DicomAttribute attrib in message.DataSet)
            {                
                tagList.Add(attrib.Tag.TagValue);               
                
                if (!attrib.IsNull)
                    switch (attrib.Tag.TagValue)
                    {
                        case DicomTags.SpecificCharacterSet:
                            specificCharacterSet = data[DicomTags.PatientId].GetString(0, string.Empty);
                            break;

                        #region Case Scheduled procedure Step Seq
                        case DicomTags.ScheduledProcedureStepSequence:
                            DicomAttributeSQ sequence = attrib as DicomAttributeSQ;
                            DicomSequenceItem sequenceSubItems = sequence[0];
                            foreach (DicomAttribute seqAttrib in sequenceSubItems)
                            {
                                if (seqAttrib.IsNull) continue;

                                tagList.Add(seqAttrib.Tag.TagValue);
                                switch (seqAttrib.Tag.TagValue)
                                {
                                    // Required matching keys in ScheduledProcedureStepSequence
                                    case DicomTags.ScheduledStationName:
                                        SetStringCondition(ref sqlWhereClause, "ScheduledAET",
                                            sequenceSubItems[DicomTags.ScheduledStationName].GetString(0, string.Empty));
                                        break;
                                    case DicomTags.Modality:
                                        SetStringCondition(ref sqlWhereClause, "Modality",
                                            sequenceSubItems[DicomTags.Modality].GetString(0, string.Empty));
                                        break;
                                    case DicomTags.ScheduledPerformingPhysiciansName:
                                        SetStringCondition(ref sqlWhereClause, "PerformingPhysician",
                                            sequenceSubItems[DicomTags.ScheduledPerformingPhysiciansName].GetString(0, string.Empty));
                                        break;
                                    case DicomTags.ScheduledProcedureStepStartDate:
                                        SetDateRangeCondition(ref sqlWhereClause, "ExamScheduledDateAndTime",
                                        sequenceSubItems[DicomTags.ScheduledProcedureStepStartDate].GetString(0, string.Empty));
                                        break;
                                    case DicomTags.ScheduledProcedureStepStartTime:
                                        SetTimeRangeCondition(ref sqlWhereClause, "ExamScheduledDateAndTime",
                                            sequenceSubItems[DicomTags.ScheduledProcedureStepStartTime].GetString(0, string.Empty));
                                        break;

                                    // Optional matching keys
                                    case DicomTags.ScheduledProcedureStepLocation:
                                        SetTimeRangeCondition(ref sqlWhereClause, "ExamRoom",
                                            sequenceSubItems[DicomTags.ScheduledProcedureStepLocation].GetString(0, string.Empty));
                                        break;

                                    case DicomTags.ScheduledProcedureStepDescription:
                                        SetTimeRangeCondition(ref sqlWhereClause, "ExamDescription",
                                            sequenceSubItems[DicomTags.ScheduledProcedureStepDescription].GetString(0, string.Empty));
                                        break;
                                    case DicomTags.RequestedProcedureId:
                                        SetTimeRangeCondition(ref sqlWhereClause, "ProcedureID",
                                            sequenceSubItems[DicomTags.RequestedProcedureId].GetString(0, string.Empty));
                                        break;
                                    case DicomTags.ScheduledProcedureStepId:
                                        SetTimeRangeCondition(ref sqlWhereClause, "ProcedureStepID",
                                            sequenceSubItems[DicomTags.ScheduledProcedureStepId].GetString(0, string.Empty));
                                        break;
                                    default:
                                        break;
                                }

                            }
                            break;
                        #endregion

                        case DicomTags.PatientId:
                            SetInt32Condition(ref sqlWhereClause, "PatientID",
                                               data[DicomTags.PatientId].GetString(0, string.Empty));
                            break;

                        case DicomTags.PatientsName:
                            SetStringCondition(ref sqlWhereClause, "FullName",
                                               data[DicomTags.PatientsName].GetString(0, string.Empty));
                            break;

                        // Optional matching keys                             
                        case DicomTags.AccessionNumber:
                            SetInt32Condition(ref sqlWhereClause, "AccessionNumber",
                                data[DicomTags.AccessionNumber].GetString(0, string.Empty));
                            break;
                        case DicomTags.ReferringPhysiciansName:
                            SetInt32Condition(ref sqlWhereClause, "ReferringPhysician",
                                data[DicomTags.ReferringPhysiciansName].GetString(0, string.Empty));
                            break;
                        default:
                            break;
                    }
                }

                int resultCount = 0;
                try
                {
                    //_sessionDebug._searchCriteriaDumpString = sqlWhereClause;
                    List<ExamsScheduled> list =  DBBroker.ExamsScheduled.GetWorkList(sqlWhereClause);
                    foreach (ExamsScheduled row in list)
                    {
                        if (_cancelReceived)
                            throw new DicomException("DICOM C-Cancel Received");

                        resultCount++;
                        if (_maxQueryResponses != -1
                            && _maxQueryResponses < resultCount)
                        {
                            SendBufferedResponses(server, presentationID, message, pendingStatus);
                            throw new DicomException("Maximum Configured Query Responses Exceeded: " + resultCount);
                        }

                        DicomMessage response = new DicomMessage();
                        PopulateResponse(response, tagList, row, specificCharacterSet);
                        _responseQueue.Enqueue(response);

                        if (_responseQueue.Count >= _bufferedQueryResponses)
                            SendBufferedResponses(server, presentationID, message, pendingStatus);
                        
                    }

                    SendBufferedResponses(server, presentationID, message, pendingStatus);
                    Platform.Log(LogLevel.Info, "All Worklist have been sent successfully.");                     

                }
                catch (Exception e)
                {
                    if (_cancelReceived)
                    {
                        DicomMessage errorResponse = new DicomMessage();
                        server.SendCFindResponse(presentationID, message.MessageId, errorResponse,
                                                 DicomStatuses.Cancel);
                    }
                    else if (_maxQueryResponses != -1
                          && _maxQueryResponses < resultCount)
                    {
                        Platform.Log(LogLevel.Warn, "Maximum Configured Query Responses Exceeded: {0} on query from {1}", resultCount, server.AssociationParams.CallingAE);
                        DicomMessage errorResponse = new DicomMessage();
                        server.SendCFindResponse(presentationID, message.MessageId, errorResponse,
                                                 DicomStatuses.QueryRetrieveOutOfResources);                       
                    }
                    else
                    {
                        Platform.Log(LogLevel.Error, e, "Unexpected exception when processing FIND request.");
                        DicomMessage errorResponse = new DicomMessage();
                        server.SendCFindResponse(presentationID, message.MessageId, errorResponse,
                                                 DicomStatuses.QueryRetrieveUnableToProcess);                        
                    }
                    return;
                }
             

            DicomMessage finalResponse = new DicomMessage();
            server.SendCFindResponse(presentationID, message.MessageId, finalResponse, DicomStatuses.Success);            
            return;
        }

        private void SetDateRangeCondition(ref string sqlWhereClause, string columnName, string val)
        {
            if (val.Length == 0)
                return;
            
            if (val.Contains("-"))
            {
                string[] vals = val.Split(new char[] { '-' });
                if (val.IndexOf('-') == 0)
                    //cond.LessThanOrEqualTo(vals[1]); 
                    sqlWhereClause+= "AND  DATEDIFF(day,CONVERT(CHAR(8),"+columnName+",112) , CONVERT(CHAR(8),'"+ vals[1] +"',112)) <= 0";
                else if (val.IndexOf('-') == val.Length - 1)
                    //cond.MoreThanOrEqualTo(vals[0]);
                    sqlWhereClause += "AND  DATEDIFF(day,CONVERT(CHAR(8)," + columnName + ",112) , CONVERT(CHAR(8),'" + vals[0] + "',112)) >= 0";
                else
                    //cond.Between(vals[0], vals[1]);
                    sqlWhereClause += "AND  DATEDIFF(day,CONVERT(CHAR(8)," + columnName + ",112) , CONVERT(CHAR(8),'" + vals[0] + "',112)) >= 0  AND "+
                                       " DATEDIFF(day,CONVERT(CHAR(8),"+columnName+",112) , CONVERT(CHAR(8),'"+ vals[1] +"',112)) <= 0";
            }
            else
                //cond.EqualTo(val);
                sqlWhereClause += "AND CONVERT(CHAR(8)," + columnName + ",112)  = '" +val+ "'";
        }

        private void SetTimeRangeCondition(ref string sqlWhereClause, string columnName, string val)
        {
            if (val.Length == 0)
                return;
            if (val.Contains("-"))
            {
                string[] vals = val.Split(new char[] { '-' });
                if (val.IndexOf('-') == 0)
                    //cond.LessThanOrEqualTo(vals[1]); 
                    sqlWhereClause += "AND  DATEDIFF(second,CONVERT(CHAR(8)," + columnName + ",8) , CONVERT(CHAR(8),SUBSTRING('" + vals[1] + "',1, 2)+':'+SUBSTRING('" + vals[1] + "',3, 2)+':'+SUBSTRING('" + vals[1] + "',5, 2),8)) <= 0";
                else if (val.IndexOf('-') == val.Length - 1)
                    //cond.MoreThanOrEqualTo(vals[0]);
                    sqlWhereClause += "AND  DATEDIFF(second,CONVERT(CHAR(8)," + columnName + ",8) , CONVERT(CHAR(8),SUBSTRING('" + vals[0] + "',1, 2)+':'+SUBSTRING('" + vals[0] + "',3, 2)+':'+SUBSTRING('" + vals[0] + "',5, 2),8)) >= 0";
                else
                    //cond.Between(vals[0], vals[1]);
                    sqlWhereClause += "AND  DATEDIFF(second,CONVERT(CHAR(8)," + columnName + ",8) , CONVERT(CHAR(8),SUBSTRING('" + vals[1] + "',1, 2)+':'+SUBSTRING('" + vals[1] + "',3, 2)+':'+SUBSTRING('" + vals[1] + "',5, 2),8)) <= 0 AND " +
                " DATEDIFF(second,CONVERT(CHAR(8)," + columnName + ",8) , CONVERT(CHAR(8),SUBSTRING('" + vals[0] + "',1, 2)+':'+SUBSTRING('" + vals[0] + "',3, 2)+':'+SUBSTRING('" + vals[0] + "',5, 2),8)) >= 0";  
            }
            else
                //cond.EqualTo(val);
                sqlWhereClause += "AND CONVERT(CHAR(8)," + columnName + ",8)  = CONVERT(CHAR(8),SUBSTRING('" + val + "',1, 2)+':'+SUBSTRING('" + val + "',3, 2)+':'+SUBSTRING('" + val + "',5, 2),8))";
        }

        private void SetInt32Condition(ref string sqlWhereClause, string columnName, string val)
        {
            if (val.Length == 0)
                return;

            if (val.Contains("*") || val.Contains("?"))
            {
                String value = val.Replace('*', '%');
                value = value.Replace('?', '_');
                sqlWhereClause += " AND " + "RTRIM(LTRIM(CAST("+columnName + " AS VARCHAR(11))))"+ " LIKE '" + value.Replace("'", "''") + "'";
            }
            else
                sqlWhereClause += " AND " + "RTRIM(LTRIM(CAST(" + columnName + " AS VARCHAR(11))))" + " = " + "'" + val.Replace("'", "''") + "'";
        }

        private void SetStringCondition(ref string sqlWhereClause, string columnName, string val)
        {
            if (val.Length == 0)
                return;

            if (val.Contains("*") || val.Contains("?"))
            {
                String value = val.Replace('*', '%');
                value = value.Replace('?', '_');
                sqlWhereClause += " AND "+ columnName + " LIKE '"+ value.Replace("'","''")+"'";
            }
            else
                sqlWhereClause += " AND " + columnName + " = " + "'" + val.Replace("'", "''") + "'";
        }

        #endregion


        private bool CheckForUnSupportedOptionalMatchingKey(DicomServer server, byte presentationID, DicomMessage message,
                                                            bool logFirstUnsupportedAttributeOnly)
        {
            DicomAttribute attrib;
            bool UnsupportedOptionalMatchingDetected = false;
            string comments = "";
            
            do
            {
               // ModalityWorklistIod modalityWorklistIod = new ModalityWorklistIod(message.DataSet);

                attrib = message.DataSet[DicomTags.ScheduledProcedureStepSequence];
                
                DicomAttributeSQ sequence = attrib as DicomAttributeSQ; 
                
                DicomSequenceItem sequenceSubItems = sequence[0]; //sequence supposed non empty as 
                                                                  // it have to be checked by the function
                                                                  // CheckForMissingRequiredMatchingKey() first.

                if (!sequenceSubItems[DicomTags.ScheduledProcedureStepDescription].IsNull)
                {
                    UnsupportedOptionalMatchingDetected = true;
                    comments+= "Scheduled Procedure Step Description\n";
                    if (logFirstUnsupportedAttributeOnly) break;
                }

                if (!sequenceSubItems[DicomTags.ScheduledStationName].IsNull)
                {
                    UnsupportedOptionalMatchingDetected = true;
                    comments += "Scheduled Station Name\n";
                    if (logFirstUnsupportedAttributeOnly) break;
                }

                if (!sequenceSubItems[DicomTags.ScheduledProcedureStepLocation].IsNull)
                {
                    UnsupportedOptionalMatchingDetected = true;
                    comments+="Scheduled Procedure Step Location\n";
                    if (logFirstUnsupportedAttributeOnly) break;
                }

                if (!sequenceSubItems[DicomTags.ScheduledProtocolCodeSequence].IsNull)
                {
                    UnsupportedOptionalMatchingDetected = true;
                    comments+="Scheduled Protocol Code Sequence\n";
                    if (logFirstUnsupportedAttributeOnly) break;
                }

                if (!sequenceSubItems[DicomTags.PreMedication].IsNull)
                {
                    UnsupportedOptionalMatchingDetected = true;
                    comments+="PreMedication\n";
                    if (logFirstUnsupportedAttributeOnly) break;
                }

                if (!sequenceSubItems[DicomTags.RequestedContrastAgent].IsNull)
                {
                    UnsupportedOptionalMatchingDetected = true;
                    comments+="Requested Contrast Agent\n";
                    if (logFirstUnsupportedAttributeOnly) break;
                }
                if (!sequenceSubItems[DicomTags.RequestedContrastAgent].IsNull)
                {
                    UnsupportedOptionalMatchingDetected = true;
                    comments+="Requested Contrast Agent\n";
                    if (logFirstUnsupportedAttributeOnly) break;
                }

                if (!sequenceSubItems[DicomTags.ScheduledProcedureStepStatus].IsNull)
                {
                    UnsupportedOptionalMatchingDetected = true;
                    comments+="Scheduled Procedure Step Status\n";
                    if (logFirstUnsupportedAttributeOnly) break;
                }

                if (!sequenceSubItems[DicomTags.CommentsOnTheScheduledProcedureStep].IsNull)
                {
                    UnsupportedOptionalMatchingDetected = true;
                    comments+="Comments On The Scheduled Procedure Step\n";
                    if (logFirstUnsupportedAttributeOnly) break;
                }

                //TODO:  verify the rest of the Optional Matching keys against  your database existing fields in order 
                // to send appropriate status. 

            } while (false);

            // send specific error status to the calling AE
            if (UnsupportedOptionalMatchingDetected)
            {
                Platform.Log(LogLevel.Warn, "One or more Optional matching Sent by {0} key are note supported." ,                                             
                                             server.AssociationParams.CallingAE);
                Platform.Log(LogLevel.Warn, "Unsupported Optional Matching Key Attributes Details : {0}," + comments);
                
            }
            return UnsupportedOptionalMatchingDetected;  

        }

        private bool CheckForMissingRequiredMatchingKey(DicomServer server, byte presentationID, DicomMessage message)
        {
            DicomAttribute attrib;
            bool requiredMatchingKeyMissing = false;
            string  comment=""; // will receive a description of the first encountred missing r key.
                                // we don't need to collect all missing keys to speed up processing.
            do
	        {
                attrib = message.DataSet[DicomTags.ScheduledProcedureStepSequence];
                if (attrib.IsNull)
                {
                    requiredMatchingKeyMissing = true;
                    comment = "Missing Scheduled Procedure Step Sequence";
                    break;
                }
                DicomAttributeSQ sequence = attrib as DicomAttributeSQ;
                if (attrib.Count == 0)
                {
                   requiredMatchingKeyMissing = true;                 
                   comment = "Scheduled Procedure Step Sequence is empty";
                   break;
                }
                if (attrib.Count > 1)
                {
                   requiredMatchingKeyMissing = true;                 
                   comment = "Found Multiple Items in Scheduled Procedure Step Sequence";
                   break;
                }
                
                DicomSequenceItem sequenceSubItems = sequence[0];
                
                if (sequenceSubItems[DicomTags.ScheduledStationAeTitle].IsNull)
                {
                   requiredMatchingKeyMissing = true;
                   comment = "Missing Scheduled Station Ae Title";
                   break;
                }

                if (sequenceSubItems[DicomTags.Modality].IsNull)
                {
                   requiredMatchingKeyMissing = true;                 
                   comment = "Missing Modality";
                   break;
                }

                if (sequenceSubItems[DicomTags.ScheduledPerformingPhysiciansName].IsNull)
                {
                   requiredMatchingKeyMissing = true;                 
                   comment = "Missing Scheduled Performing Physicians Name";
                   break;
                }

                if (sequenceSubItems[DicomTags.ScheduledProcedureStepStartDate].IsNull)
                {
                   requiredMatchingKeyMissing = true;                 
                   comment = "Missing ScheduledProcedureStepStartDate";
                   break;
                }

                if (sequenceSubItems[DicomTags.ScheduledProcedureStepStartTime].IsNull)
                {
                   requiredMatchingKeyMissing = true;                 
                   comment = "Missing Scheduled Procedure Step Start Time";
                   break;
                }
	         
	        } while (false);

             // send specific error status to the calling AE
            if (requiredMatchingKeyMissing)
            {
                Platform.Log(LogLevel.Error, "Required matching key missing on query from {0},"+
                                             "\n Sending Failure Status Identifier Does Not Match SOPClass.",
                                             server.AssociationParams.CallingAE);
                Platform.Log(LogLevel.Error, "Error Details : {0},"+ comment);
                DicomMessage errorResponse = new DicomMessage();
                server.SendCFindResponse(presentationID, message.MessageId, errorResponse,
                                     DicomStatuses.QueryRetrieveIdentifierDoesNotMatchSOPClass);                 
            }

            return requiredMatchingKeyMissing;            
        }


        #region IDicomServerHandler Member

        void IDicomServerHandler.OnReceiveAssociateRequest(DicomServer server, ServerAssociationParameters association)
        {
            //_sessionDebug.SetAssociationDumpString(association);
            server.SendAssociateAccept(association);
            Platform.Log(LogLevel.Info,string.Format("Accepting association between {0} and {1}.",
                             association.CallingAE, association.CalledAE));
            
        }

        void IDicomServerHandler.OnReceiveRequestMessage(DicomServer server, ServerAssociationParameters association, byte presentationID, DicomMessage message)
        {
            //_sessionDebug.SetAssociationDumpString(association);
            //_sessionDebug._request = message.Dump();

            #region Cancel request
            if (message.CommandField == DicomCommandField.CCancelRequest)
            {
                Platform.Log(LogLevel.Info,string.Format("Received CANCEL-RQ message from {0}.", association.CallingAE));
                _cancelReceived = true;
                return;
            }
            #endregion

            #region CEcho request
            if (message.CommandField == DicomCommandField.CEchoRequest)
            {
                server.SendCEchoResponse(presentationID, message.MessageId, DicomStatuses.Success);
                Platform.Log(LogLevel.Info,string.Format("Received ECHO-RQ message from {0}.", association.CallingAE));
                return;
            }
            #endregion

            #region MWL C-FIND request
            if (message.CommandField == DicomCommandField.CFindRequest)
            {                
                Platform.Log(LogLevel.Info,string.Format("Message Dumped :\n" + message.Dump("", DicomDumpOptions.KeepGroupLengthElements)));

                String level = message.DataSet[DicomTags.QueryRetrieveLevel].GetString(0, string.Empty);

                _cancelReceived = false;

                if (message.AffectedSopClassUid.Equals(SopClass.ModalityWorklistInformationModelFindUid))
                    OnReceiveMWLQuery(server, presentationID, message);                    
                else
                   // Not supported message type, send a failure status.
                    server.SendCFindResponse(presentationID, message.MessageId, new DicomMessage(),
                                                DicomStatuses.QueryRetrieveIdentifierDoesNotMatchSOPClass);
                return;

            }
            #endregion          

            //ignore all unsupported request

            server.SendAssociateAbort(DicomAbortSource.ServiceProvider, DicomAbortReason.UnexpectedPDU);
            Platform.Log(LogLevel.Info,string.Format("Unexpected Command. Send Associate Abort message from server to {0}.", association.CallingAE));
            return; 
           
        }
     
        void IDicomServerHandler.OnReceiveResponseMessage(DicomServer server, ServerAssociationParameters association, byte presentationID, DicomMessage message)
        {
            Platform.Log(LogLevel.Info,string.Format("Unexpectedly received response mess on server."));
            server.SendAssociateAbort(DicomAbortSource.ServiceUser, DicomAbortReason.UnrecognizedPDU);
            //_sessionDebug.DumpSession();
           
        }

        void IDicomServerHandler.OnReceiveReleaseRequest(DicomServer server, ServerAssociationParameters association)
        {            
            Platform.Log(LogLevel.Info,string.Format("Received association release request from  {0}.", association.CallingAE));
            //_sessionDebug.DumpSession();          
        }

        void IDicomServerHandler.OnReceiveAbort(DicomServer server, ServerAssociationParameters association, DicomAbortSource source, DicomAbortReason reason)
        {
            _cancelReceived = true;
            Platform.Log(LogLevel.Info,string.Format("Unexpected association abort received."));
            //_sessionDebug.DumpSession();
           
        }

        void IDicomServerHandler.OnNetworkError(DicomServer server, ServerAssociationParameters association, Exception e)
        {
            Platform.Log(LogLevel.Error,e,string.Format("Unexpected network error over association from {0}.", association.CallingAE));
            //_sessionDebug.DumpSession();          
            
        }

        void IDicomServerHandler.OnDimseTimeout(DicomServer server, ServerAssociationParameters association)
        {
            
            Platform.Log(LogLevel.Info,string.Format("Received DIMSE Timeout, continuing listening for messages"));
            //_sessionDebug.DumpSession();
           
        }

        protected void LogAssociationStatistics(ServerAssociationParameters association)
        {

        }

        #endregion  
    }
}
