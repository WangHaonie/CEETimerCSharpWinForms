using CEETimerCSharpWinForms.Modules.JsonConverters;
using Newtonsoft.Json;
using System;
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

        public RulesManagerObject CustomRules { get; set; } = new();

        public int[] CustomColors { get; set; } = [.. Enumerable.Repeat(16777215, 16)];

        [JsonConverter(typeof(PointFormatConverter))]
        public Point Pos { get; set; } = new(0, 0);
    }

    public sealed class GeneralObject
    {
        public string ExamName { get; set; } = "";

        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime ExamStartTime { get; set; } = DateTime.Now;

        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime ExamEndTime { get; set; } = DateTime.Now;

        public bool RAMCleaner { get; set; }

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
                if (value is < 0 or > 3)
                {
                    throw new ArgumentOutOfRangeException("value");
                }

                field = value;
            }
        } = 0;

        public bool Rounding { get; set; }

        public bool ShowEnd { get; set; }

        public bool ShowPast { get; set; }

        public bool CustomText { get; set; }

        public CustomTextObject CustomTexts { get; set; } = new();

        public int ScreenIndex
        {
            get => field;
            set
            {
                if (value < 0 || value > Screen.AllScreens.Length)
                {
                    throw new ArgumentOutOfRangeException("value");
                }

                field = value;
            }
        } = 0;

        public int Position
        {
            get => field;
            set
            {
                if (value is < 0 or > 8)
                {
                    throw new ArgumentOutOfRangeException("value");
                }

                field = value;
            }
        } = 0;

        public bool Draggable { get; set; }

        public bool SeewoPptsvc { get; set; }
    }

    public sealed class AppearanceObject
    {
        [JsonConverter(typeof(FontFormatConverter))]
        public Font TextFont { get; set; }

        public ColorSetObject[] Colors { get; set; }
            = [new(Color.Red, Color.White), new(Color.Green, Color.White), new(Color.Black, Color.White), new(Color.Black, Color.White)];
    }

    public sealed class ToolsObject
    {

    }

    public sealed class RulesManagerObject
    {
        public CountdownPhase Phase { get; set; }

        [JsonConverter(typeof(TimeSpanConverter))]
        public TimeSpan Tick { get; set; }

        public ColorSetObject Color { get; set; }

        public string Text { get; set; }
    }

    public sealed class CustomTextObject
    {
        public string P1 { get; set; } = Placeholders.PH_P1;

        public string P2 { get; set; } = Placeholders.PH_P2;

        public string P3 { get; set; } = Placeholders.PH_P3;
    }

    [JsonConverter(typeof(ColorSetConverter))]
    public sealed class ColorSetObject(Color fore, Color back)
    {
        public Color Fore { get; set; } = fore;

        public Color Back { get; set; } = back;
    }
}
