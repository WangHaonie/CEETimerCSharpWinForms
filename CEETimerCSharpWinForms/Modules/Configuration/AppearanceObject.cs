using CEETimerCSharpWinForms.Modules.JsonConverters;
using Newtonsoft.Json;
using System;
using System.Drawing;

namespace CEETimerCSharpWinForms.Modules.Configuration
{
    public sealed class AppearanceObject : ValidatableConfigObject
    {
        [JsonConverter(typeof(FontFormatConverter))]
        public Font Font { get; set; } = new((Font)new FontConverter().ConvertFromString(ConfigPolicy.DefaultFont), FontStyle.Bold);

        public ColorSetObject[] Colors
        {
            get => field;
            set
            {
                Validate(() =>
                {
                    if (value.Length > 4)
                    {
                        throw new Exception();
                    }

                    for (int i = 0; i < 4; i++)
                    {
                        if (!ColorHelper.IsNiceContrast(value[i].Fore, value[i].Back))
                        {
                            throw new Exception();
                        }
                    }
                });

                field = value;
            }
        } = [new(Color.Red, Color.White), new(Color.Green, Color.White), new(Color.Black, Color.White), new(Color.Black, Color.White)];
    }
}
