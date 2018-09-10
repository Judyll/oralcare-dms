using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace DentalLib
{
    partial class ProcedureInformationUpdate
    {
        #region Class General Variable Declarations
        private CommonExchange.Procedure _procedureInfoTemp;
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
        public ProcedureInformationUpdate(CommonExchange.SysAccess userInfo, ProcedureLogic procedureManager, CommonExchange.Procedure procedureInfo)
            :
            base(userInfo)
        {
            this.InitializeComponent();

            _procedureInfo = procedureInfo;
            _procedureInfoTemp = procedureInfo;

            _procedureManager = procedureManager;

            this.FormClosing += new FormClosingEventHandler(ClassClosing);
            this.btnUpdate.Click += new EventHandler(btnUpdateClick);
            this.btnClose.Click += new EventHandler(btnCloseClick);
        }

        
        #endregion

        #region Class Event Void Procedures
        //#####################################CLASS ProcedureInformationUpdate EVENTS########################################
        //event is raised when the class is loaded
        protected override void ClassLoad(object sender, EventArgs e)
        {
            if (!DatabaseLib.ProcStatic.IsSystemAccessAdmin(_userInfo))
            {
                DentalLib.ProcStatic.ShowErrorDialog("You are not allowed to access this module.",
                    "Patient Information");

                _hasErrors = true;

                this.Close();
            }

            this.lblSysID.Text = _procedureInfo.ProcedureSystemId;
            this.txtName.Text = _procedureInfo.ProcedureName;
            this.txtAmount.Text = _procedureInfo.Amount.ToString("N");
            
        } //----------------------------------

        //event is raised when the class is closing
        private void ClassClosing(object sender, FormClosingEventArgs e)
        {
            if (!_hasUpdated && !_procedureInfo.Equals(_procedureInfoTemp))
            {
                String strMsg = "There has been changes made in the current procedure information. \nExiting will not save this changes." +
                                "\n\nAre you sure you want to exit?";
                DialogResult msgResult = MessageBox.Show(strMsg, "Confirm Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (msgResult == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }

        } //----------------------------------

        
        //####################################END CLASS ProcedureInformationUpdate EVENTS######################################

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
                    String strMsg = "Are you sure you want to update the procedure information?";

                    DialogResult msgResult = MessageBox.Show(strMsg, "Confirm Update", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                    if (msgResult == DialogResult.Yes)
                    {
                        strMsg = "The procedure information has been successfully updated.";

                        _procedureManager.UpdateProcedureInformation(_userInfo, _procedureInfo);

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


        #endregion

    }
}
