using Newtonsoft.Json;
using System;
using System.IO;

namespace CEETimerCSharpWinForms.Modules.Configuration
{
    public class ConfigHandler
    {
        public event EventHandler<ConfigLoadedEventArgs> ConfigLoaded;

        private readonly JsonSerializerSettings Settings;
        private ConfigObject Config;

        public ConfigHandler()
        {
            Settings = new JsonSerializerSettings()
            {
                DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate,
                Formatting = Formatting.None,
                NullValueHandling = NullValueHandling.Ignore,
                TypeNameHandling = TypeNameHandling.Auto
            };
        }

        public void Save(ConfigObject Config)
            => IgnoreExceptions(() => File.WriteAllText(AppLauncher.ConfigFilePath, JsonConvert.SerializeObject(Config, Settings)));

        public void Read()
        {
            UnnamedMethod(
                () => Config = JsonConvert.DeserializeObject<ConfigObject>(File.ReadAllText(AppLauncher.ConfigFilePath)),
                () => Config = new(),
                OnConfigLoaded);
        }

        protected virtual void OnConfigLoaded()
        {
            ConfigLoaded?.Invoke(this, new(Config));
        }

        private void IgnoreExceptions(Action Method)
        {
            try
            {
                Method();
            }
            catch { }
        }

        private void UnnamedMethod(Action Try, Action Catch, Action Finally)
        {
            try
            {
                Try();
            }
            catch
            {
                Catch();
            }
            finally
            {
                Finally();
            }
        }
    }
}
