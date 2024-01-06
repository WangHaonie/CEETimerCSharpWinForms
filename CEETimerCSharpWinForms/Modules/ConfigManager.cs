using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace CEETimerCSharpWinForms.Modules
{
    public class ConfigManager
    {
        #region 来自网络
        /* 
        
        JSON 配置读写参考：

        C#--使用json配置文件方法【读写Json，适合小项目】 - 包子789654 - 博客园
        https://www.cnblogs.com/baozi789654/p/15645897.html

        */
        private static Dictionary<string, string> JsonConfig = new Dictionary<string, string>();
        private static string ConfigFile = $"{LaunchManager.CurrentExecutablePath}CEETimerCSharpWinForms.dll";
        public static string ReadConfig(string key)
        {
            if (!File.Exists(ConfigFile))
            {
                File.Create(ConfigFile).Close();
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
        }
        #endregion
        public static bool IsValidData(string ExamName)
        {
            if (string.IsNullOrEmpty(ExamName) || ExamName.Length > 15)
            {
                return false;
            }
            return true;
        }
        public static bool IsValidData(DateTime ExamTime)
        {
            if (ExamTime < new DateTime(1753, 1, 1))
            {
                return false;
            }
            return true;
        }
    }
}
