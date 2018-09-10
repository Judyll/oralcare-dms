using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Data;

namespace DentalSystem
{
    partial class DentalSystemManager
    {
        #region Class Member Declarations
        private CommonExchange.SysAccess _userInfo;
        private DentalSystemLogic _dentalManager;
        private PatientSearchList _frmSearch;

        private Timer _tmrMain;
        private DateTime _serverDateTime;
        #endregion

        #region Class Constructor
        public DentalSystemManager(CommonExchange.SysAccess userInfo)
        {
            this.InitializeComponent();

            _userInfo = userInfo;

            _tmrMain = new Timer();
                
            this.Load += new EventHandler(ClassLoad);
            this.FormClosed += new FormClosedEventHandler(ClassClosed);
            this.ctlMain.ClickSalesReport += new DentalLib.ControlManagerButtonClick(ctlMainClickSalesReport);
            this.ctlMain.ClickAccountsReceivable += new DentalLib.ControlManagerButtonClick(ctlMainClickAccountsReceivable);
            this.ctlMain.ClickCashReport += new DentalLib.ControlManagerButtonClick(ctlMainClickCashReport);
            this.ctlMain.ClickUsers += new DentalLib.ControlManagerButtonClick(ctlMainClickUsers);
            this.ctlMain.ClickClose += new DentalLib.ControlManagerButtonClick(ctlMainClickClose);
            this.ctlMain.ClickProcedures += new DentalLib.ControlManagerButtonClick(ctlMainClickProcedures);
            this.ctlMain.OnPressEnter += new DentalLib.ControlManagerPressEnter(ctlMainOnPressEnter);
            this.ctlMain.OnTextboxKeyUp += new DentalLib.ControlManagerKeyUp(ctlMainOnTextboxKeyUp);
            _tmrMain.Tick += new EventHandler(tmrMainTick);
        }

                      
                        
        #endregion

        #region Class Event Void Procedures

        //#######################################CLASS DentalSystemManager EVENTS#########################################
        //event is raised when the class is loaded        
        private void ClassLoad(object sender, EventArgs e)
        {
            try
            {
                _dentalManager = new DentalSystemLogic(_userInfo);

                _serverDateTime = DateTime.Parse(_dentalManager.ServerDateTime);

                tslSpace.Text = "".PadLeft(200);
                tslUser.Text = _userInfo.UserName.PadRight(30);
                tslTime.Text = _serverDateTime.ToLongDateString() + " " + _serverDateTime.ToLongTimeString();

                _tmrMain.Interval = 1000;
                _tmrMain.Start();

                _frmSearch = new PatientSearchList();
                _frmSearch.OnDoubleClickEnter += new SearchListDataGridDoubleClickEnter(_frmSearchOnDoubleClickEnter);
                _frmSearch.LinkClickedCreate += new PatientSearchListCreateLinkClicked(_frmSearchLinkClickedCreate);
                _frmSearch.LinkClickedUpdate += new PatientSearchListUpdateLinkClicked(_frmSearchLinkClickedUpdate);
                _frmSearch.LocationPoint = new Point(50, 300);
                _frmSearch.AdoptGridSize = true;
                _frmSearch.MdiParent = this;                
                
            }
            catch (Exception ex)
            {
                DentalLib.ProcStatic.ShowErrorDialog(ex.Message, "Error Loading Dental System Manager");
                this.Close();
            }            
            
        } //-------------------------        

        //event is raised when the class is closed
        private void ClassClosed(object sender, FormClosedEventArgs e)
        {
            _dentalManager.DeleteImageDirectory(Application.StartupPath);
            
        } //--------------------------
        //#####################################END CLASS DentalSystemManager EVENTS##########################################

        //#####################################CLASS PatientSearchList EVENTS###################################################
        //event is raised when the grid is double clicked
        private void _frmSearchOnDoubleClickEnter(string id)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                using (DentalLib.PatientRegistrationList frmList = new DentalLib.PatientRegistrationList(_userInfo, id))
                {
                    _frmSearch.WindowState = FormWindowState.Minimized;

                    frmList.ShowDialog(this);                    

                    this.ShowSearchResultDialog(false);
                }

