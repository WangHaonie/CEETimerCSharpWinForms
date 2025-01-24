using CEETimerCSharpWinForms.Forms;
using CEETimerCSharpWinForms.Modules.JsonConverters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CEETimerCSharpWinForms.Modules.Configuration
{
    public sealed class ConfigObject
    {
        public GeneralObject General { get; set; } = new();

        public DisplayObject Display { get; set; } = new();

        public AppearanceObject Appearance { get; set; } = new();

        public ToolsObject Tools { get; set; } = new();

        public List<RulesManagerObject> CustomRules { get; set; } = [];

        public int[] CustomColors { get; set; } = [.. Enumerable.Repeat(16777215, 16)];

        [JsonConverter(typeof(PointFormatConverter))]
        public Point Pos { get; set; }
    }

    public sealed class GeneralObject
    {
        public string ExamName
        {
            get => field;
            set
            {
                if (MainForm.ValidateNeeded && !value.Length.IsValid())
                {
                    throw new ArgumentException("value");
                }

                field = value;
            }
        } = "";

        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime ExamStartTime { get; set; } = DateTime.Now;

        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime ExamEndTime { get; set; } = DateTime.Now;

        public bool MemClean { get; set; }

        public bool TopMost { get; set; } = true;

        public bool UniTopMost { get; set; }
    }

    public sealed class DisplayObject
    {
        public bool ShowXOnly { get; set; }

        public int X
        {
            get => field;
            set
            {
                if (MainForm.ValidateNeeded && value is < 0 or > 3)
                {
                    throw new ArgumentOutOfRangeException("value");
                }

                field = value;
            }
        }

        public bool Rounding { get; set; }

        public bool ShowEnd { get; set; }

        public bool ShowPast { get; set; }

        public bool CustomText { get; set; }

        public string[] CustomTexts { get; set; }
            = [Placeholders.PH_P1, Placeholders.PH_P2, Placeholders.PH_P3];

        public int ScreenIndex
        {
            get => field;
            set
            {
                if (MainForm.ValidateNeeded && value < 0 || value > Screen.AllScreens.Length)
                {
                    throw new ArgumentOutOfRangeException("value");
                }

                field = value;
            }
        }

        public CountdownPosition Position { get; set; }

        public bool Draggable { get; set; }

        public bool SeewoPptsvc { get; set; }
    }

    public sealed class AppearanceObject
    {
        [JsonConverter(typeof(FontFormatConverter))]
        public Font Font { get; set; } = new((Font)new FontConverter().ConvertFromString(ConfigPolicy.DefaultFont), FontStyle.Bold);

        public ColorSetObject[] Colors
        {
            get => field;
            set
            {
                if (MainForm.ValidateNeeded)
                {
                    if (value.Length > 4)
                    {
                        throw new ArgumentException("value");
                    }

                    for (int i = 0; i < 4; i++)
                    {
                        if (!ColorHelper.IsNiceContrast(value[i].Fore, value[i].Back))
                        {
                            throw new ArgumentException("value");
                        }
                    }
                }

                field = value;
            }
        } = [new(Color.Red, Color.White), new(Color.Green, Color.White), new(Color.Black, Color.White), new(Color.Black, Color.White)];
    }

    public sealed class ToolsObject
    {

    }

    public sealed class RulesManagerObject
    {
        public CountdownPhase Phase { get; set; }

        [JsonConverter(typeof(TimeSpanConverter))]
        public TimeSpan Tick { get; set; }

        public string Text { get; set; }

        public ColorSetObject Color { get; set; }
    }

    [JsonConverter(typeof(ColorSetConverter))]
    public sealed class ColorSetObject(Color fore, Color back)
    {
        public Color Fore { get; set; } = fore;

        public Color Back { get; set; } = back;
    }
}
