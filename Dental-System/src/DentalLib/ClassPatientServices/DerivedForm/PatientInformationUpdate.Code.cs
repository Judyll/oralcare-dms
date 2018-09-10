using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace DentalLib
{
    partial class PatientInformationUpdate
    {
        #region Class General Variable Declarations
        private CommonExchange.Patient _patientInfoTemp;
        private String _patientSysId;
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
        public PatientInformationUpdate(CommonExchange.SysAccess userInfo, String patientSysId)
            :
            base(userInfo)
        {
            this.InitializeComponent();

            _patientSysId = patientSysId;

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
            if (!DatabaseLib.ProcStatic.IsSystemAccessAdmin(_userInfo) && !DatabaseLib.ProcStatic.IsSystemAccessDentalUser(_userInfo))
            {
                DentalLib.ProcStatic.ShowErrorDialog("You are not allowed to access this module.",
                    "Patient Information");

                _hasErrors = true;

                this.Close();
            }

            _patientManager.DeleteImageDirectory(Application.StartupPath);

            _patientInfo = _patientManager.SelectByPatientSystemIdPatientInformation(_userInfo, _patientSysId, Application.StartupPath);
            _patientInfoTemp = _patientInfo;

            if (!String.IsNullOrEmpty(_patientInfo.ImagePath))
            {
                this.pbxPatient.Image = Image.FromFile(_patientInfo.ImagePath);
            }
            
            this.lblSysID.Text = _patientInfo.PatientSystemId;
            this.txtLastName.Text = _patientInfo.LastName;
            this.txtFirstName.Text = _patientInfo.FirstName;
            this.txtMiddleName.Text = _patientInfo.MiddleName;
            this.txtBirthdate.Text = DateTime.Parse(_patientInfo.BirthDate).ToLongDateString();
            this.txtPhone.Text = _patientInfo.PhoneNos;
            this.txtEmail.Text = _patientInfo.Email;
            this.txtAddress.Text = _patientInfo.HomeAddress;
            this.txtMedicalHistory.Text = _patientInfo.MedicalHistory;
            this.txtContactPerson.Text = _patientInfo.EmergencyInfo;

            this.txtLastName.Focus();

        } //----------------------------------

        //event is raised when the class is closing
        private void ClassClosing(object sender, FormClosingEventArgs e)
        {
            if (!_hasUpdated && !_patientInfo.Equals(_patientInfoTemp))
            {
                String strMsg = "There has been changes made in the current procedure information. \nExiting will not save this changes." +
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
                    String strMsg = "Are you sure you want to update the patient information?";

                    DialogResult msgResult = MessageBox.Show(strMsg, "Confirm Update", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                    if (msgResult == DialogResult.Yes)
                    {
                        strMsg = "The patient information has been successfully updated.";

                        _patientManager.UpdatePatientInformation(_userInfo, _patientInfo);

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
