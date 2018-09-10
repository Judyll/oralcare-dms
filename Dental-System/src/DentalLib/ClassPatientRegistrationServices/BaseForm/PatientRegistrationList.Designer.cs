namespace DentalLib
{
    partial class PatientRegistrationList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PatientRegistrationList));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.paneltop = new System.Windows.Forms.Panel();
            this.panelbottom = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblSysId = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.gbxList = new System.Windows.Forms.GroupBox();
            this.lnkUpdate = new System.Windows.Forms.LinkLabel();
            this.lnkCreate = new System.Windows.Forms.LinkLabel();
            this.lblResult = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvList = new System.Windows.Forms.DataGridView();
            this.pbxPatient = new System.Windows.Forms.PictureBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.optProcedure = new System.Windows.Forms.RadioButton();
            this.optRegistration = new System.Windows.Forms.RadioButton();
            this.lnkPrint = new System.Windows.Forms.LinkLabel();
            this.panelbottom.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.gbxList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxPatient)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // paneltop
            // 
            this.paneltop.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("paneltop.BackgroundImage")));
            this.paneltop.Location = new System.Drawing.Point(-1, -1);
            this.paneltop.Name = "paneltop";
            this.paneltop.Size = new System.Drawing.Size(818, 55);
            this.paneltop.TabIndex = 17;
            // 
            // panelbottom
            // 
            this.panelbottom.BackColor = System.Drawing.Color.Lavender;
            this.panelbottom.Controls.Add(this.btnClose);
            this.panelbottom.Location = new System.Drawing.Point(-1, 642);
            this.panelbottom.Name = "panelbottom";
            this.panelbottom.Size = new System.Drawing.Size(818, 35);
            this.panelbottom.TabIndex = 2;
            // 
            // btnClose
            // 
            this.btnClose.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClose.BackgroundImage")));
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(710, 5);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(86, 23);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Close";
            this.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblSysId);
            this.groupBox1.Controls.Add(this.lblName);
            this.groupBox1.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.Navy;
            this.groupBox1.Location = new System.Drawing.Point(12, 61);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(634, 75);
            this.groupBox1.TabIndex = 20;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "PATIENT INFORMATION";
            // 
            // lblSysId
            // 
            this.lblSysId.AutoSize = true;
            this.lblSysId.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSysId.ForeColor = System.Drawing.Color.Chocolate;
            this.lblSysId.Location = new System.Drawing.Point(20, 20);
            this.lblSysId.Name = "lblSysId";
            this.lblSysId.Size = new System.Drawing.Size(106, 16);
            this.lblSysId.TabIndex = 21;
            this.lblSysId.Text = "MM-0000-0000";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.ForeColor = System.Drawing.Color.Black;
            this.lblName.Location = new System.Drawing.Point(19, 41);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(202, 20);
            this.lblName.TabIndex = 22;
            this.lblName.Text = "ITE102 -Judyll\'s Subject";
            // 
            // gbxList
            // 
            this.gbxList.Controls.Add(this.lnkPrint);
            this.gbxList.Controls.Add(this.lnkUpdate);
            this.gbxList.Controls.Add(this.lnkCreate);
            this.gbxList.Controls.Add(this.lblResult);
            this.gbxList.Controls.Add(this.label1);
            this.gbxList.Controls.Add(this.dgvList);
            this.gbxList.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbxList.ForeColor = System.Drawing.Color.Navy;
            this.gbxList.Location = new System.Drawing.Point(12, 210);
            this.gbxList.Name = "gbxList";
            this.gbxList.Size = new System.Drawing.Size(783, 416);
            this.gbxList.TabIndex = 1;
            this.gbxList.TabStop = false;
            this.gbxList.Text = "REGISTRATION LIST";
            // 
            // lnkUpdate
            // 
            this.lnkUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lnkUpdate.AutoSize = true;
            this.lnkUpdate.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkUpdate.ForeColor = System.Drawing.Color.Red;
            this.lnkUpdate.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.lnkUpdate.LinkColor = System.Drawing.Color.MidnightBlue;
            this.lnkUpdate.Location = new System.Drawing.Point(539, 394);
            this.lnkUpdate.Name = "lnkUpdate";
            this.lnkUpdate.Size = new System.Drawing.Size(107, 15);
            this.lnkUpdate.TabIndex = 0;
            this.lnkUpdate.TabStop = true;
            this.lnkUpdate.Text = "| Update Existing |";
            // 
            // lnkCreate
            // 
            this.lnkCreate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lnkCreate.AutoSize = true;
            this.lnkCreate.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkCreate.ForeColor = System.Drawing.Color.Red;
            this.lnkCreate.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.lnkCreate.LinkColor = System.Drawing.Color.MidnightBlue;
            this.lnkCreate.Location = new System.Drawing.Point(652, 394);
            this.lnkCreate.Name = "lnkCreate";
            this.lnkCreate.Size = new System.Drawing.Size(116, 15);
            this.lnkCreate.TabIndex = 1;
            this.lnkCreate.TabStop = true;
            this.lnkCreate.Text = "| New Registration |";
            // 
            // lblResult
            // 
            this.lblResult.AutoSize = true;
            this.lblResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblResult.ForeColor = System.Drawing.Color.Maroon;
            this.lblResult.Location = new System.Drawing.Point(50, 20);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(47, 13);
            this.lblResult.TabIndex = 63;
            this.lblResult.Text = "Result:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Maroon;
            this.label1.Location = new System.Drawing.Point(7, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 62;
            this.label1.Text = "Query:";
            // 
            // dgvList
            // 
            this.dgvList.AllowUserToAddRows = false;
            this.dgvList.AllowUserToDeleteRows = false;
            this.dgvList.AllowUserToResizeColumns = false;
            this.dgvList.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dgvList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvList.BackgroundColor = System.Drawing.Color.Silver;
            this.dgvList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvList.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.DarkSlateBlue;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvList.Location = new System.Drawing.Point(10, 37);
            this.dgvList.Name = "dgvList";
            this.dgvList.RowHeadersVisible = false;
            this.dgvList.RowHeadersWidth = 15;
            this.dgvList.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.Lavender;
            this.dgvList.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvList.Size = new System.Drawing.Size(758, 354);
            this.dgvList.TabIndex = 61;
            // 
            // pbxPatient
            // 
            this.pbxPatient.BackColor = System.Drawing.Color.DarkSlateGray;
            this.pbxPatient.Cursor = System.Windows.Forms.Cursors.Default;
            this.pbxPatient.Location = new System.Drawing.Point(675, 12);
            this.pbxPatient.Name = "pbxPatient";
            this.pbxPatient.Size = new System.Drawing.Size(120, 126);
            this.pbxPatient.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbxPatient.TabIndex = 22;
            this.pbxPatient.TabStop = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.optProcedure);
            this.groupBox3.Controls.Add(this.optRegistration);
            this.groupBox3.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.ForeColor = System.Drawing.Color.Navy;
            this.groupBox3.Location = new System.Drawing.Point(12, 143);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(634, 59);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "VIEW OPTIONS";
            // 
            // optProcedure
            // 
            this.optProcedure.AutoSize = true;
            this.optProcedure.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optProcedure.ForeColor = System.Drawing.Color.Chocolate;
            this.optProcedure.Location = new System.Drawing.Point(447, 22);
            this.optProcedure.Name = "optProcedure";
            this.optProcedure.Size = new System.Drawing.Size(170, 23);
            this.optProcedure.TabIndex = 1;
            this.optProcedure.TabStop = true;
            this.optProcedure.Text = "By Procedures Taken";
            this.optProcedure.UseVisualStyleBackColor = true;
            // 
            // optRegistration
            // 
            this.optRegistration.AutoSize = true;
            this.optRegistration.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optRegistration.ForeColor = System.Drawing.Color.Chocolate;
            this.optRegistration.Location = new System.Drawing.Point(263, 22);
            this.optRegistration.Name = "optRegistration";
            this.optRegistration.Size = new System.Drawing.Size(158, 23);
            this.optRegistration.TabIndex = 0;
            this.optRegistration.TabStop = true;
            this.optRegistration.Text = "By Registration List";
            this.optRegistration.UseVisualStyleBackColor = true;
            // 
            // lnkPrint
            // 
            this.lnkPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lnkPrint.AutoSize = true;
            this.lnkPrint.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkPrint.ForeColor = System.Drawing.Color.Red;
            this.lnkPrint.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.lnkPrint.LinkColor = System.Drawing.Color.MidnightBlue;
            this.lnkPrint.Location = new System.Drawing.Point(7, 394);
            this.lnkPrint.Name = "lnkPrint";
            this.lnkPrint.Size = new System.Drawing.Size(70, 15);
            this.lnkPrint.TabIndex = 64;
            this.lnkPrint.TabStop = true;
            this.lnkPrint.Text = "| Print List |";
            // 
            // PatientRegistrationList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.GhostWhite;
            this.ClientSize = new System.Drawing.Size(807, 676);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.pbxPatient);
            this.Controls.Add(this.gbxList);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panelbottom);
            this.Controls.Add(this.paneltop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PatientRegistrationList";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.panelbottom.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gbxList.ResumeLayout(false);
            this.gbxList.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxPatient)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel paneltop;
        private System.Windows.Forms.Panel panelbottom;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblSysId;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.GroupBox gbxList;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.Label label1;
        protected System.Windows.Forms.DataGridView dgvList;
        private System.Windows.Forms.LinkLabel lnkUpdate;
        private System.Windows.Forms.LinkLabel lnkCreate;
        protected System.Windows.Forms.PictureBox pbxPatient;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton optProcedure;
        private System.Windows.Forms.RadioButton optRegistration;
        private System.Windows.Forms.LinkLabel lnkPrint;
    }
}