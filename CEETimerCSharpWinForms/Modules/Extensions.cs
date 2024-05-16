using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace CEETimerCSharpWinForms.Modules
{
    public static class Extensions
    {
        public static string FormatLog(this string UpdateLog, string LatestVersion)
            => Regex.Replace(UpdateLog.RemoveIllegalChars(), @"[#\>]", "").Replace($"v{LatestVersion}更新日志新功能修复移除", "").Replace("+", "\n● ");

        public static bool IsVersionNumber(this string VersionNumber)
            => Regex.IsMatch(VersionNumber, @"^\d+(\.\d+){1,3}$");

        public static double ToLuminance(this Color _Color)
            => _Color.R * 0.299 + _Color.G * 0.587 + _Color.B * 0.114;

        public static int WithDpi(this int Pixel, Form _Form)
        {
            var _Graphics = _Form.CreateGraphics();
            var _Pixel = (int)(Pixel * (_Graphics.DpiX / 96));
            _Graphics.Dispose();
            return _Pixel;
        }

        #region 来自网络
        /*

        移除字符串里不可见的空格 (Unicode 控制字符) 参考：

        c# - Removing hidden characters from within strings - Stack Overflow
        https://stackoverflow.com/a/40888424/21094697

        */
        public static string RemoveIllegalChars(this string Text)
            => new(Text.Trim().Replace(" ", "").Where(c => char.IsLetterOrDigit(c) || (c >= ' ' && c <= byte.MaxValue)).ToArray());
        #endregion
    }
}