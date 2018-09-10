using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace DentalLib
{
    partial class PatientChargesSummary
    {
        #region Class Member Declarations
        private CommonExchange.SysAccess _userInfo;
        private CommonExchange.Patient _patientInfo;
        private CommonExchange.Registration _regInfo;
        private CommonExchange.PaymentSummary _paymentSummary;
        private ChargesLogic _chargesManager;
        private Boolean _hasErrors = false;

        private Int64 _detailsId;
        private String _receiptNo;
        private Int32 _primaryIndex = 0;
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
        public PatientChargesSummary(CommonExchange.SysAccess userInfo, CommonExchange.Patient patientInfo, CommonExchange.Registration regInfo)
        {
            this.InitializeComponent();

            _userInfo = userInfo;
            _patientInfo = patientInfo;
            _regInfo = regInfo;

            this.Load += new EventHandler(ClassLoad);
            this.FormClosed += new FormClosedEventHandler(ClassClosed);
            this.dgvProcedures.MouseDown += new MouseEventHandler(dgvProceduresMouseDown);
            this.dgvProcedures.DoubleClick += new EventHandler(dgvProceduresDoubleClick);
            this.dgvProcedures.KeyPress += new KeyPressEventHandler(dgvProceduresKeyPress);
            this.dgvProcedures.KeyDown += new KeyEventHandler(dgvProceduresKeyDown);
            this.dgvProcedures.DataSourceChanged += new EventHandler(dgvProceduresDataSourceChanged);
            this.dgvProcedures.SelectionChanged += new EventHandler(dgvProceduresSelectionChanged);
            this.dgvPayments.MouseDown += new MouseEventHandler(dgvPaymentsMouseDown);
            this.dgvPayments.DoubleClick += new EventHandler(dgvPaymentsDoubleClick);
            this.dgvPayments.KeyPress += new KeyPressEventHandler(dgvPaymentsKeyPress);
            this.dgvPayments.KeyDown += new KeyEventHandler(dgvPaymentsKeyDown);
            this.dgvPayments.DataSourceChanged += new EventHandler(dgvPaymentsDataSourceChanged);
            this.dgvPayments.SelectionChanged += new EventHandler(dgvPaymentsSelectionChanged);
            this.btnClose.Click += new EventHandler(btnCloseClick);
            this.lnkCreateCharges.LinkClicked += new LinkLabelLinkClickedEventHandler(lnkCreateChargesLinkClicked);
            this.lnkCreatePayment.LinkClicked += new LinkLabelLinkClickedEventHandler(lnkCreatePaymentLinkClicked);
            this.btnReceipt.Click += new EventHandler(btnReceiptClick);
            this.lnkPrescription.LinkClicked += new LinkLabelLinkClickedEventHandler(lnkPrescriptionLinkClicked);
        }

        

        #endregion

        #region Class Event Void Procedures
        //#########################################CLASS PatientChargesSummary EVENTS######################################################
        //event is raised when the class is loaded
        private void ClassLoad(object sender, EventArgs e)
        {
            if (!DatabaseLib.ProcStatic.IsSystemAccessAdmin(_userInfo) && !DatabaseLib.ProcStatic.IsSystemAccessDentalUser(_userInfo))
            {
                DentalLib.ProcStatic.ShowErrorDialog("You are not allowed to access this module.",
                    "Patient Charges Summary");

                _hasErrors = true;

                this.Close();
            }

            _chargesManager = new ChargesLogic(_userInfo);

            this.lblSysId.Text = _patientInfo.PatientSystemId;
            this.lblName.Text = _patientInfo.LastName.ToUpper() + ", " + _patientInfo.FirstName + " " + _patientInfo.MiddleName;

            if (!String.IsNullOrEmpty(_patientInfo.ImagePath))
            {
                this.pbxPatient.Image = Image.FromFile(_patientInfo.ImagePath);
            }

            this.lblRegistrationId.Text = _regInfo.RegistrationSystemId;
            this.lblRegistrationDate.Text = DateTime.Parse(_regInfo.RegistrationDate).ToLongDateString();

            this.SetProceduresTakenDataGridViewSource(true);
            this.SetPaymentsMadeDataGridViewSource(true);

            DentalLib.ProcStatic.SetDataGridViewColumns(this.dgvProcedures, false);
            DentalLib.ProcStatic.SetDataGridViewColumns(this.dgvPayments, false);

            this.SetPaymentSummary();
            

        } //---------------------------------

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
        } //-----------------------------------

        //########################################END CLASS PatientChargesSummary EVENTS###################################################

        //####################################################DATAGRIDVIEW dgvProcedures EVENTS####################################################
        //event is raised when the mouse is down
        private void dgvProceduresMouseDown(object sender, MouseEventArgs e)
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
                        _detailsId = 0;
                        break;
                }

                if (rowIndex != -1)
                {
                    _detailsId = Int64.Parse(dgvBase[_primaryIndex, rowIndex].Value.ToString());                    
                }
            }

        } //-----------------------------

        //event is raised when the mouse is double clicked
        private void dgvProceduresDoubleClick(object sender, EventArgs e)
        {
            this.ShowPatientChargesUpdateDialog();

        } //---------------------------------

        //event is raised when the key is pressed        
        private void dgvProceduresKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                DataGridView dgvBase = (DataGridView)sender;

                if (dgvBase.Rows.GetRowCount(DataGridViewElementStates.Selected) > 0)
                {
                    DataGridViewRow row = dgvBase.SelectedRows[0];

                    _detailsId = Int64.Parse(row.Cells[_primaryIndex].Value.ToString());

                    this.ShowPatientChargesUpdateDialog();
                }

            }
            else
            {
                e.Handled = true;
            }
        } //-----------------------------------

        //event is raised when the key is up
        private void dgvProceduresKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
            }
        } //--------------------------------------

        //event is raised when the data source is changed
        protected virtual void dgvProceduresDataSourceChanged(object sender, EventArgs e)
        {
            DataGridView dgvBase = (DataGridView)sender;
            Int32 result = dgvBase.Rows.Count;

            if (result == 1)
            {
                _detailsId = Int64.Parse(dgvBase[_primaryIndex, 0].Value.ToString());
            }
            else
            {
                _detailsId = 0;
            }

            if (result == 0 || result == 1)
            {
                this.lblResultCharges.Text = result.ToString() + " Record";
            }
            else
            {
                this.lblResultCharges.Text = result.ToString() + " Records";
            }           

        } //--------------------------------

        //event is raised when the selection is changed
        protected virtual void dgvProceduresSelectionChanged(object sender, EventArgs e)
        {
            DataGridView dgvBase = (DataGridView)sender;

            if (dgvBase.Rows.GetRowCount(DataGridViewElementStates.Selected) > 0)
            {
                DataGridViewRow row = dgvBase.SelectedRows[0];

                _detailsId = Int64.Parse(row.Cells[_primaryIndex].Value.ToString());
            }
        } //------------------------------------

        //################################################END DATAGRIDVIEW dgvProcedures EVENTS####################################################

        //####################################################DATAGRIDVIEW dgvPayments EVENTS####################################################
        //event is raised when the mouse is down
        private void dgvPaymentsMouseDown(object sender, MouseEventArgs e)
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
                        _receiptNo = "";
                        break;
                }

                if (rowIndex != -1)
                {
                    _receiptNo = dgvBase[_primaryIndex, rowIndex].Value.ToString();                    
                }
            }

        } //-----------------------------

        //event is raised when the mouse is double clicked
        private void dgvPaymentsDoubleClick(object sender, EventArgs e)
        {
            this.ShowPaymentDetailsUpdateDialog();

        } //---------------------------------

        //event is raised when the key is pressed        
        private void dgvPaymentsKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                DataGridView dgvBase = (DataGridView)sender;

                if (dgvBase.Rows.GetRowCount(DataGridViewElementStates.Selected) > 0)
                {
                    DataGridViewRow row = dgvBase.SelectedRows[0];

                    _receiptNo = row.Cells[_primaryIndex].Value.ToString();

                    this.ShowPaymentDetailsUpdateDialog();
                }

            }
            else
            {
                e.Handled = true;
            }
        } //-----------------------------------

        //event is raised when the key is up
        private void dgvPaymentsKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
            }
        } //--------------------------------------

        //event is raised when the data source is changed
        protected virtual void dgvPaymentsDataSourceChanged(object sender, EventArgs e)
        {
            DataGridView dgvBase = (DataGridView)sender;
            Int32 result = dgvBase.Rows.Count;

            if (result == 1)
            {
                _receiptNo = dgvBase[_primaryIndex, 0].Value.ToString();
            }
            else
            {
                _receiptNo = "";
            }

            if (result == 0 || result == 1)
            {
                this.lblResultPayment.Text = result.ToString() + " Record";
            }
            else
            {
                this.lblResultPayment.Text = result.ToString() + " Records";
            }
            

        } //--------------------------------

        //event is raised when the selection is changed
        protected virtual void dgvPaymentsSelectionChanged(object sender, EventArgs e)
        {
            DataGridView dgvBase = (DataGridView)sender;

            if (dgvBase.Rows.GetRowCount(DataGridViewElementStates.Selected) > 0)
            {
                DataGridViewRow row = dgvBase.SelectedRows[0];

                _receiptNo = row.Cells[_primaryIndex].Value.ToString();
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

        //#############################################LINKBUTTON lnkCreateCharges EVENTS##################################################
        //event is raised when the link button is clicked
        private void lnkCreateChargesLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                using (PatientChargesCreate frmCreate = new PatientChargesCreate(_userInfo, _patientInfo, _regInfo, _chargesManager))
                {
                    frmCreate.ShowDialog(this);

                    if (frmCreate.HasCreated)
                    {
                        _hasUpdated = frmCreate.HasCreated;

                        this.SetProceduresTakenDataGridViewSource(true);
                        this.SetPaymentSummary();
                    }
                }

            }
            catch (Exception ex)
            {
                DentalLib.ProcStatic.ShowErrorDialog(ex.Message, "Error Loading Patient Charges Create");
            }            

        } //-------------------------
        //############################################END LINKBUTTON lnkCreateCharges EVENTS###############################################

        //############################################LINKBUTTON lnkCreatePayments EVENTS###################################################
        //event is raised when the link button is clicked
        private void lnkCreatePaymentLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                using (PaymentDetailsCreate frmCreate = new PaymentDetailsCreate(_userInfo, _patientInfo, _regInfo, _chargesManager))
                {
                    frmCreate.ShowDialog(this);

                    if (frmCreate.HasCreated)
                    {
                        _hasUpdated = frmCreate.HasCreated;

                        this.SetPaymentsMadeDataGridViewSource(false);
                        this.SetPaymentSummary();
                    }
                }

            }
            catch (Exception ex)
            {
                DentalLib.ProcStatic.ShowErrorDialog(ex.Message, "Error Loading Payment Details Create");
            }            
        } //----------------------------
        //##########################################END LINKBUTTON lnkCreatePayments EVENTS#################################################

        //############################################BUTTON btnReceipt EVENTS#############################################################
        //event is raised when the button is clicked
        private void btnReceiptClick(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            _chargesManager.PrintPatientChargesReceipt(_userInfo, _patientInfo, _regInfo);

            this.Cursor = Cursors.Arrow;
        } //----------------------------------
        //#########################################END BUTTON btnReceipt EVENTS############################################################

        //###########################################LINKBUTTON lnkPrescription EVENTS#####################################################
        //event is raised when the link is clicked
        private void lnkPrescriptionLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            using (DentalLib.MedicalPrescription frmPrescription = new DentalLib.MedicalPrescription(_userInfo, _regInfo))
            {
                frmPrescription.ShowDialog(this);

                if (frmPrescription.HasSaved)
                {
                    _regInfo.MedicalPrescription = frmPrescription.PatientMedicalPrescription;
                }
            }

            this.Cursor = Cursors.Arrow;
        } //---------------------------------
        //##########################################END LINKBUTTON lnkPrescription EVENTS###################################################

        #endregion

        #region Programmer-Defined Void Procedures
        //this procedure sets the payment summary
        private void SetPaymentSummary()
        {
            _paymentSummary = _chargesManager.GetPaymentSummary();

            this.lblAmountPayable.Text = _paymentSummary.AmountPayable.ToString("N");
            this.lblAmountPaid.Text = _paymentSummary.AmountPaid.ToString("N");
            this.lblDiscount.Text = _paymentSummary.Discount.ToString("N");
            this.lblBalance.Text = _paymentSummary.Balance.ToString("N");

        } //--------------------------

        //this procedure sets the datagridview datasource
        private void SetProceduresTakenDataGridViewSource(Boolean isNewQuery)
        {
            this.dgvProcedures.DataSource = _chargesManager.SelectByRegistrationIDRegistrationDetails(_userInfo, _regInfo.RegistrationSystemId,
                isNewQuery);
        } //--------------------------

        //this procedure sets the datagridview datasource
        private void SetPaymentsMadeDataGridViewSource(Boolean isNewQuery)
        {
            this.dgvPayments.DataSource = _chargesManager.SelectByRegistrationIDPaymentDetails(_userInfo, _regInfo.RegistrationSystemId,
                isNewQuery);
        } //--------------------------

        //this procedure shows the patient charges update
        private void ShowPatientChargesUpdateDialog()
        {
            try
            {
                if (_detailsId > 0)
                {
                    using (PatientChargesUpdate frmUpdate = new PatientChargesUpdate(_userInfo, _patientInfo, _regInfo,
                    _chargesManager.GetRegistrationDetails(_detailsId), _chargesManager))
                    {
                        frmUpdate.ShowDialog(this);

                        if (frmUpdate.HasUpdated || frmUpdate.HasDeleted)
                        {
                            _hasUpdated = frmUpdate.HasUpdated;
                            _hasDeleted = frmUpdate.HasDeleted;

                            this.SetProceduresTakenDataGridViewSource(false);
                            this.SetPaymentsMadeDataGridViewSource(false);
                            this.SetPaymentSummary();
                        }
                    }
                }               

            }
            catch (Exception ex)
            {
                DentalLib.ProcStatic.ShowErrorDialog(ex.Message, "Error Loading Patient Charges Update");
            }            
        }

        //this procedure shows the payment details update
        private void ShowPaymentDetailsUpdateDialog()
        {
            try
            {
                if (!String.IsNullOrEmpty(_receiptNo))
                {
                    using (PaymentDetailsUpdate frmUpdate = new PaymentDetailsUpdate(_userInfo, _patientInfo, _regInfo,
                        _chargesManager.GetPaymentDetails(_receiptNo), _chargesManager))
                    {
                        frmUpdate.ShowDialog(this);

                        if (frmUpdate.HasUpdated || frmUpdate.HasDeleted)
                        {
                            _hasUpdated = frmUpdate.HasUpdated;
                            _hasDeleted = frmUpdate.HasDeleted;

                            this.SetProceduresTakenDataGridViewSource(false);
                            this.SetPaymentsMadeDataGridViewSource(false);
                            this.SetPaymentSummary();

                        }
                    }
                }

            }
            catch (Exception ex)
            {
                DentalLib.ProcStatic.ShowErrorDialog(ex.Message, "Error Loading Payment Details Update");
            }
        }
        #endregion
    }
}
