using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace DentalLib
{
    partial class PatientInformation
    {
        #region Class Member Declarations
        protected CommonExchange.SysAccess _userInfo;
        protected CommonExchange.Patient _patientInfo;
        protected PatientLogic _patientManager;
        protected Boolean _hasErrors = false;

        private ErrorProvider _errProvider;
        #endregion

        #region Class Properties Declarations
        public CommonExchange.Patient PatientInfo
        {
            get { return _patientManager.PatientInfo; }
        }
        #endregion

        #region Class Constructor
        public PatientInformation()
        {
            this.InitializeComponent();
        }

        public PatientInformation(CommonExchange.SysAccess userInfo)
        {
            this.InitializeComponent();

            _userInfo = userInfo;

            _errProvider = new ErrorProvider();

            _patientManager = new PatientLogic(userInfo);

            this.Load += new EventHandler(ClassLoad);
            this.FormClosed += new FormClosedEventHandler(ClassClosed);
            this.pbxPatient.Click += new EventHandler(pbxPatientClick);
            this.txtLastName.Validated += new EventHandler(txtLastNameValidated);
            this.txtFirstName.Validated += new EventHandler(txtFirstNameValidated);
            this.txtMiddleName.Validated += new EventHandler(txtMiddleNameValidated);
            this.lnkChange.LinkClicked += new LinkLabelLinkClickedEventHandler(lnkChangeLinkClicked);
            this.txtPhone.Validated += new EventHandler(txtPhoneValidated);
            this.txtEmail.Validated += new EventHandler(txtEmailValidated);
            this.txtAddress.Validated += new EventHandler(txtAddressValidated);
            this.txtMedicalHistory.Validated += new EventHandler(txtMedicalHistoryValidated);
            this.txtContactPerson.Validated += new EventHandler(txtContactPersonValidated);
        }
        
        #endregion

        #region Class Event Void Procedures

        //############################################CLASS ProcedureInformation EVENTS#######################################################
        //event is raised when the class is loaded
        protected virtual void ClassLoad(object sender, EventArgs e)
        {
            if (!DatabaseLib.ProcStatic.IsSystemAccessAdmin(_userInfo) && !DatabaseLib.ProcStatic.IsSystemAccessDentalUser(_userInfo))
            {
                DentalLib.ProcStatic.ShowErrorDialog("You are not allowed to access this module.",
                    "Patient Information");

                _hasErrors = true;

                this.Close();
            }

            _patientManager.DeleteImageDirectory(Application.StartupPath);

            _patientInfo = new CommonExchange.Patient();
            _patientInfo.BirthDate = _patientManager.ServerDateTime;
            _patientInfo.EmergencyInfo = this.txtContactPerson.Text;

            this.txtBirthdate.Text = DateTime.Parse(_patientManager.ServerDateTime).ToLongDateString();
        }

        //event is raised when the class is closed
        private void ClassClosed(object sender, FormClosedEventArgs e)
        {
            if (this.pbxPatient.Image != null)
            {
                pbxPatient.Image.Dispose();
                pbxPatient.Image = null;

                pbxPatient.Dispose();
                pbxPatient = null;

                GC.SuppressFinalize(this);
                GC.Collect();
            }            
                
        } //---------------------------
        //############################################END CLASS ProcedureInformation EVENTS#######################################################

        //#############################################PICTUREBOX pbxPatient EVENTS##############################################################
        //event is raised when the picture box is clicked
        private void pbxPatientClick(object sender, EventArgs e)
        {
            using (OpenFileDialog imageChooser = new OpenFileDialog())
            {
                imageChooser.Title = "Select an image";
                imageChooser.Filter = "All image file (*.jpg, *.jpeg, *.bmp, *.gif) | *.jpg; *.jpeg; *.bmp; *.gif";
                imageChooser.Multiselect = false;
                imageChooser.CheckFileExists = true;
                imageChooser.CheckPathExists = true;

                //determines if an image has been selected
                if (imageChooser.ShowDialog() == DialogResult.OK)
                {
                    this.Cursor = Cursors.WaitCursor;

                    this.pbxPatient.Image = Image.FromFile(imageChooser.FileName);

                    _patientInfo.ImagePath = imageChooser.FileName;
                    _patientInfo.ImageBytes = DentalLib.ProcStatic.GetImageByte(imageChooser.FileName);
                    _patientInfo.ImageExtension = _patientManager.GetImageExtensionName(imageChooser.FileName);

                    this.Cursor = Cursors.Arrow;

                } //--------------------------

            }
        } //--------------------------------
        //#############################################END PICTUREBOX pbxPatient EVENTS##########################################################

        //############################################TEXTBOX txtLastName EVENTS##################################################################
        //event is raised when the control is validated
        private void txtLastNameValidated(object sender, EventArgs e)
        {
            _patientInfo.LastName = DentalLib.ProcStatic.TrimStartEndString(txtLastName.Text);
        } //---------------------------------
        //##########################################END TEXTBOX txtLastName EVENTS################################################################

        //###############################################TEXTBOX txtFirstName EVENTS################################################################
        //event is raised when the control is validated
        private void txtFirstNameValidated(object sender, EventArgs e)
        {
            _patientInfo.FirstName = DentalLib.ProcStatic.TrimStartEndString(txtFirstName.Text);
        } //--------------------------------
        //############################################END TEXTBOX txtFirstName EVENTS###############################################################

        //###############################################TEXTBOX txtMiddleName EVENTS###############################################################
        //event is raised when the control is validated
        private void txtMiddleNameValidated(object sender, EventArgs e)
        {
            _patientInfo.MiddleName = DentalLib.ProcStatic.TrimStartEndString(txtMiddleName.Text);
        } //---------------------------------------
        //##############################################END TEXTBOX txtMiddleName EVENTS############################################################

        //##############################################LINKBUTTON lnkChange EVENTS################################################################
        //event is raised when the link is clicked
        private void lnkChangeLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            DateTime bDate = DateTime.Parse(_patientInfo.BirthDate);

            using (DatePicker frmPicker = new DatePicker(bDate))
            {
                frmPicker.ShowDialog(this);

                if (frmPicker.HasSelectedDate)
                {
                    if (DateTime.TryParse(frmPicker.GetSelectedDate.ToShortDateString() + " 12:00:00 AM", out bDate))
                    {
                        _patientInfo.BirthDate = bDate.ToString();
                    }

                    txtBirthdate.Text = DateTime.Parse(_patientInfo.BirthDate).ToLongDateString();
                }
            }

            this.Cursor = Cursors.Arrow;
            
        } //-------------------------------
        //############################################END LINKBUTTON lnkChange EVENTS##############################################################

        //#################################################TEXTBOX txtPhone EVENTS#################################################################
        //event is raised when the control is validated
        private void txtPhoneValidated(object sender, EventArgs e)
        {
            _patientInfo.PhoneNos = DentalLib.ProcStatic.TrimStartEndString(txtPhone.Text);
        } //-------------------------------
        //###############################################END TEXTBOX txtPhone EVENTS###############################################################

        //################################################TEXTBOX txtEmail EVENTS###############################################################
        //event is raised when the control is validated
        private void txtEmailValidated(object sender, EventArgs e)
        {
            _patientInfo.Email = DentalLib.ProcStatic.TrimStartEndString(txtEmail.Text);
        } //-----------------------------------
        //##############################################END TEXTBOX txtEmail EVENTS################################################################

        //#################################################TEXTBOX txtAddress EVENTS##############################################################
        //event is raised when the control is validated
        private void txtAddressValidated(object sender, EventArgs e)
        {
            _patientInfo.HomeAddress = DentalLib.ProcStatic.TrimStartEndString(txtAddress.Text);
        } //-------------------------------
        //################################################END TEXTBOX txtAddress EVENTS###########################################################

        //#################################################TEXTBOX txtMedicalHistory EVENTS#######################################################
        //event is raised when the control is validated
        private void txtMedicalHistoryValidated(object sender, EventArgs e)
        {
            _patientInfo.MedicalHistory = DentalLib.ProcStatic.TrimStartEndString(txtMedicalHistory.Text);
        }//---------------------------------
        //#############################################END TEXTBOX txtMedicalHistory EVENTS########################################################

        //###############################################TEXTBOX txtContactPerson EVENTS##########################################################
        //event is raised when the control is validated
        private void txtContactPersonValidated(object sender, EventArgs e)
        {
            _patientInfo.EmergencyInfo = DentalLib.ProcStatic.TrimStartEndString(txtContactPerson.Text);
        } //------------------------------------
        //#############################################END TEXTBOX txtContactPerson EVENTS#########################################################

        #endregion

        #region Programmer-Defined Function Procedures

        //this function determines if the controls are validated
        protected Boolean ValidateControls()
        {
            Boolean isValid = true;

            _errProvider.SetError(this.txtLastName, "");
            _errProvider.SetError(this.txtFirstName, "");

            if (String.IsNullOrEmpty(_patientInfo.LastName))
            {
                _errProvider.SetError(this.txtLastName, "A patient last name is required.");
                isValid = false;
            }

            if (String.IsNullOrEmpty(_patientInfo.FirstName))
            {
                _errProvider.SetError(this.txtFirstName, "A patient first name is required.");
                isValid = false;
            }

            return isValid;
        } //------------------------

        #endregion
    }
}
