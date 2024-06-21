using System;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;

namespace CEETimerCSharpWinForms.Modules
{
    public static class ColorHelper
    {
        public static bool TryParseRGB(string s, out Color color)
        {
            color = Color.Empty;

            if (Regex.IsMatch(s, @"^\d{1,3},\d{1,3},\d{1,3}$"))
            {
                color = GetColor(s);
            }
            else
            {
                return false;
            }

            return true;
        }

        public static Color GetColor(string rgb)
        {
            string[] RGB = rgb.Split(',');
            int R = int.Parse(RGB[0]);
            int G = int.Parse(RGB[1]);
            int B = int.Parse(RGB[2]);

            if (!(R.IsRGB() && G.IsRGB() && B.IsRGB()))
            {
                return Color.Empty;
            }
            else
            {
                return Color.FromArgb(R, G, B);
            }
        }

        public static string GetStringFromArgbArray(int[] arr)
        {
            return string.Join(",", arr);
        }

        public static int[] GetArgbArray(string s)
        {
            try
            {
                var input = s.Split(',');
                var arr = new int[16];

                for (int i = 0; i < Math.Min(input.Length, 16); i++)
                {
                    if (!int.TryParse(input[i], out int tmp))
                    {
                        ConfigPolicy.NotAllowed<int[]>();
                    }

                    arr[i] = tmp;
                }

                for (int i = input.Length; i < 16; i++)
                {
                    arr[i] = 16777215;
                }

                return arr;
            }
            catch
            {
                return [.. Enumerable.Repeat(16777215, 16)];
            }
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

            double ForeLuminance = Fore.ToLuminance();
            double BackLuminance = Back.ToLuminance();
            double Contrast = (Math.Max(ForeLuminance, BackLuminance) + 0.05) / (Math.Min(ForeLuminance, BackLuminance) + 0.05);
            double Difference = Math.Sqrt(2 * Math.Pow(Fore.R - Back.R, 2) + 4 * Math.Pow(Fore.G - Back.G, 2) + 3 * Math.Pow(Fore.B - Back.B, 2));
#if DEBUG
            Console.WriteLine($"{Contrast} <-> {Difference}");
#endif

            if (Contrast >= 1000 && Difference < 320)
            {
#if DEBUG
                Console.WriteLine("1 X");
#endif
                return false;
            }
            else if (Contrast >= 1000 && Difference > 360 && Difference < 400)
            {
#if DEBUG
                Console.WriteLine("2");
#endif
                return true;
            }
            else if (Contrast >= 1.6 && Contrast < 2 && Difference >= 500 && Difference < 535)
            {
#if DEBUG
                Console.WriteLine("17");
#endif
                return true;
            }
            else if (Contrast < 2 && Difference >= 400 && Difference < 500)
            {
#if DEBUG
                Console.WriteLine("3 X");
#endif
                return false;
            }
            else if (Contrast > 2.8 && Contrast < 3 && Difference > 400 && Difference < 420)
            {
#if DEBUG
                Console.WriteLine("10 X");
#endif
                return false;
            }
            else if (Contrast >= 2.9 && Contrast < 3 && Difference >= 500 && Difference < 520)
            {
#if DEBUG
                Console.WriteLine("14");
#endif
                return true;
            }
            else if (Contrast >= 2 && Contrast < 2.5 && Difference >= 440 && Difference < 450)
            {
#if DEBUG
                Console.WriteLine("16");
#endif
                return true;
            }
            else if (Contrast >= 2 && Contrast < 3 && Difference >= 400 && Difference < 520)
            {
#if DEBUG
                Console.WriteLine("5 X");
#endif
                return false;
            }
            else if (Contrast > 2 && Contrast < 2.3 && Difference > 338 && Difference < 340)
            {
#if DEBUG
                Console.WriteLine("11 X");
#endif
                return false;
            }
            else if (Contrast > 1.6 && Contrast < 2.3 && Difference > 250 && Difference < 420)
            {
#if DEBUG
                Console.WriteLine("12 X");
#endif
                return false;
            }
            else if (Contrast > 2 && Contrast < 2.5 && Difference > 280 && Difference < 300)
            {
#if DEBUG
                Console.WriteLine("15 X");
#endif
                return false;
            }
            else if (Contrast > 1.5 && Contrast < 2.5 && Difference > 250 && Difference < 340)
            {
#if DEBUG
                Console.WriteLine("6");
#endif
                return true;
            }
            else if (Contrast >= 3 && Contrast < 5.6 && Difference >= 400 && Difference < 472)
            {
#if DEBUG
                Console.WriteLine("14 X");
#endif
                return false;
            }
            else if (Contrast >= 2 && Contrast < 15 && Difference >= 400 && Difference < 500)
            {
#if DEBUG
                Console.WriteLine("4");
#endif
                return true;
            }
            else if (Contrast > 2.5 && Contrast < 2.7 && Difference > 550 && Difference < 580)
            {
#if DEBUG
                Console.WriteLine("12 X");
#endif
                return false;
            }
            else if (Contrast >= 1800)
            {
#if DEBUG
                Console.WriteLine("7");
#endif
                return true;
            }
            else if (Difference < 400)
            {
#if DEBUG
                Console.WriteLine("8 X");
#endif
                return false;
            }
            else
            {
#if DEBUG
                Console.WriteLine("9 I");
#endif
                return Contrast >= 2 && Difference >= 500 && Difference < 1500;
            }
            #endregion
        }
    }
}
