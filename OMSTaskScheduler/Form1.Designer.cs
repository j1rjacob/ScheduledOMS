namespace OMSTaskScheduler
{
    partial class FormTaskScheduler
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormTaskScheduler));
            this.richTextBoxDebug = new System.Windows.Forms.RichTextBox();
            this.metroCheckBoxAuto = new MetroFramework.Controls.MetroCheckBox();
            this.metroTileExport = new MetroFramework.Controls.MetroTile();
            this.metroTileCopy = new MetroFramework.Controls.MetroTile();
            this.metroTileLatest = new MetroFramework.Controls.MetroTile();
            this.metroTileBackup = new MetroFramework.Controls.MetroTile();
            this.SuspendLayout();
            // 
            // richTextBoxDebug
            // 
            this.richTextBoxDebug.BackColor = System.Drawing.SystemColors.MenuText;
            this.richTextBoxDebug.ForeColor = System.Drawing.SystemColors.Window;
            this.richTextBoxDebug.Location = new System.Drawing.Point(8, 88);
            this.richTextBoxDebug.Name = "richTextBoxDebug";
            this.richTextBoxDebug.Size = new System.Drawing.Size(424, 360);
            this.richTextBoxDebug.TabIndex = 5;
            this.richTextBoxDebug.Text = "";
            // 
            // metroCheckBoxAuto
            // 
            this.metroCheckBoxAuto.AutoSize = true;
            this.metroCheckBoxAuto.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.metroCheckBoxAuto.FontSize = MetroFramework.MetroCheckBoxSize.Tall;
            this.metroCheckBoxAuto.ForeColor = System.Drawing.Color.Green;
            this.metroCheckBoxAuto.Location = new System.Drawing.Point(624, 56);
            this.metroCheckBoxAuto.Name = "metroCheckBoxAuto";
            this.metroCheckBoxAuto.Size = new System.Drawing.Size(116, 25);
            this.metroCheckBoxAuto.TabIndex = 6;
            this.metroCheckBoxAuto.Text = "Allow Auto";
            this.metroCheckBoxAuto.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.metroCheckBoxAuto.UseSelectable = true;
            this.metroCheckBoxAuto.CheckedChanged += new System.EventHandler(this.metroCheckBoxAuto_CheckedChanged);
            // 
            // metroTileExport
            // 
            this.metroTileExport.ActiveControl = null;
            this.metroTileExport.Location = new System.Drawing.Point(600, 88);
            this.metroTileExport.Name = "metroTileExport";
            this.metroTileExport.Size = new System.Drawing.Size(144, 360);
            this.metroTileExport.TabIndex = 7;
            this.metroTileExport.Text = "Export Hydrus Data";
            this.metroTileExport.UseSelectable = true;
            this.metroTileExport.Click += new System.EventHandler(this.metroTileExport_Click);
            // 
            // metroTileCopy
            // 
            this.metroTileCopy.ActiveControl = null;
            this.metroTileCopy.Location = new System.Drawing.Point(442, 88);
            this.metroTileCopy.Name = "metroTileCopy";
            this.metroTileCopy.Size = new System.Drawing.Size(144, 111);
            this.metroTileCopy.TabIndex = 8;
            this.metroTileCopy.Text = "Copy 2 Database";
            this.metroTileCopy.UseSelectable = true;
            this.metroTileCopy.Click += new System.EventHandler(this.metroTileCopy_Click);
            // 
            // metroTileLatest
            // 
            this.metroTileLatest.ActiveControl = null;
            this.metroTileLatest.Location = new System.Drawing.Point(442, 216);
            this.metroTileLatest.Name = "metroTileLatest";
            this.metroTileLatest.Size = new System.Drawing.Size(144, 111);
            this.metroTileLatest.TabIndex = 9;
            this.metroTileLatest.Text = "Get Latest";
            this.metroTileLatest.UseSelectable = true;
            this.metroTileLatest.Click += new System.EventHandler(this.metroTileLatest_Click);
            // 
            // metroTileBackup
            // 
            this.metroTileBackup.ActiveControl = null;
            this.metroTileBackup.Location = new System.Drawing.Point(442, 336);
            this.metroTileBackup.Name = "metroTileBackup";
            this.metroTileBackup.Size = new System.Drawing.Size(144, 111);
            this.metroTileBackup.TabIndex = 10;
            this.metroTileBackup.Text = "Move 2 Backup";
            this.metroTileBackup.UseSelectable = true;
            this.metroTileBackup.Click += new System.EventHandler(this.metroTileBackup_Click);
            // 
            // FormTaskScheduler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(754, 461);
            this.Controls.Add(this.metroTileBackup);
            this.Controls.Add(this.metroTileLatest);
            this.Controls.Add(this.metroTileCopy);
            this.Controls.Add(this.metroTileExport);
            this.Controls.Add(this.metroCheckBoxAuto);
            this.Controls.Add(this.richTextBoxDebug);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormTaskScheduler";
            this.Opacity = 0.9D;
            this.Resizable = false;
            this.Text = "OMS Task Scheduler";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private MetroFramework.Controls.MetroCheckBox metroCheckBoxAuto;
        private MetroFramework.Controls.MetroTile metroTileExport;
        private MetroFramework.Controls.MetroTile metroTileCopy;
        private MetroFramework.Controls.MetroTile metroTileLatest;
        private MetroFramework.Controls.MetroTile metroTileBackup;
        private System.Windows.Forms.RichTextBox richTextBoxDebug;
    }
}

