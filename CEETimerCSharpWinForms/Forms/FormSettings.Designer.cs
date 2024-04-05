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
            this.CheckBoxEnableMO = new System.Windows.Forms.CheckBox();
            this.CheckBoxSetTopMost = new System.Windows.Forms.CheckBox();
            this.CheckBoxStartup = new System.Windows.Forms.CheckBox();
            this.GBoxExamEnd = new System.Windows.Forms.GroupBox();
            this.GBoxExamStart = new System.Windows.Forms.GroupBox();
            this.GBoxExamName = new System.Windows.Forms.GroupBox();
            this.LabelExamNameCounter = new System.Windows.Forms.Label();
            this.TabPageDisplay = new System.Windows.Forms.TabPage();
            this.GBoxPPTService = new System.Windows.Forms.GroupBox();
            this.CheckBoxSwPptSvc = new System.Windows.Forms.CheckBox();
            this.LabelLine12 = new System.Windows.Forms.Label();
            this.LabelLine13 = new System.Windows.Forms.Label();
            this.GBoxContent = new System.Windows.Forms.GroupBox();
            this.ComboBoxShowOnly = new System.Windows.Forms.ComboBox();
            this.CheckBoxSetUniTopMost = new System.Windows.Forms.CheckBox();
            this.CheckBoxShowPast = new System.Windows.Forms.CheckBox();
            this.CheckBoxShowEnd = new System.Windows.Forms.CheckBox();
            this.CheckBoxSetRounding = new System.Windows.Forms.CheckBox();
            this.CheckBoxShowOnly = new System.Windows.Forms.CheckBox();
            this.GBoxSetDragable = new System.Windows.Forms.GroupBox();
            this.CheckBoxEnableDragable = new System.Windows.Forms.CheckBox();
            this.LabelScreensHint = new System.Windows.Forms.Label();
            this.LabelScreens = new System.Windows.Forms.Label();
            this.ComboBoxScreens = new System.Windows.Forms.ComboBox();
            this.TabPageStyle = new System.Windows.Forms.TabPage();
            this.GBoxColors = new System.Windows.Forms.GroupBox();
            this.LabelPreviewCorlor4 = new System.Windows.Forms.Label();
            this.LabelColor42 = new System.Windows.Forms.Label();
            this.LabelColor41 = new System.Windows.Forms.Label();
            this.LabelLine18 = new System.Windows.Forms.Label();
            this.LabelPreviewCorlor1 = new System.Windows.Forms.Label();
            this.LabelPreviewCorlor2 = new System.Windows.Forms.Label();
            this.LabelPreviewCorlor3 = new System.Windows.Forms.Label();
            this.LabelLine14 = new System.Windows.Forms.Label();
            this.ButtonColorDefault = new System.Windows.Forms.Button();
            this.ButtonColorApply = new System.Windows.Forms.Button();
            this.LabelColor32 = new System.Windows.Forms.Label();
            this.LabelColor31 = new System.Windows.Forms.Label();
            this.LabelColor22 = new System.Windows.Forms.Label();
            this.LabelColor21 = new System.Windows.Forms.Label();
            this.LabelColor12 = new System.Windows.Forms.Label();
            this.LabelColor11 = new System.Windows.Forms.Label();
            this.LabelLine17 = new System.Windows.Forms.Label();
            this.LabelLine16 = new System.Windows.Forms.Label();
            this.LabelLine15 = new System.Windows.Forms.Label();
            this.GBoxChangeFont = new System.Windows.Forms.GroupBox();
            this.ButtonRestoreFont = new System.Windows.Forms.Button();
            this.ButtonChooseFont = new System.Windows.Forms.Button();
            this.LabelPreviewFont = new System.Windows.Forms.Label();
            this.LabelFontInfo = new System.Windows.Forms.Label();
            this.LabelLine11 = new System.Windows.Forms.Label();
            this.TabPageTools = new System.Windows.Forms.TabPage();
            this.GBoxVDM = new System.Windows.Forms.GroupBox();
            this.CheckBoxEnableVDM = new System.Windows.Forms.CheckBox();
            this.LabelLine2 = new System.Windows.Forms.Label();
            this.LabelLine1 = new System.Windows.Forms.Label();
            this.GBoxRestart = new System.Windows.Forms.GroupBox();
            this.LabelLine10 = new System.Windows.Forms.Label();
            this.LabelLine9 = new System.Windows.Forms.Label();
            this.GBoxSyncTime = new System.Windows.Forms.GroupBox();
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
            this.GBoxPPTService.SuspendLayout();
            this.GBoxContent.SuspendLayout();
            this.GBoxSetDragable.SuspendLayout();
            this.TabPageStyle.SuspendLayout();
            this.GBoxColors.SuspendLayout();
            this.GBoxChangeFont.SuspendLayout();
            this.TabPageTools.SuspendLayout();
            this.GBoxVDM.SuspendLayout();
            this.GBoxRestart.SuspendLayout();
            this.GBoxSyncTime.SuspendLayout();
            this.SuspendLayout();
            // 
            // ButtonClose
            // 
            this.ButtonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.ButtonClose.Location = new System.Drawing.Point(360, 329);
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
            this.ButtonSyncTime.Location = new System.Drawing.Point(6, 67);
            this.ButtonSyncTime.Name = "ButtonSyncTime";
            this.ButtonSyncTime.Size = new System.Drawing.Size(84, 25);
            this.ButtonSyncTime.TabIndex = 19;
            this.ButtonSyncTime.Text = "立即同步(&Y)";
            this.ButtonSyncTime.UseVisualStyleBackColor = true;
            this.ButtonSyncTime.Click += new System.EventHandler(this.ButtonSyncTime_Click);
            // 
            // TextBoxExamName
            // 
            this.TextBoxExamName.Location = new System.Drawing.Point(9, 22);
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
            this.DTPExamStart.Location = new System.Drawing.Point(9, 22);
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
            this.DTPExamEnd.Location = new System.Drawing.Point(9, 22);
            this.DTPExamEnd.Name = "DTPExamEnd";
            this.DTPExamEnd.Size = new System.Drawing.Size(357, 23);
            this.DTPExamEnd.TabIndex = 39;
            this.DTPExamEnd.ValueChanged += new System.EventHandler(this.SettingsChanged);
            // 
            // ButtonSave
            // 
            this.ButtonSave.Location = new System.Drawing.Point(279, 329);
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
            this.TabControlMain.Controls.Add(this.TabPageStyle);
            this.TabControlMain.Controls.Add(this.TabPageTools);
            this.TabControlMain.Location = new System.Drawing.Point(7, 4);
            this.TabControlMain.Name = "TabControlMain";
            this.TabControlMain.SelectedIndex = 0;
            this.TabControlMain.Size = new System.Drawing.Size(429, 322);
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
            this.TabPageGeneral.Size = new System.Drawing.Size(421, 294);
            this.TabPageGeneral.TabIndex = 0;
            this.TabPageGeneral.Text = "基本";
            this.TabPageGeneral.UseVisualStyleBackColor = true;
            // 
            // GBoxStartup
            // 
            this.GBoxStartup.Controls.Add(this.CheckBoxEnableMO);
            this.GBoxStartup.Controls.Add(this.CheckBoxSetTopMost);
            this.GBoxStartup.Controls.Add(this.CheckBoxStartup);
            this.GBoxStartup.Location = new System.Drawing.Point(7, 198);
            this.GBoxStartup.Name = "GBoxStartup";
            this.GBoxStartup.Size = new System.Drawing.Size(405, 73);
            this.GBoxStartup.TabIndex = 44;
            this.GBoxStartup.TabStop = false;
            this.GBoxStartup.Text = "其他";
            // 
            // CheckBoxEnableMO
            // 
            this.CheckBoxEnableMO.AutoSize = true;
            this.CheckBoxEnableMO.Location = new System.Drawing.Point(186, 22);
            this.CheckBoxEnableMO.Name = "CheckBoxEnableMO";
            this.CheckBoxEnableMO.Size = new System.Drawing.Size(175, 19);
            this.CheckBoxEnableMO.TabIndex = 2;
            this.CheckBoxEnableMO.Text = "在需要时自动优化内存(&M)";
            this.CheckBoxEnableMO.UseVisualStyleBackColor = true;
            this.CheckBoxEnableMO.CheckedChanged += new System.EventHandler(this.SettingsChanged);
            // 
            // CheckBoxSetTopMost
            // 
            this.CheckBoxSetTopMost.AutoSize = true;
            this.CheckBoxSetTopMost.Checked = true;
            this.CheckBoxSetTopMost.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CheckBoxSetTopMost.Location = new System.Drawing.Point(9, 47);
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
            this.CheckBoxStartup.Location = new System.Drawing.Point(9, 22);
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
            this.GBoxExamEnd.Location = new System.Drawing.Point(7, 134);
            this.GBoxExamEnd.Name = "GBoxExamEnd";
            this.GBoxExamEnd.Size = new System.Drawing.Size(405, 58);
            this.GBoxExamEnd.TabIndex = 42;
            this.GBoxExamEnd.TabStop = false;
            this.GBoxExamEnd.Text = "考试结束日期和时间";
            // 
            // GBoxExamStart
            // 
            this.GBoxExamStart.Controls.Add(this.DTPExamStart);
            this.GBoxExamStart.Location = new System.Drawing.Point(7, 70);
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
            this.GBoxExamName.Location = new System.Drawing.Point(7, 6);
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
            this.LabelExamNameCounter.Location = new System.Drawing.Point(367, 25);
            this.LabelExamNameCounter.Name = "LabelExamNameCounter";
            this.LabelExamNameCounter.Size = new System.Drawing.Size(30, 15);
            this.LabelExamNameCounter.TabIndex = 35;
            this.LabelExamNameCounter.Text = "0/15";
            // 
            // TabPageDisplay
            // 
            this.TabPageDisplay.Controls.Add(this.GBoxPPTService);
            this.TabPageDisplay.Controls.Add(this.GBoxContent);
            this.TabPageDisplay.Controls.Add(this.GBoxSetDragable);
            this.TabPageDisplay.Location = new System.Drawing.Point(4, 24);
            this.TabPageDisplay.Name = "TabPageDisplay";
            this.TabPageDisplay.Size = new System.Drawing.Size(421, 294);
            this.TabPageDisplay.TabIndex = 3;
            this.TabPageDisplay.Text = "显示";
            this.TabPageDisplay.UseVisualStyleBackColor = true;
            // 
            // GBoxPPTService
            // 
            this.GBoxPPTService.Controls.Add(this.CheckBoxSwPptSvc);
            this.GBoxPPTService.Controls.Add(this.LabelLine12);
            this.GBoxPPTService.Controls.Add(this.LabelLine13);
            this.GBoxPPTService.Location = new System.Drawing.Point(7, 209);
            this.GBoxPPTService.Name = "GBoxPPTService";
            this.GBoxPPTService.Size = new System.Drawing.Size(405, 78);
            this.GBoxPPTService.TabIndex = 0;
            this.GBoxPPTService.TabStop = false;
            this.GBoxPPTService.Text = "兼容希沃PPT小工具";
            // 
            // CheckBoxSwPptSvc
            // 
            this.CheckBoxSwPptSvc.AutoSize = true;
            this.CheckBoxSwPptSvc.Location = new System.Drawing.Point(9, 53);
            this.CheckBoxSwPptSvc.Name = "CheckBoxSwPptSvc";
            this.CheckBoxSwPptSvc.Size = new System.Drawing.Size(15, 14);
            this.CheckBoxSwPptSvc.TabIndex = 3;
            this.CheckBoxSwPptSvc.UseVisualStyleBackColor = true;
            this.CheckBoxSwPptSvc.CheckedChanged += new System.EventHandler(this.SettingsChanged);
            // 
            // LabelLine12
            // 
            this.LabelLine12.AutoSize = true;
            this.LabelLine12.Location = new System.Drawing.Point(6, 34);
            this.LabelLine12.Name = "LabelLine12";
            this.LabelLine12.Size = new System.Drawing.Size(353, 15);
            this.LabelLine12.TabIndex = 0;
            this.LabelLine12.Text = "(或者你也可以开启拖动功能，将倒计时窗口拖动到其他位置)";
            // 
            // LabelLine13
            // 
            this.LabelLine13.AutoSize = true;
            this.LabelLine13.Location = new System.Drawing.Point(6, 19);
            this.LabelLine13.Name = "LabelLine13";
            this.LabelLine13.Size = new System.Drawing.Size(404, 15);
            this.LabelLine13.TabIndex = 0;
            this.LabelLine13.Text = "用于解决希沃PPT小工具的内置白板打开后底部工具栏会消失的问题。";
            // 
            // GBoxContent
            // 
            this.GBoxContent.Controls.Add(this.ComboBoxShowOnly);
            this.GBoxContent.Controls.Add(this.CheckBoxSetUniTopMost);
            this.GBoxContent.Controls.Add(this.CheckBoxShowPast);
            this.GBoxContent.Controls.Add(this.CheckBoxShowEnd);
            this.GBoxContent.Controls.Add(this.CheckBoxSetRounding);
            this.GBoxContent.Controls.Add(this.CheckBoxShowOnly);
            this.GBoxContent.Location = new System.Drawing.Point(7, 6);
            this.GBoxContent.Name = "GBoxContent";
            this.GBoxContent.Size = new System.Drawing.Size(405, 120);
            this.GBoxContent.TabIndex = 1;
            this.GBoxContent.TabStop = false;
            this.GBoxContent.Text = "倒计时内容";
            // 
            // ComboBoxShowOnly
            // 
            this.ComboBoxShowOnly.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxShowOnly.Enabled = false;
            this.ComboBoxShowOnly.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.ComboBoxShowOnly.Location = new System.Drawing.Point(73, 19);
            this.ComboBoxShowOnly.MaxDropDownItems = 1;
            this.ComboBoxShowOnly.Name = "ComboBoxShowOnly";
            this.ComboBoxShowOnly.Size = new System.Drawing.Size(38, 23);
            this.ComboBoxShowOnly.TabIndex = 5;
            this.ComboBoxShowOnly.DropDown += new System.EventHandler(this.ComboBoxes_DropDown);
            this.ComboBoxShowOnly.SelectedIndexChanged += new System.EventHandler(this.ComboBoxShowOnly_SelectedIndexChanged);
            // 
            // CheckBoxSetUniTopMost
            // 
            this.CheckBoxSetUniTopMost.AutoSize = true;
            this.CheckBoxSetUniTopMost.Location = new System.Drawing.Point(9, 97);
            this.CheckBoxSetUniTopMost.Name = "CheckBoxSetUniTopMost";
            this.CheckBoxSetUniTopMost.Size = new System.Drawing.Size(263, 19);
            this.CheckBoxSetUniTopMost.TabIndex = 4;
            this.CheckBoxSetUniTopMost.Text = "顶置属性同样适用于本程序的其他窗口(&U)";
            this.CheckBoxSetUniTopMost.UseVisualStyleBackColor = true;
            this.CheckBoxSetUniTopMost.CheckedChanged += new System.EventHandler(this.SettingsChanged);
            // 
            // CheckBoxShowPast
            // 
            this.CheckBoxShowPast.AutoSize = true;
            this.CheckBoxShowPast.Location = new System.Drawing.Point(9, 72);
            this.CheckBoxShowPast.Name = "CheckBoxShowPast";
            this.CheckBoxShowPast.Size = new System.Drawing.Size(291, 19);
            this.CheckBoxShowPast.TabIndex = 3;
            this.CheckBoxShowPast.Text = "显示 \"考试已过去了多久\" (距离...已过去了...)(&P)";
            this.CheckBoxShowPast.UseVisualStyleBackColor = true;
            this.CheckBoxShowPast.CheckedChanged += new System.EventHandler(this.CheckBoxShowPast_CheckedChanged);
            // 
            // CheckBoxShowEnd
            // 
            this.CheckBoxShowEnd.AutoSize = true;
            this.CheckBoxShowEnd.Location = new System.Drawing.Point(9, 47);
            this.CheckBoxShowEnd.Name = "CheckBoxShowEnd";
            this.CheckBoxShowEnd.Size = new System.Drawing.Size(290, 19);
            this.CheckBoxShowEnd.TabIndex = 2;
            this.CheckBoxShowEnd.Text = "显示 \"考试还有多久结束\" (距离...结束还有...)(&E)";
            this.CheckBoxShowEnd.UseVisualStyleBackColor = true;
            this.CheckBoxShowEnd.CheckedChanged += new System.EventHandler(this.CheckBoxShowEnd_CheckedChanged);
            // 
            // CheckBoxSetRounding
            // 
            this.CheckBoxSetRounding.AutoSize = true;
            this.CheckBoxSetRounding.Enabled = false;
            this.CheckBoxSetRounding.Location = new System.Drawing.Point(117, 22);
            this.CheckBoxSetRounding.Name = "CheckBoxSetRounding";
            this.CheckBoxSetRounding.Size = new System.Drawing.Size(199, 19);
            this.CheckBoxSetRounding.TabIndex = 1;
            this.CheckBoxSetRounding.Text = "将不足一天的时间视为一天(&N)";
            this.CheckBoxSetRounding.UseVisualStyleBackColor = true;
            this.CheckBoxSetRounding.CheckedChanged += new System.EventHandler(this.SettingsChanged);
            // 
            // CheckBoxShowOnly
            // 
            this.CheckBoxShowOnly.AutoSize = true;
            this.CheckBoxShowOnly.Location = new System.Drawing.Point(9, 22);
            this.CheckBoxShowOnly.Name = "CheckBoxShowOnly";
            this.CheckBoxShowOnly.Size = new System.Drawing.Size(65, 19);
            this.CheckBoxShowOnly.TabIndex = 0;
            this.CheckBoxShowOnly.Text = "只显示";
            this.CheckBoxShowOnly.UseVisualStyleBackColor = true;
            this.CheckBoxShowOnly.CheckedChanged += new System.EventHandler(this.CheckBoxShowOnly_CheckedChanged);
            // 
            // GBoxSetDragable
            // 
            this.GBoxSetDragable.Controls.Add(this.CheckBoxEnableDragable);
            this.GBoxSetDragable.Controls.Add(this.LabelScreensHint);
            this.GBoxSetDragable.Controls.Add(this.LabelScreens);
            this.GBoxSetDragable.Controls.Add(this.ComboBoxScreens);
            this.GBoxSetDragable.Location = new System.Drawing.Point(7, 132);
            this.GBoxSetDragable.Name = "GBoxSetDragable";
            this.GBoxSetDragable.Size = new System.Drawing.Size(405, 71);
            this.GBoxSetDragable.TabIndex = 4;
            this.GBoxSetDragable.TabStop = false;
            this.GBoxSetDragable.Text = "多显示器与拖动";
            // 
            // CheckBoxEnableDragable
            // 
            this.CheckBoxEnableDragable.AutoSize = true;
            this.CheckBoxEnableDragable.Location = new System.Drawing.Point(9, 45);
            this.CheckBoxEnableDragable.Name = "CheckBoxEnableDragable";
            this.CheckBoxEnableDragable.Size = new System.Drawing.Size(172, 19);
            this.CheckBoxEnableDragable.TabIndex = 3;
            this.CheckBoxEnableDragable.Text = "允许倒计时窗口被拖动(&D)";
            this.CheckBoxEnableDragable.UseVisualStyleBackColor = true;
            this.CheckBoxEnableDragable.CheckedChanged += new System.EventHandler(this.CheckBoxEnableDragable_CheckedChanged);
            // 
            // LabelScreensHint
            // 
            this.LabelScreensHint.AutoSize = true;
            this.LabelScreensHint.Location = new System.Drawing.Point(235, 21);
            this.LabelScreensHint.Name = "LabelScreensHint";
            this.LabelScreensHint.Size = new System.Drawing.Size(20, 15);
            this.LabelScreensHint.TabIndex = 6;
            this.LabelScreensHint.Text = "上";
            // 
            // LabelScreens
            // 
            this.LabelScreens.AutoSize = true;
            this.LabelScreens.Location = new System.Drawing.Point(6, 21);
            this.LabelScreens.Name = "LabelScreens";
            this.LabelScreens.Size = new System.Drawing.Size(124, 15);
            this.LabelScreens.TabIndex = 5;
            this.LabelScreens.Text = "将倒计时显示在屏幕";
            // 
            // ComboBoxScreens
            // 
            this.ComboBoxScreens.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxScreens.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.ComboBoxScreens.Location = new System.Drawing.Point(132, 17);
            this.ComboBoxScreens.MaxDropDownItems = 1;
            this.ComboBoxScreens.Name = "ComboBoxScreens";
            this.ComboBoxScreens.Size = new System.Drawing.Size(100, 23);
            this.ComboBoxScreens.TabIndex = 4;
            this.ComboBoxScreens.DropDown += new System.EventHandler(this.ComboBoxes_DropDown);
            this.ComboBoxScreens.SelectedIndexChanged += new System.EventHandler(this.SettingsChanged);
            // 
            // TabPageStyle
            // 
            this.TabPageStyle.Controls.Add(this.GBoxColors);
            this.TabPageStyle.Controls.Add(this.GBoxChangeFont);
            this.TabPageStyle.Location = new System.Drawing.Point(4, 24);
            this.TabPageStyle.Name = "TabPageStyle";
            this.TabPageStyle.Padding = new System.Windows.Forms.Padding(3);
            this.TabPageStyle.Size = new System.Drawing.Size(421, 294);
            this.TabPageStyle.TabIndex = 4;
            this.TabPageStyle.Text = "样式";
            this.TabPageStyle.UseVisualStyleBackColor = true;
            // 
            // GBoxColors
            // 
            this.GBoxColors.Controls.Add(this.LabelPreviewCorlor4);
            this.GBoxColors.Controls.Add(this.LabelColor42);
            this.GBoxColors.Controls.Add(this.LabelColor41);
            this.GBoxColors.Controls.Add(this.LabelLine18);
            this.GBoxColors.Controls.Add(this.LabelPreviewCorlor1);
            this.GBoxColors.Controls.Add(this.LabelPreviewCorlor2);
            this.GBoxColors.Controls.Add(this.LabelPreviewCorlor3);
            this.GBoxColors.Controls.Add(this.LabelLine14);
            this.GBoxColors.Controls.Add(this.ButtonColorDefault);
            this.GBoxColors.Controls.Add(this.ButtonColorApply);
            this.GBoxColors.Controls.Add(this.LabelColor32);
            this.GBoxColors.Controls.Add(this.LabelColor31);
            this.GBoxColors.Controls.Add(this.LabelColor22);
            this.GBoxColors.Controls.Add(this.LabelColor21);
            this.GBoxColors.Controls.Add(this.LabelColor12);
            this.GBoxColors.Controls.Add(this.LabelColor11);
            this.GBoxColors.Controls.Add(this.LabelLine17);
            this.GBoxColors.Controls.Add(this.LabelLine16);
            this.GBoxColors.Controls.Add(this.LabelLine15);
            this.GBoxColors.Location = new System.Drawing.Point(7, 133);
            this.GBoxColors.Name = "GBoxColors";
            this.GBoxColors.Size = new System.Drawing.Size(405, 154);
            this.GBoxColors.TabIndex = 0;
            this.GBoxColors.TabStop = false;
            this.GBoxColors.Text = "字体颜色";
            // 
            // LabelPreviewCorlor4
            // 
            this.LabelPreviewCorlor4.AutoSize = true;
            this.LabelPreviewCorlor4.Location = new System.Drawing.Point(202, 104);
            this.LabelPreviewCorlor4.Name = "LabelPreviewCorlor4";
            this.LabelPreviewCorlor4.Size = new System.Drawing.Size(133, 15);
            this.LabelPreviewCorlor4.TabIndex = 19;
            this.LabelPreviewCorlor4.Text = "欢迎使用高考倒计时...";
            // 
            // LabelColor42
            // 
            this.LabelColor42.AutoSize = true;
            this.LabelColor42.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LabelColor42.Location = new System.Drawing.Point(112, 103);
            this.LabelColor42.Name = "LabelColor42";
            this.LabelColor42.Size = new System.Drawing.Size(39, 17);
            this.LabelColor42.TabIndex = 18;
            this.LabelColor42.Text = "          ";
            this.LabelColor42.Click += new System.EventHandler(this.ColorLabels_Click);
            // 
            // LabelColor41
            // 
            this.LabelColor41.AutoSize = true;
            this.LabelColor41.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LabelColor41.Location = new System.Drawing.Point(157, 103);
            this.LabelColor41.Name = "LabelColor41";
            this.LabelColor41.Size = new System.Drawing.Size(39, 17);
            this.LabelColor41.TabIndex = 17;
            this.LabelColor41.Text = "          ";
            this.LabelColor41.Click += new System.EventHandler(this.ColorLabels_Click);
            // 
            // LabelLine18
            // 
            this.LabelLine18.AutoSize = true;
            this.LabelLine18.Location = new System.Drawing.Point(6, 104);
            this.LabelLine18.Name = "LabelLine18";
            this.LabelLine18.Size = new System.Drawing.Size(102, 15);
            this.LabelLine18.TabIndex = 16;
            this.LabelLine18.Text = "程序欢迎界面 (4)";
            // 
            // LabelPreviewCorlor1
            // 
            this.LabelPreviewCorlor1.AutoSize = true;
            this.LabelPreviewCorlor1.Location = new System.Drawing.Point(202, 38);
            this.LabelPreviewCorlor1.Name = "LabelPreviewCorlor1";
            this.LabelPreviewCorlor1.Size = new System.Drawing.Size(77, 15);
            this.LabelPreviewCorlor1.TabIndex = 15;
            this.LabelPreviewCorlor1.Text = "距离...还有...";
            // 
            // LabelPreviewCorlor2
            // 
            this.LabelPreviewCorlor2.AutoSize = true;
            this.LabelPreviewCorlor2.Location = new System.Drawing.Point(202, 60);
            this.LabelPreviewCorlor2.Name = "LabelPreviewCorlor2";
            this.LabelPreviewCorlor2.Size = new System.Drawing.Size(103, 15);
            this.LabelPreviewCorlor2.TabIndex = 14;
            this.LabelPreviewCorlor2.Text = "距离...结束还有...";
            // 
            // LabelPreviewCorlor3
            // 
            this.LabelPreviewCorlor3.AutoSize = true;
            this.LabelPreviewCorlor3.Location = new System.Drawing.Point(202, 82);
            this.LabelPreviewCorlor3.Name = "LabelPreviewCorlor3";
            this.LabelPreviewCorlor3.Size = new System.Drawing.Size(103, 15);
            this.LabelPreviewCorlor3.TabIndex = 13;
            this.LabelPreviewCorlor3.Text = "距离...已过去了...";
            // 
            // LabelLine14
            // 
            this.LabelLine14.AutoSize = true;
            this.LabelLine14.Location = new System.Drawing.Point(6, 19);
            this.LabelLine14.Name = "LabelLine14";
            this.LabelLine14.Size = new System.Drawing.Size(384, 15);
            this.LabelLine14.TabIndex = 12;
            this.LabelLine14.Text = "请点击色块来挑选颜色。两个色块分别对应文字颜色、背景颜色。";
            // 
            // ButtonColorDefault
            // 
            this.ButtonColorDefault.AutoSize = true;
            this.ButtonColorDefault.Location = new System.Drawing.Point(99, 123);
            this.ButtonColorDefault.Name = "ButtonColorDefault";
            this.ButtonColorDefault.Size = new System.Drawing.Size(85, 25);
            this.ButtonColorDefault.TabIndex = 11;
            this.ButtonColorDefault.Text = "恢复默认(&D)";
            this.ButtonColorDefault.UseVisualStyleBackColor = true;
            this.ButtonColorDefault.Click += new System.EventHandler(this.ButtonColorDefault_Click);
            // 
            // ButtonColorApply
            // 
            this.ButtonColorApply.AutoSize = true;
            this.ButtonColorApply.Location = new System.Drawing.Point(9, 123);
            this.ButtonColorApply.Name = "ButtonColorApply";
            this.ButtonColorApply.Size = new System.Drawing.Size(84, 25);
            this.ButtonColorApply.TabIndex = 10;
            this.ButtonColorApply.Text = "应用全部(&Y)";
            this.ButtonColorApply.UseVisualStyleBackColor = true;
            this.ButtonColorApply.Click += new System.EventHandler(this.ButtonColorApply_Click);
            // 
            // LabelColor32
            // 
            this.LabelColor32.AutoSize = true;
            this.LabelColor32.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LabelColor32.Location = new System.Drawing.Point(112, 81);
            this.LabelColor32.Name = "LabelColor32";
            this.LabelColor32.Size = new System.Drawing.Size(39, 17);
            this.LabelColor32.TabIndex = 9;
            this.LabelColor32.Text = "          ";
            this.LabelColor32.Click += new System.EventHandler(this.ColorLabels_Click);
            // 
            // LabelColor31
            // 
            this.LabelColor31.AutoSize = true;
            this.LabelColor31.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LabelColor31.Location = new System.Drawing.Point(157, 81);
            this.LabelColor31.Name = "LabelColor31";
            this.LabelColor31.Size = new System.Drawing.Size(39, 17);
            this.LabelColor31.TabIndex = 8;
            this.LabelColor31.Text = "          ";
            this.LabelColor31.Click += new System.EventHandler(this.ColorLabels_Click);
            // 
            // LabelColor22
            // 
            this.LabelColor22.AutoSize = true;
            this.LabelColor22.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LabelColor22.Location = new System.Drawing.Point(112, 59);
            this.LabelColor22.Name = "LabelColor22";
            this.LabelColor22.Size = new System.Drawing.Size(39, 17);
            this.LabelColor22.TabIndex = 7;
            this.LabelColor22.Text = "          ";
            this.LabelColor22.Click += new System.EventHandler(this.ColorLabels_Click);
            // 
            // LabelColor21
            // 
            this.LabelColor21.AutoSize = true;
            this.LabelColor21.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LabelColor21.Location = new System.Drawing.Point(157, 59);
            this.LabelColor21.Name = "LabelColor21";
            this.LabelColor21.Size = new System.Drawing.Size(39, 17);
            this.LabelColor21.TabIndex = 6;
            this.LabelColor21.Text = "          ";
            this.LabelColor21.Click += new System.EventHandler(this.ColorLabels_Click);
            // 
            // LabelColor12
            // 
            this.LabelColor12.AutoSize = true;
            this.LabelColor12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LabelColor12.Location = new System.Drawing.Point(112, 37);
            this.LabelColor12.Name = "LabelColor12";
            this.LabelColor12.Size = new System.Drawing.Size(39, 17);
            this.LabelColor12.TabIndex = 5;
            this.LabelColor12.Text = "          ";
            this.LabelColor12.Click += new System.EventHandler(this.ColorLabels_Click);
            // 
            // LabelColor11
            // 
            this.LabelColor11.AutoSize = true;
            this.LabelColor11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LabelColor11.Location = new System.Drawing.Point(157, 37);
            this.LabelColor11.Name = "LabelColor11";
            this.LabelColor11.Size = new System.Drawing.Size(39, 17);
            this.LabelColor11.TabIndex = 0;
            this.LabelColor11.Text = "          ";
            this.LabelColor11.Click += new System.EventHandler(this.ColorLabels_Click);
            // 
            // LabelLine17
            // 
            this.LabelLine17.AutoSize = true;
            this.LabelLine17.Location = new System.Drawing.Point(6, 82);
            this.LabelLine17.Name = "LabelLine17";
            this.LabelLine17.Size = new System.Drawing.Size(102, 15);
            this.LabelLine17.TabIndex = 4;
            this.LabelLine17.Text = "考试已结束时 (3)";
            // 
            // LabelLine16
            // 
            this.LabelLine16.AutoSize = true;
            this.LabelLine16.Location = new System.Drawing.Point(6, 60);
            this.LabelLine16.Name = "LabelLine16";
            this.LabelLine16.Size = new System.Drawing.Size(102, 15);
            this.LabelLine16.TabIndex = 3;
            this.LabelLine16.Text = "考试已开始时 (2)";
            // 
            // LabelLine15
            // 
            this.LabelLine15.AutoSize = true;
            this.LabelLine15.Location = new System.Drawing.Point(6, 38);
            this.LabelLine15.Name = "LabelLine15";
            this.LabelLine15.Size = new System.Drawing.Size(102, 15);
            this.LabelLine15.TabIndex = 2;
            this.LabelLine15.Text = "考试未开始时 (1)";
            // 
            // GBoxChangeFont
            // 
            this.GBoxChangeFont.Controls.Add(this.ButtonRestoreFont);
            this.GBoxChangeFont.Controls.Add(this.ButtonChooseFont);
            this.GBoxChangeFont.Controls.Add(this.LabelPreviewFont);
            this.GBoxChangeFont.Controls.Add(this.LabelFontInfo);
            this.GBoxChangeFont.Controls.Add(this.LabelLine11);
            this.GBoxChangeFont.Location = new System.Drawing.Point(7, 6);
            this.GBoxChangeFont.Name = "GBoxChangeFont";
            this.GBoxChangeFont.Size = new System.Drawing.Size(405, 121);
            this.GBoxChangeFont.TabIndex = 0;
            this.GBoxChangeFont.TabStop = false;
            this.GBoxChangeFont.Text = "字体和大小";
            // 
            // ButtonRestoreFont
            // 
            this.ButtonRestoreFont.AutoSize = true;
            this.ButtonRestoreFont.Location = new System.Drawing.Point(98, 90);
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
            this.ButtonChooseFont.Location = new System.Drawing.Point(9, 90);
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
            // TabPageTools
            // 
            this.TabPageTools.Controls.Add(this.GBoxVDM);
            this.TabPageTools.Controls.Add(this.GBoxRestart);
            this.TabPageTools.Controls.Add(this.GBoxSyncTime);
            this.TabPageTools.Location = new System.Drawing.Point(4, 24);
            this.TabPageTools.Name = "TabPageTools";
            this.TabPageTools.Padding = new System.Windows.Forms.Padding(3);
            this.TabPageTools.Size = new System.Drawing.Size(421, 294);
            this.TabPageTools.TabIndex = 1;
            this.TabPageTools.Text = "工具";
            this.TabPageTools.UseVisualStyleBackColor = true;
            // 
            // GBoxVDM
            // 
            this.GBoxVDM.Controls.Add(this.CheckBoxEnableVDM);
            this.GBoxVDM.Controls.Add(this.LabelLine2);
            this.GBoxVDM.Controls.Add(this.LabelLine1);
            this.GBoxVDM.Location = new System.Drawing.Point(7, 110);
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
            this.CheckBoxEnableVDM.Size = new System.Drawing.Size(106, 19);
            this.CheckBoxEnableVDM.TabIndex = 2;
            this.CheckBoxEnableVDM.Text = "启用此功能(&V)";
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
            // GBoxRestart
            // 
            this.GBoxRestart.Controls.Add(this.LabelLine10);
            this.GBoxRestart.Controls.Add(this.LabelLine9);
            this.GBoxRestart.Controls.Add(this.ButtonRestart);
            this.GBoxRestart.Location = new System.Drawing.Point(7, 194);
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
            this.GBoxSyncTime.Controls.Add(this.LabelLine7);
            this.GBoxSyncTime.Controls.Add(this.LabelLine6);
            this.GBoxSyncTime.Controls.Add(this.LabelLine5);
            this.GBoxSyncTime.Controls.Add(this.ButtonSyncTime);
            this.GBoxSyncTime.Location = new System.Drawing.Point(7, 6);
            this.GBoxSyncTime.Name = "GBoxSyncTime";
            this.GBoxSyncTime.Size = new System.Drawing.Size(405, 98);
            this.GBoxSyncTime.TabIndex = 44;
            this.GBoxSyncTime.TabStop = false;
            this.GBoxSyncTime.Text = "同步网络时钟";
            // 
            // LabelLine7
            // 
            this.LabelLine7.AutoSize = true;
            this.LabelLine7.ForeColor = System.Drawing.Color.Red;
            this.LabelLine7.Location = new System.Drawing.Point(6, 49);
            this.LabelLine7.Name = "LabelLine7";
            this.LabelLine7.Size = new System.Drawing.Size(227, 15);
            this.LabelLine7.TabIndex = 23;
            this.LabelLine7.Text = "启动 Windows Time 服务, 请谨慎操作。";
            // 
            // LabelLine6
            // 
            this.LabelLine6.AutoSize = true;
            this.LabelLine6.ForeColor = System.Drawing.Color.Red;
            this.LabelLine6.Location = new System.Drawing.Point(6, 34);
            this.LabelLine6.Name = "LabelLine6";
            this.LabelLine6.Size = new System.Drawing.Size(396, 15);
            this.LabelLine6.TabIndex = 22;
            this.LabelLine6.Text = "注意: 此项会将系统的 NTP 服务器设置为 ntp1.aliyun.com, 并且将自动";
            // 
            // LabelLine5
            // 
            this.LabelLine5.AutoSize = true;
            this.LabelLine5.Location = new System.Drawing.Point(6, 19);
            this.LabelLine5.Name = "LabelLine5";
            this.LabelLine5.Size = new System.Drawing.Size(371, 15);
            this.LabelLine5.TabIndex = 20;
            this.LabelLine5.Text = "通过运行系统命令将当前系统时间与网络同步以确保准确无误。";
            // 
            // FormSettings
            // 
            this.AcceptButton = this.ButtonSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(443, 360);
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
            this.GBoxPPTService.ResumeLayout(false);
            this.GBoxPPTService.PerformLayout();
            this.GBoxContent.ResumeLayout(false);
            this.GBoxContent.PerformLayout();
            this.GBoxSetDragable.ResumeLayout(false);
            this.GBoxSetDragable.PerformLayout();
            this.TabPageStyle.ResumeLayout(false);
            this.GBoxColors.ResumeLayout(false);
            this.GBoxColors.PerformLayout();
            this.GBoxChangeFont.ResumeLayout(false);
            this.GBoxChangeFont.PerformLayout();
            this.TabPageTools.ResumeLayout(false);
            this.GBoxVDM.ResumeLayout(false);
            this.GBoxVDM.PerformLayout();
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
        private System.Windows.Forms.TabPage TabPageTools;
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
        private System.Windows.Forms.GroupBox GBoxVDM;
        private System.Windows.Forms.Label LabelLine1;
        private System.Windows.Forms.CheckBox CheckBoxEnableVDM;
        private System.Windows.Forms.Label LabelLine2;
        private System.Windows.Forms.CheckBox CheckBoxEnableMO;
        private System.Windows.Forms.TabPage TabPageDisplay;
        private System.Windows.Forms.GroupBox GBoxChangeFont;
        private System.Windows.Forms.Label LabelLine11;
        private System.Windows.Forms.Label LabelFontInfo;
        private System.Windows.Forms.Label LabelPreviewFont;
        private System.Windows.Forms.Button ButtonChooseFont;
        private System.Windows.Forms.Button ButtonRestoreFont;
        private System.Windows.Forms.GroupBox GBoxContent;
        private System.Windows.Forms.CheckBox CheckBoxShowOnly;
        private System.Windows.Forms.CheckBox CheckBoxShowPast;
        private System.Windows.Forms.CheckBox CheckBoxShowEnd;
        private System.Windows.Forms.CheckBox CheckBoxSetRounding;
        private System.Windows.Forms.GroupBox GBoxSetDragable;
        private System.Windows.Forms.CheckBox CheckBoxEnableDragable;
        private System.Windows.Forms.Label LabelLine12;
        private System.Windows.Forms.CheckBox CheckBoxSetUniTopMost;
        private System.Windows.Forms.GroupBox GBoxPPTService;
        private System.Windows.Forms.Label LabelLine13;
        private System.Windows.Forms.CheckBox CheckBoxSwPptSvc;
        private System.Windows.Forms.Label LabelScreensHint;
        private System.Windows.Forms.Label LabelScreens;
        private System.Windows.Forms.ComboBox ComboBoxScreens;
        private System.Windows.Forms.ComboBox ComboBoxShowOnly;
        private System.Windows.Forms.TabPage TabPageStyle;
        private System.Windows.Forms.GroupBox GBoxColors;
        private System.Windows.Forms.Label LabelColor32;
        private System.Windows.Forms.Label LabelColor31;
        private System.Windows.Forms.Label LabelColor22;
        private System.Windows.Forms.Label LabelColor21;
        private System.Windows.Forms.Label LabelColor12;
        private System.Windows.Forms.Label LabelColor11;
        private System.Windows.Forms.Label LabelLine17;
        private System.Windows.Forms.Label LabelLine16;
        private System.Windows.Forms.Label LabelLine15;
        private System.Windows.Forms.Button ButtonColorApply;
        private System.Windows.Forms.Button ButtonColorDefault;
        private System.Windows.Forms.Label LabelLine14;
        private System.Windows.Forms.Label LabelPreviewCorlor1;
        private System.Windows.Forms.Label LabelPreviewCorlor2;
        private System.Windows.Forms.Label LabelPreviewCorlor3;
        private System.Windows.Forms.Label LabelPreviewCorlor4;
        private System.Windows.Forms.Label LabelColor42;
        private System.Windows.Forms.Label LabelColor41;
        private System.Windows.Forms.Label LabelLine18;
    }
}