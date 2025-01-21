using Newtonsoft.Json.Converters;

namespace CEETimerCSharpWinForms.Modules.JsonConverters
{
    public class DateTimeFormatConverter : IsoDateTimeConverter
    {
        public DateTimeFormatConverter()
        {
            DateTimeFormat = "yyyyMMddHHmmss";
        }
    }
}
