using CEETimerCSharpWinForms.Modules;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
        public bool TopMostChecked { get; set; }
        public DateTime ExamStartTime { get; set; }
        public DateTime ExamEndTime { get; set; }
        public Font CountdownFont { get; set; }
        public FontStyle CountdownFontStyle { get; set; }
        public string ExamName { get; set; }

        public delegate void ConfigChangedHandler(object sender, EventArgs e);
        public ConfigChangedHandler ConfigChanged;

        private bool isSyncingTime = false;
        private bool IsSettingsChanged;
        private const string GBoxRestartTitleOriginal = "重启倒计时";
        private const string LabelLine9Original = "如果你更改了屏幕缩放或者分辨率, 可以点击此按钮来重启倒计时以";
        private const string LabelLine10Original = "确保显示的文字不会变模糊。";
        private const string ButtonRestartTextOriginal = "点击重启(&R)";
        private const string GBoxRestartTitle = "关闭倒计时";
        private const string LabelLine9Text = "如果你由于某些原因需要临时关闭这个倒计时, 那你现在就可以选择";
        private const string LabelLine10Text = "关闭它了。";
        private const string ButtonRestartText = "点击关闭(&C)";
        private readonly FontConverter fontConverter = new();

        public FormSettings()
        {
            InitializeComponent();
        }

        private void FormSettings_Load(object sender, EventArgs e)
        {
            RestoreFunny();
            CheckStartupSetting();
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
                GBoxRestart.Text = GBoxRestartTitle;
                LabelLine9.Text = LabelLine9Text;
                LabelLine10.Text = LabelLine10Text;
                ButtonRestart.Text = ButtonRestartText;
            }
        }

        private async void ButtonSyncTime_Click(object sender, EventArgs e)
        {
            isSyncingTime = true;
            ButtonSyncTime.Enabled = false;
            ButtonRestart.Enabled = false;
            ButtonSyncTime.Text = "正在同步中，请稍候...";
            await Task.Run(StartSyncTime);
            isSyncingTime = false;
            ButtonSyncTime.Enabled = true;
            ButtonRestart.Enabled = true;
            ButtonSyncTime.Text = "立即同步(&S)";
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
                CountdownFont = FontDialogMain.Font;
                ChangeFont(CountdownFont);
                SettingsChanged(sender, e);
            }

            FontDialogMain.Dispose();
        }

        private void ButtonRestoreFont_Click(object sender, EventArgs e)
        {
            Font OriginalFont = new((Font)fontConverter.ConvertFromString(LaunchManager.OriginalFontString), FontStyle.Bold);

            ChangeFont(OriginalFont);
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

        private void CheckBoxSetNoStart_CheckedChanged(object sender, EventArgs e)
        {
            SettingsChanged(sender, e);
            CheckBoxSetNoPast.Checked = CheckBoxSetNoStart.Checked;
            CheckBoxSetNoPast.Enabled = !CheckBoxSetNoStart.Checked;
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
            if (isSyncingTime)
            {
                e.Cancel = true;
            }
            else if (IsSettingsChanged)
            {
                DialogResult result = MessageBox.Show("检测到设置被更改但没有被保存，是否立即进行保存？", LaunchManager.WarnMsg, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
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
            RestoreFunny();
            ButtonRestart.Click -= ButtonRestart_Funny_Click;
            ButtonRestart.Click += ButtonRestart_Click;
        }

        private void RefreshSettings()
        {
            CheckBoxSetTopMost.Checked = TopMostChecked;
            TextBoxExamName.Text = ExamName;
            DTPExamStart.Value = ConfigManager.IsValidData(ExamStartTime) ? ExamStartTime : DateTime.Now;
            DTPExamEnd.Value = ConfigManager.IsValidData(ExamEndTime) ? ExamEndTime : DateTime.Now;
            var SelectedFont = CountdownFont;
            CheckBoxEnableVDM.Checked = FeatureVDMEnabled;
            CheckBoxEnableMO.Checked = FeatureMOEnabled;
            CheckBoxEnableDragable.Checked = IsDragable;
            CheckBoxSetDaysOnly.Checked = IsDaysOnly;
            CheckBoxSetRounding.Checked = IsRounding;
            CheckBoxSetNoStart.Checked = IsNoStart;
            CheckBoxSetNoPast.Checked = IsNoPast;

            ChangeFont(new Font(SelectedFont, CountdownFontStyle));

            if (LaunchManager.CurrentWindowsVersion < 10)
            {
                CheckBoxEnableVDM.Enabled = false;
                CheckBoxEnableVDM.Checked = false;
                CheckBoxEnableVDM.Text = $"此功能在当前系统上不可用";
            }
        }

        private void CheckStartupSetting()
        {
            RegistryKey reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            if (reg.GetValue("CEETimerCSharpWinForms") is string regvalue)
            {
                if (regvalue.Equals(Application.ExecutablePath, StringComparison.OrdinalIgnoreCase))
                {
                    CheckBoxStartup.Checked = true;
                }
                else
                {
                    CheckBoxStartup.Checked = false;
                }
            }
            else
            {
                CheckBoxStartup.Checked = false;
            }
        }

        private bool ValidateInput()
        {
            ExamName = TextBoxExamName.Text.RemoveAllBadChars();

            if (string.IsNullOrWhiteSpace(ExamName) || (ExamName.Length < 2) || (ExamName.Length > 15))
            {
                MessageBox.Show("输入的考试名称有误！\n\n请检查输入的考试名称是否太长或太短！", LaunchManager.ErrMsg, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (DTPExamStart.Value >= DTPExamEnd.Value)
            {
                MessageBox.Show("考试开始时间必须在结束时间之前！", LaunchManager.ErrMsg, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            TimeSpan timeSpan = DTPExamEnd.Value - DTPExamStart.Value;
            string UniMsg = "";
            string TimeMsg = "";

            if (timeSpan.TotalDays > 4)
            {
                TimeMsg = $"{timeSpan.TotalDays:0.0} 天";
            }
            else if (timeSpan.TotalMinutes < 40 && timeSpan.TotalSeconds > 60)
            {
                TimeMsg = $"{timeSpan.TotalMinutes:0.0} 分钟";
            }
            else if (timeSpan.TotalSeconds < 60)
            {
                TimeMsg = $"{timeSpan.TotalSeconds:0.0} 秒";
            }

            if (!string.IsNullOrEmpty(TimeMsg))
            {
                UniMsg = $"检测到设置的考试时长太长或太短！\n\n当前考试时长: {TimeMsg}。\n\n如果你确定当前设置的是正确的考试时间，请点击确定，否则请点击取消。";
            }

            if (!string.IsNullOrEmpty(UniMsg))
            {
                DialogResult result = MessageBox.Show(UniMsg, LaunchManager.WarnMsg, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (result == DialogResult.Cancel)
                {
                    return false;
                }
            }

            return true;
        }

        private void SaveSettings()
        {
            try
            {
                RegistryKey reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

                if (CheckBoxStartup.Checked)
                {
                    reg.SetValue("CEETimerCSharpWinForms", LaunchManager.CurrentExecutable);
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
                    { "Dragable", CheckBoxEnableDragable.Checked.ToString() }
                });
            }
            catch
            {

            }

        }

        private void StartSyncTime()
        {
            ProcessStartInfo processStartInfo = new()
            {
                FileName = @"cmd.exe",
                Arguments = "/c w32tm /config /manualpeerlist:ntp1.aliyun.com /syncfromflags:manual /reliable:YES /update && net stop w32time && net start w32time && sc config w32time start= auto && w32tm /resync && w32tm /resync",
                Verb = "runas",
                CreateNoWindow = true,
                WindowStyle = ProcessWindowStyle.Hidden
            };

            Process syncTimeProcess = Process.Start(processStartInfo);
            syncTimeProcess.WaitForExit();
            MessageBox.Show($"命令执行完成！\n\n返回值为 {syncTimeProcess.ExitCode}\n(0 代表成功，其他值为失败)", LaunchManager.InfoMsg, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void RestoreFunny()
        {
            GBoxRestart.Text = GBoxRestartTitleOriginal;
            LabelLine9.Text = LabelLine9Original;
            LabelLine10.Text = LabelLine10Original;
            ButtonRestart.Text = ButtonRestartTextOriginal;
        }

        private void ChangeFont(Font NewFont)
        {
            CountdownFont = NewFont;
            CountdownFontStyle = NewFont.Style;
            LabelPreviewFont.Font = NewFont;
            LabelFontInfo.Text = $"当前选择的字体/大小/样式: {NewFont.Name}, {NewFont.Size}pt, {NewFont.Style}";
        }

        protected virtual void OnConfigChanged()
        {
            ConfigChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
