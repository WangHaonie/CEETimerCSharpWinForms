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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.FormAboutLabelVersion = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.panel8 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel8.SuspendLayout();
            this.SuspendLayout();
            // 
            // FormAboutLabelInfo
            // 
            this.FormAboutLabelInfo.AutoSize = true;
            this.FormAboutLabelInfo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormAboutLabelInfo.Location = new System.Drawing.Point(3, 0);
            this.FormAboutLabelInfo.Name = "FormAboutLabelInfo";
            this.FormAboutLabelInfo.Size = new System.Drawing.Size(217, 15);
            this.FormAboutLabelInfo.TabIndex = 1;
            this.FormAboutLabelInfo.Text = "CEETimerCSharpWinForms 高考倒计时";
            // 
            // FormAboutLabelAuthor
            // 
            this.FormAboutLabelAuthor.AutoSize = true;
            this.FormAboutLabelAuthor.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormAboutLabelAuthor.Location = new System.Drawing.Point(3, 0);
            this.FormAboutLabelAuthor.Name = "FormAboutLabelAuthor";
            this.FormAboutLabelAuthor.Size = new System.Drawing.Size(173, 15);
            this.FormAboutLabelAuthor.TabIndex = 3;
            this.FormAboutLabelAuthor.Text = "Copyright © 2023 WangHaonie";
            // 
            // FormAboutBottonGH
            // 
            this.FormAboutBottonGH.AutoSize = true;
            this.FormAboutBottonGH.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.FormAboutBottonGH.Cursor = System.Windows.Forms.Cursors.Hand;
            this.FormAboutBottonGH.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FormAboutBottonGH.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormAboutBottonGH.Location = new System.Drawing.Point(0, 0);
            this.FormAboutBottonGH.Name = "FormAboutBottonGH";
            this.FormAboutBottonGH.Size = new System.Drawing.Size(69, 22);
            this.FormAboutBottonGH.TabIndex = 4;
            this.FormAboutBottonGH.Text = "GitHub(&G)";
            this.FormAboutBottonGH.UseVisualStyleBackColor = true;
            this.FormAboutBottonGH.Click += new System.EventHandler(this.FormAboutBottonGH_Click);
            // 
            // FormAboutBottonClose
            // 
            this.FormAboutBottonClose.AutoSize = true;
            this.FormAboutBottonClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.FormAboutBottonClose.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FormAboutBottonClose.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormAboutBottonClose.Location = new System.Drawing.Point(0, 0);
            this.FormAboutBottonClose.Name = "FormAboutBottonClose";
            this.FormAboutBottonClose.Size = new System.Drawing.Size(69, 22);
            this.FormAboutBottonClose.TabIndex = 5;
            this.FormAboutBottonClose.Text = "关闭(&C)";
            this.FormAboutBottonClose.UseVisualStyleBackColor = true;
            this.FormAboutBottonClose.Click += new System.EventHandler(this.FormAboutBottonClose_Click);
            // 
            // FormAboutLicenseButton
            // 
            this.FormAboutLicenseButton.AutoSize = true;
            this.FormAboutLicenseButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.FormAboutLicenseButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.FormAboutLicenseButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FormAboutLicenseButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormAboutLicenseButton.Location = new System.Drawing.Point(0, 0);
            this.FormAboutLicenseButton.Name = "FormAboutLicenseButton";
            this.FormAboutLicenseButton.Size = new System.Drawing.Size(69, 22);
            this.FormAboutLicenseButton.TabIndex = 6;
            this.FormAboutLicenseButton.Text = "License(&L)";
            this.FormAboutLicenseButton.UseVisualStyleBackColor = true;
            this.FormAboutLicenseButton.Click += new System.EventHandler(this.FormAboutLicenseButton_Click);
            // 
            // FormAboutLicenseHint
            // 
            this.FormAboutLicenseHint.AutoSize = true;
            this.FormAboutLicenseHint.Cursor = System.Windows.Forms.Cursors.Default;
            this.FormAboutLicenseHint.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormAboutLicenseHint.Location = new System.Drawing.Point(3, 0);
            this.FormAboutLicenseHint.Name = "FormAboutLicenseHint";
            this.FormAboutLicenseHint.Size = new System.Drawing.Size(180, 15);
            this.FormAboutLicenseHint.TabIndex = 7;
            this.FormAboutLicenseHint.Text = "Licensed under the GNU GPL, v3.";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::CEETimerCSharpWinForms.Properties.Resources.AppIconLogo;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(69, 42);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // FormAboutLabelVersion
            // 
            this.FormAboutLabelVersion.AutoSize = true;
            this.FormAboutLabelVersion.Cursor = System.Windows.Forms.Cursors.Hand;
            this.FormAboutLabelVersion.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormAboutLabelVersion.Location = new System.Drawing.Point(3, 0);
            this.FormAboutLabelVersion.Name = "FormAboutLabelVersion";
            this.FormAboutLabelVersion.Size = new System.Drawing.Size(134, 15);
            this.FormAboutLabelVersion.TabIndex = 2;
            this.FormAboutLabelVersion.Text = "FormAboutLabelVersion";
            this.FormAboutLabelVersion.Click += new System.EventHandler(this.FormAboutLabelVersion_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel3, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel4, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel5, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.panel6, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.panel7, 2, 4);
            this.tableLayoutPanel1.Controls.Add(this.panel8, 3, 4);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(300, 124);
            this.tableLayoutPanel1.TabIndex = 8;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.tableLayoutPanel1.SetRowSpan(this.panel1, 2);
            this.panel1.Size = new System.Drawing.Size(69, 42);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.panel2, 3);
            this.panel2.Controls.Add(this.FormAboutLabelInfo);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(78, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(219, 18);
            this.panel2.TabIndex = 1;
            // 
            // panel3
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.panel3, 3);
            this.panel3.Controls.Add(this.FormAboutLabelVersion);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(78, 27);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(219, 18);
            this.panel3.TabIndex = 2;
            // 
            // panel4
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.panel4, 3);
            this.panel4.Controls.Add(this.FormAboutLicenseHint);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(78, 51);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(219, 18);
            this.panel4.TabIndex = 3;
            // 
            // panel5
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.panel5, 3);
            this.panel5.Controls.Add(this.FormAboutLabelAuthor);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(78, 75);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(219, 18);
            this.panel5.TabIndex = 4;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.FormAboutLicenseButton);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(78, 99);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(69, 22);
            this.panel6.TabIndex = 5;
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.FormAboutBottonGH);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel7.Location = new System.Drawing.Point(153, 99);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(69, 22);
            this.panel7.TabIndex = 6;
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.FormAboutBottonClose);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel8.Location = new System.Drawing.Point(228, 99);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(69, 22);
            this.panel8.TabIndex = 7;
            // 
            // FormAbout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(300, 124);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormAbout";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "关于此程序";
            this.Load += new System.EventHandler(this.FormAbout_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label FormAboutLabelInfo;
        private System.Windows.Forms.Label FormAboutLabelAuthor;
        private System.Windows.Forms.Button FormAboutBottonGH;
        private System.Windows.Forms.Button FormAboutBottonClose;
        private System.Windows.Forms.Button FormAboutLicenseButton;
        private System.Windows.Forms.Label FormAboutLicenseHint;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label FormAboutLabelVersion;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Panel panel8;
    }
}