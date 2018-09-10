using System;
using System.Collections.Generic;
using System.Text;

namespace DentalLib
{
    public partial class ControlManager: AnimatedPanel
    {
        protected System.Windows.Forms.PictureBox pictureBox3;
        protected System.Windows.Forms.PictureBox pbxClose;
        protected System.Windows.Forms.PictureBox pbxUsers;
        protected System.Windows.Forms.PictureBox pbxSales;
        protected System.Windows.Forms.PictureBox pbxAccountsReceivables;
        protected System.Windows.Forms.PictureBox pbxProcedures;
        protected System.Windows.Forms.PictureBox pbxCash;
        protected System.Windows.Forms.TextBox txtSearch;
    
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ControlManager));
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pbxClose = new System.Windows.Forms.PictureBox();
            this.pbxUsers = new System.Windows.Forms.PictureBox();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.pbxProcedures = new System.Windows.Forms.PictureBox();
            this.pbxAccountsReceivables = new System.Windows.Forms.PictureBox();
            this.pbxSales = new System.Windows.Forms.PictureBox();
            this.pbxCash = new System.Windows.Forms.PictureBox();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxUsers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxProcedures)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxAccountsReceivables)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxSales)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxCash)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlHeader.BackgroundImage")));
            this.pnlHeader.Size = new System.Drawing.Size(310, 28);
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.pbxClose);
            this.pnlMain.Controls.Add(this.txtSearch);
            this.pnlMain.Controls.Add(this.pictureBox3);
            this.pnlMain.Controls.Add(this.pbxUsers);
            this.pnlMain.Controls.Add(this.pbxSales);
            this.pnlMain.Controls.Add(this.pbxAccountsReceivables);
            this.pnlMain.Controls.Add(this.pbxProcedures);
            this.pnlMain.Controls.Add(this.pbxCash);
            this.pnlMain.Size = new System.Drawing.Size(304, 109);
            this.pnlMain.Controls.SetChildIndex(this.pbxCash, 0);
            this.pnlMain.Controls.SetChildIndex(this.pbxProcedures, 0);
            this.pnlMain.Controls.SetChildIndex(this.pbxAccountsReceivables, 0);
            this.pnlMain.Controls.SetChildIndex(this.pbxSales, 0);
            this.pnlMain.Controls.SetChildIndex(this.pbxUsers, 0);
            this.pnlMain.Controls.SetChildIndex(this.pictureBox3, 0);
            this.pnlMain.Controls.SetChildIndex(this.txtSearch, 0);
            this.pnlMain.Controls.SetChildIndex(this.pbxClose, 0);
            this.pnlMain.Controls.SetChildIndex(this.pnlHeader, 0);
            // 
            // pictureBox3
            // 
            this.pictureBox3.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.pictureBox3.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(3, 53);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(50, 53);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox3.TabIndex = 11;
            this.pictureBox3.TabStop = false;
            // 
            // pbxClose
            // 
            this.pbxClose.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.pbxClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pbxClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbxClose.Image = ((System.Drawing.Image)(resources.GetObject("pbxClose.Image")));
            this.pbxClose.Location = new System.Drawing.Point(271, 38);
            this.pbxClose.Name = "pbxClose";
            this.pbxClose.Size = new System.Drawing.Size(26, 28);
            this.pbxClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbxClose.TabIndex = 9;
            this.pbxClose.TabStop = false;
            // 
            // pbxUsers
            // 
            this.pbxUsers.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.pbxUsers.BackColor = System.Drawing.Color.Transparent;
            this.pbxUsers.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pbxUsers.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbxUsers.Image = ((System.Drawing.Image)(resources.GetObject("pbxUsers.Image")));
            this.pbxUsers.Location = new System.Drawing.Point(239, 38);
            this.pbxUsers.Name = "pbxUsers";
            this.pbxUsers.Size = new System.Drawing.Size(26, 28);
            this.pbxUsers.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbxUsers.TabIndex = 10;
            this.pbxUsers.TabStop = false;
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.txtSearch.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            this.txtSearch.Font = new System.Drawing.Font("MS Reference Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.Location = new System.Drawing.Point(36, 72);
            this.txtSearch.MaxLength = 200;
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(264, 27);
            this.txtSearch.TabIndex = 8;
            // 
            // pbxProcedures
            // 
            this.pbxProcedures.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.pbxProcedures.BackColor = System.Drawing.Color.Transparent;
            this.pbxProcedures.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pbxProcedures.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbxProcedures.Image = ((System.Drawing.Image)(resources.GetObject("pbxProcedures.Image")));
            this.pbxProcedures.Location = new System.Drawing.Point(207, 38);
            this.pbxProcedures.Name = "pbxProcedures";
            this.pbxProcedures.Size = new System.Drawing.Size(26, 28);
            this.pbxProcedures.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbxProcedures.TabIndex = 12;
            this.pbxProcedures.TabStop = false;
            // 
            // pbxAccountsReceivables
            // 
            this.pbxAccountsReceivables.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.pbxAccountsReceivables.BackColor = System.Drawing.Color.Transparent;
            this.pbxAccountsReceivables.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pbxAccountsReceivables.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbxAccountsReceivables.Image = ((System.Drawing.Image)(resources.GetObject("pbxAccountsReceivables.Image")));
            this.pbxAccountsReceivables.Location = new System.Drawing.Point(175, 38);
            this.pbxAccountsReceivables.Name = "pbxAccountsReceivables";
            this.pbxAccountsReceivables.Size = new System.Drawing.Size(26, 28);
            this.pbxAccountsReceivables.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbxAccountsReceivables.TabIndex = 13;
            this.pbxAccountsReceivables.TabStop = false;
            // 
            // pbxSales
            // 
            this.pbxSales.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.pbxSales.BackColor = System.Drawing.Color.Transparent;
            this.pbxSales.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pbxSales.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbxSales.Image = ((System.Drawing.Image)(resources.GetObject("pbxSales.Image")));
            this.pbxSales.Location = new System.Drawing.Point(143, 38);
            this.pbxSales.Name = "pbxSales";
            this.pbxSales.Size = new System.Drawing.Size(26, 28);
            this.pbxSales.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbxSales.TabIndex = 14;
            this.pbxSales.TabStop = false;
            // 
            // pbxCash
            // 
            this.pbxCash.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.pbxCash.BackColor = System.Drawing.Color.Transparent;
            this.pbxCash.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pbxCash.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbxCash.Image = ((System.Drawing.Image)(resources.GetObject("pbxCash.Image")));
            this.pbxCash.Location = new System.Drawing.Point(111, 38);
            this.pbxCash.Name = "pbxCash";
            this.pbxCash.Size = new System.Drawing.Size(26, 28);
            this.pbxCash.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbxCash.TabIndex = 15;
            this.pbxCash.TabStop = false;
            // 
            // ControlManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Name = "ControlManager";
            this.Size = new System.Drawing.Size(304, 109);
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxUsers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxProcedures)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxAccountsReceivables)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxSales)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxCash)).EndInit();
            this.ResumeLayout(false);

        }        
        
    }
}
