using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;

namespace DentalLib
{
    partial class ReportsSalesReport
    {
        #region Class Properties Declarations
        private CheckedListBox _procedureList;
        public CheckedListBox ProcedureCheckedListBox
        {
            get { return _procedureList; }
            set { _procedureList = value; }
        }       
        #endregion

        #region Class Constructor
        public ReportsSalesReport(CommonExchange.SysAccess userInfo, DateTime dateFrom, DateTime dateTo, RegistrationLogic regManager)
            : 
            base(userInfo)
        {
            this.InitializeComponent();

            _userInfo = userInfo;
            _dateFrom = dateFrom;
            _dateTo = dateTo;
            _regManager = regManager;

            this.FormClosed += new FormClosedEventHandler(ClassClosed);
            this.btnPrint.Click += new EventHandler(btnPrintClick);            
        }
        #endregion

        #region Class Event Void Procedures

        //####################################CLASS ReportsSalesReport EVENTS################################################
        //event is raised when the class is loaded
        protected override void ClassLoad(object sender, EventArgs e)
        {
            try
            {
                if (!DatabaseLib.ProcStatic.IsSystemAccessAdmin(_userInfo) && !DatabaseLib.ProcStatic.IsSystemAccessDentalUser(_userInfo))
                {
                    DentalLib.ProcStatic.ShowErrorDialog("You are not allowed to access this module.",
                        "Sales Report");

                    this.Close();
                }

                _regManager.DeleteImageDirectory(Application.StartupPath);

                this.SetReportData();

                this.pgbSales.Visible = false;

            }
            catch (Exception ex)
            {
                DentalLib.ProcStatic.ShowErrorDialog(ex.Message, "Error Loading Sales Report Module");
                this.Close();
            }

        } //-----------------------------------------

        //event is raised when the class is closed
        private void ClassClosed(object sender, FormClosedEventArgs e)
        {

        } //-----------------------------------    
        //##################################END CLASS ReportsSalesReport EVENTS##############################################

        //##############################################LINKBUTTON lnkChange EVENTS##########################################################
        //event is raised when the link button is clicked
        protected override void lnkChangeLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Boolean appliedOptions = false;

            using (DentalLib.ReportOptionsSalesReport frmOptions = new DentalLib.ReportOptionsSalesReport(_userInfo, _dateFrom, 
                _dateTo, _regManager))
            {
                frmOptions.ShowDialog(this);

                if (frmOptions.ApplyOptions)
                {
                    appliedOptions = frmOptions.ApplyOptions;
                    _dateFrom = frmOptions.DateFrom;
                    _dateTo = frmOptions.DateTo;
                    _procedureList = frmOptions.ProcedureCheckedListBox;

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

            _regManager.PrintSalesReport(_userInfo, _dateFrom, _dateTo);

            this.Cursor = Cursors.Arrow;
        } //----------------------------------
        //##########################################END BUTTON btnPrint EVENTS###############################################################
        #endregion

        #region Programmer-Defined Void Procedures

        //this procedure selects the first row in the datagridview
        protected override void OnDoubleClickEnter(String id)
        {
            this.Cursor = Cursors.WaitCursor;

            CommonExchange.Registration regInfo = _regManager.GetRegistrationDetailsForSalesReport(id);
            CommonExchange.Patient patientInfo = _regManager.SelectByPatientSystemIdPatientInformation(_userInfo,
                _regManager.GetPatientSystemIdForSalesReport(id), Application.StartupPath);

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

            if (_procedureList != null)
            {
                this.pgbSales.Visible = true;

                _regManager.SetSalesReportTable(_userInfo, _dateFrom.ToString(), _dateTo.ToString(), _procedureList, this.pgbSales);

                this.pgbSales.Visible = false;
            }

            this.SetDataGridViewSource(_regManager.SelectForSalesPatientRegistration());
            this.SetDateCoveredString(_dateFrom.ToLongDateString() + "   to   " + _dateTo.ToLongDateString());
            this.SetTotalString("Php " + _regManager.GetTotalForSalesReport().ToString("N"));

            this.Cursor = Cursors.Arrow;
        } //-----------------------------------

        #endregion

    }
}
