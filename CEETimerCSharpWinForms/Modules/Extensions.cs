using System.Linq;
using System.Text.RegularExpressions;

namespace CEETimerCSharpWinForms.Modules
{
    public static class Extensions
    {
        public static string RemoveAllBadChars(this string Text)
        {
            #region 来自网络
            /*
            
            移除字符串里不可见的空格 (Unicode 控制字符) 参考：

            c# - Removing hidden characters from within strings - Stack Overflow
            https://stackoverflow.com/a/40888424/21094697

            */
            return new string(Text.Trim().Replace(" ", "").Where(c => char.IsLetterOrDigit(c) || (c >= ' ' && c <= byte.MaxValue)).ToArray());
            #endregion
        }

        public static string RemoveInvalidLogChars(this string UpdateLog, string LatestVersion)
        {
            return Regex.Replace(UpdateLog.RemoveAllBadChars(), @"[#\>]", "").Replace($"v{LatestVersion}更新日志新功能修复移除", "").Replace("+","\n# ");
        }
    }
}
