﻿using System;
using System.Runtime.InteropServices;

namespace PlainCEETimer.Interop
{
    public static class RoundCorner
    {
        [DllImport("PlainCEETimer.Natives.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern void SetRoundCornerModern(IntPtr hWnd);

        [DllImport("PlainCEETimer.Natives.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern void SetRoundCornerRegion(IntPtr hWnd, int nRightRect, int nBottomRect, int radius);
    }
}
