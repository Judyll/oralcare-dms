using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace DentalLib
{
    partial class PatientChargesUpdate
    {
        #region Class Member Declarations
        protected CommonExchange.RegistrationDetails _detailsInfoTemp;
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
        public PatientChargesUpdate(CommonExchange.SysAccess userInfo, CommonExchange.Patient patientInfo, CommonExchange.Registration regInfo, 
            CommonExchange.RegistrationDetails detailsInfo, ChargesLogic chargesManager)
            : base(userInfo, patientInfo, regInfo, chargesManager)
        {
            this.InitializeComponent();

            _detailsInfo = detailsInfo;
            _detailsInfoTemp = detailsInfo;

            this.FormClosing += new FormClosingEventHandler(ClassClosing);
            this.btnUpdate.Click += new EventHandler(btnUpdateClick);
            this.btnDelete.Click += new EventHandler(btnDeleteClick);
            this.btnClose.Click += new EventHandler(btnCloseClick);
        }
        #endregion

        #region Class Event Void Procedures
        //#####################################CLASS PatientChargesUpdate EVENTS########################################
        //event is raised when the class is loaded
        protected override void ClassLoad(object sender, EventArgs e)
        {
            if (!DatabaseLib.ProcStatic.IsSystemAccessAdmin(_userInfo) && !DatabaseLib.ProcStatic.IsSystemAccessDentalUser(_userInfo))
            {
                DentalLib.ProcStatic.ShowErrorDialog("You are not allowed to access this module.",
                    "Patient Charges");

                _hasErrors = true;

                this.Close();
            }
            else if (!DatabaseLib.ProcStatic.IsSystemAccessAdmin(_userInfo))
            {
                this.btnUpdate.Visible = this.btnDelete.Visible = this.lnkChange.Visible = false;

                _hasErrors = true;
            }

            this.lblSysId.Text = _detailsInfo.DetailsId.ToString();
            this.lblDate.Text = DateTime.Parse(_detailsInfo.DateAdministered).ToLongDateString();
            this.lblProcedureId.Text = _detailsInfo.ProcedureSystemId;
            this.lblProcedureName.Text = _detailsInfo.ProcedureName;
            this.txtToothNo.Text = _detailsInfo.ToothNo.ToString();     
            this.txtAmount.Text = _detailsInfo.Amount.ToString("N");
            this.txtRemarks.Text = _detailsInfo.Remarks;

        } //----------------------------------

        //event is raised when the class is closing
        private void ClassClosing(object sender, FormClosingEventArgs e)
        {
            if ((!_hasUpdated && !_hasDeleted && !_hasErrors) && !_detailsInfo.Equals(_detailsInfoTemp))
            {
                String strMsg = "There has been changes made in the current patient charges information. \nExiting will not save this changes." +
                                "\n\nAre you sure you want to exit?";
                DialogResult msgResult = MessageBox.Show(strMsg, "Confirm Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (msgResult == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }

        } //----------------------------------
        //####################################END CLASS PatientChargesUpdate EVENTS######################################

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
                    String strMsg = "Are you sure you want to update the patient charges information?";

                    DialogResult msgResult = MessageBox.Show(strMsg, "Confirm Update", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                    if (msgResult == DialogResult.Yes)
                    {
                        strMsg = "The patient charges information has been successfully updated.";

                        _chargesManager.UpdateRegistrationDetails(_userInfo, _detailsInfo);

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

        //###############################################BUTTON btnDelete EVENTS################################################
        //event is raised when the button is clicked
        private void btnDeleteClick(object sender, EventArgs e)
        {
            try
            {
                String strMsg = "Deleting a patient charges information might affect the following:" +
                                "\n\n1.) The system inventory." +
                                "\n\nAre you sure you want to delete the patient charges information?";

                DialogResult msgResult = MessageBox.Show(strMsg, "Confirm Delete", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);

                if (msgResult == DialogResult.Yes)
                {
                    strMsg = "Are you really sure you want to delete the patient charges information?";

                    msgResult = MessageBox.Show(strMsg, "Confirm Delete", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                    if (msgResult == DialogResult.Yes)
                    {
                        strMsg = "The patient charges information has been successfully deleted.";

                        _chargesManager.DeleteRegistrationDetails(_userInfo, _detailsInfo);

                        MessageBox.Show(strMsg, "Success", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                        _hasDeleted = true;

                        this.Close();
                    }
                    else if (msgResult == DialogResult.Cancel)
                    {
                        this.Close();
                    }
                }
                else if (msgResult == DialogResult.Cancel)
                {
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                DentalLib.ProcStatic.ShowErrorDialog(ex.Message, "Error in Deleting");
            }

        } //------------------------------
        //#############################################END BUTTON btnDelete EVENTS##############################################


        #endregion


    }
}
