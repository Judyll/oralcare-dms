using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Threading;

namespace DatabaseLib
{
    internal class PrimaryKeys
    {
        #region Programmer-Defined Function Procedures
        
        //this function gets a new patient system id
        public static String GetNewSysIDPatientInformation(CommonExchange.SysAccess userInfo)
        {
            Int32 rowCount = 0;
            String patientId = "";

            using (SqlCommand sqlComm = new SqlCommand())
            {
                sqlComm.Connection = userInfo.Connection;
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.CommandText = "dental.GetCountPatientInformation";

                sqlComm.Parameters.Add("@system_user_id", SqlDbType.VarChar).Value = userInfo.UserId;

                rowCount = (Int32)sqlComm.ExecuteScalar();
            }

            do
            {
                patientId = "SYSPNT" + ProcStatic.NineDigitZero(++rowCount);

            } while (IsExistsSysIDPatientInformation(userInfo.UserId, userInfo.Connection, patientId));

            return patientId;
        }

        private static Boolean IsExistsSysIDPatientInformation(String userId, SqlConnection sqlConn, String patientId)
        {
            Boolean isExist = false;

            using (SqlCommand sqlComm = new SqlCommand())
            {
                sqlComm.Connection = sqlConn;
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.CommandText = "dental.IsExistsSysIDPatientInformation";

                sqlComm.Parameters.Add("@sysid_patient", SqlDbType.VarChar).Value = patientId;
                sqlComm.Parameters.Add("@system_user_id", SqlDbType.VarChar).Value = userId;

                isExist = (Boolean)sqlComm.ExecuteScalar();
            }

            return isExist;

        } //-----------------------------

        //this function gets a new procedure system id
        public static String GetNewSysIDProcedureInformation(CommonExchange.SysAccess userInfo)
        {
            Int32 rowCount = 0;
            String procedureId = "";

            using (SqlCommand sqlComm = new SqlCommand())
            {
                sqlComm.Connection = userInfo.Connection;
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.CommandText = "dental.GetCountProcedureInformation";

                sqlComm.Parameters.Add("@system_user_id", SqlDbType.VarChar).Value = userInfo.UserId;

                rowCount = (Int32)sqlComm.ExecuteScalar();
            }

            do
            {
                procedureId = "SYSPRC" + ProcStatic.SixDigitZero(++rowCount);

            } while (IsExistsSysIDProcedureInformation(userInfo.UserId, userInfo.Connection, procedureId));

            return procedureId;
        }

        private static Boolean IsExistsSysIDProcedureInformation(String userId, SqlConnection sqlConn, String procedureId)
        {
            Boolean isExist = false;

            using (SqlCommand sqlComm = new SqlCommand())
            {
                sqlComm.Connection = sqlConn;
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.CommandText = "dental.IsExistsSysIDProcedureInformation";

                sqlComm.Parameters.Add("@sysid_procedure", SqlDbType.VarChar).Value = procedureId;
                sqlComm.Parameters.Add("@system_user_id", SqlDbType.VarChar).Value = userId;

                isExist = (Boolean)sqlComm.ExecuteScalar();
            }

            return isExist;

        } //-----------------------------

        //this function gets a new registration system id
        public static String GetNewSysIDPatientRegistration(CommonExchange.SysAccess userInfo)
        {
            Int32 rowCount = 0;
            String regId = "";

            using (SqlCommand sqlComm = new SqlCommand())
            {
                sqlComm.Connection = userInfo.Connection;
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.CommandText = "dental.GetCountPatientRegistration";

                sqlComm.Parameters.Add("@system_user_id", SqlDbType.VarChar).Value = userInfo.UserId;

                rowCount = (Int32)sqlComm.ExecuteScalar();
            }

            do
            {
                regId = "SYSREG" + ProcStatic.EightDigitZero(++rowCount);

            } while (IsExistsSysIDPatientRegistration(userInfo.UserId, userInfo.Connection, regId));

            return regId;
        }

