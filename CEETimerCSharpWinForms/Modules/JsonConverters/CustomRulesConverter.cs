using Newtonsoft.Json;
using CEETimerCSharpWinForms.Modules.Configuration;
using System;
using Newtonsoft.Json.Linq;

namespace CEETimerCSharpWinForms.Modules.JsonConverters
{
    internal class CustomRulesConverter : JsonConverter<RulesManagerObject>
    {
        public override RulesManagerObject ReadJson(JsonReader reader, Type objectType, RulesManagerObject existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var Json = serializer.Deserialize<JObject>(reader);
            var PhaseValue = Convert.ToInt32(Json["Phase"]);

            if (!Enum.IsDefined(typeof(CountdownPhase), PhaseValue))
            {
                throw new Exception();
            }

            var Phase = (CountdownPhase)PhaseValue;

            var TimeSpanParts = Json["Tick"].ToString().Split(',');
            var Tick = new TimeSpan(
                    int.Parse(TimeSpanParts[0]),
                    int.Parse(TimeSpanParts[1]),
                    int.Parse(TimeSpanParts[2]),
                    int.Parse(TimeSpanParts[3]));

            var Fore = ColorHelper.GetColor(Json["Fore"].ToString());
            var Back = ColorHelper.GetColor(Json["Back"].ToString());

            if (!ColorHelper.IsNiceContrast(Fore, Back))
            {
                throw new Exception();
            }

            var Text = Json["Text"].ToString().RemoveIllegalChars();

            Console.WriteLine($"{Phase}{Tick}{Text}{Fore}{Back}");

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
            var Phase = (int)value.Phase;
            var Tick = value.Tick.ToStr();
            var Text = value.Text;
            var Fore = value.Fore.ToRgb();
            var Back = value.Back.ToRgb();

            new JObject()
            {
                { nameof(Phase), Phase },
                { nameof(Tick), Tick },
                { nameof(Text), Text },
                { nameof(Fore), Fore },
                { nameof(Back), Back }
            }.WriteTo(writer);
        }
    }
}
