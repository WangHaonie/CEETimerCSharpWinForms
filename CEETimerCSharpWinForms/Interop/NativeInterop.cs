using System;
using System.Runtime.InteropServices;

namespace CEETimerCSharpWinForms.Interop
{
    public static class NativeInterop
    {
        #region 来自网络
        /*
        
        通过清空工作集来实现减少内存占用参考:

        .NET EXE memory footprint - Stack Overflow
        https://stackoverflow.com/a/223300/21094697

         */
        [DllImport("psapi.dll")]
        public static extern int EmptyWorkingSet(IntPtr hwProc);
        #endregion

        #region 来自网络
        /*
        
        提取 DLL 里的图标参考:

        How can I use the images within shell32.dll in my C# project? - Stack Overflow
        https://stackoverflow.com/a/6873026/21094697

        */
        [DllImport("shell32.dll", EntryPoint = "ExtractIconExW", CharSet = CharSet.Unicode, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern int ExtractIconEx(string lpszFile, int nIconIndex, out IntPtr phiconLarge, out IntPtr phiconSmall, int nIcons);
        #endregion

        #region 来自网络
        /*
        
        WinForms 无边框窗体自动圆角 参考:

        Rounded Corners in C# windows forms - Stack Overflow
        https://stackoverflow.com/a/18822204/21094697

        Apply rounded corners in desktop apps - Windows apps | Microsoft Learn
        https://learn.microsoft.com/en-us/windows/apps/desktop/modernize/apply-rounded-corners#example-2---rounding-an-apps-main-window-in-c---winforms

        */
        [DllImport("gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        public static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);

        [DllImport("dwmapi.dll", CharSet = CharSet.Unicode, PreserveSig = false)]
        public static extern void DwmSetWindowAttribute(IntPtr hWnd, DWMWINDOWATTRIBUTE dwAttribute, ref DWM_WINDOW_CORNER_PREFERENCE pvAttribute, uint cbAttribute);
        #endregion

        [DllImport("gdi32.dll", EntryPoint = "DeleteObject")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DeleteObject(IntPtr hObject);
    }
}
