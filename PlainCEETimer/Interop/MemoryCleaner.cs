using System.Runtime.InteropServices;

namespace PlainCEETimer.Interop
{
    public static class MemoryCleaner
    {
        [DllImport("PlainCEETimer.Natives.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern void CleanMemory(int threshold);
    }
}
