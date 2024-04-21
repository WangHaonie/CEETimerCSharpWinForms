using CEETimerCSharpWinForms.Modules;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace CEETimerCSharpWinForms.Forms
{
    public partial class FormMain : Form
    {
        public static bool IsUniTopMost { get; private set; }
        public static bool IsTopMost { get; private set; }

        private Color Back1;
        private Color Back2;
        private Color Back3;
        private Color Back4;
        private Color Fore1;
        private Color Fore2;
        private Color Fore3;
        private Color Fore4;
        private bool IsFeatureMOEnabled;
        private bool IsShowOnly;
        private bool IsDragable;
        private bool IsShowEnd;
        private bool IsShowPast;
        private bool IsRounding;
        private bool IsPPTService;
        private int ScreenIndex;
        private int PositionIndex;
        private int ShowOnlyIndex;
        private DateTime ExamEndTime;
        private DateTime ExamStartTime;
        private Font SelectedFont;
        private FontStyle SelectedFontStyle;
        private string ExamName;

        private enum CountdownState
        {
            Normal,
            DaysOnly,
            DaysOnlyWithRounding,
            HoursOnly,
            MinutesOnly,
            SecondsOnly
        }

        private bool IsReadyToMove;
        private bool IsReady;
        private bool DisplaySettingsChangedEventInvoked;
        private readonly int PptsvcThreshold = 1;
        private readonly FontConverter fontConverter = new();
        private CountdownState SelectedState;
        private Timer TimerCountdown;
        private System.Threading.Timer TimerMORunner;
        private Point LastLocation;
        private Point LastMouseLocation;
        private Rectangle SelectedScreen;

        public FormMain()
        {
            InitializeComponent();
            InitializeExtra();
            RefreshSettings(null, EventArgs.Empty);
            LabelCountdown.ForeColor = Fore4;
            BackColor = Back4;
        }

        private void InitializeExtra()
        {
            FormSettings.ConfigChanged += RefreshSettings;

            TimerCountdown = new Timer()
            {
                Interval = 1000
            };

            TimerCountdown.Tick += StartCountdown;
            TimerCountdown.Start();
        }

        private void RefreshSettings(object sender, EventArgs e)
        {
            ConfigManager.MountConfig(true);

            ExamName = ConfigManager.ReadConfig(ConfigItems.ExamName);
            TopMost = IsTopMost = !bool.TryParse(ConfigManager.ReadConfig(ConfigItems.TopMost), out bool tmpa) || tmpa;
            IsFeatureMOEnabled = bool.TryParse(ConfigManager.ReadConfig(ConfigItems.MemOpti), out bool tmpc) && tmpc;
            IsShowOnly = bool.TryParse(ConfigManager.ReadConfig(ConfigItems.ShowOnly), out bool tmpd) && tmpd;
            IsRounding = bool.TryParse(ConfigManager.ReadConfig(ConfigItems.Rounding), out bool tmpe) && tmpe;
            IsShowPast = bool.TryParse(ConfigManager.ReadConfig(ConfigItems.ShowPast), out bool tmpg) && tmpg;
            IsShowEnd = bool.TryParse(ConfigManager.ReadConfig(ConfigItems.ShowEnd), out bool tmpf) && tmpf;
            IsDragable = bool.TryParse(ConfigManager.ReadConfig(ConfigItems.Dragable), out bool tmph) && tmph;
            IsUniTopMost = bool.TryParse(ConfigManager.ReadConfig(ConfigItems.UniTopMost), out bool tmpi) && tmpi;
            IsPPTService = bool.TryParse(ConfigManager.ReadConfig(ConfigItems.SeewoPptSvc), out bool tmpj) && tmpj;
            ScreenIndex = int.TryParse(ConfigManager.ReadConfig(ConfigItems.Screen), out int tmpk) ? tmpk : 0;
            PositionIndex = int.TryParse(ConfigManager.ReadConfig(ConfigItems.Position), out int tmpu) ? tmpu : 0;
            ShowOnlyIndex = int.TryParse(ConfigManager.ReadConfig(ConfigItems.ShowValue), out int tmpl) ? tmpl : 0;
            Back1 = ColorHelper.TryParseRGB(ConfigManager.ReadConfig(ConfigItems.Back1), out Color tmpm) ? tmpm : Color.White;
            Back2 = ColorHelper.TryParseRGB(ConfigManager.ReadConfig(ConfigItems.Back2), out Color tmpn) ? tmpn : Color.White;
            Back3 = ColorHelper.TryParseRGB(ConfigManager.ReadConfig(ConfigItems.Back3), out Color tmpo) ? tmpo : Color.White;
            Back4 = ColorHelper.TryParseRGB(ConfigManager.ReadConfig(ConfigItems.Back4), out Color tmpp) ? tmpp : Color.White;
            Fore1 = ColorHelper.TryParseRGB(ConfigManager.ReadConfig(ConfigItems.Fore1), out Color tmpq) ? tmpq : Color.Red;
            Fore2 = ColorHelper.TryParseRGB(ConfigManager.ReadConfig(ConfigItems.Fore2), out Color tmpr) ? tmpr : Color.Green;
            Fore3 = ColorHelper.TryParseRGB(ConfigManager.ReadConfig(ConfigItems.Fore3), out Color tmps) ? tmps : Color.Black;
            Fore4 = ColorHelper.TryParseRGB(ConfigManager.ReadConfig(ConfigItems.Fore4), out Color tmpt) ? tmpt : Color.Black;
            DateTime.TryParseExact(ConfigManager.ReadConfig(ConfigItems.StartTime), "yyyyMMddHHmmss", null, DateTimeStyles.None, out ExamStartTime);
            DateTime.TryParseExact(ConfigManager.ReadConfig(ConfigItems.EndTime), "yyyyMMddHHmmss", null, DateTimeStyles.None, out ExamEndTime);
            int.TryParse(ConfigManager.ReadConfig(ConfigItems.PosX), out int x);
            int.TryParse(ConfigManager.ReadConfig(ConfigItems.PosY), out int y);

            ShowInTaskbar = !TopMost;
            IsShowPast = IsShowPast && IsShowEnd;
            IsRounding = IsRounding && IsShowOnly && ShowOnlyIndex == 0;
            IsUniTopMost = IsUniTopMost && TopMost;
            if (ScreenIndex < 0 || ScreenIndex > Screen.AllScreens.Length) ScreenIndex = 0;
            if (PositionIndex < 0 || PositionIndex > 8) PositionIndex = 0;
            if (ShowOnlyIndex > 3) ShowOnlyIndex = 0;
            if (ExamName.Length > ConfigPolicy.MaxExamNameLength || ExamName.Length < ConfigPolicy.MinExamNameLength) ExamName = "";
            IsReady = !string.IsNullOrWhiteSpace(ExamName) && ConfigManager.IsValidData(ExamStartTime) && ConfigManager.IsValidData(ExamEndTime) && (ExamEndTime > ExamStartTime || !IsShowEnd);
            IsPPTService = IsPPTService && ((TopMost && ShowOnlyIndex == 0) || IsDragable);

            DetermineCountdownState();
            SystemEvents.DisplaySettingsChanged += SystemEvents_DisplaySettingsChanged;

            if (!ColorHelper.IsNiceContrast(Fore1, Back1))
            {
                Fore1 = Color.Red;
                Back1 = Color.White;
            }
            if (!ColorHelper.IsNiceContrast(Fore2, Back2))
            {
                Fore2 = Color.Green;
                Back2 = Color.White;
            }
            if (!ColorHelper.IsNiceContrast(Fore3, Back3))
            {
                Fore3 = Color.Black;
                Back3 = Color.White;
            }
            if (!ColorHelper.IsNiceContrast(Fore4, Back4))
            {
                Fore4 = Color.Black;
                Back4 = Color.White;
            }

#if DEBUG
            Console.WriteLine("##########################");
#endif

            try
            {
                SelectedFont = (Font)fontConverter.ConvertFromString(ConfigManager.ReadConfig(ConfigItems.Font));
                SelectedFontStyle = (FontStyle)Enum.Parse(typeof(FontStyle), ConfigManager.ReadConfig(ConfigItems.FontStyle));

                if (SelectedFont.Size > ConfigPolicy.MaxFontSize || SelectedFont.Size < ConfigPolicy.MinFontSize)
                {
                    throw new Exception();
                }
            }
            catch
            {
                SelectedFont = (Font)fontConverter.ConvertFromString(ConfigPolicy.DefaultFont);
                SelectedFontStyle = FontStyle.Bold;
            }

            LabelCountdown.Font = new Font(SelectedFont, SelectedFontStyle);

            ConfigManager.MountConfig(false);

            LabelCountdown.MouseDown -= LabelCountdown_MouseDown;
            LabelCountdown.MouseMove -= LabelCountdown_MouseMove;
            LabelCountdown.MouseUp -= LabelCountdown_MouseUp;

            if (IsDragable)
            {
                LabelCountdown.MouseDown += LabelCountdown_MouseDown;
                LabelCountdown.MouseMove += LabelCountdown_MouseMove;
                LabelCountdown.MouseUp += LabelCountdown_MouseUp;
                Location = new Point(x, y);
            }
            else
            {
                RefreshScreen();
            }

            ApplyLocation();
            CompatibleWithPPTService();

            var OpeningForms = Application.OpenForms.Cast<Form>().ToList();
            foreach (Form form in OpeningForms)
            {
                if (form == this) continue;
                form.TopMost = IsUniTopMost;
            }

            TimerMORunner?.Dispose();

            if (IsFeatureMOEnabled)
                TimerMORunner = new System.Threading.Timer(MemoryManager.OptimizeMemory, null, TimeSpan.Zero, TimeSpan.FromMinutes(5));

            FormSettings.FeatureMOEnabled = IsFeatureMOEnabled;
            FormSettings.TopMostChecked = TopMost;
            FormSettings.ExamStartTime = ExamStartTime;
            FormSettings.ExamEndTime = ExamEndTime;
            FormSettings.CountdownFont = LabelCountdown.Font;
            FormSettings.CountdownFontStyle = LabelCountdown.Font.Style;
            FormSettings.ExamName = ExamName;
            FormSettings.IsShowOnly = IsShowOnly;
            FormSettings.ShowOnlyIndex = ShowOnlyIndex;
            FormSettings.IsShowEnd = IsShowEnd;
            FormSettings.IsShowPast = IsShowPast;
            FormSettings.IsRounding = IsRounding;
            FormSettings.IsDragable = IsDragable;
            FormSettings.IsPPTService = IsPPTService;
            FormSettings.ScreenIndex = ScreenIndex;
            FormSettings.PositionIndex = PositionIndex;
            FormSettings.Back1 = Back1;
            FormSettings.Back2 = Back2;
            FormSettings.Back3 = Back3;
            FormSettings.Back4 = Back4;
            FormSettings.Fore1 = Fore1;
            FormSettings.Fore2 = Fore2;
            FormSettings.Fore3 = Fore3;
            FormSettings.Fore4 = Fore4;
        }

        private void SystemEvents_DisplaySettingsChanged(object sender, EventArgs e)
        {
            RefreshScreen();

            if (!DisplaySettingsChangedEventInvoked)
            {
                DisplaySettingsChangedEventInvoked = true;
                if (MessageX.Popup("检测到显示设置发生了更改，如果你刚刚更改的是缩放，推荐重启倒计时以确保文字不会变模糊、功能不会出现异常。\n\n是否立即重启倒计时？\n(也可以在 设置 >工具 里手动重启倒计时)", MessageLevel.Warning, Buttons: MessageBoxButtons.YesNo) == DialogResult.Yes) LaunchManager.Restart();
                DisplaySettingsChangedEventInvoked = false;
            }
        }

        private void RefreshScreen()
        {
            SelectedScreen = Screen.AllScreens[ScreenIndex - 1 == -1 ? 0 : ScreenIndex - 1].WorkingArea;
        }

        #region 来自网络
        /*
        
        无边框窗口的拖动 参考：

        C#创建无边框可拖动窗口 - 掘金
        https://juejin.cn/post/6989144829607280648

         */
        private void LabelCountdown_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                IsReadyToMove = true;
                Cursor = Cursors.SizeAll;
                LastMouseLocation = e.Location;
                LastLocation = Location;
            }
        }

        private void LabelCountdown_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsReadyToMove)
            {
                Location = new Point(MousePosition.X - LastMouseLocation.X, MousePosition.Y - LastMouseLocation.Y);
            }
        }

        private void LabelCountdown_MouseUp(object sender, MouseEventArgs e)
        {
            IsReadyToMove = false;
            Cursor = Cursors.Default;

            if (LastLocation != Location)
            {
                KeepOnScreen();
                CompatibleWithPPTService();
                SaveLocation(Location);
            }
        }
        #endregion

        private void ContextMenuSettings_Click(object sender, EventArgs e)
        {
            SingleInstanceRunner<FormSettings>.GetInstance().Show();
        }

        private void ContextMenuAbout_Click(object sender, EventArgs e)
        {
            SingleInstanceRunner<FormAbout>.GetInstance().Show();
        }

        private void ContextMenuOpenDir_Click(object sender, EventArgs e)
        {
            Process.Start(LaunchManager.CurrentExecutablePath);
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.WindowsShutDown)
            {
                Environment.Exit(0);
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void DetermineCountdownState()
        {
            SelectedState = CountdownState.Normal;

            if (IsShowOnly)
            {
                SelectedState = ShowOnlyIndex switch
                {
                    0 => IsRounding ? CountdownState.DaysOnlyWithRounding : CountdownState.DaysOnly,
                    1 => CountdownState.HoursOnly,
                    2 => CountdownState.MinutesOnly,
                    3 => CountdownState.SecondsOnly,
                    _ => CountdownState.Normal
                };
            }
        }

        private void StartCountdown(object sender, EventArgs e)
        {
            if (IsReady && DateTime.Now < ExamStartTime)
            {
                TimeSpan TimeLeft = ExamStartTime - DateTime.Now;
                BackColor = Back1;
                LabelCountdown.ForeColor = Fore1;

                LabelCountdown.Text = SelectedState switch
                {
                    CountdownState.Normal => $"距离{ExamName}还有{TimeLeft.Days}天{TimeLeft.Hours:00}时{TimeLeft.Minutes:00}分{TimeLeft.Seconds:00}秒",
                    CountdownState.DaysOnly => $"距离{ExamName}还有{TimeLeft.Days}天",
                    CountdownState.DaysOnlyWithRounding => $"距离{ExamName}还有{TimeLeft.Days + 1}天",
                    CountdownState.HoursOnly => $"距离{ExamName}还有{TimeLeft.TotalHours:0}小时",
                    CountdownState.MinutesOnly => $"距离{ExamName}还有{TimeLeft.TotalMinutes:0}分钟",
                    CountdownState.SecondsOnly => $"距离{ExamName}还有{TimeLeft.TotalSeconds:0}秒",
                    _ => ""
                };
            }
            else if (IsReady && IsShowEnd && DateTime.Now >= ExamStartTime && DateTime.Now < ExamEndTime)
            {
                TimeSpan TimeLeftPast = ExamEndTime - DateTime.Now;
                BackColor = Back2;
                LabelCountdown.ForeColor = Fore2;

                LabelCountdown.Text = SelectedState switch
                {
                    CountdownState.Normal => $"距离{ExamName}结束还有{TimeLeftPast.Days}天{TimeLeftPast.Hours:00}时{TimeLeftPast.Minutes:00}分{TimeLeftPast.Seconds:00}秒",
                    CountdownState.DaysOnly => $"距离{ExamName}结束还有{TimeLeftPast.Days}天",
                    CountdownState.DaysOnlyWithRounding => $"距离{ExamName}结束还有{TimeLeftPast.Days + 1}天",
                    CountdownState.HoursOnly => $"距离{ExamName}结束还有{TimeLeftPast.TotalHours:0}小时",
                    CountdownState.MinutesOnly => $"距离{ExamName}结束还有{TimeLeftPast.TotalMinutes:0}分钟",
                    CountdownState.SecondsOnly => $"距离{ExamName}结束还有{TimeLeftPast.TotalSeconds:0}秒",
                    _ => ""
                };
            }
            else if (IsReady && IsShowEnd && DateTime.Now >= ExamEndTime && IsShowPast)
            {
                TimeSpan TimePast = DateTime.Now - ExamEndTime;
                BackColor = Back3;
                LabelCountdown.ForeColor = Fore3;

                LabelCountdown.Text = SelectedState switch
                {
                    CountdownState.Normal => $"距离{ExamName}已过去了{TimePast.Days}天{TimePast.Hours:00}时{TimePast.Minutes:00}分{TimePast.Seconds:00}秒",
                    CountdownState.DaysOnly => $"距离{ExamName}已过去了{TimePast.Days}天",
                    CountdownState.DaysOnlyWithRounding => $"距离{ExamName}已过去了{TimePast.Days + 1}天",
                    CountdownState.HoursOnly => $"距离{ExamName}已过去了{TimePast.TotalHours:0}小时",
                    CountdownState.MinutesOnly => $"距离{ExamName}已过去了{TimePast.TotalMinutes:0}分钟",
                    CountdownState.SecondsOnly => $"距离{ExamName}已过去了{TimePast.TotalSeconds:0}秒",
                    _ => ""
                };
            }
            else
            {
                BackColor = Back4;
                LabelCountdown.ForeColor = Fore4;
                LabelCountdown.Text = "欢迎使用高考倒计时, 请右键点击此处到设置里添加考试信息";
            }

            if (!IsReadyToMove)
            {
                ApplyLocation();
                KeepOnScreen();
            }
        }

        private void ApplyLocation()
        {
            if (!IsDragable)
            {
                Location = PositionIndex switch
                {
                    0 => IsPPTService ? new Point(SelectedScreen.Location.X + PptsvcThreshold, SelectedScreen.Location.Y) : SelectedScreen.Location,
                    1 => new Point(SelectedScreen.Left, SelectedScreen.Top + SelectedScreen.Height / 2 - Height / 2),
                    2 => new Point(SelectedScreen.Left, SelectedScreen.Bottom - Height),
                    3 => new Point(SelectedScreen.Left + SelectedScreen.Width / 2 - Width / 2, SelectedScreen.Top),
                    4 => new Point(SelectedScreen.Left + SelectedScreen.Width / 2 - Width / 2, SelectedScreen.Top + SelectedScreen.Height / 2 - Height / 2),
                    5 => new Point(SelectedScreen.Left + SelectedScreen.Width / 2 - Width / 2, SelectedScreen.Bottom - Height),
                    6 => new Point(SelectedScreen.Right - Width, SelectedScreen.Top),
                    7 => new Point(SelectedScreen.Right - Width, SelectedScreen.Top + SelectedScreen.Height / 2 - Height / 2),
                    8 => new Point(SelectedScreen.Right - Width, SelectedScreen.Bottom - Height),
                    _ => Location
                };
            }
        }

        private void CompatibleWithPPTService()
        {
            if (IsPPTService && TopMost)
            {
                var ValidArea = Screen.GetWorkingArea(this);
                if (Left == ValidArea.Left && Top == ValidArea.Top)
                {
                    Left = ValidArea.Left + PptsvcThreshold;
                }
            }
        }

        private void KeepOnScreen()
        {
            var ValidArea = Screen.GetWorkingArea(this);

            if (Left < ValidArea.Left) Left = ValidArea.Left;
            if (Top < ValidArea.Top) Top = ValidArea.Top;
            if (Right > ValidArea.Right) Left = ValidArea.Right - Width;
            if (Bottom > ValidArea.Bottom) Top = ValidArea.Bottom - Height;
        }

        private void SaveLocation(Point NewLocation)
        {
            ConfigManager.WriteConfig(new Dictionary<string, string>
            {
                { ConfigItems.PosX, $"{NewLocation.X}" },
                { ConfigItems.PosY, $"{NewLocation.Y}" }
            });
        }
    }
}