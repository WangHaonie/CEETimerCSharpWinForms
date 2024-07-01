using CEETimerCSharpWinForms.Modules;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CEETimerCSharpWinForms.Forms
{
    public partial class MainForm : Form
    {
        public static bool UniTopMost { get; private set; } = true;

        private bool IsMemoryOptimizationEnabled;
        private bool IsShowXOnly;
        private bool IsDraggable;
        private bool IsShowEnd;
        private bool IsShowPast;
        private bool IsRounding;
        private bool IsPPTService;
        private bool IsCustomText;
        private int ScreenIndex;
        private int PositionIndex;
        private int ShowXOnlyIndex;
        private DateTime ExamEndTime;
        private DateTime ExamStartTime;
        private Font SelectedFont;
        private FontStyle SelectedFontStyle;
        private List<TupleEx<Color, Color>> CountdownColors;
        private List<TupleEx<Color, Color>> DefaultColors;
        private List<TupleEx<TupleEx<int, TimeSpan>, TupleEx<Color, Color, string>>> CustomRules;
        private string ExamName;
        private string[] CustomText;

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
        private bool IsCountdownReady;
        private bool IsWin10BelowRounded;
        private readonly int PptsvcThreshold = 1;
        private readonly int BorderRadius = 13;
        private CountdownState SelectedState;
        private Timer TimerCountdown;
        private System.Threading.Timer MemoryOptimizer;
        private Point LastLocation;
        private Point LastMouseLocation;
        private Rectangle SelectedScreen;
        private SettingsForm _SettingsForm;
        private AboutForm _AboutForm;
        private readonly ConfigManager configManager = new();
        private readonly FontConverter fontConverter = new();

        public MainForm()
        {
            InitializeComponent();
            SizeChanged += MainForm_SizeChanged;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            DefaultColors = [new(Color.Red, Color.White), new(Color.Green, Color.White), new(Color.Black, Color.White), new(Color.Black, Color.White)];
            SystemEvents.DisplaySettingsChanged += SystemEvents_DisplaySettingsChanged;
            RefreshSettings(sender, e);
            TimerCountdown = new() { Interval = 1000 };
            TimerCountdown.Tick += StartCountdown;
            TimerCountdown.Start();
            LabelCountdown.ForeColor = CountdownColors[3].Item1;
            BackColor = CountdownColors[3].Item2;
            Task.Run(() => UpdateChecker.CheckUpdate(true, this));
            _ = 1.WithDpi(this);
        }

        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            if (IsWin10BelowRounded)
            {
                var _BorderRadius = BorderRadius.WithDpi(this);
                Region = Region.FromHrgn(WindowsAPI.CreateRoundRectRgn(0, 0, Width, Height, _BorderRadius, _BorderRadius));
            }
        }

        private void RefreshSettings(object sender, EventArgs e)
        {
            configManager.MountConfig(true);

            ExamName = configManager.ReadConfig(ConfigItems.KExamName);
            CustomText = CustomRuleHelper.GetCustomTextFormRaw(configManager.ReadConfig(ConfigItems.KCustomTextP1), configManager.ReadConfig(ConfigItems.KCustomTextP2), configManager.ReadConfig(ConfigItems.KCustomTextP3));
            ExamStartTime = DateTime.TryParseExact(configManager.ReadConfig(ConfigItems.KStartTime), "yyyyMMddHHmmss", null, DateTimeStyles.None, out DateTime tmpw) ? tmpw : DateTime.Now;
            ExamEndTime = DateTime.TryParseExact(configManager.ReadConfig(ConfigItems.KEndTime), "yyyyMMddHHmmss", null, DateTimeStyles.None, out DateTime tmpx) ? tmpx : DateTime.Now;
            IsMemoryOptimizationEnabled = bool.TryParse(configManager.ReadConfig(ConfigItems.KMemOpti), out bool tmpc) && tmpc;
            TopMost = !bool.TryParse(configManager.ReadConfig(ConfigItems.KTopMost), out bool tmpa) || tmpa;
            IsShowXOnly = bool.TryParse(configManager.ReadConfig(ConfigItems.KShowXOnly), out bool tmpd) && tmpd;
            IsRounding = bool.TryParse(configManager.ReadConfig(ConfigItems.KRounding), out bool tmpe) && tmpe;
            IsShowPast = bool.TryParse(configManager.ReadConfig(ConfigItems.KShowPast), out bool tmpg) && tmpg;
            IsShowEnd = bool.TryParse(configManager.ReadConfig(ConfigItems.KShowEnd), out bool tmpf) && tmpf;
            IsDraggable = bool.TryParse(configManager.ReadConfig(ConfigItems.KDraggable), out bool tmph) && tmph;
            UniTopMost = bool.TryParse(configManager.ReadConfig(ConfigItems.KUniTopMost), out bool tmpi) && tmpi;
            IsPPTService = bool.TryParse(configManager.ReadConfig(ConfigItems.KSeewoPptSvc), out bool tmpj) && tmpj;
            IsCustomText = bool.TryParse(configManager.ReadConfig(ConfigItems.KIsCustomText), out bool tmpo) && tmpo;
            ScreenIndex = int.TryParse(configManager.ReadConfig(ConfigItems.KScreen), out int tmpk) ? tmpk : 0;
            PositionIndex = int.TryParse(configManager.ReadConfig(ConfigItems.KPosition), out int tmpu) ? tmpu : 0;
            ShowXOnlyIndex = int.TryParse(configManager.ReadConfig(ConfigItems.KShowValue), out int tmpl) ? tmpl : 0;
            try { CustomRules = CustomRuleHelper.GetObject(configManager.ReadConfigEx(ConfigItems.KCustomRules)); } catch { CustomRules = []; }
            ColorDialogHelper.CustomColorCollection = ColorHelper.GetArgbArray(configManager.ReadConfig(ConfigItems.KCustomColors));
            int.TryParse(configManager.ReadConfig(ConfigItems.KPosX), out int x);
            int.TryParse(configManager.ReadConfig(ConfigItems.KPosY), out int y);
            CountdownColors = [];

            for (int i = 0; i < 4; i++)
            {
                var Fore = ColorHelper.TryParseRGB(configManager.ReadConfig($"Fore{i + 1}"), out Color tmpfore) ? tmpfore : DefaultColors[i].Item1;
                var Back = ColorHelper.TryParseRGB(configManager.ReadConfig($"Back{i + 1}"), out Color tmpback) ? tmpback : DefaultColors[i].Item2;

                if (!ColorHelper.IsNiceContrast(Fore, Back))
                {
                    Fore = DefaultColors[i].Item1;
                    Back = DefaultColors[i].Item2;
                }

                CountdownColors.Add(new(Fore, Back));
            }

#if DEBUG
            Console.WriteLine("##########################");
#endif

            ShowInTaskbar = !TopMost;
            IsShowPast = IsShowPast && IsShowEnd;
            IsRounding = IsRounding && IsShowXOnly && ShowXOnlyIndex == 0;
            IsCustomText = IsCustomText && !IsShowXOnly;
            UniTopMost = UniTopMost && TopMost;
            if (ScreenIndex < 0 || ScreenIndex > Screen.AllScreens.Length) ScreenIndex = 0;
            if (PositionIndex is < 0 or > 8) PositionIndex = 0;
            if (ShowXOnlyIndex > 3) ShowXOnlyIndex = 0;
            if (ExamName.Length is > ConfigPolicy.MaxExamNameLength or < ConfigPolicy.MinExamNameLength) ExamName = "";
            IsCountdownReady = !string.IsNullOrWhiteSpace(ExamName) && configManager.IsValidData(tmpw) && configManager.IsValidData(tmpx) && (tmpx > tmpw || !IsShowEnd);
            IsPPTService = IsPPTService && ((TopMost && ShowXOnlyIndex == 0) || IsDraggable);

            SelectedState = CountdownState.Normal;

            if (IsShowXOnly)
            {
                SelectedState = ShowXOnlyIndex switch
                {
                    0 => IsRounding ? CountdownState.DaysOnlyWithRounding : CountdownState.DaysOnly,
                    1 => CountdownState.HoursOnly,
                    2 => CountdownState.MinutesOnly,
                    3 => CountdownState.SecondsOnly,
                    _ => ConfigPolicy.NotAllowed<CountdownState>()
                };
            }

            try
            {
                SelectedFont = (Font)fontConverter.ConvertFromString(configManager.ReadConfig(ConfigItems.KFont));
                SelectedFontStyle = (FontStyle)Enum.Parse(typeof(FontStyle), configManager.ReadConfig(ConfigItems.KFontStyle));

                if (SelectedFont.Size is > ConfigPolicy.MaxFontSize or < ConfigPolicy.MinFontSize)
                {
                    ConfigPolicy.NotAllowed<Font>();
                }
            }
            catch
            {
                SelectedFont = (Font)fontConverter.ConvertFromString(ConfigPolicy.DefaultFont);
                SelectedFontStyle = FontStyle.Bold;
            }

            LabelCountdown.Font = new(SelectedFont, SelectedFontStyle);

            LabelCountdown.MouseDown -= LabelCountdown_MouseDown;
            LabelCountdown.MouseMove -= LabelCountdown_MouseMove;
            LabelCountdown.MouseUp -= LabelCountdown_MouseUp;

            if (IsDraggable)
            {
                LabelCountdown.MouseDown += LabelCountdown_MouseDown;
                LabelCountdown.MouseMove += LabelCountdown_MouseMove;
                LabelCountdown.MouseUp += LabelCountdown_MouseUp;
                Location = new(x, y);
            }
            else
            {
                RefreshScreen();
            }

            ApplyLocation();
            CompatibleWithPPTService();

            foreach (var form in UIHelper.GetOpenForms())
            {
                if (form == this) continue;
                form.TopMost = UniTopMost;
            }

            MemoryOptimizer?.Dispose();

            if (IsMemoryOptimizationEnabled)
                MemoryOptimizer = new(OptimizeMemory, null, TimeSpan.Zero, TimeSpan.FromMinutes(5));

            configManager.MountConfig(false);
            SetLabelCountdownAutoWrap();
        }

        private void SystemEvents_DisplaySettingsChanged(object sender, EventArgs e)
        {
            RefreshScreen();
            SetLabelCountdownAutoWrap();
        }

        private void SetLabelCountdownAutoWrap()
        {
            UIHelper.SetLabelAutoWrap(LabelCountdown, true);
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
                Location = new(MousePosition.X - LastMouseLocation.X, MousePosition.Y - LastMouseLocation.Y);
            }
        }

        private void LabelCountdown_MouseUp(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Default;

            if (LastLocation != Location && IsReadyToMove)
            {
                KeepOnScreen();
                CompatibleWithPPTService();
                SetLabelCountdownAutoWrap();
                SaveLocation();
            }

            IsReadyToMove = false;
        }
        #endregion

        private void ContextSettings_Click(object sender, EventArgs e)
        {
            if (_SettingsForm == null || _SettingsForm.IsDisposed)
            {
                _SettingsForm = new()
                {
                    IsMemoryOptimizationEnabled = IsMemoryOptimizationEnabled,
                    IsTopMost = TopMost,
                    ExamStartTime = ExamStartTime,
                    ExamEndTime = ExamEndTime,
                    CountdownFont = LabelCountdown.Font,
                    CountdownFontStyle = LabelCountdown.Font.Style,
                    ExamName = ExamName,
                    CustomTextRaw = CustomText,
                    IsUserCustom = IsCustomText,
                    IsShowXOnly = IsShowXOnly,
                    ShowXOnlyIndex = ShowXOnlyIndex,
                    IsShowEnd = IsShowEnd,
                    IsShowPast = IsShowPast,
                    IsRounding = IsRounding,
                    IsDraggable = IsDraggable,
                    IsPPTService = IsPPTService,
                    ScreenIndex = ScreenIndex,
                    PositionIndex = PositionIndex,
                    DefaultColors = DefaultColors,
                    CountdownColors = CountdownColors,
                    UserCustomRules = CustomRules
                };

                _SettingsForm.ConfigChanged += RefreshSettings;
            }

            _SettingsForm.ReActivate();
        }

        private void ContextAbout_Click(object sender, EventArgs e)
        {
            if (_AboutForm == null || _AboutForm.IsDisposed)
            {
                _AboutForm = new();
            }

            _AboutForm.ReActivate();
        }

        private void ContextOpenDir_Click(object sender, EventArgs e)
        {
            LaunchManager.OpenDir();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = e.CloseReason != CloseReason.WindowsShutDown;
        }

        private void StartCountdown(object sender, EventArgs e)
        {
            if (IsCountdownReady && DateTime.Now < ExamStartTime)
            {
                ApplyColorRule(0, ExamStartTime - DateTime.Now, ExamName, Placeholders.PH_START);
            }
            else if (IsCountdownReady && DateTime.Now < ExamEndTime && IsShowEnd)
            {
                ApplyColorRule(1, ExamEndTime - DateTime.Now, ExamName, Placeholders.PH_LEFT);
            }
            else if (IsCountdownReady && DateTime.Now > ExamEndTime && IsShowEnd && IsShowPast)
            {
                ApplyColorRule(2, DateTime.Now - ExamEndTime, ExamName, Placeholders.PH_PAST);
            }
            else
            {
                LabelCountdown.ForeColor = CountdownColors[3].Item1;
                BackColor = CountdownColors[3].Item2;
                LabelCountdown.Text = "欢迎使用高考倒计时";
            }

            if (!IsReadyToMove)
            {
                ApplyLocation();
                KeepOnScreen();
            }
        }

        private void ApplyColorRule(int Phase, TimeSpan Span, string ExamName, string Hint)
        {
            var r = CustomRules.Where(i => i.Item1.Item1 == Phase).Select(x => new { Tick = x.Item1.Item2, Fore = x.Item2.Item1, Back = x.Item2.Item2, Custom = x.Item2.Item3 });
            var R = Phase == 2 ? r.OrderByDescending(x => x.Tick) : r.OrderBy(x => x.Tick);
            var Rules = R.ToList();

            if (Rules.Count > 0)
            {
                foreach (var Rule in Rules)
                {
                    if (Phase == 2 ? (Span >= Rule.Tick) : (Span <= Rule.Tick + new TimeSpan(0, 0, 0, 1)))
                    {
                        SetCountdown(Span, ExamName, Hint, Rule.Fore, Rule.Back, Rule.Custom);
                        return;
                    }
                }
            }

            SetCountdown(Span, ExamName, Hint, CountdownColors[Phase].Item1, CountdownColors[Phase].Item2, CustomText[Phase]);
        }

        private void ApplyLocation()
        {
            if (!IsDraggable)
            {
                Location = PositionIndex switch
                {
                    0 => IsPPTService ? new(SelectedScreen.Location.X + PptsvcThreshold, SelectedScreen.Location.Y) : SelectedScreen.Location,
                    1 => new(SelectedScreen.Left, SelectedScreen.Top + SelectedScreen.Height / 2 - Height / 2),
                    2 => new(SelectedScreen.Left, SelectedScreen.Bottom - Height),
                    3 => new(SelectedScreen.Left + SelectedScreen.Width / 2 - Width / 2, SelectedScreen.Top),
                    4 => new(SelectedScreen.Left + SelectedScreen.Width / 2 - Width / 2, SelectedScreen.Top + SelectedScreen.Height / 2 - Height / 2),
                    5 => new(SelectedScreen.Left + SelectedScreen.Width / 2 - Width / 2, SelectedScreen.Bottom - Height),
                    6 => new(SelectedScreen.Right - Width, SelectedScreen.Top),
                    7 => new(SelectedScreen.Right - Width, SelectedScreen.Top + SelectedScreen.Height / 2 - Height / 2),
                    8 => new(SelectedScreen.Right - Width, SelectedScreen.Bottom - Height),
                    _ => ConfigPolicy.NotAllowed<Point>()
                };
            }
        }

        private void SetCountdown(TimeSpan Span, string ExamName, string Hint, Color Fore, Color Back, string Custom)
        {
            LabelCountdown.ForeColor = Fore;
            BackColor = Back;

            if (IsCustomText)
            {
                SetCustomText(Custom, Span);
            }
            else
            {
                LabelCountdown.Text = SelectedState switch
                {
                    CountdownState.Normal => $"{Placeholders.PH_JULI}{ExamName}{Hint}{Span.Days}天{Span.Hours:00}时{Span.Minutes:00}分{Span.Seconds:00}秒",
                    CountdownState.DaysOnly => $"{Placeholders.PH_JULI}{ExamName}{Hint}{Span.Days}天",
                    CountdownState.DaysOnlyWithRounding => $"{Placeholders.PH_JULI}{ExamName}{Hint}{Span.Days + 1}天",
                    CountdownState.HoursOnly => $"{Placeholders.PH_JULI}{ExamName}{Hint}{Span.TotalHours:0}小时",
                    CountdownState.MinutesOnly => $"{Placeholders.PH_JULI}{ExamName}{Hint}{Span.TotalMinutes:0}分钟",
                    CountdownState.SecondsOnly => $"{Placeholders.PH_JULI}{ExamName}{Hint}{Span.TotalSeconds:0}秒",
                    _ => ConfigPolicy.NotAllowed<string>()
                };
            }
        }

        private void SetCustomText(string Custom, TimeSpan Span)
        {
            LabelCountdown.Text = Custom
                    .Replace(Placeholders.PH_EXAMNAME, ExamName)
                    .Replace(Placeholders.PH_DAYS, $"{Span.Days}")
                    .Replace(Placeholders.PH_HOURS, $"{Span.Hours:00}")
                    .Replace(Placeholders.PH_MINUTES, $"{Span.Minutes:00}")
                    .Replace(Placeholders.PH_SECONDS, $"{Span.Seconds:00}")
                    .Replace(Placeholders.PH_ROUNDEDDAYS, $"{Span.Days + 1}")
                    .Replace(Placeholders.PH_TOTALHOURS, $"{Span.TotalHours:0}")
                    .Replace(Placeholders.PH_TOTALMINUTES, $"{Span.TotalMinutes:0}")
                    .Replace(Placeholders.PH_TOTALSECONDS, $"{Span.TotalSeconds:0}");
        }

        private void CompatibleWithPPTService()
        {
            if (IsPPTService)
            {
                var ValidArea = GetScreenWorkingArea();

                if (Left == ValidArea.Left && Top == ValidArea.Top)
                {
                    Left = ValidArea.Left + PptsvcThreshold;
                }
            }
        }

        private void RefreshScreen()
        {
            var SelectedIndex = ScreenIndex - 1;
            SelectedScreen = GetScreenWorkingArea(SelectedIndex == -1 ? 0 : SelectedIndex);
        }

        private void KeepOnScreen()
        {
            var ValidArea = GetScreenWorkingArea();

            if (Left < ValidArea.Left) Left = ValidArea.Left;
            if (Top < ValidArea.Top) Top = ValidArea.Top;
            if (Right > ValidArea.Right) Left = ValidArea.Right - Width;
            if (Bottom > ValidArea.Bottom) Top = ValidArea.Bottom - Height;
        }

        private void SaveLocation()
        {
            configManager.WriteConfig(new()
            {
                { ConfigItems.KPosX, $"{Location.X}" },
                { ConfigItems.KPosY, $"{Location.Y}" }
            });
        }

        private void OptimizeMemory(object state)
        {
            try
            {
                int MemoryUsage = int.Parse(ProcessHelper.GetProcessOutput(ProcessHelper.RunProcess("powershell.exe", $"-Command (Get-Counter \\\"\\Process({LaunchManager.AppNameEn})\\Working Set - Private\\\").CounterSamples.CookedValue", RedirectOutput: true)));

                if (MemoryUsage > 9437184)
                {
                    ConfigPolicy.NotAllowed<int>();
                }
            }
            catch
            {
                WindowsAPI.EmptyWorkingSet(Process.GetCurrentProcess().Handle);
            }
        }

        private void SetRoundedCorners()
        {
            if (Environment.OSVersion.Version > new Version(10, 0, 21999))
            {
                var attribute = WindowsAPI.DWMWINDOWATTRIBUTE.DWMWA_WINDOW_CORNER_PREFERENCE;
                var preference = WindowsAPI.DWM_WINDOW_CORNER_PREFERENCE.DWMWCP_ROUND;
                WindowsAPI.DwmSetWindowAttribute(Handle, attribute, ref preference, sizeof(uint));
            }
            else
            {
                IsWin10BelowRounded = true;
            }
        }

        private Rectangle GetScreenWorkingArea(int Index = -1)
        {
            if (Index >= 0)
            {
                return Screen.AllScreens[Index].WorkingArea;
            }

            return Screen.GetWorkingArea(this);
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            SetRoundedCorners();
            base.OnHandleCreated(e);
        }
    }
}