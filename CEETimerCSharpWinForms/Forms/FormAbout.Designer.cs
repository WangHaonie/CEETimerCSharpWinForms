using CEETimerCSharpWinForms.Modules;

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
            this.FormAboutLabelInfo = new System.Windows.Forms.Label();
            this.FormAboutLabelAuthor = new System.Windows.Forms.Label();
            this.FormAboutBottonGH = new System.Windows.Forms.Button();
            this.FormAboutBottonClose = new System.Windows.Forms.Button();
            this.FormAboutLicenseButton = new System.Windows.Forms.Button();
            this.FormAboutLicenseHint = new System.Windows.Forms.Label();
            this.FormAboutLabelVersion = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // FormAboutLabelInfo
            // 
            this.FormAboutLabelInfo.AutoSize = true;
            this.FormAboutLabelInfo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormAboutLabelInfo.Location = new System.Drawing.Point(3, 3);
            this.FormAboutLabelInfo.Name = "FormAboutLabelInfo";
            this.FormAboutLabelInfo.Size = new System.Drawing.Size(217, 15);
            this.FormAboutLabelInfo.TabIndex = 1;
            this.FormAboutLabelInfo.Text = "CEETimerCSharpWinForms 高考倒计时";
            // 
            // FormAboutLabelAuthor
            // 
            this.FormAboutLabelAuthor.AutoSize = true;
            this.FormAboutLabelAuthor.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormAboutLabelAuthor.Location = new System.Drawing.Point(3, 48);
            this.FormAboutLabelAuthor.Name = "FormAboutLabelAuthor";
            this.FormAboutLabelAuthor.Size = new System.Drawing.Size(133, 15);
            this.FormAboutLabelAuthor.TabIndex = 3;
            this.FormAboutLabelAuthor.Text = "FormAboutLabelAuthor";
            // 
            // FormAboutBottonGH
            // 
            this.FormAboutBottonGH.AutoSize = true;
            this.FormAboutBottonGH.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.FormAboutBottonGH.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormAboutBottonGH.Location = new System.Drawing.Point(14, 66);
            this.FormAboutBottonGH.Name = "FormAboutBottonGH";
            this.FormAboutBottonGH.Size = new System.Drawing.Size(85, 25);
            this.FormAboutBottonGH.TabIndex = 4;
            this.FormAboutBottonGH.Text = "项目主页(&G)";
            this.FormAboutBottonGH.UseVisualStyleBackColor = true;
            this.FormAboutBottonGH.Click += new System.EventHandler(this.FormAboutBottonGH_Click);
            // 
            // FormAboutBottonClose
            // 
            this.FormAboutBottonClose.AutoSize = true;
            this.FormAboutBottonClose.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.FormAboutBottonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.FormAboutBottonClose.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormAboutBottonClose.Location = new System.Drawing.Point(181, 66);
            this.FormAboutBottonClose.Name = "FormAboutBottonClose";
            this.FormAboutBottonClose.Size = new System.Drawing.Size(59, 25);
            this.FormAboutBottonClose.TabIndex = 5;
            this.FormAboutBottonClose.Text = "关闭(&C)";
            this.FormAboutBottonClose.UseVisualStyleBackColor = true;
            this.FormAboutBottonClose.Click += new System.EventHandler(this.FormAboutBottonClose_Click);
            // 
            // FormAboutLicenseButton
            // 
            this.FormAboutLicenseButton.AutoSize = true;
            this.FormAboutLicenseButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.FormAboutLicenseButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormAboutLicenseButton.Location = new System.Drawing.Point(105, 66);
            this.FormAboutLicenseButton.Name = "FormAboutLicenseButton";
            this.FormAboutLicenseButton.Size = new System.Drawing.Size(70, 25);
            this.FormAboutLicenseButton.TabIndex = 6;
            this.FormAboutLicenseButton.Text = "许可证(&L)";
            this.FormAboutLicenseButton.UseVisualStyleBackColor = true;
            this.FormAboutLicenseButton.Click += new System.EventHandler(this.FormAboutLicenseButton_Click);
            // 
            // FormAboutLicenseHint
            // 
            this.FormAboutLicenseHint.AutoSize = true;
            this.FormAboutLicenseHint.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormAboutLicenseHint.Location = new System.Drawing.Point(3, 33);
            this.FormAboutLicenseHint.Name = "FormAboutLicenseHint";
            this.FormAboutLicenseHint.Size = new System.Drawing.Size(180, 15);
            this.FormAboutLicenseHint.TabIndex = 7;
            this.FormAboutLicenseHint.Text = "Licensed under the GNU GPL, v3.";
            // 
            // FormAboutLabelVersion
            // 
            this.FormAboutLabelVersion.AutoSize = true;
            this.FormAboutLabelVersion.Cursor = System.Windows.Forms.Cursors.Help;
            this.FormAboutLabelVersion.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormAboutLabelVersion.Location = new System.Drawing.Point(3, 18);
            this.FormAboutLabelVersion.Name = "FormAboutLabelVersion";
            this.FormAboutLabelVersion.Size = new System.Drawing.Size(134, 15);
            this.FormAboutLabelVersion.TabIndex = 2;
            this.FormAboutLabelVersion.Text = "FormAboutLabelVersion";
            this.FormAboutLabelVersion.Click += new System.EventHandler(this.FormAboutLabelVersion_Click);
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel1.Controls.Add(this.FormAboutLabelInfo);
            this.panel1.Controls.Add(this.FormAboutLicenseButton);
            this.panel1.Controls.Add(this.FormAboutBottonClose);
            this.panel1.Controls.Add(this.FormAboutLabelVersion);
            this.panel1.Controls.Add(this.FormAboutBottonGH);
            this.panel1.Controls.Add(this.FormAboutLicenseHint);
            this.panel1.Controls.Add(this.FormAboutLabelAuthor);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(243, 94);
            this.panel1.TabIndex = 8;
            // 
            // FormAbout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.CancelButton = this.FormAboutBottonClose;
            this.ClientSize = new System.Drawing.Size(243, 94);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormAbout";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "关于此程序";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormAbout_FormClosing);
            this.Load += new System.EventHandler(this.FormAbout_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label FormAboutLabelInfo;
        private System.Windows.Forms.Label FormAboutLabelAuthor;
        private System.Windows.Forms.Button FormAboutBottonGH;
        private System.Windows.Forms.Button FormAboutBottonClose;
        private System.Windows.Forms.Button FormAboutLicenseButton;
        private System.Windows.Forms.Label FormAboutLicenseHint;
        private System.Windows.Forms.Label FormAboutLabelVersion;
        private System.Windows.Forms.Panel panel1;
    }
}