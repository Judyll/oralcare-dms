using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DatabaseLib
{
    public class DbLibPatientManager : IDisposable
    {
        #region Class Constructor and Destructor

        public DbLibPatientManager()
        {

        }

        public void Dispose()
        {
            
        }

        #endregion

        #region Programmer-Defined Void Procedures

        //this procedure inserts a new patient information
        public void InsertPatientInformation(CommonExchange.SysAccess userInfo, ref CommonExchange.Patient patientInfo)
        {
            patientInfo.PatientSystemId = PrimaryKeys.GetNewSysIDPatientInformation(userInfo);

            using (SqlCommand sqlComm = new SqlCommand())
            {
                sqlComm.Connection = userInfo.Connection;
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.CommandText = "dental.InsertPatientInformation";

                sqlComm.Parameters.Add("@sysid_patient", SqlDbType.VarChar).Value = patientInfo.PatientSystemId;
                sqlComm.Parameters.Add("@last_name", SqlDbType.VarChar).Value = patientInfo.LastName;
                sqlComm.Parameters.Add("@first_name", SqlDbType.VarChar).Value = patientInfo.FirstName;
                sqlComm.Parameters.Add("@middle_name", SqlDbType.VarChar).Value = patientInfo.MiddleName;
                sqlComm.Parameters.Add("@home_address", SqlDbType.VarChar).Value = patientInfo.HomeAddress;
                sqlComm.Parameters.Add("@phone_nos", SqlDbType.VarChar).Value = patientInfo.PhoneNos;
                sqlComm.Parameters.Add("@birthdate", SqlDbType.DateTime).Value = DateTime.Parse(patientInfo.BirthDate);
                sqlComm.Parameters.Add("@e_mail", SqlDbType.VarChar).Value = patientInfo.Email;
                sqlComm.Parameters.Add("@medical_history", SqlDbType.VarChar).Value = patientInfo.MedicalHistory;
                sqlComm.Parameters.Add("@emergency_info", SqlDbType.VarChar).Value = patientInfo.EmergencyInfo;

                if (patientInfo.ImageBytes != null && !String.IsNullOrEmpty(patientInfo.ImagePath) && !String.IsNullOrEmpty(patientInfo.ImageExtension))
                {
                    sqlComm.Parameters.Add("@pic", SqlDbType.VarBinary).Value = patientInfo.ImageBytes;
                    sqlComm.Parameters.Add("@extension_name", SqlDbType.VarChar).Value = patientInfo.ImageExtension;
                }
                else
                {
                    sqlComm.Parameters.Add("@pic", SqlDbType.VarBinary).Value = DBNull.Value;
                    sqlComm.Parameters.Add("@extension_name", SqlDbType.VarChar).Value = DBNull.Value;
                }

                sqlComm.Parameters.Add("@created_by", SqlDbType.VarChar).Value = userInfo.UserId;

                sqlComm.ExecuteNonQuery();

            }

        } //------------------------------------

        //this procedure updates a patient information
        public void UpdatePatientInformation(CommonExchange.SysAccess userInfo, CommonExchange.Patient patientInfo)
        {
            using (SqlCommand sqlComm = new SqlCommand())
            {
                sqlComm.Connection = userInfo.Connection;
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.CommandText = "dental.UpdatePatientInformation";

                sqlComm.Parameters.Add("@sysid_patient", SqlDbType.VarChar).Value = patientInfo.PatientSystemId;
                sqlComm.Parameters.Add("@last_name", SqlDbType.VarChar).Value = patientInfo.LastName;
                sqlComm.Parameters.Add("@first_name", SqlDbType.VarChar).Value = patientInfo.FirstName;
                sqlComm.Parameters.Add("@middle_name", SqlDbType.VarChar).Value = patientInfo.MiddleName;
                sqlComm.Parameters.Add("@home_address", SqlDbType.VarChar).Value = patientInfo.HomeAddress;
                sqlComm.Parameters.Add("@phone_nos", SqlDbType.VarChar).Value = patientInfo.PhoneNos;
                sqlComm.Parameters.Add("@birthdate", SqlDbType.DateTime).Value = DateTime.Parse(patientInfo.BirthDate);
                sqlComm.Parameters.Add("@e_mail", SqlDbType.VarChar).Value = patientInfo.Email;
                sqlComm.Parameters.Add("@medical_history", SqlDbType.VarChar).Value = patientInfo.MedicalHistory;
                sqlComm.Parameters.Add("@emergency_info", SqlDbType.VarChar).Value = patientInfo.EmergencyInfo;

                if (patientInfo.ImageBytes != null && !String.IsNullOrEmpty(patientInfo.ImagePath) && !String.IsNullOrEmpty(patientInfo.ImageExtension))
                {
                    sqlComm.Parameters.Add("@pic", SqlDbType.VarBinary).Value = patientInfo.ImageBytes;
                    sqlComm.Parameters.Add("@extension_name", SqlDbType.VarChar).Value = patientInfo.ImageExtension;
                }
                else
                {
                    sqlComm.Parameters.Add("@pic", SqlDbType.VarBinary).Value = DBNull.Value;
                    sqlComm.Parameters.Add("@extension_name", SqlDbType.VarChar).Value = DBNull.Value;
                }

                sqlComm.Parameters.Add("@edited_by", SqlDbType.VarChar).Value = userInfo.UserId;

                sqlComm.ExecuteNonQuery();

            }

        } //------------------------------------

        #endregion

        #region Programmer-Defined Function Procedures

        //this function returns a selected patient information
        public DataTable SelectPatientInformation(CommonExchange.SysAccess userInfo, String queryString)
        {
            DataTable dbTable = new DataTable("SelectedPatientInformationTable");
            dbTable.Columns.Add("sysid_patient", System.Type.GetType("System.String"));
            dbTable.Columns.Add("last_name", System.Type.GetType("System.String"));
            dbTable.Columns.Add("first_name", System.Type.GetType("System.String"));
            dbTable.Columns.Add("middle_name", System.Type.GetType("System.String"));
            dbTable.Columns.Add("home_address", System.Type.GetType("System.String"));
            dbTable.Columns.Add("phone_nos", System.Type.GetType("System.String"));
            dbTable.Columns.Add("birthdate", System.Type.GetType("System.String"));
            dbTable.Columns.Add("e_mail", System.Type.GetType("System.String"));
            dbTable.Columns.Add("medical_history", System.Type.GetType("System.String"));
            dbTable.Columns.Add("emergency_info", System.Type.GetType("System.String"));

            using (SqlCommand sqlComm = new SqlCommand())
            {
                sqlComm.Connection = userInfo.Connection;
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.CommandText = "dental.SelectPatientInformation";

                sqlComm.Parameters.Add("@query_string", SqlDbType.VarChar).Value = queryString;
                sqlComm.Parameters.Add("@system_user_id", SqlDbType.VarChar).Value = userInfo.UserId;

                using (SqlDataReader sqlReader = sqlComm.ExecuteReader())
                {
                    if (sqlReader.HasRows)
                    {
                        while (sqlReader.Read())
                        {
                            DataRow newRow = dbTable.NewRow();

                            newRow["sysid_patient"] = DatabaseLib.ProcStatic.DataReaderConvert(sqlReader, "sysid_patient", "");
                            newRow["last_name"] = DatabaseLib.ProcStatic.DataReaderConvert(sqlReader, "last_name", "");
                            newRow["first_name"] = DatabaseLib.ProcStatic.DataReaderConvert(sqlReader, "first_name", "");
                            newRow["middle_name"] = DatabaseLib.ProcStatic.DataReaderConvert(sqlReader, "middle_name", "");
                            newRow["home_address"] = DatabaseLib.ProcStatic.DataReaderConvert(sqlReader, "home_address", "");
                            newRow["phone_nos"] = DatabaseLib.ProcStatic.DataReaderConvert(sqlReader, "phone_nos", "");
                            newRow["birthdate"] = DatabaseLib.ProcStatic.DataReaderConvert(sqlReader, "birthdate", "");
                            newRow["e_mail"] = DatabaseLib.ProcStatic.DataReaderConvert(sqlReader, "e_mail", "");
                            newRow["medical_history"] = DatabaseLib.ProcStatic.DataReaderConvert(sqlReader, "medical_history", "");
                            newRow["emergency_info"] = DatabaseLib.ProcStatic.DataReaderConvert(sqlReader, "emergency_info", "");

                            dbTable.Rows.Add(newRow);
                        }
                    }

                    sqlReader.Close();
                }

                dbTable.AcceptChanges();
            }

            return dbTable;
        } //--------------------------

        //this function returns a selected patient information by patient id
        public CommonExchange.Patient SelectByPatientSystemIdPatientInformation(CommonExchange.SysAccess userInfo, String patientId)
        {
            CommonExchange.Patient patientInfo = new CommonExchange.Patient();

            using (SqlCommand sqlComm = new SqlCommand())
            {
                sqlComm.Connection = userInfo.Connection;
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.CommandText = "dental.SelectBySysIDPatientPatientInformation";

                sqlComm.Parameters.Add("@sysid_patient", SqlDbType.VarChar).Value = patientId;
                sqlComm.Parameters.Add("@system_user_id", SqlDbType.VarChar).Value = userInfo.UserId;

                using (SqlDataReader sqlReader = sqlComm.ExecuteReader())
                {
                    if (sqlReader.HasRows)
                    {
                        while (sqlReader.Read())
                        {                            
                            patientInfo.PatientSystemId = DatabaseLib.ProcStatic.DataReaderConvert(sqlReader, "sysid_patient", "");
                            patientInfo.LastName = DatabaseLib.ProcStatic.DataReaderConvert(sqlReader, "last_name", "");
                            patientInfo.FirstName = DatabaseLib.ProcStatic.DataReaderConvert(sqlReader, "first_name", "");
                            patientInfo.MiddleName = DatabaseLib.ProcStatic.DataReaderConvert(sqlReader, "middle_name", "");
                            patientInfo.HomeAddress = DatabaseLib.ProcStatic.DataReaderConvert(sqlReader, "home_address", "");
                            patientInfo.PhoneNos = DatabaseLib.ProcStatic.DataReaderConvert(sqlReader, "phone_nos", "");
                            patientInfo.BirthDate = DatabaseLib.ProcStatic.DataReaderConvert(sqlReader, "birthdate", "");
                            patientInfo.Email = DatabaseLib.ProcStatic.DataReaderConvert(sqlReader, "e_mail", "");
                            patientInfo.MedicalHistory = DatabaseLib.ProcStatic.DataReaderConvert(sqlReader, "medical_history", "");
                            patientInfo.EmergencyInfo = DatabaseLib.ProcStatic.DataReaderConvert(sqlReader, "emergency_info", "");

                            break;                           
                        }
                    }

                    sqlReader.Close();
                }
                
            }

            return patientInfo;
        } //--------------------------

        //this function returns the patient image table
        public DataTable SelectImagePatientInformation(CommonExchange.SysAccess userInfo, String patientId)
        {
            DataTable dbTable = new DataTable("PatientImageTable");
            dbTable.Columns.Add("sysid_patient", System.Type.GetType("System.String"));
            dbTable.Columns.Add("extension_name", System.Type.GetType("System.String"));
            dbTable.Columns.Add("pic", System.Type.GetType("System.Byte[]"));

            using (SqlCommand sqlComm = new SqlCommand())
            {
                sqlComm.Connection = userInfo.Connection;
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.CommandText = "dental.SelectImagePatientInformation";

                sqlComm.Parameters.Add("@sysid_patient", SqlDbType.VarChar).Value = patientId;
                sqlComm.Parameters.Add("@system_user_id", SqlDbType.VarChar).Value = userInfo.UserId;

                using (SqlDataReader sqlReader = sqlComm.ExecuteReader(CommandBehavior.SequentialAccess))
                {
                    if (sqlReader.HasRows)
                    {
                        while (sqlReader.Read())
                        {
                            DataRow newRow = dbTable.NewRow();

                            newRow["sysid_patient"] = sqlReader["sysid_patient"].ToString();
                            newRow["extension_name"] = sqlReader["extension_name"].ToString();
                            newRow["pic"] = sqlReader["pic"];

                            dbTable.Rows.Add(newRow);
                        }
                    }

                    sqlReader.Close();
                }

                dbTable.AcceptChanges();
            }

            return dbTable;

        } //--------------------------------

        #endregion
    }
}
