namespace CEETimerCSharpWinForms.Forms
{
    partial class FormSettings
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
            this.FormSettingsCloseMain = new System.Windows.Forms.Button();
            this.FormSettingsSyncTimeButton = new System.Windows.Forms.Button();
            this.FormSettingsSetExamNameText = new System.Windows.Forms.TextBox();
            this.BtnRestart = new System.Windows.Forms.Button();
            this.StartTimePicker = new System.Windows.Forms.DateTimePicker();
            this.EndTimePicker = new System.Windows.Forms.DateTimePicker();
            this.FormSettingsApply = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.LblCounter = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.FormSettingsSetStartupCheck = new System.Windows.Forms.CheckBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.chkSetTopMost = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.SuspendLayout();
            // 
            // FormSettingsCloseMain
            // 
            this.FormSettingsCloseMain.AutoSize = true;
            this.FormSettingsCloseMain.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.FormSettingsCloseMain.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.FormSettingsCloseMain.Location = new System.Drawing.Point(354, 266);
            this.FormSettingsCloseMain.Name = "FormSettingsCloseMain";
            this.FormSettingsCloseMain.Size = new System.Drawing.Size(59, 25);
            this.FormSettingsCloseMain.TabIndex = 17;
            this.FormSettingsCloseMain.Text = "关闭(&C)";
            this.FormSettingsCloseMain.UseVisualStyleBackColor = true;
            this.FormSettingsCloseMain.Click += new System.EventHandler(this.FormSettingsCloseMain_Click);
            // 
            // FormSettingsSyncTimeButton
            // 
            this.FormSettingsSyncTimeButton.AutoSize = true;
            this.FormSettingsSyncTimeButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.FormSettingsSyncTimeButton.Location = new System.Drawing.Point(9, 112);
            this.FormSettingsSyncTimeButton.Name = "FormSettingsSyncTimeButton";
            this.FormSettingsSyncTimeButton.Size = new System.Drawing.Size(83, 25);
            this.FormSettingsSyncTimeButton.TabIndex = 19;
            this.FormSettingsSyncTimeButton.Text = "立即同步(&S)";
            this.FormSettingsSyncTimeButton.UseVisualStyleBackColor = true;
            this.FormSettingsSyncTimeButton.Click += new System.EventHandler(this.FormSettingsSyncTimeButton_Click);
            // 
            // FormSettingsSetExamNameText
            // 
            this.FormSettingsSetExamNameText.Location = new System.Drawing.Point(6, 22);
            this.FormSettingsSetExamNameText.MaxLength = 99;
            this.FormSettingsSetExamNameText.Name = "FormSettingsSetExamNameText";
            this.FormSettingsSetExamNameText.Size = new System.Drawing.Size(357, 23);
            this.FormSettingsSetExamNameText.TabIndex = 34;
            this.FormSettingsSetExamNameText.TextChanged += new System.EventHandler(this.FormSettingsSetExamNameText_TextChanged);
            // 
            // BtnRestart
            // 
            this.BtnRestart.AutoSize = true;
            this.BtnRestart.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BtnRestart.Location = new System.Drawing.Point(9, 54);
            this.BtnRestart.Name = "BtnRestart";
            this.BtnRestart.Size = new System.Drawing.Size(84, 25);
            this.BtnRestart.TabIndex = 36;
            this.BtnRestart.Text = "点击重启(&R)";
            this.BtnRestart.UseVisualStyleBackColor = true;
            this.BtnRestart.Click += new System.EventHandler(this.BtnRestart_Click);
            this.BtnRestart.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BtnRestart_MouseDown);
            // 
            // StartTimePicker
            // 
            this.StartTimePicker.Checked = false;
            this.StartTimePicker.CustomFormat = "yyyy-MM-dd dddd HH:mm:ss";
            this.StartTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.StartTimePicker.Location = new System.Drawing.Point(6, 22);
            this.StartTimePicker.Name = "StartTimePicker";
            this.StartTimePicker.Size = new System.Drawing.Size(357, 23);
            this.StartTimePicker.TabIndex = 38;
            this.StartTimePicker.Value = new System.DateTime(2024, 1, 1, 0, 0, 0, 0);
            // 
            // EndTimePicker
            // 
            this.EndTimePicker.CustomFormat = "yyyy-MM-dd dddd HH:mm:ss";
            this.EndTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.EndTimePicker.Location = new System.Drawing.Point(6, 22);
            this.EndTimePicker.Name = "EndTimePicker";
            this.EndTimePicker.Size = new System.Drawing.Size(357, 23);
            this.EndTimePicker.TabIndex = 39;
            this.EndTimePicker.Value = new System.DateTime(2024, 1, 1, 0, 0, 0, 0);
            // 
            // FormSettingsApply
            // 
            this.FormSettingsApply.AutoSize = true;
            this.FormSettingsApply.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.FormSettingsApply.Location = new System.Drawing.Point(291, 266);
            this.FormSettingsApply.Name = "FormSettingsApply";
            this.FormSettingsApply.Size = new System.Drawing.Size(57, 25);
            this.FormSettingsApply.TabIndex = 16;
            this.FormSettingsApply.Text = "保存(&S)";
            this.FormSettingsApply.UseVisualStyleBackColor = true;
            this.FormSettingsApply.Click += new System.EventHandler(this.FormSettingsApply_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(429, 325);
            this.tabControl1.TabIndex = 40;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox7);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.groupBox6);
            this.tabPage1.Controls.Add(this.groupBox5);
            this.tabPage1.Controls.Add(this.groupBox4);
            this.tabPage1.Controls.Add(this.FormSettingsApply);
            this.tabPage1.Controls.Add(this.FormSettingsCloseMain);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(421, 297);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "基本";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.EndTimePicker);
            this.groupBox6.Location = new System.Drawing.Point(8, 194);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(405, 58);
            this.groupBox6.TabIndex = 42;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "考试结束日期和时间";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.StartTimePicker);
            this.groupBox5.Location = new System.Drawing.Point(8, 130);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(405, 58);
            this.groupBox5.TabIndex = 41;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "考试开始日期和时间";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.LblCounter);
            this.groupBox4.Controls.Add(this.FormSettingsSetExamNameText);
            this.groupBox4.Location = new System.Drawing.Point(8, 66);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(405, 58);
            this.groupBox4.TabIndex = 40;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "考试名称 (不超过15字)";
            // 
            // LblCounter
            // 
            this.LblCounter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.LblCounter.AutoSize = true;
            this.LblCounter.Location = new System.Drawing.Point(364, 24);
            this.LblCounter.Name = "LblCounter";
            this.LblCounter.Size = new System.Drawing.Size(30, 15);
            this.LblCounter.TabIndex = 35;
            this.LblCounter.Text = "0/15";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox3);
            this.tabPage2.Controls.Add(this.groupBox2);
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(421, 297);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "高级";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.BtnRestart);
            this.groupBox3.Location = new System.Drawing.Point(6, 155);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(407, 85);
            this.groupBox3.TabIndex = 45;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "重启倒计时";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.FormSettingsSyncTimeButton);
            this.groupBox2.Location = new System.Drawing.Point(6, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(407, 143);
            this.groupBox2.TabIndex = 44;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "同步网络时钟";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.FormSettingsSetStartupCheck);
            this.groupBox1.Location = new System.Drawing.Point(8, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(202, 54);
            this.groupBox1.TabIndex = 44;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "开机自启动";
            // 
            // FormSettingsSetStartupCheck
            // 
            this.FormSettingsSetStartupCheck.AutoSize = true;
            this.FormSettingsSetStartupCheck.Location = new System.Drawing.Point(6, 22);
            this.FormSettingsSetStartupCheck.Name = "FormSettingsSetStartupCheck";
            this.FormSettingsSetStartupCheck.Size = new System.Drawing.Size(184, 19);
            this.FormSettingsSetStartupCheck.TabIndex = 18;
            this.FormSettingsSetStartupCheck.Text = "允许开机自动启动倒计时(&B)";
            this.FormSettingsSetStartupCheck.UseVisualStyleBackColor = true;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.chkSetTopMost);
            this.groupBox7.Location = new System.Drawing.Point(217, 6);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(196, 54);
            this.groupBox7.TabIndex = 45;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "顶置显示";
            // 
            // chkSetTopMost
            // 
            this.chkSetTopMost.AutoSize = true;
            this.chkSetTopMost.Checked = true;
            this.chkSetTopMost.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSetTopMost.Location = new System.Drawing.Point(6, 22);
            this.chkSetTopMost.Name = "chkSetTopMost";
            this.chkSetTopMost.Size = new System.Drawing.Size(183, 19);
            this.chkSetTopMost.TabIndex = 0;
            this.chkSetTopMost.Text = "允许倒计时显示到最上层(&T)";
            this.chkSetTopMost.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(390, 15);
            this.label1.TabIndex = 20;
            this.label1.Text = "如果你发现当前系统时间与实际有所差异, 可以点击此按钮来同步网";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 15);
            this.label2.TabIndex = 21;
            this.label2.Text = "络时间。";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(6, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(383, 15);
            this.label3.TabIndex = 22;
            this.label3.Text = "注意: 此项会将系统的 NTP 服务器永久设置为 ntp1.aliyun.com, 相较";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(6, 64);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(354, 15);
            this.label4.TabIndex = 23;
            this.label4.Text = "于系统自带的 time.windows.com 拥有更低的延迟, 并且还会将";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(6, 79);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(389, 15);
            this.label5.TabIndex = 24;
            this.label5.Text = "Windows Time 服务设置为 自动, 请谨慎操作, 或者你也可以到控制面";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(6, 94);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(124, 15);
            this.label6.TabIndex = 25;
            this.label6.Text = "板手动设置并同步。";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 19);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(390, 15);
            this.label7.TabIndex = 37;
            this.label7.Text = "如果你更改了屏幕缩放或者分辨率, 可以点击此按钮来重启倒计时以";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 36);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(176, 15);
            this.label8.TabIndex = 38;
            this.label8.Text = "确保显示的文字不会变模糊。";
            // 
            // FormSettings
            // 
            this.AcceptButton = this.FormSettingsApply;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(429, 325);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "FormSettings";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "设置 - 高考倒计时";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormSettings_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormSettings_FormClosed);
            this.Load += new System.EventHandler(this.FormSettings_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormSettings_KeyDown);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button FormSettingsCloseMain;
        private System.Windows.Forms.Button FormSettingsSyncTimeButton;
        private System.Windows.Forms.TextBox FormSettingsSetExamNameText;
        private System.Windows.Forms.Button BtnRestart;
        private System.Windows.Forms.DateTimePicker StartTimePicker;
        private System.Windows.Forms.DateTimePicker EndTimePicker;
        private System.Windows.Forms.Button FormSettingsApply;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label LblCounter;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox FormSettingsSetStartupCheck;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.CheckBox chkSetTopMost;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}