using System;
using System.Runtime.InteropServices;

namespace CEETimerCSharpWinForms.Modules
{
    public class WindowsAPI
    {
        /*
        
        C# 无边框窗体的拖动 参考：

        winforms - How to move c# form app without default border - Stack Overflow
        https://stackoverflow.com/a/50287357/21094697

         */
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        /*
        
        通过清空工作集来实现减少内存占用参考：

        .NET EXE memory footprint - Stack Overflow
        https://stackoverflow.com/a/223300/21094697

         */
        [DllImport("psapi.dll")]
        public static extern int EmptyWorkingSet(IntPtr hwProc);
    }
}
