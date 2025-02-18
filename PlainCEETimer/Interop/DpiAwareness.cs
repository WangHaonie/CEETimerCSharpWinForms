//using System.Runtime.InteropServices;

//namespace PlainCEETimer.Interop
//{
//    public static class DpiAwareness
//    {
//        [DllImport("PlainCEETimer.Natives.dll", CallingConvention = CallingConvention.StdCall)]
//        public static extern void SetProcessDpiAwarenessEx(int windowsid);
//    }
//}

//public static bool IsWindows81Above => Environment.OSVersion.Version >= new Version(6, 3, 9600);
//public static bool IsWindows10Build1607Above => Environment.OSVersion.Version >= new Version(10, 0, 14393);

//int WindowsID;
//if (IsWindows10Build1607Above)
//{
//    WindowsID = 2;
//}
//else if (IsWindows81Above)
//{
//    WindowsID = 1;
//}
//else
//{
//    WindowsID = 0;
//}

//DpiAwareness.SetProcessDpiAwarenessEx(WindowsID);
