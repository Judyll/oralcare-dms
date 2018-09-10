using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace DentalLib
{
    partial class ProcedureInformation
    {
        #region Class Member Declarations
        protected CommonExchange.SysAccess _userInfo;
        protected CommonExchange.Procedure _procedureInfo;
        protected ProcedureLogic _procedureManager;
        protected Boolean _hasErrors = false;

        private ErrorProvider _errProvider;
        #endregion

        #region Class Constructor
        public ProcedureInformation()
        {
            this.InitializeComponent();
        }

        public ProcedureInformation(CommonExchange.SysAccess userInfo)
        {
            this.InitializeComponent();

            _userInfo = userInfo;

            _errProvider = new ErrorProvider();

            this.Load += new EventHandler(ClassLoad);
            this.txtName.Validated += new EventHandler(txtNameValidated);
            this.txtAmount.KeyPress += new KeyPressEventHandler(txtAmountKeyPress);
            this.txtAmount.Validating += new System.ComponentModel.CancelEventHandler(txtAmountValidating);
            this.txtAmount.Validated += new EventHandler(txtAmountValidated);
        }        
        
        #endregion

        #region Class Event Void Procedures

        //############################################CLASS ProcedureInformation EVENTS#######################################################
        //event is raised when the class is loaded
        protected virtual void ClassLoad(object sender, EventArgs e)
        {
            if (!DatabaseLib.ProcStatic.IsSystemAccessAdmin(_userInfo))
            {
                DentalLib.ProcStatic.ShowErrorDialog("You are not allowed to access this module.",
                    "Procedure Information");

                _hasErrors = true;

                this.Close();
            }

            _procedureInfo = new CommonExchange.Procedure();
        }
        //############################################END CLASS ProcedureInformation EVENTS#######################################################

        //##############################################TEXTBOX txtName EVENTS####################################################################
        //event is raised when the control is validated
        private void txtNameValidated(object sender, EventArgs e)
        {
            _procedureInfo.ProcedureName = DentalLib.ProcStatic.TrimStartEndString(txtName.Text);
        } //--------------------------------
        //#############################################END TEXTBOX txtName EVENTS#################################################################

        //#############################################TEXTBOX txtAmount EVENTS##################################################################
        //event is raised when the control is validated
        private void txtAmountValidated(object sender, EventArgs e)
        {
            Decimal amount = 0;

            if (Decimal.TryParse(txtAmount.Text, out amount))
            {
                _procedureInfo.Amount = amount;
            }

        } //-----------------------------------

        private void txtAmountValidating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DentalLib.ProcStatic.TextBoxValidateAmount((TextBox)sender);
        }

        private void txtAmountKeyPress(object sender, KeyPressEventArgs e)
        {
            DentalLib.ProcStatic.TextBoxAmountOnly((TextBox)sender, e);
        }

        //###########################################END TEXTBOX txtAmount EVENTS################################################################

        #endregion

        #region Programmer-Defined Function Procedures

        //this function determines if the controls are validated
        protected Boolean ValidateControls()
        {
            Boolean isValid = true;

            _errProvider.SetError(this.txtName, "");

            if (_procedureManager.IsExistsNameProcedureInformation(_userInfo, _procedureInfo))
            {
                _errProvider.SetIconAlignment(txtName, ErrorIconAlignment.MiddleLeft);
                _errProvider.SetError(txtName, "The procedure name already exists.");
                isValid = false;
            }

            return isValid;
        } //------------------------

        #endregion


    }
}
