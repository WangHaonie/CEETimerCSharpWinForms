using CEETimerCSharpWinForms.Controls;
using CEETimerCSharpWinForms.Interop;
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
    public partial class MainForm : TrackableForm
    {
        public static bool UniTopMost { get; private set; } = true;
        public static bool IsNormalStart { get; set; } = false;

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
        private List<TupleEx<int, TimeSpan, TupleEx<Color, Color, string>>> CustomRules;
        private string WindowTitle = "高考倒计时";
        private string ExamName;
        private string[] CustomText;

        private bool IsReadyToMove;
        private bool IsCountdownReady;
        private bool IsCountdownRunning;
        private bool IsWin10BelowRounded;
        private readonly int PptsvcThreshold = 1;
        private readonly int BorderRadius = 13;
        private CountdownState SelectedState;
        private System.Windows.Forms.Timer LocationWatcher;
        private System.Threading.Timer MemoryOptimizer;
        private Point LastLocation;
        private Point LastMouseLocation;
        private Rectangle SelectedScreen;
        private SettingsForm FormSettings;
        private AboutForm FormAbout;
        private NotifyIcon TrayIcon;
        private readonly ConfigManager Config = new();
        private readonly FontConverter fontConverter = new();

        public MainForm()
        {
            InitializeComponent();
        }

        protected override void OnTrackableFormLoad()
        {
            DefaultColors = [new(Color.Red, Color.White), new(Color.Green, Color.White), new(Color.Black, Color.White), new(Color.Black, Color.White)];

            SizeChanged += MainForm_SizeChanged;
            SystemEvents.DisplaySettingsChanged += SystemEvents_DisplaySettingsChanged;

            InitializeContextMenuAndTrayIcon();
            RefreshSettings();

            LocationWatcher = new() { Interval = 1000 };
            LocationWatcher.Tick += LocationWatcher_Tick;
            LocationWatcher.Start();

            Task.Run(() => UpdateChecker.CheckUpdate(true, this));
            _ = 1.WithDpi(this);
            IsNormalStart = true;
        }

        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            if (IsWin10BelowRounded)
            {
                var _BorderRadius = BorderRadius.WithDpi(this);
                Region = Region.FromHrgn(NativeInterop.CreateRoundRectRgn(0, 0, Width, Height, _BorderRadius, _BorderRadius));
            }
        }

        private void InitializeContextMenuAndTrayIcon()
        {
            #region 来自网络
            /*
            
            克隆 (重用) 现有 ContextMenuStrip 实例 参考：

            .net - C# - Duplicate ContextMenuStrip Items into another - Stack Overflow
            https://stackoverflow.com/questions/37884815/c-sharp-duplicate-contextmenustrip-items-into-another

            */
            ContextMenuStrip BaseContextMenu() => new ContextMenuStrip()
                .AddContextMenuItem("设置(&S)", ContextSettings_Click)
                .AddContextMenuItem("关于(&A)", ContextAbout_Click)
                .AddContextSeparator()
                .AddContextMenuItem("安装目录(&D)", (sender, e) => AppLauncher.OpenInstallDir());
            #endregion

            var ContextMenuMain = BaseContextMenu();
            ContextMenuStrip = ContextMenuMain;
            LabelCountdown.ContextMenuStrip = ContextMenuMain;

            TrayIcon = new()
            {
                Visible = true,
                Text = Text,
                Icon = Icon.FromHandle(Properties.Resources.AppIcon100px.GetHicon()),
                ContextMenuStrip = BaseContextMenu()
                    .AddContextSeparator()
                    .AddContextMenuItem("显示界面(&S)", (sender, e) => AppLauncher.OnTrayMenuShowAllClicked())
                    .AddContextMenuItem("退出(&Q)", (sender, e) => AppLauncher.Shutdown())
            };
        }

        private async void RefreshSettings()
        {
            Config.MountConfig(true);

            ExamName = Config.ReadConfig(ConfigItems.KExamName);
            CustomText = CustomRuleHelper.GetCustomTextFormRaw(Config.ReadConfig(ConfigItems.KCustomTextP1), Config.ReadConfig(ConfigItems.KCustomTextP2), Config.ReadConfig(ConfigItems.KCustomTextP3));
            ExamStartTime = DateTime.TryParseExact(Config.ReadConfig(ConfigItems.KStartTime), "yyyyMMddHHmmss", null, DateTimeStyles.None, out DateTime tmpw) ? tmpw : DateTime.Now;
            ExamEndTime = DateTime.TryParseExact(Config.ReadConfig(ConfigItems.KEndTime), "yyyyMMddHHmmss", null, DateTimeStyles.None, out DateTime tmpx) ? tmpx : DateTime.Now;
            IsMemoryOptimizationEnabled = bool.TryParse(Config.ReadConfig(ConfigItems.KMemOpti), out bool tmpc) && tmpc;
            TopMost = !bool.TryParse(Config.ReadConfig(ConfigItems.KTopMost), out bool tmpa) || tmpa;
            IsShowXOnly = bool.TryParse(Config.ReadConfig(ConfigItems.KShowXOnly), out bool tmpd) && tmpd;
            IsRounding = bool.TryParse(Config.ReadConfig(ConfigItems.KRounding), out bool tmpe) && tmpe;
            IsShowPast = bool.TryParse(Config.ReadConfig(ConfigItems.KShowPast), out bool tmpg) && tmpg;
            IsShowEnd = bool.TryParse(Config.ReadConfig(ConfigItems.KShowEnd), out bool tmpf) && tmpf;
            IsDraggable = bool.TryParse(Config.ReadConfig(ConfigItems.KDraggable), out bool tmph) && tmph;
            UniTopMost = bool.TryParse(Config.ReadConfig(ConfigItems.KUniTopMost), out bool tmpi) && tmpi;
            IsPPTService = bool.TryParse(Config.ReadConfig(ConfigItems.KSeewoPptSvc), out bool tmpj) && tmpj;
            IsCustomText = bool.TryParse(Config.ReadConfig(ConfigItems.KIsCustomText), out bool tmpo) && tmpo;
            ScreenIndex = int.TryParse(Config.ReadConfig(ConfigItems.KScreen), out int tmpk) ? tmpk : 0;
            PositionIndex = int.TryParse(Config.ReadConfig(ConfigItems.KPosition), out int tmpu) ? tmpu : 0;
            ShowXOnlyIndex = int.TryParse(Config.ReadConfig(ConfigItems.KShowValue), out int tmpl) ? tmpl : 0;
            try { CustomRules = CustomRuleHelper.GetObject(Config.ReadConfigEx(ConfigItems.KCustomRules)); } catch { CustomRules = []; }
            ColorDialogEx.CustomColorCollection = ColorHelper.GetArgbArray(Config.ReadConfig(ConfigItems.KCustomColors));
            int.TryParse(Config.ReadConfig(ConfigItems.KPosX), out int x);
            int.TryParse(Config.ReadConfig(ConfigItems.KPosY), out int y);
            CountdownColors = [];

            for (int i = 0; i < 4; i++)
            {
                var Fore = ColorHelper.TryParseRGB(Config.ReadConfig($"Fore{i + 1}"), out Color tmpfore) ? tmpfore : DefaultColors[i].Item1;
                var Back = ColorHelper.TryParseRGB(Config.ReadConfig($"Back{i + 1}"), out Color tmpback) ? tmpback : DefaultColors[i].Item2;

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
            IsCountdownReady = !string.IsNullOrWhiteSpace(ExamName) && Config.IsValidData(tmpw) && Config.IsValidData(tmpx) && (tmpx > tmpw || !IsShowEnd);
            IsPPTService = IsPPTService && ((TopMost && ShowXOnlyIndex == 0) || IsDraggable);

            SelectedState = CountdownState.Normal;

            if (IsShowXOnly)
            {
                SelectedState = ShowXOnlyIndex switch
                {
                    1 => CountdownState.HoursOnly,
                    2 => CountdownState.MinutesOnly,
                    3 => CountdownState.SecondsOnly,
                    _ => IsRounding ? CountdownState.DaysOnlyWithRounding : CountdownState.DaysOnly
                };
            }

            try
            {
                SelectedFont = (Font)fontConverter.ConvertFromString(Config.ReadConfig(ConfigItems.KFont));
                SelectedFontStyle = (FontStyle)Enum.Parse(typeof(FontStyle), Config.ReadConfig(ConfigItems.KFontStyle));

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

            foreach (var f in FormManager.OpenForms)
            {
                if (f == this)
                {
                    continue;
                }

                f.TopMost = UniTopMost;
            }

            MemoryOptimizer?.Dispose();
            if (IsMemoryOptimizationEnabled)
            {
                MemoryOptimizer = new(OptimizeMemory, null, TimeSpan.Zero, TimeSpan.FromMinutes(5));
            }

            Config.MountConfig(false);
            SetLabelCountdownAutoWrap();

            await StartCountdown();
        }

        private void SystemEvents_DisplaySettingsChanged(object sender, EventArgs e)
        {
            RefreshScreen();
            SetLabelCountdownAutoWrap();
        }

        #region 来自网络
        /*
        
        无边框窗口的拖动 参考:

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

            if (IsReadyToMove && Location != LastLocation)
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
            if (FormSettings == null || FormSettings.IsDisposed)
            {
                FormSettings = new()
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

                FormSettings.ConfigChanged += (sender, e) => RefreshSettings();
            }

            FormSettings.ReActivate();
        }

        private void ContextAbout_Click(object sender, EventArgs e)
        {
            if (FormAbout == null || FormAbout.IsDisposed)
            {
                FormAbout = new();
            }

            FormAbout.ReActivate();
        }

        protected override void OnTrackableFormClosing(FormClosingEventArgs e)
        {
            e.Cancel = e.CloseReason != CloseReason.WindowsShutDown;
        }

        private void LocationWatcher_Tick(object sender, EventArgs e)
        {
            if (!IsReadyToMove)
            {
                ApplyLocation();
                KeepOnScreen();
            }
        }

        private async Task StartCountdown()
        {
            if (IsCountdownRunning)
            {
                return;
            }

            IsCountdownRunning = true;

            while (true)
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
                    break;
                }

                await Task.Delay(1000);
            }

            IsCountdownRunning = false;
            UpdateCountdown("欢迎使用高考倒计时", CountdownColors[3].Item1, CountdownColors[3].Item2);
            UpdateTrayIconText(WindowTitle);
        }

        private void ApplyColorRule(int Phase, TimeSpan Span, string Name, string Hint)
        {
            if (IsCustomText)
            {
                var r = CustomRules.Where(i => i.Item1 == Phase).Select(x => new
                {
                    Tick = x.Item2,
                    Fore = x.Item3.Item1,
                    Back = x.Item3.Item2,
                    Custom = x.Item3.Item3
                });

                var Rules = (Phase == 2 ? r.OrderByDescending(x => x.Tick) : r.OrderBy(x => x.Tick)).ToList();

                if (Rules.Count > 0)
                {
                    foreach (var Rule in Rules)
                    {
                        if (Phase == 2 ? (Span >= Rule.Tick) : (Span <= Rule.Tick + new TimeSpan(0, 0, 0, 1)))
                        {
                            SetCountdown(Span, Name, Hint, Rule.Fore, Rule.Back, Rule.Custom);
                            return;
                        }
                    }
                }
            }

            SetCountdown(Span, Name, Hint, CountdownColors[Phase].Item1, CountdownColors[Phase].Item2, CustomText[Phase]);
        }

        private void ApplyLocation()
        {
            if (!IsDraggable)
            {
                Location = PositionIndex switch
                {
                    1 => new(SelectedScreen.Left, SelectedScreen.Top + SelectedScreen.Height / 2 - Height / 2),
                    2 => new(SelectedScreen.Left, SelectedScreen.Bottom - Height),
                    3 => new(SelectedScreen.Left + SelectedScreen.Width / 2 - Width / 2, SelectedScreen.Top),
                    4 => new(SelectedScreen.Left + SelectedScreen.Width / 2 - Width / 2, SelectedScreen.Top + SelectedScreen.Height / 2 - Height / 2),
                    5 => new(SelectedScreen.Left + SelectedScreen.Width / 2 - Width / 2, SelectedScreen.Bottom - Height),
                    6 => new(SelectedScreen.Right - Width, SelectedScreen.Top),
                    7 => new(SelectedScreen.Right - Width, SelectedScreen.Top + SelectedScreen.Height / 2 - Height / 2),
                    8 => new(SelectedScreen.Right - Width, SelectedScreen.Bottom - Height),
                    _ => IsPPTService ? new(SelectedScreen.Location.X + PptsvcThreshold, SelectedScreen.Location.Y) : SelectedScreen.Location
                };
            }
        }

        private void SetCountdown(TimeSpan Span, string Name, string Hint, Color Fore, Color Back, string Custom)
        {
            UpdateCountdown(IsCustomText ? GetCountdownWithCustomText(Span, Name, Custom) : GetCountdown(Span, Name, Hint), Fore, Back);
        }

        private string GetCountdownWithCustomText(TimeSpan Span, string Name, string Custom) => Custom
            .Replace(Placeholders.PH_EXAMNAME, Name)
            .Replace(Placeholders.PH_DAYS, $"{Span.Days}")
            .Replace(Placeholders.PH_HOURS, $"{Span.Hours:00}")
            .Replace(Placeholders.PH_MINUTES, $"{Span.Minutes:00}")
            .Replace(Placeholders.PH_SECONDS, $"{Span.Seconds:00}")
            .Replace(Placeholders.PH_ROUNDEDDAYS, $"{Span.Days + 1}")
            .Replace(Placeholders.PH_TOTALHOURS, $"{Span.TotalHours:0}")
            .Replace(Placeholders.PH_TOTALMINUTES, $"{Span.TotalMinutes:0}")
            .Replace(Placeholders.PH_TOTALSECONDS, $"{Span.TotalSeconds:0}");


        private string GetCountdown(TimeSpan Span, string Name, string Hint) => SelectedState switch
        {
            CountdownState.DaysOnly => $"{Placeholders.PH_JULI}{Name}{Hint}{Span.Days}天",
            CountdownState.DaysOnlyWithRounding => $"{Placeholders.PH_JULI}{Name}{Hint}{Span.Days + 1}天",
            CountdownState.HoursOnly => $"{Placeholders.PH_JULI}{Name}{Hint}{Span.TotalHours:0}小时",
            CountdownState.MinutesOnly => $"{Placeholders.PH_JULI}{Name}{Hint}{Span.TotalMinutes:0}分钟",
            CountdownState.SecondsOnly => $"{Placeholders.PH_JULI}{Name}{Hint}{Span.TotalSeconds:0}秒",
            _ => $"{Placeholders.PH_JULI}{Name}{Hint}{Span.Days}天{Span.Hours:00}时{Span.Minutes:00}分{Span.Seconds:00}秒"
        };


        private void UpdateCountdown(string CountdownText, Color Fore, Color Back)
        {
            BeginInvoke(() =>
            {
                LabelCountdown.Text = CountdownText;
                LabelCountdown.ForeColor = Fore;
                BackColor = Back;
                UpdateTrayIconText(CountdownText, false);
            });
        }

        private void UpdateTrayIconText(string cText, bool cInvokeRequired = true)
        {
            if (cInvokeRequired)
            {
                BeginInvoke(() =>
                {
                    TrayIcon.Text = cText;
                });
            }
            else
            {
                TrayIcon.Text = cText;
            }
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

        private void SetLabelCountdownAutoWrap()
        {
            SetLabelAutoWrap(LabelCountdown, true);
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
            Config.WriteConfig(new()
            {
                { ConfigItems.KPosX, $"{Location.X}" },
                { ConfigItems.KPosY, $"{Location.Y}" }
            });
        }

        private void OptimizeMemory(object state)
        {
            try
            {
                int MemoryUsage = int.Parse(ProcessHelper.GetProcessOutput(ProcessHelper.RunProcess("powershell.exe", $"-Command (Get-Counter \\\"\\Process({AppLauncher.AppNameEng})\\Working Set - Private\\\").CounterSamples.CookedValue", RedirectOutput: true)));

                if (MemoryUsage > 9437184)
                {
                    ConfigPolicy.NotAllowed<int>();
                }
            }
            catch
            {
                NativeInterop.EmptyWorkingSet(Process.GetCurrentProcess().Handle);
            }
        }

        private void SetRoundedCorners()
        {
            if (Environment.OSVersion.Version > new Version(10, 0, 21999))
            {
                var attribute = DWMWINDOWATTRIBUTE.DWMWA_WINDOW_CORNER_PREFERENCE;
                var preference = DWM_WINDOW_CORNER_PREFERENCE.DWMWCP_ROUND;
                NativeInterop.DwmSetWindowAttribute(Handle, attribute, ref preference, sizeof(uint));
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