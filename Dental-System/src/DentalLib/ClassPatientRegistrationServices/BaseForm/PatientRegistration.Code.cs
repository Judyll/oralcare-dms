using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace DentalLib
{
    partial class PatientRegistration
    {
        #region Class Member Declarations
        protected CommonExchange.SysAccess _userInfo;
        protected CommonExchange.Registration _regInfo;
        protected CommonExchange.Patient _patientInfo;
        protected RegistrationLogic _regManager;
        protected Boolean _hasErrors = false; 
        #endregion

        #region Class Constructor
        public PatientRegistration()
        {
            this.InitializeComponent();
        }

        public PatientRegistration(CommonExchange.SysAccess userInfo, RegistrationLogic regManager, CommonExchange.Patient patientInfo)
        {
            this.InitializeComponent();

            _userInfo = userInfo;
            _patientInfo = patientInfo;
            _regManager = regManager;

            this.Load += new EventHandler(ClassLoad);
            this.lnkChange.LinkClicked += new LinkLabelLinkClickedEventHandler(lnkChangeLinkClicked);
            
        }
        
        #endregion

        #region Class Event Void Procedures

        //############################################CLASS PatientRegistration EVENTS#######################################################
        //event is raised when the class is loaded
        protected virtual void ClassLoad(object sender, EventArgs e)
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
                this.lnkChange.Visible = false;
            }

            this.lblPatientSysId.Text = _patientInfo.PatientSystemId;
            this.lblName.Text = _patientInfo.LastName.ToUpper() + ", " + _patientInfo.FirstName + " " + _patientInfo.MiddleName;

            _regInfo = new CommonExchange.Registration();

            _regInfo.PatientSystemId = _patientInfo.PatientSystemId;
            _regInfo.RegistrationDate = _regManager.ServerDateTime;

            this.lblDate.Text = DateTime.Parse(_regInfo.RegistrationDate).ToLongDateString();
        }
        //############################################END CLASS PatientRegistration EVENTS#######################################################

        //#########################################LINKBUTTON lnkChange EVENTS##################################################################
        //event is raised when the link is clicked
        private void lnkChangeLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            DateTime bDate = DateTime.Parse(_regManager.ServerDateTime);

            using (DatePicker frmPicker = new DatePicker(bDate))
            {
                frmPicker.ShowDialog(this);

                if (frmPicker.HasSelectedDate)
                {
                    if (DateTime.TryParse(frmPicker.GetSelectedDate.ToShortDateString() + " " +
                        DateTime.Parse(_regManager.ServerDateTime).ToLongTimeString(), out bDate))
                    {
                        _regInfo.RegistrationDate = bDate.ToString();
                    }

                    this.lblDate.Text = DateTime.Parse(_regInfo.RegistrationDate).ToLongDateString();
                }
            }

            this.Cursor = Cursors.Arrow;
        } //-----------------------------------
        //######################################END LINKBUTTON lnkChange EVENTS#################################################################

        #endregion
    }
}
