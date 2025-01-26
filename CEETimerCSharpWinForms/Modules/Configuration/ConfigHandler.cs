using CEETimerCSharpWinForms.Controls;
using Newtonsoft.Json;
using System;
using System.IO;

namespace CEETimerCSharpWinForms.Modules.Configuration
{
    public class ConfigHandler
    {
        private readonly JsonSerializerSettings Settings;
        private readonly MessageBoxHelper MessageX;

        public ConfigHandler(TrackableForm Owner)
        {
            Settings = new JsonSerializerSettings()
            {
                DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate,
                Formatting = Formatting.None,
                NullValueHandling = NullValueHandling.Ignore,
                TypeNameHandling = TypeNameHandling.Auto
            };

            MessageX = new(Owner);
        }

        public void Save(ConfigObject Config)
        {
            try
            {
                File.WriteAllText(AppLauncher.ConfigFilePath, JsonConvert.SerializeObject(Config, Settings));
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
                return JsonConvert.DeserializeObject<ConfigObject>(File.ReadAllText(AppLauncher.ConfigFilePath));
            }
            catch
            {
                return new();
            }
        }
    }
}
