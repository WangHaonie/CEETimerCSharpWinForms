using CEETimerCSharpWinForms.Modules.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace CEETimerCSharpWinForms.Modules.JsonConverters
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

            var TimeSpanParts = Json[nameof(existingValue.Tick)].ToString().Split(ConfigPolicy.ValueSeperator);
            var Tick = new TimeSpan(
                    int.Parse(TimeSpanParts[0]),
                    int.Parse(TimeSpanParts[1]),
                    int.Parse(TimeSpanParts[2]),
                    int.Parse(TimeSpanParts[3]));

            var Fore = ColorHelper.GetColor(Json[nameof(existingValue.Fore)].ToString());
            var Back = ColorHelper.GetColor(Json[nameof(existingValue.Back)].ToString());

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
