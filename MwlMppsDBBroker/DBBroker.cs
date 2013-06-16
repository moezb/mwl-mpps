using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Xml;
using ClearCanvas.Common;



namespace Edgeteks.MWLServer.DBBroker
{
    [Serializable] // for dumping or logging purpose
    public class ExamsScheduled
    {
        #region Constructors
        public ExamsScheduled()

        { }
        public ExamsScheduled(
             System.Int32 _accessionNumber_
            , System.DateTime _dateOfBirth_
            , System.String _examDescription_
            , System.String _examRoom_
            , System.DateTime _examScheduledDateAndTime_
            , System.Int32 _examStatusId_
            , System.String _forename_
            , System.String _fullName_
            , System.String _hospitalName_
            , System.String _modality_
            , System.Int32 _patientID_
            , System.DateTime _performedProcedureDateAndTime_
            , System.String _performingPhysician_
            , System.Int32 _procedureID_
            , System.Int32 _procedureStepID_
            , System.String _referringPhysician_
            , System.String _scheduledAET_
            , System.String _sex_
            , System.String _surname_
            , System.String _title_
            )
        {
            _accessionNumber = _accessionNumber_;
            _dateOfBirth = _dateOfBirth_;
            _examDescription = _examDescription_;
            _examRoom = _examRoom_;
            _examScheduledDateAndTime = _examScheduledDateAndTime_;
            _examStatusId = _examStatusId_;
            _forename = _forename_;
            _fullName = _fullName_;
            _hospitalName = _hospitalName_;
            _modality = _modality_;
            _patientID = _patientID_;
            _performedProcedureDateAndTime = _performedProcedureDateAndTime_;
            _performingPhysician = _performingPhysician_;
            _procedureID = _procedureID_;
            _procedureStepID = _procedureStepID_;
            _referringPhysician = _referringPhysician_;
            _scheduledAET = _scheduledAET_;
            _sex = _sex_;
            _surname = _surname_;
            _title = _title_;
        }
        #endregion

        #region Private Members
        private Int32 _accessionNumber;
        private DateTime _dateOfBirth;
        private String _examDescription;
        private String _examRoom;
        private DateTime _examScheduledDateAndTime;
        private Int32 _examStatusId;
        private String _forename;
        private String _fullName;
        private String _hospitalName;
        private String _modality;
        private Int32 _patientID;
        private DateTime _performedProcedureDateAndTime;
        private String _performingPhysician;
        private Int32 _procedureID;
        private Int32 _procedureStepID;
        private String _referringPhysician;
        private String _scheduledAET;
        private String _sex;
        private String _surname;
        private String _title;
        #endregion

        #region Public Properties

        public Int32 AccessionNumber
        {
            get { return _accessionNumber; }
            set { _accessionNumber = value; }
        }

        public DateTime DateOfBirth
        {
            get { return _dateOfBirth; }
            set { _dateOfBirth = value; }
        }

        public String ExamDescription
        {
            get { return _examDescription; }
            set { _examDescription = value; }
        }

        public String ExamRoom
        {
            get { return _examRoom; }
            set { _examRoom = value; }
        }

        public DateTime ExamScheduledDateAndTime
        {
            get { return _examScheduledDateAndTime; }
            set { _examScheduledDateAndTime = value; }
        }

        public Int32 ExamStatusId
        {
            get { return _examStatusId; }
            set { _examStatusId = value; }
        }

        public String Forename
        {
            get { return _forename; }
            set { _forename = value; }
        }

        public String FullName
        {
            get { return _fullName; }
            set { _fullName = value; }
        }

        public String HospitalName
        {
            get { return _hospitalName; }
            set { _hospitalName = value; }
        }

        public String Modality
        {
            get { return _modality; }
            set { _modality = value; }
        }

        public Int32 PatientID
        {
            get { return _patientID; }
            set { _patientID = value; }
        }

        public DateTime PerformedProcedureDateAndTime
        {
            get { return _performedProcedureDateAndTime; }
            set { _performedProcedureDateAndTime = value; }
        }

        public String PerformingPhysician
        {
            get { return _performingPhysician; }
            set { _performingPhysician = value; }
        }

        public Int32 ProcedureID
        {
            get { return _procedureID; }
            set { _procedureID = value; }
        }

        public Int32 ProcedureStepID
        {
            get { return _procedureStepID; }
            set { _procedureStepID = value; }
        }

