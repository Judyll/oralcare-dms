using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace DentalLib
{
    partial class ProcedureSearchOnTextboxList
    {
        #region Class Member Declarations
        private CommonExchange.SysAccess _userInfo;
        private Boolean _forMaintenance;
        private ProcedureLogic _procedureManager;
        #endregion

        #region Class Properties Declarations
        public CommonExchange.Procedure ProcedureInformation
        {
            get { return _procedureManager.GetBySystemIdProcedureInformationDetails(base.PrimaryId); }
        }
        #endregion

        #region Class Constructor
        public ProcedureSearchOnTextboxList(CommonExchange.SysAccess userInfo, Boolean forMaintenance)
        {
            this.InitializeComponent();

            _userInfo = userInfo;
            _forMaintenance = forMaintenance;

            _procedureManager = new ProcedureLogic(userInfo);

            this.lnkCreate.LinkClicked += new LinkLabelLinkClickedEventHandler(lnkCreateLinkClicked);
            this.pbxRefresh.Click += new EventHandler(pbxRefreshClick);
        }

        
        #endregion

        #region Class Event Void Procedures

        //####################################CLASS ProcedureSearchOnTextboxList EVENTS####################################
        //event is raised when the class is loaded
        protected override void ClassLoad(object sender, EventArgs e)
        {
            if (!_forMaintenance || !DatabaseLib.ProcStatic.IsSystemAccessAdmin(_userInfo))
            {
                this.lnkCreate.Visible = false;
            }

            this.SetDataGridViewSource(_procedureManager.GetSearchedProcedureInformation(""));

            base.AdoptGridSize = true;

            base.ClassLoad(sender, e);
        } //------------------------------
        //################################END CLASS ProcedureSearchOnTextboxList EVENTS####################################

        //##################################TEXTBOX txtSearch EVENTS##########################################################
        //event is raised when the key is up
        protected override void txtSearchKeyUp(object sender, KeyEventArgs e)
        {
            this.SetDataGridViewSource(_procedureManager.GetSearchedProcedureInformation(((TextBox)sender).Text));

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
            if (!_forMaintenance)
            {
                base.dgvListDoubleClick(sender, e);
            }
            else
            {
                this.ShowProcedureUpdateDialog();
            }

        } //---------------------------------

        //event is raised when the key is pressed        
        protected override void dgvListKeyPress(object sender, KeyPressEventArgs e)
        {
            if (!_forMaintenance)
            {
                base.dgvListKeyPress(sender, e);
            }
            else
            {
                this.ShowProcedureUpdateDialog();
            }
        } //-------------------------------
        //################################################END DATAGRIDVIEW dgvList EVENTS###############################################

        //############################################LINKBUTTON lnkCreate EVENTS####################################################
        //event is raised when the link is clicked
        private void lnkCreateLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                using (ProcedureInformationCreate frmCreate = new ProcedureInformationCreate(_userInfo, _procedureManager))
                {
                    frmCreate.ShowDialog(this);

                    if (frmCreate.HasCreated)
                    {
                        this.SetDataGridViewSource(_procedureManager.GetSearchedProcedureInformation(""));

                        base.txtSearch.Clear();
                        base.txtSearch.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                DentalLib.ProcStatic.ShowErrorDialog(ex.Message, "Error Creating A Procedure Information");
            }

        } //---------------------------------
        //##########################################END LINKBUTTON lnkCreate EVENTS###################################################

        
        #endregion

        #region Programmer-Defined Void Procedures
        private void ShowProcedureUpdateDialog()
        {
            try
            {
                if (!String.IsNullOrEmpty(_primaryId))
                {
                    using (ProcedureInformationUpdate frmUpdate = new ProcedureInformationUpdate(_userInfo, _procedureManager,
                    _procedureManager.GetBySystemIdProcedureInformationDetails(_primaryId)))
                    {
                        frmUpdate.ShowDialog(this);

                        if (frmUpdate.HasUpdated)
                        {
                            this.SetDataGridViewSource(_procedureManager.GetSearchedProcedureInformation(this.txtSearch.Text));
                        }
                    }
                }
                
            }
            catch (Exception ex)
            {
                DentalLib.ProcStatic.ShowErrorDialog(ex.Message, "Error Updating A Procedure Information");
            }
        }
        #endregion
    }
}
