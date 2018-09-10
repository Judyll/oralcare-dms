using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace DentalLib
{
    partial class PatientRegistrationCreate
    {
        #region Class Properties Declarations
        private Boolean _hasCreated = false;
        public Boolean HasCreated
        {
            get { return _hasCreated; }
        }
        #endregion

        #region Class Constructor
        public PatientRegistrationCreate(CommonExchange.SysAccess userInfo, RegistrationLogic regManager, CommonExchange.Patient patientInfo)
            : base(userInfo, regManager, patientInfo)
        {
            this.InitializeComponent();

            this.FormClosing += new FormClosingEventHandler(ClassClosing);
            this.btnRegister.Click += new EventHandler(btnRegisterClick);
            this.btnCancel.Click += new EventHandler(btnCancelClick);
        }

        
        #endregion

        #region Class Event Void Procedures

        //################################################CLASS ProcedureInformationCreate EVENTS#######################################################

        //event is raised when the class is closing
        private void ClassClosing(object sender, FormClosingEventArgs e)
        {
            if (!_hasCreated && !_hasErrors)
            {
                String strMsg = "Are you sure you want to cancel the new patient registration?";
                DialogResult msgResult = MessageBox.Show(strMsg, "Confirm Cancel", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (msgResult == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }

        } //---------------------------

        //#############################################END CLASS ProcedureInformationCreate EVENTS#######################################################

        //##############################################BUTTON btnCancel EVENTS########################################################
        //event is raised when the button is clicked
        private void btnCancelClick(object sender, EventArgs e)
        {
            this.Close();
        } //------------------------------
        //############################################END BUTTON btnCancel EVENTS######################################################

        //#########################################BUTTON btnRegister EVENTS#####################################################
        //event is raised when the button is clicked
        private void btnRegisterClick(object sender, EventArgs e)
        {
            try
            {
                String strMsg = "Are you sure you want to register the new patient registration?";

                DialogResult msgResult = MessageBox.Show(strMsg, "Confirm Register", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (msgResult == DialogResult.Yes)
                {
                    strMsg = "The patient registration has been successfully registered.";

                    this.Cursor = Cursors.WaitCursor;

                    _regManager.InsertPatientRegistration(_userInfo, _regInfo);

                    this.Cursor = Cursors.Arrow;

                    MessageBox.Show(strMsg, "Success", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                    _hasCreated = true;

                    this.Close();
                }
                else if (msgResult == DialogResult.Cancel)
                {
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                DentalLib.ProcStatic.ShowErrorDialog(ex.Message, "Error Creating a Patient Registration");
            }
            
        } //-----------------------------------
        //#######################################END BUTTON btnRegister EVENTS#####################################################

        #endregion
    }
}
