﻿using System;

namespace PlainCEETimer.Modules
{
    public static class ConfigPolicy
    {
        public const int MinExamNameLength = 2;
        public const int MaxExamNameLength = 10;
        public const int MinCustomTextLength = 0;
        public const int MaxCustomTextLength = 50;
        public const int MinFontSize = 10;
        public const int MaxFontSize = 28;
        public static char[] CharsNotAllowed => ['\\', '/', '*', '?', '"', '\'', '<', '>', '|'];
        public static char ValueSeparator => ',';
        public static string ValueSeparatorString => ", ";
        public static TimeSpan TsMaxAllowed => new(65535, 23, 59, 59);
        public static TimeSpan TsMinAllowed => new(0, 0, 0, 1);

        public static T NotAllowed<T>()
        {
            throw new Exception();
        }
    }
}
