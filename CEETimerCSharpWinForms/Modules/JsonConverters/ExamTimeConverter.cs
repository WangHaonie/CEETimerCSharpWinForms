using Newtonsoft.Json.Converters;

namespace CEETimerCSharpWinForms.Modules.JsonConverters
{
    public class ExamTimeConverter : IsoDateTimeConverter
    {
        public ExamTimeConverter()
        {
            DateTimeFormat = "yyyyMMddHHmmss";
        }
    }
}
