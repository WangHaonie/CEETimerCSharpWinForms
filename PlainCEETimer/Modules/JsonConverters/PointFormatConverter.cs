using Newtonsoft.Json;
using System;
using System.Drawing;

namespace PlainCEETimer.Modules.JsonConverters
{
    public class PointFormatConverter : JsonConverter<Point>
    {
        public override Point ReadJson(JsonReader reader, Type objectType, Point existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            string[] PointParts = reader.Value.ToString().Split(ConfigPolicy.ValueSeperator);

            if (PointParts.Length == 2)
            {
                return new Point(
                    int.Parse(PointParts[0]),
                    int.Parse(PointParts[1]));
            }

            throw new Exception();
        }

        public override void WriteJson(JsonWriter writer, Point value, JsonSerializer serializer)
        {
            writer.WriteValue($"{value.X},{value.Y}");
        }
    }
}
