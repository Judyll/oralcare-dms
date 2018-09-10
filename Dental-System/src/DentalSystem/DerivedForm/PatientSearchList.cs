using System;
using System.Collections.Generic;
using System.Text;

namespace DentalSystem
{
    internal partial class PatientSearchList : SearchList
    {
        private System.Windows.Forms.LinkLabel lnkCreate;
        private System.Windows.Forms.LinkLabel lnkUpdate;
    
        private void InitializeComponent()
        {
            this.lnkCreate = new System.Windows.Forms.LinkLabel();
            this.lnkUpdate = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // lnkCreate
            // 
            this.lnkCreate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lnkCreate.AutoSize = true;
            this.lnkCreate.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkCreate.ForeColor = System.Drawing.Color.Red;
            this.lnkCreate.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.lnkCreate.LinkColor = System.Drawing.Color.MidnightBlue;
            this.lnkCreate.Location = new System.Drawing.Point(333, 264);
            this.lnkCreate.Name = "lnkCreate";
            this.lnkCreate.Size = new System.Drawing.Size(126, 15);
            this.lnkCreate.TabIndex = 73;
            this.lnkCreate.TabStop = true;
            this.lnkCreate.Text = "| Create new patient |";
            // 
            // lnkUpdate
            // 
            this.lnkUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lnkUpdate.AutoSize = true;
            this.lnkUpdate.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkUpdate.ForeColor = System.Drawing.Color.Red;
            this.lnkUpdate.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.lnkUpdate.LinkColor = System.Drawing.Color.MidnightBlue;
            this.lnkUpdate.Location = new System.Drawing.Point(205, 264);
            this.lnkUpdate.Name = "lnkUpdate";
            this.lnkUpdate.Size = new System.Drawing.Size(125, 15);
            this.lnkUpdate.TabIndex = 74;
            this.lnkUpdate.TabStop = true;
            this.lnkUpdate.Text = "| Update this patient |";
            // 
            // PatientSearchList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(463, 283);
            this.Controls.Add(this.lnkUpdate);
            this.Controls.Add(this.lnkCreate);
            this.Name = "PatientSearchList";
            this.Text = "  Patient List";
            this.Controls.SetChildIndex(this.lnkCreate, 0);
            this.Controls.SetChildIndex(this.lnkUpdate, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
