using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PlainCEETimer.Modules.Configuration;
using System;

namespace PlainCEETimer.Modules.JsonConverters
{
    public class ColorSetConverter : JsonConverter<ColorSetObject>
    {
        public override ColorSetObject ReadJson(JsonReader reader, Type objectType, ColorSetObject existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var Json = JObject.Load(reader);
            var Fore = ColorHelper.GetColor(Json, 0);
            var Back = ColorHelper.GetColor(Json, 1);

            if (ColorHelper.IsNiceContrast(Fore, Back))
            {
                return new ColorSetObject(Fore, Back);
            }

            throw new Exception();
        }

        public override void WriteJson(JsonWriter writer, ColorSetObject value, JsonSerializer serializer)
        {
            new JObject()
            {
                { nameof(value.Fore), value.Fore.ToRgb() },
                { nameof(value.Back), value.Back.ToRgb() }
            }.WriteTo(writer);
        }
    }
}