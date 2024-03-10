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
        private static Dictionary<string, string> JsonConfig = [];
        private static readonly string ConfigFile = $"{LaunchManager.CurrentExecutablePath}CEETimerCSharpWinForms.dll";

        private static void CheckConfig()
        {
            if (!File.Exists(ConfigFile))
            {
                if (Directory.Exists(ConfigFile))
                {
                    Directory.Delete(ConfigFile);
                }
                else
                {
                    File.Create(ConfigFile).Close();
                }
            }
        }

        public static string ReadConfig(string key)
        {
            CheckConfig();

            try
            {
                JsonConfig = JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText(ConfigFile));
            }
            catch
            {
                JsonConfig = [];
            }

            if (JsonConfig != null && JsonConfig.ContainsKey(key))
            {
                if (key == "Font")
                {
                    return JsonConfig["Font"];
                }
                else
                {
                    return JsonConfig[key].RemoveAllBadChars();
                }
            }
            else
            {
                return string.Empty;
            }
        }

        public static void WriteConfig(Dictionary<string, string> DataSet)
        {
            CheckConfig();
            JsonConfig ??= [];

            foreach (var Keys in DataSet)
            {
                JsonConfig[Keys.Key] = Keys.Value;
            }

            string Config = JsonConvert.SerializeObject(JsonConfig);
            File.WriteAllText(ConfigFile, Config);
        }
        #endregion

        public static bool IsValidData(string ExamName)
        {
            if (string.IsNullOrEmpty(ExamName) || ExamName.Length < 2 || ExamName.Length > 15)
            {
                return false;
            }
            return true;
        }

        public static bool IsValidData(DateTime ExamTime)
        {
            if (ExamTime < new DateTime(1753, 1, 1, 23, 59, 59) || ExamTime > new DateTime(2106, 12, 31, 23, 59, 59))
            {
                return false;
            }
            return true;
        }
    }
}
