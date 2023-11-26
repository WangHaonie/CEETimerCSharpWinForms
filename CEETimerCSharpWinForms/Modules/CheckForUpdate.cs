using System;
using System.Net.Http;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;

namespace CEETimerCSharpWinForms.Modules
{
    public class CheckForUpdate
    {
        public const string GitHubAPI = "https://api.github.com/repos/WangHaonie/CEETimerCSharpWinForms/releases/latest";
        private const string CurrentVersion = "1.7";
        public static void checkForUpdate()
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
                    string lV = release["name"].ToString();
                    if (Version.Parse(lV) > Version.Parse(CurrentVersion))
                    {
                        DialogResult result = MessageBox.Show("检测到新版本，是否跳转下载？", "发现新版本 - 高考倒计时", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (result == DialogResult.Yes)
                        {
                            System.Diagnostics.Process.Start("https://github.com/WangHaonie/CEETimerCSharpWinForms/releases/latest");
                        }
                    }
                }
                catch
                {

                }
            }
        }
        
    }
}
