using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DatabaseLib
{
    public class DbLibProcedureManager : IDisposable
    {
        #region Constructor and Destructor

        public DbLibProcedureManager()
        {
        }

        public void Dispose()
        {
            
        }

        #endregion

        #region Programmer-Defined Void Procedures

        //this procedure inserts a new procedure information
        public void InsertProcedureInformation(CommonExchange.SysAccess userInfo, ref CommonExchange.Procedure procedureInfo)
        {
            procedureInfo.ProcedureSystemId = PrimaryKeys.GetNewSysIDProcedureInformation(userInfo);

            using (SqlCommand sqlComm = new SqlCommand())
            {
                sqlComm.Connection = userInfo.Connection;
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.CommandText = "dental.InsertProcedureInformation";

                sqlComm.Parameters.Add("@sysid_procedure", SqlDbType.VarChar).Value = procedureInfo.ProcedureSystemId;
                sqlComm.Parameters.Add("@procedure_name", SqlDbType.VarChar).Value = procedureInfo.ProcedureName;
                sqlComm.Parameters.Add("@amount", SqlDbType.Decimal).Value = procedureInfo.Amount;

                sqlComm.Parameters.Add("@created_by", SqlDbType.VarChar).Value = userInfo.UserId;

                sqlComm.ExecuteNonQuery();
            }

        } //--------------------------------

        //this procedure updates a procedure information
        public void UpdateProcedureInformation(CommonExchange.SysAccess userInfo, CommonExchange.Procedure procedureInfo)
        {
            using (SqlCommand sqlComm = new SqlCommand())
            {
                sqlComm.Connection = userInfo.Connection;
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.CommandText = "dental.UpdateProcedureInformation";

                sqlComm.Parameters.Add("@sysid_procedure", SqlDbType.VarChar).Value = procedureInfo.ProcedureSystemId;
                sqlComm.Parameters.Add("@procedure_name", SqlDbType.VarChar).Value = procedureInfo.ProcedureName;
                sqlComm.Parameters.Add("@amount", SqlDbType.Decimal).Value = procedureInfo.Amount;

                sqlComm.Parameters.Add("@edited_by", SqlDbType.VarChar).Value = userInfo.UserId;

                sqlComm.ExecuteNonQuery();
            }

        } //--------------------------------

        #endregion

        #region Programmer-Defined Function Procedures

        //this function returns the selected procedure information
        public DataTable SelectProcedureInformation(CommonExchange.SysAccess userInfo)
        {
            DataTable dbTable = new DataTable("ProcedureInformationTable");

            using (SqlCommand sqlComm = new SqlCommand())
            {
                sqlComm.Connection = userInfo.Connection;
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.CommandText = "dental.SelectProcedureInformation";

                sqlComm.Parameters.Add("@system_user_id", SqlDbType.VarChar).Value = userInfo.UserId;

                using (SqlDataAdapter sqlAdapter = new SqlDataAdapter())
                {
                    sqlAdapter.SelectCommand = sqlComm;
                    sqlAdapter.Fill(dbTable);
                }
            }

            return dbTable;

        } //------------------------

        //this function determines if the procedure name already exists
        public Boolean IsExistsNameProcedureInformation(CommonExchange.SysAccess userInfo, CommonExchange.Procedure procedureInfo)
        {
            Boolean isExist = false;

            using (SqlCommand sqlComm = new SqlCommand())
            {
                sqlComm.Connection = userInfo.Connection;
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.CommandText = "dental.IsExistsNameProcedureInformation";

                sqlComm.Parameters.Add("@sysid_procedure", SqlDbType.VarChar).Value = procedureInfo.ProcedureSystemId;
                sqlComm.Parameters.Add("@procedure_name", SqlDbType.VarChar).Value = procedureInfo.ProcedureName;
                sqlComm.Parameters.Add("@system_user_id", SqlDbType.VarChar).Value = userInfo.UserId;

                isExist = (Boolean)sqlComm.ExecuteScalar();
            }

            return isExist;
        } //-----------------------
        #endregion
    }
}
