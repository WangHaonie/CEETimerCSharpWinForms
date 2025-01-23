﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace CEETimerCSharpWinForms.Modules
{
    public class ConfigManager
    {
        #region 来自网络
        /* 
        
        JSON 配置读写参考:

        C#--使用json配置文件方法【读写Json，适合小项目】 - 包子789654 - 博客园
        https://www.cnblogs.com/baozi789654/p/15645897.html

        */
        private bool IsConfigMounted;
        private Dictionary<string, object> JsonConfig;
        private JObject ConfigObject;
        private static readonly List<string> AllowedKeys;
        private readonly string ConfigFile = AppLauncher.ConfigFilePath;

        static ConfigManager()
        {
            #region 来自网络
            /*
            
            获取 ConfigItems 类里的所有 const string 的值 参考:

            c# - How can I  get all constants of a type by reflection? - Stack Overflow
            https://stackoverflow.com/a/41618045/21094697

            */
            AllowedKeys = typeof(ConfigItems).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy).Where(fi => fi.IsLiteral && !fi.IsInitOnly).Select(x => x.GetRawConstantValue()).OfType<string>().ToList();
            #endregion
        }

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
                if (Key == ConfigItems.KFont)
                {
                    return (string)JsonConfig[ConfigItems.KFont];
                }
                else
                {
                    return ((string)JsonConfig[Key]).RemoveIllegalChars();
                }
            }
            else
            {
                return string.Empty;
            }
        }

        public IEnumerable<CustomRuleHelper.Config> ReadConfigEx(string Key)
        {
            if (IsConfigMounted && JsonConfig != null && JsonConfig.ContainsKey(Key))
            {
                if (JsonConfig[Key] is JArray arr)
                {
                    return arr.ToObject<IEnumerable<CustomRuleHelper.Config>>();
                }
            }

            return [];
        }

        public void WriteConfig(Dictionary<string, object> DataSet)
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

            File.WriteAllText(ConfigFile, CleanKeys(ConfigObject));
        }
        #endregion

        public void MountConfig(bool IsMount)
        {
            CheckConfig();

            try
            {
                if (IsMount)
                {
                    JsonConfig = JsonConvert.DeserializeObject<Dictionary<string, object>>(File.ReadAllText(ConfigFile));
                    IsConfigMounted = true;
                }
                else
                {
                    ConfigPolicy.NotAllowed<Dictionary<string, object>>();
                }
            }
            catch
            {
                JsonConfig = null;
                IsConfigMounted = false;
            }
        }

        private string CleanKeys(JObject o)
        {
            List<string> KeysToClean = [];
            JObject JSorted = [];

            foreach (var Property in o.Properties())
            {
                if (!AllowedKeys.Contains(Property.Name))
                {
                    KeysToClean.Add(Property.Name);
                }
            }

            foreach (var Key in KeysToClean)
            {
                o.Remove(Key);
            }

            foreach (var Key in AllowedKeys)
            {
                if (o.ContainsKey(Key))
                {
                    JSorted[Key] = o[Key];
                }
            }

            return JSorted.ToString(Formatting.None);
        }
    }

    public static class ConfigItems
    {
        public const string KExamName = "ExamName";
        public const string KStartTime = "StartTime";
        public const string KEndTime = "EndTime";
        public const string KMemOpti = "MemOpti";
        public const string KTopMost = "TopMost";
        public const string KUniTopMost = "UniTopMost";
        public const string KShowXOnly = "ShowXOnly";
        public const string KShowValue = "X";
        public const string KRounding = "Rounding";
        public const string KShowEnd = "ShowEnd";
        public const string KShowPast = "ShowPast";
        public const string KIsCustomText = "Custom";
        public const string KCustomTextP1 = "CustomP1";
        public const string KCustomTextP2 = "CustomP2";
        public const string KCustomTextP3 = "CustomP3";
        public const string KScreen = "Screen";
        public const string KPosition = "Position";
        public const string KDraggable = "Draggable";
        public const string KSeewoPptSvc = "SeewoPptSvc";
        public const string KFont = "Font";
        public const string KFontStyle = "FontStyle";
        public const string KFore1 = "Fore1";
        public const string KBack1 = "Back1";
        public const string KFore2 = "Fore2";
        public const string KBack2 = "Back2";
        public const string KFore3 = "Fore3";
        public const string KBack3 = "Back3";
        public const string KFore4 = "Fore4";
        public const string KBack4 = "Back4";
        public const string KCustomColors = "CustomColors";
        public const string KCustomRules = "CustomRules";
        public const string KPosX = "PosX";
        public const string KPosY = "PosY";
    }

    public static class ConfigPolicy
    {
        public const int MinExamNameLength = 2;
        public const int MaxExamNameLength = 10;
        public const int MinCustomTextLength = 0;
        public const int MaxCustomTextLength = 50;
        public const int MinFontSize = 10;
        public const int MaxFontSize = 28;
        public const string DefaultFont = "Microsoft YaHei, 17.25pt";
        public static char[] CharsNotAllowed => ['\\', '/', '*', '?', '"', '\'', '<', '>', '|'];
        public static TimeSpan TsMaxAllowed => new(65535, 23, 59, 59);
        public static TimeSpan TsMinAllowed => new(0, 0, 0, 1);

        public static T NotAllowed<T>()
        {
            throw new Exception();
        }
    }
}
