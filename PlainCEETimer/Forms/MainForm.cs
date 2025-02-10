using Microsoft.Win32;
using PlainCEETimer.Controls;
using PlainCEETimer.Dialogs;
using PlainCEETimer.Interop;
using PlainCEETimer.Modules;
using PlainCEETimer.Modules.Configuration;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlainCEETimer.Forms
{
    public partial class MainForm : AppForm
    {
        public static bool UniTopMost { get; private set; } = true;
        public static bool IsNormalStart { get; set; }
        public static bool ValidateNeeded { get; private set; } = true;

        public static ConfigObject AppConfigPub
        {
            get => AppConfig;
            set
            {
                AppConfig = value;
                AppLauncher.OnAppConfigChanged();
            }
        }

        public static Screen CurrentScreen { get; private set; } = null;

        private bool MemClean;
        private bool IsShowXOnly;
        private bool IsDraggable;
        private bool IsShowEnd;
        private bool IsShowPast;
        private bool IsRounding;
        private bool IsPPTService;
        private bool IsCustomText;
        private int ScreenIndex;
        private CountdownPosition CountdownPos;
        private int ShowXOnlyIndex;
        private DateTime ExamEndTime;
        private DateTime ExamStartTime;
        private ColorSetObject[] CountdownColors;
        private RulesManagerObject[] CustomRules;
        private string ExamName;
        private string[] CustomText;

        private bool IsReadyToMove;
        private bool IsCountdownReady;
        private bool IsCountdownRunning;
        private bool IsWin10BelowRound;
        private bool ShowTrayIcon;
        private bool ShowTrayText;
        private readonly int PptsvcThreshold = 1;
        private readonly int BorderRadius = 13;
        private CountdownState SelectedState;
        private System.Windows.Forms.Timer LocationWatcher;
        private System.Threading.Timer MemCleaner;
        private Point LastLocation;
        private Point LastMouseLocation;
        private Rectangle CurrentScreenRect;
        private SettingsForm FormSettings;
        private AboutForm FormAbout;
        private NotifyIcon TrayIcon;

        private ConfigHandler Config;
        private static ConfigObject AppConfig;
        private bool TrayIconReopen;
        private ExamInfoObject CurrentExam;
        private ExamInfoObject[] Exams;
        private int ExamIndex;

        private ContextMenu ContextMenuMain;
        private ContextMenu ContextMenuTray;
        private Menu.MenuItemCollection ExamSwitchMain;
        private Menu.MenuItemCollection ExamSwitchTray;

        public MainForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad()
        {
            LocationWatcher = new() { Interval = 1000 };
            LocationWatcher.Tick += LocationWatcher_Tick;
            LocationWatcher.Start();
            SizeChanged += MainForm_SizeChanged;
            SystemEvents.DisplaySettingsChanged += SystemEvents_DisplaySettingsChanged;
        }

        protected override void OnShown()
        {
            Config = new ConfigHandler();
            AppConfig = Config.Read();

            AppLauncher.AppConfigChanged += (sender, e) =>
            {
                Config.Save(AppConfig);
                RefreshSettings();
            };

            RefreshSettings();
            ValidateNeeded = false;
            Task.Run(() => UpdateChecker.CheckUpdate(true, this));
            _ = 1.ScaleTo(this);
            IsNormalStart = true;
        }

        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            if (IsWin10BelowRound)
            {
                var _BorderRadius = BorderRadius.ScaleTo(this);
                var HRgn = NativeInterop.CreateRoundRectRgn(0, 0, Width, Height, _BorderRadius, _BorderRadius);
                Region = Region.FromHrgn(HRgn);
                NativeInterop.DeleteObject(HRgn);
            }
        }

        private async void RefreshSettings()
        {
            if (ValidateNeeded)
            {
                AppConfig.Display.Rounding = AppConfig.Display.Rounding && AppConfig.Display.ShowXOnly && AppConfig.Display.X == 0;
                AppConfig.Display.CustomText = AppConfig.Display.CustomText && !AppConfig.Display.ShowXOnly;
                AppConfig.Display.SeewoPptsvc = AppConfig.Display.SeewoPptsvc && ((AppConfig.General.TopMost && AppConfig.Display.X == 0) || AppConfig.Display.Draggable);
                AppConfig.Tools.TrayText = AppConfig.Tools.TrayText && AppConfig.Tools.TrayIcon;
            }

            Exams = AppConfig.General.ExamInfo;
            ExamIndex = AppConfig.General.ExamIndex;

            try
            {
                CurrentExam = Exams[ExamIndex];
            }
            catch
            {
                CurrentExam = new();
            }

            ExamName = CurrentExam.ExamName;
            CustomText = AppConfig.Display.CustomTexts;
            ExamStartTime = CurrentExam.ExamStartTime;
            ExamEndTime = CurrentExam.ExamEndTime;
            MemClean = AppConfig.General.MemClean;
            var TopMostTmp = AppConfig.General.TopMost;
            IsShowXOnly = AppConfig.Display.ShowXOnly;
            IsRounding = AppConfig.Display.Rounding;
            IsShowEnd = ValidateEndPast(AppConfig.Display.EndIndex);
            IsShowPast = AppConfig.Display.EndIndex == 2;
            IsDraggable = AppConfig.Display.Draggable;
            UniTopMost = AppConfig.General.UniTopMost;
            IsPPTService = AppConfig.Display.SeewoPptsvc;
            IsCustomText = AppConfig.Display.CustomText;
            ScreenIndex = AppConfig.Display.ScreenIndex - 1;
            CountdownPos = AppConfig.Display.Position;
            ShowXOnlyIndex = AppConfig.Display.X;
            ShowTrayIcon = AppConfig.Tools.TrayIcon;
            ShowTrayText = AppConfig.Tools.TrayText;
            CustomRules = AppConfig.CustomRules;
            ColorDialogEx.CustomColorCollection = AppConfig.CustomColors;
            CountdownColors = AppConfig.Appearance.Colors;

#if DEBUG
            Console.WriteLine("##########################");
#endif

            IsCountdownReady = !string.IsNullOrWhiteSpace(ExamName) && ExamStartTime.IsValid() && ExamEndTime.IsValid() && (ExamEndTime > ExamStartTime || !IsShowEnd);

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

            LabelCountdown.Font = AppConfig.Appearance.Font;

            LabelCountdown.MouseDown -= LabelCountdown_MouseDown;
            LabelCountdown.MouseMove -= LabelCountdown_MouseMove;
            LabelCountdown.MouseUp -= LabelCountdown_MouseUp;

            if (IsDraggable)
            {
                LabelCountdown.MouseDown += LabelCountdown_MouseDown;
                LabelCountdown.MouseMove += LabelCountdown_MouseMove;
                LabelCountdown.MouseUp += LabelCountdown_MouseUp;
                Location = AppConfig.Pos;
            }
            else
            {
                RefreshScreen();
            }

            ApplyLocation();
            CompatibleWithPPTService();
            InitializeContextMenuAndTrayIcon();

            AppLauncher.OnUniTopMostStateChanged();

            MemCleaner?.Dispose();
            if (MemClean)
            {
                MemCleaner = new(CleanMemory, null, TimeSpan.Zero, TimeSpan.FromMinutes(5));
            }

            SetLabelCountdownAutoWrap();
            TopMost = false;
            TopMost = TopMostTmp;
            ShowInTaskbar = !TopMost;
            await StartCountdown();
        }

        private void InitializeContextMenuAndTrayIcon()
        {
            #region 来自网络
            /*
            
            克隆 (重用) 现有 ContextMenuStrip 实例 参考：

            .net - C# - Duplicate ContextMenuStrip Items into another - Stack Overflow
            https://stackoverflow.com/questions/37884815/c-sharp-duplicate-contextmenustrip-items-into-another

            */

            ContextMenu BaseContextMenu() => CreateNew
            ([
                AddSubMenu("切换(&Q)",
                [
                    AddItem("暂无考试信息"),
                    AddSeparator(),
                    AddItem("管理(&M)", ExamSwitchManage_Click)
                ]),
                AddSeparator(),
                AddItem("设置(&S)", ContextSettings_Click),
                AddItem("关于(&A)", ContextAbout_Click),
                AddSeparator(),
                AddItem("安装目录(&D)", (sender, e) => AppLauncher.OpenInstallDir())
            ]);
            #endregion

            ContextMenuMain = BaseContextMenu();
            ContextMenuTray = BaseContextMenu();

            ExamSwitchMain = ContextMenuMain.MenuItems[0].MenuItems;
            ExamSwitchTray = ContextMenuTray.MenuItems[0].MenuItems;

            ContextMenu = ContextMenuMain;
            LabelCountdown.ContextMenu = ContextMenuMain;

            if (Exams.Length != 0)
            {
                ExamSwitchMain.RemoveAt(0);
                ExamSwitchTray.RemoveAt(0);

                var ItemIndex = 0;
                foreach (var Exam in Exams)
                {
                    var ItemMain = new MenuItem()
                    {
                        Text = Exam.ToString(),
                        Tag = ItemIndex,
                        RadioCheck = true
                    };

                    var ItemTray = new MenuItem()
                    {
                        Text = Exam.ToString(),
                        Tag = ItemIndex,
                        RadioCheck = true
                    };

                    ItemMain.Click += ExamItems_Click;
                    ItemTray.Click += ExamItems_Click;

                    ExamSwitchMain.Add(0, ItemMain);
                    ExamSwitchTray.Add(0, ItemTray);

                    ItemIndex++;
                }
            }

            ExamSwitchMain[ExamIndex].Checked = true;
            ExamSwitchTray[ExamIndex].Checked = true;

            if (TrayIcon == null)
            {
                if (ShowTrayIcon)
                {
                    if (TrayIconReopen)
                    {
                        if (MessageX.Warn("由于系统限制，重新开关托盘图标需要重启应用程序后方可正常显示。\n\n是否立即重启？", Buttons: MessageBoxExButtons.YesNo) == DialogResult.Yes)
                        {
                            AppLauncher.Shutdown(true);
                        }
                        else
                        {
                            return;
                        }
                    }

                    TrayIcon = new()
                    {
                        Visible = true,
                        Text = Text,
                        Icon = AppLauncher.AppIcon,
                        ContextMenu = Merge(ContextMenuTray, CreateNew
                        ([
                            AddSeparator(),
                            AddItem("显示界面(&S)", (sender, e) => AppLauncher.OnTrayMenuShowAllClicked()),
                            AddSubMenu("关闭(&C)",
                            [
                                AddItem("重启(&R)", (sender, e) => AppLauncher.Shutdown(true)),
                                AddItem("退出(&Q)", (sender, e) => AppLauncher.Shutdown())
                            ])
                        ]))
                    };

                    TrayIcon.MouseClick += TrayIcon_MouseClick;

                    if (!ShowTrayText)
                    {
                        UpdateTrayIconText(AppLauncher.AppName, false);
                    }
                }
            }
            else
            {
                if (!ShowTrayIcon)
                {
                    TrayIcon.Dispose();
                    TrayIcon = null;
                    TrayIconReopen = true;
                }
                else
                {
                    if (!ShowTrayText)
                    {
                        UpdateTrayIconText(AppLauncher.AppName, false);
                    }
                }
            }
        }

        private void ExamItems_Click(object sender, EventArgs e)
        {
            var Item = (MenuItem)sender;

            if (!Item.Checked)
            {
                Item.Checked = true;
            }

            if (Item.Checked)
            {
                ExamIndex = (int)Item.Tag;
                RefreshSettings();
            }
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

        private void ExamSwitchManage_Click(object sender, EventArgs e)
        {
            ExamInfoManager ExamInfoMan = new()
            {
                ExamInfo = Exams,
                PeriodIndex = ExamIndex
            };

            if (ExamInfoMan.ShowDialog() == DialogResult.OK)
            {

            }
        }

        private void ContextSettings_Click(object sender, EventArgs e)
        {
            if (FormSettings == null || FormSettings.IsDisposed)
            {
                FormSettings = new()
                {
                    ExamName = ExamName,
                    ExamStartTime = ExamStartTime,
                    ExamEndTime = ExamEndTime
                };
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

        private void TrayIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) AppLauncher.OnTrayMenuShowAllClicked();
        }

        protected override void OnClosing(FormClosingEventArgs e)
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
            UpdateCountdown("欢迎使用高考倒计时", CountdownColors[3].Fore, CountdownColors[3].Back);
            UpdateTrayIconText(AppLauncher.AppName);
        }

        private void ApplyColorRule(int Phase, TimeSpan Span, string Name, string Hint)
        {
            if (IsCustomText)
            {
                var r = CustomRules.Where(x => (int)x.Phase == Phase).Select(x => new { x.Tick, x.Text, x.Fore, x.Back });
                var Rules = (Phase == 2 ? r.OrderByDescending(x => x.Tick) : r.OrderBy(x => x.Tick)).ToList();

                if (Rules.Count > 0)
                {
                    foreach (var Rule in Rules)
                    {
                        if (Phase == 2 ? (Span >= Rule.Tick) : (Span <= Rule.Tick + new TimeSpan(0, 0, 0, 1)))
                        {
                            SetCountdown(Span, Name, Hint, Rule.Fore, Rule.Back, Rule.Text);
                            return;
                        }
                    }
                }
            }

            SetCountdown(Span, Name, Hint, CountdownColors[Phase].Fore, CountdownColors[Phase].Back, CustomText[Phase]);
        }

        private void ApplyLocation()
        {
            if (!IsDraggable)
            {
                Location = CountdownPos switch
                {
                    CountdownPosition.LeftCenter
                        => new(CurrentScreenRect.Left, CurrentScreenRect.Top + CurrentScreenRect.Height / 2 - Height / 2),

                    CountdownPosition.BottomLeft
                        => new(CurrentScreenRect.Left, CurrentScreenRect.Bottom - Height),

                    CountdownPosition.TopCenter
                        => new(CurrentScreenRect.Left + CurrentScreenRect.Width / 2 - Width / 2, CurrentScreenRect.Top),

                    CountdownPosition.Center
                        => new(CurrentScreenRect.Left + CurrentScreenRect.Width / 2 - Width / 2, CurrentScreenRect.Top + CurrentScreenRect.Height / 2 - Height / 2),

                    CountdownPosition.BottomCenter
                        => new(CurrentScreenRect.Left + CurrentScreenRect.Width / 2 - Width / 2, CurrentScreenRect.Bottom - Height),

                    CountdownPosition.TopRight
                        => new(CurrentScreenRect.Right - Width, CurrentScreenRect.Top),

                    CountdownPosition.RightCenter
                        => new(CurrentScreenRect.Right - Width, CurrentScreenRect.Top + CurrentScreenRect.Height / 2 - Height / 2),

                    CountdownPosition.BottomRight
                        => new(CurrentScreenRect.Right - Width, CurrentScreenRect.Bottom - Height),

                    _
                        => IsPPTService ? new(CurrentScreenRect.Location.X + PptsvcThreshold, CurrentScreenRect.Location.Y) : CurrentScreenRect.Location
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
            CountdownState.DaysOnly
                => string.Format("{0}{1}{2}{3}天", Placeholders.PH_JULI, Name, Hint, Span.Days),

            CountdownState.DaysOnlyWithRounding
                => string.Format("{0}{1}{2}{3}天", Placeholders.PH_JULI, Name, Hint, Span.Days + 1),

            CountdownState.HoursOnly
                => string.Format("{0}{1}{2}{3:0}小时", Placeholders.PH_JULI, Name, Hint, Span.TotalHours),

            CountdownState.MinutesOnly
                => string.Format("{0}{1}{2}{3:0}分钟", Placeholders.PH_JULI, Name, Hint, Span.TotalMinutes),

            CountdownState.SecondsOnly
                => string.Format("{0}{1}{2}{3:0}秒", Placeholders.PH_JULI, Name, Hint, Span.TotalSeconds),

            _
                => string.Format("{0}{1}{2}{3}天{4:00}时{5:00}分{6:00}秒", Placeholders.PH_JULI, Name, Hint, Span.Days, Span.Hours, Span.Minutes, Span.Seconds)
        };


        private void UpdateCountdown(string CountdownText, Color Fore, Color Back)
        {
            BeginInvoke(() =>
            {
                LabelCountdown.Text = CountdownText;
                LabelCountdown.ForeColor = Fore;
                BackColor = Back;

                if (ShowTrayText)
                {
                    UpdateTrayIconText(CountdownText, false);
                }
            });
        }

        private void UpdateTrayIconText(string cText, bool cInvokeRequired = true)
        {
            if (TrayIcon != null)
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
        }

        private void CompatibleWithPPTService()
        {
            if (IsPPTService)
            {
                var ValidArea = GetScreenRect();

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
            CurrentScreenRect = GetScreenRect(ScreenIndex);
            CurrentScreen = Screen.FromControl(this);
        }

        private void KeepOnScreen()
        {
            var ValidArea = GetScreenRect();

            if (Left < ValidArea.Left) Left = ValidArea.Left;
            if (Top < ValidArea.Top) Top = ValidArea.Top;
            if (Right > ValidArea.Right) Left = ValidArea.Right - Width;
            if (Bottom > ValidArea.Bottom) Top = ValidArea.Bottom - Height;
        }

        private void SaveLocation()
        {
            AppConfig.Pos = Location;
            Config.Save(AppConfig);
        }

        private void CleanMemory(object state)
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

        private void SetRoundCorners()
        {
            if (Environment.OSVersion.Version > new Version(10, 0, 21999))
            {
                var attribute = DWMWINDOWATTRIBUTE.DWMWA_WINDOW_CORNER_PREFERENCE;
                var preference = DWM_WINDOW_CORNER_PREFERENCE.DWMWCP_ROUND;
                NativeInterop.DwmSetWindowAttribute(Handle, attribute, ref preference, sizeof(uint));
            }
            else
            {
                IsWin10BelowRound = true;
            }
        }

        private Rectangle GetScreenRect(int Index = -1)
        {
            if (Index >= 0)
            {
                return Screen.AllScreens[Index].WorkingArea;
            }

            return Screen.GetWorkingArea(this);
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            SetRoundCorners();
        }
    }
}