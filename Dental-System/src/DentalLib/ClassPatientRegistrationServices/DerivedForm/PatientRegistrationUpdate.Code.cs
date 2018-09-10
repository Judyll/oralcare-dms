using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace DentalLib
{
    partial class PatientRegistrationUpdate
    {
        #region Class General Variable Declarations
        private CommonExchange.Registration _regInfoTemp;
        private String _registrationSysId;
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
        public PatientRegistrationUpdate(CommonExchange.SysAccess userInfo, RegistrationLogic regManager, CommonExchange.Patient patientInfo, 
            String registrationSysId)
            :
            base(userInfo, regManager, patientInfo)
        {
            this.InitializeComponent();

            _registrationSysId = registrationSysId;

            this.FormClosing += new FormClosingEventHandler(ClassClosing);
            this.btnUpdate.Click += new EventHandler(btnUpdateClick);
            this.btnClose.Click += new EventHandler(btnCloseClick);
        }
        #endregion

        #region Class Event Void Procedures
        //#####################################CLASS PatientRegistrationUpdate EVENTS########################################
        //event is raised when the class is loaded
        protected override void ClassLoad(object sender, EventArgs e)
        {
            if (!DatabaseLib.ProcStatic.IsSystemAccessAdmin(_userInfo) && !DatabaseLib.ProcStatic.IsSystemAccessDentalUser(_userInfo))
            {
                DentalLib.ProcStatic.ShowErrorDialog("You are not allowed to access this module.",
                    "Patient Registration");

                _hasErrors = true;

                this.Close();
            }
            else if (!DatabaseLib.ProcStatic.IsSystemAccessAdmin(_userInfo))
            {
                this.lnkChange.Visible = this.btnUpdate.Visible = false;

                _hasErrors = true;
            }

            this.lblPatientSysId.Text = _patientInfo.PatientSystemId;
            this.lblName.Text = _patientInfo.LastName.ToUpper() + ", " + _patientInfo.FirstName + " " + _patientInfo.MiddleName;

            _regInfo = _regManager.GetPatientRegistrationDetails(_registrationSysId);
            _regInfoTemp = _regInfo;

            this.lblSysID.Text = _regInfo.RegistrationSystemId;
            this.lblDate.Text = DateTime.Parse(_regInfo.RegistrationDate).ToLongDateString();


        } //----------------------------------

        //event is raised when the class is closingss
        private void ClassClosing(object sender, FormClosingEventArgs e)
        {
            if (!_hasUpdated && !_hasErrors && !_regInfo.Equals(_regInfoTemp))
            {
                String strMsg = "There has been changes made in the current patient registration information. \nExiting will not save this changes." +
                                "\n\nAre you sure you want to exit?";
                DialogResult msgResult = MessageBox.Show(strMsg, "Confirm Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (msgResult == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }

        } //----------------------------------
        //####################################END CLASS PatientRegistrationUpdate EVENTS######################################

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
            try
            {
                String strMsg = "Are you sure you want to update the patient registration?";

                DialogResult msgResult = MessageBox.Show(strMsg, "Confirm Update", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (msgResult == DialogResult.Yes)
                {
                    strMsg = "The patient registration has been successfully updated.";

                    _regManager.UpdatePatientRegistration(_userInfo, _regInfo);

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
        } //---------------------------------------
        //############################################END BUTTON btnUpdate EVENTS############################################


        #endregion


    }
}
