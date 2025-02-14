using System;
using System.Runtime.InteropServices;

namespace PlainCEETimer.Interop
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
    }
}
