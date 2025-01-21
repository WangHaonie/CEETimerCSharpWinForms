using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Drawing;
using System.IO;

namespace CEETimerCSharpWinForms.Modules
{
    public sealed class ConfigObject
    {
        public GeneralObject General { get; set; }
        public DisplayObject Display { get; set; }
        public AppearanceObject Appearance { get; set; }
        public ToolsObject Tools { get; set; }
        public RulesManagerObject RulesManager { get; set; }
        public Point Pos { get; set; }
    }

    public sealed class GeneralObject
    {
        public string ExamName { get; set; }
        public DateTime ExamStartTime { get; set; }
        public DateTime ExamEndTime { get; set; }
        public bool RAMCleaner { get; set; }
        public bool TopMost { get; set; }
        public bool UniTopMost { get; set; }
    }

    public sealed class DisplayObject
    {
        public bool ShowXOnly { get; set; }
        public int X { get; set; }
        public bool Rounding { get; set; }
        public bool ShowEnd { get; set; }
        public bool ShowPast { get; set; }
        public bool CustomText { get; set; }
        public CustomTextObject CustomTexts { get; set; }
        public int ScreenIndex { get; set; }
        public int Position { get; set; }
        public bool Draggable { get; set; }
        public bool SeewoPptsvc { get; set; }
    }

    public sealed class AppearanceObject
    {
        public Font TextFont { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public FontStyle TextFontStyle { get; set; }

        public ColorSetObject[] Colors { get; set; }
    }

    public sealed class ToolsObject
    {

    }

    public sealed class RulesManagerObject
    {
        public int Phase { get; set; }
        public TimeSpan Tick { get; set; }
        public ColorSetObject Color { get; set; }
        public string Text { get; set; }
    }

    public sealed class CustomTextObject
    {
        public string P1 { get; set; }
        public string P2 { get; set; }
        public string P3 { get; set; }
    }

    public sealed class ColorSetObject
    {
        public Color Fore { get; set; }
        public Color Back { get; set; }
    }

    public class ConfigHandler
    {
        private readonly JsonSerializerSettings Settings;

        public ConfigHandler()
        {
            Settings = new JsonSerializerSettings()
            {
                Formatting = Formatting.None,
                TypeNameHandling = TypeNameHandling.Auto,
                NullValueHandling = NullValueHandling.Ignore
            };
        }

        public void Save(ConfigObject Config)
        {
            File.WriteAllText(AppLauncher.ConfigFilePath, JsonConvert.SerializeObject(Config, Settings));
        }

        public ConfigObject Read() => JsonConvert.DeserializeObject<ConfigObject>(File.ReadAllText(AppLauncher.ConfigFilePath));
    }
}
