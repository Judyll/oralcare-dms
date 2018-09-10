using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace DatabaseLib
{
    internal class Authenticate: IDisposable
    {
        #region Class Properties Declarations

        private CommonExchange.SysAccess _userInfo;
        public CommonExchange.SysAccess UserInfo
        {
            get { return _userInfo; }
        }

        #endregion

        #region Class Constructor and Destructor

        public Authenticate(CommonExchange.SysAccess userInfo)
        {
            if (!IsValidated(userInfo))
            {
                throw new Exception("Only authenticated users are allowed to access the system.");
            }
        }
          
        void IDisposable.Dispose()
        {
                        
        }

        #endregion

        #region Programmer-Defined Function Procedure

        private Boolean IsValidated(CommonExchange.SysAccess userInfo)
        {
            Boolean hasAccess = false;

            SqlConnection sqlConn;
            sqlConn = new SqlConnection();
            //sqlConn.ConnectionString = "Data Source = 192.168.0.2,1433; Network Library = DBMSSOCN; Initial Catalog = dbdentalsys; User Id = dEnTaL0608T0oTh; Password = D3U%@xL*^eUaZ0#pWt)kS;";
            sqlConn.ConnectionString = "Integrated Security = SSPI; Data Source = Localhost; Initial Catalog = dbperezorevillodms; User Id = dEnTaL0r1v3lloT0oTh; Password = g@8_f6%2CbU8!(10gHnQl;";
            sqlConn.Open();

            using (SqlCommand sqlComm = new SqlCommand())
            {
                sqlComm.Connection = sqlConn;
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.CommandText = "dental.SelectForLogInSystemUserInfo";

                sqlComm.Parameters.Add("@system_user_name", SqlDbType.VarChar).Value = userInfo.UserName;
                sqlComm.Parameters.Add("@system_user_password", SqlDbType.VarChar).Value = userInfo.Password;

                //sqlComm.Parameters.Add("@system_user_name", SqlDbType.VarChar).Value = "Debugger";
                //sqlComm.Parameters.Add("@system_user_password", SqlDbType.VarChar).Value = "b1o5r8g3e";

                using (SqlDataReader sqlReader = sqlComm.ExecuteReader())
                {
                    if (sqlReader.HasRows)
                    {

                        while (sqlReader.Read())
                        {
                            userInfo.UserId = ProcStatic.DataReaderConvert(sqlReader, "system_user_id", "");
                            userInfo.UserName = ProcStatic.DataReaderConvert(sqlReader, "system_user_name", "");
                            userInfo.Password = ProcStatic.DataReaderConvert(sqlReader, "system_user_password", "");
                            userInfo.UserStatus = ProcStatic.DataReaderConvert(sqlReader, "system_user_status", false);
                            userInfo.AccessCode = ProcStatic.DataReaderConvert(sqlReader, "access_code", "");
                            userInfo.LastName = ProcStatic.DataReaderConvert(sqlReader, "last_name", "");
                            userInfo.FirstName = ProcStatic.DataReaderConvert(sqlReader, "first_name", "");
                            userInfo.MiddleName = ProcStatic.DataReaderConvert(sqlReader, "middle_name", "");
                            userInfo.Position = ProcStatic.DataReaderConvert(sqlReader, "position", "");
                            userInfo.Connection = sqlConn;

                            break;
                        }

                        _userInfo = userInfo;
                        hasAccess = true;
                    }
                    else
                    {
                        hasAccess = false;
                    }

                    sqlReader.Close();
                }

            }

            return hasAccess;
        }

        #endregion

    }

}
