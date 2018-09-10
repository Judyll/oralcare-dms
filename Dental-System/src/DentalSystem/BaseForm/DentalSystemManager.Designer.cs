namespace DentalSystem
{
    partial class DentalSystemManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DentalSystemManager));
            this.stsMain = new System.Windows.Forms.StatusStrip();
            this.tslSpace = new System.Windows.Forms.ToolStripStatusLabel();
            this.tslUser = new System.Windows.Forms.ToolStripStatusLabel();
            this.tslTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.ctlMain = new DentalLib.ControlManager();
            this.stsMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // stsMain
            // 
            this.stsMain.BackColor = System.Drawing.SystemColors.Control;
            this.stsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tslSpace,
            this.tslUser,
            this.tslTime});
            this.stsMain.Location = new System.Drawing.Point(0, 712);
            this.stsMain.Name = "stsMain";
            this.stsMain.Size = new System.Drawing.Size(1016, 22);
            this.stsMain.SizingGrip = false;
            this.stsMain.Stretch = false;
            this.stsMain.TabIndex = 2;
            // 
            // tslSpace
            // 
            this.tslSpace.Name = "tslSpace";
            this.tslSpace.Size = new System.Drawing.Size(109, 17);
            this.tslSpace.Text = "toolStripStatusLabel1";
            // 
            // tslUser
            // 
            this.tslUser.Image = ((System.Drawing.Image)(resources.GetObject("tslUser.Image")));
            this.tslUser.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tslUser.Name = "tslUser";
            this.tslUser.Size = new System.Drawing.Size(57, 17);
            this.tslUser.Text = "Sample";
            // 
            // tslTime
            // 
            this.tslTime.Image = ((System.Drawing.Image)(resources.GetObject("tslTime.Image")));
            this.tslTime.Name = "tslTime";
            this.tslTime.Size = new System.Drawing.Size(45, 17);
            this.tslTime.Text = "Time";
            // 
            // ctlMain
            // 
            this.ctlMain.BackColor = System.Drawing.Color.Transparent;
            this.ctlMain.Location = new System.Drawing.Point(700, 104);
            this.ctlMain.Name = "ctlMain";
            this.ctlMain.Size = new System.Drawing.Size(304, 109);
            this.ctlMain.TabIndex = 0;
            // 
            // DentalSystemManager
            // 
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(1016, 734);
            this.ControlBox = false;
            this.Controls.Add(this.stsMain);
            this.Controls.Add(this.ctlMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Name = "DentalSystemManager";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "  Dental Management System";
            this.TopMost = true;
            this.stsMain.ResumeLayout(false);
            this.stsMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DentalLib.ControlManager ctlMain;
        private System.Windows.Forms.StatusStrip stsMain;
        private System.Windows.Forms.ToolStripStatusLabel tslUser;
        private System.Windows.Forms.ToolStripStatusLabel tslSpace;
        private System.Windows.Forms.ToolStripStatusLabel tslTime;
    }
}

