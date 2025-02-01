using Newtonsoft.Json;
using System;
using System.Drawing;
using System.Linq;

namespace CEETimerCSharpWinForms.Modules.JsonConverters
{
    public class FontFormatConverter : JsonConverter<Font>
    {
        public override Font ReadJson(JsonReader reader, Type objectType, Font existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            string[] FontParts = reader.Value.ToString().Split(ConfigPolicy.ValueSeperator);

            var FontPart1 = (Font)new FontConverter().ConvertFromString(string.Join(ConfigPolicy.ValueSeperatorString, FontParts.Take(2)));
            var FontPart2 = (FontStyle)Enum.Parse(typeof(FontStyle), string.Join(ConfigPolicy.ValueSeperatorString, FontParts.Skip(2)));

            if (FontPart1.Size is >= ConfigPolicy.MinFontSize or <= ConfigPolicy.MaxFontSize)
            {
                return new Font(FontPart1, FontPart2);
            }

            throw new Exception();
        }

        public override void WriteJson(JsonWriter writer, Font value, JsonSerializer serializer)
        {
            writer.WriteValue($"{value.Name}, {value.Size}pt, {value.Style}");
        }
    }
}
