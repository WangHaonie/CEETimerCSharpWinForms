using PlainCEETimer.Modules;
using System;
using System.Runtime.InteropServices;

namespace PlainCEETimer.Interop
{
    public static class TaskbarProgress
    {
        private static readonly bool IsWindows7Above;

        static TaskbarProgress()
        {
            IsWindows7Above = AppLauncher.IsWindows7Above;
        }

        [DllImport("PlainCEETimer.Natives.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void InitilizeTaskbarList(IntPtr hWnd);

        [DllImport("PlainCEETimer.Natives.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void SetTaskbarListState(int tbpFlags);

        [DllImport("PlainCEETimer.Natives.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void SetTaskbarListProgress(ulong ullCompleted, ulong ullTotal);

        [DllImport("PlainCEETimer.Natives.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void ReleaseTaskbarList();

        public static void InitilizeTaskbarListEx(IntPtr hWnd)
        {
            if (IsWindows7Above)
            {
                InitilizeTaskbarList(hWnd);
            }
        }

        public static void SetTaskbarListStateEx(TaskbarProgressState State)
        {
            if (IsWindows7Above)
            {
                SetTaskbarListState((int)State);
            }
        }

        public static void SetTaskbarListProgressEx(ulong ullCompleted, ulong ullTotal)
        {
            if (IsWindows7Above)
            {
                SetTaskbarListProgress(ullCompleted, ullTotal);
            }
        }

        public static void ReleaseTaskbarListEx()
        {
            if (IsWindows7Above)
            {
                ReleaseTaskbarList();
            }
        }
    }
}
