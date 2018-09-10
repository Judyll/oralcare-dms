using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace DentalLib
{
    partial class PaymentDetailsUpdate
    {
        #region Class Member Declarations
        protected CommonExchange.PaymentDetails _paymentInfoTemp;
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
        public PaymentDetailsUpdate(CommonExchange.SysAccess userInfo, CommonExchange.Patient patientInfo, CommonExchange.Registration regInfo,
            CommonExchange.PaymentDetails paymentInfo, ChargesLogic chargesManager)
            : base(userInfo, patientInfo, regInfo, chargesManager)
        {
            this.InitializeComponent();

            _paymentInfo = paymentInfo;
            _paymentInfoTemp = paymentInfo;

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
                    "Payment Details");

                _hasErrors = true;

                this.Close();
            }
            else if (!DatabaseLib.ProcStatic.IsSystemAccessAdmin(_userInfo))
            {
                this.btnUpdate.Visible = this.btnDelete.Visible = this.lnkChange.Visible = false;

                _hasErrors = true;
            }

            this.InitializePaymentTypeCombo();

            this.lblReceiptNo.Text = _paymentInfo.ReceiptNo;
            this.lblDate.Text = DateTime.Parse(_paymentInfo.DatePaid).ToLongDateString();
            this.cboPaymentType.SelectedIndex = (Int32)_paymentInfo.PaymentType;
            this.txtBankName.Text = _paymentInfo.BankName;
            this.txtCheckNo.Text = _paymentInfo.CheckNo;
            this.txtCardNo.Text = _paymentInfo.CardNumber;
            this.txtCardType.Text = _paymentInfo.CardType;
            this.txtExpiringDate.Text = _paymentInfo.CardExpire;
            this.txtAmountPaid.Text = _paymentInfo.Amount.ToString("N");
            this.txtDiscount.Text = _paymentInfo.Discount.ToString("N");

            _summary = _chargesManager.GetPaymentSummary();

            this.lblBalancetoDate.Text = _summary.Balance.ToString("N");
            this.lblShouldBeBalance.Text = _summary.Balance.ToString("N");

        } //----------------------------------

        //event is raised when the class is closing
        private void ClassClosing(object sender, FormClosingEventArgs e)
        {
            if ((!_hasUpdated && !_hasDeleted && !_hasErrors) && !_paymentInfo.Equals(_paymentInfoTemp))
            {
                String strMsg = "There has been changes made in the current payment information. \nExiting will not save this changes." +
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
                    String strMsg = "Are you sure you want to update the payment information?";

                    DialogResult msgResult = MessageBox.Show(strMsg, "Confirm Update", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                    if (msgResult == DialogResult.Yes)
                    {
                        strMsg = "The payment information has been successfully updated.";

                        _chargesManager.UpdatePaymentDetails(_userInfo, _paymentInfo);

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
                String strMsg = "Deleting a payment information might affect the following:" +
                                "\n\n1.) The system inventory." +
                                "\n\nAre you sure you want to delete the payment information?";

                DialogResult msgResult = MessageBox.Show(strMsg, "Confirm Delete", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);

                if (msgResult == DialogResult.Yes)
                {
                    strMsg = "Are you really sure you want to delete the payment information?";

                    msgResult = MessageBox.Show(strMsg, "Confirm Delete", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                    if (msgResult == DialogResult.Yes)
                    {
                        strMsg = "The payment information has been successfully deleted.";

                        _chargesManager.DeletePaymentDetails(_userInfo, _paymentInfo);

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

        #region Programmer-Defined Void Procedures
        //this procedure sets the payment summary
        protected override void SetPaymentSummary()
        {
            Decimal amountPaid = 0;
            Decimal discount = 0;
            Decimal payments = _chargesManager.GetTotalPaymentsMade(_paymentInfo.ReceiptNo);

            Decimal.TryParse(this.txtAmountPaid.Text, out amountPaid);
            Decimal.TryParse(this.txtDiscount.Text, out discount);

            this.lblBalancetoDate.Text = (_summary.AmountPayable - payments).ToString("N");
            this.lblShouldBeBalance.Text = (_summary.AmountPayable - (payments + amountPaid + discount)).ToString("N");


        } //-----------------------------

        #endregion

    }
}
