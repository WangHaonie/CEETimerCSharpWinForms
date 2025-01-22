using Newtonsoft.Json;
using System;

namespace CEETimerCSharpWinForms.Modules.JsonConverters
{
    public class TimeSpanConverter : JsonConverter<TimeSpan>
    {
        public override TimeSpan ReadJson(JsonReader reader, Type objectType, TimeSpan existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var TimeSpanParts = reader.Value.ToString().Split(',');

            if (TimeSpanParts.Length == 4)
            {
                return new TimeSpan(
                    int.Parse(TimeSpanParts[0]),
                    int.Parse(TimeSpanParts[1]),
                    int.Parse(TimeSpanParts[2]),
                    int.Parse(TimeSpanParts[3]));
            }

            throw new JsonSerializationException("Invalid TimeSpan Format");
        }

        public override void WriteJson(JsonWriter writer, TimeSpan value, JsonSerializer serializer)
        {
            writer.WriteValue($"{value.Days},{value.Hours},{value.Minutes},{value.Seconds}");
        }
    }
}
