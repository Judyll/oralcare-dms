using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace DentalLib
{
    partial class UserSearchOnTextboxList
    {
        #region Class Member Declarations
        private CommonExchange.SysAccess _userInfo;
        private UserLogic _userManager;
        #endregion

        #region Class Constructor
        public UserSearchOnTextboxList(CommonExchange.SysAccess userInfo)
        {
            this.InitializeComponent();

            _userInfo = userInfo;

            this.lnkCreate.LinkClicked += new LinkLabelLinkClickedEventHandler(lnkCreateLinkClicked);
            this.pbxRefresh.Click += new EventHandler(pbxRefreshClick);
        }

        #endregion

        #region Class Event Void Procedures

        //####################################CLASS ProcedureSearchOnTextboxList EVENTS####################################
        //event is raised when the class is loaded
        protected override void ClassLoad(object sender, EventArgs e)
        {
            if (!DatabaseLib.ProcStatic.IsSystemAccessAdmin(_userInfo))
            {
                DentalLib.ProcStatic.ShowErrorDialog("You are not allowed to access this module.",
                    "System User Information");               

                this.Close();
            }

            _userManager = new UserLogic(_userInfo);

            this.SetDataGridViewSource(_userManager.GetSearchedUserInformationTable(""));

            base.AdoptGridSize = false;

            base.ClassLoad(sender, e);
        } //------------------------------
        //################################END CLASS ProcedureSearchOnTextboxList EVENTS####################################

        //##################################TEXTBOX txtSearch EVENTS##########################################################
        //event is raised when the key is up
        protected override void txtSearchKeyUp(object sender, KeyEventArgs e)
        {
            this.SetDataGridViewSource(_userManager.GetSearchedUserInformationTable(((TextBox)sender).Text));

            base.txtSearchKeyUp(sender, e);
        } //-------------------------
        //#################################END TEXTBOX txtSearch EVENTS#######################################################

        //##################################PICTUREBOX pbxRefresh EVENTS######################################################
        //event is raised when the picture box is clicked
        private void pbxRefreshClick(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            this.txtSearch.Clear();

            this.Cursor = Cursors.Arrow;
        } //----------------------------
        //##############################END PICTUREBOX pbxRefresh EVENTS######################################################

        //####################################################DATAGRIDVIEW dgvList EVENTS###############################################
        //event is raised when the mouse is double clicked
        protected override void dgvListDoubleClick(object sender, EventArgs e)
        {
            this.ShowSystemUserUpdateDialog();            

        } //---------------------------------

        //event is raised when the key is pressed        
        protected override void dgvListKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                DataGridView dgvBase = (DataGridView)sender;

                if (dgvBase.Rows.GetRowCount(DataGridViewElementStates.Selected) > 0)
                {
                    DataGridViewRow row = dgvBase.SelectedRows[0];

                    _primaryId = row.Cells[_primaryIndex].Value.ToString();

                    this.ShowSystemUserUpdateDialog();
                }

            }
            else
            {
                e.Handled = true;
            }
        } //-------------------------------
        //################################################END DATAGRIDVIEW dgvList EVENTS###############################################

        //############################################LINKBUTTON lnkCreate EVENTS####################################################
        //event is raised when the link is clicked
        private void lnkCreateLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                using (SystemUserCreate frmCreate = new SystemUserCreate(_userInfo, _userManager))
                {
                    frmCreate.ShowDialog(this);

                    if (frmCreate.HasCreated)
                    {
                        this.SetDataGridViewSource(_userManager.GetSearchedUserInformationTable(""));

                        base.txtSearch.Clear();
                        base.txtSearch.Focus();
                    }
                }

                this.Cursor = Cursors.Arrow;
            }
            catch (Exception ex)
            {
                DentalLib.ProcStatic.ShowErrorDialog(ex.Message, "Error Creating A System User Information");
            }

        } //---------------------------------
        //##########################################END LINKBUTTON lnkCreate EVENTS###################################################


        #endregion

        #region Programmer-Defined Void Procedures
        private void ShowSystemUserUpdateDialog()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                if (!String.IsNullOrEmpty(_primaryId))
                {
                    using (SystemUserUpdate frmUpdate = new SystemUserUpdate(_userInfo, _userManager, 
                        _userManager.SelectForInformationSystemUserInfo(_userInfo, _primaryId)))
                    {
                        frmUpdate.ShowDialog(this);

                        if (frmUpdate.HasUpdated)
                        {
                            this.SetDataGridViewSource(_userManager.GetSearchedUserInformationTable(this.txtSearch.Text));
                        }
                    }                   
                     
                }

                this.Cursor = Cursors.Arrow;

            }
            catch (Exception ex)
            {
                DentalLib.ProcStatic.ShowErrorDialog(ex.Message, "Error Updating A System User Information");
            }
        }
        #endregion

    }
}
