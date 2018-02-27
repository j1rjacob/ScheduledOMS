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
            this.ButtonLatest = new System.Windows.Forms.Button();
            this.ButtonBackup = new System.Windows.Forms.Button();
            this.ButtonExport = new System.Windows.Forms.Button();
            this.ButtonMigrate = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ButtonLatest
            // 
            this.ButtonLatest.Location = new System.Drawing.Point(152, 8);
            this.ButtonLatest.Name = "ButtonLatest";
            this.ButtonLatest.Size = new System.Drawing.Size(100, 100);
            this.ButtonLatest.TabIndex = 2;
            this.ButtonLatest.Text = "GetLatest";
            this.ButtonLatest.UseVisualStyleBackColor = true;
            this.ButtonLatest.Click += new System.EventHandler(this.ButtonLatest_Click);
            // 
            // ButtonBackup
            // 
            this.ButtonBackup.Location = new System.Drawing.Point(280, 8);
            this.ButtonBackup.Name = "ButtonBackup";
            this.ButtonBackup.Size = new System.Drawing.Size(100, 100);
            this.ButtonBackup.TabIndex = 3;
            this.ButtonBackup.Text = "Move to Backup";
            this.ButtonBackup.UseVisualStyleBackColor = true;
            this.ButtonBackup.Click += new System.EventHandler(this.ButtonBackup_Click);
            // 
            // ButtonExport
            // 
            this.ButtonExport.Location = new System.Drawing.Point(408, 8);
            this.ButtonExport.Name = "ButtonExport";
            this.ButtonExport.Size = new System.Drawing.Size(100, 100);
            this.ButtonExport.TabIndex = 0;
            this.ButtonExport.Text = "Export  Hydrus Data";
            this.ButtonExport.UseVisualStyleBackColor = true;
            this.ButtonExport.Click += new System.EventHandler(this.ButtonExport_Click);
            // 
            // ButtonMigrate
            // 
            this.ButtonMigrate.Location = new System.Drawing.Point(24, 8);
            this.ButtonMigrate.Name = "ButtonMigrate";
            this.ButtonMigrate.Size = new System.Drawing.Size(100, 100);
            this.ButtonMigrate.TabIndex = 1;
            this.ButtonMigrate.Text = "Migrate CSV to DB";
            this.ButtonMigrate.UseVisualStyleBackColor = true;
            this.ButtonMigrate.Click += new System.EventHandler(this.ButtonMigrate_Click);
            // 
            // FormTaskScheduler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 432);
            this.Controls.Add(this.ButtonExport);
            this.Controls.Add(this.ButtonBackup);
            this.Controls.Add(this.ButtonMigrate);
            this.Controls.Add(this.ButtonLatest);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormTaskScheduler";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "OMS Task Scheduler";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button ButtonLatest;
        private System.Windows.Forms.Button ButtonBackup;
        private System.Windows.Forms.Button ButtonExport;
        private System.Windows.Forms.Button ButtonMigrate;
    }
}

