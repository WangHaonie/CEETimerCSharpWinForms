namespace CEETimerCSharpWinForms.Forms
{
    partial class DownloaderForm
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
            this.LabelDownloading = new System.Windows.Forms.Label();
            this.LabelSize = new System.Windows.Forms.Label();
            this.LabelSpeed = new System.Windows.Forms.Label();
            this.ProgressBarMain = new System.Windows.Forms.ProgressBar();
            this.ButtonRetry = new System.Windows.Forms.Button();
            this.ButtonCancel = new System.Windows.Forms.Button();
            this.LinkBroswer = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // LabelDownloading
            // 
            this.LabelDownloading.AutoSize = true;
            this.LabelDownloading.Location = new System.Drawing.Point(2, 3);
            this.LabelDownloading.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LabelDownloading.Name = "LabelDownloading";
            this.LabelDownloading.Size = new System.Drawing.Size(172, 15);
            this.LabelDownloading.TabIndex = 0;
            this.LabelDownloading.Text = "正在下载更新文件，请稍侯...";
            // 
            // LabelSize
            // 
            this.LabelSize.AutoSize = true;
            this.LabelSize.Location = new System.Drawing.Point(2, 44);
            this.LabelSize.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LabelSize.Name = "LabelSize";
            this.LabelSize.Size = new System.Drawing.Size(146, 15);
            this.LabelSize.TabIndex = 0;
            this.LabelSize.Text = "已下载/总共：(获取中...)";
            this.LabelSize.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LabelSpeed
            // 
            this.LabelSpeed.AutoSize = true;
            this.LabelSpeed.Location = new System.Drawing.Point(2, 59);
            this.LabelSpeed.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LabelSpeed.Name = "LabelSpeed";
            this.LabelSpeed.Size = new System.Drawing.Size(128, 15);
            this.LabelSpeed.TabIndex = 0;
            this.LabelSpeed.Text = "下载速度：(获取中...)";
            // 
            // ProgressBarMain
            // 
            this.ProgressBarMain.Location = new System.Drawing.Point(5, 20);
            this.ProgressBarMain.Margin = new System.Windows.Forms.Padding(2);
            this.ProgressBarMain.Name = "ProgressBarMain";
            this.ProgressBarMain.Size = new System.Drawing.Size(344, 22);
            this.ProgressBarMain.TabIndex = 0;
            // 
            // ButtonRetry
            // 
            this.ButtonRetry.Enabled = false;
            this.ButtonRetry.Location = new System.Drawing.Point(195, 47);
            this.ButtonRetry.Margin = new System.Windows.Forms.Padding(2);
            this.ButtonRetry.Name = "ButtonRetry";
            this.ButtonRetry.Size = new System.Drawing.Size(75, 25);
            this.ButtonRetry.TabIndex = 7;
            this.ButtonRetry.Text = "重试(&R)";
            this.ButtonRetry.UseVisualStyleBackColor = true;
            this.ButtonRetry.Click += new System.EventHandler(this.ButtonRetry_Click);
            // 
            // ButtonCancel
            // 
            this.ButtonCancel.Location = new System.Drawing.Point(274, 47);
            this.ButtonCancel.Margin = new System.Windows.Forms.Padding(2);
            this.ButtonCancel.Name = "ButtonCancel";
            this.ButtonCancel.Size = new System.Drawing.Size(75, 25);
            this.ButtonCancel.TabIndex = 8;
            this.ButtonCancel.Text = "取消(&C)";
            this.ButtonCancel.UseVisualStyleBackColor = true;
            this.ButtonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
            // 
            // LinkBroswer
            // 
            this.LinkBroswer.AutoSize = true;
            this.LinkBroswer.Location = new System.Drawing.Point(280, 3);
            this.LinkBroswer.Name = "LinkBroswer";
            this.LinkBroswer.Size = new System.Drawing.Size(72, 15);
            this.LinkBroswer.TabIndex = 9;
            this.LinkBroswer.TabStop = true;
            this.LinkBroswer.Text = "浏览器下载";
            this.LinkBroswer.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkBroswer_LinkClicked);
            // 
            // DownloaderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(354, 76);
            this.Controls.Add(this.LinkBroswer);
            this.Controls.Add(this.ButtonCancel);
            this.Controls.Add(this.ButtonRetry);
            this.Controls.Add(this.ProgressBarMain);
            this.Controls.Add(this.LabelSpeed);
            this.Controls.Add(this.LabelSize);
            this.Controls.Add(this.LabelDownloading);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "DownloaderForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "更新进度 - 高考倒计时";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DownloaderForm_FormClosing);
            this.Load += new System.EventHandler(this.DownloaderForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label LabelDownloading;
        private System.Windows.Forms.Label LabelSize;
        private System.Windows.Forms.Label LabelSpeed;
        private System.Windows.Forms.ProgressBar ProgressBarMain;
        private System.Windows.Forms.Button ButtonRetry;
        private System.Windows.Forms.Button ButtonCancel;
        private System.Windows.Forms.LinkLabel LinkBroswer;
    }
}