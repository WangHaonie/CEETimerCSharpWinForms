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
        public static extern int EmptyWorkingSet(IntPtr hwProc);
        #endregion

        public static void OptimizeMemory()
        {
            Process ProcessGetCurrentMemory = Process.Start(new ProcessStartInfo
            {
                FileName = "tasklist.exe",
                Arguments = $"/fi \"PID eq {Process.GetCurrentProcess().Id}\" /fo csv /nh",
                WindowStyle = ProcessWindowStyle.Hidden,
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                UseShellExecute = false
            });

            var Output = ProcessGetCurrentMemory.StandardOutput.ReadToEnd().Trim().Split('"');
            ProcessGetCurrentMemory.WaitForExit();
            int MemoryUsage = int.Parse(Output[9].Replace(",", "").Replace("K", "").Trim());

            if (MemoryUsage > 12288)
            {
                EmptyWorkingSet(Process.GetCurrentProcess().Handle);
            }
        }
    }
}
