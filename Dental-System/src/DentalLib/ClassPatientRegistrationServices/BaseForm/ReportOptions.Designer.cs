namespace DentalLib
{
    partial class ReportOptions
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReportOptions));
            this.panel1 = new System.Windows.Forms.Panel();
            this.gbxRegistrationDate = new System.Windows.Forms.GroupBox();
            this.lnkChangeDateFrom = new System.Windows.Forms.LinkLabel();
            this.lblDateFrom = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lnkChangeDateTo = new System.Windows.Forms.LinkLabel();
            this.lblDateTo = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnApply = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.gbxRegistrationDate.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(-1, -1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(775, 55);
            this.panel1.TabIndex = 2;
            // 
            // gbxRegistrationDate
            // 
            this.gbxRegistrationDate.Controls.Add(this.lnkChangeDateFrom);
            this.gbxRegistrationDate.Controls.Add(this.lblDateFrom);
            this.gbxRegistrationDate.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbxRegistrationDate.ForeColor = System.Drawing.Color.Navy;
            this.gbxRegistrationDate.Location = new System.Drawing.Point(12, 60);
            this.gbxRegistrationDate.Name = "gbxRegistrationDate";
            this.gbxRegistrationDate.Size = new System.Drawing.Size(369, 61);
            this.gbxRegistrationDate.TabIndex = 3;
            this.gbxRegistrationDate.TabStop = false;
            this.gbxRegistrationDate.Text = "DATE FROM";
            // 
            // lnkChangeDateFrom
            // 
            this.lnkChangeDateFrom.AutoSize = true;
            this.lnkChangeDateFrom.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkChangeDateFrom.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.lnkChangeDateFrom.Location = new System.Drawing.Point(321, 44);
            this.lnkChangeDateFrom.Name = "lnkChangeDateFrom";
            this.lnkChangeDateFrom.Size = new System.Drawing.Size(42, 14);
            this.lnkChangeDateFrom.TabIndex = 0;
            this.lnkChangeDateFrom.TabStop = true;
            this.lnkChangeDateFrom.Text = "change";
            // 
            // lblDateFrom
            // 
            this.lblDateFrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDateFrom.ForeColor = System.Drawing.Color.OrangeRed;
            this.lblDateFrom.Location = new System.Drawing.Point(6, 26);
            this.lblDateFrom.Name = "lblDateFrom";
            this.lblDateFrom.Size = new System.Drawing.Size(357, 18);
            this.lblDateFrom.TabIndex = 1;
            this.lblDateFrom.Text = "mmddyy";
            this.lblDateFrom.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lnkChangeDateTo);
            this.groupBox1.Controls.Add(this.lblDateTo);
            this.groupBox1.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.Navy;
            this.groupBox1.Location = new System.Drawing.Point(387, 60);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(369, 61);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "DATE TO";
            // 
            // lnkChangeDateTo
            // 
            this.lnkChangeDateTo.AutoSize = true;
            this.lnkChangeDateTo.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkChangeDateTo.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.lnkChangeDateTo.Location = new System.Drawing.Point(321, 44);
            this.lnkChangeDateTo.Name = "lnkChangeDateTo";
            this.lnkChangeDateTo.Size = new System.Drawing.Size(42, 14);
            this.lnkChangeDateTo.TabIndex = 0;
            this.lnkChangeDateTo.TabStop = true;
            this.lnkChangeDateTo.Text = "change";
            // 
            // lblDateTo
            // 
            this.lblDateTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDateTo.ForeColor = System.Drawing.Color.OrangeRed;
            this.lblDateTo.Location = new System.Drawing.Point(6, 26);
            this.lblDateTo.Name = "lblDateTo";
            this.lblDateTo.Size = new System.Drawing.Size(357, 18);
            this.lblDateTo.TabIndex = 1;
            this.lblDateTo.Text = "mmddyy";
            this.lblDateTo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.panel2.BackColor = System.Drawing.Color.Lavender;
            this.panel2.Controls.Add(this.btnApply);
            this.panel2.Controls.Add(this.btnCancel);
            this.panel2.Location = new System.Drawing.Point(-1, 137);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(775, 35);
            this.panel2.TabIndex = 23;
            // 
            // btnApply
            // 
            this.btnApply.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnApply.BackgroundImage")));
            this.btnApply.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnApply.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnApply.Location = new System.Drawing.Point(579, 6);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(86, 23);
            this.btnApply.TabIndex = 6;
            this.btnApply.Text = "  Apply";
            this.btnApply.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnApply.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCancel.BackgroundImage")));
            this.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(671, 6);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(86, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "  Cancel";
            this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // ReportOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.GhostWhite;
            this.ClientSize = new System.Drawing.Size(768, 171);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gbxRegistrationDate);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ReportOptions";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.gbxRegistrationDate.ResumeLayout(false);
            this.gbxRegistrationDate.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox gbxRegistrationDate;
        protected System.Windows.Forms.LinkLabel lnkChangeDateFrom;
        protected System.Windows.Forms.Label lblDateFrom;
        private System.Windows.Forms.GroupBox groupBox1;
        protected System.Windows.Forms.LinkLabel lnkChangeDateTo;
        protected System.Windows.Forms.Label lblDateTo;
        protected System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnCancel;
        protected System.Windows.Forms.Button btnApply;
    }
}