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
        public static void Optimize()
        {
            EmptyWorkingSet(Process.GetCurrentProcess().Handle);
        }
        #endregion
        /*public static bool IsMemoryExceeded()
        {
            long currentBytes = Process.GetCurrentProcess().;
            return currentBytes > 7340032;
        }*/
    }
}
