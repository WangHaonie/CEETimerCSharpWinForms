﻿using Newtonsoft.Json.Converters;

namespace PlainCEETimer.Modules.JsonConverters
{
    public class ExamTimeConverter : IsoDateTimeConverter
    {
        public ExamTimeConverter()
        {
            DateTimeFormat = "yyyyMMddHHmmss";
        }
    }
}
