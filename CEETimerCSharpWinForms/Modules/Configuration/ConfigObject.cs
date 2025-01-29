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

        public RulesManagerObject[] CustomRules
        {
            get => field ?? [];
            set => field = value ?? [];
        }

        public int[] CustomColors { get; set; } = [.. Enumerable.Repeat(16777215, 16)];

        [JsonConverter(typeof(PointFormatConverter))]
        public Point Pos { get; set; }
    }

    public sealed class GeneralObject
    {
        public ExamInfoObject[] ExamInfo
        {
            get => field ?? [];
            set
            {
                var tmp = value ?? [];

                ConfigHandler.Validate(() =>
                {
                    Array.Sort(tmp);
                });

                field = tmp;
            }
        }

        public int ExamIndex
        {
            get => field;
            set
            {
                ConfigHandler.Validate(() =>
                {
                    if (value < 0 || value > ExamInfo.Length)
                    {
                        throw new Exception();
                    }
                });
            }
        }

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
                ConfigHandler.Validate(() =>
                {
                    if (value is < 0 or > 3)
                    {
                        throw new Exception();
                    }
                });

                field = value;
            }
        }

        public bool Rounding { get; set; }

        public int EndIndex
        {
            get => field;
            set
            {
                ConfigHandler.Validate(() =>
                {
                    if (value is < 0 or > 2)
                    {
                        throw new Exception();
                    }
                });

                field = value;
            }
        }

        public bool CustomText { get; set; }

        public string[] CustomTexts { get; set; }
            = [Placeholders.PH_P1, Placeholders.PH_P2, Placeholders.PH_P3];

        public int ScreenIndex
        {
            get => field;
            set
            {
                ConfigHandler.Validate(() =>
                {
                    if (value < 0 || value > Screen.AllScreens.Length)
                    {
                        throw new Exception();
                    }
                });

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
                ConfigHandler.Validate(() =>
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

    public sealed class ToolsObject
    {
        public int NtpServer
        {
            get => field;
            set
            {
                ConfigHandler.Validate(() =>
                {
                    if (value is < 0 or > 3)
                    {
                        throw new Exception();
                    }
                });

                field = value;
            }
        }

        public bool TrayIcon { get; set; }

        public bool TrayText { get; set; }
    }

    public sealed class ExamInfoObject : IComparable<ExamInfoObject>
    {
        public string ExamName
        {
            get => field;
            set
            {
                ConfigHandler.Validate(() =>
                {
                    if (!value.Length.IsValid())
                    {
                        throw new Exception();
                    }
                });

                field = value;
            }
        } = "";

        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime ExamStartTime { get; set; } = DateTime.Now;

        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime ExamEndTime { get; set; } = DateTime.Now;

        public override string ToString()
            => string.Format("{0} - {1}", ExamName, ExamStartTime.ToString(AppLauncher.DateTimeFormat));

        int IComparable<ExamInfoObject>.CompareTo(ExamInfoObject other)
        {
            if (other == null)
            {
                return 1;
            }

            return ExamStartTime.CompareTo(other.ExamStartTime);
        }
    }

    [JsonConverter(typeof(CustomRulesConverter))]
    public sealed class RulesManagerObject
    {
        public CountdownPhase Phase { get; set; }

        public TimeSpan Tick { get; set; }

        public string Text { get; set; }

        public Color Fore { get; set; }

        public Color Back { get; set; }
    }

    [JsonConverter(typeof(ColorSetConverter))]
    public sealed class ColorSetObject(Color fore, Color back)
    {
        public Color Fore { get; set; } = fore;

        public Color Back { get; set; } = back;
    }
}
