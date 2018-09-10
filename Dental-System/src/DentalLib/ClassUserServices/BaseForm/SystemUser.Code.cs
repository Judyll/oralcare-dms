using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace DentalLib
{
    partial class SystemUser
    {
        #region Class Member Declarations
        protected CommonExchange.SysAccess _userInfo;
        protected CommonExchange.SysAccess _testUserInfo;
        protected UserLogic _userManager;

        protected Boolean _hasErrors = false;

        private ErrorProvider _errProvider;

        #endregion

        #region Class Constructor
        public SystemUser()
        {
            this.InitializeComponent();
        }

        public SystemUser(CommonExchange.SysAccess userInfo, UserLogic userManager)
        {
            this.InitializeComponent();

            _userInfo = userInfo;
            _userManager = userManager;

            _errProvider = new ErrorProvider();

            this.Load += new EventHandler(ClassLoad);
            this.txtUsername.Validated += new EventHandler(txtUsernameValidated);
            this.txtPassword.Validated += new EventHandler(txtPasswordValidated);
            this.lblStatus.Click += new EventHandler(lblStatusClick);
            this.cboAccessRights.SelectedIndexChanged += new EventHandler(cboAccessRightsSelectedIndexChanged);
            this.txtLastName.Validated += new EventHandler(txtLastNameValidated);
            this.txtFirstName.Validated += new EventHandler(txtFirstNameValidated);
            this.txtMiddleName.Validated += new EventHandler(txtMiddleNameValidated);
            this.txtPosition.Validated += new EventHandler(txtPositionValidated);
        }

        #endregion

        #region Class Event Void Procedures

        //###########################################CLASS SystemUser EVENTS####################################################
        //event is raised when the class is loaded
        protected virtual void ClassLoad(object sender, EventArgs e)
        {
            if (!DatabaseLib.ProcStatic.IsSystemAccessAdmin(_userInfo))
            {
                DentalLib.ProcStatic.ShowErrorDialog("You are not allowed to access this module.",
                    "System User Information");

                _hasErrors = true;

                this.Close();
            }

            _testUserInfo = new CommonExchange.SysAccess();
            _testUserInfo.UserStatus = true;

            _userManager.InitializeAccessRightsComboBox(this.cboAccessRights);

        } //-------------------------------
        //#######################################END CLASS SystemUser EVENTS####################################################

        //##########################################TEXTBOX txtUsername EVENTS##################################################
        //event is raised when the control is validated
        private void txtUsernameValidated(object sender, EventArgs e)
        {
            _testUserInfo.UserName = DentalLib.ProcStatic.TrimStartEndString(this.txtUsername.Text);
        } //---------------------------------------
        //######################################END TEXTBOX txtUsername EVENTS##################################################

        //#########################################TEXTBOX txtPassword EVENTS###################################################
        //event is raised when the control is validated
        private void txtPasswordValidated(object sender, EventArgs e)
        {
            _testUserInfo.Password = DentalLib.ProcStatic.TrimStartEndString(this.txtPassword.Text);
        } //-------------------------
        //#######################################END TEXTBOX txtPassword EVENTS#################################################

        //######################################LABEL lblStatus EVENTS###########################################################
        //event is raised when the label is clicked
        private void lblStatusClick(object sender, EventArgs e)
        {
            _testUserInfo.UserStatus = !_testUserInfo.UserStatus;

            if (_testUserInfo.UserStatus)
            {
                this.lblStatus.Text = "ACTIVE";
            }
            else
            {
                this.lblStatus.Text = "DEACTIVATED";
            }
        } //-----------------------------------
        //####################################END LABEL lblStatus EVENTS##########################################################

        //########################################COMBOBOX cboAccessRights EVENTS#################################################
        //event is raised when the selected index is changed
        private void cboAccessRightsSelectedIndexChanged(object sender, EventArgs e)
        {
            _testUserInfo.AccessCode = _userManager.GetUserAccessCode(this.cboAccessRights.SelectedIndex);
        } //---------------------------------
        //#######################################END COMBOBOX cboAccessRights EVENTS################################################

        //###########################################TEXTBOX txtLastName EVENTS#####################################################
        //event is raised when the control is validated
        private void txtLastNameValidated(object sender, EventArgs e)
        {
            _testUserInfo.LastName = DentalLib.ProcStatic.TrimStartEndString(this.txtLastName.Text);
        } //------------------------------
        //#######################################END TEXTBOX txtLastName EVENTS#####################################################

        //#########################################TEXTBOX txtFirstName EVENTS#######################################################
        //event is raised when the control is validated
        private void txtFirstNameValidated(object sender, EventArgs e)
        {
            _testUserInfo.FirstName = DentalLib.ProcStatic.TrimStartEndString(this.txtFirstName.Text);
        } //------------------------
        //#########################################TEXTBOX txtFirstName EVENTS#######################################################

        //########################################TEXTBOX txtMiddleName EVENTS#########################################################
        //event is raised when the control is validated
        private void txtMiddleNameValidated(object sender, EventArgs e)
        {
            _testUserInfo.MiddleName = DentalLib.ProcStatic.TrimStartEndString(this.txtMiddleName.Text);
        } //-------------------------------
        //######################################END TEXTBOX txtMiddleName EVENTS#######################################################

        //########################################TEXTBOX txtPosition EVENTS###########################################################
        //event is raised when the control is validated
        private void txtPositionValidated(object sender, EventArgs e)
        {
            _testUserInfo.Position = DentalLib.ProcStatic.TrimStartEndString(this.txtPosition.Text);
        } //----------------------------------
        //#######################################END TEXTBOX txtPosition EVENTS#########################################################
        #endregion

        #region Programmer-Defined Function Procedures

        //this function determines if the controls are validated
        protected Boolean ValidateControls()
        {
            Boolean isValid = true;

            _errProvider.SetError(this.txtUsername, "");
            _errProvider.SetError(this.txtPassword, "");
            _errProvider.SetError(this.txtConfirm, "");
            _errProvider.SetError(this.cboAccessRights, "");
            _errProvider.SetError(this.txtLastName, "");
            _errProvider.SetError(this.txtFirstName, "");
            
            if (String.IsNullOrEmpty(_testUserInfo.UserName))
            {
                _errProvider.SetError(this.txtUsername, "A user name is required.");
                isValid = false;
            }

            if (String.IsNullOrEmpty(_testUserInfo.Password))
            {
                _errProvider.SetError(this.txtPassword, "A password is required.");
                isValid = false;
            }
            else if (!String.Equals(_testUserInfo.Password, this.txtConfirm.Text))
            {
                _errProvider.SetError(this.txtPassword, "The passwords does not match.");
                _errProvider.SetError(this.txtConfirm, "The passwords does not match.");
                isValid = false;
            }

            if (isValid && _userManager.IsExistsNameSystemUserInformation(_userInfo, _testUserInfo))
            {
                _errProvider.SetError(this.txtUsername, "The system has rejected the selected username");
                _errProvider.SetError(this.txtPassword, "The system has rejected the selected password.");
                isValid = false;
            }

            if (String.IsNullOrEmpty(_testUserInfo.AccessCode))
            {
                _errProvider.SetError(this.cboAccessRights, "A user access rights is required.");
                isValid = false;
            }

            if (String.IsNullOrEmpty(_testUserInfo.LastName))
            {
                _errProvider.SetError(this.txtLastName, "A user last name is required.");
                isValid = false;
            }

            if (String.IsNullOrEmpty(_testUserInfo.FirstName))
            {
                _errProvider.SetError(this.txtFirstName, "A user first name is required.");
                isValid = false;
            }

            return isValid;
        } //------------------------

        #endregion
    }
}
