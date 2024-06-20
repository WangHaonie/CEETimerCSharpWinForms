using System;
using System.Collections.Generic;
using System.Drawing;

namespace CEETimerCSharpWinForms.Modules
{
    public static class ColorRulesHelper
    {
        public class Config(string typeIndex, string tick, string fore, string back)
        {
            public string Type { get; set; } = typeIndex;
            public string Tick { get; set; } = tick;
            public string Fore { get; set; } = fore;
            public string Back { get; set; } = back;
        }

        public static char[] TsSeparator => ['天', '时', '分', '秒'];
        public static TimeSpan GetExamTick(string str) => GetExamTickInternal(str, TsSeparator);
        public static TimeSpan GetExamTickFormRaw(string str) => GetExamTickInternal(str, [',']);
        public static string GetExamTickText(TimeSpan timeSpan)
            => $"{timeSpan.Days}{TsSeparator[0]}{timeSpan.Hours}{TsSeparator[1]}{timeSpan.Minutes}{TsSeparator[2]}{timeSpan.Seconds}{TsSeparator[3]}";
        public static string GetRawExamTick(TimeSpan timeSpan)
            => $"{timeSpan.Days},{timeSpan.Hours},{timeSpan.Minutes},{timeSpan.Seconds}";

        public static int GetRuleTypeIndex(string s) => s switch
        {
            Placeholders.PH_START => 0,
            Placeholders.PH_LEFT => 1,
            Placeholders.PH_PAST => 2,
            _ => 0
        };

        public static int GetRuleTypeIndexFromRaw(string s) => s switch
        {
            "0" => 0,
            "1" => 1,
            "2" => 2,
            _ => ConfigPolicy.NotAllowed<int>()
        };

        public static string GetRuleTypeText(int i) => i switch
        {
            0 => Placeholders.PH_START,
            1 => Placeholders.PH_LEFT,
            2 => Placeholders.PH_PAST,
            _ => Placeholders.PH_START
        };


        public static List<Config> Format(List<PairItems<PairItems<int, TimeSpan>, PairItems<Color, Color>>> Rules)
        {
            var tmp = new List<Config>();

            foreach (var Rule in Rules)
            {
                var Part1 = Rule.Item1;
                var Part2 = Rule.Item2;
                tmp.Add(new($"{Part1.Item1}", GetRawExamTick(Part1.Item2), Part2.Item1.ToRgb(), Part2.Item2.ToRgb()));
            }

            return tmp;
        }

        public static List<PairItems<PairItems<int, TimeSpan>, PairItems<Color, Color>>> GetColorRules(List<Config> cfg)
        {
            var tmp = new List<PairItems<PairItems<int, TimeSpan>, PairItems<Color, Color>>>();

            foreach (var Rule in cfg)
            {
                var fore = ColorHelper.GetColor(Rule.Fore);
                var back = ColorHelper.GetColor(Rule.Back);

                if (!ColorHelper.IsNiceContrast(fore, back))
                {
                    ConfigPolicy.NotAllowed<Color>();
                }

                var part1 = new PairItems<int, TimeSpan>(GetRuleTypeIndexFromRaw(Rule.Type), GetExamTickFormRaw(Rule.Tick));
                var part2 = new PairItems<Color, Color>(fore, back);
                tmp.Add(new(part1, part2));
            }

            return tmp;
        }

        private static TimeSpan GetExamTickInternal(string str, char[] Separator)
        {
            var _TimeSpan = str.Split(Separator);

            int d = int.Parse(_TimeSpan[0]);
            int h = int.Parse(_TimeSpan[1]);
            int m = int.Parse(_TimeSpan[2]);
            int s = int.Parse(_TimeSpan[3]);

            var ts = new TimeSpan(d, h, m, s);

            if (ts < ConfigPolicy.TsMinAllowed || ts > ConfigPolicy.TsMaxAllowed)
            {
                ConfigPolicy.NotAllowed<TimeSpan>();
            }

            return ts;
        }
    }
}
