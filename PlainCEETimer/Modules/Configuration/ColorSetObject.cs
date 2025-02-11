using Newtonsoft.Json;
using PlainCEETimer.Modules.JsonConverters;
using System.Drawing;

namespace PlainCEETimer.Modules.Configuration
{
    [JsonConverter(typeof(ColorSetConverter))]
    public sealed class ColorSetObject(Color fore, Color back)
    {
        public Color Fore => fore;

        public Color Back => back;
    }
}
