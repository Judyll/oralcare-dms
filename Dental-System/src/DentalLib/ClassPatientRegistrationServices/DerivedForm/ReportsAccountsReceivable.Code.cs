using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace DentalLib
{
    partial class ReportsAccountsReceivable
    {
        #region Class Constructor
        public ReportsAccountsReceivable(CommonExchange.SysAccess userInfo)
            : 
            base(userInfo)
        {
            this.InitializeComponent();

            _userInfo = userInfo;

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

                this.HideChangeDateLink(true);

            }
            catch (Exception ex)
            {
                DentalLib.ProcStatic.ShowErrorDialog(ex.Message, "Error Loading Accounts Receivable Report Module");
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
            
        } //--------------------------------
        //############################################END LINKBUTTON lnkChange EVENTS########################################################

        //#############################################BUTTON btnPrint EVENTS################################################################
        //event is raised whe the button is clicked
        private void btnPrintClick(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            _regManager.PrintAccountsReceivableReport(_userInfo);

            this.Cursor = Cursors.Arrow;
        } //----------------------------------
        //##########################################END BUTTON btnPrint EVENTS###############################################################
        #endregion

        #region Programmer-Defined Void Procedures

        //this procedure selects the first row in the datagridview
        protected override void OnDoubleClickEnter(String id)
        {
            this.Cursor = Cursors.WaitCursor;

            CommonExchange.Registration regInfo = _regManager.GetRegistrationDetailsForAccountsReceivableReport(id);
            CommonExchange.Patient patientInfo = _regManager.SelectByPatientSystemIdPatientInformation(_userInfo,
                _regManager.GetPatientSystemIdForAccountsReceivableReport(id), Application.StartupPath);

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

            this.SetDataGridViewSource(_regManager.SelectForAccountsReceivablePatientRegistration(_userInfo));
            this.SetDateCoveredString("as of " + DateTime.Parse(_regManager.ServerDateTime).ToLongDateString());
            this.SetTotalString("Php " + _regManager.GetTotalForAccountsReceivableReport().ToString("N"));

            this.Cursor = Cursors.Arrow;
        } //-----------------------------------

        #endregion

    }
}
