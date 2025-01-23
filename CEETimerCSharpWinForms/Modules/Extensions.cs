using CEETimerCSharpWinForms.Controls;
using System;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace CEETimerCSharpWinForms.Modules
{
    public static class Extensions
    {
        public static double DpiRatio { get; private set; } = 0;
        public static bool IsRGB(this int i) => i >= 0 && i <= 255;
        public static bool IsVersionNumber(this string v) => Regex.IsMatch(v, @"^\d+(\.\d+){1,3}$");
        public static bool IsValid(this DateTime dateTime) => dateTime >= new DateTime(1753, 1, 1, 0, 0, 0) || dateTime <= new DateTime(9998, 12, 31, 23, 59, 59);
        public static double ToLuminance(this Color color) => color.R * 0.299 + color.G * 0.587 + color.B * 0.114;
        public static string ToRgb(this Color color) => $"{color.R},{color.G},{color.B}";
        public static string ToMessage(this Exception ex) => $"\n\n错误信息: \n{ex.Message}\n\n错误详情: \n{ex}";
        public static string FormatLog(this string updateLog, string latestVersion)
            => $"{Regex.Replace(updateLog.RemoveIllegalChars(), @"[#\>]", "").Replace($"v{latestVersion}更新日志新功能修复移除", "").Replace("+", "\n● ")}";

        #region 来自网络
        /*

        移除字符串里不可见的空格 (Unicode 控制字符) 参考:

        c# - Removing hidden characters from within strings - Stack Overflow
        https://stackoverflow.com/a/40888424/21094697

        */
        public static string RemoveIllegalChars(this string s)
            => new(s.Trim().Replace(" ", "").Where(c => char.IsLetterOrDigit(c) || (c >= ' ' && c <= byte.MaxValue)).Where(x => !ConfigPolicy.CharsNotAllowed.Contains(x)).ToArray());
        #endregion

        public static int WithDpi(this int px, Control control)
        {
            Graphics g = null;
            int pxScaled;

            if (DpiRatio == 0)
            {
                g = control.CreateGraphics();
                DpiRatio = g.DpiX / 96;
            }

            pxScaled = (int)(px * DpiRatio);
            g?.Dispose();
            return pxScaled;
        }

        public static void ReActivate(this AppForm form)
        {
            form.WindowState = FormWindowState.Normal;
            form.Show();
            form.Activate();
        }
    }
}