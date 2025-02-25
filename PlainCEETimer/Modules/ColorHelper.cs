using Newtonsoft.Json.Linq;
using PlainCEETimer.Modules.Configuration;
using System;
using System.Drawing;
using System.Text.RegularExpressions;

namespace PlainCEETimer.Modules
{
    public static class ColorHelper
    {
        public static bool TryParseRGB(string s, out Color color)
        {
            color = Color.Empty;

            if (Regex.IsMatch(s, @"^\d{1,3},\d{1,3},\d{1,3}$"))
            {
                color = GetColor(s);
                return true;
            }

            return false;
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

        public static Color GetColor(JObject Json, int Index) => Index switch
        {
            0 => GetColor(Json[nameof(ColorSetObject.Fore)].ToString()),
            _ => GetColor(Json[nameof(ColorSetObject.Back)].ToString())
        };

        public static bool IsNiceContrast(Color Fore, Color Back)
        {
            //
            // 亮度计算公式 & 对比度判断 参考:
            //
            // Guidance on Applying WCAG 2 to Non-Web Information and ...
            // https://www.w3.org/TR/wcag2ict/#dfn-relative-luminance
            // https://www.w3.org/TR/wcag2ict/#dfn-contrast-ratio
            //

            double L1 = GetRelativeLuminance(Fore);
            double L2 = GetRelativeLuminance(Back);

            if (L1 < L2)
            {
                (L1, L2) = (L2, L1);
            }

            return (L1 + 0.05) / (L2 + 0.05) >= 3;
        }

        private static double GetRelativeLuminance(Color color)
        {
            double RsRGB = color.R / 255.0;
            double GsRGB = color.G / 255.0;
            double BsRGB = color.B / 255.0;

            double R = RsRGB <= 0.03928 ? RsRGB / 12.92 : Math.Pow((RsRGB + 0.055) / 1.055, 2.4);
            double G = GsRGB <= 0.03928 ? GsRGB / 12.92 : Math.Pow((GsRGB + 0.055) / 1.055, 2.4);
            double B = BsRGB <= 0.03928 ? BsRGB / 12.92 : Math.Pow((BsRGB + 0.055) / 1.055, 2.4);

            return 0.2126 * R + 0.7152 * G + 0.0722 * B;
        }
    }
}
