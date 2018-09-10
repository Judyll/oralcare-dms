using System;
using System.Collections.Generic;
using System.Text;

namespace DentalLib
{
    public partial class ReportsCashReport : Reports
    {
        private System.Windows.Forms.Button btnPrint;

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReportsCashReport));
            this.btnPrint = new System.Windows.Forms.Button();
            this.panelbottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelbottom
            // 
            this.panelbottom.Controls.Add(this.btnPrint);
            this.panelbottom.Controls.SetChildIndex(this.btnPrint, 0);
            // 
            // paneltop
            // 
            this.paneltop.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("paneltop.BackgroundImage")));
            // 
            // btnPrint
            // 
            this.btnPrint.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPrint.BackgroundImage")));
            this.btnPrint.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnPrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.Location = new System.Drawing.Point(618, 5);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(86, 23);
            this.btnPrint.TabIndex = 6;
            this.btnPrint.Text = "Print";
            this.btnPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnPrint.UseVisualStyleBackColor = true;
            // 
            // ReportsCashReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(805, 666);
            this.Name = "ReportsCashReport";
            this.panelbottom.ResumeLayout(false);
            this.ResumeLayout(false);

        }
    }
}
