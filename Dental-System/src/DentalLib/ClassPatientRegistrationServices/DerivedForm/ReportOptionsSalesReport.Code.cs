using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;

namespace DentalLib
{
    partial class ReportOptionsSalesReport
    {
        #region Class Member Declarations
        private CommonExchange.SysAccess _userInfo;
        private RegistrationLogic _regManager;
        #endregion

        #region Class Properties Declarations
        public CheckedListBox ProcedureCheckedListBox
        {
            get { return this.cbxProcedures; }
        }
        #endregion

        #region Class Constructor
        public ReportOptionsSalesReport(CommonExchange.SysAccess userInfo, DateTime iDateFrom, DateTime iDateTo, RegistrationLogic regManager)
            : base(iDateFrom, iDateTo)
        {
            this.InitializeComponent();

            _userInfo = userInfo;
            _regManager = regManager;

            this.lnkAll.LinkClicked += new LinkLabelLinkClickedEventHandler(lnkAllLinkClicked);
            this.lnkNone.LinkClicked += new LinkLabelLinkClickedEventHandler(lnkNoneLinkClicked);
            this.cbxProcedures.SelectedIndexChanged += new EventHandler(cbxProceduresSelectedIndexChanged);
        }
        #endregion

        #region Class Event Void Procedures

        //######################################CLASS ReportOptionsSalesReport EVENTS##########################################################
        //event is raised when the class is loaded
        protected override void ClassLoad(object sender, EventArgs e)
        {
            base.ClassLoad(sender, e);

            this.SetProceduresCheckedListBox(_regManager.SelectProcedureInformation(_userInfo), "procedure_name");

            this.SetAllListAsChecked(this.cbxProcedures, true);
        } //----------------------------------------
        //####################################END CLASS ReportOptionsSalesReport EVENTS########################################################

        //########################################CHECKEDLISTBOX cbxProcedures EVENTS###################################################
        //event is raised when the selected index is changed
        private void cbxProceduresSelectedIndexChanged(object sender, EventArgs e)
        {
            lblSelected.Text = cbxProcedures.CheckedItems.Count.ToString();

            if (cbxProcedures.CheckedItems.Count == 0)
            {
                this.btnApply.Enabled = false;
            }
            else
            {
                this.btnApply.Enabled = true;
            }

        } //---------------------------------  

        //######################################END CHECKEDLISTBOX cbxProcedures EVENTS##################################################

        //###########################################LINKBUTTON lnkAll EVENTS##########################################################
        //event is raised when the link is clicked
        private void lnkAllLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.SetAllListAsChecked(cbxProcedures, true);
        } //------------------------
        //###########################################END LINKBUTTON lnkAll EVENTS######################################################

        //#########################################LINKBUTTON lnkNone EVENTS###########################################################
        //event is raised when the link is clicked
        private void lnkNoneLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.SetAllListAsChecked(cbxProcedures, false);
        } //--------------------------
        //########################################END LINKBUTTON lnkNone EVENTS###########################################################  

        #endregion

        #region Programmer-Defined Void Procedures

        //this procedure fills the checkbox list
        private void SetProceduresCheckedListBox(DataTable srcTable, String colName)
        {
            cbxProcedures.Items.Clear();
            lblSelected.Text = cbxProcedures.CheckedItems.Count.ToString();

            foreach (DataRow baseRow in srcTable.Rows)
            {
                cbxProcedures.Items.Add(baseRow[colName].ToString());
            }

        } //---------------------------

        //this procedure checks all the list in the checkbox
        private void SetAllListAsChecked(CheckedListBox cbxBase, Boolean isChecked)
        {
            for (Int32 i = 0; i < cbxBase.Items.Count; i++)
            {
                cbxBase.SetItemChecked(i, isChecked);
            }

            lblSelected.Text = cbxBase.CheckedItems.Count.ToString();

            if (cbxBase.CheckedItems.Count == 0)
            {
                this.btnApply.Enabled = false;
            }
            else
            {
                this.btnApply.Enabled = true;
            }

        } //-----------------------------

        #endregion       

    }
}
