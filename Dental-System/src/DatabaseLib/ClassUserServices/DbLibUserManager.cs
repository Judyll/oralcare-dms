using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DatabaseLib
{
    public class DbLibUserManager : IDisposable
    {

        #region Class Constructor and Destructor
        public DbLibUserManager()
        {
        }

        public void Dispose()
        {            
        }
        #endregion

        #region Programmer-Defined Void Procedures
        //this procedure inserts a new system user
        public void InsertSystemUserInfo(CommonExchange.SysAccess userInfo, ref CommonExchange.SysAccess newUserInfo)
        {
            newUserInfo.UserId = PrimaryKeys.GetNewSystemUserId(userInfo);

            using (SqlCommand sqlComm = new SqlCommand())
            {
                sqlComm.Connection = userInfo.Connection;
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.CommandText = "dental.InsertSystemUserInfo";

                sqlComm.Parameters.Add("@system_user_id", SqlDbType.VarChar).Value = newUserInfo.UserId;
	            sqlComm.Parameters.Add("@system_user_name", SqlDbType.VarChar).Value = newUserInfo.UserName;
	            sqlComm.Parameters.Add("@system_user_password", SqlDbType.VarChar).Value = newUserInfo.Password;
	            sqlComm.Parameters.Add("@system_user_status", SqlDbType.Bit).Value = newUserInfo.UserStatus;
	            sqlComm.Parameters.Add("@access_code", SqlDbType.VarChar).Value = newUserInfo.AccessCode;
                sqlComm.Parameters.Add("@last_name", SqlDbType.VarChar).Value = newUserInfo.LastName;
	            sqlComm.Parameters.Add("@first_name", SqlDbType.VarChar).Value = newUserInfo.FirstName;
	            sqlComm.Parameters.Add("@middle_name", SqlDbType.VarChar).Value = newUserInfo.MiddleName;
	            sqlComm.Parameters.Add("@position", SqlDbType.VarChar).Value = newUserInfo.Position;

                sqlComm.Parameters.Add("@created_by", SqlDbType.VarChar).Value = userInfo.UserId;

                sqlComm.ExecuteNonQuery();
            }
        } //----------------------------

        //this procedure inserts a new system user
        public void UpdateSystemUserInfo(CommonExchange.SysAccess userInfo, CommonExchange.SysAccess newUserInfo)
        {
            using (SqlCommand sqlComm = new SqlCommand())
            {
                sqlComm.Connection = userInfo.Connection;
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.CommandText = "dental.UpdateSystemUserInfo";

                sqlComm.Parameters.Add("@system_user_id", SqlDbType.VarChar).Value = newUserInfo.UserId;
                sqlComm.Parameters.Add("@system_user_name", SqlDbType.VarChar).Value = newUserInfo.UserName;
                sqlComm.Parameters.Add("@system_user_password", SqlDbType.VarChar).Value = newUserInfo.Password;
                sqlComm.Parameters.Add("@system_user_status", SqlDbType.Bit).Value = newUserInfo.UserStatus;
                sqlComm.Parameters.Add("@access_code", SqlDbType.VarChar).Value = newUserInfo.AccessCode;
                sqlComm.Parameters.Add("@last_name", SqlDbType.VarChar).Value = newUserInfo.LastName;
                sqlComm.Parameters.Add("@first_name", SqlDbType.VarChar).Value = newUserInfo.FirstName;
                sqlComm.Parameters.Add("@middle_name", SqlDbType.VarChar).Value = newUserInfo.MiddleName;
                sqlComm.Parameters.Add("@position", SqlDbType.VarChar).Value = newUserInfo.Position;

                sqlComm.Parameters.Add("@edited_by", SqlDbType.VarChar).Value = userInfo.UserId;

                sqlComm.ExecuteNonQuery();
            }
        } //----------------------------
        #endregion

        #region Programmer-Defined Function Procedures

        //this function determines if the user name and password already exists
        public Boolean IsExistsNameSystemUserInformation(CommonExchange.SysAccess userInfo, CommonExchange.SysAccess newUserInfo)
        {
            Boolean isExist = false;

            using (SqlCommand sqlComm = new SqlCommand())
            {
                sqlComm.Connection = userInfo.Connection;
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.CommandText = "dental.IsExistsNameSystemUserInformation";

                sqlComm.Parameters.Add("@system_user_name", SqlDbType.VarChar).Value = newUserInfo.UserName;
                sqlComm.Parameters.Add("@system_user_password", SqlDbType.VarChar).Value = newUserInfo.Password;

                sqlComm.Parameters.Add("@system_user_id", SqlDbType.VarChar).Value = userInfo.UserId;

                isExist = (Boolean)sqlComm.ExecuteScalar();
            }

            return isExist;
        } //---------------------------
        
        //this function returns the user information dataset
        public DataSet GetUserInformationDataSet(CommonExchange.SysAccess userInfo)
        {
            DataSet dbSet = new DataSet("UserInformationDataSet");

            dbSet.Tables.Add(this.SelectSystemAccessCode(userInfo));
            dbSet.Tables.Add(this.SelectSystemUserInfo(userInfo));

            return dbSet;
        } //------------------------

        //this function returns the user access code datatable
        private DataTable SelectSystemAccessCode(CommonExchange.SysAccess userInfo)
        {
            DataTable dbTable = new DataTable("SystemAccessCodeTable");

            using (SqlCommand sqlComm = new SqlCommand())
            {
                sqlComm.Connection = userInfo.Connection;
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.CommandText = "dental.SelectSystemAccessCode";

                sqlComm.Parameters.Add("@system_user_id", SqlDbType.VarChar).Value = userInfo.UserId;

                using (SqlDataAdapter sqlAdapter = new SqlDataAdapter())
                {
                    sqlAdapter.SelectCommand = sqlComm;
                    sqlAdapter.Fill(dbTable);
                }
            }

            return dbTable;
        } //------------------------------
        //this function returns the user datatable
        private DataTable SelectSystemUserInfo(CommonExchange.SysAccess userInfo)
        {
            DataTable dbTable = new DataTable("SystemUserTable");

            using (SqlCommand sqlComm = new SqlCommand())
            {
                sqlComm.Connection = userInfo.Connection;
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.CommandText = "dental.SelectSystemUserInfo";

                sqlComm.Parameters.Add("@system_user_id", SqlDbType.VarChar).Value = userInfo.UserId;

                using (SqlDataAdapter sqlAdapter = new SqlDataAdapter())
                {
                    sqlAdapter.SelectCommand = sqlComm;
                    sqlAdapter.Fill(dbTable);
                }
            }

            return dbTable;
        } //---------------------------------
        #endregion
    }
}
