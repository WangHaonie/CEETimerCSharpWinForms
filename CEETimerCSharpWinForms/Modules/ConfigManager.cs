using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace CEETimerCSharpWinForms.Modules
{
    public class ConfigManager
    {
        private static Dictionary<string, string> JsonConfig = new Dictionary<string, string>();
        private static string ConfigFile = $"{AppDomain.CurrentDomain.BaseDirectory}CEETimerCSharpWinForms.dll";
        public static string ReadConfig(string key)
        {
            if (!File.Exists(ConfigFile))
            {
                FileStream fileStream = File.Create(ConfigFile);
            }
            try
            {
                JsonConfig = JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText(ConfigFile));
            }
            catch
            {
                JsonConfig = new Dictionary<string, string>();
            }

            if (JsonConfig != null && JsonConfig.ContainsKey(key))
            {
                return JsonConfig[key];
            }
            else
            {
                return string.Empty;
            }
        }
        public static void WriteConfig(string key, string value)
        {
            if (JsonConfig == null)
            {
                JsonConfig = new Dictionary<string, string>();
            }
            JsonConfig[key] = value;
            string Config = JsonConvert.SerializeObject(JsonConfig);
            File.WriteAllText(ConfigFile, Config);
        }/*
        public static bool IsNew()
        {
            if (!JsonConfig.ContainsKey("ExamName") && !JsonConfig.ContainsKey("ExamStartTime") && !JsonConfig.ContainsKey("ExamEndTime") && !File.Exists(ConfigFile))
            {
                return true;
            }
            return false;
        }*/
    }
}
