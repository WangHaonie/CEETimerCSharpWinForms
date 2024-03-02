using CEETimerCSharpWinForms.Modules;
using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CEETimerCSharpWinForms.Forms
{
    public partial class FormSettings : Form
    {
        public Font CountdownFont { get; set; }
        private string ExamName;
        private Font SelectedFont;
        private FontStyle SelectedFontStyle;
        private DateTime ExamStartTime = new();
        private DateTime ExamEndTime = new();
        private FontConverter fontConverter = new();
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
        public delegate void ConfigChangedHandler(object sender, EventArgs e);
        public ConfigChangedHandler ConfigChanged;

        public FormSettings()
        {
            InitializeComponent();
        }

        private void FormSettings_Load(object sender, EventArgs e)
        {
            RestoreFunny();
            CheckStartupSetting();
            RefreshSettings();
            UpdateSomeControls();
            IsSettingsChanged = false;
            ButtonApply.Enabled = false;
        }

        private void SettingsChanged(object sender, EventArgs e)
        {
            IsSettingsChanged = true;
            ButtonApply.Enabled = true;
        }

        private void FormSettingsSetExamNameText_TextChanged(object sender, EventArgs e)
        {
            SettingsChanged(sender, e);
            int CharCount = TextBoxExamName.Text.RemoveAllBadChars().Length;
            LabelExamNameCounter.Text = $"{CharCount}/15";
            LabelExamNameCounter.ForeColor = CharCount > 15 ? Color.Red : Color.Black;
        }

        private void BtnRestart_Click(object sender, EventArgs e)
        {
            LaunchManager.Restart();
        }

        private void BtnRestartFunny_Click(object sender, EventArgs e)
        {
            LaunchManager.Shutdown();
        }

        private void BtnRestart_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ButtonRestart.Click -= BtnRestart_Click;
                ButtonRestart.Click += BtnRestartFunny_Click;
                GBoxRestart.Text = GBoxRestartTitle;
                LabelLine9.Text = LabelLine9Text;
                LabelLine10.Text = LabelLine10Text;
                ButtonRestart.Text = ButtonRestartText;
            }
        }

        private async void FormSettingsSyncTimeButton_Click(object sender, EventArgs e)
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

        private void FormSettingsApply_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                IsSettingsChanged = false;
                SaveSettings();
                OnConfigChanged();
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
                    FormSettingsApply_Click(sender, e);
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
            ButtonRestart.Click -= BtnRestartFunny_Click;
            ButtonRestart.Click += BtnRestart_Click;
        }

        private void FormSettingsCloseMain_Click(object sender, EventArgs e)
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

        public bool ValidateInput()
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

        private void RefreshSettings()
        {
            try
            {
                DateTime.TryParseExact(ConfigManager.ReadConfig("ExamStartTime"), "yyyyMMddHHmmss", null, System.Globalization.DateTimeStyles.None, out ExamStartTime);
                DateTime.TryParseExact(ConfigManager.ReadConfig("ExamEndTime"), "yyyyMMddHHmmss", null, System.Globalization.DateTimeStyles.None, out ExamEndTime);
                ExamName = ConfigManager.ReadConfig("ExamName");

                TextBoxExamName.Text = ConfigManager.IsValidData(ExamName) ? ExamName : "";
                DTPExamStart.Value = ConfigManager.IsValidData(ExamStartTime) ? ExamStartTime : DateTime.Now;
                DTPExamEnd.Value = ConfigManager.IsValidData(ExamEndTime) ? ExamEndTime : DateTime.Now;
                CheckBoxSetTopMost.Checked = !bool.TryParse(ConfigManager.ReadConfig("TopMost"), out bool tmpa) || tmpa;
                CheckBoxEnableVDM.Checked = bool.TryParse(ConfigManager.ReadConfig("FeatureVDM"), out bool tmpb) && tmpb;
                CheckBoxEnableMO.Checked = bool.TryParse(ConfigManager.ReadConfig("FeatureMO"), out bool tmpc) && tmpc;
                SelectedFont = (Font)fontConverter.ConvertFromString(ConfigManager.ReadConfig("Font"));
                SelectedFontStyle = (FontStyle)Enum.Parse(typeof(FontStyle), ConfigManager.ReadConfig("FontStyle"));
                ChangeFont(new Font(SelectedFont, SelectedFontStyle));

                if (LaunchManager.CurrentWindowsVersion < 10)
                {
                    CheckBoxEnableVDM.Enabled = false;
                    CheckBoxEnableVDM.Checked = false;
                    CheckBoxEnableVDM.Text = $"此功能在当前系统上不可用";
                }
            }
            catch
            {

            }
        }

        private void UpdateSomeControls()
        {
            //LabelPreviewFont.Font = new Font(SelectedFont, SelectedFontStyle);
            //LabelFontInfo.Text = $"当前选择的字体: {SelectedFont.Name}, {SelectedFont.Size}pt, {SelectedFontStyle}";
            LabelPreviewFont.Font = CountdownFont;
            LabelFontInfo.Text = $"当前选择的字体: {CountdownFont.Name}, {CountdownFont.Size}pt, {CountdownFont.Style}";
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

                ConfigManager.WriteConfig("ExamName", ExamName);
                ConfigManager.WriteConfig("ExamStartTime", DTPExamStart.Value.ToString("yyyyMMddHHmmss"));
                ConfigManager.WriteConfig("ExamEndTime", DTPExamEnd.Value.ToString("yyyyMMddHHmmss"));
                ConfigManager.WriteConfig("TopMost", CheckBoxSetTopMost.Checked.ToString());
                ConfigManager.WriteConfig("FeatureVDM", CheckBoxEnableVDM.Checked.ToString());
                ConfigManager.WriteConfig("FeatureMO", CheckBoxEnableMO.Checked.ToString());
                ConfigManager.WriteConfig("Font", $"{SelectedFont.Name}, {SelectedFont.Size}pt");
                ConfigManager.WriteConfig("FontStyle", $"{SelectedFont.Style}");
            }
            catch
            {
                ConfigManager.WriteConfig("Font", LaunchManager.OriginalFontString);
                ConfigManager.WriteConfig("FontStyle", "Bold");
            }
            
        }

        private void RestoreFunny()
        {
            GBoxRestart.Text = GBoxRestartTitleOriginal;
            LabelLine9.Text = LabelLine9Original;
            LabelLine10.Text = LabelLine10Original;
            ButtonRestart.Text = ButtonRestartTextOriginal;
        }

        protected virtual void OnConfigChanged()
        {
            ConfigChanged?.Invoke(this, EventArgs.Empty);
        }

        private void ButtonChooseFont_Click(object sender, EventArgs e)
        {
            FontDialog FontDialogMain = new()
            {
                AllowScriptChange = true,
                AllowVerticalFonts = false,
                Font = SelectedFont ?? CountdownFont,
                FontMustExist = true,
                MinSize = 10,
                MaxSize = 24,
                ScriptsOnly = true,
                ShowEffects = false
            };

            if (FontDialogMain.ShowDialog() == DialogResult.OK)
            {
                SelectedFont = FontDialogMain.Font;
                ChangeFont(SelectedFont);
                SettingsChanged(sender, e);
            }

            FontDialogMain.Dispose();
        }

        private void ButtonRestoreFont_Click(object sender, EventArgs e)
        {
            Font OriginalFont = (Font)fontConverter.ConvertFromString(LaunchManager.OriginalFontString);
            Font FinalFont = new(OriginalFont, FontStyle.Bold);

            ChangeFont(FinalFont);
            SettingsChanged(sender, e);
        }

        private void ChangeFont(Font NewFont)
        {
            SelectedFont = NewFont;
            SelectedFontStyle = NewFont.Style;
            LabelPreviewFont.Font = NewFont;
            LabelFontInfo.Text = $"当前选择的字体: {NewFont.Name}, {NewFont.Size}pt, {NewFont.Style}";
        }
    }
}
