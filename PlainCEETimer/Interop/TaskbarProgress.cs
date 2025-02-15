using PlainCEETimer.Modules;
using System;
using System.Runtime.InteropServices;

namespace PlainCEETimer.Interop
{
    public class TaskbarProgress
    {
        private static readonly bool IsWindows7Above;

        public TaskbarProgress(IntPtr hWnd)
        {
            if (IsWindows7Above)
            {
                InitilizeTaskbarList(hWnd);
            }
        }

        static TaskbarProgress()
        {
            IsWindows7Above = AppLauncher.IsWindows7Above;
        }

        public void SetTaskbarListStateEx(TaskbarProgressState State)
        {
            if (IsWindows7Above)
            {
                SetTaskbarListState((int)State);
            }
        }

        public void SetTaskbarListProgressEx(ulong ullCompleted, ulong ullTotal)
        {
            if (IsWindows7Above)
            {
                SetTaskbarListProgress(ullCompleted, ullTotal);
            }
        }

        public void ReleaseTaskbarListEx()
        {
            if (IsWindows7Above)
            {
                ReleaseTaskbarList();
            }
        }

        #region
        /*
        
        实现任务栏图标上的进度条 参考：

        任务栏扩展 - Win32 apps | Microsoft Learn
        https://learn.microsoft.com/zh-cn/windows/win32/shell/taskbar-extensions#progress-bars

        ITaskbarList3 (shobjidl_core.h)  - Win32 apps | Microsoft Learn
        https://learn.microsoft.com/zh-cn/windows/win32/api/shobjidl_core/nn-shobjidl_core-itaskbarlist3

        ITaskbarList3：：SetProgressState (shobjidl_core.h)  - Win32 apps | Microsoft Learn
        https://learn.microsoft.com/zh-cn/windows/win32/api/shobjidl_core/nf-shobjidl_core-itaskbarlist3-setprogressstate

        ITaskbarList3：：SetProgressValue (shobjidl_core.h)  - Win32 apps | Microsoft Learn
        https://learn.microsoft.com/zh-cn/windows/win32/api/shobjidl_core/nf-shobjidl_core-itaskbarlist3-setprogressvalue

        ITaskbarList：：HrInit (shobjidl_core.h)  - Win32 apps | Microsoft Learn
        https://learn.microsoft.com/zh-cn/windows/win32/api/shobjidl_core/nf-shobjidl_core-itaskbarlist-hrinit

        IUnknown：：Release - Win32 apps | Microsoft Learn
        https://learn.microsoft.com/zh-cn/windows/win32/api/unknwn/nf-unknwn-iunknown-release

         */

        [DllImport("PlainCEETimer.Natives.dll", CallingConvention = CallingConvention.StdCall)]
        private static extern void InitilizeTaskbarList(IntPtr hWnd);

        [DllImport("PlainCEETimer.Natives.dll", CallingConvention = CallingConvention.StdCall)]
        private static extern void SetTaskbarListState(int tbpFlags);

        [DllImport("PlainCEETimer.Natives.dll", CallingConvention = CallingConvention.StdCall)]
        private static extern void SetTaskbarListProgress(ulong ullCompleted, ulong ullTotal);

        [DllImport("PlainCEETimer.Natives.dll", CallingConvention = CallingConvention.StdCall)]
        private static extern void ReleaseTaskbarList();
        #endregion
    }
}
