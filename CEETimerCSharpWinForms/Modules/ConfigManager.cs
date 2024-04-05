using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
        private static bool IsConfigMounted;
        private static Dictionary<string, string> JsonConfig;
        private static JObject ConfigObject;
        private static readonly string ConfigFile = $"{LaunchManager.CurrentExecutablePath}CEETimerCSharpWinForms.dll";

        private static void CheckConfig()
        {
            if (!File.Exists(ConfigFile))
            {
                if (Directory.Exists(ConfigFile))
                {
                    Directory.Delete(ConfigFile);
                }
                File.Create(ConfigFile).Close();
            }
        }

        public static string ReadConfig(string Key)
        {
            if (IsConfigMounted && JsonConfig != null && JsonConfig.ContainsKey(Key))
            {
                if (Key == "Font")
                {
                    return JsonConfig["Font"];
                }
                else
                {
                    return JsonConfig[Key].RemoveAllBadChars();
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

            try
            {
                string OriginalDataContent = File.ReadAllText(ConfigFile);
                ConfigObject = JObject.Parse(OriginalDataContent);
            }
            catch
            {
                ConfigObject = [];
            }

            foreach (var Data in DataSet)
            {
                ConfigObject[Data.Key] = JToken.FromObject(Data.Value);
            }

            File.WriteAllText(ConfigFile, $"{ConfigObject}");
        }
        #endregion

        public static void MountConfig(bool IsMount)
        {
            CheckConfig();

            try
            {
                if (IsMount)
                {
                    JsonConfig = JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText(ConfigFile));
                    IsConfigMounted = true;
                }
                else
                {
                    throw new Exception();
                }
            }
            catch
            {
                JsonConfig = null;
                IsConfigMounted = false;
            }
        }

        public static bool IsValidData(DateTime ExamTime)
        {
            if (ExamTime < new DateTime(1753, 1, 1, 0, 0, 0) || ExamTime > new DateTime(9998, 12, 31, 23, 59, 59))
            {
                return false;
            }
            return true;
        }
    }
}
