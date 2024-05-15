using CEETimerCSharpWinForms.Forms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CEETimerCSharpWinForms.Modules
{
    public static class LaunchManager
    {
        public static bool IsAdmin { get; private set; }
        public static string CurrentLatest { get; set; }

        public const string AppVersion = "3.0.3";
        public const string AppVersionText = $"版本 v{AppVersion} x64 (2024/05/02)";
        public const string InfoMsg = "提示 - 高考倒计时";
        public const string WarnMsg = "警告 - 高考倒计时";
        public const string ErrMsg = "错误 - 高考倒计时";
        public const string AppName = "高考倒计时 by WangHaonie";
        public const string CopyrightInfo = "Copyright © 2023-2024 WangHaonie";
        public const string RequestUA = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/125.0.0.0 Safari/537.36";
        public const string OriginalFileName = "CEETimerCSharpWinForms.exe";
        public const string GitHubAPI = "https://api.github.com/repos/WangHaonie/CEETimerCSharpWinForms/releases/latest";

        public static readonly bool IsWindows10Above = Environment.OSVersion.Version.Major >= 10;
        public static readonly string CurrentExecutablePath = AppDomain.CurrentDomain.BaseDirectory;
        public static readonly string CurrentExecutable = Application.ExecutablePath;

        private const string PipeName = "CEETimerCSharpWinForms_[34c14833-98da-49f7-a2ab-369e88e73b95]";
        private static readonly string CurrentExecutableName = Path.GetFileName(CurrentExecutable);

        public static void StartProgram(string[] Args)
        {
            var _Args = Array.ConvertAll(Args, x => x.ToLower());
            var AllArgs = string.Join(" ", Args);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            using var MutexMain = new Mutex(true, "CEETimerCSharpWinForms_[61c0097d-3682-421c-84e6-70ca37dc31dd]", out bool IsNewProcess);

            if (IsNewProcess)
            {
                new Thread(StartPipeServer).Start();

                var DllHashes = new Dictionary<string, string>()
                {
                    { "Newtonsoft.Json.dll", "E1E27AF7B07EEEDF5CE71A9255F0422816A6FC5849A483C6714E1B472044FA9D" }
                };

                foreach (var Dll in DllHashes)
                {
                    string DllPath = $"{CurrentExecutablePath}{Dll.Key}";
                    string DllName = Dll.Key;
                    string DllHash = Dll.Value;

                    if (!File.Exists(DllPath))
                    {
                        MessageBox.Show($"由于找不到 {DllName}, 无法继续执行代码。重新安装程序可能会解决此问题。", $"{CurrentExecutableName} - 系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Environment.Exit(2);
                    }
                    else
                    {
                        using FileStream fs = File.OpenRead(DllPath);
                        byte[] HashBytes = SHA256.Create().ComputeHash(fs);
                        string CurrentHash = BitConverter.ToString(HashBytes).Replace("-", "");

                        if (!string.Equals(CurrentHash, DllHash, StringComparison.OrdinalIgnoreCase))
                        {
                            MessageBox.Show($"{DllPath} 没有被指定在 Windows 上运行，或者它包含错误。请尝试使用原始安装介质重新安装程序，或联系你的系统管理员或软件供应商以获取支持。错误状态 0x00000003", $"{CurrentExecutableName} - 损坏的映像", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Environment.Exit(3);
                        }
                    }
                }

                if (!CurrentExecutableName.Equals(OriginalFileName, StringComparison.OrdinalIgnoreCase))
                {
                    MessageX.Popup($"为了您的使用体验，请不要更改程序文件名! \n程序将在该消息框自动关闭后尝试自动恢复到原文件名，若自动恢复失败请手动改回。\n\n当前文件名：{CurrentExecutableName}\n原始文件名：{OriginalFileName}", MessageLevel.Error, Position: FormStartPosition.CenterScreen, AutoClose: true);
                    ProcessHelper.RunProcess("cmd.exe", $"/c ren \"{CurrentExecutable}\" {OriginalFileName} & start \"\" \"{CurrentExecutablePath}{OriginalFileName}\" {AllArgs}");
                    Environment.Exit(4);
                }
                else
                {
                    if (_Args.Length == 0)
                    {
                        Task.Run(() => CheckAdmin(out _));
                        Application.Run(new FormMain());
                    }
                    else
                    {
                        switch (_Args[0])
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
                                if (_Args.Length > 1) FormDownloader.ManualVersion = _Args[1];
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
                if (_Args.Length != 0)
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
        }

        public static void CheckAdmin(out string UserName, bool QueryUserName = false)
        {
            var AdminChecker = ProcessHelper.RunProcess("cmd.exe", $"/c {(QueryUserName ? "whoami & " : "")}net session", RedirectOutput: QueryUserName);
            AdminChecker.WaitForExit();
            var Output = AdminChecker.StandardOutput.ReadToEnd();
            IsAdmin = AdminChecker.ExitCode == 0;
            UserName = Output.Split(new[] { "\r\n" }, StringSplitOptions.None)[0];
        }

        private static void StartPipeServer()
        {
            try
            {
                while (true)
                {
                    using var PipeServer = new NamedPipeServerStream(PipeName, PipeDirection.InOut);
                    PipeServer.WaitForConnection();
                    FormManager.ShowLastOpenedForm();
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
    }
}