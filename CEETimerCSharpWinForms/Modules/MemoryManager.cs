using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace CEETimerCSharpWinForms.Modules
{
    public class MemoryManager
    {
        #region 来自网络
        /*
        
        通过清空工作集来实现减少内存占用参考：

        .NET EXE memory footprint - Stack Overflow
        https://stackoverflow.com/a/223300/21094697

         */
        [DllImport("psapi.dll")]
        static extern int EmptyWorkingSet(IntPtr hwProc);
        #endregion
        public static void Optimize()
        {
            #region [当前使用] 获取工作集大小
            ProcessStartInfo processStartInfo = new()
            {
                FileName = @"tasklist.exe",
                Arguments = $"/fi \"PID eq {Process.GetCurrentProcess().Id}\" /fo csv /nh",
                WindowStyle = ProcessWindowStyle.Hidden,
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                UseShellExecute = false
            };
            Process ProcessGetCurrentMemory = Process.Start(processStartInfo);
            var Output = ProcessGetCurrentMemory.StandardOutput.ReadToEnd().Trim().Split('"');
            ProcessGetCurrentMemory.WaitForExit();
            int MemoryUsage = int.Parse(Output[9].Replace(",", "").Replace("K", "").Trim());
            // Console.WriteLine($"{Output[9]} :: {MemoryUsage}");
            if (MemoryUsage > 22000)
            {
                EmptyWorkingSet(Process.GetCurrentProcess().Handle);
            }
            #endregion
            #region [已弃用] 获取私有工作集 (任务管理器同款)
            /*
             * 这也太占内存了吧，一路狂飙到 70 MB
             * 
            PerformanceCounter performanceCounter = new("Process", "Working Set - Private", Process.GetCurrentProcess().ProcessName);
            long MemoryUsage = Convert.ToInt64(performanceCounter.NextValue());
            Console.WriteLine(MemoryUsage);
            */
            #endregion
        }
    }
}
