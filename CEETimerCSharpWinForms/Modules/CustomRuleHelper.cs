using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;

namespace CEETimerCSharpWinForms.Modules
{
    public static class CustomRuleHelper
    {
        public class Config(string typeIndex, string tick, string fore, string back, string custom)
        {
            public string Type { get; set; } = typeIndex;
            public string Tick { get; set; } = tick;
            public string Fore { get; set; } = fore;
            public string Back { get; set; } = back;
            public string Text { get; set; } = custom;
        }

        private static readonly string[] AllPHs = [Placeholders.PH_EXAMNAME, Placeholders.PH_DAYS, Placeholders.PH_HOURS, Placeholders.PH_MINUTES, Placeholders.PH_SECONDS, Placeholders.PH_ROUNDEDDAYS, Placeholders.PH_TOTALHOURS, Placeholders.PH_TOTALMINUTES, Placeholders.PH_TOTALSECONDS];

        public static char[] TsSeparator => ['天', '时', '分', '秒'];
        public static TimeSpan GetExamTick(string str) => GetExamTickCore(str, TsSeparator);
        public static TimeSpan GetExamTickFormRaw(string str) => GetExamTickCore(str, [',']);
        public static string GetExamTickText(TimeSpan timeSpan)
            => $"{timeSpan.Days}{TsSeparator[0]}{timeSpan.Hours}{TsSeparator[1]}{timeSpan.Minutes}{TsSeparator[2]}{timeSpan.Seconds}{TsSeparator[3]}";
        public static string GetExamTickConfig(TimeSpan timeSpan)
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

        public static string GetCustomTextDefault(int Index, string[] Pref = null) => Index switch
        {
            0 => Pref[0] ?? Placeholders.PH_P1,
            1 => Pref[1] ?? Placeholders.PH_P2,
            _ => Pref[2] ?? Placeholders.PH_P3,
        };

        public static string[] GetCustomTextFormRaw(string p1, string p2, string p3)
        {
            string[] tmp = [p1, p2, p3];

            if ((bool)CheckCustomText(tmp, out _))
            {
                return tmp;
            }

            return [Placeholders.PH_P1, Placeholders.PH_P2, Placeholders.PH_P3];
        }

        /// <summary>
        /// 检查用户输入的自定义文本是否有效并输出错误信息。
        /// </summary>
        /// <param name="arr">包含了P1~P3自定义文本的集合。允许只包含一个元素，但要提供 index 参数</param>
        /// <param name="msg">错误信息</param>
        /// <param name="index">[仅进行单个判断时提供] 索引，可以为 0, 1, 2</param>
        /// <param name="ToBoolean">[仅进行单个判断时提供] 是否返回为 bool 类型</param>
        /// <returns>bool (是否有效), string (更正后的文本)</returns>
        public static object CheckCustomText(string[] arr, out string msg, int index = 0, bool ToBoolean = false)
        {
            bool Result = true;
            string Error = "";

            if (arr.Length == 3)
            {
                for (int i = 0; i < 3; i++)
                {
                    VerifyCustomText(i, arr[i], out bool IsValid, out string Msg);
                    if (!IsValid)
                    {
                        Error = Msg;
                        Result = IsValid;
                        break;
                    }
                }
            }
            else
            {
                VerifyCustomText(index, arr[0], out bool IsValid, out string Msg);
                msg = Msg;

                if (ToBoolean)
                {
                    return IsValid;
                }

                if (!IsValid)
                {
                    return GetCustomTextDefault(index);
                }

                return arr[0];
            }

            msg = Error;
            return Result;
        }

        public static List<Config> GetConfig(List<TupleEx<TupleEx<int, TimeSpan>, TupleEx<Color, Color, string>>> Rules)
        {
            var tmp = new List<Config>();

            foreach (var Rule in Rules)
            {
                var Part2 = Rule.Item3;
                tmp.Add(new($"{Rule.Item1}", GetExamTickConfig(Rule.Item2), Part2.Item1.ToRgb(), Part2.Item2.ToRgb(), Part2.Item3));
            }

            return tmp;
        }

        public static List<TupleEx<int, TimeSpan, TupleEx<Color, Color, string>>> GetObject(List<Config> cfg)
        {
            var tmp = new List<TupleEx<int, TimeSpan, TupleEx<Color, Color, string>>>();

            foreach (var Rule in cfg)
            {
                var fore = ColorHelper.GetColor(Rule.Fore);
                var back = ColorHelper.GetColor(Rule.Back);

                if (!ColorHelper.IsNiceContrast(fore, back))
                {
                    ConfigPolicy.NotAllowed<Color>();
                }

                var item1 = GetRuleTypeIndexFromRaw(Rule.Type);
                tmp.Add(new(item1, GetExamTickFormRaw(Rule.Tick), new(fore, back, (string)CheckCustomText([Rule.Text.RemoveIllegalChars()], out _, item1))));
            }

            return tmp;
        }

        private static TimeSpan GetExamTickCore(string str, char[] Separator)
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

        private static void VerifyCustomText(int Index, string CustomText, out bool IsValid, out string Warning)
        {
            var IndexHint = $"第{Index + 1}个自定义文本";
            var Matches = Regex.Matches(CustomText, @"\{.*?\}");

            if (string.IsNullOrWhiteSpace(CustomText))
            {
                Warning = $"{IndexHint}不能为空白！";
                IsValid = false;
                return;
            }

            foreach (Match m in Matches)
            {
                var mv = m.Value;

                if (!AllPHs.Contains(mv))
                {
                    Warning = $"在{IndexHint}中检测到了无效的占位符 {mv}，请重新设置！";
                    IsValid = false;
                    return;
                }
            }

            if (Matches.Count == 0)
            {
                Warning = $"请在{IndexHint}中至少使用一个占位符！";
                IsValid = false;
                return;
            }

            Warning = "";
            IsValid = true;
            return;
        }
    }
}