        public String ReferringPhysician
        {
            get { return _referringPhysician; }
            set { _referringPhysician = value; }
        }

        public String ScheduledAET
        {
            get { return _scheduledAET; }
            set { _scheduledAET = value; }
        }

        public String Sex
        {
            get { return _sex; }
            set { _sex = value; }
        }

        public String Surname
        {
            get { return _surname; }
            set { _surname = value; }
        }

        public String Title
        {
            get { return _title; }
            set { _title = value; }
        }

        #endregion

        #region Static Methods

        static public List<ExamsScheduled> GetWorkList(string wherClause)
        {
            System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection();
            conn.ConnectionString = MwlMppsDBBroker.Properties.Settings.Default.ConnectionString;
            List<ExamsScheduled> list = new List<ExamsScheduled>();
            try
            {
                conn.Open();
                string selectStatement = "SELECT * FROM dbo.ExamsScheduled " + wherClause + " Order by Surname, Forename";
                selectStatement = selectStatement.Replace("'", "''");
                string commandText = "EXEC dbo.sp_GetWorkList @Query = '" + selectStatement + "'";
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = commandText;
                SqlDataReader resultSet = cmd.ExecuteReader();
                
                while (resultSet.Read())
                {
                    list.Add(new ExamsScheduled(resultSet.GetInt32(0),
                                                resultSet.GetDateTime(1),
                                                resultSet.GetString(2),
                                                resultSet.GetString(3),
                                                resultSet.GetDateTime(4),
                                                resultSet.GetInt32(5),
                                                resultSet.GetString(6),
                                                resultSet.GetString(7),
                                                resultSet.GetString(8),
                                                resultSet.GetString(9),
                                                resultSet.GetInt32(10),
                                                resultSet.GetDateTime(11),
                                                resultSet.GetString(12),
                                                resultSet.GetInt32(13),
                                                resultSet.GetInt32(14),
                                                resultSet.GetString(15),
                                                resultSet.GetString(16),
                                                resultSet.GetString(17),
                                                resultSet.GetString(18),
                                                resultSet.GetString(19)));

                }
                return list;
            }
            catch (Exception ex)
            {
                Platform.Log(LogLevel.Error, ex);
                return null;
            }
            finally
            {
                if (conn.State != System.Data.ConnectionState.Closed)
                    conn.Close();
            }
        }
        static public bool CreateModalityPerformedProcedureStep(int procedureStepId)
        {
            System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection();
            conn.ConnectionString = MwlMppsDBBroker.Properties.Settings.Default.ConnectionString;
            try
            {
                conn.Open();
                string commandText = string.Format("EXEC dbo.sp_CreateModalityPerformedProcedureStep @psId = {0} ", procedureStepId);
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = commandText;
                int recordAffected = cmd.ExecuteNonQuery();
                conn.Close();
                return true;

            }
            catch (Exception ex)
            {
                Platform.Log(LogLevel.Error, ex);
                return false;
            }
            finally
            {
                if (conn.State != System.Data.ConnectionState.Closed)
                    conn.Close();
            }
        }
        static public bool DiscontinueModalityPerformedProcedureStep(int procedureStepId,XmlElement sequenceXmlDoc)
        {
            SqlConnection conn = new System.Data.SqlClient.SqlConnection();
            conn.ConnectionString = MwlMppsDBBroker.Properties.Settings.Default.ConnectionString;
            SqlTransaction transaction = null;

            try
            {
                conn.Open();
                transaction = conn.BeginTransaction();
                string commandText = string.Format("EXEC dbo.sp_DiscontinueModalityPerformedProcedureStep @psId = {0} ", procedureStepId);
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = commandText;
                int recordAffected = cmd.ExecuteNonQuery();

                bool success = CreatePerformedSeriesSequence(sequenceXmlDoc, transaction);

                if (success)
                    transaction.Commit();
                else
                    transaction.Rollback();
                return true;

            }
            catch (Exception ex)
            {
                Platform.Log(LogLevel.Error, ex);
                if (transaction != null)
                    transaction.Rollback();
                return false;
            }
            finally
            {
                if (conn.State != System.Data.ConnectionState.Closed)
                    conn.Close();
            }
        }
        static public bool SetModalityPerformedProcedureStep(int procedureStepId,XmlElement sequenceXmlDoc)
        {
            SqlConnection conn = new System.Data.SqlClient.SqlConnection();
            conn.ConnectionString = MwlMppsDBBroker.Properties.Settings.Default.ConnectionString;
            SqlTransaction transaction = null;
                
            try
            {
                conn.Open();
                transaction = conn.BeginTransaction();
                string commandText = string.Format("EXEC dbo.sp_SetModalityPerformedProcedureStep @psId = {0} ", procedureStepId);
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = commandText;
                int recordAffected = cmd.ExecuteNonQuery();

                bool success =  CreatePerformedSeriesSequence(sequenceXmlDoc, transaction);

                if (success)
                    transaction.Commit();
                else
                    transaction.Rollback();
                return true;

            }
            catch (Exception ex)
            {
                Platform.Log(LogLevel.Error, ex);
                if (transaction != null) 
                    transaction.Rollback(); 
                return false;
            }
            finally
            {
                if (conn.State != System.Data.ConnectionState.Closed)
                    conn.Close();
            }
        }

