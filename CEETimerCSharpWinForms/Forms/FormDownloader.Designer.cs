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
            this.FrmDlL3 = new System.Windows.Forms.Label();
            this.FrmDlL4 = new System.Windows.Forms.Label();
            this.FrmDlPb = new System.Windows.Forms.ProgressBar();
            this.FrmDlBtnR = new System.Windows.Forms.Button();
            this.FrmDlBtnC = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // FrmDlL1
            // 
            this.FrmDlL1.AutoSize = true;
            this.FrmDlL1.Location = new System.Drawing.Point(11, 9);
            this.FrmDlL1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.FrmDlL1.Name = "FrmDlL1";
            this.FrmDlL1.Size = new System.Drawing.Size(172, 15);
            this.FrmDlL1.TabIndex = 0;
            this.FrmDlL1.Text = "正在下载更新文件，请稍侯...";
            // 
            // FrmDlL3
            // 
            this.FrmDlL3.AutoSize = true;
            this.FrmDlL3.Location = new System.Drawing.Point(11, 50);
            this.FrmDlL3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.FrmDlL3.Name = "FrmDlL3";
            this.FrmDlL3.Size = new System.Drawing.Size(146, 15);
            this.FrmDlL3.TabIndex = 0;
            this.FrmDlL3.Text = "已下载/总共：(获取中...)";
            this.FrmDlL3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FrmDlL4
            // 
            this.FrmDlL4.AutoSize = true;
            this.FrmDlL4.Location = new System.Drawing.Point(11, 65);
            this.FrmDlL4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.FrmDlL4.Name = "FrmDlL4";
            this.FrmDlL4.Size = new System.Drawing.Size(128, 15);
            this.FrmDlL4.TabIndex = 0;
            this.FrmDlL4.Text = "下载速度：(获取中...)";
            // 
            // FrmDlPb
            // 
            this.FrmDlPb.Location = new System.Drawing.Point(14, 26);
            this.FrmDlPb.Margin = new System.Windows.Forms.Padding(2);
            this.FrmDlPb.Name = "FrmDlPb";
            this.FrmDlPb.Size = new System.Drawing.Size(332, 22);
            this.FrmDlPb.TabIndex = 0;
            // 
            // FrmDlBtnR
            // 
            this.FrmDlBtnR.AutoSize = true;
            this.FrmDlBtnR.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.FrmDlBtnR.Enabled = false;
            this.FrmDlBtnR.Location = new System.Drawing.Point(226, 60);
            this.FrmDlBtnR.Margin = new System.Windows.Forms.Padding(2);
            this.FrmDlBtnR.Name = "FrmDlBtnR";
            this.FrmDlBtnR.Size = new System.Drawing.Size(57, 25);
            this.FrmDlBtnR.TabIndex = 7;
            this.FrmDlBtnR.Text = "链接(&L)";
            this.FrmDlBtnR.UseVisualStyleBackColor = true;
            this.FrmDlBtnR.Click += new System.EventHandler(this.FrmDlBtnR_Click);
            // 
            // FrmDlBtnC
            // 
            this.FrmDlBtnC.AutoSize = true;
            this.FrmDlBtnC.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.FrmDlBtnC.Location = new System.Drawing.Point(287, 60);
            this.FrmDlBtnC.Margin = new System.Windows.Forms.Padding(2);
            this.FrmDlBtnC.Name = "FrmDlBtnC";
            this.FrmDlBtnC.Size = new System.Drawing.Size(59, 25);
            this.FrmDlBtnC.TabIndex = 8;
            this.FrmDlBtnC.Text = "取消(&C)";
            this.FrmDlBtnC.UseVisualStyleBackColor = true;
            this.FrmDlBtnC.Click += new System.EventHandler(this.FrmDlBtnC_Click);
            // 
            // FormDownloader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(357, 91);
            this.Controls.Add(this.FrmDlBtnC);
            this.Controls.Add(this.FrmDlBtnR);
            this.Controls.Add(this.FrmDlPb);
            this.Controls.Add(this.FrmDlL4);
            this.Controls.Add(this.FrmDlL3);
            this.Controls.Add(this.FrmDlL1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "FormDownloader";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "更新进度 - 高考倒计时";
            this.Load += new System.EventHandler(this.FormDownloader_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label FrmDlL1;
        private System.Windows.Forms.Label FrmDlL3;
        private System.Windows.Forms.Label FrmDlL4;
        private System.Windows.Forms.ProgressBar FrmDlPb;
        private System.Windows.Forms.Button FrmDlBtnR;
        private System.Windows.Forms.Button FrmDlBtnC;
    }
}