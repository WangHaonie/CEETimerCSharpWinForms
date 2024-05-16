using System;
using System.Runtime.InteropServices;

namespace CEETimerCSharpWinForms.Modules
{
    public static class WindowsAPI
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

        #region 来自网络
        /*
        
        提取 DLL 里的图标参考：

        How can I use the images within shell32.dll in my C# project? - Stack Overflow
        https://stackoverflow.com/a/6873026/21094697

        */
        [DllImport("shell32.dll", EntryPoint = "ExtractIconExW", CharSet = CharSet.Unicode, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern int ExtractIconEx(string sFile, int iIndex, out IntPtr piLargeVersion, out IntPtr piSmallVersion, int amountIcons);
        #endregion

        [DllImport("user32.dll")]
        public static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        #region 来自网络
        /*
        
        WinForms 无边框窗体自动圆角 参考：

        Rounded Corners in C# windows forms - Stack Overflow
        https://stackoverflow.com/a/18822204/21094697

        Apply rounded corners in desktop apps - Windows apps | Microsoft Learn
        https://learn.microsoft.com/en-us/windows/apps/desktop/modernize/apply-rounded-corners#example-2---rounding-an-apps-main-window-in-c---winforms

        */
        [DllImport("gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        public static extern IntPtr CreateRoundRectRgn(int x1, int y1, int x2, int y2, int w, int h);

        public enum DWM_WINDOW_CORNER_PREFERENCE
        {
            DWMWCP_DEFAULT = 0,
            DWMWCP_DONOTROUND = 1,
            DWMWCP_ROUND = 2,
            DWMWCP_ROUNDSMALL = 3
        }

        public enum DWMWINDOWATTRIBUTE
        {
            DWMWA_WINDOW_CORNER_PREFERENCE = 33
        }

        [DllImport("dwmapi.dll", CharSet = CharSet.Unicode, PreserveSig = false)]
        public static extern void DwmSetWindowAttribute(IntPtr hWnd, DWMWINDOWATTRIBUTE attribute, ref DWM_WINDOW_CORNER_PREFERENCE pvAttribute, uint cbAttribute);
        #endregion
    }
}
