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
            this.FormAboutLabelVersion = new System.Windows.Forms.Label();
            this.FormAboutLabelAuthor = new System.Windows.Forms.Label();
            this.FormAboutBottonGH = new System.Windows.Forms.Button();
            this.FormAboutBottonClose = new System.Windows.Forms.Button();
            this.FormAboutLicenseButton = new System.Windows.Forms.Button();
            this.FormAboutLicenseHint = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // FormAboutLabelInfo
            // 
            this.FormAboutLabelInfo.AutoSize = true;
            this.FormAboutLabelInfo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.FormAboutLabelInfo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormAboutLabelInfo.Location = new System.Drawing.Point(0, 5);
            this.FormAboutLabelInfo.Name = "FormAboutLabelInfo";
            this.FormAboutLabelInfo.Size = new System.Drawing.Size(217, 15);
            this.FormAboutLabelInfo.TabIndex = 1;
            this.FormAboutLabelInfo.Text = "CEETimerCSharpWinForms 高考倒计时";
            // 
            // FormAboutLabelVersion
            // 
            this.FormAboutLabelVersion.AutoSize = true;
            this.FormAboutLabelVersion.Cursor = System.Windows.Forms.Cursors.Hand;
            this.FormAboutLabelVersion.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.FormAboutLabelVersion.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormAboutLabelVersion.Location = new System.Drawing.Point(0, 5);
            this.FormAboutLabelVersion.Name = "FormAboutLabelVersion";
            this.FormAboutLabelVersion.Size = new System.Drawing.Size(0, 15);
            this.FormAboutLabelVersion.TabIndex = 2;
            this.FormAboutLabelVersion.Click += new System.EventHandler(this.FormAboutLabelVersion_Click);
            // 
            // FormAboutLabelAuthor
            // 
            this.FormAboutLabelAuthor.AutoSize = true;
            this.FormAboutLabelAuthor.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.FormAboutLabelAuthor.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormAboutLabelAuthor.Location = new System.Drawing.Point(0, 5);
            this.FormAboutLabelAuthor.Name = "FormAboutLabelAuthor";
            this.FormAboutLabelAuthor.Size = new System.Drawing.Size(173, 15);
            this.FormAboutLabelAuthor.TabIndex = 3;
            this.FormAboutLabelAuthor.Text = "Copyright © 2023 WangHaonie";
            // 
            // FormAboutBottonGH
            // 
            this.FormAboutBottonGH.AutoSize = true;
            this.FormAboutBottonGH.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.FormAboutBottonGH.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.FormAboutBottonGH.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormAboutBottonGH.Location = new System.Drawing.Point(165, 107);
            this.FormAboutBottonGH.Name = "FormAboutBottonGH";
            this.FormAboutBottonGH.Size = new System.Drawing.Size(75, 23);
            this.FormAboutBottonGH.TabIndex = 4;
            this.FormAboutBottonGH.Text = "GitHub";
            this.FormAboutBottonGH.UseVisualStyleBackColor = true;
            this.FormAboutBottonGH.Click += new System.EventHandler(this.FormAboutBottonGH_Click);
            // 
            // FormAboutBottonClose
            // 
            this.FormAboutBottonClose.AutoSize = true;
            this.FormAboutBottonClose.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.FormAboutBottonClose.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormAboutBottonClose.Location = new System.Drawing.Point(246, 107);
            this.FormAboutBottonClose.Name = "FormAboutBottonClose";
            this.FormAboutBottonClose.Size = new System.Drawing.Size(75, 23);
            this.FormAboutBottonClose.TabIndex = 5;
            this.FormAboutBottonClose.Text = "关闭";
            this.FormAboutBottonClose.UseVisualStyleBackColor = true;
            this.FormAboutBottonClose.Click += new System.EventHandler(this.FormAboutBottonClose_Click);
            // 
            // FormAboutLicenseButton
            // 
            this.FormAboutLicenseButton.AutoSize = true;
            this.FormAboutLicenseButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.FormAboutLicenseButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.FormAboutLicenseButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormAboutLicenseButton.Location = new System.Drawing.Point(84, 107);
            this.FormAboutLicenseButton.Name = "FormAboutLicenseButton";
            this.FormAboutLicenseButton.Size = new System.Drawing.Size(75, 23);
            this.FormAboutLicenseButton.TabIndex = 6;
            this.FormAboutLicenseButton.Text = "License";
            this.FormAboutLicenseButton.UseVisualStyleBackColor = true;
            this.FormAboutLicenseButton.Click += new System.EventHandler(this.FormAboutLicenseButton_Click);
            // 
            // FormAboutLicenseHint
            // 
            this.FormAboutLicenseHint.AutoSize = true;
            this.FormAboutLicenseHint.Cursor = System.Windows.Forms.Cursors.Default;
            this.FormAboutLicenseHint.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.FormAboutLicenseHint.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormAboutLicenseHint.Location = new System.Drawing.Point(0, 5);
            this.FormAboutLicenseHint.Name = "FormAboutLicenseHint";
            this.FormAboutLicenseHint.Size = new System.Drawing.Size(180, 15);
            this.FormAboutLicenseHint.TabIndex = 7;
            this.FormAboutLicenseHint.Text = "Licensed under the GNU GPL, v3.";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.FormAboutBottonClose, 3, 4);
            this.tableLayoutPanel1.Controls.Add(this.FormAboutLicenseButton, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.FormAboutBottonGH, 2, 4);
            this.tableLayoutPanel1.Controls.Add(this.panel3, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel4, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.panel5, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(324, 133);
            this.tableLayoutPanel1.TabIndex = 8;
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.SetColumnSpan(this.panel1, 3);
            this.panel1.Controls.Add(this.FormAboutLabelInfo);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(84, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(237, 20);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.panel2, 3);
            this.panel2.Controls.Add(this.FormAboutLabelVersion);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(84, 29);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(237, 20);
            this.panel2.TabIndex = 1;
            // 
            // panel3
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.panel3, 3);
            this.panel3.Controls.Add(this.FormAboutLicenseHint);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(84, 55);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(237, 20);
            this.panel3.TabIndex = 2;
            // 
            // panel4
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.panel4, 3);
            this.panel4.Controls.Add(this.FormAboutLabelAuthor);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(84, 81);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(237, 20);
            this.panel4.TabIndex = 3;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.pictureBox1);
            this.panel5.Location = new System.Drawing.Point(3, 3);
            this.panel5.Name = "panel5";
            this.tableLayoutPanel1.SetRowSpan(this.panel5, 2);
            this.panel5.Size = new System.Drawing.Size(74, 46);
            this.panel5.TabIndex = 7;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::CEETimerCSharpWinForms.Properties.Resources.AppIcon;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox1.Location = new System.Drawing.Point(3, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(72, 46);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // FormAbout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(324, 133);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormAbout";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "关于此程序";
            this.Load += new System.EventHandler(this.FormAbout_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label FormAboutLabelInfo;
        private System.Windows.Forms.Label FormAboutLabelVersion;
        private System.Windows.Forms.Label FormAboutLabelAuthor;
        private System.Windows.Forms.Button FormAboutBottonGH;
        private System.Windows.Forms.Button FormAboutBottonClose;
        private System.Windows.Forms.Button FormAboutLicenseButton;
        private System.Windows.Forms.Label FormAboutLicenseHint;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}