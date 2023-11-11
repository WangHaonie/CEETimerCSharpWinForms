namespace CEETimerCSharpWinForms.Forms
{
    partial class FormLicense
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormLicense));
            this.FormLicenseContent = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // FormLicenseContent
            // 
            this.FormLicenseContent.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.FormLicenseContent.DetectUrls = false;
            this.FormLicenseContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FormLicenseContent.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormLicenseContent.Location = new System.Drawing.Point(0, 0);
            this.FormLicenseContent.Name = "FormLicenseContent";
            this.FormLicenseContent.ReadOnly = true;
            this.FormLicenseContent.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.FormLicenseContent.Size = new System.Drawing.Size(527, 366);
            this.FormLicenseContent.TabIndex = 0;
            this.FormLicenseContent.Text = resources.GetString("FormLicenseContent.Text");
            // 
            // FormLicense
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(527, 366);
            this.Controls.Add(this.FormLicenseContent);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormLicense";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "许可证 - 高考倒计时";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox FormLicenseContent;
    }
}