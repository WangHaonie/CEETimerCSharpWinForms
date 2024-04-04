using System;
using System.Drawing;

namespace CEETimerCSharpWinForms.Modules
{
    public class ColorHelper
    {
        public static bool IsNiceContrast(Color Fore, Color Back)
        {
            #region 来自网络
            /*
            
            RGB 亮度计算公式 参考：

            image - Formula to determine perceived brightness of RGB color - Stack Overflow
            https://stackoverflow.com/a/596243/21094697

            Web Content Accessibility Guidelines (WCAG) 2.1
            https://www.w3.org/TR/WCAG21/#dfn-contrast-ratio

            Building your own color contrast checker - DEV Community
            https://dev.to/alvaromontoro/building-your-own-color-contrast-checker-4j7o

            颜色差异 - 维基百科，自由的百科全书
            https://zh.wikipedia.org/wiki/%E9%A2%9C%E8%89%B2%E5%B7%AE%E5%BC%82#%E6%AC%A7%E6%B0%8F%E8%B7%9D%E7%A6%BB
            
            */
            // double ForeLuminance = Fore.R * 0.2126 + Fore.G * 0.7152 + Fore.B * 0.0722;
            // double BackLuminance = Back.R * 0.2126 + Back.G * 0.7152 + Back.B * 0.0722;

            double ForeLuminance = Fore.R * 0.299 + Fore.G * 0.587 + Fore.B * 0.114;
            double BackLuminance = Back.R * 0.299 + Back.G * 0.587 + Back.B * 0.114;
            double Contrast = (Math.Max(ForeLuminance, BackLuminance) + 0.05) / (Math.Min(ForeLuminance, BackLuminance) + 0.05);
            double ColorDifference = Math.Sqrt(2 * Math.Pow(Fore.R - Back.R, 2) + 4 * Math.Pow(Fore.G - Back.G, 2) + 3 * Math.Pow(Fore.B - Back.B, 2));

            return Contrast >= 3 && ColorDifference > 500;
            #endregion
        }
    }
}
