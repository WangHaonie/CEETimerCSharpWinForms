using System.Runtime.InteropServices;

namespace PlainCEETimer.Interop
{
    public static class MemoryCleaner
    {
        #region 来自网络
        /*
        
        通过清空工作集来实现减少内存占用参考:

        .NET EXE memory footprint - Stack Overflow
        https://stackoverflow.com/a/223300/21094697

         */

        [DllImport("PlainCEETimer.Natives.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern void CleanMemory(int threshold);

        #endregion
    }
}
