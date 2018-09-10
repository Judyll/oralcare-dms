using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace DentalLib
{
    public delegate void ControlManagerButtonClick();
    public delegate void ControlManagerPressEnter(object sender, KeyEventArgs e);
    public delegate void ControlManagerKeyUp(object sender, KeyEventArgs e);

    partial class ControlManager
    {
        #region Class Event Declarations
        public event ControlManagerButtonClick ClickSalesReport;
        public event ControlManagerButtonClick ClickCashReport;
        public event ControlManagerButtonClick ClickAccountsReceivable;
        public event ControlManagerButtonClick ClickClose;
        public event ControlManagerButtonClick ClickUsers;
        public event ControlManagerButtonClick ClickProcedures;
        public event ControlManagerPressEnter OnPressEnter;
        public event ControlManagerKeyUp OnTextboxKeyUp;
        #endregion

        #region Class Member Declarations
        private ToolTip ttpMain = new ToolTip();
        #endregion

        #region Class Properties Declarations
        private String _strSearch;
        public String GetSearchString
        {
            get { return _strSearch; }
        }
        #endregion

        #region Class Constructor
        public ControlManager()
        {            
            this.InitializeComponent();

            this.pbxSales.Click += new EventHandler(pbxSalesClick);
            this.pbxAccountsReceivables.Click += new EventHandler(pbxAccountsReceivablesClick);
            this.pbxCash.Click += new EventHandler(pbxCashClick);
            this.pbxClose.Click += new EventHandler(pbxCloseClick);
            this.pbxUsers.Click += new EventHandler(pbxUsersClick);
            this.pbxProcedures.Click += new EventHandler(pbxProceduresClick);
            this.txtSearch.KeyUp += new KeyEventHandler(txtSearchKeyUp);
            this.txtSearch.KeyPress += new KeyPressEventHandler(txtSearchKeyPress);
        }              
        
        #endregion

        #region Class Event Void Procedures

        //################################CLASS ControlManager EVENTS########################################
        //event is raised when the class is loaded
        protected override void ClassLoad(object sender, EventArgs e)
        {
            base.ClassLoad(sender, e);

            _maxHeight = this.Height;

            ttpMain.ToolTipIcon = ToolTipIcon.Info;
            ttpMain.ToolTipTitle = "Console";
            ttpMain.UseAnimation = true;
            ttpMain.UseFading = true;
            ttpMain.IsBalloon = true;
            ttpMain.AutoPopDelay = 3000;
            ttpMain.SetToolTip(pbxClose, "Close");
            ttpMain.SetToolTip(pbxUsers, "User Maintenance");
            ttpMain.SetToolTip(pbxProcedures, "Procedure Maintenance");
            ttpMain.SetToolTip(pbxAccountsReceivables, "Accounts Receivable");
            ttpMain.SetToolTip(pbxSales, "Sales Report");
            ttpMain.SetToolTip(pbxCash, "Cash Report");
        }
        //################################END CLASS ControlManager EVENTS#####################################

        //#################################PICTUREBOX pbxClose EVENTS########################################
        //event is raised when the control is clicked
        private void pbxCloseClick(object sender, EventArgs e)
        {
            ControlManagerButtonClick ev = ClickClose;

            if (ev != null)
            {
                ClickClose();
            }

        } //----------------------------
        //##############################END PICTUREBOX pbxClose EVENTS#######################################

        //##################################PICTUREBOX pbxUsers EVENTS#######################################
        //event is raised when the control is clicked
        private void pbxUsersClick(object sender, EventArgs e)
        {
            ControlManagerButtonClick ev = ClickUsers;

            if (ev != null)
            {
                ClickUsers();
            }

            this.txtSearch.Focus();
        } //--------------------------------
        //################################END PICTUREBOX pbxUsers EVENTS#####################################

        //#####################################PICTUREBOX pbxProcedures EVENTS##################################
        //event is raised when the control is clicked
        private void pbxProceduresClick(object sender, EventArgs e)
        {
            ControlManagerButtonClick ev = ClickProcedures;

            if (ev != null)
            {
                ClickProcedures();
            }

            this.txtSearch.Focus();
        } //-------------------------------
        //###################################END PICTUREBOX pbxProcedures EVENTS################################

        //####################################PICTUREBOX pbxCash EVENTS########################################
        //event is raised when the control is clicked
        private void pbxCashClick(object sender, EventArgs e)
        {
            ControlManagerButtonClick ev = ClickCashReport;

            if (ev != null)
            {
                ClickCashReport();
            }

            this.txtSearch.Focus();
        } //--------------------------------
        //###################################END PICTUREBOX pbxCash EVENTS#####################################

        //####################################PICTUREBOX pbxAccountsReceivable EVENTS###########################
        //event is raised when the control is clicked
        private void pbxAccountsReceivablesClick(object sender, EventArgs e)
        {
            ControlManagerButtonClick ev = ClickAccountsReceivable;

            if (ev != null)
            {
                ClickAccountsReceivable();
            }

            this.txtSearch.Focus();
        } //-----------------------------
        //##################################END PICTUREBOX pbxAccountsReceivable EVENTS###########################

        //######################################PICTUREBOX pbxSales EVENTS########################################
        //event is raised when the control is clicked
        private void pbxSalesClick(object sender, EventArgs e)
        {
            ControlManagerButtonClick ev = ClickSalesReport;

            if (ev != null)
            {
                ClickSalesReport();
            }

            this.txtSearch.Focus();
        } //------------------------------------
        //#####################################END PICTUREBOX pbxSales EVENTS#####################################

        //###################################TEXTBOX txtSearch EVENTS##########################################
        //event is raised when the key is pressed
        private void txtSearchKeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || char.IsLetter(e.KeyChar) || char.IsControl(e.KeyChar) ||
                char.IsWhiteSpace(e.KeyChar) || e.KeyChar == '*' || e.KeyChar == '\r' || char.IsPunctuation(e.KeyChar))
            {
                txtSearch.ReadOnly = false;
                _strSearch = e.KeyChar.ToString();
            }
            else
            {
                txtSearch.ReadOnly = true;
            }
        } //---------------------------------

        //event is raised when the key is up
        private void txtSearchKeyUp(object sender, KeyEventArgs e)
        {
            _strSearch = txtSearch.Text;

            if (e.KeyCode == Keys.Enter)
            {
                _strSearch = txtSearch.Text;

                ControlManagerPressEnter ev = OnPressEnter;

                if (ev != null)
                {
                    OnPressEnter(sender, e);
                }
            }
            else
            {
                ControlManagerKeyUp ev = OnTextboxKeyUp;

                if (ev != null)
                {
                    OnTextboxKeyUp(sender, e);
                }
            }
            
        }        
        //#################################END TEXTBOX txtSearch EVENTS########################################
        #endregion

        #region Programmer-Defined Void Procedures

        //this procedure sets the focus on search textbox
        public void SetFocusOnSearchTextBox()
        {
            this.txtSearch.Focus();
        } //----------------------------
        #endregion
    }
}
