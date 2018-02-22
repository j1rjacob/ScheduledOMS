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
            this.ButtonLatest = new System.Windows.Forms.Button();
            this.ButtonBackup = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ButtonLatest
            // 
            this.ButtonLatest.Location = new System.Drawing.Point(256, 64);
            this.ButtonLatest.Name = "ButtonLatest";
            this.ButtonLatest.Size = new System.Drawing.Size(96, 23);
            this.ButtonLatest.TabIndex = 0;
            this.ButtonLatest.Text = "GetLatest";
            this.ButtonLatest.UseVisualStyleBackColor = true;
            this.ButtonLatest.Click += new System.EventHandler(this.ButtonLatest_Click);
            // 
            // ButtonBackup
            // 
            this.ButtonBackup.Location = new System.Drawing.Point(256, 112);
            this.ButtonBackup.Name = "ButtonBackup";
            this.ButtonBackup.Size = new System.Drawing.Size(96, 23);
            this.ButtonBackup.TabIndex = 1;
            this.ButtonBackup.Text = "Move to Backup";
            this.ButtonBackup.UseVisualStyleBackColor = true;
            this.ButtonBackup.Click += new System.EventHandler(this.ButtonBackup_Click);
            // 
            // FormTaskScheduler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(504, 432);
            this.Controls.Add(this.ButtonBackup);
            this.Controls.Add(this.ButtonLatest);
            this.Name = "FormTaskScheduler";
            this.Text = "OMS Task Scheduler";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button ButtonLatest;
        private System.Windows.Forms.Button ButtonBackup;
    }
}

