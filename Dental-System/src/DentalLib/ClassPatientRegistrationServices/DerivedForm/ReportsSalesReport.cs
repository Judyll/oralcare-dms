using System;
using System.Collections.Generic;
using System.Text;

namespace DentalLib
{
    public partial class ReportsSalesReport : Reports
    {
        private System.Windows.Forms.ProgressBar pgbSales;
        private System.Windows.Forms.Button btnPrint;

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReportsSalesReport));
            this.btnPrint = new System.Windows.Forms.Button();
            this.pgbSales = new System.Windows.Forms.ProgressBar();
            this.panelbottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelbottom
            // 
            this.panelbottom.Controls.Add(this.btnPrint);
            this.panelbottom.Location = new System.Drawing.Point(-7, 644);
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
            this.btnPrint.TabIndex = 7;
            this.btnPrint.Text = "Print";
            this.btnPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnPrint.UseVisualStyleBackColor = true;
            // 
            // pgbSales
            // 
            this.pgbSales.Location = new System.Drawing.Point(11, 631);
            this.pgbSales.Name = "pgbSales";
            this.pgbSales.Size = new System.Drawing.Size(784, 10);
            this.pgbSales.TabIndex = 26;
            // 
            // ReportsSalesReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(805, 678);
            this.Controls.Add(this.pgbSales);
            this.Name = "ReportsSalesReport";
            this.Controls.SetChildIndex(this.paneltop, 0);
            this.Controls.SetChildIndex(this.panelbottom, 0);
            this.Controls.SetChildIndex(this.pgbSales, 0);
            this.panelbottom.ResumeLayout(false);
            this.ResumeLayout(false);

        }
    }
}
