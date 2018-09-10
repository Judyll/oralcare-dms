using System;
using System.Collections.Generic;
using System.Text;

namespace DentalLib
{
    public partial class ReportOptionsSalesReport : ReportOptions
    {
        private System.Windows.Forms.GroupBox groupBox2;
        protected System.Windows.Forms.Label lblSelected;
        protected System.Windows.Forms.Label label1;
        protected System.Windows.Forms.CheckedListBox cbxProcedures;
        protected System.Windows.Forms.LinkLabel lnkAll;
        protected System.Windows.Forms.LinkLabel lnkNone;
    
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReportOptionsSalesReport));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblSelected = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbxProcedures = new System.Windows.Forms.CheckedListBox();
            this.lnkAll = new System.Windows.Forms.LinkLabel();
            this.lnkNone = new System.Windows.Forms.LinkLabel();
            this.panel2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            // 
            // panel2
            // 
            this.panel2.Location = new System.Drawing.Point(-1, 471);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblSelected);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.cbxProcedures);
            this.groupBox2.Controls.Add(this.lnkAll);
            this.groupBox2.Controls.Add(this.lnkNone);
            this.groupBox2.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.Color.Navy;
            this.groupBox2.Location = new System.Drawing.Point(12, 127);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(744, 334);
            this.groupBox2.TabIndex = 24;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "PROCEDURE LIST";
            // 
            // lblSelected
            // 
            this.lblSelected.AutoSize = true;
            this.lblSelected.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelected.ForeColor = System.Drawing.Color.Red;
            this.lblSelected.Location = new System.Drawing.Point(60, 311);
            this.lblSelected.Name = "lblSelected";
            this.lblSelected.Size = new System.Drawing.Size(18, 19);
            this.lblSelected.TabIndex = 10;
            this.lblSelected.Text = "0";
            this.lblSelected.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkBlue;
            this.label1.Location = new System.Drawing.Point(4, 314);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 14);
            this.label1.TabIndex = 6;
            this.label1.Text = "Selected:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbxProcedures
            // 
            this.cbxProcedures.BackColor = System.Drawing.Color.Beige;
            this.cbxProcedures.CheckOnClick = true;
            this.cbxProcedures.FormattingEnabled = true;
            this.cbxProcedures.HorizontalScrollbar = true;
            this.cbxProcedures.Location = new System.Drawing.Point(6, 36);
            this.cbxProcedures.Name = "cbxProcedures";
            this.cbxProcedures.Size = new System.Drawing.Size(732, 274);
            this.cbxProcedures.TabIndex = 2;
            this.cbxProcedures.ThreeDCheckBoxes = true;
            // 
            // lnkAll
            // 
            this.lnkAll.AutoSize = true;
            this.lnkAll.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkAll.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.lnkAll.LinkColor = System.Drawing.Color.DarkBlue;
            this.lnkAll.Location = new System.Drawing.Point(588, 17);
            this.lnkAll.Name = "lnkAll";
            this.lnkAll.Size = new System.Drawing.Size(70, 14);
            this.lnkAll.TabIndex = 0;
            this.lnkAll.TabStop = true;
            this.lnkAll.Text = "| Select All |";
            // 
            // lnkNone
            // 
            this.lnkNone.AutoSize = true;
            this.lnkNone.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkNone.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.lnkNone.LinkColor = System.Drawing.Color.DarkBlue;
            this.lnkNone.Location = new System.Drawing.Point(654, 17);
            this.lnkNone.Name = "lnkNone";
            this.lnkNone.Size = new System.Drawing.Size(84, 14);
            this.lnkNone.TabIndex = 1;
            this.lnkNone.TabStop = true;
            this.lnkNone.Text = "| Select None |";
            // 
            // ReportOptionsSalesReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(768, 505);
            this.Controls.Add(this.groupBox2);
            this.Name = "ReportOptionsSalesReport";
            this.Controls.SetChildIndex(this.groupBox2, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.panel2, 0);
            this.panel2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }
    }
}
