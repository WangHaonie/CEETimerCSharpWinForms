using CEETimerCSharpWinForms.Controls;
using CEETimerCSharpWinForms.Dialogs;
using CEETimerCSharpWinForms.Modules;
using CEETimerCSharpWinForms.Modules.Configuration;
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
    public partial class SettingsForm : TrackableForm
    {
        private Font SelectedFont;
        private RulesManagerObject[] EditedCustomRules;
        private string[] EditedCustomTexts;

        private bool IsColorLabelsDragging;
        private bool IsSyncingTime;
        private bool HasSettingsChanged;
        private bool InvokeChangeRequired;
        private bool IsFunny;
        private bool IsFunnyClick;
        private bool ChangingCheckBox;
        private List<Label> ColorLabels;
        private List<ColorSetObject> SelectedColors;
        private readonly FontConverter fontConverter = new();
        private readonly ConfigObject AppConfig = MainForm.AppConfigPub;

        public SettingsForm()
        {
            InitializeComponent();
            CompositedStyle = true;
        }

        protected override void OnTrackableFormLoad()
        {
            InitializeExtra();
            RefreshSettings();
            ChangeWorkingStyle(WorkingArea.LastColor);
            ChangeWorkingStyle(WorkingArea.Funny, false);
            ChangeWorkingStyle(WorkingArea.ShowLeftPast, AppConfig.Display.ShowEnd);
        }

        private void InitializeExtra()
        {
            GBoxExamName.Text = $"考试名称 ({ConfigPolicy.MinExamNameLength}~{ConfigPolicy.MaxExamNameLength}字)";
            LabelPreviewColor1.Text = $"{Placeholders.PH_JULI}...{Placeholders.PH_START}...";
            LabelPreviewColor2.Text = $"{Placeholders.PH_JULI}...{Placeholders.PH_LEFT}...";
            LabelPreviewColor3.Text = $"{Placeholders.PH_JULI}...{Placeholders.PH_PAST}...";
            DtpExamStart.CustomFormat = AppLauncher.DateTimeFormat;
            DtpExamEnd.CustomFormat = AppLauncher.DateTimeFormat;
            LabelExamNameCounter.Text = $"0/{ConfigPolicy.MaxExamNameLength}";
            LabelExamNameCounter.ForeColor = Color.Red;

            SetTextBoxMax(TextBoxExamName, ConfigPolicy.MaxExamNameLength);
            BindComboData(ComboBoxNtpServers, [
                new("time.windows.com", 0),
                new("ntp.aliyun.com", 1),
                new("ntp.tencent.com", 2),
                new("time.cloudflare.com", 3)
                ]);
            BindComboData(ComboBoxShowXOnly, [
                new("天", 0),
                new("时", 1),
                new("分", 2),
                new("秒", 3)
                ]);
            BindComboData(ComboBoxPosition, [
                new("左上角", 0),
                new("左侧中央", 1),
                new("左下角", 2),
                new("上侧中央", 3),
                new("中央", 4),
                new("下侧中央", 5),
                new("右上角", 6),
                new("右侧中央", 7),
                new("右下角", 8)
                ]);

            List<ComboData> Monitors = [new("<请选择>", 0)];
            Screen[] CurrentScreens = Screen.AllScreens;
            for (int i = 0; i < CurrentScreens.Length; i++)
            {
                var CurrentScreen = CurrentScreens[i];
                Monitors.Add(new($"{i + 1} {CurrentScreen.DeviceName} ({CurrentScreen.Bounds.Width}x{CurrentScreen.Bounds.Height})", i + 1));
            }
            BindComboData(ComboBoxScreens, Monitors);

            ColorLabels = [LabelColor12, LabelColor22, LabelColor32, LabelColor42, LabelColor11, LabelColor21, LabelColor31, LabelColor41];
            foreach (var l in ColorLabels)
            {
                l.Click += ColorLabels_Click;
                l.MouseDown += ColorLabels_MouseDown;
                l.MouseMove += ColorLabels_MouseMove;
                l.MouseUp += ColorLabels_MouseUp;
            }
        }

        protected override void AdjustUI()
        {
            AlignControlsR(ButtonSave, ButtonCancel, TabControlMain);
            SetLabelAutoWrap(LabelPptsvc, GBoxPptsvc);
            SetLabelAutoWrap(LabelSyncTime, GBoxSyncTime);
            SetLabelAutoWrap(LabelLine01, GBoxColors);
            SetLabelAutoWrap(LabelRestart, GBoxRestart);
            CompactControlsX(ComboBoxShowXOnly, CheckBoxShowXOnly);
            CompactControlsX(CheckBoxRounding, ComboBoxShowXOnly, 10);
            CompactControlsX(ComboBoxScreens, LabelScreens);
            CompactControlsX(LabelChar1, ComboBoxScreens);
            CompactControlsX(ComboBoxPosition, LabelChar1);
            CompactControlsY(ButtonSyncTime, LabelSyncTime, 3);
            CompactControlsY(ButtonRestart, LabelRestart, 3);

            Adjust(() =>
            {
                AlignControlsL(ButtonRulesMan, ButtonSave, TabControlMain);
                AlignControlsX(ComboBoxShowXOnly, CheckBoxShowXOnly, -1);
                AlignControlsX(ComboBoxScreens, LabelScreens);
                AlignControlsX(ComboBoxPosition, LabelChar1);
                AlignControlsX(LabelExamNameCounter, TextBoxExamName);
                AlignControlsX(CheckBoxCustomText, CheckBoxShowPast);
                AlignControlsX(ButtonCustomText, CheckBoxCustomText);
                AlignControlsX(ComboBoxNtpServers, ButtonSyncTime, 3);
            });
        }

        private void RefreshSettings()
        {
            CheckBoxStartup.Checked = (Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true)?.GetValue(AppLauncher.AppNameEng) is string regvalue) && regvalue.Equals($"\"{AppLauncher.CurrentExecutablePath}\"", StringComparison.OrdinalIgnoreCase);
            CheckBoxTopMost.Checked = AppConfig.General.TopMost;
            TextBoxExamName.Text = AppConfig.General.ExamName;
            DtpExamStart.Value = AppConfig.General.ExamStartTime;
            DtpExamEnd.Value = AppConfig.General.ExamEndTime;
            CheckBoxMemClean.Checked = AppConfig.General.MemClean;
            CheckBoxDraggable.Checked = AppConfig.Display.Draggable;
            CheckBoxShowXOnly.Checked = AppConfig.Display.ShowXOnly;
            CheckBoxCustomText.Checked = AppConfig.Display.CustomText;
            CheckBoxRounding.Checked = AppConfig.Display.Rounding;
            CheckBoxShowEnd.Checked = DtpExamEnd.Enabled = AppConfig.Display.ShowEnd;
            CheckBoxShowPast.Checked = AppConfig.Display.ShowPast;
            CheckBoxPptSvc.Checked = AppConfig.Display.SeewoPptsvc;
            CheckBoxUniTopMost.Checked = MainForm.UniTopMost;
            ComboBoxScreens.SelectedIndex = AppConfig.Display.ScreenIndex;
            ComboBoxPosition.SelectedIndex = (int)AppConfig.Display.Position;
            ComboBoxShowXOnly.SelectedIndex = AppConfig.Display.X;
            ChangeWorkingStyle(WorkingArea.ChangeFont, NewFont: AppConfig.Appearance.Font);
            ChangePptsvcStyle(null, EventArgs.Empty);
            ComboBoxShowXOnly.SelectedIndex = AppConfig.Display.ShowXOnly ? AppConfig.Display.X : 0;
            ComboBoxNtpServers.SelectedIndex = AppConfig.Tools.NtpServer;
            EditedCustomTexts = AppConfig.Display.CustomTexts;
            EditedCustomRules = AppConfig.CustomRules;
        }

        private void SettingsChanged(object sender, EventArgs e)
        {
            Execute(() =>
            {
                HasSettingsChanged = true;
                ButtonSave.Enabled = true;
            });
        }

        private void TextBoxExamName_TextChanged(object sender, EventArgs e)
        {
            SettingsChanged(sender, e);
            int CharCount = TextBoxExamName.Text.RemoveIllegalChars().Length;
            LabelExamNameCounter.Text = $"{CharCount}/{ConfigPolicy.MaxExamNameLength}";
            LabelExamNameCounter.ForeColor = (CharCount.IsValid()) ? Color.Black : Color.Red;
        }

        private void CheckBoxShowXOnly_CheckedChanged(object sender, EventArgs e)
        {
            SettingsChanged(sender, e);
            CheckBoxRounding.Enabled = ComboBoxShowXOnly.Enabled = CheckBoxShowXOnly.Checked;
            ComboBoxShowXOnly.SelectedIndex = CheckBoxShowXOnly.Checked ? AppConfig.Display.X : 0;
            ChangeCustomTextStyle(sender);

            if (CheckBoxRounding.Checked && !CheckBoxShowXOnly.Checked)
            {
                CheckBoxRounding.Checked = false;
                CheckBoxRounding.Enabled = false;
            }
        }

        private void CheckBoxTopMost_CheckedChanged(object sender, EventArgs e)
        {
            ChangePptsvcStyle(sender, e);
            CheckBoxUniTopMost.Enabled = CheckBoxTopMost.Checked;

            if (CheckBoxUniTopMost.Checked && !CheckBoxTopMost.Checked)
            {
                CheckBoxUniTopMost.Checked = false;
                CheckBoxUniTopMost.Enabled = false;
            }
        }

        private void CheckBoxShowEnd_CheckedChanged(object sender, EventArgs e)
        {
            SettingsChanged(sender, e);
            ChangeWorkingStyle(WorkingArea.ShowLeftPast, CheckBoxShowEnd.Checked);
            DtpExamEnd.Enabled = CheckBoxShowEnd.Checked;

            Execute(() =>
            {
                if (CheckBoxShowEnd.Checked)
                {
                    MessageX.Info("由于已开启显示考试结束倒计时，请设置考试结束日期和时间。");
                    TabControlMain.SelectedTab = TabPageGeneral;
                    DtpExamEnd.Focus();
                }
            });
        }

        private void CheckBoxShowPast_CheckedChanged(object sender, EventArgs e)
        {
            SettingsChanged(sender, e);
            CheckBoxShowEnd.Checked = CheckBoxShowPast.Checked;
            CheckBoxShowEnd.Enabled = !CheckBoxShowPast.Checked;
        }

        private void CheckBoxCustomText_CheckedChanged(object sender, EventArgs e)
        {
            SettingsChanged(sender, e);
            ChangeCustomTextStyle(sender);
        }

        private void ButtonCustomText_Click(object sender, EventArgs e)
        {
            CustomTextDialog _CustomTextDialog = new() { CustomText = EditedCustomTexts };

            if (_CustomTextDialog.ShowDialog() == DialogResult.OK)
            {
                SettingsChanged(sender, e);
                EditedCustomTexts = _CustomTextDialog.CustomText;
            }

            _CustomTextDialog.Dispose();
        }

        private void ButtonFont_Click(object sender, EventArgs e)
        {
            FontDialog FontDialogMain = new()
            {
                AllowScriptChange = true,
                AllowVerticalFonts = false,
                Font = AppConfig.Appearance.Font,
                FontMustExist = true,
                MinSize = ConfigPolicy.MinFontSize,
                MaxSize = ConfigPolicy.MaxFontSize,
                ScriptsOnly = true,
                ShowEffects = false
            };

            if (FontDialogMain.ShowDialog() == DialogResult.OK)
            {
                SettingsChanged(sender, e);
                ChangeWorkingStyle(WorkingArea.ChangeFont, NewFont: FontDialogMain.Font);
            }

            FontDialogMain.Dispose();
        }

        private void ButtonDefaultFont_Click(object sender, EventArgs e)
        {
            ChangeWorkingStyle(WorkingArea.ChangeFont, NewFont: new((Font)fontConverter.ConvertFromString(ConfigPolicy.DefaultFont), FontStyle.Bold));
            SettingsChanged(sender, e);
        }

        private void ColorLabels_Click(object sender, EventArgs e)
        {
            var LabelSender = (Label)sender;
            var ColorDialogMain = new ColorDialogEx();

            if (ColorDialogMain.ShowDialog(LabelSender.BackColor) == DialogResult.OK)
            {
                SettingsChanged(sender, e);
                LabelSender.BackColor = ColorDialogMain.Color;
                ChangeWorkingStyle(WorkingArea.SelectedColor);
            }

            ColorDialogMain.Dispose();
        }

        private void ColorLabels_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                IsColorLabelsDragging = true;
            }
        }

        private void ColorLabels_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsColorLabelsDragging)
            {
                Cursor = Cursors.Cross;
            }
        }

        private void ColorLabels_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                IsColorLabelsDragging = false;
                Cursor = Cursors.Default;

                var LabelSender = (Label)sender;
                var ParentContainer = LabelSender.Parent;
                var CursorPosition = ParentContainer.PointToClient(Cursor.Position);
                var TargetControl = ParentContainer.GetChildAtPoint(CursorPosition);

                if (TargetControl != null && TargetControl is Label TagetLabel && ColorLabels.Contains(TagetLabel) && LabelSender != TagetLabel)
                {
                    TagetLabel.BackColor = LabelSender.BackColor;
                    SettingsChanged(sender, e);
                    ChangeWorkingStyle(WorkingArea.SelectedColor);
                }
            }
            catch { }
        }

        private void ButtonDefaultColor_Click(object sender, EventArgs e)
        {
            SettingsChanged(sender, e);
            ChangeWorkingStyle(WorkingArea.DefaultColor);
        }

        private void ButtonRulesMan_Click(object sender, EventArgs e)
        {
            RulesManager Manager = new()
            {
                CustomRules = EditedCustomRules,
                Preferences = CheckBoxCustomText.Checked ? AppConfig.Display.CustomTexts : [null, null, null],
                ShowWarning = !CheckBoxCustomText.Checked
            };

            if (Manager.ShowDialog() == DialogResult.OK)
            {
                SettingsChanged(sender, e);
                EditedCustomRules = Manager.CustomRules;
            }
        }

        private void ButtonRestart_MouseDown(object sender, MouseEventArgs e)
        {
            IsFunny = IsFunnyClick;

            switch (e.Button)
            {
                case MouseButtons.Left:
                    IsFunnyClick = false;
                    break;
                case MouseButtons.Right:
                    ChangeWorkingStyle(WorkingArea.Funny);
                    IsFunnyClick = true;
                    break;
            }
        }

        private void ButtonRestart_Click(object sender, EventArgs e)
        {
            AppLauncher.Shutdown(!IsFunny);
        }

        private async void ButtonSyncTime_Click(object sender, EventArgs e)
        {
            var server = ((ComboData)ComboBoxNtpServers.SelectedItem).Display;
            ChangeWorkingStyle(WorkingArea.SyncTime);
            await Task.Run(() => StartSyncTime(server));
            ChangeWorkingStyle(WorkingArea.SyncTime, false);
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            if (IsSyncingTime)
            {
                MessageX.Warn("无法执行此操作，请等待同步网络时钟完成！");
                return;
            }

            if (IsSettingsFormatValid())
            {
                InvokeChangeRequired = true;
                HasSettingsChanged = false;
                Close();
            }
        }

        private void CheckBoxDraggable_CheckedChanged(object sender, EventArgs e)
        {
            ChangePptsvcStyle(sender, e);
            ComboBoxScreens.SelectedValue = CheckBoxDraggable.Checked ? 0 : AppConfig.Display.ScreenIndex;
            ComboBoxPosition.SelectedValue = CheckBoxDraggable.Checked ? 0 : (int)AppConfig.Display.Position;
            LabelScreens.Enabled = LabelChar1.Enabled = ComboBoxScreens.Enabled = !CheckBoxDraggable.Checked;
        }

        private void ComboBoxes_DropDown(object sender, EventArgs e)
        {
            #region
            /*
             
            DropDown 自适应大小 参考:

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

        private void ComboBoxShowXOnly_SelectedIndexChanged(object sender, EventArgs e)
        {
            SettingsChanged(sender, e);
            CheckBoxRounding.Visible = ComboBoxShowXOnly.SelectedIndex == 0;
            CheckBoxRounding.Checked = ComboBoxShowXOnly.SelectedIndex == 0 && AppConfig.Display.Rounding;
        }

        private void ComboBoxScreens_SelectedIndexChanged(object sender, EventArgs e)
        {
            SettingsChanged(sender, e);

            ComboBoxPosition.Enabled = !CheckBoxDraggable.Checked && ComboBoxScreens.SelectedIndex != 0;
            ComboBoxPosition.SelectedIndex = ComboBoxPosition.Enabled ? (int)AppConfig.Display.Position : 0;
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        protected override void OnTrackableFormClosing(FormClosingEventArgs e)
        {
            if (IsSyncingTime)
            {
                e.Cancel = true;
            }
            else if (HasSettingsChanged)
            {
                ShowUnsavedWarning("检测到当前设置未保存，是否立即进行保存？", e, () => ButtonSave_Click(null, null), () =>
                {
                    HasSettingsChanged = false;
                    Close();
                });
            }
        }

        protected override void OnTrackableFormClosed()
        {
            if (InvokeChangeRequired)
            {
                SaveSettings();
            }

            ChangeWorkingStyle(WorkingArea.Funny, false);
            base.OnTrackableFormClosed();
        }

        private void ChangeCustomTextStyle(object sender)
        {
            if (ChangingCheckBox) return;
            ChangingCheckBox = true;
            var cb = (CheckBox)sender;

            try
            {
                if (cb == CheckBoxShowXOnly)
                {
                    CheckBoxCustomText.Enabled = !cb.Checked;
                    ButtonCustomText.Enabled = false;
                }
                else
                {
                    ButtonCustomText.Enabled = cb.Checked;
                    CheckBoxShowXOnly.Enabled = !cb.Checked;
                }
            }
            finally
            {
                ChangingCheckBox = false;
            }
        }

        private void ChangePptsvcStyle(object sender, EventArgs e)
        {
            SettingsChanged(sender, e);

            var a = CheckBoxTopMost.Checked;
            var b = ComboBoxPosition.SelectedIndex == 0;
            var c = CheckBoxDraggable.Checked;

            if (!a)
            {
                ChangeWorkingStyle(WorkingArea.SetPPTService, false);
            }
            else if ((a && c) || (a && b))
            {
                ChangeWorkingStyle(WorkingArea.SetPPTService);
            }
            else
            {
                ChangeWorkingStyle(WorkingArea.SetPPTService, false, 1);
            }
        }

        private bool IsSettingsFormatValid()
        {
            var ExamName = TextBoxExamName.Text.RemoveIllegalChars();
            TimeSpan ExamTimeSpan = DtpExamEnd.Value - DtpExamStart.Value;
            string UniMsg = "";
            string TimeMsg = "";

            if (string.IsNullOrWhiteSpace(ExamName) || !ExamName.Length.IsValid())
            {
                MessageX.Error("输入的考试名称有误！\n\n请检查输入的考试名称是否太长或太短！", TabControlMain, TabPageGeneral);
                return false;
            }

            if (DtpExamEnd.Enabled)
            {
                if (DtpExamEnd.Value <= DtpExamStart.Value)
                {
                    MessageX.Error("考试结束时间必须在开始时间之后！", TabControlMain, TabPageGeneral);
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
                    UniMsg = $"检测到设置的考试时间太长或太短！\n\n当前考试时长: {TimeMsg}。\n\n如果你确认当前设置的是正确的考试时间，请点击 是，否则请点击 否。";
                }

                if (!string.IsNullOrEmpty(UniMsg))
                {
                    var _DialogResult = MessageX.Warn(UniMsg, TabControlMain, TabPageGeneral, Buttons: MessageBoxExButtons.YesNo);

                    if (_DialogResult is DialogResult.No or DialogResult.None)
                    {
                        return false;
                    }
                }
            }

            int ColorCheckMsg = 0;
            SelectedColors = GetSelectedColors();

            for (int i = 0; i < 4; i++)
            {
                if (!ColorHelper.IsNiceContrast(SelectedColors[i].Fore, SelectedColors[i].Back))
                {
                    ColorCheckMsg = i + 1;
                    break;
                }
            }

#if DEBUG
            Console.WriteLine("##########################");
#endif

            if (ColorCheckMsg != 0)
            {
                MessageX.Error($"第{ColorCheckMsg}组的颜色相似或对比度较低，将无法看清文字。\n\n请尝试更换其它背景颜色或文字颜色！", TabControlMain, TabPageAppearance);
                return false;
            }

            return true;
        }

        private void StartSyncTime(string Server)
        {
            try
            {
                if (!AppLauncher.IsAdmin) MessageX.Warn("检测到当前用户不具有管理员权限，运行该操作会发生错误。\n\n程序将在此消息框关闭后尝试弹出 UAC 提示框，前提要把系统的 UAC 设置为 \"仅当应用尝试更改我的计算机时通知我\" 或及以上，否则将无法进行授权。\n\n稍后若没有看见提示框，请更改 UAC 设置: 开始菜单搜索 uac");

                Process SyncTimeProcess = ProcessHelper.RunProcess("cmd.exe", $"/c net stop w32time & sc config w32time start= auto & net start w32time && w32tm /config /manualpeerlist:{Server} /syncfromflags:manual /reliable:YES /update && w32tm /resync && w32tm /resync", AdminRequired: true);

                SyncTimeProcess.WaitForExit();
                var ExitCode = SyncTimeProcess.ExitCode;
                MessageX.Info($"命令执行完成！\n\n返回值为 {ExitCode} (0x{ExitCode:X})\n(0 代表成功，其他值为失败)", TabControlMain, TabPageTools);
            }
            #region 来自网络
            /*
                 
                检测用户是否点击了 UAC 提示框的 "否" 参考:

                c# - Run process as administrator from a non-admin application - Stack Overflow
                https://stackoverflow.com/a/20872219/21094697
                 
            */
            catch (Win32Exception ex) when (ex.NativeErrorCode == 1223)
            {
                MessageX.Error($"授权失败，请在 UAC 对话框弹出时点击 \"是\"。{ex.ToMessage()}", TabControlMain, TabPageTools);
            }
            #endregion
            catch (Exception ex)
            {
                MessageX.Error($"命令执行时发生了错误。{ex.ToMessage()}", TabControlMain, TabPageTools);
            }
        }

        private List<ColorSetObject> GetSelectedColors()
        {
            return [new(LabelColor11.BackColor, LabelColor12.BackColor),
                    new(LabelColor21.BackColor, LabelColor22.BackColor),
                    new(LabelColor31.BackColor, LabelColor32.BackColor),
                    new(LabelColor41.BackColor, LabelColor42.BackColor)];
        }

        private void ChangeWorkingStyle(WorkingArea Where, bool IsWorking = true, int SubCase = 0, Font NewFont = null)
        {
            switch (Where)
            {
                case WorkingArea.SyncTime:
                    IsSyncingTime = IsWorking;
                    ButtonSyncTime.Enabled = !IsWorking;
                    ComboBoxNtpServers.Enabled = !IsWorking;
                    ButtonRestart.Enabled = !IsWorking;
                    ButtonSave.Enabled = !IsWorking && HasSettingsChanged;
                    ButtonCancel.Enabled = !IsWorking;
                    ButtonSyncTime.Text = IsWorking ? "正在同步中，请稍候..." : "立即同步(&S)";
                    break;
                case WorkingArea.Funny:
                    GBoxRestart.Text = IsWorking ? "关闭倒计时" : "重启倒计时";
                    LabelRestart.Text = $"用于更改了屏幕缩放之后, 可以点击此按钮来重启程序以确保窗口的文字不会变模糊。{(IsWorking ? "(●'◡'●)" : "")}";
                    ButtonRestart.Text = IsWorking ? "点击关闭(&L)" : "点击重启(&R)";
                    break;
                case WorkingArea.SetPPTService:
                    CheckBoxPptSvc.Enabled = IsWorking;
                    CheckBoxPptSvc.Checked = IsWorking && AppConfig.Display.SeewoPptsvc;
                    CheckBoxPptSvc.Text = IsWorking ? "启用此功能(&X)" : $"此项暂不可用，因为倒计时没有{(SubCase == 0 ? "顶置" : "在左上角")}。";
                    break;
                case WorkingArea.ChangeFont:
                    SelectedFont = NewFont;
                    LabelFont.Text = $"当前字体: {NewFont.Name}, {NewFont.Style}, {NewFont.Size}pt";
                    break;
                case WorkingArea.LastColor:
                    var CountdownColors = AppConfig.Appearance.Colors;

                    LabelPreviewColor1.ForeColor = LabelColor11.BackColor = CountdownColors[0].Fore;
                    LabelPreviewColor2.ForeColor = LabelColor21.BackColor = CountdownColors[1].Fore;
                    LabelPreviewColor3.ForeColor = LabelColor31.BackColor = CountdownColors[2].Fore;
                    LabelPreviewColor4.ForeColor = LabelColor41.BackColor = CountdownColors[3].Fore;
                    LabelPreviewColor1.BackColor = LabelColor12.BackColor = CountdownColors[0].Back;
                    LabelPreviewColor2.BackColor = LabelColor22.BackColor = CountdownColors[1].Back;
                    LabelPreviewColor3.BackColor = LabelColor32.BackColor = CountdownColors[2].Back;
                    LabelPreviewColor4.BackColor = LabelColor42.BackColor = CountdownColors[3].Back;
                    break;
                case WorkingArea.SelectedColor:
                    LabelPreviewColor1.BackColor = LabelColor12.BackColor;
                    LabelPreviewColor2.BackColor = LabelColor22.BackColor;
                    LabelPreviewColor3.BackColor = LabelColor32.BackColor;
                    LabelPreviewColor4.BackColor = LabelColor42.BackColor;
                    LabelPreviewColor1.ForeColor = LabelColor11.BackColor;
                    LabelPreviewColor2.ForeColor = LabelColor21.BackColor;
                    LabelPreviewColor3.ForeColor = LabelColor31.BackColor;
                    LabelPreviewColor4.ForeColor = LabelColor41.BackColor;
                    break;
                case WorkingArea.DefaultColor:
                    var DefaultColors = new ConfigObject().Appearance.Colors;

                    LabelPreviewColor1.ForeColor = LabelColor11.BackColor = DefaultColors[0].Fore;
                    LabelPreviewColor2.ForeColor = LabelColor21.BackColor = DefaultColors[1].Fore;
                    LabelPreviewColor3.ForeColor = LabelColor31.BackColor = DefaultColors[2].Fore;
                    LabelPreviewColor4.ForeColor = LabelColor41.BackColor = DefaultColors[3].Fore;
                    LabelPreviewColor1.BackColor = LabelColor12.BackColor = DefaultColors[0].Back;
                    LabelPreviewColor2.BackColor = LabelColor22.BackColor = DefaultColors[1].Back;
                    LabelPreviewColor3.BackColor = LabelColor32.BackColor = DefaultColors[2].Back;
                    LabelPreviewColor4.BackColor = LabelColor42.BackColor = DefaultColors[3].Back;
                    break;
                case WorkingArea.ShowLeftPast:
                    GBoxExamEnd.Visible = IsWorking;
                    GBoxOthers.Location = IsWorking ?
                                          new(GBoxExamEnd.Location.X, GBoxExamEnd.Location.Y + GBoxExamEnd.Height + 6.WithDpi(this)) :
                                          new(GBoxOthers.Location.X, GBoxExamStart.Location.Y + GBoxExamStart.Height + 6.WithDpi(this));
                    break;
            }
        }

        private void SaveSettings()
        {
            try
            {
                using var reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

                if (CheckBoxStartup.Checked)
                    reg.SetValue(AppLauncher.AppNameEng, $"\"{AppLauncher.CurrentExecutablePath}\"");
                else
                    reg.DeleteValue(AppLauncher.AppNameEng, false);

                AppConfig.General = new()
                {
                    ExamName = TextBoxExamName.Text,
                    ExamStartTime = DtpExamStart.Value,
                    ExamEndTime = DtpExamEnd.Value,
                    MemClean = CheckBoxMemClean.Checked,
                    TopMost = CheckBoxTopMost.Checked,
                    UniTopMost = CheckBoxUniTopMost.Checked
                };

                AppConfig.Display = new()
                {
                    ShowXOnly = CheckBoxShowXOnly.Checked,
                    X = ComboBoxShowXOnly.SelectedIndex,
                    Rounding = CheckBoxRounding.Checked,
                    ShowEnd = CheckBoxShowEnd.Checked,
                    ShowPast = CheckBoxShowPast.Checked,
                    CustomText = CheckBoxCustomText.Checked,
                    CustomTexts = EditedCustomTexts,
                    ScreenIndex = ComboBoxScreens.SelectedIndex,
                    Position = (CountdownPosition)ComboBoxPosition.SelectedIndex,
                    Draggable = CheckBoxDraggable.Checked,
                    SeewoPptsvc = CheckBoxPptSvc.Checked,
                };

                AppConfig.Appearance = new()
                {
                    Font = SelectedFont,
                    Colors = [new(LabelColor11.BackColor,LabelColor12.BackColor),
                            new(LabelColor21.BackColor,LabelColor22.BackColor),
                            new(LabelColor31.BackColor,LabelColor32.BackColor),
                            new(LabelColor41.BackColor,LabelColor42.BackColor)],
                };

                AppConfig.Tools = new()
                {
                    NtpServer = ComboBoxNtpServers.SelectedIndex
                };

                AppConfig.CustomRules = EditedCustomRules;

                MainForm.AppConfigPub = AppConfig;
            }
            catch { }
        }
    }
}
