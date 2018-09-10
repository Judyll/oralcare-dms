namespace DentalLib
{
    partial class PatientCharges
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PatientCharges));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.gbxProcedure = new System.Windows.Forms.GroupBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.lblProcedureId = new System.Windows.Forms.Label();
            this.lblProcedureName = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtToothNo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtRemarks = new System.Windows.Forms.TextBox();
            this.lblRemarks = new System.Windows.Forms.Label();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.lblAmount = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblSysId = new System.Windows.Forms.Label();
            this.gbxRegistrationDate = new System.Windows.Forms.GroupBox();
            this.lnkChange = new System.Windows.Forms.LinkLabel();
            this.lblDate = new System.Windows.Forms.Label();
            this.gbxProcedure.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.gbxRegistrationDate.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(-1, -1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(658, 55);
            this.panel1.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Lavender;
            this.panel2.Location = new System.Drawing.Point(0, 422);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(657, 35);
            this.panel2.TabIndex = 22;
            // 
            // gbxProcedure
            // 
            this.gbxProcedure.Controls.Add(this.btnSearch);
            this.gbxProcedure.Controls.Add(this.lblProcedureId);
            this.gbxProcedure.Controls.Add(this.lblProcedureName);
            this.gbxProcedure.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbxProcedure.ForeColor = System.Drawing.Color.Navy;
            this.gbxProcedure.Location = new System.Drawing.Point(12, 138);
            this.gbxProcedure.Name = "gbxProcedure";
            this.gbxProcedure.Size = new System.Drawing.Size(634, 96);
            this.gbxProcedure.TabIndex = 1;
            this.gbxProcedure.TabStop = false;
            this.gbxProcedure.Text = "PROCEDURE";
            // 
            // btnSearch
            // 
            this.btnSearch.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.BackgroundImage")));
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.ForeColor = System.Drawing.Color.Transparent;
            this.btnSearch.Location = new System.Drawing.Point(603, 12);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(25, 25);
            this.btnSearch.TabIndex = 0;
            this.btnSearch.UseVisualStyleBackColor = true;
            // 
            // lblProcedureId
            // 
            this.lblProcedureId.AutoSize = true;
            this.lblProcedureId.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProcedureId.ForeColor = System.Drawing.Color.Chocolate;
            this.lblProcedureId.Location = new System.Drawing.Point(13, 33);
            this.lblProcedureId.Name = "lblProcedureId";
            this.lblProcedureId.Size = new System.Drawing.Size(18, 16);
            this.lblProcedureId.TabIndex = 26;
            this.lblProcedureId.Text = "--";
            // 
            // lblProcedureName
            // 
            this.lblProcedureName.AutoSize = true;
            this.lblProcedureName.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProcedureName.ForeColor = System.Drawing.Color.Black;
            this.lblProcedureName.Location = new System.Drawing.Point(12, 51);
            this.lblProcedureName.Name = "lblProcedureName";
            this.lblProcedureName.Size = new System.Drawing.Size(204, 18);
            this.lblProcedureName.TabIndex = 27;
            this.lblProcedureName.Text = "Please select a procedure";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtToothNo);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.txtRemarks);
            this.groupBox3.Controls.Add(this.lblRemarks);
            this.groupBox3.Controls.Add(this.txtAmount);
            this.groupBox3.Controls.Add(this.lblAmount);
            this.groupBox3.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.ForeColor = System.Drawing.Color.Navy;
            this.groupBox3.Location = new System.Drawing.Point(12, 240);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(634, 164);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "DETAILS";
            // 
            // txtToothNo
            // 
            this.txtToothNo.BackColor = System.Drawing.Color.White;
            this.txtToothNo.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtToothNo.ForeColor = System.Drawing.Color.Black;
            this.txtToothNo.Location = new System.Drawing.Point(88, 23);
            this.txtToothNo.MaxLength = 50;
            this.txtToothNo.Name = "txtToothNo";
            this.txtToothNo.Size = new System.Drawing.Size(275, 31);
            this.txtToothNo.TabIndex = 0;
            this.txtToothNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(13, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 18);
            this.label1.TabIndex = 33;
            this.label1.Text = "Tooth No.";
            // 
            // txtRemarks
            // 
            this.txtRemarks.BackColor = System.Drawing.Color.White;
            this.txtRemarks.Location = new System.Drawing.Point(88, 59);
            this.txtRemarks.MaxLength = 1000;
            this.txtRemarks.Multiline = true;
            this.txtRemarks.Name = "txtRemarks";
            this.txtRemarks.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtRemarks.Size = new System.Drawing.Size(275, 99);
            this.txtRemarks.TabIndex = 2;
            // 
            // lblRemarks
            // 
            this.lblRemarks.AutoSize = true;
            this.lblRemarks.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRemarks.ForeColor = System.Drawing.Color.Black;
            this.lblRemarks.Location = new System.Drawing.Point(21, 59);
            this.lblRemarks.Name = "lblRemarks";
            this.lblRemarks.Size = new System.Drawing.Size(61, 18);
            this.lblRemarks.TabIndex = 31;
            this.lblRemarks.Text = "Remarks";
            // 
            // txtAmount
            // 
            this.txtAmount.BackColor = System.Drawing.Color.White;
            this.txtAmount.Font = new System.Drawing.Font("Calibri", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAmount.ForeColor = System.Drawing.Color.Red;
            this.txtAmount.Location = new System.Drawing.Point(449, 23);
            this.txtAmount.MaxLength = 15;
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(169, 40);
            this.txtAmount.TabIndex = 1;
            this.txtAmount.Text = "00.00";
            this.txtAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblAmount
            // 
            this.lblAmount.AutoSize = true;
            this.lblAmount.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAmount.ForeColor = System.Drawing.Color.Black;
            this.lblAmount.Location = new System.Drawing.Point(385, 23);
            this.lblAmount.Name = "lblAmount";
            this.lblAmount.Size = new System.Drawing.Size(58, 18);
            this.lblAmount.TabIndex = 29;
            this.lblAmount.Text = "Amount";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblSysId);
            this.groupBox1.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.Navy;
            this.groupBox1.Location = new System.Drawing.Point(13, 71);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(258, 61);
            this.groupBox1.TabIndex = 27;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "SYSTEM ID";
            // 
            // lblSysId
            // 
            this.lblSysId.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSysId.ForeColor = System.Drawing.Color.Black;
            this.lblSysId.Location = new System.Drawing.Point(6, 26);
            this.lblSysId.Name = "lblSysId";
            this.lblSysId.Size = new System.Drawing.Size(246, 18);
            this.lblSysId.TabIndex = 0;
            this.lblSysId.Text = "Processing...";
            this.lblSysId.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gbxRegistrationDate
            // 
            this.gbxRegistrationDate.Controls.Add(this.lnkChange);
            this.gbxRegistrationDate.Controls.Add(this.lblDate);
            this.gbxRegistrationDate.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbxRegistrationDate.ForeColor = System.Drawing.Color.Navy;
            this.gbxRegistrationDate.Location = new System.Drawing.Point(277, 71);
            this.gbxRegistrationDate.Name = "gbxRegistrationDate";
            this.gbxRegistrationDate.Size = new System.Drawing.Size(369, 61);
            this.gbxRegistrationDate.TabIndex = 0;
            this.gbxRegistrationDate.TabStop = false;
            this.gbxRegistrationDate.Text = "DATE ADMINISTERED";
            // 
            // lnkChange
            // 
            this.lnkChange.AutoSize = true;
            this.lnkChange.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkChange.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.lnkChange.Location = new System.Drawing.Point(321, 44);
            this.lnkChange.Name = "lnkChange";
            this.lnkChange.Size = new System.Drawing.Size(42, 14);
            this.lnkChange.TabIndex = 0;
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
            this.lblDate.TabIndex = 1;
            this.lblDate.Text = "mmddyy";
            this.lblDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PatientCharges
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.GhostWhite;
            this.ClientSize = new System.Drawing.Size(652, 454);
            this.Controls.Add(this.gbxRegistrationDate);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.gbxProcedure);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PatientCharges";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.gbxProcedure.ResumeLayout(false);
            this.gbxProcedure.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.gbxRegistrationDate.ResumeLayout(false);
            this.gbxRegistrationDate.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbxProcedure;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label lblRemarks;
        private System.Windows.Forms.Label lblAmount;
        private System.Windows.Forms.GroupBox groupBox1;
        protected System.Windows.Forms.Label lblSysId;
        private System.Windows.Forms.GroupBox gbxRegistrationDate;
        protected System.Windows.Forms.LinkLabel lnkChange;
        protected System.Windows.Forms.Label lblDate;
        protected System.Windows.Forms.Panel panel1;
        protected System.Windows.Forms.Panel panel2;
        protected System.Windows.Forms.Label lblProcedureId;
        protected System.Windows.Forms.Label lblProcedureName;
        protected System.Windows.Forms.TextBox txtRemarks;
        protected System.Windows.Forms.TextBox txtAmount;
        protected System.Windows.Forms.Button btnSearch;
        protected System.Windows.Forms.TextBox txtToothNo;
        private System.Windows.Forms.Label label1;
    }
}