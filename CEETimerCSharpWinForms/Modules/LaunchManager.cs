using CEETimerCSharpWinForms.Forms;
using System;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CEETimerCSharpWinForms.Modules
{
    public static class LaunchManager
    {
        public static bool IsAdmin { get; private set; }

        public const string AppName = "高考倒计时 by WangHaonie";
        public const string AppNameEn = "CEETimerCSharpWinForms";
        public const string AppVersion = "3.0.5";
        public const string AppBuildDate = "2024/06/13";
        public const string CopyrightInfo = "Copyright © 2023-2024 WangHaonie";
        public const string OriginalFileName = $"{AppNameEn}.exe";
        public const string InfoMsg = "提示 - 高考倒计时";
        public const string WarnMsg = "警告 - 高考倒计时";
        public const string ErrMsg = "错误 - 高考倒计时";
        public const string DateTimeFormat = "yyyy-MM-dd dddd HH:mm:ss";
        public const string RequestUA = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/125.0.0.0 Safari/537.36";

        public static readonly string CurrentExecutablePath = AppDomain.CurrentDomain.BaseDirectory;
        public static readonly string CurrentExecutable = Application.ExecutablePath;

        private static readonly string PipeName = $"{AppNameEn}_[34c14833-98da-49f7-a2ab-369e88e73b95]";
        private static readonly string CurrentExecutableName = Path.GetFileName(CurrentExecutable);

        public static void StartProgram(string[] args)
        {
            var Args = Array.ConvertAll(args, x => x.ToLower());
            var AllArgs = string.Join(" ", args);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += Application_ThreadException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            using var MutexMain = new Mutex(true, $"{AppNameEn}_MUTEX_61c0097d-3682-421c-84e6-70ca37dc31dd_[A3F8B92E6D14]", out bool IsNewProcess);

            if (IsNewProcess)
            {
                new Thread(StartPipeServer).Start();

                if (!CurrentExecutableName.Equals(OriginalFileName, StringComparison.OrdinalIgnoreCase))
                {
                    MessageX.Popup($"为了您的使用体验，请不要更改程序文件名! 程序将在该消息框自动关闭后尝试自动恢复到原文件名，若自动恢复失败请手动改回。\n\n当前文件名：{CurrentExecutableName}\n原始文件名：{OriginalFileName}", MessageLevel.Error, Position: FormStartPosition.CenterScreen, AutoClose: true);
                    ProcessHelper.RunProcess("cmd.exe", $"/c ren \"{CurrentExecutable}\" {OriginalFileName} & start \"\" \"{CurrentExecutablePath}{OriginalFileName}\" {AllArgs}");
                    Environment.Exit(4);
                }
                else
                {
                    if (Args.Length == 0)
                    {
                        Task.Run(() => CheckAdmin(out _));
                        Application.Run(new FormMain());
                    }
                    else
                    {
                        switch (Args[0])
                        {
                            case "/?":
                            case "/h":
                                MessageX.Popup("可用的命令行参数：\n\n/h    显示此帮助信息；\n/ac  检测当前用户是否具有管理员权限；\n/fr <版本号>\n        强制下载并安装指定的版本，留空则当前版本，\n        推荐在特殊情况下使用，不支持老版本。", MessageLevel.Info);
                                break;
                            case "/ac":
                                CheckAdmin(out string UserName, true);
                                MessageX.Popup($"当前用户 {UserName} {(IsAdmin ? "" : "不")}具有管理员权限。", MessageLevel.Info);
                                break;
                            case "/fr":
                                if (Args.Length > 1) FormDownloader.ManualVersion = Args[1];
                                Application.Run(new FormDownloader());
                                break;
                            default:
                                MessageX.Popup($"无法解析的命令行参数：\n{AllArgs}", MessageLevel.Error, AutoClose: true);
                                break;
                        }
                    }
                    Environment.Exit(0);
                }
            }
            else
            {
                if (Args.Length != 0)
                {
                    MessageX.Popup("请先关闭已打开的实例再使用命令行功能。", MessageLevel.Error, AutoClose: true);
                }

                StartPipeClient();
                Environment.Exit(1);
            }
        }

        public static void Shutdown(bool Restart = false)
        {
            ProcessHelper.RunProcess("cmd.exe", $"/c taskkill /f /fi \"PID eq {Process.GetCurrentProcess().Id}\" /im {CurrentExecutableName} {(Restart ? $"& start \"\" \"{CurrentExecutable}\"" : "")}");
            Environment.Exit(255);
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
                    UIHelper.ShowLastForm();
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

        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            HandleException(e.Exception);
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            HandleException((Exception)e.ExceptionObject);
        }

        private static void HandleException(Exception ex)
        {
            var ExOutput = string.Format(@"
╭───────────────────────────────────────────────────╮
日期和时间：{0}{1}
╰───────────────────────────────────────────────────╯
", DateTime.Now.ToString(DateTimeFormat), ex.ToMessage());

            var ExFileName = "UnhandledException.txt";
            var ExFilePath = $"{CurrentExecutablePath}{ExFileName}";

            Clipboard.SetText(ExOutput);
            File.AppendAllText(ExFilePath, ExOutput);

            var _DialogResult = MessageX.Popup($"程序出现意外错误，无法继续运行，非常抱歉给您带来不便，相关错误信息已写入到安装文件夹中的 {ExFileName} 文件和系统剪切板，建议您将相关信息并发送给软件开发者以便我们更好的定位并解决问题。或者您也可以点击 \"是\" 来重启应用程序，\"否\" 关闭应用程序{ex.ToMessage()}", MessageLevel.Error, Buttons: MessageBoxExButtons.YesNo);
            OpenDir();
            Shutdown(Restart: _DialogResult == DialogResult.Yes);
        }

        public static void OpenDir()
        {
            Process.Start(CurrentExecutablePath);
        }
    }
}