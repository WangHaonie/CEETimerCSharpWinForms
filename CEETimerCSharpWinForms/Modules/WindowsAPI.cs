using System;
using System.Runtime.InteropServices;

namespace CEETimerCSharpWinForms.Modules
{
    public class WindowsAPI
    {
        /*
        
        通过清空工作集来实现减少内存占用参考：

        .NET EXE memory footprint - Stack Overflow
        https://stackoverflow.com/a/223300/21094697

         */
        [DllImport("psapi.dll")]
        public static extern int EmptyWorkingSet(IntPtr hwProc);
    }
}
