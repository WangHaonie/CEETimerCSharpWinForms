namespace CEETimerCSharpWinForms.Dialogs
{
    partial class RuleDialog
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
            this.LabelForeColor = new System.Windows.Forms.Label();
            this.LabelBackColor = new System.Windows.Forms.Label();
            this.LabelChars07 = new System.Windows.Forms.Label();
            this.LabelChars06 = new System.Windows.Forms.Label();
            this.ComboBoxRuleType = new System.Windows.Forms.ComboBox();
            this.LabelChars01 = new System.Windows.Forms.Label();
            this.NudSeconds = new System.Windows.Forms.NumericUpDown();
            this.NudMinutes = new System.Windows.Forms.NumericUpDown();
            this.LabelChars05 = new System.Windows.Forms.Label();
            this.LabelChars04 = new System.Windows.Forms.Label();
            this.LabelChars03 = new System.Windows.Forms.Label();
            this.NudHours = new System.Windows.Forms.NumericUpDown();
            this.LabelChars02 = new System.Windows.Forms.Label();
            this.NudDays = new System.Windows.Forms.NumericUpDown();
            this.LabelPreviewColor = new System.Windows.Forms.Label();
            this.TextBoxCustomText = new System.Windows.Forms.TextBox();
            this.LabelCustomText = new System.Windows.Forms.Label();
            this.LabelCustomInfo = new System.Windows.Forms.Label();
            this.PanelMain = new System.Windows.Forms.Panel();
            this.LinkReset = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.NudSeconds)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NudMinutes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NudHours)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NudDays)).BeginInit();
            this.PanelMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // ButtonA
            // 
            this.ButtonA.Name = "ButtonA";
            this.ButtonA.Size = new System.Drawing.Size(75, 23);
            this.ButtonA.TabIndex = 7;
            this.ButtonA.Text = "确定(&O)";
            this.ButtonA.UseVisualStyleBackColor = true;
            // 
            // ButtonB
            // 
            this.ButtonB.Name = "ButtonB";
            this.ButtonB.Size = new System.Drawing.Size(75, 23);
            this.ButtonB.TabIndex = 6;
            this.ButtonB.Text = "取消(&C)";
            this.ButtonB.UseVisualStyleBackColor = true;
            // 
            // LabelForeColor
            // 
            this.LabelForeColor.AutoSize = true;
            this.LabelForeColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LabelForeColor.Location = new System.Drawing.Point(77, 35);
            this.LabelForeColor.Name = "LabelForeColor";
            this.LabelForeColor.Size = new System.Drawing.Size(39, 17);
            this.LabelForeColor.TabIndex = 7;
            this.LabelForeColor.Text = "          ";
            this.LabelForeColor.Click += new System.EventHandler(this.ColorLabels_Click);
            // 
            // LabelBackColor
            // 
            this.LabelBackColor.AutoSize = true;
            this.LabelBackColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LabelBackColor.Location = new System.Drawing.Point(198, 35);
            this.LabelBackColor.Name = "LabelBackColor";
            this.LabelBackColor.Size = new System.Drawing.Size(39, 17);
            this.LabelBackColor.TabIndex = 6;
            this.LabelBackColor.Text = "          ";
            this.LabelBackColor.Click += new System.EventHandler(this.ColorLabels_Click);
            // 
            // LabelChars07
            // 
            this.LabelChars07.AutoSize = true;
            this.LabelChars07.Location = new System.Drawing.Point(118, 35);
            this.LabelChars07.Name = "LabelChars07";
            this.LabelChars07.Size = new System.Drawing.Size(78, 15);
            this.LabelChars07.TabIndex = 13;
            this.LabelChars07.Text = ", 背景颜色为";
            // 
            // LabelChars06
            // 
            this.LabelChars06.AutoSize = true;
            this.LabelChars06.Location = new System.Drawing.Point(3, 35);
            this.LabelChars06.Name = "LabelChars06";
            this.LabelChars06.Size = new System.Drawing.Size(72, 15);
            this.LabelChars06.TabIndex = 12;
            this.LabelChars06.Text = "文字颜色为";
            // 
            // ComboBoxRuleType
            // 
            this.ComboBoxRuleType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxRuleType.FormattingEnabled = true;
            this.ComboBoxRuleType.Location = new System.Drawing.Point(77, 6);
            this.ComboBoxRuleType.Name = "ComboBoxRuleType";
            this.ComboBoxRuleType.Size = new System.Drawing.Size(80, 23);
            this.ComboBoxRuleType.TabIndex = 11;
            this.ComboBoxRuleType.SelectedIndexChanged += new System.EventHandler(this.ComboBoxRuleType_SelectedIndexChanged);
            // 
            // LabelChars01
            // 
            this.LabelChars01.AutoSize = true;
            this.LabelChars01.Location = new System.Drawing.Point(3, 9);
            this.LabelChars01.Name = "LabelChars01";
            this.LabelChars01.Size = new System.Drawing.Size(72, 15);
            this.LabelChars01.TabIndex = 10;
            this.LabelChars01.Text = "当距离考试";
            // 
            // NudSeconds
            // 
            this.NudSeconds.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.NudSeconds.Location = new System.Drawing.Point(370, 7);
            this.NudSeconds.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.NudSeconds.Name = "NudSeconds";
            this.NudSeconds.Size = new System.Drawing.Size(39, 23);
            this.NudSeconds.TabIndex = 9;
            this.NudSeconds.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // NudMinutes
            // 
            this.NudMinutes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.NudMinutes.Location = new System.Drawing.Point(307, 7);
            this.NudMinutes.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.NudMinutes.Name = "NudMinutes";
            this.NudMinutes.Size = new System.Drawing.Size(39, 23);
            this.NudMinutes.TabIndex = 8;
            this.NudMinutes.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // LabelChars05
            // 
            this.LabelChars05.AutoSize = true;
            this.LabelChars05.Location = new System.Drawing.Point(411, 9);
            this.LabelChars05.Name = "LabelChars05";
            this.LabelChars05.Size = new System.Drawing.Size(39, 15);
            this.LabelChars05.TabIndex = 7;
            this.LabelChars05.Text = "秒 时,";
            // 
            // LabelChars04
            // 
            this.LabelChars04.AutoSize = true;
            this.LabelChars04.Location = new System.Drawing.Point(348, 9);
            this.LabelChars04.Name = "LabelChars04";
            this.LabelChars04.Size = new System.Drawing.Size(20, 15);
            this.LabelChars04.TabIndex = 5;
            this.LabelChars04.Text = "分";
            // 
            // LabelChars03
            // 
            this.LabelChars03.AutoSize = true;
            this.LabelChars03.Location = new System.Drawing.Point(285, 9);
            this.LabelChars03.Name = "LabelChars03";
            this.LabelChars03.Size = new System.Drawing.Size(20, 15);
            this.LabelChars03.TabIndex = 3;
            this.LabelChars03.Text = "时";
            // 
            // NudHours
            // 
            this.NudHours.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.NudHours.Location = new System.Drawing.Point(244, 7);
            this.NudHours.Maximum = new decimal(new int[] {
            23,
            0,
            0,
            0});
            this.NudHours.Name = "NudHours";
            this.NudHours.Size = new System.Drawing.Size(39, 23);
            this.NudHours.TabIndex = 2;
            this.NudHours.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // LabelChars02
            // 
            this.LabelChars02.AutoSize = true;
            this.LabelChars02.Location = new System.Drawing.Point(222, 9);
            this.LabelChars02.Name = "LabelChars02";
            this.LabelChars02.Size = new System.Drawing.Size(20, 15);
            this.LabelChars02.TabIndex = 1;
            this.LabelChars02.Text = "天";
            // 
            // NudDays
            // 
            this.NudDays.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.NudDays.Location = new System.Drawing.Point(159, 7);
            this.NudDays.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.NudDays.Name = "NudDays";
            this.NudDays.Size = new System.Drawing.Size(61, 23);
            this.NudDays.TabIndex = 0;
            this.NudDays.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // LabelPreviewColor
            // 
            this.LabelPreviewColor.AutoSize = true;
            this.LabelPreviewColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LabelPreviewColor.Location = new System.Drawing.Point(385, 35);
            this.LabelPreviewColor.Name = "LabelPreviewColor";
            this.LabelPreviewColor.Size = new System.Drawing.Size(61, 17);
            this.LabelPreviewColor.TabIndex = 20;
            this.LabelPreviewColor.Text = "颜色效果预览";
            // 
            // TextBoxCustomText
            // 
            this.TextBoxCustomText.Location = new System.Drawing.Point(75, 57);
            this.TextBoxCustomText.Name = "TextBoxCustomText";
            this.TextBoxCustomText.Size = new System.Drawing.Size(336, 23);
            this.TextBoxCustomText.TabIndex = 21;
            this.TextBoxCustomText.TextChanged += new System.EventHandler(this.TextBoxCustomText_TextChanged);
            // 
            // LabelCustomText
            // 
            this.LabelCustomText.AutoSize = true;
            this.LabelCustomText.Location = new System.Drawing.Point(3, 61);
            this.LabelCustomText.Name = "LabelCustomText";
            this.LabelCustomText.Size = new System.Drawing.Size(72, 15);
            this.LabelCustomText.TabIndex = 22;
            this.LabelCustomText.Text = "自定义文本";
            // 
            // LabelCustomInfo
            // 
            this.LabelCustomInfo.AutoSize = true;
            this.LabelCustomInfo.Location = new System.Drawing.Point(3, 84);
            this.LabelCustomInfo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabelCustomInfo.Name = "LabelCustomInfo";
            this.LabelCustomInfo.Size = new System.Drawing.Size(458, 45);
            this.LabelCustomInfo.TabIndex = 23;
            this.LabelCustomInfo.Text = "用占位符表示变量: {x}-考试名称 {d}-天 {h}-时 {m}-分 {s}-秒 {rd}-四舍五入的天数 {th}-总小时数 {tm}-总分钟数 {ts}-总秒数。\r\n注意: 自定义文本仅在勾选了 设置>显示>自定义文本 时才会生效。";
            // 
            // PanelMain
            // 
            this.PanelMain.Controls.Add(this.LinkReset);
            this.PanelMain.Controls.Add(this.LabelPreviewColor);
            this.PanelMain.Controls.Add(this.LabelCustomInfo);
            this.PanelMain.Controls.Add(this.NudMinutes);
            this.PanelMain.Controls.Add(this.LabelCustomText);
            this.PanelMain.Controls.Add(this.TextBoxCustomText);
            this.PanelMain.Controls.Add(this.LabelChars07);
            this.PanelMain.Controls.Add(this.NudHours);
            this.PanelMain.Controls.Add(this.LabelChars03);
            this.PanelMain.Controls.Add(this.LabelChars06);
            this.PanelMain.Controls.Add(this.LabelChars05);
            this.PanelMain.Controls.Add(this.LabelBackColor);
            this.PanelMain.Controls.Add(this.NudSeconds);
            this.PanelMain.Controls.Add(this.LabelChars01);
            this.PanelMain.Controls.Add(this.LabelChars02);
            this.PanelMain.Controls.Add(this.NudDays);
            this.PanelMain.Controls.Add(this.LabelChars04);
            this.PanelMain.Controls.Add(this.LabelForeColor);
            this.PanelMain.Controls.Add(this.ComboBoxRuleType);
            this.PanelMain.Location = new System.Drawing.Point(0, 0);
            this.PanelMain.Name = "PanelMain";
            this.PanelMain.Size = new System.Drawing.Size(453, 130);
            this.PanelMain.TabIndex = 24;
            // 
            // LinkReset
            // 
            this.LinkReset.AutoSize = true;
            this.LinkReset.Location = new System.Drawing.Point(413, 61);
            this.LinkReset.Name = "LinkReset";
            this.LinkReset.Size = new System.Drawing.Size(33, 15);
            this.LinkReset.TabIndex = 24;
            this.LinkReset.TabStop = true;
            this.LinkReset.Text = "重置";
            this.LinkReset.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkReset_LinkClicked);
            // 
            // RuleDialog
            // 
            this.ClientSize = new System.Drawing.Size(456, 87);
            this.Controls.Add(this.LabelPreviewColor);
            this.Controls.Add(this.PanelMain);
            this.Name = "RuleDialog";
            this.Text = "自定义规则 - 高考倒计时";
            ((System.ComponentModel.ISupportInitialize)(this.NudSeconds)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NudMinutes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NudHours)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NudDays)).EndInit();
            this.PanelMain.ResumeLayout(false);
            this.PanelMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label LabelForeColor;
        private System.Windows.Forms.Label LabelBackColor;
        private System.Windows.Forms.Label LabelChars07;
        private System.Windows.Forms.Label LabelChars06;
        private System.Windows.Forms.ComboBox ComboBoxRuleType;
        private System.Windows.Forms.Label LabelChars01;
        private System.Windows.Forms.NumericUpDown NudSeconds;
        private System.Windows.Forms.NumericUpDown NudMinutes;
        private System.Windows.Forms.Label LabelChars05;
        private System.Windows.Forms.Label LabelChars04;
        private System.Windows.Forms.Label LabelChars03;
        private System.Windows.Forms.NumericUpDown NudHours;
        private System.Windows.Forms.Label LabelChars02;
        private System.Windows.Forms.NumericUpDown NudDays;
        private System.Windows.Forms.Label LabelPreviewColor;
        private System.Windows.Forms.Label LabelCustomText;
        private System.Windows.Forms.TextBox TextBoxCustomText;
        private System.Windows.Forms.Label LabelCustomInfo;
        private System.Windows.Forms.LinkLabel LinkReset;
    }
}