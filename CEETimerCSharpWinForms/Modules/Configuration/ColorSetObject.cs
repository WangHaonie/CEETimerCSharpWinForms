using CEETimerCSharpWinForms.Modules.JsonConverters;
using Newtonsoft.Json;
using System.Drawing;

namespace CEETimerCSharpWinForms.Modules.Configuration
{
    [JsonConverter(typeof(ColorSetConverter))]
    public sealed class ColorSetObject(Color fore, Color back)
    {
        public Color Fore { get; set; } = fore;

        public Color Back { get; set; } = back;
    }
}
