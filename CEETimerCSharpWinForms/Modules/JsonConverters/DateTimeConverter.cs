using Newtonsoft.Json.Converters;

namespace CEETimerCSharpWinForms.Modules.JsonConverters
{
    public class DateTimeConverter : IsoDateTimeConverter
    {
        public DateTimeConverter()
        {
            DateTimeFormat = "yyyyMMddHHmmss";
        }
    }
}
