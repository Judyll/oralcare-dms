using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace DatabaseLib
{
    public class DbLibGeneral: IDisposable
    {
        #region Class Constructor and Destructor
        public DbLibGeneral()
        {
        }

        void IDisposable.Dispose()
        {
            
        }
        #endregion

        #region Programmer-Defined Function Procedures

        //this function authenticates the user
        public Boolean AuthenticateUser(ref CommonExchange.SysAccess userInfo)
        {
            Boolean isValid = false;

            try
            {
                using (Authenticate auth = new Authenticate(userInfo))
                {
                    userInfo = auth.UserInfo;
                    isValid = true;                    
                }
            }
            catch 
            {
                isValid = false;                
            }

            return isValid;
        } //----------------------------------------

        //this function return the server date
        public String GetServerDateTime(SqlConnection sqlConn)
        {
            DateTime serverTime;            
            
            using (SqlCommand sqlComm = new SqlCommand())
            {
                sqlComm.Connection = sqlConn;
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.CommandText = "dental.GetServerDateTime";

                serverTime = ((DateTime)sqlComm.ExecuteScalar());
            }

            return serverTime.ToString("o");
                        
        } //--------------------------

        #endregion
       
    }
}
