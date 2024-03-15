namespace CEETimerCSharpWinForms.Forms
{
    partial class FormAbout
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
            this.LabelInfo = new System.Windows.Forms.Label();
            this.LabelAuthor = new System.Windows.Forms.Label();
            this.ButtonGitHub = new System.Windows.Forms.Button();
            this.ButtonClose = new System.Windows.Forms.Button();
            this.ButtonLicense = new System.Windows.Forms.Button();
            this.LabelLicense = new System.Windows.Forms.Label();
            this.LableVersion = new System.Windows.Forms.Label();
            this.PanelMain = new System.Windows.Forms.Panel();
            this.PanelMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // LabelInfo
            // 
            this.LabelInfo.AutoSize = true;
            this.LabelInfo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelInfo.Location = new System.Drawing.Point(3, 3);
            this.LabelInfo.Name = "LabelInfo";
            this.LabelInfo.Size = new System.Drawing.Size(217, 15);
            this.LabelInfo.TabIndex = 1;
            this.LabelInfo.Text = "CEETimerCSharpWinForms 高考倒计时";
            // 
            // LabelAuthor
            // 
            this.LabelAuthor.AutoSize = true;
            this.LabelAuthor.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelAuthor.Location = new System.Drawing.Point(3, 48);
            this.LabelAuthor.Name = "LabelAuthor";
            this.LabelAuthor.Size = new System.Drawing.Size(0, 15);
            this.LabelAuthor.TabIndex = 3;
            // 
            // ButtonGitHub
            // 
            this.ButtonGitHub.AutoSize = true;
            this.ButtonGitHub.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ButtonGitHub.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonGitHub.Location = new System.Drawing.Point(14, 66);
            this.ButtonGitHub.Name = "ButtonGitHub";
            this.ButtonGitHub.Size = new System.Drawing.Size(85, 25);
            this.ButtonGitHub.TabIndex = 4;
            this.ButtonGitHub.Text = "项目主页(&G)";
            this.ButtonGitHub.UseVisualStyleBackColor = true;
            this.ButtonGitHub.Click += new System.EventHandler(this.ButtonGitHub_Click);
            // 
            // ButtonClose
            // 
            this.ButtonClose.AutoSize = true;
            this.ButtonClose.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ButtonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.ButtonClose.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonClose.Location = new System.Drawing.Point(181, 66);
            this.ButtonClose.Name = "ButtonClose";
            this.ButtonClose.Size = new System.Drawing.Size(59, 25);
            this.ButtonClose.TabIndex = 5;
            this.ButtonClose.Text = "关闭(&C)";
            this.ButtonClose.UseVisualStyleBackColor = true;
            this.ButtonClose.Click += new System.EventHandler(this.ButtonClose_Click);
            // 
            // ButtonLicense
            // 
            this.ButtonLicense.AutoSize = true;
            this.ButtonLicense.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ButtonLicense.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonLicense.Location = new System.Drawing.Point(105, 66);
            this.ButtonLicense.Name = "ButtonLicense";
            this.ButtonLicense.Size = new System.Drawing.Size(70, 25);
            this.ButtonLicense.TabIndex = 6;
            this.ButtonLicense.Text = "许可证(&L)";
            this.ButtonLicense.UseVisualStyleBackColor = true;
            this.ButtonLicense.Click += new System.EventHandler(this.ButtonLicense_Click);
            // 
            // LabelLicense
            // 
            this.LabelLicense.AutoSize = true;
            this.LabelLicense.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelLicense.Location = new System.Drawing.Point(3, 33);
            this.LabelLicense.Name = "LabelLicense";
            this.LabelLicense.Size = new System.Drawing.Size(180, 15);
            this.LabelLicense.TabIndex = 7;
            this.LabelLicense.Text = "Licensed under the GNU GPL, v3.";
            // 
            // LableVersion
            // 
            this.LableVersion.AutoSize = true;
            this.LableVersion.Cursor = System.Windows.Forms.Cursors.Help;
            this.LableVersion.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LableVersion.Location = new System.Drawing.Point(3, 18);
            this.LableVersion.Name = "LableVersion";
            this.LableVersion.Size = new System.Drawing.Size(0, 15);
            this.LableVersion.TabIndex = 2;
            this.LableVersion.Click += new System.EventHandler(this.LabelVersion_Click);
            // 
            // PanelMain
            // 
            this.PanelMain.AutoSize = true;
            this.PanelMain.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.PanelMain.Controls.Add(this.LabelInfo);
            this.PanelMain.Controls.Add(this.ButtonLicense);
            this.PanelMain.Controls.Add(this.ButtonClose);
            this.PanelMain.Controls.Add(this.LableVersion);
            this.PanelMain.Controls.Add(this.ButtonGitHub);
            this.PanelMain.Controls.Add(this.LabelLicense);
            this.PanelMain.Controls.Add(this.LabelAuthor);
            this.PanelMain.Location = new System.Drawing.Point(0, 0);
            this.PanelMain.Name = "PanelMain";
            this.PanelMain.Size = new System.Drawing.Size(243, 94);
            this.PanelMain.TabIndex = 8;
            // 
            // FormAbout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.CancelButton = this.ButtonClose;
            this.ClientSize = new System.Drawing.Size(243, 94);
            this.Controls.Add(this.PanelMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormAbout";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "关于此程序";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormAbout_FormClosing);
            this.Load += new System.EventHandler(this.FormAbout_Load);
            this.PanelMain.ResumeLayout(false);
            this.PanelMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label LabelInfo;
        private System.Windows.Forms.Label LabelAuthor;
        private System.Windows.Forms.Button ButtonGitHub;
        private System.Windows.Forms.Button ButtonClose;
        private System.Windows.Forms.Button ButtonLicense;
        private System.Windows.Forms.Label LabelLicense;
        private System.Windows.Forms.Label LableVersion;
        private System.Windows.Forms.Panel PanelMain;
    }
}