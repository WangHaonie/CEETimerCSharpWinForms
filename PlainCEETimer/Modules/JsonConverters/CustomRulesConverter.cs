using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PlainCEETimer.Modules.Configuration;
using System;

namespace PlainCEETimer.Modules.JsonConverters
{
    internal class CustomRulesConverter : JsonConverter<RulesManagerObject>
    {
        public override RulesManagerObject ReadJson(JsonReader reader, Type objectType, RulesManagerObject existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var Json = serializer.Deserialize<JObject>(reader);
            var PhaseValue = Convert.ToInt32(Json[nameof(existingValue.Phase)]);

            if (!Enum.IsDefined(typeof(CountdownPhase), PhaseValue))
            {
                throw new Exception();
            }

            var Phase = (CountdownPhase)PhaseValue;
            var Tick = Json[nameof(existingValue.Tick)].ToString().ToTimeSpan(ConfigPolicy.ValueSeperator);
            var Fore = ColorHelper.GetColor(Json, 0);
            var Back = ColorHelper.GetColor(Json, 1);

            if (!ColorHelper.IsNiceContrast(Fore, Back))
            {
                throw new Exception();
            }

            var Text = Json[nameof(existingValue.Text)].ToString().RemoveIllegalChars();

            return new()
            {
                Phase = Phase,
                Tick = Tick,
                Text = Text,
                Fore = Fore,
                Back = Back
            };
        }

        public override void WriteJson(JsonWriter writer, RulesManagerObject value, JsonSerializer serializer)
        {
            new JObject()
            {
                { nameof(value.Phase), (int)value.Phase },
                { nameof(value.Tick), value.Tick.ToStr() },
                { nameof(value.Text), value.Text },
                { nameof(value.Fore), value.Fore.ToRgb() },
                { nameof(value.Back), value.Back.ToRgb() }
            }.WriteTo(writer);
        }
    }
}
