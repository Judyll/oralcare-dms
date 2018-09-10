using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;

namespace DentalLib
{
    partial class Reports
    {
        #region Class Member Declarations
        protected CommonExchange.SysAccess _userInfo;
        protected RegistrationLogic _regManager;

        protected DateTime _dateFrom;
        protected DateTime _dateTo;

        #endregion

        #region Class Properties Declarations
        private Int32 _primaryIndex = 0;
        public Int32 PrimaryIndex
        {
            get { return _primaryIndex; }
            set { _primaryIndex = value; }
        }

        private String _primaryId = "";
        public String PrimaryId
        {
            get { return _primaryId; }
        }

        public Int32 RowCount
        {
            get { return dgvList.Rows.Count; }
        }
        #endregion  
      
        #region Class Constructor
        public Reports()
        {
            this.InitializeComponent();
        }

        public Reports(CommonExchange.SysAccess userInfo)
        {
            this.InitializeComponent();

            _userInfo = userInfo;

            this.Load += new EventHandler(ClassLoad);
            this.dgvList.MouseDown += new MouseEventHandler(dgvListMouseDown);
            this.dgvList.DoubleClick += new EventHandler(dgvListDoubleClick);
            this.dgvList.KeyPress += new KeyPressEventHandler(dgvListKeyPress);
            this.dgvList.KeyDown += new KeyEventHandler(dgvListKeyDown);
            this.dgvList.DataSourceChanged += new EventHandler(dgvListDataSourceChanged);
            this.dgvList.SelectionChanged += new EventHandler(dgvListSelectionChanged);
            this.lnkChange.LinkClicked += new LinkLabelLinkClickedEventHandler(lnkChangeLinkClicked);
            this.btnClose.Click += new EventHandler(btnCloseClick);
        }
        
        #endregion        

        #region Class Event Void Procedures
        //#########################################CLASS Reports EVENTS#####################################################################
        //event is raised when the class is loaded
        protected virtual void ClassLoad(object sender, EventArgs e)
        {
            if (!DatabaseLib.ProcStatic.IsSystemAccessAdmin(_userInfo) && !DatabaseLib.ProcStatic.IsSystemAccessDentalUser(_userInfo))
            {
                DentalLib.ProcStatic.ShowErrorDialog("You are not allowed to access this module.",
                    "Reports");

                this.Close();
            }

            _regManager = new RegistrationLogic(_userInfo);

        } //------------------------------
        //########################################END CLASS Reports EVENTS##################################################################

        //####################################################DATAGRIDVIEW dgvList EVENTS####################################################
        //event is raised when the mouse is down
        protected virtual void dgvListMouseDown(object sender, MouseEventArgs e)
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
                }
            }

        } //-----------------------------

        //event is raised when the mouse is double clicked
        private void dgvListDoubleClick(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(_primaryId))
            {
                this.OnDoubleClickEnter(_primaryId);
            }

        } //---------------------------------

        //event is raised when the key is pressed        
        private void dgvListKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                DataGridView dgvBase = (DataGridView)sender;

                if (dgvBase.Rows.GetRowCount(DataGridViewElementStates.Selected) > 0)
                {
                    DataGridViewRow row = dgvBase.SelectedRows[0];

                    _primaryId = row.Cells[_primaryIndex].Value.ToString();

                    if (!String.IsNullOrEmpty(_primaryId))
                    {
                        this.OnDoubleClickEnter(_primaryId);
                    }
                }

            }
            else
            {
                e.Handled = true;
            }
        } //-----------------------------------

        //event is raised when the key is up
        private void dgvListKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
            }
        } //--------------------------------------

        //event is raised when the data source is changed
        protected virtual void dgvListDataSourceChanged(object sender, EventArgs e)
        {
            DataGridView dgvBase = (DataGridView)sender;
            Int32 result = dgvBase.Rows.Count;

            if (result == 1)
            {
                _primaryId = dgvBase[_primaryIndex, 0].Value.ToString();
            }
            else
            {
                _primaryId = "";
            }

            if (result == 0 || result == 1)
            {
                this.lblResult.Text = result.ToString() + " Record";
            }
            else
            {
                this.lblResult.Text = result.ToString() + " Records";
            }

        } //--------------------------------

        //event is raised when the selection is changed
        protected virtual void dgvListSelectionChanged(object sender, EventArgs e)
        {
            DataGridView dgvBase = (DataGridView)sender;

            if (dgvBase.Rows.GetRowCount(DataGridViewElementStates.Selected) > 0)
            {
                DataGridViewRow row = dgvBase.SelectedRows[0];

                _primaryId = row.Cells[_primaryIndex].Value.ToString();
            }
        } //------------------------------------

        //################################################END DATAGRIDVIEW dgvList EVENTS####################################################

        //##############################################LINKBUTTON lnkChange EVENTS##########################################################
        //event is raised when the link button is clicked
        protected virtual void lnkChangeLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
        } //--------------------------------
        //############################################END LINKBUTTON lnkChange EVENTS########################################################

        //##############################################BUTTON btnClose EVENTS###############################################################
        //event is raised when the button is clicked
        private void btnCloseClick(object sender, EventArgs e)
        {
            this.Close();
        } //---------------------------------------
        //#############################################END BUTTON btnClose EVENTS############################################################
        #endregion

        #region Programmer-Defined Void Procedures

        //this procedure selects the first row in the datagridview
        protected virtual void OnDoubleClickEnter(String id)
        {
            

        } //-----------------------

        //this procedure sets the date covered string
        protected void SetDateCoveredString(String str)
        {
            this.lblDate.Text = str;
        } //-------------------------------

        //this procedure sets the total sting
        protected void SetTotalString(String str)
        {
            this.lblTotal.Text = str;
        } //-----------------------------

        //this procedure sets the datasource
        protected void SetDataGridViewSource(DataTable srcTable)
        {
            this.Cursor = Cursors.WaitCursor;

            this.dgvList.DataSource = null;
            this.dgvList.DataSource = srcTable;

            DentalLib.ProcStatic.SetDataGridViewColumns(this.dgvList, false);

            if (dgvList.Rows.Count >= 1)
            {
                dgvList.Rows[0].Selected = false;
            }            

            this.Cursor = Cursors.Arrow;
        } //----------------------------

        //this procedure hides the change date link
        protected void HideChangeDateLink(Boolean hide)
        {
            this.lnkChange.Visible = !hide;
        } //-----------------------

        #endregion


    }
}