        private static Boolean IsExistsSysIDPatientRegistration(String userId, SqlConnection sqlConn, String regId)
        {
            Boolean isExist = false;

            using (SqlCommand sqlComm = new SqlCommand())
            {
                sqlComm.Connection = sqlConn;
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.CommandText = "dental.IsExistsSysIDPatientRegistration";

                sqlComm.Parameters.Add("@sysid_registration", SqlDbType.VarChar).Value = regId;
                sqlComm.Parameters.Add("@system_user_id", SqlDbType.VarChar).Value = userId;

                isExist = (Boolean)sqlComm.ExecuteScalar();
            }

            return isExist;

        } //-----------------------------

        //this function gets a new receipt no
        public static String GetNewReceiptNoPaymentDetails(CommonExchange.SysAccess userInfo)
        {
            Int32 rowCount = 0;
            String receiptNo = "";

            using (SqlCommand sqlComm = new SqlCommand())
            {
                sqlComm.Connection = userInfo.Connection;
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.CommandText = "dental.GetCountPaymentDetails";

                sqlComm.Parameters.Add("@system_user_id", SqlDbType.VarChar).Value = userInfo.UserId;

                rowCount = (Int32)sqlComm.ExecuteScalar();
            }

            do
            {
                receiptNo = "RN" + ProcStatic.TenDigitZero(++rowCount);

            } while (IsExistsReceiptNoPaymentDetails(userInfo.UserId, userInfo.Connection, receiptNo));

            return receiptNo;
        }

        private static Boolean IsExistsReceiptNoPaymentDetails(String userId, SqlConnection sqlConn, String receiptNo)
        {
            Boolean isExist = false;

            using (SqlCommand sqlComm = new SqlCommand())
            {
                sqlComm.Connection = sqlConn;
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.CommandText = "dental.IsExistsReceiptNoPaymentDetails";

                sqlComm.Parameters.Add("@receipt_no", SqlDbType.VarChar).Value = receiptNo;
                sqlComm.Parameters.Add("@system_user_id", SqlDbType.VarChar).Value = userId;

                isExist = (Boolean)sqlComm.ExecuteScalar();
            }

            return isExist;

        } //-----------------------------

        //this function gets a new system user id
        public static String GetNewSystemUserId(CommonExchange.SysAccess userInfo)
        {
            StringBuilder userId;

            do
            {
                userId = new StringBuilder();

                Int32 sSpecial, eSpecial, sUChar, eUChar, sLChar, eLChar, delimeter;

                sSpecial = Convert.ToInt32('!');
                eSpecial = Convert.ToInt32('?');
                sUChar = Convert.ToInt32('@');
                eUChar = Convert.ToInt32('_');
                sLChar = Convert.ToInt32('`');
                eLChar = Convert.ToInt32('~');
                delimeter = Convert.ToInt32('#');

                userId.Append("#");

                for (Int32 i = 1; i <= 26; i++)
                {
                    Boolean isValid = false;
                    Int32 iRandom = 0;

                    Thread.Sleep(15);

                    Random randObj = new Random(DateTime.Now.Millisecond);

                    do
                    {
                        iRandom = randObj.Next(sSpecial, eLChar);

                        if ((((iRandom >= sSpecial) && (iRandom <= eSpecial)) ||
                            ((iRandom >= sUChar) && (iRandom <= eUChar)) ||
                            ((iRandom >= sLChar) && (iRandom <= eLChar))) &&
                            (iRandom != delimeter))
                        {
                            userId.Append(Convert.ToChar(iRandom).ToString());
                            isValid = true;
                        }

                    } while (!isValid);
                }

                userId.Append("#");

            } while (IsExistsIDSystemUserInfo(userInfo.UserId, userInfo.Connection, userId.ToString()));

            return userId.ToString();
        }

        private static Boolean IsExistsIDSystemUserInfo(String userId, SqlConnection sqlConn, String newUserId)
        {
            Boolean isExist = false;

            using (SqlCommand sqlComm = new SqlCommand())
            {
                sqlComm.Connection = sqlConn;
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.CommandText = "dental.IsExistsIDSystemUserInformation";

                sqlComm.Parameters.Add("@new_user_id", SqlDbType.VarChar).Value = newUserId;
                sqlComm.Parameters.Add("@system_user_id", SqlDbType.VarChar).Value = userId;

                isExist = (Boolean)sqlComm.ExecuteScalar();
            }

            return isExist;

        } //-----------------------------


        #endregion
    }
}
