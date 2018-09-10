using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DatabaseLib
{
    public class DbLibRegistrationManager : IDisposable
    {
        #region Class Constructor and Destructor
        public DbLibRegistrationManager()
        {
        }
        
        public void Dispose()
        {
           
        }

        #endregion

        #region Programmer-Defined Void Procedures
        //this procedure inserts a new patient registration
        public void InsertPatientRegistration(CommonExchange.SysAccess userInfo, ref CommonExchange.Registration regInfo)
        {
            regInfo.RegistrationSystemId = PrimaryKeys.GetNewSysIDPatientRegistration(userInfo);

            using (SqlCommand sqlComm = new SqlCommand())
            {
                sqlComm.Connection = userInfo.Connection;
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.CommandText = "dental.InsertPatientRegistration";

                sqlComm.Parameters.Add("@sysid_registration", SqlDbType.VarChar).Value = regInfo.RegistrationSystemId;
                sqlComm.Parameters.Add("@sysid_patient", SqlDbType.VarChar).Value = regInfo.PatientSystemId;
                sqlComm.Parameters.Add("@registration_date", SqlDbType.DateTime).Value = DateTime.Parse(regInfo.RegistrationDate);

                sqlComm.Parameters.Add("@created_by", SqlDbType.VarChar).Value = userInfo.UserId;

                sqlComm.ExecuteNonQuery();
            }

        } //------------------------------

        //this procedure updates a patient registration
        public void UpdatePatientRegistration(CommonExchange.SysAccess userInfo, CommonExchange.Registration regInfo)
        {
            using (SqlCommand sqlComm = new SqlCommand())
            {
                sqlComm.Connection = userInfo.Connection;
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.CommandText = "dental.UpdatePatientRegistration";

                sqlComm.Parameters.Add("@sysid_registration", SqlDbType.VarChar).Value = regInfo.RegistrationSystemId;
                sqlComm.Parameters.Add("@registration_date", SqlDbType.DateTime).Value = DateTime.Parse(regInfo.RegistrationDate);

                sqlComm.Parameters.Add("@edited_by", SqlDbType.VarChar).Value = userInfo.UserId;

                sqlComm.ExecuteNonQuery();
            }

        } //------------------------------

        //this procedure updates a patient medical prescription
        public void UpdateMedicalPrescriptionPatientRegistration(CommonExchange.SysAccess userInfo, CommonExchange.Registration regInfo)
        {
            using (SqlCommand sqlComm = new SqlCommand())
            {
                sqlComm.Connection = userInfo.Connection;
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.CommandText = "dental.UpdateMedicalPrescriptionPatientRegistration";

                sqlComm.Parameters.Add("@sysid_registration", SqlDbType.VarChar).Value = regInfo.RegistrationSystemId;
                sqlComm.Parameters.Add("@medical_prescription", SqlDbType.VarChar).Value = regInfo.MedicalPrescription;

                sqlComm.Parameters.Add("@edited_by", SqlDbType.VarChar).Value = userInfo.UserId;

                sqlComm.ExecuteNonQuery();
            }
        } //-----------------------------------

        //this procedure inserts a new registration details
        public void InsertRegistrationDetails(CommonExchange.SysAccess userInfo, CommonExchange.RegistrationDetails detailsInfo)
        {
            using (SqlCommand sqlComm = new SqlCommand())
            {
                sqlComm.Connection = userInfo.Connection;
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.CommandText = "dental.InsertRegistrationDetails";

                sqlComm.Parameters.Add("@sysid_registration", SqlDbType.VarChar).Value = detailsInfo.RegistrationSystemId;
                sqlComm.Parameters.Add("@sysid_procedure", SqlDbType.VarChar).Value = detailsInfo.ProcedureSystemId;
                sqlComm.Parameters.Add("@date_administered", SqlDbType.DateTime).Value = DateTime.Parse(detailsInfo.DateAdministered);
                sqlComm.Parameters.Add("@tooth_no", SqlDbType.VarChar).Value = detailsInfo.ToothNo;
                sqlComm.Parameters.Add("@amount", SqlDbType.Decimal).Value = detailsInfo.Amount;
                sqlComm.Parameters.Add("@remarks", SqlDbType.VarChar).Value = detailsInfo.Remarks;

                sqlComm.Parameters.Add("@created_by", SqlDbType.VarChar).Value = userInfo.UserId;

                sqlComm.ExecuteNonQuery();
            }
        } //-----------------------------

        //this procedure updates a registration details
        public void UpdateRegistrationDetails(CommonExchange.SysAccess userInfo, CommonExchange.RegistrationDetails detailsInfo)
        {
            using (SqlCommand sqlComm = new SqlCommand())
            {
                sqlComm.Connection = userInfo.Connection;
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.CommandText = "dental.UpdateRegistrationDetails";

                sqlComm.Parameters.Add("@details_id", SqlDbType.BigInt).Value = detailsInfo.DetailsId;
                sqlComm.Parameters.Add("@sysid_procedure", SqlDbType.VarChar).Value = detailsInfo.ProcedureSystemId;
                sqlComm.Parameters.Add("@date_administered", SqlDbType.DateTime).Value = DateTime.Parse(detailsInfo.DateAdministered);
                sqlComm.Parameters.Add("@tooth_no", SqlDbType.VarChar).Value = detailsInfo.ToothNo;
                sqlComm.Parameters.Add("@amount", SqlDbType.Decimal).Value = detailsInfo.Amount;
                sqlComm.Parameters.Add("@remarks", SqlDbType.VarChar).Value = detailsInfo.Remarks;

                sqlComm.Parameters.Add("@edited_by", SqlDbType.VarChar).Value = userInfo.UserId;

                sqlComm.ExecuteNonQuery();
            }
        } //-----------------------------

        //this procedure deletes a registration details
        public void DeleteRegistrationDetails(CommonExchange.SysAccess userInfo, CommonExchange.RegistrationDetails detailsInfo)
        {
            using (SqlCommand sqlComm = new SqlCommand())
            {
                sqlComm.Connection = userInfo.Connection;
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.CommandText = "dental.DeleteRegistrationDetails";

                sqlComm.Parameters.Add("@details_id", SqlDbType.BigInt).Value = detailsInfo.DetailsId;

                sqlComm.Parameters.Add("@deleted_by", SqlDbType.VarChar).Value = userInfo.UserId;

                sqlComm.ExecuteNonQuery();
            }
        } //-----------------------------

        //this procedure inserts a new payment details
        public void InsertPaymentDetails(CommonExchange.SysAccess userInfo, ref CommonExchange.PaymentDetails paymentInfo)
        {
            paymentInfo.ReceiptNo = PrimaryKeys.GetNewReceiptNoPaymentDetails(userInfo);

            using (SqlCommand sqlComm = new SqlCommand())
            {
                sqlComm.Connection = userInfo.Connection;
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.CommandText = "dental.InsertPaymentDetails";

                sqlComm.Parameters.Add("@receipt_no", SqlDbType.VarChar).Value = paymentInfo.ReceiptNo;
                sqlComm.Parameters.Add("@sysid_registration", SqlDbType.VarChar).Value = paymentInfo.RegistrationSystemId;
                sqlComm.Parameters.Add("@date_paid", SqlDbType.DateTime).Value = DateTime.Parse(paymentInfo.DatePaid);
                sqlComm.Parameters.Add("@amount", SqlDbType.Decimal).Value = paymentInfo.Amount;
                sqlComm.Parameters.Add("@discount", SqlDbType.Decimal).Value = paymentInfo.Discount;
                sqlComm.Parameters.Add("@payment_type", SqlDbType.TinyInt).Value = paymentInfo.PaymentType;
                sqlComm.Parameters.Add("@bank_name", SqlDbType.VarChar).Value = paymentInfo.BankName;
                sqlComm.Parameters.Add("@check_no", SqlDbType.VarChar).Value = paymentInfo.CheckNo;
                sqlComm.Parameters.Add("@card_number", SqlDbType.VarChar).Value = paymentInfo.CardNumber;
                sqlComm.Parameters.Add("@card_type", SqlDbType.VarChar).Value = paymentInfo.CardType;
                sqlComm.Parameters.Add("@card_expire", SqlDbType.VarChar).Value = paymentInfo.CardExpire;

                sqlComm.Parameters.Add("@created_by", SqlDbType.VarChar).Value = userInfo.UserId;

                sqlComm.ExecuteNonQuery();
            }

        } //------------------------------

        //this procedure inserts a new payment details
        public void UpdatePaymentDetails(CommonExchange.SysAccess userInfo, CommonExchange.PaymentDetails paymentInfo)
        {
            using (SqlCommand sqlComm = new SqlCommand())
            {
                sqlComm.Connection = userInfo.Connection;
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.CommandText = "dental.UpdatePaymentDetails";

                sqlComm.Parameters.Add("@receipt_no", SqlDbType.VarChar).Value = paymentInfo.ReceiptNo;
                sqlComm.Parameters.Add("@date_paid", SqlDbType.DateTime).Value = DateTime.Parse(paymentInfo.DatePaid);
                sqlComm.Parameters.Add("@amount", SqlDbType.Decimal).Value = paymentInfo.Amount;
                sqlComm.Parameters.Add("@discount", SqlDbType.Decimal).Value = paymentInfo.Discount;
                sqlComm.Parameters.Add("@payment_type", SqlDbType.TinyInt).Value = paymentInfo.PaymentType;
                sqlComm.Parameters.Add("@bank_name", SqlDbType.VarChar).Value = paymentInfo.BankName;
                sqlComm.Parameters.Add("@check_no", SqlDbType.VarChar).Value = paymentInfo.CheckNo;
                sqlComm.Parameters.Add("@card_number", SqlDbType.VarChar).Value = paymentInfo.CardNumber;
                sqlComm.Parameters.Add("@card_type", SqlDbType.VarChar).Value = paymentInfo.CardType;
                sqlComm.Parameters.Add("@card_expire", SqlDbType.VarChar).Value = paymentInfo.CardExpire;

                sqlComm.Parameters.Add("@edited_by", SqlDbType.VarChar).Value = userInfo.UserId;

                sqlComm.ExecuteNonQuery();
            }

        } //------------------------------

        //this procedure inserts a new payment details
        public void DeletePaymentDetails(CommonExchange.SysAccess userInfo, CommonExchange.PaymentDetails paymentInfo)
        {
            using (SqlCommand sqlComm = new SqlCommand())
            {
                sqlComm.Connection = userInfo.Connection;
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.CommandText = "dental.DeletePaymentDetails";

                sqlComm.Parameters.Add("@receipt_no", SqlDbType.VarChar).Value = paymentInfo.ReceiptNo;

                sqlComm.Parameters.Add("@deleted_by", SqlDbType.VarChar).Value = userInfo.UserId;

                sqlComm.ExecuteNonQuery();
            }

        } //------------------------------
        #endregion

        #region Programmer-Defined Function Procedures
        //this function returns the patient registration table
        public DataTable SelectByPatientIDPatientRegistration(CommonExchange.SysAccess userInfo, String patientId)
        {
            DataTable dbTable = new DataTable("PatientRegistrationByPatientID");
            dbTable.Columns.Add("sysid_registration", System.Type.GetType("System.String"));
            dbTable.Columns.Add("registration_date", System.Type.GetType("System.String"));
            dbTable.Columns.Add("medical_prescription", System.Type.GetType("System.String"));
            dbTable.Columns.Add("amount_payable", System.Type.GetType("System.Decimal"));
            dbTable.Columns.Add("amount_paid", System.Type.GetType("System.Decimal"));
            dbTable.Columns.Add("discount", System.Type.GetType("System.Decimal"));
            dbTable.Columns.Add("amount_balance", System.Type.GetType("System.Decimal"));

            using (SqlCommand sqlComm = new SqlCommand())
            {
                sqlComm.Connection = userInfo.Connection;
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.CommandText = "dental.SelectByPatientIDPatientRegistration";

                sqlComm.Parameters.Add("@sysid_patient", SqlDbType.VarChar).Value = patientId;
                sqlComm.Parameters.Add("@system_user_id", SqlDbType.VarChar).Value = userInfo.UserId;

                using (SqlDataReader sqlReader = sqlComm.ExecuteReader())
                {
                    if (sqlReader.HasRows)
                    {
                        while (sqlReader.Read())
                        {
                            DataRow newRow = dbTable.NewRow();

                            newRow["sysid_registration"] = DatabaseLib.ProcStatic.DataReaderConvert(sqlReader, "sysid_registration", "");
                            newRow["registration_date"] = DatabaseLib.ProcStatic.DataReaderConvert(sqlReader, "registration_date", "");
                            newRow["medical_prescription"] = DatabaseLib.ProcStatic.DataReaderConvert(sqlReader, "medical_prescription", "");
                            newRow["amount_payable"] = DatabaseLib.ProcStatic.DataReaderConvert(sqlReader, "amount_payable", Decimal.Parse("0"));
                            newRow["amount_paid"] = DatabaseLib.ProcStatic.DataReaderConvert(sqlReader, "amount_paid", Decimal.Parse("0"));
                            newRow["discount"] = DatabaseLib.ProcStatic.DataReaderConvert(sqlReader, "discount", Decimal.Parse("0"));
                            newRow["amount_balance"] = DatabaseLib.ProcStatic.DataReaderConvert(sqlReader, "amount_balance", Decimal.Parse("0"));

                            dbTable.Rows.Add(newRow);
                        }
                    }

                    sqlReader.Close();
                }

                dbTable.AcceptChanges();
            }

            return dbTable;
        } //--------------------------

        //this function returns the patient registration details
        public DataTable SelectByRegistrationIDRegistrationDetails(CommonExchange.SysAccess userInfo, String registrationSysId)
        {
            DataTable dbTable = new DataTable("PatientRegistrationDetailsTable");
            dbTable.Columns.Add("details_id", System.Type.GetType("System.Int64"));
            dbTable.Columns.Add("sysid_procedure", System.Type.GetType("System.String"));
            dbTable.Columns.Add("date_administered", System.Type.GetType("System.String"));
            dbTable.Columns.Add("tooth_no", System.Type.GetType("System.String"));
            dbTable.Columns.Add("amount", System.Type.GetType("System.Decimal"));
            dbTable.Columns.Add("remarks", System.Type.GetType("System.String"));
            dbTable.Columns.Add("procedure_name", System.Type.GetType("System.String"));

            using (SqlCommand sqlComm = new SqlCommand())
            {
                sqlComm.Connection = userInfo.Connection;
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.CommandText = "dental.SelectByRegistrationIDRegistrationDetails";

                sqlComm.Parameters.Add("@sysid_registration", SqlDbType.VarChar).Value = registrationSysId;
                sqlComm.Parameters.Add("@system_user_id", SqlDbType.VarChar).Value = userInfo.UserId;

                using (SqlDataReader sqlReader = sqlComm.ExecuteReader())
                {
                    if (sqlReader.HasRows)
                    {
                        while (sqlReader.Read())
                        {
                            DataRow newRow = dbTable.NewRow();

                            newRow["details_id"] = DatabaseLib.ProcStatic.DataReaderConvert(sqlReader, "details_id", Int64.Parse("0"));
                            newRow["sysid_procedure"] = DatabaseLib.ProcStatic.DataReaderConvert(sqlReader, "sysid_procedure", "");
                            newRow["date_administered"] = DatabaseLib.ProcStatic.DataReaderConvert(sqlReader, "date_administered", "");
                            newRow["tooth_no"] = DatabaseLib.ProcStatic.DataReaderConvert(sqlReader, "tooth_no", "");
                            newRow["amount"] = DatabaseLib.ProcStatic.DataReaderConvert(sqlReader, "amount", Decimal.Parse("0"));
                            newRow["remarks"] = DatabaseLib.ProcStatic.DataReaderConvert(sqlReader, "remarks", "");
                            newRow["procedure_name"] = DatabaseLib.ProcStatic.DataReaderConvert(sqlReader, "procedure_name", "");

                            dbTable.Rows.Add(newRow);
                        }
                    }

                    sqlReader.Close();
                }

                dbTable.AcceptChanges();

            }

            return dbTable;
        } //----------------------------------

        //this function returns the payment details table
        public DataTable SelectByRegistrationIDPaymentDetails(CommonExchange.SysAccess userInfo, String registrationSysId)
        {
            DataTable dbTable = new DataTable("PaymentDetailsTable");
            dbTable.Columns.Add("receipt_no", System.Type.GetType("System.String"));
            dbTable.Columns.Add("date_paid", System.Type.GetType("System.String"));
            dbTable.Columns.Add("amount", System.Type.GetType("System.Decimal"));
            dbTable.Columns.Add("discount", System.Type.GetType("System.Decimal"));
            dbTable.Columns.Add("payment_type", System.Type.GetType("System.Byte"));
            dbTable.Columns.Add("bank_name", System.Type.GetType("System.String"));
            dbTable.Columns.Add("check_no", System.Type.GetType("System.String"));
            dbTable.Columns.Add("card_number", System.Type.GetType("System.String"));
            dbTable.Columns.Add("card_type", System.Type.GetType("System.String"));
            dbTable.Columns.Add("card_expire", System.Type.GetType("System.String"));

            using (SqlCommand sqlComm = new SqlCommand())
            {
                sqlComm.Connection = userInfo.Connection;
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.CommandText = "dental.SelectByRegistrationIDPaymentDetails";

                sqlComm.Parameters.Add("@sysid_registration", SqlDbType.VarChar).Value = registrationSysId;
                sqlComm.Parameters.Add("@system_user_id", SqlDbType.VarChar).Value = userInfo.UserId;

                using (SqlDataReader sqlReader = sqlComm.ExecuteReader())
                {
                    if (sqlReader.HasRows)
                    {
                        while (sqlReader.Read())
                        {
                            DataRow newRow = dbTable.NewRow();

                            newRow["receipt_no"] = DatabaseLib.ProcStatic.DataReaderConvert(sqlReader, "receipt_no", "");
                            newRow["date_paid"] = DatabaseLib.ProcStatic.DataReaderConvert(sqlReader, "date_paid", "");
                            newRow["amount"] = DatabaseLib.ProcStatic.DataReaderConvert(sqlReader, "amount", Decimal.Parse("0"));
                            newRow["discount"] = DatabaseLib.ProcStatic.DataReaderConvert(sqlReader, "discount", Decimal.Parse("0"));
                            newRow["payment_type"] = DatabaseLib.ProcStatic.DataReaderConvert(sqlReader, "payment_type", "");
                            newRow["bank_name"] = DatabaseLib.ProcStatic.DataReaderConvert(sqlReader, "bank_name", "");
                            newRow["check_no"] = DatabaseLib.ProcStatic.DataReaderConvert(sqlReader, "check_no", "");
                            newRow["card_number"] = DatabaseLib.ProcStatic.DataReaderConvert(sqlReader, "card_number", "");
                            newRow["card_type"] = DatabaseLib.ProcStatic.DataReaderConvert(sqlReader, "card_type", "");
                            newRow["card_expire"] = DatabaseLib.ProcStatic.DataReaderConvert(sqlReader, "card_expire", "");

                            dbTable.Rows.Add(newRow);
                        }
                    }

                    sqlReader.Close();
                }

                dbTable.AcceptChanges();
            }

            return dbTable;

        } //-------------------------------

        //this function returns the patient registration table
        public DataTable SelectByProcedureTakenPatientRegistration(CommonExchange.SysAccess userInfo, String patientId)
        {
            DataTable dbTable = new DataTable("ByProcedureTakenTable");
            dbTable.Columns.Add("sysid_registration", System.Type.GetType("System.String"));
            dbTable.Columns.Add("registration_date", System.Type.GetType("System.String"));
            dbTable.Columns.Add("medical_prescription", System.Type.GetType("System.String"));
            dbTable.Columns.Add("date_administered", System.Type.GetType("System.String"));
            dbTable.Columns.Add("tooth_no", System.Type.GetType("System.String"));
            dbTable.Columns.Add("amount", System.Type.GetType("System.Decimal"));
            dbTable.Columns.Add("procedure_name", System.Type.GetType("System.String"));

            using (SqlCommand sqlComm = new SqlCommand())
            {
                sqlComm.Connection = userInfo.Connection;
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.CommandText = "dental.SelectByProcedureTakenPatientRegistration";

                sqlComm.Parameters.Add("@sysid_patient", SqlDbType.VarChar).Value = patientId;
                sqlComm.Parameters.Add("@system_user_id", SqlDbType.VarChar).Value = userInfo.UserId;

                using (SqlDataReader sqlReader = sqlComm.ExecuteReader())
                {
                    if (sqlReader.HasRows)
                    {
                        while (sqlReader.Read())
                        {
                            DataRow newRow = dbTable.NewRow();

                            newRow["sysid_registration"] = DatabaseLib.ProcStatic.DataReaderConvert(sqlReader, "sysid_registration", "");
                            newRow["registration_date"] = DatabaseLib.ProcStatic.DataReaderConvert(sqlReader, "registration_date", "");
                            newRow["medical_prescription"] = DatabaseLib.ProcStatic.DataReaderConvert(sqlReader, "medical_prescription", "");
                            newRow["date_administered"] = DatabaseLib.ProcStatic.DataReaderConvert(sqlReader, "date_administered", "");
                            newRow["tooth_no"] = DatabaseLib.ProcStatic.DataReaderConvert(sqlReader, "tooth_no", "");
                            newRow["amount"] = DatabaseLib.ProcStatic.DataReaderConvert(sqlReader, "amount", Decimal.Parse("0"));
                            newRow["procedure_name"] = DatabaseLib.ProcStatic.DataReaderConvert(sqlReader, "procedure_name", "");

                            dbTable.Rows.Add(newRow);
                        }
                    }

                    sqlReader.Close();
                }

                dbTable.AcceptChanges();
            }

            return dbTable;
        }

        //this function returns the patient registration table
        public DataTable SelectForCashReportPatientRegistration(CommonExchange.SysAccess userInfo, String dateFrom, String dateTo)
        {

            DataTable dbTable = new DataTable("CashReportTable");
            dbTable.Columns.Add("sysid_registration", System.Type.GetType("System.String"));
			dbTable.Columns.Add("sysid_patient", System.Type.GetType("System.String"));
			dbTable.Columns.Add("registration_date", System.Type.GetType("System.String"));
            dbTable.Columns.Add("medical_prescription", System.Type.GetType("System.String"));
			dbTable.Columns.Add("receipt_no", System.Type.GetType("System.String"));
			dbTable.Columns.Add("date_paid", System.Type.GetType("System.String"));
			dbTable.Columns.Add("amount", System.Type.GetType("System.Decimal"));
			dbTable.Columns.Add("last_name", System.Type.GetType("System.String"));
			dbTable.Columns.Add("first_name", System.Type.GetType("System.String"));
            dbTable.Columns.Add("middle_name", System.Type.GetType("System.String"));

            using (SqlCommand sqlComm = new SqlCommand())
            {
                sqlComm.Connection = userInfo.Connection;
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.CommandText = "dental.SelectForCashReportPatientRegistration";

                sqlComm.Parameters.Add("@date_from", SqlDbType.DateTime).Value = DateTime.Parse(dateFrom);
                sqlComm.Parameters.Add("@date_to", SqlDbType.DateTime).Value = DateTime.Parse(dateTo);

                sqlComm.Parameters.Add("@system_user_id", SqlDbType.VarChar).Value = userInfo.UserId;

                using (SqlDataReader sqlReader = sqlComm.ExecuteReader())
                {
                    if (sqlReader.HasRows)
                    {
                        while (sqlReader.Read())
                        {
                            DataRow newRow = dbTable.NewRow();

                            newRow["sysid_registration"] = DatabaseLib.ProcStatic.DataReaderConvert(sqlReader, "sysid_registration", "");
                            newRow["sysid_patient"] = DatabaseLib.ProcStatic.DataReaderConvert(sqlReader, "sysid_patient", "");
                            newRow["registration_date"] = DatabaseLib.ProcStatic.DataReaderConvert(sqlReader, "registration_date", "");
                            newRow["medical_prescription"] = DatabaseLib.ProcStatic.DataReaderConvert(sqlReader, "medical_prescription", "");
                            newRow["receipt_no"] = DatabaseLib.ProcStatic.DataReaderConvert(sqlReader, "receipt_no", "");
                            newRow["date_paid"] = DatabaseLib.ProcStatic.DataReaderConvert(sqlReader, "date_paid", "");
                            newRow["amount"] = DatabaseLib.ProcStatic.DataReaderConvert(sqlReader, "amount", Decimal.Parse("0"));
                            newRow["last_name"] = DatabaseLib.ProcStatic.DataReaderConvert(sqlReader, "last_name", "");
                            newRow["first_name"] = DatabaseLib.ProcStatic.DataReaderConvert(sqlReader, "first_name", "");
                            newRow["middle_name"] = DatabaseLib.ProcStatic.DataReaderConvert(sqlReader, "middle_name", "");

                            dbTable.Rows.Add(newRow);
                        }
                    }

                    sqlReader.Close();
                }

                dbTable.AcceptChanges();
            }

            return dbTable;

        } //------------------------------------

        //this function returns the accounts table
        public DataTable SelectForAccountsReceivablePatientRegistration(CommonExchange.SysAccess userInfo)
        {
            DataTable dbTable = new DataTable("AccountsReceivableTable");
            dbTable.Columns.Add("sysid_registration", System.Type.GetType("System.String"));
            dbTable.Columns.Add("sysid_patient", System.Type.GetType("System.String"));
            dbTable.Columns.Add("registration_date", System.Type.GetType("System.String"));
            dbTable.Columns.Add("medical_prescription", System.Type.GetType("System.String"));
            dbTable.Columns.Add("amount_payable", System.Type.GetType("System.Decimal"));
            dbTable.Columns.Add("amount_paid", System.Type.GetType("System.Decimal"));
            dbTable.Columns.Add("discount", System.Type.GetType("System.Decimal"));
            dbTable.Columns.Add("amount_balance", System.Type.GetType("System.Decimal"));
            dbTable.Columns.Add("last_name", System.Type.GetType("System.String"));
            dbTable.Columns.Add("first_name", System.Type.GetType("System.String"));
            dbTable.Columns.Add("middle_name", System.Type.GetType("System.String"));

            using (SqlCommand sqlComm = new SqlCommand())
            {
                sqlComm.Connection = userInfo.Connection;
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.CommandText = "dental.SelectForAccountsReceivablePatientRegistration";

                sqlComm.Parameters.Add("@system_user_id", SqlDbType.VarChar).Value = userInfo.UserId;

                using (SqlDataReader sqlReader = sqlComm.ExecuteReader())
                {
                    if (sqlReader.HasRows)
                    {
                        while (sqlReader.Read())
                        {
                            DataRow newRow = dbTable.NewRow();

                            newRow["sysid_registration"] = DatabaseLib.ProcStatic.DataReaderConvert(sqlReader, "sysid_registration", "");
                            newRow["sysid_patient"] = DatabaseLib.ProcStatic.DataReaderConvert(sqlReader, "sysid_patient", "");
                            newRow["registration_date"] = DatabaseLib.ProcStatic.DataReaderConvert(sqlReader, "registration_date", "");
                            newRow["medical_prescription"] = DatabaseLib.ProcStatic.DataReaderConvert(sqlReader, "medical_prescription", "");
                            newRow["amount_payable"] = DatabaseLib.ProcStatic.DataReaderConvert(sqlReader, "amount_payable", Decimal.Parse("0"));
                            newRow["amount_paid"] = DatabaseLib.ProcStatic.DataReaderConvert(sqlReader, "amount_paid", Decimal.Parse("0"));
                            newRow["discount"] = DatabaseLib.ProcStatic.DataReaderConvert(sqlReader, "discount", Decimal.Parse("0"));
                            newRow["amount_balance"] = DatabaseLib.ProcStatic.DataReaderConvert(sqlReader, "amount_balance", Decimal.Parse("0"));
                            newRow["last_name"] = DatabaseLib.ProcStatic.DataReaderConvert(sqlReader, "last_name", "");
                            newRow["first_name"] = DatabaseLib.ProcStatic.DataReaderConvert(sqlReader, "first_name", "");
                            newRow["middle_name"] = DatabaseLib.ProcStatic.DataReaderConvert(sqlReader, "middle_name", "");

                            dbTable.Rows.Add(newRow);
                        }
                    }

                    sqlReader.Close();
                }

                dbTable.AcceptChanges();
            }

            return dbTable;

        } //---------------------------------------

        //this function returns the patient registration table
        public DataTable SelectForSalesPatientRegistration(CommonExchange.SysAccess userInfo, String dateFrom, String dateTo,
            String procedureSysId)
        {
            DataTable dbTable = new DataTable("SalesTable");
            dbTable.Columns.Add("sysid_registration", System.Type.GetType("System.String"));
            dbTable.Columns.Add("sysid_patient", System.Type.GetType("System.String"));
            dbTable.Columns.Add("registration_date", System.Type.GetType("System.String"));
            dbTable.Columns.Add("medical_prescription", System.Type.GetType("System.String"));
            dbTable.Columns.Add("date_administered", System.Type.GetType("System.String"));
            dbTable.Columns.Add("tooth_no", System.Type.GetType("System.String"));
            dbTable.Columns.Add("amount", System.Type.GetType("System.Decimal"));
            dbTable.Columns.Add("last_name", System.Type.GetType("System.String"));
            dbTable.Columns.Add("first_name", System.Type.GetType("System.String"));
            dbTable.Columns.Add("middle_name", System.Type.GetType("System.String"));
            dbTable.Columns.Add("procedure_name", System.Type.GetType("System.String"));

            using (SqlCommand sqlComm = new SqlCommand())
            {
                sqlComm.Connection = userInfo.Connection;
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.CommandText = "dental.SelectForSalesPatientRegistration";

                sqlComm.Parameters.Add("@date_from", SqlDbType.DateTime).Value = dateFrom;
                sqlComm.Parameters.Add("@date_to", SqlDbType.DateTime).Value = dateTo;
                sqlComm.Parameters.Add("@sysid_procedure", SqlDbType.VarChar).Value = procedureSysId;

                sqlComm.Parameters.Add("@system_user_id", SqlDbType.VarChar).Value = userInfo.UserId;

                using (SqlDataReader sqlReader = sqlComm.ExecuteReader())
                {
                    if (sqlReader.HasRows)
                    {
                        while (sqlReader.Read())
                        {
                            DataRow newRow = dbTable.NewRow();

                            newRow["sysid_registration"] = DatabaseLib.ProcStatic.DataReaderConvert(sqlReader, "sysid_registration", "");
                            newRow["sysid_patient"] = DatabaseLib.ProcStatic.DataReaderConvert(sqlReader, "sysid_patient", "");
                            newRow["registration_date"] = DatabaseLib.ProcStatic.DataReaderConvert(sqlReader, "registration_date", "");
                            newRow["medical_prescription"] = DatabaseLib.ProcStatic.DataReaderConvert(sqlReader, "medical_prescription", "");
                            newRow["date_administered"] = DatabaseLib.ProcStatic.DataReaderConvert(sqlReader, "date_administered", "");
                            newRow["tooth_no"] = DatabaseLib.ProcStatic.DataReaderConvert(sqlReader, "tooth_no", "");
                            newRow["amount"] = DatabaseLib.ProcStatic.DataReaderConvert(sqlReader, "amount", Decimal.Parse("0"));
                            newRow["last_name"] = DatabaseLib.ProcStatic.DataReaderConvert(sqlReader, "last_name", "");
                            newRow["first_name"] = DatabaseLib.ProcStatic.DataReaderConvert(sqlReader, "first_name", "");
                            newRow["middle_name"] = DatabaseLib.ProcStatic.DataReaderConvert(sqlReader, "middle_name", "");
                            newRow["procedure_name"] = DatabaseLib.ProcStatic.DataReaderConvert(sqlReader, "procedure_name", "");

                            dbTable.Rows.Add(newRow);
                        }
                    }

                    sqlReader.Close();
                }

                dbTable.AcceptChanges();
            }

            return dbTable;

        } //------------------------------------------
        #endregion
    }
}
