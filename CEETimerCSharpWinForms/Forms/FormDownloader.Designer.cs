namespace CEETimerCSharpWinForms.Forms
{
    partial class FormDownloader
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
            this.FrmDlL1 = new System.Windows.Forms.Label();
            this.FrmDlL2 = new System.Windows.Forms.Label();
            this.FrmDlL3 = new System.Windows.Forms.Label();
            this.FrmDlL4 = new System.Windows.Forms.Label();
            this.FrmDlPb = new System.Windows.Forms.ProgressBar();
            this.FrmDlBtnR = new System.Windows.Forms.Button();
            this.FrmDlBtnC = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel7.SuspendLayout();
            this.SuspendLayout();
            // 
            // FrmDlL1
            // 
            this.FrmDlL1.AutoSize = true;
            this.FrmDlL1.Location = new System.Drawing.Point(2, 0);
            this.FrmDlL1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.FrmDlL1.Name = "FrmDlL1";
            this.FrmDlL1.Size = new System.Drawing.Size(172, 15);
            this.FrmDlL1.TabIndex = 0;
            this.FrmDlL1.Text = "正在下载更新文件，请稍侯...";
            // 
            // FrmDlL2
            // 
            this.FrmDlL2.AutoSize = true;
            this.FrmDlL2.Location = new System.Drawing.Point(2, 0);
            this.FrmDlL2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.FrmDlL2.Name = "FrmDlL2";
            this.FrmDlL2.Size = new System.Drawing.Size(29, 15);
            this.FrmDlL2.TabIndex = 0;
            this.FrmDlL2.Text = "N/A";
            // 
            // FrmDlL3
            // 
            this.FrmDlL3.AutoSize = true;
            this.FrmDlL3.Location = new System.Drawing.Point(2, 0);
            this.FrmDlL3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.FrmDlL3.Name = "FrmDlL3";
            this.FrmDlL3.Size = new System.Drawing.Size(29, 15);
            this.FrmDlL3.TabIndex = 0;
            this.FrmDlL3.Text = "N/A";
            this.FrmDlL3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FrmDlL4
            // 
            this.FrmDlL4.AutoSize = true;
            this.FrmDlL4.Location = new System.Drawing.Point(2, 0);
            this.FrmDlL4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.FrmDlL4.Name = "FrmDlL4";
            this.FrmDlL4.Size = new System.Drawing.Size(29, 15);
            this.FrmDlL4.TabIndex = 0;
            this.FrmDlL4.Text = "N/A";
            // 
            // FrmDlPb
            // 
            this.FrmDlPb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FrmDlPb.Location = new System.Drawing.Point(0, 0);
            this.FrmDlPb.Margin = new System.Windows.Forms.Padding(2);
            this.FrmDlPb.Name = "FrmDlPb";
            this.FrmDlPb.Size = new System.Drawing.Size(384, 22);
            this.FrmDlPb.TabIndex = 0;
            // 
            // FrmDlBtnR
            // 
            this.FrmDlBtnR.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FrmDlBtnR.Enabled = false;
            this.FrmDlBtnR.Location = new System.Drawing.Point(0, 0);
            this.FrmDlBtnR.Margin = new System.Windows.Forms.Padding(2);
            this.FrmDlBtnR.Name = "FrmDlBtnR";
            this.FrmDlBtnR.Size = new System.Drawing.Size(72, 24);
            this.FrmDlBtnR.TabIndex = 7;
            this.FrmDlBtnR.Text = "链接(&L)";
            this.FrmDlBtnR.UseVisualStyleBackColor = true;
            this.FrmDlBtnR.Click += new System.EventHandler(this.FrmDlBtnR_Click);
            // 
            // FrmDlBtnC
            // 
            this.FrmDlBtnC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FrmDlBtnC.Location = new System.Drawing.Point(0, 0);
            this.FrmDlBtnC.Margin = new System.Windows.Forms.Padding(2);
            this.FrmDlBtnC.Name = "FrmDlBtnC";
            this.FrmDlBtnC.Size = new System.Drawing.Size(72, 24);
            this.FrmDlBtnC.TabIndex = 8;
            this.FrmDlBtnC.Text = "取消(&C)";
            this.FrmDlBtnC.UseVisualStyleBackColor = true;
            this.FrmDlBtnC.Click += new System.EventHandler(this.FrmDlBtnC_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 10;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel3, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel4, 8, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel5, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel6, 6, 3);
            this.tableLayoutPanel1.Controls.Add(this.panel7, 8, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(390, 114);
            this.tableLayoutPanel1.TabIndex = 9;
            // 
            // panel1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.panel1, 10);
            this.panel1.Controls.Add(this.FrmDlL1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(384, 22);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.FrmDlL2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 31);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(33, 22);
            this.panel2.TabIndex = 1;
            // 
            // panel3
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.panel3, 3);
            this.panel3.Controls.Add(this.FrmDlL3);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(120, 31);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(111, 22);
            this.panel3.TabIndex = 2;
            // 
            // panel4
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.panel4, 2);
            this.panel4.Controls.Add(this.FrmDlL4);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(315, 31);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(72, 22);
            this.panel4.TabIndex = 3;
            // 
            // panel5
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.panel5, 10);
            this.panel5.Controls.Add(this.FrmDlPb);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(3, 59);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(384, 22);
            this.panel5.TabIndex = 4;
            // 
            // panel6
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.panel6, 2);
            this.panel6.Controls.Add(this.FrmDlBtnR);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(237, 87);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(72, 24);
            this.panel6.TabIndex = 5;
            // 
            // panel7
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.panel7, 2);
            this.panel7.Controls.Add(this.FrmDlBtnC);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel7.Location = new System.Drawing.Point(315, 87);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(72, 24);
            this.panel7.TabIndex = 6;
            // 
            // FormDownloader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(390, 114);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormDownloader";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "更新进度 - 高考倒计时";
            this.Load += new System.EventHandler(this.FormDownloader_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label FrmDlL1;
        private System.Windows.Forms.Label FrmDlL2;
        private System.Windows.Forms.Label FrmDlL3;
        private System.Windows.Forms.Label FrmDlL4;
        private System.Windows.Forms.ProgressBar FrmDlPb;
        private System.Windows.Forms.Button FrmDlBtnR;
        private System.Windows.Forms.Button FrmDlBtnC;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel7;
    }
}