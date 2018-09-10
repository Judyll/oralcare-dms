using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace DentalLib
{
    partial class PatientRegistrationList
    {
        #region Class Member Declarations
        private CommonExchange.SysAccess _userInfo;
        private CommonExchange.Patient _patientInfo;
        private RegistrationLogic _regManager;

        private String _patientId;
        private String _primaryId;
        private Int32 _primaryIndex = 0;
        #endregion

        #region Class Constructor
        public PatientRegistrationList(CommonExchange.SysAccess userInfo, String patientId)
        {
            this.InitializeComponent();

            _userInfo = userInfo;
            _patientId = patientId;

            this.Load += new EventHandler(ClassLoad);
            this.FormClosed += new FormClosedEventHandler(ClassClosed);
            this.btnClose.Click += new EventHandler(btnCloseClick);
            this.dgvList.MouseDown += new MouseEventHandler(dgvListMouseDown);
            this.dgvList.DoubleClick += new EventHandler(dgvListDoubleClick);
            this.dgvList.KeyPress += new KeyPressEventHandler(dgvListKeyPress);
            this.dgvList.KeyDown += new KeyEventHandler(dgvListKeyDown);
            this.dgvList.DataSourceChanged += new EventHandler(dgvListDataSourceChanged);
            this.dgvList.SelectionChanged += new EventHandler(dgvListSelectionChanged);
            this.lnkCreate.LinkClicked += new LinkLabelLinkClickedEventHandler(lnkCreateLinkClicked);
            this.lnkUpdate.LinkClicked += new LinkLabelLinkClickedEventHandler(lnkUpdateLinkClicked);
            this.lnkPrint.LinkClicked += new LinkLabelLinkClickedEventHandler(lnkPrintLinkClicked);
            this.optRegistration.CheckedChanged += new EventHandler(optRegistrationCheckedChanged);
        }                
        
        #endregion

        #region Class Event Declarations
        //##########################################CLASS PatientRegistrationList EVENTS#################################################
        //event is raised when the class is loaded
        private void ClassLoad(object sender, EventArgs e)
        {
            try
            {
                _regManager = new RegistrationLogic(_userInfo);

                _regManager.DeleteImageDirectory(Application.StartupPath);

                _patientInfo = _regManager.SelectByPatientSystemIdPatientInformation(_userInfo, _patientId, Application.StartupPath);

                this.lblSysId.Text = _patientInfo.PatientSystemId;
                this.lblName.Text = _patientInfo.LastName.ToUpper() + ", " + _patientInfo.FirstName + " " + _patientInfo.MiddleName;

                if (!String.IsNullOrEmpty(_patientInfo.ImagePath))
                {
                    this.pbxPatient.Image = Image.FromFile(_patientInfo.ImagePath);
                }

                this.optRegistration.Checked = true;               

            }
            catch (Exception ex)
            {
                DentalLib.ProcStatic.ShowErrorDialog(ex.Message, "Error Loading Patient Registration List");
                this.Close();
            }            

        } //-------------------------------

        //event is raised when the class is closed
        private void ClassClosed(object sender, FormClosedEventArgs e)
        {
            if (this.pbxPatient.Image != null)
            {
                pbxPatient.Image.Dispose();
                pbxPatient.Image = null;

                pbxPatient.Dispose();
                pbxPatient = null;
            }
        } //--------------------------------
        //##########################################END CLASS PatientRegistrationList EVENTS#################################################

        //####################################################DATAGRIDVIEW dgvList EVENTS####################################################
        //event is raised when the mouse is down
        private void dgvListMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                DataGridView dgvBase = (DataGridView)sender;

                DataGridView.HitTestInfo hitTest = dgvBase.HitTest(e.X, e.Y);

                Int32 rowIndex = -1;

                switch (hitTest.Type)
                {
                    case DataGridViewHitTestType.Cell:
                        rowIndex = hitTest.RowIndex;
                        break;
                    case DataGridViewHitTestType.RowHeader:
                        rowIndex = hitTest.RowIndex;
                        break;
                    default:
                        rowIndex = -1;
                        _primaryId = "";
                        break;
                }

                if (rowIndex != -1)
                {
                    _primaryId = dgvBase[_primaryIndex, rowIndex].Value.ToString();

                    this.lnkUpdate.Visible = this.optRegistration.Checked;
                }
            }

        } //-----------------------------

        //event is raised when the mouse is double clicked
        private void dgvListDoubleClick(object sender, EventArgs e)
        {
            this.ShowPatientChargesSummary();

        } //---------------------------------

        //event is raised when the key is pressed        
        private void dgvListKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                DataGridView dgvBase = (DataGridView)sender;

                if (dgvBase.Rows.GetRowCount(DataGridViewElementStates.Selected) > 0)
                {
                    DataGridViewRow row = dgvBase.SelectedRows[0];

                    _primaryId = row.Cells[_primaryIndex].Value.ToString();

                    this.ShowPatientChargesSummary();
                }

            }
            else
            {
                e.Handled = true;
            }
        } //-----------------------------------

        //event is raised when the key is up
        private void dgvListKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
            }
        } //--------------------------------------

        //event is raised when the data source is changed
        protected virtual void dgvListDataSourceChanged(object sender, EventArgs e)
        {
            DataGridView dgvBase = (DataGridView)sender;
            Int32 result = dgvBase.Rows.Count;

            if (result == 1)
            {
                _primaryId = dgvBase[_primaryIndex, 0].Value.ToString();
            }
            else
            {
                _primaryId = "";
            }

            if (result == 0 || result == 1)
            {
                this.lblResult.Text = result.ToString() + " Record";
            }
            else
            {
                this.lblResult.Text = result.ToString() + " Records";
            }

            this.lnkUpdate.Visible = false;

        } //--------------------------------

        //event is raised when the selection is changed
        protected virtual void dgvListSelectionChanged(object sender, EventArgs e)
        {
            DataGridView dgvBase = (DataGridView)sender;

            if (dgvBase.Rows.GetRowCount(DataGridViewElementStates.Selected) > 0)
            {
                DataGridViewRow row = dgvBase.SelectedRows[0];

                _primaryId = row.Cells[_primaryIndex].Value.ToString();
            }
        } //------------------------------------

        //################################################END DATAGRIDVIEW dgvList EVENTS####################################################

        //##############################################BUTTON btnClose EVENTS#############################################################
        //event is raised when the button is clicked
        private void btnCloseClick(object sender, EventArgs e)
        {
            this.Close();
        } //-------------------------------
        //###########################################END BUTTON btnClose EVENTS#############################################################

        //#############################################LINKBUTTON lnkCreate EVENTS##########################################################
        //event is raised when the link is clicked
        private void lnkCreateLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                using (PatientRegistrationCreate frmCreate = new PatientRegistrationCreate(_userInfo, _regManager, _patientInfo))
                {
                    frmCreate.ShowDialog(this);

                    if (frmCreate.HasCreated)
                    {
                        this.SetDataGridViewSource(false);
                    }
                }
            }
            catch (Exception ex)
            {
                DentalLib.ProcStatic.ShowErrorDialog(ex.Message, "Error Loading Patient Registration Create Module");
            }
        } //------------------------------------------
        //############################################END LINKBUTTON lnkCreate EVENTS########################################################

        //##############################################LINKBUTTON lnkUpdate EVENTS############################################################
        //event is raised when the link is clicked
        private void lnkUpdateLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                using (PatientRegistrationUpdate frmUpdate = new PatientRegistrationUpdate(_userInfo, _regManager, _patientInfo, _primaryId))
                {
                    frmUpdate.ShowDialog(this);

                    if (frmUpdate.HasUpdated)
                    {
                        this.SetDataGridViewSource(false);
                    }
                }
            }
            catch (Exception ex)
            {
                DentalLib.ProcStatic.ShowErrorDialog(ex.Message, "Error Loading Patient Registration Update Module");
            }
        } //---------------------------------
        //#############################################END LINKBUTTON lnkUpdate EVENTS#########################################################

        //##############################################LINKBUTTON lnkPrint EVENTS#############################################################
        //event is raised when the link is clicked
        private void lnkPrintLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            _regManager.PrintRegistrationList(_userInfo, _patientInfo);

            this.Cursor = Cursors.Arrow;
            
        } //-------------------------------
        //############################################END LINKBUTTON lnkPrint EVENTS###########################################################

        //#############################################RADIOBUTTON optRegistration EVENTS######################################################
        //event is raised when the checked is changed
        private void optRegistrationCheckedChanged(object sender, EventArgs e)
        {
            this.SetDataGridViewSource(true);

            this.lnkCreate.Visible = this.lnkPrint.Visible = this.optRegistration.Checked;
        } //----------------------------------------
        //############################################RADIOBUTTON optRegistration EVENTS#######################################################
        
        #endregion

        #region Programmer-Defined Void Procedures

        //this procedure sets the datagridview datasource
        private void SetDataGridViewSource(Boolean isNewQuery)
        {
            this.Cursor = Cursors.WaitCursor;

            if (this.optRegistration.Checked)
            {
                this.dgvList.DataSource = null;
                this.dgvList.DataSource = _regManager.SelectByPatientIDPatientRegistration(_userInfo, _patientId, isNewQuery);
                
                DentalLib.ProcStatic.SetDataGridViewColumns(this.dgvList, false);

                this.gbxList.Text = "REGISTRATION LIST";
            }
            else
            {
                this.dgvList.DataSource = null;
                this.dgvList.DataSource = _regManager.SelectByProcedureTakenPatientRegistration(_userInfo, _patientId, isNewQuery);

                DentalLib.ProcStatic.SetDataGridViewColumns(this.dgvList, false);

                this.gbxList.Text = "PROCEDURES TAKEN";
            }

            this.Cursor = Cursors.Arrow;
        } //--------------------------

        //this procedure shows the patient charges summary dialog
        private void ShowPatientChargesSummary()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                if (!String.IsNullOrEmpty(_primaryId))
                {
                    using (PatientChargesSummary frmSummary = new PatientChargesSummary(_userInfo, _patientInfo,
                    _regManager.GetPatientRegistrationDetails(_primaryId)))
                    {
                        frmSummary.ShowDialog(this);

                        if (frmSummary.HasUpdated || frmSummary.HasDeleted)
                        {
                            this.SetDataGridViewSource(true);
                        }
                    }
                }

                this.Cursor = Cursors.Arrow;
                
            }
            catch (Exception ex)
            {
                DentalLib.ProcStatic.ShowErrorDialog(ex.Message, "Error Loading Patient Charges Summary Module");
            }
        } //-----------------------
        #endregion
    }
}