        /// <summary>
        /// Create The performed Series sequence passed into  XmlDocument
        /// this should be done in one stored procedure into a single transaction
        /// </summary>
        /// <param name="sequenceXmlDoc">Performed Series Sequence hierarchic structure.</param>
        /// <returns>True if success otherwise it retursn false.</returns>
        static private bool CreatePerformedSeriesSequence(XmlElement sequenceXmlDoc,SqlTransaction transaction)
        {
            System.Data.SqlClient.SqlConnection conn = transaction.Connection;
            try
            {               
                string commandText;

                XmlNodeList xmlSeriesNode = sequenceXmlDoc.ChildNodes;
                for (int i = 0; i < xmlSeriesNode.Count; i++)
                {

                    XmlAttributeCollection xmlAttributes = xmlSeriesNode[i].Attributes;
                    int ProcedureStepID = Int32.Parse(xmlAttributes["ProcedureStepID"].Value);
                    string StudyInstanceUID = xmlAttributes["StudyInstanceUID"].Value;
                    string SeriesInstanceUID = xmlAttributes["SeriesInstanceUID"].Value;
                    string PerformingPhysicianName = xmlAttributes["PerformingPhysicianName"].Value;
                    string OperatorName = xmlAttributes["OperatorName"].Value;
                    string ProtocolName = xmlAttributes["ProtocolName"].Value;
                    string SeriesDescription = xmlAttributes["SeriesDescription"].Value;
                    string RetrieveAET = xmlAttributes["RetrieveAET"].Value;

                    commandText = string.Format("EXEC dbo.sp_InsertPerformedSeriesInstance " +
                    " @ProcedureStepID = {0}," +
                    " @StudyInstanceUID = {1}," +
                    " @SeriesInstanceUID = {2}," +
                    " @PerformingPhysicianName = {3}," +
                    " @OperatorName = {4}," +
                    " @ProtocolName = {5}," +
                    " @SeriesDescription = {6}," +
                    " @RetrieveAET = {7}",
                    ProcedureStepID,
                    StudyInstanceUID,
                    SeriesInstanceUID,
                    PerformingPhysicianName,
                    OperatorName,
                    ProtocolName,
                    SeriesDescription,
                    RetrieveAET);

                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = commandText;
                    cmd.ExecuteNonQuery();

                    // Insert referenced Images of the Series
                    XmlNodeList xmlImagesNode = xmlSeriesNode[i].ChildNodes;

                    for (i = 0; i < xmlImagesNode.Count; i++)
                    {
                        xmlAttributes = xmlImagesNode[i].Attributes;
                        SeriesInstanceUID = xmlAttributes["SeriesInstanceUID"].Value;
                        string ReferencedImageSopInstanceUID = xmlAttributes["ReferencedImageSopInstanceUID"].Value;
                        string ReferencedImageSopClassUID = xmlAttributes["ReferencedImageSopClassUID"].Value;
                        commandText = string.Format("EXEC dbo.sp_InsertReferencedImageInstance " +
                                                    " @SeriesInstanceUID = {0}," +
                                                    " @ReferencedImageSopClassUID = {1} " +
                                                    " @ReferencedImageSopInstanceUID = {2} "+

                                                    SeriesInstanceUID, ReferencedImageSopClassUID,
                                                    ReferencedImageSopInstanceUID);

                        cmd = conn.CreateCommand();
                        cmd.CommandText = commandText;
                        cmd.ExecuteNonQuery();
                    }

                }                
                return true;

            }
            catch (Exception ex)
            {
                Platform.Log(LogLevel.Error, ex);               
                return false;
            }            
        }



        #endregion
    }


}
