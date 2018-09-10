using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;

namespace DentalLib
{
    internal class UserLogic
    {
        #region Class Member Declarations
        private DataSet _classDataSet;
        #endregion

        #region Class Properties Declarations
        private String _serverDateTime;
        public String ServerDateTime
        {
            get { return _serverDateTime; }
        }
        #endregion

        #region Class Constructor
        public UserLogic(CommonExchange.SysAccess userInfo)
        {
            this.InitializeClass(userInfo);
        }
        #endregion

        #region Programmer-Defined Void Procedures
        //this procedure inserts a new system user
        public void InsertSystemUserInfo(CommonExchange.SysAccess userInfo, CommonExchange.SysAccess newUserInfo)
        {
            using (DatabaseLib.DbLibUserManager dbLib = new DatabaseLib.DbLibUserManager())
            {
                dbLib.InsertSystemUserInfo(userInfo, ref newUserInfo);
            }

            if (_classDataSet != null)
            {
                DataRow newRow = _classDataSet.Tables["SystemUserTable"].NewRow();

                newRow["system_user_id"] = newUserInfo.UserId;
                newRow["system_user_name"] = newUserInfo.UserName;
                newRow["system_user_password"] = newUserInfo.Password;
                newRow["system_user_status"] = newUserInfo.UserStatus;
                newRow["access_code"] = newUserInfo.AccessCode;
                newRow["last_name"] = newUserInfo.LastName;
                newRow["first_name"] = newUserInfo.FirstName;
                newRow["middle_name"] = newUserInfo.MiddleName;
                newRow["position"] = newUserInfo.Position;

                _classDataSet.Tables["SystemUserTable"].Rows.Add(newRow);
                _classDataSet.AcceptChanges();
            }
            

        } //------------------------

        //this procedure inserts a new system user
        public void UpdateSystemUserInfo(CommonExchange.SysAccess userInfo, CommonExchange.SysAccess newUserInfo)
        {
            using (DatabaseLib.DbLibUserManager dbLib = new DatabaseLib.DbLibUserManager())
            {
                dbLib.UpdateSystemUserInfo(userInfo, newUserInfo);
            }

            if (_classDataSet != null)
            {
                Int32 index = 0;

                foreach (DataRow userRow in _classDataSet.Tables["SystemUserTable"].Rows)
                {
                    if (String.Equals(userRow["system_user_id"].ToString(), newUserInfo.UserId))
                    {
                        DataRow editRow = _classDataSet.Tables["SystemUserTable"].Rows[index];

                        editRow.BeginEdit();

                        editRow["system_user_name"] = newUserInfo.UserName;
                        editRow["system_user_password"] = newUserInfo.Password;
                        editRow["system_user_status"] = newUserInfo.UserStatus;
                        editRow["access_code"] = newUserInfo.AccessCode;
                        editRow["last_name"] = newUserInfo.LastName;
                        editRow["first_name"] = newUserInfo.FirstName;
                        editRow["middle_name"] = newUserInfo.MiddleName;
                        editRow["position"] = newUserInfo.Position;

                        editRow.EndEdit();

                        _classDataSet.AcceptChanges();

                        break;
                    }

                    index++;
                }
            }
            
        } //-------------------------------------------

        //this procedure initializes the access rights combo box
        public void InitializeAccessRightsComboBox(ComboBox cboBase)
        {
            cboBase.Items.Clear();

            foreach (DataRow accessRow in _classDataSet.Tables["SystemAccessCodeTable"].Rows)
            {
                cboBase.Items.Add(DatabaseLib.ProcStatic.DataRowConvert(accessRow, "access_description", ""));
            }

        } //---------------------------

        //this procedure initializes the access rights combo box
        public void InitializeAccessRightsComboBox(ComboBox cboBase, String accessCode)
        {
            cboBase.Items.Clear();

            Int32 index = 0;

            foreach (DataRow accessRow in _classDataSet.Tables["SystemAccessCodeTable"].Rows)
            {
                cboBase.Items.Add(DatabaseLib.ProcStatic.DataRowConvert(accessRow, "access_description", ""));

                if (String.Equals(accessRow["access_code"].ToString(), accessCode))
                {
                    cboBase.SelectedIndex = index;
                }

                index++;
            }

        } //---------------------------

        //this procedure initializes the class
        private void InitializeClass(CommonExchange.SysAccess userInfo)
        {
            using (DatabaseLib.DbLibUserManager dbLib = new DatabaseLib.DbLibUserManager())
            {
                _classDataSet = dbLib.GetUserInformationDataSet(userInfo);
            }

            using (DatabaseLib.DbLibGeneral dbLib = new DatabaseLib.DbLibGeneral())
            {
                _serverDateTime = dbLib.GetServerDateTime(userInfo.Connection);
            }
        } //-----------------------------
        #endregion

        #region Programmer-Defined Function Procedures

        //this function determines if the user name and password already exists
        public Boolean IsExistsNameSystemUserInformation(CommonExchange.SysAccess userInfo, CommonExchange.SysAccess newUserInfo)
        {
            Boolean isExist = false;

            using (DatabaseLib.DbLibUserManager dbLib = new DatabaseLib.DbLibUserManager())
            {
                isExist = dbLib.IsExistsNameSystemUserInformation(userInfo, newUserInfo);
            }

            return isExist;
        } //------------------------------

