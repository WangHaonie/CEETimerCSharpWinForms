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
        private readonly string ConfigFile = $"{LaunchManager.CurrentExecutablePath}CEETimerCSharpWinForms.cfg";

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
        public const string ExamName = "9A3F";
        public const string StartTime = "C4E1";
        public const string EndTime = "2B78";
        public const string MemOpti = "F5D2";
        public const string TopMost = "4136";
        public const string ShowOnly = "E8B9";
        public const string ShowValue = "7D24";
        public const string Rounding = "3C5F";
        public const string ShowEnd = "A9E8";
        public const string ShowPast = "6D1B";
        public const string UniTopMost = "B2F4";
        public const string Screen = "8C73";
        public const string Position = "1D5A";
        public const string Dragable = "5E9C";
        public const string SeewoPptSvc = "D3F1";
        public const string Font = "7286";
        public const string FontStyle = "4A8B";
        public const string Fore1 = "9C2E";
        public const string Back1 = "6F3D";
        public const string Fore2 = "B1F5";
        public const string Back2 = "2D8A";
        public const string Fore3 = "E45C";
        public const string Back3 = "8179";
        public const string Fore4 = "3B6F";
        public const string Back4 = "7A2D";
        public const string PosX = "F4E3";
        public const string PosY = "B337";
    }

    public static class ConfigPolicy
    {
        public const int MinExamNameLength = 2;
        public const int MaxExamNameLength = 10;
        public const int MinFontSize = 10;
        public const int MaxFontSize = 24;
        public const string DefaultFont = "Microsoft YaHei, 17.25pt";
    }
}
