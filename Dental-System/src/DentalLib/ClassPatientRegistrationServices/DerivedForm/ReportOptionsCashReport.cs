using System;
using System.Collections.Generic;
using System.Text;

namespace DentalLib
{
    public partial class ReportOptionsCashReport : ReportOptions
    {
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReportOptionsCashReport));
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            // 
            // ReportOptionsCashReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(768, 171);
            this.Name = "ReportOptionsCashReport";
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }
    }
}
