using System;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CEETimerCSharpWinForms.Modules
{
    public class LaunchManager
    {
        public const string AppVersion = "2.4";
        public const string AppVersionText = $"版本 v{AppVersion} (x64) (于 2024-02-03 编译)";
        public const string InfoMsg = "提示 - 高考倒计时";
        public const string WarnMsg = "警告 - 高考倒计时";
        public const string ErrMsg = "错误 - 高考倒计时";
        public const string AppName = "高考倒计时 by WangHaonie";
        public const string CopyrightInfo = "Copyright © 2023-2024 WangHaonie";
        public const string RequestUa = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/121.0.0.0 Safari/537.36 Edg/121.0.0.0";
        public static readonly string CurrentExecutablePath = AppDomain.CurrentDomain.BaseDirectory;
        public static readonly string CurrentExecutable = Application.ExecutablePath;
        private static readonly string CurrentExecutableName = Path.GetFileName(CurrentExecutable);
        private static readonly string OriginalFileName = "CEETimerCSharpWinForms.exe";
        private static readonly string Dll = $"{CurrentExecutablePath}Newtonsoft.Json.dll";
        private const string DllHash = "E1E27AF7B07EEEDF5CE71A9255F0422816A6FC5849A483C6714E1B472044FA9D";
        public static void Restart()
        {
            ProcessStartInfo processStartInfo = new()
            {
                FileName = @"cmd.exe",
                Arguments = $"/c taskkill /f /fi \"PID eq {Process.GetCurrentProcess().Id}\" /im {CurrentExecutableName} & start \"\" \"{CurrentExecutable}\"",
                CreateNoWindow = true,
                WindowStyle = ProcessWindowStyle.Hidden
            };
            Process.Start(processStartInfo);
        }
        public static void Shutdown()
        {
            ProcessStartInfo processStartInfo = new()
            {
                FileName = @"cmd.exe",
                Arguments = $"/c taskkill /f /fi \"PID eq {Process.GetCurrentProcess().Id}\" /im {CurrentExecutableName}",
                CreateNoWindow = true,
                WindowStyle = ProcessWindowStyle.Hidden
            };
            Process.Start(processStartInfo);
        }
        public static void CheckEnv()
        {
            if (!File.Exists($"{Dll}"))
            {
                MessageBox.Show("由于找不到 Newtonsoft.Json.dll, 无法继续执行代码。重新安装程序可能会解决此问题。", $"{CurrentExecutableName} - 系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(5);
            }
            else if (!BitConverter.ToString(SHA256.Create().ComputeHash(File.OpenRead($"{Dll}"))).Replace("-", "").Equals(DllHash, StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show($"{Dll} 没有被指定在 Windows 上运行，或者它包含错误。请尝试使用原始安装介质重新安装程序，或联系你的系统管理员或软件供应商以获取支持。错误状态 Unknown", $"{CurrentExecutableName} - 损坏的映像", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(5);
            }
            else if (!CurrentExecutableName.Equals(OriginalFileName, StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show($"为了您的使用体验，请不要更改程序文件名！程序将在该对话框关闭后尝试自动恢复到原文件名，若自动恢复失败请手动改回。\n\n当前文件名：{CurrentExecutableName}\n原始文件名：{OriginalFileName}", $"{ErrMsg}", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ProcessStartInfo processStartInfo = new()
                {
                    FileName = @"cmd.exe",
                    Arguments = $"/c ren \"{CurrentExecutable}\" {OriginalFileName} & start \"\" \"{CurrentExecutablePath}{OriginalFileName}\"",
                    CreateNoWindow = true,
                    WindowStyle = ProcessWindowStyle.Hidden
                };
                Process.Start(processStartInfo);
                Environment.Exit(3);
            }
            else
            {
                Task.Run(() => CheckForUpdate.Start(true));
                Application.Run(new CEETimerCSharpWinForms());
            }
        }
        public static void StartProgram()
        {
            using Mutex mutex = new(true, $"CEETimerCSharpWinForms_MUTEX_61c0097d-3682-421c-84e6-70ca37dc31dd_[A3F8B92E6D14]", out bool isNewProcess);
            if (isNewProcess)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                CheckEnv();
            }
            else
            {
                Environment.Exit(3);
            }
        }
    }
}
