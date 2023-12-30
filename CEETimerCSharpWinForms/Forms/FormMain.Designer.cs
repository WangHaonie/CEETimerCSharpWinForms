namespace CEETimerCSharpWinForms
{
    partial class CEETimerCSharpWinForms
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.labelCountdown = new System.Windows.Forms.Label();
            this.Context = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ContextSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ContextOpenDir = new System.Windows.Forms.ToolStripMenuItem();
            this.Context.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelCountdown
            // 
            this.labelCountdown.AutoSize = true;
            this.labelCountdown.BackColor = System.Drawing.Color.Transparent;
            this.labelCountdown.ContextMenuStrip = this.Context;
            this.labelCountdown.Font = new System.Drawing.Font("微软雅黑", 17.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelCountdown.ForeColor = System.Drawing.Color.Red;
            this.labelCountdown.Location = new System.Drawing.Point(1, 0);
            this.labelCountdown.Name = "labelCountdown";
            this.labelCountdown.Size = new System.Drawing.Size(472, 31);
            this.labelCountdown.TabIndex = 0;
            this.labelCountdown.Text = "欢迎使用高考倒计时，程序加载中，请稍候...";
            // 
            // Context
            // 
            this.Context.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ContextSettings,
            this.ContextAbout,
            this.toolStripSeparator1,
            this.ContextOpenDir});
            this.Context.Name = "Context";
            this.Context.Size = new System.Drawing.Size(183, 76);
            // 
            // ContextSettings
            // 
            this.ContextSettings.Name = "ContextSettings";
            this.ContextSettings.Size = new System.Drawing.Size(182, 22);
            this.ContextSettings.Text = "设置(&S)";
            this.ContextSettings.Click += new System.EventHandler(this.ContextSettings_Click);
            // 
            // ContextAbout
            // 
            this.ContextAbout.Name = "ContextAbout";
            this.ContextAbout.ShowShortcutKeys = false;
            this.ContextAbout.Size = new System.Drawing.Size(182, 22);
            this.ContextAbout.Text = "关于(&A)";
            this.ContextAbout.Click += new System.EventHandler(this.ContextAbout_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(179, 6);
            // 
            // ContextOpenDir
            // 
            this.ContextOpenDir.Name = "ContextOpenDir";
            this.ContextOpenDir.Size = new System.Drawing.Size(182, 22);
            this.ContextOpenDir.Text = "打开程序文件夹(&O)";
            this.ContextOpenDir.Click += new System.EventHandler(this.ContextOpenDir_Click);
            // 
            // CEETimerCSharpWinForms
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(431, 23);
            this.ContextMenuStrip = this.Context;
            this.ControlBox = false;
            this.Controls.Add(this.labelCountdown);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CEETimerCSharpWinForms";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "高考倒计时";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CEETimerCSharpWinForms_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Context.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelCountdown;
        private System.Windows.Forms.ContextMenuStrip Context;
        private System.Windows.Forms.ToolStripMenuItem ContextAbout;
        private System.Windows.Forms.ToolStripMenuItem ContextSettings;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem ContextOpenDir;
    }
}

