using Newtonsoft.Json;
using System;
using System.Drawing;

namespace CEETimerCSharpWinForms.Modules.JsonConverters
{
    public class FontFormatConverter : JsonConverter<Font>
    {
        public override Font ReadJson(JsonReader reader, Type objectType, Font existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            string[] FontParts = reader.Value.ToString().Split(',');

            var FontPart1 = (Font)new FontConverter().ConvertFromString($"{FontParts[0]}, {FontParts[1]}pt");
            var FontPart2 = (FontStyle)Enum.Parse(typeof(FontStyle), FontParts[2]);

            if (FontPart1.Size is >= ConfigPolicy.MinFontSize or <= ConfigPolicy.MaxFontSize)
            {
                return new Font(FontPart1, FontPart2);
            }

            throw new JsonSerializationException("Invalid Font Format");
        }

        public override void WriteJson(JsonWriter writer, Font value, JsonSerializer serializer)
        {
            writer.WriteValue($"{value.Name},{value.Size},{value.Style}");
        }
    }
}
