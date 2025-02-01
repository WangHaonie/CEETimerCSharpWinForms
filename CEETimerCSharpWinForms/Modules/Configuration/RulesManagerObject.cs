using CEETimerCSharpWinForms.Modules.JsonConverters;
using Newtonsoft.Json;
using System;
using System.Drawing;

namespace CEETimerCSharpWinForms.Modules.Configuration
{
    [JsonConverter(typeof(CustomRulesConverter))]
    public sealed class RulesManagerObject : IComparable<RulesManagerObject>, IEquatable<RulesManagerObject>
    {
        public CountdownPhase Phase { get; set; }

        public TimeSpan Tick { get; set; }

        public string Text { get; set; }

        public Color Fore { get; set; }

        public Color Back { get; set; }

        int IComparable<RulesManagerObject>.CompareTo(RulesManagerObject other)
        {
            if (other == null)
            {
                return 1;
            }

            var PhaseComparer = Phase.CompareTo(other.Phase);
            if (PhaseComparer != 0)
            {
                return PhaseComparer;
            }

            return Tick.CompareTo(other.Tick);
        }

        bool IEquatable<RulesManagerObject>.Equals(RulesManagerObject other)
        {
            if (other == null)
            {
                return false;
            }

            return Phase == other.Phase && Tick == other.Tick;
        }
    }
}
