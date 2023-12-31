﻿using CEETimerCSharpWinForms.Forms;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Windows.Forms;

namespace CEETimerCSharpWinForms.Modules
{
    public class CheckForUpdate
    {
        public static string LatestVersion { get; private set; }
        public const string GitHubAPI = "https://api.github.com/repos/WangHaonie/CEETimerCSharpWinForms/releases/latest";
        public static void Start(bool isProgramStart)
        {
            using (HttpClient updateCheck = new HttpClient())
            {
                updateCheck.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/119.0.0.0 Safari/537.36 Edg/119.0.0.0");
                try
                {
                    HttpResponseMessage response = updateCheck.GetAsync(GitHubAPI).Result;
                    response.EnsureSuccessStatusCode();
                    string apiRes = response.Content.ReadAsStringAsync().Result;
                    JObject release = JObject.Parse(apiRes);
                    LatestVersion = release["name"].ToString();
                    if (Version.Parse(LatestVersion) > Version.Parse(LaunchManager.AppVersion))
                    {
                        DialogResult result = DialogResult.Yes;

                        if (isProgramStart || !isProgramStart)
                        {
                            result = MessageBox.Show($"检测到新版本，是否下载并安装？\n当前版本: v{LaunchManager.AppVersion}\n新版本: v{LatestVersion}", $"{LaunchManager.InfoMsg}", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        }

                        if (result == DialogResult.Yes)
                        {
                            Application.Run(new FormDownloader());
                        }
                    }
                    else if (!isProgramStart)
                    {
                        MessageBox.Show($"当前 v{LaunchManager.AppVersion} 已是最新版本。\n\n获取到的版本：v{LatestVersion}。", $"{LaunchManager.InfoMsg}", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    if (!isProgramStart)
                    {
                        MessageBox.Show($"检查更新时发生错误! \n\n系统信息：\n" + ex.Message, $"{LaunchManager.ErrMsg}", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                finally
                {
                    updateCheck.Dispose();
                }
            }
        }
    }
}
