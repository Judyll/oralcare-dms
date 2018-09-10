using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace DentalLib
{
    partial class MedicalPrescription
    {
        #region Class Member Declarations
        private CommonExchange.SysAccess _userInfo;
        private CommonExchange.Registration _regInfo;
        private RegistrationLogic _regManager;

        private String _strPrescriptionTemp;
        
        #endregion

        #region Class Properties Declarations
        private Boolean _hasSaved = false;
        public Boolean HasSaved
        {
            get { return _hasSaved; }
        }

        public String PatientMedicalPrescription
        {
            get { return DentalLib.ProcStatic.TrimStartEndString(this.txtPrescription.Text); }
        }
        #endregion

        #region Class Constructor
        public MedicalPrescription(CommonExchange.SysAccess userInfo, CommonExchange.Registration regInfo)
        {
            this.InitializeComponent();

            _userInfo = userInfo;
            _regInfo = regInfo;

            this.Load += new EventHandler(ClassLoad);
            this.FormClosing += new FormClosingEventHandler(ClassClosing);
            this.txtPrescription.Validated += new EventHandler(txtPrescriptionValidated);
            this.btnClose.Click += new EventHandler(btnCloseClick);
            this.btnSave.Click += new EventHandler(btnSaveClick);
            
        }
        
        #endregion

        #region Class Event Void Procedures
        //#######################################CLASS MedicalPrescription EVENTS###########################################
        //event is raised when the class is loaded
        private void ClassLoad(object sender, EventArgs e)
        {
            _strPrescriptionTemp = _regInfo.MedicalPrescription;

            this.txtPrescription.Text = _regInfo.MedicalPrescription;
            this.txtPrescription.Select(0, 0);

            _regManager = new RegistrationLogic(_userInfo);

        } //-----------------------------------

        //event is raised when the class is closing
        private void ClassClosing(object sender, FormClosingEventArgs e)
        {
            if (!_hasSaved && !String.Equals(_strPrescriptionTemp, _regInfo.MedicalPrescription))
            {
                String strMsg = "There has been changes made in the current medical prescription. \nExiting will not save this changes." +
                                "\n\nAre you sure you want to exit?";
                DialogResult msgResult = MessageBox.Show(strMsg, "Confirm Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (msgResult == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }

        } //----------------------------------
        //#####################################END CLASS MedicalPrescription EVENTS#########################################

        //#########################################TEXTBOX txtPrescription EVENTS##############################################
        //event is raised when the control is validated
        private void txtPrescriptionValidated(object sender, EventArgs e)
        {
            _regInfo.MedicalPrescription = DentalLib.ProcStatic.TrimStartEndString(this.txtPrescription.Text);
        } //----------------------------------------
        //#######################################END TEXTBOX txtPrescription EVENTS#############################################

        //#########################################BUTTON btnClose EVENTS##################################################
        //event is raised when the button is clicked
        private void btnCloseClick(object sender, EventArgs e)
        {
            this.Close();
        } //------------------------------------------
        //#######################################END BUTTON btnClose EVENTS#################################################

        //##########################################BUTTON btnSave EVENTS#####################################################
        //event is raised when the button is clicked
        private void btnSaveClick(object sender, EventArgs e)
        {
            try
            {
                String strMsg = "Are you sure you want to save the patient's medical prescription?";

                DialogResult msgResult = MessageBox.Show(strMsg, "Confirm Save", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (msgResult == DialogResult.Yes)
                {
                    strMsg = "The patient's medical prescription has been successfully saved.";

                    _regManager.UpdateMedicalPrescriptionPatientRegistration(_userInfo, _regInfo);

                    MessageBox.Show(strMsg, "Success", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                    _hasSaved = true;

                    this.Close();
                }
                else if (msgResult == DialogResult.Cancel)
                {
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                DentalLib.ProcStatic.ShowErrorDialog(ex.Message, "Error in Saving");
            }
        } //--------------------------------------
        //#########################################END BUTTON btnSave EVENTS##################################################
        #endregion
    }
}