        //this function determines the user log in
        public CommonExchange.SysAccess SelectForInformationSystemUserInfo(CommonExchange.SysAccess userInfo, String userId)
        {
            CommonExchange.SysAccess testUserInfo = new CommonExchange.SysAccess();

            String strFilter = "system_user_id = '" + userId + "'";
            DataRow[] selectRow = _classDataSet.Tables["SystemUserTable"].Select(strFilter);

            foreach (DataRow userRow in selectRow)
            {
                testUserInfo.UserId = DatabaseLib.ProcStatic.DataRowConvert(userRow, "system_user_id", "");
                testUserInfo.UserName = DatabaseLib.ProcStatic.DataRowConvert(userRow, "system_user_name", "");
                testUserInfo.Password = DatabaseLib.ProcStatic.DataRowConvert(userRow, "system_user_password", "");
                testUserInfo.UserStatus = DatabaseLib.ProcStatic.DataRowConvert(userRow, "system_user_status", false);
                testUserInfo.AccessCode = DatabaseLib.ProcStatic.DataRowConvert(userRow, "access_code", "");
                testUserInfo.LastName = DatabaseLib.ProcStatic.DataRowConvert(userRow, "last_name", "");
                testUserInfo.FirstName = DatabaseLib.ProcStatic.DataRowConvert(userRow, "first_name", "");
                testUserInfo.MiddleName = DatabaseLib.ProcStatic.DataRowConvert(userRow, "middle_name", "");
                testUserInfo.Position = DatabaseLib.ProcStatic.DataRowConvert(userRow, "position", "");

                break;
            }

            return testUserInfo;
        } //------------------------

        //this function returns the searched user
        public DataTable GetSearchedUserInformationTable(String strCriteria)
        {
            DataTable dbTable = new DataTable("SystemUserTableConcactName");
            dbTable.Columns.Add("system_user_id", System.Type.GetType("System.String"));
            dbTable.Columns.Add("system_user_name", System.Type.GetType("System.String"));
            dbTable.Columns.Add("system_user_full_name", System.Type.GetType("System.String"));
            dbTable.Columns.Add("access_rights", System.Type.GetType("System.String"));
            dbTable.Columns.Add("system_user_status", System.Type.GetType("System.String"));
            dbTable.Columns.Add("position", System.Type.GetType("System.String"));

            foreach (DataRow userRow in _classDataSet.Tables["SystemUserTable"].Rows)
            {
                DataRow newRow = dbTable.NewRow();

                newRow["system_user_id"] = DatabaseLib.ProcStatic.DataRowConvert(userRow, "system_user_id", "");
                newRow["system_user_name"] = DatabaseLib.ProcStatic.DataRowConvert(userRow, "system_user_name", "");
                newRow["system_user_full_name"] = DentalLib.ProcStatic.GetCompleteNameMiddleInitial(userRow, "last_name", "first_name", "middle_name");
                newRow["access_rights"] = this.GetUserAccessRights(DatabaseLib.ProcStatic.DataRowConvert(userRow, "access_code", ""));
                newRow["system_user_status"] = (DatabaseLib.ProcStatic.DataRowConvert(userRow, "system_user_status", false)) ? "ACTIVE" : "DEACTIVATED";
                newRow["position"] = DatabaseLib.ProcStatic.DataRowConvert(userRow, "position", "");

                dbTable.Rows.Add(newRow);
            }

            dbTable.AcceptChanges();

            DataTable userTable = new DataTable("SearchedSystemUserTable");
            userTable.Columns.Add("system_user_id", System.Type.GetType("System.String"));
            userTable.Columns.Add("system_user_name", System.Type.GetType("System.String"));
            userTable.Columns.Add("system_user_full_name", System.Type.GetType("System.String"));
            userTable.Columns.Add("access_rights", System.Type.GetType("System.String"));
            userTable.Columns.Add("system_user_status", System.Type.GetType("System.String"));
            userTable.Columns.Add("position", System.Type.GetType("System.String"));

            strCriteria = strCriteria.Replace("*", "").Replace("%", "");

            String strFilter = "system_user_name LIKE '%" + strCriteria + "%' OR system_user_full_name LIKE '%" + strCriteria + "%'";
            DataRow[] selectRow = dbTable.Select(strFilter, "system_user_name ASC");

            foreach (DataRow userRow in selectRow)
            {
                DataRow newRow = userTable.NewRow();

                newRow["system_user_id"] = DatabaseLib.ProcStatic.DataRowConvert(userRow, "system_user_id", "");
                newRow["system_user_name"] = DatabaseLib.ProcStatic.DataRowConvert(userRow, "system_user_name", "");
                newRow["system_user_full_name"] = DatabaseLib.ProcStatic.DataRowConvert(userRow, "system_user_full_name", "");
                newRow["access_rights"] = DatabaseLib.ProcStatic.DataRowConvert(userRow, "access_rights", "");
                newRow["system_user_status"] = DatabaseLib.ProcStatic.DataRowConvert(userRow, "system_user_status", "");
                newRow["position"] = DatabaseLib.ProcStatic.DataRowConvert(userRow, "position", "");

                userTable.Rows.Add(newRow);
            }

            userTable.AcceptChanges();

            return userTable;
        } //-----------------------------------

        //this function returns the access code
        public String GetUserAccessCode(Int32 index)
        {
            DataRow accessRow = _classDataSet.Tables["SystemAccessCodeTable"].Rows[index];

            return DatabaseLib.ProcStatic.DataRowConvert(accessRow, "access_code", "");
        } //--------------------------

        //this function returns the access code
        private String GetUserAccessRights(String accessCode)
        {
            String accessRights = "";

            foreach (DataRow accessRow in _classDataSet.Tables["SystemAccessCodeTable"].Rows)
            {
                if (String.Equals(accessRow["access_code"].ToString(), accessCode))
                {
                    accessRights = DatabaseLib.ProcStatic.DataRowConvert(accessRow, "access_description", "");

                    break;
                }
            }

            return accessRights;
        } //----------------------
        #endregion
    }
}