                this.Cursor = Cursors.Arrow;
            }
            catch (Exception ex)
            {
                DentalLib.ProcStatic.ShowErrorDialog(ex.Message, "Error Loading Patient Registration List Module");
            }
            
        } //---------------------------

        //event is raised when the link create is clicked
        private void _frmSearchLinkClickedCreate()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                using (DentalLib.PatientInformationCreate frmCreate = new DentalLib.PatientInformationCreate(_userInfo))
                {
                    _frmSearch.WindowState = FormWindowState.Minimized;

                    frmCreate.ShowDialog(this);

                    if (frmCreate.HasCreated)
                    {
                        _dentalManager.InsertPatientInformation(frmCreate.PatientInfo);
                    }

                    this.ShowSearchResultDialog(false);
                }

                this.Cursor = Cursors.Arrow;
            }
            catch (Exception ex)
            {
                DentalLib.ProcStatic.ShowErrorDialog(ex.Message, "Error Loading Create Patient Module");
            }
        } //---------------------------------

        //event is raised when the link update is clicked
        private void _frmSearchLinkClickedUpdate(string id)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                using (DentalLib.PatientInformationUpdate frmUpdate = new DentalLib.PatientInformationUpdate(_userInfo, id))
                {
                    _frmSearch.WindowState = FormWindowState.Minimized;

                    frmUpdate.ShowDialog(this);

                    if (frmUpdate.HasUpdated)
                    {
                        _dentalManager.UpdatePatientInformation(frmUpdate.PatientInfo);                        
                    }

                    this.ShowSearchResultDialog(false);
                }

                this.Cursor = Cursors.Arrow;
            }
            catch (Exception ex)
            {
                DentalLib.ProcStatic.ShowErrorDialog(ex.Message, "Error Loading Update Patient Module");
            }
        } //---------------------------------------
        //####################################END CLASS PatientSearchList EVENTS################################################

        //#####################################CONTROLMANAGER ctlMain EVENTS###############################################
        //event is raised when the sales button is clicked
        private void ctlMainClickSalesReport()
        {
            Boolean appliedOptions = false;
            DateTime dateFrom = DateTime.Parse(_dentalManager.ServerDateTime);
            DateTime dateTo = DateTime.Parse(_dentalManager.ServerDateTime);
            DentalLib.RegistrationLogic regManager = new DentalLib.RegistrationLogic(_userInfo);
            CheckedListBox procedureList = new CheckedListBox();

            this.Cursor = Cursors.WaitCursor;

            using (DentalLib.ReportOptionsSalesReport frmOptions = new DentalLib.ReportOptionsSalesReport(_userInfo,
                dateFrom, dateTo, regManager))
            {
                frmOptions.ShowDialog(this);

                if (frmOptions.ApplyOptions)
                {
                    appliedOptions = frmOptions.ApplyOptions;
                    dateFrom = frmOptions.DateFrom;
                    dateTo = frmOptions.DateTo;
                    procedureList = frmOptions.ProcedureCheckedListBox;
                }
            }

            this.Cursor = Cursors.Arrow;

            if (appliedOptions)
            {
                this.Cursor = Cursors.WaitCursor;

                using (DentalLib.ReportsSalesReport frmReport = new DentalLib.ReportsSalesReport(_userInfo, dateFrom, dateTo, regManager))
                {
                    frmReport.ProcedureCheckedListBox = procedureList;
                    frmReport.ShowDialog(this);
                }

                this.Cursor = Cursors.Arrow;
            }

        } //------------------------------

        //event is raised when the accounts receivable button is clicked
        private void ctlMainClickAccountsReceivable()
        {
            this.Cursor = Cursors.WaitCursor;

            using (DentalLib.ReportsAccountsReceivable frmReport = new DentalLib.ReportsAccountsReceivable(_userInfo))
            {
                frmReport.ShowDialog(this);
            }

            this.Cursor = Cursors.Arrow;
        } //-------------------------------------

        //event is raised when the cash report button is clicked
        private void ctlMainClickCashReport()
        {
            Boolean appliedOptions = false;
            DateTime dateFrom = DateTime.Parse(_dentalManager.ServerDateTime);
            DateTime dateTo = DateTime.Parse(_dentalManager.ServerDateTime);

            this.Cursor = Cursors.WaitCursor;

            using (DentalLib.ReportOptionsCashReport frmOptions = new DentalLib.ReportOptionsCashReport(
                dateFrom, dateTo))
            {
                frmOptions.ShowDialog(this);

                if (frmOptions.ApplyOptions)
                {
                    appliedOptions = frmOptions.ApplyOptions;
                    dateFrom = frmOptions.DateFrom;
                    dateTo = frmOptions.DateTo;
                } 
            }

            this.Cursor = Cursors.Arrow;
            
            if (appliedOptions)
            {
                this.Cursor = Cursors.WaitCursor;

                using (DentalLib.ReportsCashReport frmReport = new DentalLib.ReportsCashReport(_userInfo, dateFrom, dateTo))
                {
                    frmReport.ShowDialog(this);
                }

                this.Cursor = Cursors.Arrow;
            }

        } //-------------------------------

        //event is raised when the close button is clicked
        private void ctlMainClickClose()
        {
            this.Close();
        } //--------------------------------

        //event is raised when the procedures button is clicked
        private void ctlMainClickProcedures()
        {
            try
            {
                using (DentalLib.ProcedureSearchOnTextboxList frmProcedure = new DentalLib.ProcedureSearchOnTextboxList(_userInfo, true))
                {
                    frmProcedure.ShowDialog(this);
                }
            }
            catch (Exception ex)
            {
                DentalLib.ProcStatic.ShowErrorDialog(ex.Message, "Procedure Maintenance Error");
            }
            
        } //--------------------------------

        //event is raised when the user button is clicked
        private void ctlMainClickUsers()
        {
            try
            {
                using (DentalLib.UserSearchOnTextboxList frmUsers = new DentalLib.UserSearchOnTextboxList(_userInfo))
                {
                    frmUsers.ShowDialog(this);
                }
            }
            catch (Exception ex)
            {
                DentalLib.ProcStatic.ShowErrorDialog(ex.Message, "User Maintenance Error");
            }
        } //------------------------------------

        //event is raised when the key is up on the search textbox
        private void ctlMainOnTextboxKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                _frmSearch.SelectFirstRowInDataGridView();
            }
            else if (String.IsNullOrEmpty(DentalLib.ProcStatic.TrimStartEndString(ctlMain.GetSearchString)))
            {
                _frmSearch.WindowState = FormWindowState.Minimized;

                _frmSearch.Hide();

                this.ctlMain.SetFocusOnSearchTextBox();
            }
        } //------------------------

        //event is raised when the enter key is pressed
        private void ctlMainOnPressEnter(object sender, KeyEventArgs e)
        {
            this.ShowSearchResultDialog(true);
        } //---------------------------------
        //##################################END CONTROLMANAGER ctlMain EVENTS##############################################

        //#####################################TIMER tmrMain EVENTS#########################################################
        //event is raised when the timer ticks
        private void tmrMainTick(object sender, EventArgs e)
        {
            _serverDateTime = _serverDateTime.AddSeconds(1);

            tslTime.Text = _serverDateTime.ToLongDateString() + " " + _serverDateTime.ToLongTimeString();
            
        } //---------------------------
        //####################################END TIMER tmrMain EVENTS######################################################
        #endregion

        #region Programmer-Defined Void Procedures
        
        //this procedure shows the search result
        private void ShowSearchResultDialog(Boolean isNewSearch)
        {
            String strSearch = DentalLib.ProcStatic.TrimStartEndString(ctlMain.GetSearchString);

            if (!String.IsNullOrEmpty(strSearch))
            {
                this.Cursor = Cursors.WaitCursor;

                _frmSearch.DataSource = _dentalManager.GetSearchedPatientInformation(_userInfo, strSearch, isNewSearch);

                this.Cursor = Cursors.Arrow;

                _frmSearch.WindowState = FormWindowState.Normal;
            }
            else
            {
                _frmSearch.WindowState = FormWindowState.Minimized;

            }

            this.ctlMain.SetFocusOnSearchTextBox();

        } //--------------------------

        #endregion

    }
}
