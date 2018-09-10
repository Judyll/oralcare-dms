using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace DentalLib
{
    internal class ProcedureLogic
    {
        #region Class Member Declarations
        private DataTable _procedureTable;
        #endregion

        #region Class Constructor
        public ProcedureLogic(CommonExchange.SysAccess userInfo)
        {
            this.InitializeClass(userInfo);
        }
        #endregion

        #region Programmer-Defined Void Procedures

        //this procedure inserts a new procedure information
        public void InsertProcedureInformation(CommonExchange.SysAccess userInfo, CommonExchange.Procedure procedureInfo)
        {
            using (DatabaseLib.DbLibProcedureManager dbLib = new DatabaseLib.DbLibProcedureManager())
            {
                dbLib.InsertProcedureInformation(userInfo, ref procedureInfo);
            }

            if (_procedureTable != null)
            {
                DataRow newRow = _procedureTable.NewRow();

                newRow["sysid_procedure"] = procedureInfo.ProcedureSystemId;
                newRow["procedure_name"] = procedureInfo.ProcedureName;
                newRow["amount"] = procedureInfo.Amount;

                _procedureTable.Rows.Add(newRow);
            }

            _procedureTable.AcceptChanges();

        } //----------------------------

        //this procedure updates a procedure information
        public void UpdateProcedureInformation(CommonExchange.SysAccess userInfo, CommonExchange.Procedure procedureInfo)
        {
            using (DatabaseLib.DbLibProcedureManager dbLib = new DatabaseLib.DbLibProcedureManager())
            {
                dbLib.UpdateProcedureInformation(userInfo, procedureInfo);
            }

            Int32 index = 0;

            if (_procedureTable != null)
            {
                foreach (DataRow procRow in _procedureTable.Rows)
                {
                    if (String.Equals(procRow["sysid_procedure"].ToString(), procedureInfo.ProcedureSystemId))
                    {
                        DataRow editRow = _procedureTable.Rows[index];

                        editRow.BeginEdit();

                        editRow["procedure_name"] = procedureInfo.ProcedureName;
                        editRow["amount"] = procedureInfo.Amount;

                        editRow.EndEdit();
                    }

                    index++;
                }

                _procedureTable.AcceptChanges();
            }

        } //---------------------------

        //this function returns all the procedures
        private void InitializeClass(CommonExchange.SysAccess userInfo)
        {
            using (DatabaseLib.DbLibProcedureManager dbLib = new DatabaseLib.DbLibProcedureManager())
            {
                _procedureTable = dbLib.SelectProcedureInformation(userInfo);
            }

        } //----------------------

        #endregion

        #region Programmer-Defined Function Procedures

        //this function determines if the procedure name already exists
        public Boolean IsExistsNameProcedureInformation(CommonExchange.SysAccess userInfo, CommonExchange.Procedure procedureInfo)
        {
            using (DatabaseLib.DbLibProcedureManager dbLib = new DatabaseLib.DbLibProcedureManager())
            {
                return dbLib.IsExistsNameProcedureInformation(userInfo, procedureInfo);
            }

        } //-----------------------------

        //this function returns the searched procedure information
        public DataTable GetSearchedProcedureInformation(String strCriteria)
        {
            DataTable procedureTable = new DataTable("SearchedProcedureInformationTable");
            procedureTable.Columns.Add("sysid_procedure", System.Type.GetType("System.String"));
            procedureTable.Columns.Add("procedure_name", System.Type.GetType("System.String"));
            procedureTable.Columns.Add("amount", System.Type.GetType("System.Decimal"));

            if (_procedureTable != null)
            {
                strCriteria = DentalLib.ProcStatic.TrimStartEndString(strCriteria.Replace("*", "").Replace("%", ""));

                String strFilter = "procedure_name LIKE '%" + strCriteria + "%'";
                DataRow[] selectRow = _procedureTable.Select(strFilter, "procedure_name ASC");

                foreach (DataRow procRow in selectRow)
                {
                    DataRow newRow = procedureTable.NewRow();

                    newRow["sysid_procedure"] = procRow["sysid_procedure"];
                    newRow["procedure_name"] = procRow["procedure_name"];
                    newRow["amount"] = procRow["amount"];

                    procedureTable.Rows.Add(newRow);
                }

            }

            return procedureTable;
        } //---------------------------

        //this function returns the procedure details
        public CommonExchange.Procedure GetBySystemIdProcedureInformationDetails(String procedureSysId)
        {
            CommonExchange.Procedure procedureInfo = new CommonExchange.Procedure();

            if (_procedureTable != null)
            {
                String strFilter = "sysid_procedure = '" + procedureSysId + "'";
                DataRow[] selectRow = _procedureTable.Select(strFilter, "procedure_name ASC");

                foreach (DataRow procRow in selectRow)
                {
                    procedureInfo.ProcedureSystemId = DatabaseLib.ProcStatic.DataRowConvert(procRow, "sysid_procedure", "");
                    procedureInfo.ProcedureName = DatabaseLib.ProcStatic.DataRowConvert(procRow, "procedure_name", "");
                    procedureInfo.Amount = DatabaseLib.ProcStatic.DataRowConvert(procRow, "amount", Decimal.Parse("0"));

                    break;
                }
            }
            
            return procedureInfo;
        } //------------------------------
        #endregion
    }
}
