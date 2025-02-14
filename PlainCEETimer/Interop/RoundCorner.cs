using System;
using System.Runtime.InteropServices;

namespace PlainCEETimer.Interop
{
    public static class RoundCorner
    {
        #region 来自网络
        /*
        
        WinForms 无边框窗体自动圆角 参考:

        Rounded Corners in C# windows forms - Stack Overflow
        https://stackoverflow.com/a/18822204/21094697

        Apply rounded corners in desktop apps - Windows apps | Microsoft Learn
        https://learn.microsoft.com/en-us/windows/apps/desktop/modernize/ui/apply-rounded-corners#example-3---rounding-an-apps-main-window-in-c

        */

        [DllImport("PlainCEETimer.Natives.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern void SetRoundCornerModern(IntPtr hWnd);

        [DllImport("PlainCEETimer.Natives.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern void SetRoundCornerRegion(IntPtr hWnd, int nRightRect, int nBottomRect, int radius);
        #endregion
    }
}
