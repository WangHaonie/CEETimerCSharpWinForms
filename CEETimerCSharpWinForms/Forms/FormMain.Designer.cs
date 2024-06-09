namespace CEETimerCSharpWinForms.Forms
{
    partial class FormMain
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
            this.LabelCountdown = new System.Windows.Forms.Label();
            this.ContextMenuMain = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ContextSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripSeparatorMain = new System.Windows.Forms.ToolStripSeparator();
            this.ContextOpenDir = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenuMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // LabelCountdown
            // 
            this.LabelCountdown.AutoSize = true;
            this.LabelCountdown.BackColor = System.Drawing.Color.Transparent;
            this.LabelCountdown.ContextMenuStrip = this.ContextMenuMain;
            this.LabelCountdown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LabelCountdown.ForeColor = System.Drawing.Color.Black;
            this.LabelCountdown.Location = new System.Drawing.Point(0, 0);
            this.LabelCountdown.Name = "LabelCountdown";
            this.LabelCountdown.Size = new System.Drawing.Size(107, 15);
            this.LabelCountdown.TabIndex = 0;
            this.LabelCountdown.Text = "正在加载中...";
            // 
            // ContextMenuMain
            // 
            this.ContextMenuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ContextSettings,
            this.ContextAbout,
            this.ToolStripSeparatorMain,
            this.ContextOpenDir});
            this.ContextMenuMain.Name = "Context";
            this.ContextMenuMain.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.ContextMenuMain.Size = new System.Drawing.Size(183, 76);
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
            // ToolStripSeparatorMain
            // 
            this.ToolStripSeparatorMain.Name = "ToolStripSeparatorMain";
            this.ToolStripSeparatorMain.Size = new System.Drawing.Size(179, 6);
            // 
            // ContextOpenDir
            // 
            this.ContextOpenDir.Name = "ContextOpenDir";
            this.ContextOpenDir.Size = new System.Drawing.Size(182, 22);
            this.ContextOpenDir.Text = "打开所在文件夹(&O)";
            this.ContextOpenDir.Click += new System.EventHandler(this.ContextOpenDir_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(104, 23);
            this.ContextMenuStrip = this.ContextMenuMain;
            this.ControlBox = false;
            this.Controls.Add(this.LabelCountdown);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormMain";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "高考倒计时";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.ContextMenuMain.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LabelCountdown;
        private System.Windows.Forms.ContextMenuStrip ContextMenuMain;
        private System.Windows.Forms.ToolStripMenuItem ContextAbout;
        private System.Windows.Forms.ToolStripMenuItem ContextSettings;
        private System.Windows.Forms.ToolStripSeparator ToolStripSeparatorMain;
        private System.Windows.Forms.ToolStripMenuItem ContextOpenDir;
    }
}

