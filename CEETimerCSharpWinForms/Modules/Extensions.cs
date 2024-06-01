﻿using System;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace CEETimerCSharpWinForms.Modules
{
    public static class Extensions
    {
        public static int DpiRatio { get; private set; } = 0;

        public static bool IsVersionNumber(this string v)
            => Regex.IsMatch(v, @"^\d+(\.\d+){1,3}$");

        public static double ToLuminance(this Color color)
            => color.R * 0.299 + color.G * 0.587 + color.B * 0.114;

        public static int WithDpi(this int px, Form form)
        {
            Graphics g = null;
            int pxScaled;

            if (DpiRatio == 0)
            {
                g = form.CreateGraphics();
                DpiRatio = (int)(g.DpiX / 96);
            }

            pxScaled = px * DpiRatio;
            g?.Dispose();
            return pxScaled;
        }

        public static string FormatLog(this string updateLog, string latestVersion)
            => Regex.Replace(updateLog.RemoveIllegalChars(), @"[#\>]", "").Replace($"v{latestVersion}更新日志新功能修复移除", "").Replace("+", "\n● ");

        public static string ToMessage(this Exception ex)
            => $"\n\n错误信息: \n{ex.Message}\n\n错误详情: \n{ex}";

        #region 来自网络
        /*

        移除字符串里不可见的空格 (Unicode 控制字符) 参考：

        c# - Removing hidden characters from within strings - Stack Overflow
        https://stackoverflow.com/a/40888424/21094697

        */
        public static string RemoveIllegalChars(this string s)
            => new(s.Trim().Replace(" ", "").Where(c => char.IsLetterOrDigit(c) || (c >= ' ' && c <= byte.MaxValue)).ToArray());
        #endregion

        public static void ReActivate(this Form form)
        {
            var IsTopMost = form.TopMost;
            form.TopMost = true;
            form.WindowState = FormWindowState.Normal;
            form.BringToFront();
            form.Activate();
            form.TopMost = IsTopMost;
        }
    }
}