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

        public static string RemoveInvalidLogChars(this string UpdateLog)
        {
            #region 来自网络
            /*
            
            移除字符串里所有的 Emoji 参考：

            c# - How do I remove emoji characters from a string? - Stack Overflow
            https://stackoverflow.com/a/28025891/21094697

            (可恶，为什么不能移除⛔啊，++)

            */
            return Regex.Replace(UpdateLog.Replace(">  🌈 新功能、🛠️ 修复、⛔ 移除\r\n\r\n", "").Replace("⛔", "").Replace("## ", "").Replace("+ ", "# ").Replace("；", ""), @"\p{Cs}", "");
            #endregion
        }
    }
}
