using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace DentalSystem
{
    public delegate void PatientSearchListCreateLinkClicked();
    public delegate void PatientSearchListUpdateLinkClicked(String id);

    partial class PatientSearchList
    {
        #region Class Events Declarations
        public event PatientSearchListCreateLinkClicked LinkClickedCreate;
        public event PatientSearchListUpdateLinkClicked LinkClickedUpdate;
        #endregion

        #region Class Constructor
        public PatientSearchList()
        {
            this.InitializeComponent();

            this.lnkCreate.LinkClicked += new LinkLabelLinkClickedEventHandler(lnkCreateLinkClicked);
            this.lnkUpdate.LinkClicked += new LinkLabelLinkClickedEventHandler(lnkUpdateLinkClicked);
        }                
        #endregion

        //###################################LINKBUTTON lnkCreate EVENTS#########################################################
        //event is raised when the link is clicked
        private void lnkCreateLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            PatientSearchListCreateLinkClicked ev = LinkClickedCreate;

            if (ev != null)
            {
                LinkClickedCreate();
            }
        } //-------------------------------------
        //###################################END LINKBUTTON lnkCreate EVENTS#####################################################

        //#######################################LINKBUTTON lnkUpdate EVENTS#####################################################
        //event is raised when the link is clicked
        private void lnkUpdateLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            PatientSearchListUpdateLinkClicked ev = LinkClickedUpdate;

            if (ev != null)
            {
                LinkClickedUpdate(_primaryId);
            }
        } //-----------------------------------------
        //#####################################END LINKBUTTON lnkUpdate EVENTS####################################################

        //####################################################DATAGRIDVIEW dgvList EVENTS####################################################
        //event is raised when the mouse is down
        protected override void dgvListMouseDown(object sender, MouseEventArgs e)
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
                        _primaryId = "";
                        break;
                }

                if (rowIndex != -1)
                {
                    _primaryId = dgvBase[_primaryIndex, rowIndex].Value.ToString();

                    this.lnkUpdate.Visible = true;
                }
            }
        } //-------------------------------

        //event is raised when the data source is changed
        protected override void dgvListDataSourceChanged(object sender, EventArgs e)
        {
            base.dgvListDataSourceChanged(sender, e);

            if (dgvList.Rows.Count >= 1)
            {
                dgvList.Rows[0].Selected = false;
            }

            this.lnkUpdate.Visible = false;
        } //--------------------------

        //################################################END DATAGRIDVIEW dgvList EVENTS####################################################

        #region Programmer-Defined Void Procedures

        //this procedure selects the first row in the datagridview
        public override void SelectFirstRowInDataGridView()
        {
            if (dgvList.Rows.Count >= 1)
            {
                dgvList.Rows[0].Selected = true;
                dgvList.Focus();

                this.lnkUpdate.Visible = true;
            }

        } //-----------------------

        #endregion
    }
}
