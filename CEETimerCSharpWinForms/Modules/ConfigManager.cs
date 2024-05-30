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
        private bool IsConfigMounted;
        private Dictionary<string, string> JsonConfig;
        private JObject ConfigObject;
        private readonly string ConfigFile = $"{LaunchManager.CurrentExecutablePath}{LaunchManager.AppNameEn}.config";

        private void CheckConfig()
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

        public string ReadConfig(string Key)
        {
            if (IsConfigMounted && JsonConfig != null && JsonConfig.ContainsKey(Key))
            {
                if (Key == ConfigItems.Font)
                {
                    return JsonConfig[ConfigItems.Font];
                }
                else
                {
                    return JsonConfig[Key].RemoveIllegalChars();
                }
            }
            else
            {
                return string.Empty;
            }
        }

        public void WriteConfig(Dictionary<string, string> DataSet)
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

        public void MountConfig(bool IsMount)
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

        public bool IsValidData(DateTime ExamTime)
        {
            if (ExamTime < new DateTime(1753, 1, 1, 0, 0, 0) || ExamTime > new DateTime(9998, 12, 31, 23, 59, 59))
            {
                return false;
            }
            return true;
        }
    }

    public static class ConfigItems
    {
        public const string ExamName = "ExamName";
        public const string StartTime = "StartTime";
        public const string EndTime = "EndTime";
        public const string MemOpti = "MemOpti";
        public const string TopMost = "TopMost";
        public const string ShowOnly = "ShowOnly";
        public const string ShowValue = "Show";
        public const string Rounding = "Rounding";
        public const string ShowEnd = "ShowEnd";
        public const string ShowPast = "ShowPast";
        public const string UniTopMost = "UniTopMost";
        public const string Screen = "Screen";
        public const string Position = "Position";
        public const string Dragable = "Dragable";
        public const string SeewoPptSvc = "SeewoPptSvc";
        public const string Font = "Font";
        public const string FontStyle = "FontStyle";
        public const string Fore1 = "Fore1";
        public const string Back1 = "Back1";
        public const string Fore2 = "Fore2";
        public const string Back2 = "Back2";
        public const string Fore3 = "Fore3";
        public const string Back3 = "Back3";
        public const string Fore4 = "Fore4";
        public const string Back4 = "Back4";
        public const string PosX = "PosX";
        public const string PosY = "PosY";
    }

    public static class ConfigPolicy
    {
        public const int MinExamNameLength = 2;
        public const int MaxExamNameLength = 10;
        public const int MinFontSize = 10;
        public const int MaxFontSize = 28;
        public const string DefaultFont = "Microsoft YaHei, 17.25pt";
    }
}
