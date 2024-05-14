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
            this.ContextMenuSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenuAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ContextMenuOpenDir = new System.Windows.Forms.ToolStripMenuItem();
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
            this.LabelCountdown.Size = new System.Drawing.Size(204, 15);
            this.LabelCountdown.TabIndex = 0;
            this.LabelCountdown.Text = "欢迎使用高考倒计时, 正在加载中...";
            // 
            // ContextMenuMain
            // 
            this.ContextMenuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ContextMenuSettings,
            this.ContextMenuAbout,
            this.ToolStripSeparator1,
            this.ContextMenuOpenDir});
            this.ContextMenuMain.Name = "Context";
            this.ContextMenuMain.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.ContextMenuMain.Size = new System.Drawing.Size(183, 76);
            // 
            // ContextMenuSettings
            // 
            this.ContextMenuSettings.Name = "ContextMenuSettings";
            this.ContextMenuSettings.Size = new System.Drawing.Size(182, 22);
            this.ContextMenuSettings.Text = "设置(&S)";
            this.ContextMenuSettings.Click += new System.EventHandler(this.ContextMenuSettings_Click);
            // 
            // ContextMenuAbout
            // 
            this.ContextMenuAbout.Name = "ContextMenuAbout";
            this.ContextMenuAbout.ShowShortcutKeys = false;
            this.ContextMenuAbout.Size = new System.Drawing.Size(182, 22);
            this.ContextMenuAbout.Text = "关于(&A)";
            this.ContextMenuAbout.Click += new System.EventHandler(this.ContextMenuAbout_Click);
            // 
            // ToolStripSeparator1
            // 
            this.ToolStripSeparator1.Name = "ToolStripSeparator1";
            this.ToolStripSeparator1.Size = new System.Drawing.Size(179, 6);
            // 
            // ContextMenuOpenDir
            // 
            this.ContextMenuOpenDir.Name = "ContextMenuOpenDir";
            this.ContextMenuOpenDir.Size = new System.Drawing.Size(182, 22);
            this.ContextMenuOpenDir.Text = "打开程序文件夹(&O)";
            this.ContextMenuOpenDir.Click += new System.EventHandler(this.ContextMenuOpenDir_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(204, 23);
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
        private System.Windows.Forms.ToolStripMenuItem ContextMenuAbout;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuSettings;
        private System.Windows.Forms.ToolStripSeparator ToolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuOpenDir;
    }
}

