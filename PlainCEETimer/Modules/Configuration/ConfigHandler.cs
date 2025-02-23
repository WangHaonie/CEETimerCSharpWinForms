using Newtonsoft.Json;
using System;
using System.IO;

namespace PlainCEETimer.Modules.Configuration
{
    public class ConfigHandler
    {
        private readonly JsonSerializerSettings Settings;
        private readonly MessageBoxHelper MessageX;

        public ConfigHandler()
        {
            Settings = new JsonSerializerSettings()
            {
                DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate,
                Formatting = Formatting.None,
                NullValueHandling = NullValueHandling.Ignore,
                TypeNameHandling = TypeNameHandling.Auto
            };

            MessageX = new();
        }

        public void Save(ConfigObject Config)
        {
            try
            {
                File.WriteAllText(App.ConfigFilePath, JsonConvert.SerializeObject(Config, Settings));
            }
            catch (Exception ex)
            {
                MessageX.Error($"保存设置时出现错误！{ex.ToMessage()}");
            }
        }

        public ConfigObject Read()
        {
            try
            {
                return JsonConvert.DeserializeObject<ConfigObject>(File.ReadAllText(App.ConfigFilePath));
            }
            catch
            {
                return new();
            }
        }

        public static TimeSpan GetAutoSwitchInterval(int Index) => Index switch
        {
            1 => TimeSpan.FromSeconds(15),
            2 => TimeSpan.FromSeconds(30),
            3 => TimeSpan.FromSeconds(45),
            4 => TimeSpan.FromMinutes(1),
            5 => TimeSpan.FromMinutes(2),
            6 => TimeSpan.FromMinutes(3),
            7 => TimeSpan.FromMinutes(5),
            8 => TimeSpan.FromMinutes(10),
            9 => TimeSpan.FromMinutes(15),
            10 => TimeSpan.FromMinutes(30),
            11 => TimeSpan.FromMinutes(45),
            12 => TimeSpan.FromHours(1),
            _ => TimeSpan.FromSeconds(10)
        };
    }
}
