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
        public bool FeatureMOEnabled { get; set; }
        public bool FeatureVDMEnabled { get; set; }
        public bool IsDaysOnly { get; set; }
        public bool IsDragable { get; set; }
        public bool IsNoPast { get; set; }
        public bool IsNoStart { get; set; }
        public bool IsRounding { get; set; }
        public bool IsPPTService {  get; set; }
        public bool TopMostChecked { get; set; }
        public DateTime ExamStartTime { get; set; }
        public DateTime ExamEndTime { get; set; }
        public Font CountdownFont { get; set; }
        public FontStyle CountdownFontStyle { get; set; }
        public string ExamName { get; set; }

        public delegate void ConfigChangedHandler(object sender, EventArgs e);
        public static ConfigChangedHandler ConfigChanged;

        private bool IsSyncingTime = false;
        private bool IsSettingsChanged;
        private enum WorkingArea { Funny, SyncTime }
        private readonly FontConverter fontConverter = new();

        public FormSettings()
        {
            InitializeComponent();
            ConfigChanged += RefreshSettings;
        }

        private void FormSettings_Load(object sender, EventArgs e)
        {
            ChangeWorkingStyle(false, WorkingArea.Funny);
            RefreshSettings();
            IsSettingsChanged = false;
            ButtonSave.Enabled = false;
        }

        private void RefreshSettings(object sender, EventArgs e)
        {
            ConfigManager.SetTopMost(this);
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

        private void ButtonRestart_Click(object sender, EventArgs e)
        {
            LaunchManager.Restart();
        }

        private void ButtonRestart_Funny_Click(object sender, EventArgs e)
        {
            LaunchManager.Shutdown();
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

        private async void ButtonSyncTime_Click(object sender, EventArgs e)
        {
            ChangeWorkingStyle(true, WorkingArea.SyncTime);
            await Task.Run(StartSyncTime);
            ChangeWorkingStyle(false, WorkingArea.SyncTime);
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
                ChangeFont(FontDialogMain.Font);
                SettingsChanged(sender, e);
            }

            FontDialogMain.Dispose();
        }

        private void ButtonRestoreFont_Click(object sender, EventArgs e)
        {
            ChangeFont(new((Font)fontConverter.ConvertFromString(LaunchManager.OriginalFontString), FontStyle.Bold));
            SettingsChanged(sender, e);
        }

        private void CheckBoxSetDaysOnly_CheckedChanged(object sender, EventArgs e)
        {
            SettingsChanged(sender, e);
            CheckBoxSetRounding.Enabled = CheckBoxSetDaysOnly.Checked;

            if (CheckBoxSetRounding.Checked && !CheckBoxSetDaysOnly.Checked)
            {
                CheckBoxSetRounding.Checked = false;
                CheckBoxSetRounding.Enabled = false;
            }
        }

        private void CheckBoxSetTopMost_CheckedChanged(object sender, EventArgs e)
        {
            SettingsChanged(sender, e);
            CheckBoxSetUniTopMost.Enabled = CheckBoxSetTopMost.Checked;

            if (CheckBoxSetUniTopMost.Checked && !CheckBoxSetTopMost.Checked)
            {
                CheckBoxSetUniTopMost.Checked = false;
                CheckBoxSetUniTopMost.Enabled = false;
            }
        }

        private void CheckBoxSetNoStart_CheckedChanged(object sender, EventArgs e)
        {
            SettingsChanged(sender, e);
            CheckBoxSetNoPast.Checked = CheckBoxSetNoStart.Checked;
            CheckBoxSetNoPast.Enabled = !CheckBoxSetNoStart.Checked;
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                IsSettingsChanged = false;
                SaveSettings();
                OnConfigChanged();
                Close();
            }
        }

        private void ButtonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FormSettings_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
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
                    ButtonSave_Click(sender, e);
                }
                else
                {
                    IsSettingsChanged = false;
                    Close();
                }
            }
        }

        private void FormSettings_FormClosed(object sender, FormClosedEventArgs e)
        {
            ChangeWorkingStyle(false, WorkingArea.Funny);
            ButtonRestart.Click -= ButtonRestart_Funny_Click;
            ButtonRestart.Click += ButtonRestart_Click;
        }

        private void RefreshSettings()
        {
            CheckBoxStartup.Checked = (Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true)?.GetValue("CEETimerCSharpWinForms") is string regvalue) && regvalue.Equals($"\"{LaunchManager.CurrentExecutable}\"", StringComparison.OrdinalIgnoreCase);
            CheckBoxSwPptSvc.Checked = IsAccessible;

            CheckBoxSetTopMost.Checked = TopMostChecked;
            TextBoxExamName.Text = ExamName;
            DTPExamStart.Value = ConfigManager.IsValidData(ExamStartTime) ? ExamStartTime : DateTime.Now;
            DTPExamEnd.Value = ConfigManager.IsValidData(ExamEndTime) ? ExamEndTime : DateTime.Now;
            CheckBoxEnableVDM.Checked = FeatureVDMEnabled;
            CheckBoxEnableMO.Checked = FeatureMOEnabled;
            CheckBoxEnableDragable.Checked = IsDragable;
            CheckBoxSetDaysOnly.Checked = IsDaysOnly;
            CheckBoxSetRounding.Checked = IsRounding;
            CheckBoxSetNoStart.Checked = IsNoStart;
            CheckBoxSetNoPast.Checked = IsNoPast;
            CheckBoxSwPptSvc.Checked = IsPPTService;
            CheckBoxSetUniTopMost.Checked = ConfigManager.UniTopMost;

            ConfigManager.SetTopMost(this);

            ChangeFont(new Font(CountdownFont, CountdownFontStyle));

            if (LaunchManager.CurrentWindowsVersion < 10)
            {
                CheckBoxEnableVDM.Enabled = false;
                CheckBoxEnableVDM.Checked = false;
                CheckBoxEnableVDM.Text = $"此功能在当前系统上不可用";
            }
        }

        private void StartSyncTime()
        {
            try
            {
                Process SyncTimeProcess = Process.Start(new ProcessStartInfo
                {
                    UseShellExecute = true,
                    FileName = @"cmd.exe",
                    Arguments = "/c net stop w32time & sc config w32time start= auto & net start w32time && w32tm /config /manualpeerlist:ntp1.aliyun.com /syncfromflags:manual /reliable:YES /update && w32tm /resync && w32tm /resync",
                    Verb = "runas",
                    CreateNoWindow = true,
                    WindowStyle = ProcessWindowStyle.Hidden
                });

                SyncTimeProcess.WaitForExit();
                var ExitCode = SyncTimeProcess.ExitCode;
                MessageX.Popup($"命令执行完成！\n\n返回值为 {ExitCode} (0x{ExitCode:X})\n(0 代表成功，其他值为失败)", MessageLevel.Info, this);
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
                    MessageX.Popup("请在 UAC 对话框弹出时点击 \"是\"。", ex, this);
                }
                #endregion
            }
            catch (Exception ex)
            {
                MessageX.Popup($"命令执行时发生了错误。", ex, this);
            }
        }

        private bool ValidateInput()
        {
            ExamName = TextBoxExamName.Text.RemoveAllBadChars();
            TimeSpan ExamTimeSpan = DTPExamEnd.Value - DTPExamStart.Value;
            string UniMsg = "";
            string TimeMsg = "";

            if (string.IsNullOrWhiteSpace(ExamName) || (ExamName.Length < 2) || (ExamName.Length > 15))
            {
                MessageX.Popup("输入的考试名称有误！\n\n请检查输入的考试名称是否太长或太短！", MessageLevel.Error);
                return false;
            }
            else if (DTPExamStart.Value >= DTPExamEnd.Value)
            {
                MessageX.Popup("考试开始时间必须在结束时间之前！", MessageLevel.Error);
                return false;
            }
            else if (ExamTimeSpan.TotalDays > 4)
            {
                TimeMsg = $"{ExamTimeSpan.TotalDays:0.0} 天";
            }
            else if (ExamTimeSpan.TotalMinutes < 40 && ExamTimeSpan.TotalSeconds > 60)
            {
                TimeMsg = $"{ExamTimeSpan.TotalMinutes:0.0} 分钟";
            }
            else if (ExamTimeSpan.TotalSeconds < 60)
            {
                TimeMsg = $"{ExamTimeSpan.TotalSeconds:0.0} 秒";
            }

            if (!string.IsNullOrEmpty(TimeMsg))
            {
                UniMsg = $"检测到设置的考试时长太长或太短！\n\n当前考试时长: {TimeMsg}。\n\n如果你确定当前设置的是正确的考试时间，请点击确定，否则请点击取消。";
            }

            if (!string.IsNullOrEmpty(UniMsg))
            {
                if (MessageX.Popup(UniMsg, MessageLevel.Warning, MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                {
                    return false;
                }
            }

            return true;
        }

        private void ChangeFont(Font NewFont)
        {
            CountdownFont = NewFont;
            CountdownFontStyle = NewFont.Style;
            LabelPreviewFont.Font = NewFont;
            LabelFontInfo.Text = $"当前字体: {NewFont.Name}, {NewFont.Size}pt, {NewFont.Style}";
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
                    { "ExamStartTime", DTPExamStart.Value.ToString("yyyyMMddHHmmss") },
                    { "ExamEndTime", DTPExamEnd.Value.ToString("yyyyMMddHHmmss") },
                    { "TopMost", CheckBoxSetTopMost.Checked.ToString() },
                    { "FeatureVDM", CheckBoxEnableVDM.Checked.ToString() },
                    { "FeatureMO", CheckBoxEnableMO.Checked.ToString() },
                    { "Font", $"{CountdownFont.Name}, {CountdownFont.Size}pt" },
                    { "FontStyle", CountdownFontStyle.ToString() },
                    { "DaysOnly", CheckBoxSetDaysOnly.Checked.ToString() },
                    { "Rounding", CheckBoxSetRounding.Checked.ToString() },
                    { "NoStart", CheckBoxSetNoStart.Checked.ToString() },
                    { "NoPast", CheckBoxSetNoPast.Checked.ToString() },
                    { "Dragable", CheckBoxEnableDragable.Checked.ToString() },
                    { "UniTopMost", CheckBoxSetUniTopMost.Checked.ToString() },
                    { "PPTService", CheckBoxSwPptSvc.Checked.ToString() }
                });
            }
            catch
            {
            }
        }

        private void ChangeWorkingStyle(bool IsWorking, WorkingArea Where)
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
                    LabelLine9.Text = IsWorking ? "如果你由于某些原因需要临时关闭这个倒计时, 那你现在就可以选择" : "如果你更改了屏幕缩放或者分辨率, 可以点击此按钮来重启倒计时以";
                    LabelLine10.Text = IsWorking ? "关闭它了。" : "确保显示的文字不会变模糊。";
                    ButtonRestart.Text = IsWorking ? "点击关闭(&C)" : "点击重启(&R)";
                    break;
            }
        }

        protected virtual void OnConfigChanged()
        {
            ConfigChanged?.Invoke(this, EventArgs.Empty);
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
