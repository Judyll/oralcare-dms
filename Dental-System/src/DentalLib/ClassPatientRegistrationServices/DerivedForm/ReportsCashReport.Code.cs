using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace DentalLib
{
    partial class ReportsCashReport
    {
        #region Class Constructor
        public ReportsCashReport(CommonExchange.SysAccess userInfo, DateTime dateFrom, DateTime dateTo) 
            : 
            base(userInfo)
        {
            this.InitializeComponent();

            _userInfo = userInfo;
            _dateFrom = dateFrom;
            _dateTo = dateTo;

            this.FormClosed += new FormClosedEventHandler(ClassClosed);
            this.btnPrint.Click += new EventHandler(btnPrintClick);
            
        }
                
        #endregion

        #region Class Event Void Procedures
        //####################################CLASS ReportsCashReport EVENTS################################################
        //event is raised when the class is loaded
        protected override void ClassLoad(object sender, EventArgs e)
        {
            try
            {                
                base.ClassLoad(sender, e);

                _regManager.DeleteImageDirectory(Application.StartupPath);

                this.SetReportData();

            }
            catch (Exception ex)
            {
                DentalLib.ProcStatic.ShowErrorDialog(ex.Message, "Error Loading Cash Report Module");
                this.Close();
            }

        } //-----------------------------------------

        //event is raised when the class is closed
        private void ClassClosed(object sender, FormClosedEventArgs e)
        {
            
        } //-----------------------------------    
        //##################################END CLASS ReportsCashReport EVENTS##############################################

        //##############################################LINKBUTTON lnkChange EVENTS##########################################################
        //event is raised when the link button is clicked
        protected override void lnkChangeLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Boolean appliedOptions = false;

            using (DentalLib.ReportOptionsCashReport frmOptions = new DentalLib.ReportOptionsCashReport(_dateFrom, _dateTo))
            {
                frmOptions.ShowDialog(this);

                if (frmOptions.ApplyOptions)
                {
                    appliedOptions = frmOptions.ApplyOptions;
                    _dateFrom = frmOptions.DateFrom;
                    _dateTo = frmOptions.DateTo;

                    this.SetReportData();
                }
            }
        } //--------------------------------
        //############################################END LINKBUTTON lnkChange EVENTS########################################################

        //#############################################BUTTON btnPrint EVENTS################################################################
        //event is raised whe the button is clicked
        private void btnPrintClick(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            _regManager.PrintCashReport(_userInfo, _dateFrom, _dateTo);

            this.Cursor = Cursors.Arrow;
        } //----------------------------------
        //##########################################END BUTTON btnPrint EVENTS###############################################################
        #endregion

        #region Programmer-Defined Void Procedures

        //this procedure selects the first row in the datagridview
        protected override void OnDoubleClickEnter(String id)
        {
            this.Cursor = Cursors.WaitCursor;

            CommonExchange.Registration regInfo = _regManager.GetRegistrationDetailsForCashReport(id);
            CommonExchange.Patient patientInfo = _regManager.SelectByPatientSystemIdPatientInformation(_userInfo,
                _regManager.GetPatientSystemIdForCashReport(id), Application.StartupPath);

            using (DentalLib.PatientChargesSummary frmSummary = new PatientChargesSummary(_userInfo, patientInfo, regInfo))
            {
                frmSummary.ShowDialog(this);

                if (frmSummary.HasUpdated || frmSummary.HasDeleted)
                {
                    this.SetReportData();
                }
            }

            this.Cursor = Cursors.Arrow;

        } //-----------------------

        //this procedure sets the data
        private void SetReportData()
        {
            this.Cursor = Cursors.WaitCursor;

            this.SetDataGridViewSource(_regManager.SelectForCashReportPatientRegistration(_userInfo, _dateFrom.ToString(), _dateTo.ToString()));
            this.SetDateCoveredString(_dateFrom.ToLongDateString() + "   to   " + _dateTo.ToLongDateString());
            this.SetTotalString("Php " + _regManager.GetTotalForCashReport().ToString("N"));

            this.Cursor = Cursors.Arrow;
        } //-----------------------------------

        #endregion
    }
}
