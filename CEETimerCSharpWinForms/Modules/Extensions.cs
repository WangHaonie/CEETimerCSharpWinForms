using System.Linq;

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
    }
}
