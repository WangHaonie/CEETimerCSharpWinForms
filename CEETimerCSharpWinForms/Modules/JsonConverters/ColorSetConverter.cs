using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace CEETimerCSharpWinForms.Modules.JsonConverters
{
    public class ColorSetConverter : JsonConverter<ColorSetObject>
    {
        public override ColorSetObject ReadJson(JsonReader reader, Type objectType, ColorSetObject existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var jsonObject = JObject.Load(reader);

            return new ColorSetObject(
                ColorHelper.GetColor(jsonObject["Fore"].ToString()),
                ColorHelper.GetColor(jsonObject["Back"].ToString()));
        }

        public override void WriteJson(JsonWriter writer, ColorSetObject value, JsonSerializer serializer)
        {
            var Fore = value.Fore;
            var Back = value.Back;

            writer.WriteStartObject();
            writer.WritePropertyName("Fore");
            writer.WriteValue($"{Fore.R},{Fore.G},{Fore.B}");
            writer.WritePropertyName("Back");
            writer.WriteValue($"{Back.R},{Back.G},{Back.B}");
            writer.WriteEndObject();
        }
    }
}
