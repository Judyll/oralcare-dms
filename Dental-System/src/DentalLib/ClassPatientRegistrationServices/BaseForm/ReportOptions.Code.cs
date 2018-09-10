using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace DentalLib
{
    partial class ReportOptions
    {
        #region Class Properties Declarations
        private Boolean _applyOptions = false;
        public Boolean ApplyOptions
        {
            get { return _applyOptions; }
        }

        protected DateTime _dateFrom;
        public DateTime DateFrom
        {
            get { return _dateFrom; }
        }

        protected DateTime _dateTo;
        public DateTime DateTo
        {
            get { return _dateTo; }
        }

        #endregion

        #region Class Constructor
        public ReportOptions()
        {
            this.InitializeComponent();
        }

        public ReportOptions(DateTime iDateFrom, DateTime iDateTo)
        {
            this.InitializeComponent();

            _dateFrom = DateTime.Parse(iDateFrom.ToLongDateString() + " 12:00:00 AM");
            _dateTo = DateTime.Parse(iDateTo.ToLongDateString() + " 11:59:59 PM");

            this.Load += new EventHandler(ClassLoad);
            this.lnkChangeDateFrom.LinkClicked += new LinkLabelLinkClickedEventHandler(lnkChangeDateFromLinkClicked);
            this.lnkChangeDateTo.LinkClicked += new LinkLabelLinkClickedEventHandler(lnkChangeDateToLinkClicked);
            this.btnApply.Click += new EventHandler(btnApplyClick);
            this.btnCancel.Click += new EventHandler(btnCancelClick);
        }        
        
        #endregion

        #region Class Event Void Procedures

        //######################################CLASS ReportOptions EVENTS##########################################################
        //event is raised when the class is loaded
        protected virtual void ClassLoad(object sender, EventArgs e)
        {
            this.lblDateFrom.Text = _dateFrom.ToLongDateString();
            this.lblDateTo.Text = _dateTo.ToLongDateString();
        } //----------------------------------------
        //####################################END CLASS ReportOptions EVENTS########################################################

        //#################################LINKBUTTON lnkChangeDateFrom EVENTS######################################################
        //event is raised when the link is clicked
        private void lnkChangeDateFromLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            DateTime result = _dateFrom;

            using (DatePicker frmPicker = new DatePicker(result))
            {
                frmPicker.ShowDialog(this);

                if (frmPicker.HasSelectedDate)
                {
                    if (DateTime.TryParse(frmPicker.GetSelectedDate.ToShortDateString() + " 12:00:00 AM", out result) &&
                        (DateTime.Compare(result, _dateTo) <= 0))
                    {
                        _dateFrom = result;
                        this.lblDateFrom.Text = result.ToLongDateString();
                    }                    
                }
            }

            this.Cursor = Cursors.Arrow;
        } //-----------------------------------
        //##################################END LINKBUTTON lnkChangeDateFrom EVENTS##################################################

        //##########################################LINKBUTTON lnkChangeDateTo EVENTS################################################
        //event is raised when the link is clicked
        private void lnkChangeDateToLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            DateTime result = _dateTo;

            using (DatePicker frmPicker = new DatePicker(result))
            {
                frmPicker.ShowDialog(this);

                if (frmPicker.HasSelectedDate)
                {
                    if (DateTime.TryParse(frmPicker.GetSelectedDate.ToShortDateString() + " 11:59:59 PM", out result) &&
                        (DateTime.Compare(_dateFrom, result) <= 0))
                    {
                        _dateTo = result;
                        this.lblDateTo.Text = result.ToLongDateString();
                    }
                }
            }

            this.Cursor = Cursors.Arrow;
        } //------------------------------------
        //########################################END LINKBUTTON lnkChangeDateTo EVENTS##############################################

        //#############################################BUTTON btnApply EVENTS########################################################
        //event is raised when the button is clicked
        protected virtual void btnApplyClick(object sender, EventArgs e)
        {
            _applyOptions = true;

            this.Close();
        } //-------------------------------
        //###########################################END BUTTON btnApply EVENTS######################################################

        //#############################################BUTTON btnCancel EVENTS#######################################################
        //event is raised when the button is clicked
        private void btnCancelClick(object sender, EventArgs e)
        {
            _applyOptions = false;

            this.Close();
        } //------------------------------------
        //##########################################END BUTTON btnCancel EVENTS######################################################
        #endregion
    }
}
