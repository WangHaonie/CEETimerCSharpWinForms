using Newtonsoft.Json;
using System;
using System.Drawing;

namespace CEETimerCSharpWinForms.Modules.JsonConverters
{
    public class PointFormatConverter : JsonConverter<Point>
    {
        public override Point ReadJson(JsonReader reader, Type objectType, Point existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            string[] PointParts = reader.Value.ToString().Split(',');

            if (PointParts.Length == 2)
            {
                return new Point(
                    int.Parse(PointParts[0]),
                    int.Parse(PointParts[1]));
            }

            throw new JsonSerializationException("Invalid Point Format");
        }

        public override void WriteJson(JsonWriter writer, Point value, JsonSerializer serializer)
        {
            writer.WriteValue($"{value.X},{value.Y}");
        }
    }
}
