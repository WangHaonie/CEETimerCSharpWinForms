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
            this.ButtonClose = new System.Windows.Forms.Button();
            this.ButtonSyncTime = new System.Windows.Forms.Button();
            this.TextBoxExamName = new System.Windows.Forms.TextBox();
            this.ButtonRestart = new System.Windows.Forms.Button();
            this.DTPExamStart = new System.Windows.Forms.DateTimePicker();
            this.DTPExamEnd = new System.Windows.Forms.DateTimePicker();
            this.ButtonSave = new System.Windows.Forms.Button();
            this.TabControlMain = new System.Windows.Forms.TabControl();
            this.TabPageGeneral = new System.Windows.Forms.TabPage();
            this.GBoxStartup = new System.Windows.Forms.GroupBox();
            this.CheckBoxSetTopMost = new System.Windows.Forms.CheckBox();
            this.CheckBoxStartup = new System.Windows.Forms.CheckBox();
            this.GBoxExamEnd = new System.Windows.Forms.GroupBox();
            this.GBoxExamStart = new System.Windows.Forms.GroupBox();
            this.GBoxExamName = new System.Windows.Forms.GroupBox();
            this.LabelExamNameCounter = new System.Windows.Forms.Label();
            this.TabPageDisplay = new System.Windows.Forms.TabPage();
            this.GBoxContent = new System.Windows.Forms.GroupBox();
            this.CheckBoxSetUniTopMost = new System.Windows.Forms.CheckBox();
            this.CheckBoxSetNoPast = new System.Windows.Forms.CheckBox();
            this.CheckBoxSetNoStart = new System.Windows.Forms.CheckBox();
            this.CheckBoxSetRounding = new System.Windows.Forms.CheckBox();
            this.CheckBoxSetDaysOnly = new System.Windows.Forms.CheckBox();
            this.GBoxChangeFont = new System.Windows.Forms.GroupBox();
            this.ButtonRestoreFont = new System.Windows.Forms.Button();
            this.ButtonChooseFont = new System.Windows.Forms.Button();
            this.LabelPreviewFont = new System.Windows.Forms.Label();
            this.LabelFontInfo = new System.Windows.Forms.Label();
            this.LabelLine11 = new System.Windows.Forms.Label();
            this.TabPageAdvanced = new System.Windows.Forms.TabPage();
            this.GBoxSetDragable = new System.Windows.Forms.GroupBox();
            this.CheckBoxEnableDragable = new System.Windows.Forms.CheckBox();
            this.LabelLine12 = new System.Windows.Forms.Label();
            this.GBoxMO = new System.Windows.Forms.GroupBox();
            this.CheckBoxEnableMO = new System.Windows.Forms.CheckBox();
            this.LabelLine4 = new System.Windows.Forms.Label();
            this.LabelLine3 = new System.Windows.Forms.Label();
            this.GBoxVDM = new System.Windows.Forms.GroupBox();
            this.CheckBoxEnableVDM = new System.Windows.Forms.CheckBox();
            this.LabelLine2 = new System.Windows.Forms.Label();
            this.LabelLine1 = new System.Windows.Forms.Label();
            this.TabPageOther = new System.Windows.Forms.TabPage();
            this.GBoxRestart = new System.Windows.Forms.GroupBox();
            this.LabelLine10 = new System.Windows.Forms.Label();
            this.LabelLine9 = new System.Windows.Forms.Label();
            this.GBoxSyncTime = new System.Windows.Forms.GroupBox();
            this.LabelLine8 = new System.Windows.Forms.Label();
            this.LabelLine7 = new System.Windows.Forms.Label();
            this.LabelLine6 = new System.Windows.Forms.Label();
            this.LabelLine5 = new System.Windows.Forms.Label();
            this.TabControlMain.SuspendLayout();
            this.TabPageGeneral.SuspendLayout();
            this.GBoxStartup.SuspendLayout();
            this.GBoxExamEnd.SuspendLayout();
            this.GBoxExamStart.SuspendLayout();
            this.GBoxExamName.SuspendLayout();
            this.TabPageDisplay.SuspendLayout();
            this.GBoxContent.SuspendLayout();
            this.GBoxChangeFont.SuspendLayout();
            this.TabPageAdvanced.SuspendLayout();
            this.GBoxSetDragable.SuspendLayout();
            this.GBoxMO.SuspendLayout();
            this.GBoxVDM.SuspendLayout();
            this.TabPageOther.SuspendLayout();
            this.GBoxRestart.SuspendLayout();
            this.GBoxSyncTime.SuspendLayout();
            this.SuspendLayout();
            // 
            // ButtonClose
            // 
            this.ButtonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.ButtonClose.Location = new System.Drawing.Point(361, 297);
            this.ButtonClose.Name = "ButtonClose";
            this.ButtonClose.Size = new System.Drawing.Size(75, 25);
            this.ButtonClose.TabIndex = 17;
            this.ButtonClose.Text = "关闭(&C)";
            this.ButtonClose.UseVisualStyleBackColor = true;
            this.ButtonClose.Click += new System.EventHandler(this.ButtonClose_Click);
            // 
            // ButtonSyncTime
            // 
            this.ButtonSyncTime.AutoSize = true;
            this.ButtonSyncTime.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ButtonSyncTime.Location = new System.Drawing.Point(9, 82);
            this.ButtonSyncTime.Name = "ButtonSyncTime";
            this.ButtonSyncTime.Size = new System.Drawing.Size(83, 25);
            this.ButtonSyncTime.TabIndex = 19;
            this.ButtonSyncTime.Text = "立即同步(&S)";
            this.ButtonSyncTime.UseVisualStyleBackColor = true;
            this.ButtonSyncTime.Click += new System.EventHandler(this.ButtonSyncTime_Click);
            // 
            // TextBoxExamName
            // 
            this.TextBoxExamName.Location = new System.Drawing.Point(6, 22);
            this.TextBoxExamName.MaxLength = 99;
            this.TextBoxExamName.Name = "TextBoxExamName";
            this.TextBoxExamName.Size = new System.Drawing.Size(357, 23);
            this.TextBoxExamName.TabIndex = 34;
            this.TextBoxExamName.TextChanged += new System.EventHandler(this.TextBoxExamName_TextChanged);
            // 
            // ButtonRestart
            // 
            this.ButtonRestart.AutoSize = true;
            this.ButtonRestart.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ButtonRestart.Location = new System.Drawing.Point(9, 54);
            this.ButtonRestart.Name = "ButtonRestart";
            this.ButtonRestart.Size = new System.Drawing.Size(6, 6);
            this.ButtonRestart.TabIndex = 36;
            this.ButtonRestart.UseVisualStyleBackColor = true;
            this.ButtonRestart.Click += new System.EventHandler(this.ButtonRestart_Click);
            this.ButtonRestart.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ButtonRestart_MouseDown);
            // 
            // DTPExamStart
            // 
            this.DTPExamStart.Checked = false;
            this.DTPExamStart.CustomFormat = "yyyy-MM-dd dddd HH:mm:ss";
            this.DTPExamStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DTPExamStart.Location = new System.Drawing.Point(6, 22);
            this.DTPExamStart.Name = "DTPExamStart";
            this.DTPExamStart.Size = new System.Drawing.Size(357, 23);
            this.DTPExamStart.TabIndex = 38;
            this.DTPExamStart.ValueChanged += new System.EventHandler(this.SettingsChanged);
            // 
            // DTPExamEnd
            // 
            this.DTPExamEnd.Checked = false;
            this.DTPExamEnd.CustomFormat = "yyyy-MM-dd dddd HH:mm:ss";
            this.DTPExamEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DTPExamEnd.Location = new System.Drawing.Point(6, 22);
            this.DTPExamEnd.Name = "DTPExamEnd";
            this.DTPExamEnd.Size = new System.Drawing.Size(357, 23);
            this.DTPExamEnd.TabIndex = 39;
            this.DTPExamEnd.ValueChanged += new System.EventHandler(this.SettingsChanged);
            // 
            // ButtonSave
            // 
            this.ButtonSave.Location = new System.Drawing.Point(280, 297);
            this.ButtonSave.Name = "ButtonSave";
            this.ButtonSave.Size = new System.Drawing.Size(75, 25);
            this.ButtonSave.TabIndex = 16;
            this.ButtonSave.Text = "保存(&S)";
            this.ButtonSave.UseVisualStyleBackColor = true;
            this.ButtonSave.Click += new System.EventHandler(this.ButtonSave_Click);
            // 
            // TabControlMain
            // 
            this.TabControlMain.Controls.Add(this.TabPageGeneral);
            this.TabControlMain.Controls.Add(this.TabPageDisplay);
            this.TabControlMain.Controls.Add(this.TabPageAdvanced);
            this.TabControlMain.Controls.Add(this.TabPageOther);
            this.TabControlMain.Location = new System.Drawing.Point(7, 4);
            this.TabControlMain.Name = "TabControlMain";
            this.TabControlMain.SelectedIndex = 0;
            this.TabControlMain.Size = new System.Drawing.Size(429, 287);
            this.TabControlMain.TabIndex = 40;
            // 
            // TabPageGeneral
            // 
            this.TabPageGeneral.Controls.Add(this.GBoxStartup);
            this.TabPageGeneral.Controls.Add(this.GBoxExamEnd);
            this.TabPageGeneral.Controls.Add(this.GBoxExamStart);
            this.TabPageGeneral.Controls.Add(this.GBoxExamName);
            this.TabPageGeneral.Location = new System.Drawing.Point(4, 24);
            this.TabPageGeneral.Name = "TabPageGeneral";
            this.TabPageGeneral.Padding = new System.Windows.Forms.Padding(3);
            this.TabPageGeneral.Size = new System.Drawing.Size(421, 259);
            this.TabPageGeneral.TabIndex = 0;
            this.TabPageGeneral.Text = "基本";
            this.TabPageGeneral.UseVisualStyleBackColor = true;
            // 
            // GBoxStartup
            // 
            this.GBoxStartup.Controls.Add(this.CheckBoxSetTopMost);
            this.GBoxStartup.Controls.Add(this.CheckBoxStartup);
            this.GBoxStartup.Location = new System.Drawing.Point(7, 6);
            this.GBoxStartup.Name = "GBoxStartup";
            this.GBoxStartup.Size = new System.Drawing.Size(405, 54);
            this.GBoxStartup.TabIndex = 44;
            this.GBoxStartup.TabStop = false;
            this.GBoxStartup.Text = "常用";
            // 
            // CheckBoxSetTopMost
            // 
            this.CheckBoxSetTopMost.AutoSize = true;
            this.CheckBoxSetTopMost.Checked = true;
            this.CheckBoxSetTopMost.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CheckBoxSetTopMost.Location = new System.Drawing.Point(183, 22);
            this.CheckBoxSetTopMost.Name = "CheckBoxSetTopMost";
            this.CheckBoxSetTopMost.Size = new System.Drawing.Size(170, 19);
            this.CheckBoxSetTopMost.TabIndex = 0;
            this.CheckBoxSetTopMost.Text = "将倒计时显示到最上层(&T)";
            this.CheckBoxSetTopMost.UseVisualStyleBackColor = true;
            this.CheckBoxSetTopMost.CheckedChanged += new System.EventHandler(this.CheckBoxSetTopMost_CheckedChanged);
            // 
            // CheckBoxStartup
            // 
            this.CheckBoxStartup.AutoSize = true;
            this.CheckBoxStartup.Location = new System.Drawing.Point(6, 22);
            this.CheckBoxStartup.Name = "CheckBoxStartup";
            this.CheckBoxStartup.Size = new System.Drawing.Size(171, 19);
            this.CheckBoxStartup.TabIndex = 18;
            this.CheckBoxStartup.Text = "开机时自动启动倒计时(&B)";
            this.CheckBoxStartup.UseVisualStyleBackColor = true;
            this.CheckBoxStartup.CheckedChanged += new System.EventHandler(this.SettingsChanged);
            // 
            // GBoxExamEnd
            // 
            this.GBoxExamEnd.Controls.Add(this.DTPExamEnd);
            this.GBoxExamEnd.Location = new System.Drawing.Point(7, 194);
            this.GBoxExamEnd.Name = "GBoxExamEnd";
            this.GBoxExamEnd.Size = new System.Drawing.Size(405, 58);
            this.GBoxExamEnd.TabIndex = 42;
            this.GBoxExamEnd.TabStop = false;
            this.GBoxExamEnd.Text = "考试结束日期和时间";
            // 
            // GBoxExamStart
            // 
            this.GBoxExamStart.Controls.Add(this.DTPExamStart);
            this.GBoxExamStart.Location = new System.Drawing.Point(7, 130);
            this.GBoxExamStart.Name = "GBoxExamStart";
            this.GBoxExamStart.Size = new System.Drawing.Size(405, 58);
            this.GBoxExamStart.TabIndex = 41;
            this.GBoxExamStart.TabStop = false;
            this.GBoxExamStart.Text = "考试开始日期和时间";
            // 
            // GBoxExamName
            // 
            this.GBoxExamName.Controls.Add(this.LabelExamNameCounter);
            this.GBoxExamName.Controls.Add(this.TextBoxExamName);
            this.GBoxExamName.Location = new System.Drawing.Point(7, 66);
            this.GBoxExamName.Name = "GBoxExamName";
            this.GBoxExamName.Size = new System.Drawing.Size(405, 58);
            this.GBoxExamName.TabIndex = 40;
            this.GBoxExamName.TabStop = false;
            this.GBoxExamName.Text = "考试名称 (2~15字)";
            // 
            // LabelExamNameCounter
            // 
            this.LabelExamNameCounter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.LabelExamNameCounter.AutoSize = true;
            this.LabelExamNameCounter.Location = new System.Drawing.Point(364, 24);
            this.LabelExamNameCounter.Name = "LabelExamNameCounter";
            this.LabelExamNameCounter.Size = new System.Drawing.Size(30, 15);
            this.LabelExamNameCounter.TabIndex = 35;
            this.LabelExamNameCounter.Text = "0/15";
            // 
            // TabPageDisplay
            // 
            this.TabPageDisplay.Controls.Add(this.GBoxContent);
            this.TabPageDisplay.Controls.Add(this.GBoxChangeFont);
            this.TabPageDisplay.Location = new System.Drawing.Point(4, 24);
            this.TabPageDisplay.Name = "TabPageDisplay";
            this.TabPageDisplay.Size = new System.Drawing.Size(421, 259);
            this.TabPageDisplay.TabIndex = 3;
            this.TabPageDisplay.Text = "显示";
            this.TabPageDisplay.UseVisualStyleBackColor = true;
            // 
            // GBoxContent
            // 
            this.GBoxContent.Controls.Add(this.CheckBoxSetUniTopMost);
            this.GBoxContent.Controls.Add(this.CheckBoxSetNoPast);
            this.GBoxContent.Controls.Add(this.CheckBoxSetNoStart);
            this.GBoxContent.Controls.Add(this.CheckBoxSetRounding);
            this.GBoxContent.Controls.Add(this.CheckBoxSetDaysOnly);
            this.GBoxContent.Location = new System.Drawing.Point(7, 6);
            this.GBoxContent.Name = "GBoxContent";
            this.GBoxContent.Size = new System.Drawing.Size(405, 120);
            this.GBoxContent.TabIndex = 1;
            this.GBoxContent.TabStop = false;
            this.GBoxContent.Text = "倒计时内容";
            // 
            // CheckBoxSetUniTopMost
            // 
            this.CheckBoxSetUniTopMost.AutoSize = true;
            this.CheckBoxSetUniTopMost.Location = new System.Drawing.Point(6, 97);
            this.CheckBoxSetUniTopMost.Name = "CheckBoxSetUniTopMost";
            this.CheckBoxSetUniTopMost.Size = new System.Drawing.Size(247, 19);
            this.CheckBoxSetUniTopMost.TabIndex = 4;
            this.CheckBoxSetUniTopMost.Text = "顶置属性同样适用于本程序的其他窗口";
            this.CheckBoxSetUniTopMost.UseVisualStyleBackColor = true;
            this.CheckBoxSetUniTopMost.CheckedChanged += new System.EventHandler(this.SettingsChanged);
            // 
            // CheckBoxSetNoPast
            // 
            this.CheckBoxSetNoPast.AutoSize = true;
            this.CheckBoxSetNoPast.Location = new System.Drawing.Point(6, 72);
            this.CheckBoxSetNoPast.Name = "CheckBoxSetNoPast";
            this.CheckBoxSetNoPast.Size = new System.Drawing.Size(315, 19);
            this.CheckBoxSetNoPast.TabIndex = 3;
            this.CheckBoxSetNoPast.Text = "不显示 \"距离...已经过去了...\" (考试已经结束了多久)";
            this.CheckBoxSetNoPast.UseVisualStyleBackColor = true;
            this.CheckBoxSetNoPast.CheckedChanged += new System.EventHandler(this.SettingsChanged);
            // 
            // CheckBoxSetNoStart
            // 
            this.CheckBoxSetNoStart.AutoSize = true;
            this.CheckBoxSetNoStart.Location = new System.Drawing.Point(6, 47);
            this.CheckBoxSetNoStart.Name = "CheckBoxSetNoStart";
            this.CheckBoxSetNoStart.Size = new System.Drawing.Size(289, 19);
            this.CheckBoxSetNoStart.TabIndex = 2;
            this.CheckBoxSetNoStart.Text = "不显示 \"距离...结束还有...\" (考试还有多久结束)";
            this.CheckBoxSetNoStart.UseVisualStyleBackColor = true;
            this.CheckBoxSetNoStart.CheckedChanged += new System.EventHandler(this.CheckBoxSetNoStart_CheckedChanged);
            // 
            // CheckBoxSetRounding
            // 
            this.CheckBoxSetRounding.AutoSize = true;
            this.CheckBoxSetRounding.Enabled = false;
            this.CheckBoxSetRounding.Location = new System.Drawing.Point(103, 22);
            this.CheckBoxSetRounding.Name = "CheckBoxSetRounding";
            this.CheckBoxSetRounding.Size = new System.Drawing.Size(182, 19);
            this.CheckBoxSetRounding.TabIndex = 1;
            this.CheckBoxSetRounding.Text = "将不足一天的时间视为一天";
            this.CheckBoxSetRounding.UseVisualStyleBackColor = true;
            this.CheckBoxSetRounding.CheckedChanged += new System.EventHandler(this.SettingsChanged);
            // 
            // CheckBoxSetDaysOnly
            // 
            this.CheckBoxSetDaysOnly.AutoSize = true;
            this.CheckBoxSetDaysOnly.Location = new System.Drawing.Point(6, 22);
            this.CheckBoxSetDaysOnly.Name = "CheckBoxSetDaysOnly";
            this.CheckBoxSetDaysOnly.Size = new System.Drawing.Size(91, 19);
            this.CheckBoxSetDaysOnly.TabIndex = 0;
            this.CheckBoxSetDaysOnly.Text = "只显示天数";
            this.CheckBoxSetDaysOnly.UseVisualStyleBackColor = true;
            this.CheckBoxSetDaysOnly.CheckedChanged += new System.EventHandler(this.CheckBoxSetDaysOnly_CheckedChanged);
            // 
            // GBoxChangeFont
            // 
            this.GBoxChangeFont.Controls.Add(this.ButtonRestoreFont);
            this.GBoxChangeFont.Controls.Add(this.ButtonChooseFont);
            this.GBoxChangeFont.Controls.Add(this.LabelPreviewFont);
            this.GBoxChangeFont.Controls.Add(this.LabelFontInfo);
            this.GBoxChangeFont.Controls.Add(this.LabelLine11);
            this.GBoxChangeFont.Location = new System.Drawing.Point(7, 132);
            this.GBoxChangeFont.Name = "GBoxChangeFont";
            this.GBoxChangeFont.Size = new System.Drawing.Size(405, 124);
            this.GBoxChangeFont.TabIndex = 0;
            this.GBoxChangeFont.TabStop = false;
            this.GBoxChangeFont.Text = "字体和大小";
            // 
            // ButtonRestoreFont
            // 
            this.ButtonRestoreFont.AutoSize = true;
            this.ButtonRestoreFont.Location = new System.Drawing.Point(98, 91);
            this.ButtonRestoreFont.Name = "ButtonRestoreFont";
            this.ButtonRestoreFont.Size = new System.Drawing.Size(85, 25);
            this.ButtonRestoreFont.TabIndex = 4;
            this.ButtonRestoreFont.Text = "恢复默认(&D)";
            this.ButtonRestoreFont.UseVisualStyleBackColor = true;
            this.ButtonRestoreFont.Click += new System.EventHandler(this.ButtonRestoreFont_Click);
            // 
            // ButtonChooseFont
            // 
            this.ButtonChooseFont.AutoSize = true;
            this.ButtonChooseFont.Location = new System.Drawing.Point(9, 91);
            this.ButtonChooseFont.Name = "ButtonChooseFont";
            this.ButtonChooseFont.Size = new System.Drawing.Size(83, 25);
            this.ButtonChooseFont.TabIndex = 2;
            this.ButtonChooseFont.Text = "选择字体(&F)";
            this.ButtonChooseFont.UseVisualStyleBackColor = true;
            this.ButtonChooseFont.Click += new System.EventHandler(this.ButtonChooseFont_Click);
            // 
            // LabelPreviewFont
            // 
            this.LabelPreviewFont.AutoSize = true;
            this.LabelPreviewFont.Location = new System.Drawing.Point(6, 49);
            this.LabelPreviewFont.Name = "LabelPreviewFont";
            this.LabelPreviewFont.Size = new System.Drawing.Size(176, 15);
            this.LabelPreviewFont.TabIndex = 3;
            this.LabelPreviewFont.Text = "字体预览AaBbCcDd0123456789";
            // 
            // LabelFontInfo
            // 
            this.LabelFontInfo.AutoSize = true;
            this.LabelFontInfo.Location = new System.Drawing.Point(6, 34);
            this.LabelFontInfo.Name = "LabelFontInfo";
            this.LabelFontInfo.Size = new System.Drawing.Size(0, 15);
            this.LabelFontInfo.TabIndex = 1;
            // 
            // LabelLine11
            // 
            this.LabelLine11.AutoSize = true;
            this.LabelLine11.Location = new System.Drawing.Point(6, 19);
            this.LabelLine11.Name = "LabelLine11";
            this.LabelLine11.Size = new System.Drawing.Size(163, 15);
            this.LabelLine11.TabIndex = 0;
            this.LabelLine11.Text = "更改倒计时的字体和大小。";
            // 
            // TabPageAdvanced
            // 
            this.TabPageAdvanced.Controls.Add(this.GBoxSetDragable);
            this.TabPageAdvanced.Controls.Add(this.GBoxMO);
            this.TabPageAdvanced.Controls.Add(this.GBoxVDM);
            this.TabPageAdvanced.Location = new System.Drawing.Point(4, 24);
            this.TabPageAdvanced.Name = "TabPageAdvanced";
            this.TabPageAdvanced.Padding = new System.Windows.Forms.Padding(3);
            this.TabPageAdvanced.Size = new System.Drawing.Size(421, 259);
            this.TabPageAdvanced.TabIndex = 2;
            this.TabPageAdvanced.Text = "高级";
            this.TabPageAdvanced.UseVisualStyleBackColor = true;
            // 
            // GBoxSetDragable
            // 
            this.GBoxSetDragable.Controls.Add(this.CheckBoxEnableDragable);
            this.GBoxSetDragable.Controls.Add(this.LabelLine12);
            this.GBoxSetDragable.Location = new System.Drawing.Point(7, 174);
            this.GBoxSetDragable.Name = "GBoxSetDragable";
            this.GBoxSetDragable.Size = new System.Drawing.Size(405, 62);
            this.GBoxSetDragable.TabIndex = 4;
            this.GBoxSetDragable.TabStop = false;
            this.GBoxSetDragable.Text = "窗口拖动";
            // 
            // CheckBoxEnableDragable
            // 
            this.CheckBoxEnableDragable.AutoSize = true;
            this.CheckBoxEnableDragable.Location = new System.Drawing.Point(9, 37);
            this.CheckBoxEnableDragable.Name = "CheckBoxEnableDragable";
            this.CheckBoxEnableDragable.Size = new System.Drawing.Size(91, 19);
            this.CheckBoxEnableDragable.TabIndex = 3;
            this.CheckBoxEnableDragable.Text = "启用此功能";
            this.CheckBoxEnableDragable.UseVisualStyleBackColor = true;
            this.CheckBoxEnableDragable.CheckedChanged += new System.EventHandler(this.SettingsChanged);
            // 
            // LabelLine12
            // 
            this.LabelLine12.AutoSize = true;
            this.LabelLine12.Location = new System.Drawing.Point(6, 19);
            this.LabelLine12.Name = "LabelLine12";
            this.LabelLine12.Size = new System.Drawing.Size(345, 15);
            this.LabelLine12.TabIndex = 0;
            this.LabelLine12.Text = "将允许倒计时窗口被拖动。目前暂时无法记住拖动的位置。";
            // 
            // GBoxMO
            // 
            this.GBoxMO.Controls.Add(this.CheckBoxEnableMO);
            this.GBoxMO.Controls.Add(this.LabelLine4);
            this.GBoxMO.Controls.Add(this.LabelLine3);
            this.GBoxMO.Location = new System.Drawing.Point(7, 90);
            this.GBoxMO.Name = "GBoxMO";
            this.GBoxMO.Size = new System.Drawing.Size(405, 78);
            this.GBoxMO.TabIndex = 3;
            this.GBoxMO.TabStop = false;
            this.GBoxMO.Text = "内存优化";
            // 
            // CheckBoxEnableMO
            // 
            this.CheckBoxEnableMO.AutoSize = true;
            this.CheckBoxEnableMO.Location = new System.Drawing.Point(9, 52);
            this.CheckBoxEnableMO.Name = "CheckBoxEnableMO";
            this.CheckBoxEnableMO.Size = new System.Drawing.Size(91, 19);
            this.CheckBoxEnableMO.TabIndex = 2;
            this.CheckBoxEnableMO.Text = "启用此功能";
            this.CheckBoxEnableMO.UseVisualStyleBackColor = true;
            this.CheckBoxEnableMO.CheckedChanged += new System.EventHandler(this.SettingsChanged);
            // 
            // LabelLine4
            // 
            this.LabelLine4.AutoSize = true;
            this.LabelLine4.Location = new System.Drawing.Point(6, 34);
            this.LabelLine4.Name = "LabelLine4";
            this.LabelLine4.Size = new System.Drawing.Size(384, 15);
            this.LabelLine4.TabIndex = 1;
            this.LabelLine4.Text = "以达到释放内存的效果。建议根据实际情况决定是否启用此功能。";
            // 
            // LabelLine3
            // 
            this.LabelLine3.AutoSize = true;
            this.LabelLine3.Location = new System.Drawing.Point(6, 19);
            this.LabelLine3.Name = "LabelLine3";
            this.LabelLine3.Size = new System.Drawing.Size(396, 15);
            this.LabelLine3.TabIndex = 0;
            this.LabelLine3.Text = "每隔 5 分钟检测倒计时的内存占用，当超过 12 MB 时，将清空工作集";
            // 
            // GBoxVDM
            // 
            this.GBoxVDM.Controls.Add(this.CheckBoxEnableVDM);
            this.GBoxVDM.Controls.Add(this.LabelLine2);
            this.GBoxVDM.Controls.Add(this.LabelLine1);
            this.GBoxVDM.Location = new System.Drawing.Point(7, 6);
            this.GBoxVDM.Name = "GBoxVDM";
            this.GBoxVDM.Size = new System.Drawing.Size(405, 78);
            this.GBoxVDM.TabIndex = 0;
            this.GBoxVDM.TabStop = false;
            this.GBoxVDM.Text = "虚拟桌面支持";
            // 
            // CheckBoxEnableVDM
            // 
            this.CheckBoxEnableVDM.AutoSize = true;
            this.CheckBoxEnableVDM.Location = new System.Drawing.Point(9, 52);
            this.CheckBoxEnableVDM.Name = "CheckBoxEnableVDM";
            this.CheckBoxEnableVDM.Size = new System.Drawing.Size(91, 19);
            this.CheckBoxEnableVDM.TabIndex = 2;
            this.CheckBoxEnableVDM.Text = "启用此功能";
            this.CheckBoxEnableVDM.UseVisualStyleBackColor = true;
            this.CheckBoxEnableVDM.CheckedChanged += new System.EventHandler(this.SettingsChanged);
            // 
            // LabelLine2
            // 
            this.LabelLine2.AutoSize = true;
            this.LabelLine2.Location = new System.Drawing.Point(6, 34);
            this.LabelLine2.Name = "LabelLine2";
            this.LabelLine2.Size = new System.Drawing.Size(306, 15);
            this.LabelLine2.TabIndex = 1;
            this.LabelLine2.Text = "目前该功能在部分情况下可能会很快失效，请慎用。";
            // 
            // LabelLine1
            // 
            this.LabelLine1.AutoSize = true;
            this.LabelLine1.Location = new System.Drawing.Point(6, 19);
            this.LabelLine1.Name = "LabelLine1";
            this.LabelLine1.Size = new System.Drawing.Size(402, 15);
            this.LabelLine1.TabIndex = 0;
            this.LabelLine1.Text = "跟随虚拟桌面的切换而移动窗口，仅适用于 Windows 10 以上的系统。";
            // 
            // TabPageOther
            // 
            this.TabPageOther.Controls.Add(this.GBoxRestart);
            this.TabPageOther.Controls.Add(this.GBoxSyncTime);
            this.TabPageOther.Location = new System.Drawing.Point(4, 24);
            this.TabPageOther.Name = "TabPageOther";
            this.TabPageOther.Padding = new System.Windows.Forms.Padding(3);
            this.TabPageOther.Size = new System.Drawing.Size(421, 259);
            this.TabPageOther.TabIndex = 1;
            this.TabPageOther.Text = "其他";
            this.TabPageOther.UseVisualStyleBackColor = true;
            // 
            // GBoxRestart
            // 
            this.GBoxRestart.Controls.Add(this.LabelLine10);
            this.GBoxRestart.Controls.Add(this.LabelLine9);
            this.GBoxRestart.Controls.Add(this.ButtonRestart);
            this.GBoxRestart.Location = new System.Drawing.Point(7, 125);
            this.GBoxRestart.Name = "GBoxRestart";
            this.GBoxRestart.Size = new System.Drawing.Size(405, 85);
            this.GBoxRestart.TabIndex = 45;
            this.GBoxRestart.TabStop = false;
            // 
            // LabelLine10
            // 
            this.LabelLine10.AutoSize = true;
            this.LabelLine10.Location = new System.Drawing.Point(6, 36);
            this.LabelLine10.Name = "LabelLine10";
            this.LabelLine10.Size = new System.Drawing.Size(0, 15);
            this.LabelLine10.TabIndex = 38;
            // 
            // LabelLine9
            // 
            this.LabelLine9.AutoSize = true;
            this.LabelLine9.Location = new System.Drawing.Point(6, 19);
            this.LabelLine9.Name = "LabelLine9";
            this.LabelLine9.Size = new System.Drawing.Size(0, 15);
            this.LabelLine9.TabIndex = 37;
            // 
            // GBoxSyncTime
            // 
            this.GBoxSyncTime.Controls.Add(this.LabelLine8);
            this.GBoxSyncTime.Controls.Add(this.LabelLine7);
            this.GBoxSyncTime.Controls.Add(this.LabelLine6);
            this.GBoxSyncTime.Controls.Add(this.LabelLine5);
            this.GBoxSyncTime.Controls.Add(this.ButtonSyncTime);
            this.GBoxSyncTime.Location = new System.Drawing.Point(7, 6);
            this.GBoxSyncTime.Name = "GBoxSyncTime";
            this.GBoxSyncTime.Size = new System.Drawing.Size(405, 113);
            this.GBoxSyncTime.TabIndex = 44;
            this.GBoxSyncTime.TabStop = false;
            this.GBoxSyncTime.Text = "同步网络时钟";
            // 
            // LabelLine8
            // 
            this.LabelLine8.AutoSize = true;
            this.LabelLine8.ForeColor = System.Drawing.Color.Red;
            this.LabelLine8.Location = new System.Drawing.Point(6, 64);
            this.LabelLine8.Name = "LabelLine8";
            this.LabelLine8.Size = new System.Drawing.Size(163, 15);
            this.LabelLine8.TabIndex = 24;
            this.LabelLine8.Text = "器的操作可自行上网查阅。";
            // 
            // LabelLine7
            // 
            this.LabelLine7.AutoSize = true;
            this.LabelLine7.ForeColor = System.Drawing.Color.Red;
            this.LabelLine7.Location = new System.Drawing.Point(6, 49);
            this.LabelLine7.Name = "LabelLine7";
            this.LabelLine7.Size = new System.Drawing.Size(395, 15);
            this.LabelLine7.TabIndex = 23;
            this.LabelLine7.Text = "Windows Time 服务设置为自动启动, 请谨慎操作。有关修改 NTP 服务";
            // 
            // LabelLine6
            // 
            this.LabelLine6.AutoSize = true;
            this.LabelLine6.ForeColor = System.Drawing.Color.Red;
            this.LabelLine6.Location = new System.Drawing.Point(6, 34);
            this.LabelLine6.Name = "LabelLine6";
            this.LabelLine6.Size = new System.Drawing.Size(396, 15);
            this.LabelLine6.TabIndex = 22;
            this.LabelLine6.Text = "注意: 此项会将系统的 NTP 服务器设置为 ntp1.aliyun.com, 并且还会将";
            // 
            // LabelLine5
            // 
            this.LabelLine5.AutoSize = true;
            this.LabelLine5.Location = new System.Drawing.Point(6, 19);
            this.LabelLine5.Name = "LabelLine5";
            this.LabelLine5.Size = new System.Drawing.Size(371, 15);
            this.LabelLine5.TabIndex = 20;
            this.LabelLine5.Text = "通过运行外部命令将当前系统时间与网络同步以确保准确无误。";
            // 
            // FormSettings
            // 
            this.AcceptButton = this.ButtonSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(443, 327);
            this.Controls.Add(this.TabControlMain);
            this.Controls.Add(this.ButtonSave);
            this.Controls.Add(this.ButtonClose);
            this.DoubleBuffered = true;
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
            this.TabControlMain.ResumeLayout(false);
            this.TabPageGeneral.ResumeLayout(false);
            this.GBoxStartup.ResumeLayout(false);
            this.GBoxStartup.PerformLayout();
            this.GBoxExamEnd.ResumeLayout(false);
            this.GBoxExamStart.ResumeLayout(false);
            this.GBoxExamName.ResumeLayout(false);
            this.GBoxExamName.PerformLayout();
            this.TabPageDisplay.ResumeLayout(false);
            this.GBoxContent.ResumeLayout(false);
            this.GBoxContent.PerformLayout();
            this.GBoxChangeFont.ResumeLayout(false);
            this.GBoxChangeFont.PerformLayout();
            this.TabPageAdvanced.ResumeLayout(false);
            this.GBoxSetDragable.ResumeLayout(false);
            this.GBoxSetDragable.PerformLayout();
            this.GBoxMO.ResumeLayout(false);
            this.GBoxMO.PerformLayout();
            this.GBoxVDM.ResumeLayout(false);
            this.GBoxVDM.PerformLayout();
            this.TabPageOther.ResumeLayout(false);
            this.GBoxRestart.ResumeLayout(false);
            this.GBoxRestart.PerformLayout();
            this.GBoxSyncTime.ResumeLayout(false);
            this.GBoxSyncTime.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button ButtonClose;
        private System.Windows.Forms.Button ButtonSyncTime;
        private System.Windows.Forms.TextBox TextBoxExamName;
        private System.Windows.Forms.Button ButtonRestart;
        private System.Windows.Forms.DateTimePicker DTPExamStart;
        private System.Windows.Forms.DateTimePicker DTPExamEnd;
        private System.Windows.Forms.Button ButtonSave;
        private System.Windows.Forms.TabControl TabControlMain;
        private System.Windows.Forms.TabPage TabPageGeneral;
        private System.Windows.Forms.TabPage TabPageOther;
        private System.Windows.Forms.GroupBox GBoxRestart;
        private System.Windows.Forms.GroupBox GBoxSyncTime;
        private System.Windows.Forms.GroupBox GBoxExamEnd;
        private System.Windows.Forms.GroupBox GBoxExamStart;
        private System.Windows.Forms.GroupBox GBoxExamName;
        private System.Windows.Forms.Label LabelExamNameCounter;
        private System.Windows.Forms.GroupBox GBoxStartup;
        private System.Windows.Forms.CheckBox CheckBoxStartup;
        private System.Windows.Forms.CheckBox CheckBoxSetTopMost;
        private System.Windows.Forms.Label LabelLine10;
        private System.Windows.Forms.Label LabelLine9;
        private System.Windows.Forms.Label LabelLine7;
        private System.Windows.Forms.Label LabelLine6;
        private System.Windows.Forms.Label LabelLine5;
        private System.Windows.Forms.Label LabelLine8;
        private System.Windows.Forms.TabPage TabPageAdvanced;
        private System.Windows.Forms.GroupBox GBoxVDM;
        private System.Windows.Forms.Label LabelLine1;
        private System.Windows.Forms.CheckBox CheckBoxEnableVDM;
        private System.Windows.Forms.Label LabelLine2;
        private System.Windows.Forms.GroupBox GBoxMO;
        private System.Windows.Forms.CheckBox CheckBoxEnableMO;
        private System.Windows.Forms.Label LabelLine4;
        private System.Windows.Forms.Label LabelLine3;
        private System.Windows.Forms.TabPage TabPageDisplay;
        private System.Windows.Forms.GroupBox GBoxChangeFont;
        private System.Windows.Forms.Label LabelLine11;
        private System.Windows.Forms.Label LabelFontInfo;
        private System.Windows.Forms.Label LabelPreviewFont;
        private System.Windows.Forms.Button ButtonChooseFont;
        private System.Windows.Forms.Button ButtonRestoreFont;
        private System.Windows.Forms.GroupBox GBoxContent;
        private System.Windows.Forms.CheckBox CheckBoxSetDaysOnly;
        private System.Windows.Forms.CheckBox CheckBoxSetNoPast;
        private System.Windows.Forms.CheckBox CheckBoxSetNoStart;
        private System.Windows.Forms.CheckBox CheckBoxSetRounding;
        private System.Windows.Forms.GroupBox GBoxSetDragable;
        private System.Windows.Forms.CheckBox CheckBoxEnableDragable;
        private System.Windows.Forms.Label LabelLine12;
        private System.Windows.Forms.CheckBox CheckBoxSetUniTopMost;
    }
}