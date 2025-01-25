using CEETimerCSharpWinForms.Modules.Configuration;
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
            var Fore = ColorHelper.GetColor(jsonObject[nameof(existingValue.Fore)].ToString());
            var Back = ColorHelper.GetColor(jsonObject[nameof(existingValue.Back)].ToString());

            if (!ColorHelper.IsNiceContrast(Fore, Back))
            {
                throw new Exception();
            }

            return new ColorSetObject(Fore, Back);
        }

        public override void WriteJson(JsonWriter writer, ColorSetObject value, JsonSerializer serializer)
        {
            var Fore = value.Fore;
            var Back = value.Back;

            writer.WriteStartObject();
            writer.WritePropertyName(nameof(value.Fore));
            writer.WriteValue(Fore.ToRgb());
            writer.WritePropertyName(nameof(value.Back));
            writer.WriteValue(Back.ToRgb());
            writer.WriteEndObject();
        }
    }
}