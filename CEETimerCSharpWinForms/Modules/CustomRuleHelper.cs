﻿using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace CEETimerCSharpWinForms.Modules
{
    public static class CustomRuleHelper
    {
        private static readonly string[] AllPHs = [Placeholders.PH_EXAMNAME, Placeholders.PH_DAYS, Placeholders.PH_HOURS, Placeholders.PH_MINUTES, Placeholders.PH_SECONDS, Placeholders.PH_ROUNDEDDAYS, Placeholders.PH_TOTALHOURS, Placeholders.PH_TOTALMINUTES, Placeholders.PH_TOTALSECONDS];

        public static char[] TsSeparator => ['天', '时', '分', '秒'];
        public static TimeSpan GetExamTick(string str) => GetExamTickCore(str, TsSeparator);
        public static string GetExamTickText(TimeSpan timeSpan)
            => $"{timeSpan.Days}{TsSeparator[0]}{timeSpan.Hours}{TsSeparator[1]}{timeSpan.Minutes}{TsSeparator[2]}{timeSpan.Seconds}{TsSeparator[3]}";

        public static CountdownPhase GetPhase(string s) => s switch
        {
            Placeholders.PH_LEFT => CountdownPhase.P2,
            Placeholders.PH_PAST => CountdownPhase.P3,
            _ => 0
        };

        public static string GetRuleTypeText(CountdownPhase i) => i switch
        {
            CountdownPhase.P2 => Placeholders.PH_LEFT,
            CountdownPhase.P3 => Placeholders.PH_PAST,
            _ => Placeholders.PH_START
        };

        public static string GetCustomTextDefault(int Index, string[] Pref = null) => Index switch
        {
            1 => Pref[1] ?? Placeholders.PH_P2,
            2 => Pref[2] ?? Placeholders.PH_P3,
            _ => Pref[0] ?? Placeholders.PH_P1
        };

        /// <summary>
        /// 检查用户输入的自定义文本是否有效并输出错误信息。
        /// </summary>
        /// <param name="arr">包含了P1~P3自定义文本的集合。允许只包含一个元素，但要提供 index 参数</param>
        /// <param name="msg">错误信息</param>
        /// <param name="index">[仅进行单个判断时提供] 索引，可以为 0, 1, 2</param>
        /// <param name="ToBoolean">[仅进行单个判断时提供] 是否返回为 bool 类型</param>
        /// <returns>bool (是否有效), string (更正后的文本)</returns>
        public static object CheckCustomText(string[] arr, out string msg, int index = -1, bool ToBoolean = false)
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
                    return GetCustomTextDefault(0);
                }

                return arr[0];
            }

            msg = Error;
            return Result;
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
            var IndexHint = GetIndexHint(Index);
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

        private static string GetIndexHint(int Index) => Index switch
        {
            -1 => "自定义文本",
            _ => $"第{Index + 1}个自定义文本"
        };
    }
}
