using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace DentalLib
{
    partial class PaymentDetailsCreate
    {
        #region Class Properties Declarations
        private Boolean _hasCreated = false;
        public Boolean HasCreated
        {
            get { return _hasCreated; }
        }
        #endregion

        #region Class Constructor
        public PaymentDetailsCreate(CommonExchange.SysAccess userInfo, CommonExchange.Patient patientInfo, CommonExchange.Registration regInfo,
            ChargesLogic chargesManager) : base(userInfo, patientInfo, regInfo, chargesManager)
        {
            this.InitializeComponent();

            this.FormClosing += new FormClosingEventHandler(ClassClosing);
            this.btnCreate.Click += new EventHandler(btnCreateClick);
            this.btnCancel.Click += new EventHandler(btnCancelClick);
        }
        #endregion

        #region Class Event Void Procedures
        //################################################CLASS ProcedureInformationCreate EVENTS#######################################################

        //event is raised when the class is closing
        private void ClassClosing(object sender, FormClosingEventArgs e)
        {
            if (!_hasCreated && !_hasErrors)
            {
                String strMsg = "Are you sure you want to cancel the creation of a new payment information?";
                DialogResult msgResult = MessageBox.Show(strMsg, "Confirm Cancel", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (msgResult == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }

        } //---------------------------

        //#############################################END CLASS ProcedureInformationCreate EVENTS#######################################################

        //##############################################BUTTON btnCancel EVENTS########################################################
        //event is raised when the button is clicked
        private void btnCancelClick(object sender, EventArgs e)
        {
            this.Close();
        } //------------------------------
        //############################################END BUTTON btnCancel EVENTS######################################################

        //#############################################BUTTON btnCreate EVENTS########################################################
        //event is raised when the button is clicked
        private void btnCreateClick(object sender, EventArgs e)
        {
            if (this.ValidateControls())
            {
                try
                {
                    String strMsg = "Are you sure you want to create the new payment information?";

                    DialogResult msgResult = MessageBox.Show(strMsg, "Confirm Create", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                    if (msgResult == DialogResult.Yes)
                    {
                        strMsg = "The payment information has been successfully created.";

                        this.Cursor = Cursors.WaitCursor;

                        _chargesManager.InsertPaymentDetails(_userInfo, _paymentInfo);

                        this.Cursor = Cursors.Arrow;

                        MessageBox.Show(strMsg, "Success", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                        _hasCreated = true;

                        this.Close();
                    }
                    else if (msgResult == DialogResult.Cancel)
                    {
                        this.Close();
                    }
                }
                catch (Exception ex)
                {
                    DentalLib.ProcStatic.ShowErrorDialog(ex.Message, "Error Creating A Patient Charge Information");
                }
            }
        } //---------------------------------
        //#########################################END BUTTON btnCreate EVENTS########################################################

        #endregion

    }
}
