namespace DentalLib
{
    partial class PatientRegistration
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelbottom = new System.Windows.Forms.Panel();
            this.paneltop = new System.Windows.Forms.Panel();
            this.gbxRegistrationDate = new System.Windows.Forms.GroupBox();
            this.lnkChange = new System.Windows.Forms.LinkLabel();
            this.lblDate = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblPatientSysId = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.gbxSysID = new System.Windows.Forms.GroupBox();
            this.lblSysID = new System.Windows.Forms.Label();
            this.gbxRegistrationDate.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.gbxSysID.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelbottom
            // 
            this.panelbottom.BackColor = System.Drawing.Color.Lavender;
            this.panelbottom.Location = new System.Drawing.Point(-1, 248);
            this.panelbottom.Name = "panelbottom";
            this.panelbottom.Size = new System.Drawing.Size(665, 35);
            this.panelbottom.TabIndex = 17;
            // 
            // paneltop
            // 
            this.paneltop.Location = new System.Drawing.Point(-1, -1);
            this.paneltop.Name = "paneltop";
            this.paneltop.Size = new System.Drawing.Size(665, 55);
            this.paneltop.TabIndex = 16;
            // 
            // gbxRegistrationDate
            // 
            this.gbxRegistrationDate.Controls.Add(this.lnkChange);
            this.gbxRegistrationDate.Controls.Add(this.lblDate);
            this.gbxRegistrationDate.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbxRegistrationDate.ForeColor = System.Drawing.Color.Navy;
            this.gbxRegistrationDate.Location = new System.Drawing.Point(276, 169);
            this.gbxRegistrationDate.Name = "gbxRegistrationDate";
            this.gbxRegistrationDate.Size = new System.Drawing.Size(369, 61);
            this.gbxRegistrationDate.TabIndex = 20;
            this.gbxRegistrationDate.TabStop = false;
            this.gbxRegistrationDate.Text = "REGISTRATION DATE";
            // 
            // lnkChange
            // 
            this.lnkChange.AutoSize = true;
            this.lnkChange.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkChange.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.lnkChange.Location = new System.Drawing.Point(321, 44);
            this.lnkChange.Name = "lnkChange";
            this.lnkChange.Size = new System.Drawing.Size(42, 14);
            this.lnkChange.TabIndex = 21;
            this.lnkChange.TabStop = true;
            this.lnkChange.Text = "change";
            // 
            // lblDate
            // 
            this.lblDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDate.ForeColor = System.Drawing.Color.OrangeRed;
            this.lblDate.Location = new System.Drawing.Point(6, 26);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(357, 18);
            this.lblDate.TabIndex = 0;
            this.lblDate.Text = "mmddyy";
            this.lblDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblPatientSysId);
            this.groupBox1.Controls.Add(this.lblName);
            this.groupBox1.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.Navy;
            this.groupBox1.Location = new System.Drawing.Point(12, 67);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(634, 92);
            this.groupBox1.TabIndex = 21;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "PATIENT INFORMATION";
            // 
            // lblPatientSysId
            // 
            this.lblPatientSysId.AutoSize = true;
            this.lblPatientSysId.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPatientSysId.ForeColor = System.Drawing.Color.Chocolate;
            this.lblPatientSysId.Location = new System.Drawing.Point(20, 29);
            this.lblPatientSysId.Name = "lblPatientSysId";
            this.lblPatientSysId.Size = new System.Drawing.Size(106, 16);
            this.lblPatientSysId.TabIndex = 21;
            this.lblPatientSysId.Text = "MM-0000-0000";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.ForeColor = System.Drawing.Color.Black;
            this.lblName.Location = new System.Drawing.Point(19, 50);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(202, 20);
            this.lblName.TabIndex = 22;
            this.lblName.Text = "ITE102 -Judyll\'s Subject";
            // 
            // gbxSysID
            // 
            this.gbxSysID.Controls.Add(this.lblSysID);
            this.gbxSysID.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbxSysID.ForeColor = System.Drawing.Color.Navy;
            this.gbxSysID.Location = new System.Drawing.Point(12, 169);
            this.gbxSysID.Name = "gbxSysID";
            this.gbxSysID.Size = new System.Drawing.Size(258, 61);
            this.gbxSysID.TabIndex = 22;
            this.gbxSysID.TabStop = false;
            this.gbxSysID.Text = "SYSTEM ID";
            // 
            // lblSysID
            // 
            this.lblSysID.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSysID.ForeColor = System.Drawing.Color.Black;
            this.lblSysID.Location = new System.Drawing.Point(6, 26);
            this.lblSysID.Name = "lblSysID";
            this.lblSysID.Size = new System.Drawing.Size(246, 18);
            this.lblSysID.TabIndex = 0;
            this.lblSysID.Text = "Processing...";
            this.lblSysID.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PatientRegistration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.GhostWhite;
            this.ClientSize = new System.Drawing.Size(657, 282);
            this.Controls.Add(this.gbxSysID);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gbxRegistrationDate);
            this.Controls.Add(this.panelbottom);
            this.Controls.Add(this.paneltop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PatientRegistration";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.gbxRegistrationDate.ResumeLayout(false);
            this.gbxRegistrationDate.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gbxSysID.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbxRegistrationDate;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox gbxSysID;
        protected System.Windows.Forms.Label lblSysID;
        protected System.Windows.Forms.Panel panelbottom;
        protected System.Windows.Forms.Panel paneltop;
        protected System.Windows.Forms.Label lblDate;
        protected System.Windows.Forms.LinkLabel lnkChange;
        protected System.Windows.Forms.Label lblPatientSysId;
        protected System.Windows.Forms.Label lblName;
    }
}