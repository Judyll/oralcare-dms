using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace DentalLib
{
    partial class SystemUserUpdate
    {
        #region Class General Variable Declarations
        private CommonExchange.SysAccess _testUserInfoTemp;
        #endregion

        #region Class Properties Declarations
        private Boolean _hasUpdated = false;
        public Boolean HasUpdated
        {
            get { return _hasUpdated; }
        }

        private Boolean _hasDeleted = false;
        public Boolean HasDeleted
        {
            get { return _hasDeleted; }
        }
        #endregion

        #region Class Constructor
        public SystemUserUpdate(CommonExchange.SysAccess userInfo, UserLogic userManager, CommonExchange.SysAccess testUserInfo)
            :
            base(userInfo, userManager)
        {
            this.InitializeComponent();

            _testUserInfo = testUserInfo;
            _testUserInfoTemp = testUserInfo;

            this.FormClosing += new FormClosingEventHandler(ClassClosing);
            this.btnUpdate.Click += new EventHandler(btnUpdateClick);
            this.btnClose.Click += new EventHandler(btnCloseClick);
        }

        
        #endregion

        #region Class Event Void Procedures
        //#####################################CLASS ProcedureInformationUpdate EVENTS########################################
        //event is raised when the class is loaded
        protected override void ClassLoad(object sender, EventArgs e)
        {
            if (!DatabaseLib.ProcStatic.IsSystemAccessAdmin(_userInfo))
            {
                DentalLib.ProcStatic.ShowErrorDialog("You are not allowed to access this module.",
                    "System User Information");

                _hasErrors = true;

                this.Close();
            }

            this.txtUsername.Text = _testUserInfo.UserName;
            this.txtPassword.Text = this.txtConfirm.Text = _testUserInfo.Password;

            if (_testUserInfo.UserStatus)
            {
                this.lblStatus.Text = "ACTIVE";
            }
            else
            {
                this.lblStatus.Text = "DEACTIVATED";
            }

            _userManager.InitializeAccessRightsComboBox(this.cboAccessRights, _testUserInfo.AccessCode);

            this.txtLastName.Text = _testUserInfo.LastName;
            this.txtFirstName.Text = _testUserInfo.FirstName;
            this.txtMiddleName.Text = _testUserInfo.MiddleName;
            this.txtPosition.Text = _testUserInfo.Position;

        } //----------------------------------

        //event is raised when the class is closing
        private void ClassClosing(object sender, FormClosingEventArgs e)
        {
            if (!_hasUpdated && !_testUserInfo.Equals(_testUserInfoTemp))
            {
                String strMsg = "There has been changes made in the current system user information. \nExiting will not save this changes." +
                                "\n\nAre you sure you want to exit?";
                DialogResult msgResult = MessageBox.Show(strMsg, "Confirm Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (msgResult == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }

        } //----------------------------------


        //####################################END CLASS ProcedureInformationUpdate EVENTS######################################

        //#########################################BUTTON btnClose EVENTS##################################################
        //event is raised when the button is clicked
        private void btnCloseClick(object sender, EventArgs e)
        {
            this.Close();
        } //------------------------------------------
        //#######################################END BUTTON btnClose EVENTS#################################################

        //##############################################BUTTON btnUpdate EVENTS##############################################
        //event is raised when the button is clicked
        private void btnUpdateClick(object sender, EventArgs e)
        {
            if (this.ValidateControls())
            {
                try
                {
                    String strMsg = "Are you sure you want to update the system user information?";

                    DialogResult msgResult = MessageBox.Show(strMsg, "Confirm Update", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                    if (msgResult == DialogResult.Yes)
                    {
                        strMsg = "The system user information has been successfully updated.";

                        _userManager.UpdateSystemUserInfo(_userInfo, _testUserInfo);

                        MessageBox.Show(strMsg, "Success", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                        _hasUpdated = true;

                        this.Close();
                    }
                    else if (msgResult == DialogResult.Cancel)
                    {
                        this.Close();
                    }
                }
                catch (Exception ex)
                {
                    DentalLib.ProcStatic.ShowErrorDialog(ex.Message, "Error in Updating");
                }
            }
        } //---------------------------------------
        //############################################END BUTTON btnUpdate EVENTS############################################


        #endregion

    }
}
