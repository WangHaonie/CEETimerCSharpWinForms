using CEETimerCSharpWinForms.Forms;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CEETimerCSharpWinForms.Modules
{
    public static class AppLauncher
    {
        public static string CurrentExecutableDir => AppDomain.CurrentDomain.BaseDirectory;
        public static string CurrentExecutablePath => Application.ExecutablePath;
        public static string ConfigFilePath => $"{CurrentExecutableDir}{AppNameEng}.config";
        public static bool IsAdmin { get; private set; }
        public static Icon AppIcon { get; private set; }

        public const string AppName = "高考倒计时 by WangHaonie";
        public const string AppNameEng = "CEETimerCSharpWinForms";
        public const string AppVersion = "3.0.8";
        public const string AppBuildDate = "2024/08/06";
        public const string CopyrightInfo = "Copyright © 2023-2024 WangHaonie";
        public const string OriginalFileName = $"{AppNameEng}.exe";
        public const string InfoMsg = "提示 - 高考倒计时";
        public const string WarnMsg = "警告 - 高考倒计时";
        public const string ErrMsg = "错误 - 高考倒计时";
        public const string DateTimeFormat = "yyyy-MM-dd dddd HH:mm:ss";
        public const string UpdateAPI = "https://gitee.com/WangHaonie/CEETimerCSharpWinForms/raw/main/api/github.json";
        public const string UpdateURL = "https://gitee.com/WangHaonie/CEETimerCSharpWinForms/raw/main/download/CEETimerCSharpWinForms_{0}_x64_Setup.exe";
        public const string RequestUA = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/127.0.0.0 Safari/537.36";

        private static readonly string PipeName = $"{AppNameEng}_[34c14833-98da-49f7-a2ab-369e88e73b95]";
        private static readonly string CurrentExecutableName = Path.GetFileName(CurrentExecutablePath);
        private static readonly MessageBoxHelper MessageX = new(FormManager.OpenForms.LastOrDefault());

        public static event EventHandler TrayMenuShowAllClicked;
        public static void OnTrayMenuShowAllClicked() => TrayMenuShowAllClicked?.Invoke(null, EventArgs.Empty);

        public static event EventHandler UniTopMostStateChanged;
        public static void OnUniTopMostStateChanged() => UniTopMostStateChanged?.Invoke(null, EventArgs.Empty);

        public static event EventHandler AppConfigChanged;
        public static void OnAppConfigChanged() => AppConfigChanged?.Invoke(null, EventArgs.Empty);

        public static void StartProgram(string[] args)
        {
            AppIcon = Icon.ExtractAssociatedIcon(CurrentExecutablePath);

            var Args = Array.ConvertAll(args, x => x.ToLower());
            var AllArgs = string.Join(" ", args);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += (sender, e) => HandleException(e.Exception);
            AppDomain.CurrentDomain.UnhandledException += (sender, e) => HandleException((Exception)e.ExceptionObject);

            using var MutexMain = new Mutex(true, $"{AppNameEng}_MUTEX_61c0097d-3682-421c-84e6-70ca37dc31dd_[A3F8B92E6D14]", out bool IsNewProcess);

            if (IsNewProcess)
            {
                new Thread(StartPipeServer).Start();

                if (!CurrentExecutableName.Equals(OriginalFileName, StringComparison.OrdinalIgnoreCase))
                {
                    MessageX.Error($"为了您的使用体验，请不要更改程序文件名! 程序将在该消息框自动关闭后尝试自动恢复到原文件名，若自动恢复失败请手动改回。\n\n当前文件名: {CurrentExecutableName}\n原始文件名: {OriginalFileName}", Position: FormStartPosition.CenterScreen, AutoClose: true);
                    ProcessHelper.RunProcess("cmd.exe", $"/c ren \"{CurrentExecutablePath}\" {OriginalFileName} & start \"\" \"{CurrentExecutableDir}{OriginalFileName}\" {AllArgs}");
                    Exit(ExitReason.InvalidExeName);
                }
                else
                {
                    if (Args.Length == 0)
                    {
                        Task.Run(() => CheckAdmin(out _));
                        Application.Run(new MainForm());
                    }
                    else
                    {
                        switch (Args[0])
                        {
                            case "/?":
                            case "/h":
                                MessageX.Info("可用的命令行参数: \n\n/h    显示此帮助信息；\n/ac  检测当前用户是否具有管理员权限；\n/fr <版本号>\n        强制下载并安装指定的版本，留空则当前版本，\n        推荐在特殊情况下使用，不支持老版本。");
                                break;
                            case "/ac":
                                CheckAdmin(out string UserName, true);
                                MessageX.Info($"当前用户 {UserName} {(IsAdmin ? "" : "不")}具有管理员权限。");
                                break;
                            case "/fr":
                                if (Args.Length > 1) DownloaderForm.ManualVersion = Args[1];
                                Application.Run(new DownloaderForm());
                                break;
                            default:
                                MessageX.Error($"无法解析的命令行参数: \n{AllArgs}", AutoClose: true);
                                break;
                        }
                    }

                    Exit(ExitReason.NormalExit);
                }
            }
            else
            {
                if (Args.Length != 0)
                {
                    MessageX.Error("请先关闭已打开的实例再使用命令行功能。", AutoClose: true);
                }

                StartPipeClient();
                Exit(ExitReason.AnotherInstanceIsRunning);
            }
        }

        public static void Shutdown(bool Restart = false)
        {
            ProcessHelper.RunProcess("cmd.exe", $"/c taskkill /f /fi \"PID eq {Process.GetCurrentProcess().Id}\" /im {CurrentExecutableName} {(Restart ? $"& start \"\" \"{CurrentExecutablePath}\"" : "")}");
            Exit(ExitReason.UserShutdownOrRestart);
        }

        public static void CheckAdmin(out string UserName, bool QueryUserName = false)
        {
            var AdminChecker = ProcessHelper.RunProcess("cmd.exe", "/c net session");
            AdminChecker.WaitForExit();
            IsAdmin = AdminChecker.ExitCode == 0;
            UserName = "";

            if (QueryUserName)
            {
                UserName = ProcessHelper.GetProcessOutput(ProcessHelper.RunProcess("cmd.exe", "/c whoami", RedirectOutput: true));
            }
        }

        private static void StartPipeServer()
        {
            try
            {
                while (true)
                {
                    using var PipeServer = new NamedPipeServerStream(PipeName, PipeDirection.InOut);
                    PipeServer.WaitForConnection();
                    OnTrayMenuShowAllClicked();
                }
            }
            catch { }
        }

        private static void StartPipeClient()
        {
            try
            {
                using var PipeClient = new NamedPipeClientStream(".", PipeName, PipeDirection.Out);
                PipeClient.Connect(1000);
            }
            catch { }
        }

        private static void HandleException(Exception ex)
        {
            var ExOutput = $"\n\n================== v{AppVersion} - {DateTime.Now.ToString(DateTimeFormat)} =================={ex.ToMessage()}";
            var ExFileName = "UnhandledException.txt";
            var ExFilePath = $"{CurrentExecutableDir}{ExFileName}";

            Clipboard.SetText(ExOutput);
            File.AppendAllText(ExFilePath, ExOutput);

            var _DialogResult = MessageX.Error($"程序出现意外错误，无法继续运行，非常抱歉给您带来不便，相关错误信息已写入到安装文件夹中的 {ExFileName} 文件和系统剪切板，建议您将相关信息并发送给软件开发者以便我们更好的定位并解决问题。或者您也可以点击 \"是\" 来重启应用程序，\"否\" 关闭应用程序{ex.ToMessage()}", Buttons: MessageBoxExButtons.YesNo);

            OpenInstallDir();
            Shutdown(Restart: _DialogResult == DialogResult.Yes);
        }

        public static void OpenInstallDir()
        {
            Process.Start(CurrentExecutableDir);
        }

        public static void Exit(ExitReason Reason)
        {
            Environment.Exit((int)Reason);
        }
    }
}