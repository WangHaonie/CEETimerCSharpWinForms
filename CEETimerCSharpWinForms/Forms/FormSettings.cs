using CEETimerCSharpWinForms.Modules;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CEETimerCSharpWinForms.Forms
{
    public partial class FormSettings : Form
    {
        private class ComboSource
        {
            public string Item { get; set; }
            public int Value { get; set; }
        }

        private enum WorkingArea
        {
            Funny, SyncTime, SetPPTService, LastColor, SelectedColor, ColorToAll, DefaultColor
        }

        public static event EventHandler ConfigChanged;
        public static Color Fore1 { get; set; }
        public static Color Fore2 { get; set; }
        public static Color Fore3 { get; set; }
        public static Color Fore4 { get; set; }
        public static Color Back1 { get; set; }
        public static Color Back2 { get; set; }
        public static Color Back3 { get; set; }
        public static Color Back4 { get; set; }
        public static bool FeatureMOEnabled { get; set; }
        public static bool IsShowOnly { get; set; }
        public static bool IsDragable { get; set; }
        public static bool IsShowEnd { get; set; }
        public static bool IsShowPast { get; set; }
        public static bool IsRounding { get; set; }
        public static bool IsPPTService { get; set; }
        public static bool TopMostChecked { get; set; }
        public static int ScreenIndex { get; set; }
        public static int ShowOnlyIndex { get; set; }
        public static int PositionIndex { get; set; }
        public static DateTime ExamStartTime { get; set; }
        public static DateTime ExamEndTime { get; set; }
        public static Font CountdownFont { get; set; }
        public static FontStyle CountdownFontStyle { get; set; }
        public static string ExamName { get; set; }

        private bool IsSyncingTime;
        private bool IsSettingsChanged;
        private readonly FontConverter fontConverter = new();

        public FormSettings()
        {
            InitializeComponent();
            InitializeExtra();
        }

        private void InitializeExtra()
        {
            ChangeColor(WorkingArea.LastColor);

            List<ComboSource> Shows =
            [
                new ComboSource { Item = "天", Value = 0 },
                new ComboSource { Item = "时", Value = 1 },
                new ComboSource { Item = "分", Value = 2 },
                new ComboSource { Item = "秒", Value = 3 }
            ];

            ComboBoxShowOnly.DataSource = Shows;
            ComboBoxShowOnly.DisplayMember = "Item";
            ComboBoxShowOnly.ValueMember = "Value";

            List<ComboSource> Positions =
            [
                new ComboSource { Item = "左上角", Value = 0 },
                new ComboSource { Item = "左部中央", Value = 1 },
                new ComboSource { Item = "左下角", Value = 2 },
                new ComboSource { Item = "上部中央", Value = 3 },
                new ComboSource { Item = "中央", Value = 4 },
                new ComboSource { Item = "下部中央", Value = 5 },
                new ComboSource { Item = "右上角", Value = 6 },
                new ComboSource { Item = "右部中央", Value = 7 },
                new ComboSource { Item = "右下角", Value = 8 }
            ];

            ComboBoxPosition.DataSource = Positions;
            ComboBoxPosition.DisplayMember = "Item";
            ComboBoxPosition.ValueMember = "Value";
        }

        private void FormSettings_Load(object sender, EventArgs e)
        {
            TopMost = FormMain.IsUniTopMost;
            ChangeWorkingStyle(false, WorkingArea.Funny);
            RefreshScreens();
            RefreshSettings();
            IsSettingsChanged = false;
            ButtonSave.Enabled = false;
        }

        private void SettingsChanged(object sender, EventArgs e)
        {
            IsSettingsChanged = true;
            ButtonSave.Enabled = true;
        }

        private void TextBoxExamName_TextChanged(object sender, EventArgs e)
        {
            SettingsChanged(sender, e);
            int CharCount = TextBoxExamName.Text.RemoveAllBadChars().Length;
            LabelExamNameCounter.Text = $"{CharCount}/15";
            LabelExamNameCounter.ForeColor = CharCount > 15 ? Color.Red : Color.Black;
        }

        private void CheckBoxShowOnly_CheckedChanged(object sender, EventArgs e)
        {
            SettingsChanged(sender, e);
            CheckBoxSetRounding.Enabled = ComboBoxShowOnly.Enabled = CheckBoxShowOnly.Checked;
            ComboBoxShowOnly.SelectedIndex = CheckBoxShowOnly.Checked ? ShowOnlyIndex : 0;

            if (CheckBoxSetRounding.Checked && !CheckBoxShowOnly.Checked)
            {
                CheckBoxSetRounding.Checked = false;
                CheckBoxSetRounding.Enabled = false;
            }
        }

        private void CheckBoxSetTopMost_CheckedChanged(object sender, EventArgs e)
        {
            ChangePptsvcCtrlStyle(sender, e);
            CheckBoxSetUniTopMost.Enabled = CheckBoxSetTopMost.Checked;

            if (CheckBoxSetUniTopMost.Checked && !CheckBoxSetTopMost.Checked)
            {
                CheckBoxSetUniTopMost.Checked = false;
                CheckBoxSetUniTopMost.Enabled = false;
            }
        }

        private void CheckBoxShowEnd_CheckedChanged(object sender, EventArgs e)
        {
            SettingsChanged(sender, e);
            DTPExamEnd.Enabled = CheckBoxShowEnd.Checked;
        }

        private void CheckBoxShowPast_CheckedChanged(object sender, EventArgs e)
        {
            SettingsChanged(sender, e);
            CheckBoxShowEnd.Checked = CheckBoxShowPast.Checked;
            CheckBoxShowEnd.Enabled = !CheckBoxShowPast.Checked;
        }

        private void ButtonChooseFont_Click(object sender, EventArgs e)
        {
            FontDialog FontDialogMain = new()
            {
                AllowScriptChange = true,
                AllowVerticalFonts = false,
                Font = CountdownFont,
                FontMustExist = true,
                MinSize = 10,
                MaxSize = 24,
                ScriptsOnly = true,
                ShowEffects = false
            };

            if (FontDialogMain.ShowDialog() == DialogResult.OK)
            {
                SettingsChanged(sender, e);
                ChangeFont(FontDialogMain.Font);
            }

            FontDialogMain.Dispose();
        }

        private void ButtonRestoreFont_Click(object sender, EventArgs e)
        {
            ChangeFont(new((Font)fontConverter.ConvertFromString(LaunchManager.OriginalFontString), FontStyle.Bold));
            SettingsChanged(sender, e);
        }

        private void ColorLabels_Click(object sender, EventArgs e)
        {
            var LabelColor = (Label)sender;

            ColorDialog ColorDialogMain = new()
            {
                AllowFullOpen = true,
                Color = LabelColor.BackColor,
                FullOpen = true
            };

            if (ColorDialogMain.ShowDialog() == DialogResult.OK)
            {
                SettingsChanged(sender, e);
                LabelColor.BackColor = ColorDialogMain.Color;
                ChangeColor(WorkingArea.SelectedColor);
            }

            ColorDialogMain.Dispose();
        }

        private void ButtonColorApply_Click(object sender, EventArgs e)
        {
            SettingsChanged(sender, e);
            ChangeColor(WorkingArea.ColorToAll);
        }

        private void ButtonColorDefault_Click(object sender, EventArgs e)
        {
            SettingsChanged(sender, e);
            ChangeColor(WorkingArea.DefaultColor);
        }

        private void ButtonRestart_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ButtonRestart.Click -= ButtonRestart_Click;
                ButtonRestart.Click += ButtonRestart_Funny_Click;
                ChangeWorkingStyle(true, WorkingArea.Funny);
            }
        }

        private void ButtonRestart_Click(object sender, EventArgs e)
        {
            LaunchManager.Restart();
        }

        private void ButtonRestart_Funny_Click(object sender, EventArgs e)
        {
            LaunchManager.Shutdown();
        }

        private async void ButtonSyncTime_Click(object sender, EventArgs e)
        {
            ChangeWorkingStyle(true, WorkingArea.SyncTime);
            await Task.Run(StartSyncTime);
            ChangeWorkingStyle(false, WorkingArea.SyncTime);
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            if (IsSettingsFormatValid())
            {
                IsSettingsChanged = false;
                SaveSettings();
                ConfigChanged?.Invoke(this, EventArgs.Empty);
                Close();
            }
        }

        private void CheckBoxEnableDragable_CheckedChanged(object sender, EventArgs e)
        {
            SettingsChanged(sender, e);
            ComboBoxScreens.SelectedValue = CheckBoxEnableDragable.Checked ? 0 : ScreenIndex;
            ComboBoxPosition.SelectedValue = CheckBoxEnableDragable.Checked ? 0 : PositionIndex;
            LabelScreens.Enabled = LabelScreensHint.Enabled = ComboBoxScreens.Enabled = !CheckBoxEnableDragable.Checked;
        }

        private void ComboBoxes_DropDown(object sender, EventArgs e)
        {
            #region
            /*
             
            DropDown 自适应大小 参考：

            c# - Auto-width of ComboBox's content - Stack Overflow
            https://stackoverflow.com/a/16435431/21094697
            c# - ComboBox auto DropDownWidth regardless of DataSource type - Stack Overflow
            https://stackoverflow.com/a/69350288/21094697
             
             */

            var ComboBoxes = (ComboBox)sender;
            int MaxWidth = 0;

            foreach (var Item in ComboBoxes.Items)
            {
                MaxWidth = Math.Max(MaxWidth, TextRenderer.MeasureText(ComboBoxes.GetItemText(Item), ComboBoxes.Font).Width);
            }

            ComboBoxes.DropDownWidth = MaxWidth + SystemInformation.VerticalScrollBarWidth;

            #endregion
        }

        private void ComboBoxShowOnly_SelectedIndexChanged(object sender, EventArgs e)
        {
            SettingsChanged(sender, e);
            CheckBoxSetRounding.Visible = ComboBoxShowOnly.SelectedIndex == 0;
            CheckBoxSetRounding.Checked = ComboBoxShowOnly.SelectedIndex == 0 && IsRounding;
        }

        private void ComboBoxScreens_SelectedIndexChanged(object sender, EventArgs e)
        {
            SettingsChanged(sender, e);

            ComboBoxPosition.Enabled = !CheckBoxEnableDragable.Checked && ComboBoxScreens.SelectedIndex != 0;
            ComboBoxPosition.SelectedIndex = ComboBoxPosition.Enabled ? PositionIndex : 0;
        }

        private void FormSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (IsSyncingTime)
            {
                e.Cancel = true;
            }
            else if (IsSettingsChanged)
            {
                if (MessageX.Popup("检测到设置被更改但没有被保存，是否立即进行保存？", MessageLevel.Warning, MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    e.Cancel = true;
                    ButtonSave_Click(sender, e);
                }
                else
                {
                    IsSettingsChanged = false;
                    Close();
                }
            }
        }

        private void ButtonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FormSettings_FormClosed(object sender, FormClosedEventArgs e)
        {
            ChangeWorkingStyle(false, WorkingArea.Funny);
            ButtonRestart.Click -= ButtonRestart_Funny_Click;
            ButtonRestart.Click += ButtonRestart_Click;
        }

        private void ChangeWorkingStyle(bool IsWorking, WorkingArea Where, int SubCase = 0)
        {
            switch (Where)
            {
                case WorkingArea.SyncTime:
                    IsSyncingTime = IsWorking;
                    ButtonSyncTime.Enabled = !IsWorking;
                    ButtonRestart.Enabled = !IsWorking;
                    ButtonSyncTime.Text = IsWorking ? "正在同步中，请稍候..." : "立即同步(&S)";
                    break;
                case WorkingArea.Funny:
                    GBoxRestart.Text = IsWorking ? "关闭倒计时" : "重启倒计时";
                    LabelLine9.Text = IsWorking ? "当然, 你也可以关闭此倒计时。(●'◡'●)" : "用于更改了屏幕缩放或者分辨率之后, 可以点击此按钮来重启倒计时";
                    LabelLine10.Text = IsWorking ? "" : "以确保窗口的文字不会变模糊。";
                    ButtonRestart.Text = IsWorking ? "点击关闭(&L)" : "点击重启(&R)";
                    break;
                case WorkingArea.SetPPTService:
                    CheckBoxSwPptSvc.Enabled = IsWorking;
                    CheckBoxSwPptSvc.Checked = IsWorking && IsPPTService;
                    CheckBoxSwPptSvc.Text = IsWorking ? "启用此功能(&X)" : $"此项暂不可用，因为倒计时没有{(SubCase == 0 ? "顶置" : "在左上角")}，不会引起冲突";
                    break;
            }
        }

        private void ChangePptsvcCtrlStyle(object sender, EventArgs e)
        {
            SettingsChanged(sender, e);

            var a = CheckBoxSetTopMost.Checked;
            var b = ComboBoxPosition.SelectedIndex;
            var c = CheckBoxEnableDragable.Checked;

            if ((a && b == 0) || c)
            {
                ChangeWorkingStyle(true, WorkingArea.SetPPTService);
            }
            else if ((!a && b == 0) || (!a && b != 0))
            {
                ChangeWorkingStyle(false, WorkingArea.SetPPTService);
            }
            else
            {
                ChangeWorkingStyle(false, WorkingArea.SetPPTService, 1);
            }
        }

        private void RefreshScreens()
        {
            Screen[] CurrentScreens = Screen.AllScreens;
            List<ComboSource> Monitors = [];
            int i = 0;

            Monitors.Add(new ComboSource
            {
                Item = "<请选择>",
                Value = 0,
            });

            foreach (var CurrentScreen in CurrentScreens)
            {
                i++;

                Monitors.Add(new ComboSource
                {
                    Item = $"{i} {CurrentScreen.DeviceName} ({CurrentScreen.Bounds.Width}x{CurrentScreen.Bounds.Height})",
                    Value = i
                });
            }

            ComboBoxScreens.DataSource = Monitors;
            ComboBoxScreens.DisplayMember = "Item";
            ComboBoxScreens.ValueMember = "Value";
        }

        private void RefreshSettings()
        {
            CheckBoxStartup.Checked = (Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true)?.GetValue("CEETimerCSharpWinForms") is string regvalue) && regvalue.Equals($"\"{LaunchManager.CurrentExecutable}\"", StringComparison.OrdinalIgnoreCase);

            CheckBoxSetTopMost.Checked = TopMostChecked;
            TextBoxExamName.Text = ExamName;
            DTPExamStart.Value = ConfigManager.IsValidData(ExamStartTime) ? ExamStartTime : DateTime.Now;
            DTPExamEnd.Value = ConfigManager.IsValidData(ExamEndTime) ? ExamEndTime : DateTime.Now;
            CheckBoxEnableMO.Checked = FeatureMOEnabled;
            CheckBoxEnableDragable.Checked = IsDragable;
            CheckBoxShowOnly.Checked = IsShowOnly;
            CheckBoxSetRounding.Checked = IsRounding;
            CheckBoxShowEnd.Checked = DTPExamEnd.Enabled = IsShowEnd;
            CheckBoxShowPast.Checked = IsShowPast;
            CheckBoxSwPptSvc.Checked = IsPPTService;
            CheckBoxSetUniTopMost.Checked = TopMost;
            ComboBoxScreens.SelectedValue = ScreenIndex;
            ComboBoxPosition.SelectedValue = PositionIndex;
            ComboBoxShowOnly.SelectedValue = ShowOnlyIndex;
            ChangeFont(new Font(CountdownFont, CountdownFontStyle));
            ChangePptsvcCtrlStyle(null, EventArgs.Empty);

            ComboBoxShowOnly.SelectedIndex = IsShowOnly ? ShowOnlyIndex : 0;
        }

        private bool IsSettingsFormatValid()
        {
            ExamName = TextBoxExamName.Text.RemoveAllBadChars();
            TimeSpan ExamTimeSpan = DTPExamEnd.Value - DTPExamStart.Value;
            string UniMsg = "";
            string TimeMsg = "";

            if (string.IsNullOrWhiteSpace(ExamName) || (ExamName.Length < 2) || (ExamName.Length > 15))
            {
                TabControlMain.SelectedTab = TabPageGeneral;
                MessageX.Popup("输入的考试名称有误！\n\n请检查输入的考试名称是否太长或太短！", MessageLevel.Error);
                return false;
            }

            if (DTPExamEnd.Enabled)
            {
                if (DTPExamEnd.Value <= DTPExamStart.Value)
                {
                    TabControlMain.SelectedTab = TabPageGeneral;
                    MessageX.Popup("考试结束时间必须在开始时间之后！", MessageLevel.Error);
                    return false;
                }
                else if (ExamTimeSpan.TotalDays > 4)
                {
                    TimeMsg = $"{ExamTimeSpan.TotalDays:0} 天";
                }
                else if (ExamTimeSpan.TotalMinutes < 40 && ExamTimeSpan.TotalSeconds > 60)
                {
                    TimeMsg = $"{ExamTimeSpan.TotalMinutes:0} 分钟";
                }
                else if (ExamTimeSpan.TotalSeconds < 60)
                {
                    TimeMsg = $"{ExamTimeSpan.TotalSeconds:0} 秒";
                }

                if (!string.IsNullOrEmpty(TimeMsg))
                {
                    UniMsg = $"检测到设置的考试时间太长或太短！\n\n当前考试时长: {TimeMsg}。\n\n如果你确认当前设置的是正确的考试时间，请点击确定，否则请点击取消。";
                }

                if (!string.IsNullOrEmpty(UniMsg))
                {
                    TabControlMain.SelectedTab = TabPageGeneral;
                    if (MessageX.Popup(UniMsg, MessageLevel.Warning, MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                    {
                        return false;
                    }
                }
            }

            string ColorCheckMsg = "";
            if (!ColorHelper.IsNiceContrast(LabelPreviewColor1.ForeColor, LabelPreviewColor1.BackColor))
            {
                ColorCheckMsg = "1";
            }
            else if (!ColorHelper.IsNiceContrast(LabelPreviewColor2.ForeColor, LabelPreviewColor2.BackColor))
            {
                ColorCheckMsg = "2";
            }
            else if (!ColorHelper.IsNiceContrast(LabelPreviewColor3.ForeColor, LabelPreviewColor3.BackColor))
            {
                ColorCheckMsg = "3";
            }
            else if (!ColorHelper.IsNiceContrast(LabelPreviewColor4.ForeColor, LabelPreviewColor4.BackColor))
            {
                ColorCheckMsg = "4";
            }

#if DEBUG
            Console.WriteLine("##########################");
#endif

            if (!string.IsNullOrEmpty(ColorCheckMsg))
            {
                TabControlMain.SelectedTab = TabPageStyle;
                MessageX.Popup($"第{ColorCheckMsg}组的颜色相似或对比度较低，将无法看清文字，请尝试更换其它背景颜色或文字颜色！", MessageLevel.Error);
                return false;
            }

            return true;
        }

        private void StartSyncTime()
        {
            try
            {
                Process SyncTimeProcess = Process.Start(new ProcessStartInfo
                {
                    UseShellExecute = true,
                    FileName = "cmd.exe",
                    Arguments = "/c net stop w32time & sc config w32time start= auto & net start w32time && w32tm /config /manualpeerlist:ntp1.aliyun.com /syncfromflags:manual /reliable:YES /update && w32tm /resync && w32tm /resync",
                    Verb = "runas",
                    CreateNoWindow = true,
                    WindowStyle = ProcessWindowStyle.Hidden
                });

                SyncTimeProcess.WaitForExit();
                var ExitCode = SyncTimeProcess.ExitCode;
                MessageX.Popup($"命令执行完成！\n\n返回值为 {ExitCode} (0x{ExitCode:X})\n(0 代表成功，其他值为失败)", MessageLevel.Info, this, TabControlMain, TabPageTools);
            }
            catch (Win32Exception ex)
            {
                #region 来自网络
                /*
                 
                检测用户是否点击了 UAC 提示框的 "否" 参考:

                c# - Run process as administrator from a non-admin application - Stack Overflow
                https://stackoverflow.com/a/20872219/21094697
                 
                 */
                if (ex.NativeErrorCode == 1223)
                {
                    MessageX.Popup("请在 UAC 对话框弹出时点击 \"是\"。", ex, this, TabControlMain, TabPageTools);
                }
                #endregion
            }
            catch (Exception ex)
            {
                MessageX.Popup($"命令执行时发生了错误。", ex, this, TabControlMain, TabPageTools);
            }
        }

        private void ChangeFont(Font NewFont)
        {
            CountdownFont = NewFont;
            CountdownFontStyle = NewFont.Style;
            LabelPreviewFont.Font = NewFont;
            LabelFontInfo.Text = $"当前字体: {NewFont.Name}, {NewFont.Size}pt, {NewFont.Style}";
        }

        private void ChangeColor(WorkingArea Where)
        {
            switch (Where)
            {
                case WorkingArea.LastColor:
                    LabelPreviewColor1.BackColor = LabelColor11.BackColor = Back1;
                    LabelPreviewColor2.BackColor = LabelColor21.BackColor = Back2;
                    LabelPreviewColor3.BackColor = LabelColor31.BackColor = Back3;
                    LabelPreviewColor4.BackColor = LabelColor41.BackColor = Back4;
                    LabelPreviewColor1.ForeColor = LabelColor12.BackColor = Fore1;
                    LabelPreviewColor2.ForeColor = LabelColor22.BackColor = Fore2;
                    LabelPreviewColor3.ForeColor = LabelColor32.BackColor = Fore3;
                    LabelPreviewColor4.ForeColor = LabelColor42.BackColor = Fore4;
                    break;
                case WorkingArea.SelectedColor:
                    LabelPreviewColor1.BackColor = LabelColor11.BackColor;
                    LabelPreviewColor2.BackColor = LabelColor21.BackColor;
                    LabelPreviewColor3.BackColor = LabelColor31.BackColor;
                    LabelPreviewColor4.BackColor = LabelColor41.BackColor;
                    LabelPreviewColor1.ForeColor = LabelColor12.BackColor;
                    LabelPreviewColor2.ForeColor = LabelColor22.BackColor;
                    LabelPreviewColor3.ForeColor = LabelColor32.BackColor;
                    LabelPreviewColor4.ForeColor = LabelColor42.BackColor;
                    break;
                case WorkingArea.ColorToAll:
                    LabelColor41.BackColor = LabelColor31.BackColor = LabelColor21.BackColor =
                    LabelPreviewColor4.BackColor = LabelPreviewColor3.BackColor = LabelPreviewColor2.BackColor = LabelColor11.BackColor;
                    LabelColor42.BackColor = LabelColor32.BackColor = LabelColor22.BackColor =
                    LabelPreviewColor4.ForeColor = LabelPreviewColor3.ForeColor = LabelPreviewColor2.ForeColor = LabelColor12.BackColor;
                    break;
                case WorkingArea.DefaultColor:
                    LabelPreviewColor1.BackColor = LabelColor11.BackColor =
                    LabelPreviewColor2.BackColor = LabelColor21.BackColor =
                    LabelPreviewColor3.BackColor = LabelColor31.BackColor =
                    LabelPreviewColor4.BackColor = LabelColor41.BackColor = Color.White;
                    LabelPreviewColor1.ForeColor = LabelColor12.BackColor = Color.Red;
                    LabelPreviewColor2.ForeColor = LabelColor22.BackColor = Color.Green;
                    LabelPreviewColor3.ForeColor = LabelColor32.BackColor =
                    LabelPreviewColor4.ForeColor = LabelColor42.BackColor = Color.Black;
                    break;
            }
        }

        private void SaveSettings()
        {
            try
            {
                RegistryKey reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

                if (CheckBoxStartup.Checked)
                {
                    reg.SetValue("CEETimerCSharpWinForms", $"\"{LaunchManager.CurrentExecutable}\"");
                }
                else
                {
                    reg.DeleteValue("CEETimerCSharpWinForms", false);
                }

                ConfigManager.WriteConfig(new Dictionary<string, string>
                {
                    { "ExamName", ExamName },
                    { "ExamStartTime", $"{DTPExamStart.Value:yyyyMMddHHmmss}" },
                    { "ExamEndTime", $"{DTPExamEnd.Value:yyyyMMddHHmmss}" },
                    { "TopMost", $"{CheckBoxSetTopMost.Checked}" },
                    { "FeatureMO", $"{CheckBoxEnableMO.Checked}" },
                    { "Font", $"{CountdownFont.Name}, {CountdownFont.Size}pt" },
                    { "FontStyle", $"{CountdownFontStyle}" },
                    { "Fore1", $"{LabelColor12.BackColor.R},{LabelColor12.BackColor.G},{LabelColor12.BackColor.B}" },
                    { "Back1", $"{LabelColor11.BackColor.R},{LabelColor11.BackColor.G},{LabelColor11.BackColor.B}" },
                    { "Fore2", $"{LabelColor22.BackColor.R},{LabelColor22.BackColor.G},{LabelColor22.BackColor.B}" },
                    { "Back2", $"{LabelColor21.BackColor.R},{LabelColor21.BackColor.G},{LabelColor21.BackColor.B}" },
                    { "Fore3", $"{LabelColor32.BackColor.R},{LabelColor32.BackColor.G},{LabelColor32.BackColor.B}" },
                    { "Back3", $"{LabelColor31.BackColor.R},{LabelColor31.BackColor.G},{LabelColor31.BackColor.B}" },
                    { "Fore4", $"{LabelColor42.BackColor.R},{LabelColor42.BackColor.G},{LabelColor42.BackColor.B}" },
                    { "Back4", $"{LabelColor41.BackColor.R},{LabelColor41.BackColor.G},{LabelColor41.BackColor.B}" },
                    { "Screen", $"{ComboBoxScreens.SelectedValue}" },
                    { "Pos", $"{ComboBoxPosition.SelectedValue}" },
                    { "ShowOnly", $"{CheckBoxShowOnly.Checked}" },
                    { "ShowValue", $"{ComboBoxShowOnly.SelectedValue}" },
                    { "Rounding", $"{CheckBoxSetRounding.Checked}" },
                    { "ShowEnd", $"{CheckBoxShowEnd.Checked}" },
                    { "ShowPast", $"{CheckBoxShowPast.Checked}" },
                    { "Dragable", $"{CheckBoxEnableDragable.Checked}" },
                    { "UniTopMost", $"{CheckBoxSetUniTopMost.Checked}" },
                    { "PPTService", $"{CheckBoxSwPptSvc.Checked}" }
                });
            }
            catch
            {
            }
        }

        #region
        /*
        
        解决设置窗口因控件较多导致的闪烁问题 参考:

        winform窗体闪烁问题解决 - 就叫我雷人吧 - 博客园
        https://www.cnblogs.com/guosheng/p/7417918.html

         */

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }
        #endregion
    }
}
