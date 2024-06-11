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

        public const string StartHint = "还有";
        public const string LeftHint = "结束还有";
        public const string PastHint = "已过去了";

        public static char[] TimeSpanSeparator => ['天', '时', '分', '秒'];

        public static int GetRuleTypeIndex(string s) => s switch
        {
            StartHint => 0,
            LeftHint => 1,
            PastHint => 2,
            _ => 0
        };

        public static int GetRuleTypeIndexFromRaw(string s) => s switch
        {
            "0" => 0,
            "1" => 1,
            "2" => 2,
            _ => 0
        };

        public static string GetRuleTypeText(int i) => i switch
        {
            0 => StartHint,
            1 => LeftHint,
            2 => PastHint,
            _ => StartHint
        };

        public static TimeSpan GetExamTick(string str)
        {
            return GetExamTickInternal(str, TimeSpanSeparator);
        }

        public static TimeSpan GetExamTickFormRaw(string str)
        {
            return GetExamTickInternal(str, [',']);
        }

        private static TimeSpan GetExamTickInternal(string str, char[] Separator)
        {
            var _TimeSpan = str.Split(Separator);

            int d = int.Parse(_TimeSpan[0]);
            int h = int.Parse(_TimeSpan[1]);
            int m = int.Parse(_TimeSpan[2]);
            int s = int.Parse(_TimeSpan[3]);

            return new TimeSpan(d, h, m, s) + new TimeSpan(0, 0, 0, 1);
        }

        public static string GetExamTickText(TimeSpan timeSpan)
        {
            return $"{timeSpan.Days}{TimeSpanSeparator[0]}{timeSpan.Hours}{TimeSpanSeparator[1]}{timeSpan.Minutes}{TimeSpanSeparator[2]}{timeSpan.Seconds}{TimeSpanSeparator[3]}";
        }

        public static string GetRawExamTick(TimeSpan timeSpan)
        {
            return $"{timeSpan.Days},{timeSpan.Hours},{timeSpan.Minutes},{timeSpan.Seconds}";
        }

        public static List<Config> Format(List<PairItems<PairItems<int, TimeSpan>, PairItems<Color, Color>>> Rules)
        {
            var tmp = new List<Config>();

            foreach (var Rule in Rules)
            {
                var Part1 = Rule.Item1;
                var Part2 = Rule.Item2;
                tmp.Add(new($"{Part1.Item1}", GetRawExamTick(Part1.Item2 - new TimeSpan(0, 0, 0, 1)), Part2.Item1.ToRgb(), Part2.Item2.ToRgb()));
            }

            return tmp;
        }

        public static List<PairItems<PairItems<int, TimeSpan>, PairItems<Color, Color>>> GetColorRules(List<Config> cfg)
        {
            var tmp = new List<PairItems<PairItems<int, TimeSpan>, PairItems<Color, Color>>>();

            foreach (var Rule in cfg)
            {
                var part1 = new PairItems<int, TimeSpan>(GetRuleTypeIndexFromRaw(Rule.Type), GetExamTickFormRaw(Rule.Tick));
                var part2 = new PairItems<Color, Color>(ColorHelper.GetColor(Rule.Fore), ColorHelper.GetColor(Rule.Back));
                tmp.Add(new(part1, part2));
            }

            return tmp;
        }
    }
}
