using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace DentalLib
{
    partial class PaymentDetails
    {
        #region Class Member Declarations
        protected CommonExchange.SysAccess _userInfo;
        protected CommonExchange.Patient _patientInfo;
        protected CommonExchange.PaymentDetails _paymentInfo;
        protected CommonExchange.Registration _regInfo;
        protected CommonExchange.PaymentSummary _summary;
        protected ChargesLogic _chargesManager;
        protected Boolean _hasErrors = false;       

        private ErrorProvider _errProvider;        

        #endregion

        #region Class Constructor
        public PaymentDetails()
        {
            this.InitializeComponent();
        }

        public PaymentDetails(CommonExchange.SysAccess userInfo, CommonExchange.Patient patientInfo, CommonExchange.Registration regInfo,
            ChargesLogic chargesManager)
        {
            this.InitializeComponent();

            _userInfo = userInfo;
            _patientInfo = patientInfo;
            _regInfo = regInfo;
            _chargesManager = chargesManager;

            _errProvider = new ErrorProvider();

            this.Load += new EventHandler(ClassLoad);
            this.cboPaymentType.SelectedIndexChanged += new EventHandler(cboPaymentTypeSelectedIndexChanged);
            this.txtAmountPaid.KeyPress += new KeyPressEventHandler(txtAmountPaidKeyPress);
            this.txtAmountPaid.Validating += new System.ComponentModel.CancelEventHandler(txtAmountPaidValidating);
            this.txtAmountPaid.Validated += new EventHandler(txtAmountPaidValidated);
            this.txtAmountPaid.KeyUp += new KeyEventHandler(txtAmountPaidKeyUp);
            this.txtDiscount.KeyPress += new KeyPressEventHandler(txtDiscountKeyPress);
            this.txtDiscount.Validating += new System.ComponentModel.CancelEventHandler(txtDiscountValidating);
            this.txtDiscount.Validated += new EventHandler(txtDiscountValidated);
            this.txtDiscount.KeyUp += new KeyEventHandler(txtDiscountKeyUp);
            this.lnkChange.LinkClicked += new LinkLabelLinkClickedEventHandler(lnkChangeLinkClicked);
            this.txtBankName.Validated += new EventHandler(txtBankNameValidated);
            this.txtCheckNo.Validated += new EventHandler(txtCheckNoValidated);
            this.txtCardNo.Validated += new EventHandler(txtCardNoValidated);
            this.txtCardType.Validated += new EventHandler(txtCardTypeValidated);
            this.txtExpiringDate.Validated += new EventHandler(txtExpiringDateValidated);
            
        }       
        
        #endregion

        #region Class Event Void Procedures
        //############################################CLASS PatientCharges EVENTS#######################################################
        //event is raised when the class is loaded
        protected virtual void ClassLoad(object sender, EventArgs e)
        {
            if (!DatabaseLib.ProcStatic.IsSystemAccessAdmin(_userInfo) && !DatabaseLib.ProcStatic.IsSystemAccessDentalUser(_userInfo))
            {
                DentalLib.ProcStatic.ShowErrorDialog("You are not allowed to access this module.",
                    "Payment Details");

                _hasErrors = true;

                this.Close();
            }
            else if (!DatabaseLib.ProcStatic.IsSystemAccessAdmin(_userInfo))
            {
                this.lnkChange.Visible = false;
            }

            _paymentInfo = new CommonExchange.PaymentDetails();
            _paymentInfo.DatePaid = _chargesManager.ServerDateTime;
            _paymentInfo.RegistrationSystemId = _regInfo.RegistrationSystemId;

            this.lblDate.Text = DateTime.Parse(_paymentInfo.DatePaid).ToLongDateString();

            this.InitializePaymentTypeCombo();

            _summary = _chargesManager.GetPaymentSummary();

            this.lblBalancetoDate.Text = _summary.Balance.ToString("N");
            this.lblShouldBeBalance.Text = _summary.Balance.ToString("N");


        } //------------------------------------------------------------
        //############################################END CLASS PatientCharges EVENTS#######################################################

        //##############################################LINKBUTTON lnkChange EVENTS################################################################
        //event is raised when the link is clicked
        private void lnkChangeLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            DateTime bDate = DateTime.Parse(_paymentInfo.DatePaid);

            using (DatePicker frmPicker = new DatePicker(bDate))
            {
                frmPicker.ShowDialog(this);

                if (frmPicker.HasSelectedDate)
                {
                    if (DateTime.TryParse(frmPicker.GetSelectedDate.ToShortDateString() + " " +
                        DateTime.Parse(_chargesManager.ServerDateTime).ToLongTimeString(), out bDate))
                    {
                        _paymentInfo.DatePaid = bDate.ToString();
                    }

                    this.lblDate.Text = DateTime.Parse(_paymentInfo.DatePaid).ToLongDateString();
                }
            }

            this.Cursor = Cursors.Arrow;

        } //-------------------------------
        //############################################END LINKBUTTON lnkChange EVENTS##############################################################

        //##############################################COMBOBOX cboPaymentType EVENTS#######################################################
        //event is raised when the selected index is changed
        private void cboPaymentTypeSelectedIndexChanged(object sender, EventArgs e)
        {
            _paymentInfo.PaymentType = (Byte)cboPaymentType.SelectedIndex;

            this.SetTypeOfPaymentCombo();
            
        } //------------------------------------
        //###########################################END COMBOBOX cboPaymentType EVENTS######################################################

        //############################################TEXTBOX txtAmountPaid EVENTS###########################################################
        //event is raised when the is validated
        private void txtAmountPaidValidated(object sender, EventArgs e)
        {
            Decimal result = 0;

            if (Decimal.TryParse(this.txtAmountPaid.Text, out result))
            {
                _paymentInfo.Amount = result;
            }
        } //--------------------------

        //event is raised when the control is validating
        private void txtAmountPaidValidating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DentalLib.ProcStatic.TextBoxValidateAmount((TextBox)sender);   
        } //----------------------

        //event is raised when the key is pressed
        private void txtAmountPaidKeyPress(object sender, KeyPressEventArgs e)
        {
            DentalLib.ProcStatic.TextBoxAmountOnly((TextBox)sender, e);
        } //------------------------

        //event is raised when the key is up
        private void txtAmountPaidKeyUp(object sender, KeyEventArgs e)
        {
            this.SetPaymentSummary();
        } //------------------------------
        //###########################################END TEXTBOX txtAmountPaid EVENTS#########################################################

        //###############################################TEXTBOX txtDiscount EVENTS#############################################################
        //event is raised when the key is up
        private void txtDiscountKeyUp(object sender, KeyEventArgs e)
        {
            this.SetPaymentSummary();
        } //---------------------------------------

        //event is raised when the control is validated
        private void txtDiscountValidated(object sender, EventArgs e)
        {
            Decimal result = 0;

            if (Decimal.TryParse(this.txtDiscount.Text, out result))
            {
                _paymentInfo.Discount = result;
            }
        } //--------------------------

        //event is raised when the control is validating
        private void txtDiscountValidating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DentalLib.ProcStatic.TextBoxValidateAmount((TextBox)sender);   
        } //---------------------------

        //event is raised when the key is pressed
        private void txtDiscountKeyPress(object sender, KeyPressEventArgs e)
        {
            DentalLib.ProcStatic.TextBoxAmountOnly((TextBox)sender, e);
        } //---------------------------------
        //#############################################END TEXTBOX txtDiscount EVENTS###########################################################

        //######################################TEXTBOX txtExpiringDate EVENTS#################################################
        private void txtExpiringDateValidated(object sender, EventArgs e)
        {
            _paymentInfo.CardExpire = DentalLib.ProcStatic.TrimStartEndString(this.txtExpiringDate.Text);
        }
        //#######################################END TEXTBOX txtExpiringDate EVENTS###############################################

        //######################################TEXTBOX txtCardType EVENTS#####################################################
        private void txtCardTypeValidated(object sender, EventArgs e)
        {
            _paymentInfo.CardType = DentalLib.ProcStatic.TrimStartEndString(this.txtCardType.Text);
        }
        //#########################################END TEXTBOX txtCardType EVENTS#################################################

        //#############################################TEXTBOX txtCardNo EVENTS####################################################
        private void txtCardNoValidated(object sender, EventArgs e)
        {
            _paymentInfo.CardNumber = DentalLib.ProcStatic.TrimStartEndString(this.txtCardNo.Text);
        }
        //########################################END TEXTBOX txtCardNo EVENTS####################################################

        //########################################TEXTBOX txtCheckNo EVENTS#########################################################
        private void txtCheckNoValidated(object sender, EventArgs e)
        {
            _paymentInfo.CheckNo = DentalLib.ProcStatic.TrimStartEndString(this.txtCheckNo.Text);
        }
        //######################################END TEXTBOX txtCheckNo EVENTS#######################################################

        //########################################TEXTBOX txtBankName EVENTS###########################################################
        private void txtBankNameValidated(object sender, EventArgs e)
        {
            _paymentInfo.BankName = DentalLib.ProcStatic.TrimStartEndString(this.txtBankName.Text);
        } //-----------------------------------
        //####################################END TEXTBOX txtBankName EVENTS############################################################
        #endregion

        #region Programmer-Defined Void Procedures
        //this procedure initializes the payment combo box
        protected void InitializePaymentTypeCombo()
        {
            this.cboPaymentType.Items.Add("Cash");
            this.cboPaymentType.Items.Add("Check");
            this.cboPaymentType.Items.Add("Credit Card");

            this.cboPaymentType.SelectedIndex = 0;
        } //----------------------------------

        //this procedure sets the payment summary
        protected virtual void SetPaymentSummary()
        {
            Decimal amountPaid = 0;
            Decimal discount = 0;

            Decimal.TryParse(this.txtAmountPaid.Text, out amountPaid);
            Decimal.TryParse(this.txtDiscount.Text, out discount);

            this.lblShouldBeBalance.Text = (_summary.AmountPayable - 
                (_chargesManager.GetTotalPaymentsMade(_paymentInfo.ReceiptNo) + amountPaid + discount)).ToString("N");


        } //-----------------------------

        //this sets the type of payment controls
        private void SetTypeOfPaymentCombo()
        {
            Int32 index = cboPaymentType.SelectedIndex;

            if (index == (Int32)CommonExchange.PaymentType.Cash)
            {
                this.txtBankName.Enabled = false;
                this.txtCheckNo.Enabled = false;
                this.txtCardNo.Enabled = false;
                this.txtCardType.Enabled = false;
                this.txtExpiringDate.Enabled = false;

                this.txtBankName.Clear();
                this.txtCheckNo.Clear();
                this.txtCardNo.Clear();
                this.txtCardType.Clear();
                this.txtExpiringDate.Clear();
            }
            else if (index == (Int32)CommonExchange.PaymentType.Check)
            {
                this.txtBankName.Enabled = true;
                this.txtCheckNo.Enabled = true;
                this.txtCardNo.Enabled = false;
                this.txtCardType.Enabled = false;
                this.txtExpiringDate.Enabled = false;

                this.txtCardNo.Clear();
                this.txtCardType.Clear();
                this.txtExpiringDate.Clear();
            }
            else if (index == (Int32)CommonExchange.PaymentType.CreditCard)
            {
                this.txtBankName.Enabled = true;
                this.txtCheckNo.Enabled = false;
                this.txtCardNo.Enabled = true;
                this.txtCardType.Enabled = true;
                this.txtExpiringDate.Enabled = true;

                this.txtCheckNo.Clear();
            }

            _paymentInfo.BankName = DentalLib.ProcStatic.TrimStartEndString(this.txtBankName.Text);
            _paymentInfo.CheckNo = DentalLib.ProcStatic.TrimStartEndString(this.txtCheckNo.Text);
            _paymentInfo.CardNumber = DentalLib.ProcStatic.TrimStartEndString(this.txtCardNo.Text);
            _paymentInfo.CardType = DentalLib.ProcStatic.TrimStartEndString(this.txtCardType.Text);
            _paymentInfo.CardExpire = DentalLib.ProcStatic.TrimStartEndString(this.txtExpiringDate.Text);
        } //------------------------------------

        #endregion

        #region Programmer-Defined Function Procedures
        //this function determines if the controls are validated
        protected Boolean ValidateControls()
        {
            Boolean isValid = true;

            _errProvider.SetError(this.lblAmountPaid, "");
            _errProvider.SetError(this.lblDiscount, "");

            if (_paymentInfo.Amount <= 0)
            {
                _errProvider.SetIconAlignment(this.lblAmountPaid, ErrorIconAlignment.MiddleLeft);
                _errProvider.SetError(this.lblAmountPaid, "The amount paid must be greater than zero.");
                isValid = false;
            }
            else if ((_paymentInfo.Amount + _paymentInfo.Discount) > 
                (_summary.AmountPayable - _chargesManager.GetTotalPaymentsMade(_paymentInfo.ReceiptNo)))
            {
                _errProvider.SetIconAlignment(this.lblAmountPaid, ErrorIconAlignment.MiddleLeft);
                _errProvider.SetIconAlignment(this.lblDiscount, ErrorIconAlignment.MiddleLeft);
                _errProvider.SetError(this.lblAmountPaid, "The amount paid plus the discount must not exceed the amount payable.");
                _errProvider.SetError(this.lblDiscount, "The amount paid plus the discount must not exceed the amount payable.");
                isValid = false;

            }

            return isValid;
        } //------------------------
        #endregion
    }
}
