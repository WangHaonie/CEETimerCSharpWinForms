using System;
using System.Drawing;
using System.Text.RegularExpressions;

namespace CEETimerCSharpWinForms.Modules
{
    public class ColorHelper
    {
        public static bool TryParseRGB(string Text, out Color TargetColor)
        {
            TargetColor = Color.Empty;

            if (Regex.IsMatch(Text, @"^\d{1,3},\d{1,3},\d{1,3}$"))
            {
                string[] RGB = Text.Split(',');
                int R = int.Parse(RGB[0]);
                int G = int.Parse(RGB[1]);
                int B = int.Parse(RGB[2]);

                if (!(R >= 0 && R <= 255 && G >= 0 && G <= 255 && B >= 0 && B <= 255))
                {
                    return false;
                }
                else
                {
                    TargetColor = Color.FromArgb(R, G, B);
                }
            }
            else
            {
                return false;
            }

            return true;
        }

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

            if (Contrast >= 1000 && ColorDifference < 300)
                return false;
            else if (Contrast >= 1800)
                return true;
            else if (ColorDifference < 400)
                return false;
            else if (Contrast < 2 && ColorDifference >= 400 && ColorDifference < 500)
                return false;
            else if (Contrast >= 2 && Contrast < 15 && ColorDifference >= 400 && ColorDifference < 500)
                return true;
            else
                return Contrast >= 2 && ColorDifference >= 500 && ColorDifference < 1500;
            #endregion
        }
    }
}
