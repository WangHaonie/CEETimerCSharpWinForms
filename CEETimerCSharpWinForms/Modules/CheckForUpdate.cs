using CEETimerCSharpWinForms.Forms;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Windows.Forms;

namespace CEETimerCSharpWinForms.Modules
{
    public class CheckForUpdate
    {
        public static string LatestVersion { get; private set; }
        public const string GitHubAPI = "https://wanghaonie.github.io/static-pages/api/software-update/CEETimerCSharpWinForms/get/";
        private static FormDownloader formDownloader;
        public static void Start(bool isProgramStart)
        {
            using var updateCheck = new HttpClient();
            updateCheck.DefaultRequestHeaders.UserAgent.ParseAdd(LaunchManager.RequestUa);
            try
            {
                HttpResponseMessage response = updateCheck.GetAsync(GitHubAPI).Result;
                response.EnsureSuccessStatusCode();
                string apiRes = response.Content.ReadAsStringAsync().Result;
                JObject release = JObject.Parse(apiRes);
                LatestVersion = release["v"].ToString();
                if (Version.Parse(LatestVersion) > Version.Parse(LaunchManager.AppVersion))
                {
                    CEETimerCSharpWinForms MainForm = Application.OpenForms[0] as CEETimerCSharpWinForms;
                    MainForm.Invoke(new Action(() =>
                    {
                        DialogResult result = MessageBox.Show($"检测到新版本，是否下载并安装？\n当前版本: v{LaunchManager.AppVersion}\n新版本: v{LatestVersion}", LaunchManager.InfoMsg, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (result == DialogResult.Yes)
                        {
                            if (formDownloader == null || formDownloader.IsDisposed)
                            {
                                formDownloader = new FormDownloader();
                            }
                            formDownloader.WindowState = FormWindowState.Normal;
                            formDownloader.Show();
                            formDownloader.Activate();
                        }
                    }));
                }
                else if (!isProgramStart)
                {
                    MessageBox.Show($"当前 v{LaunchManager.AppVersion} 已是最新版本。\n\n获取到的版本：v{LatestVersion}。", LaunchManager.InfoMsg, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                if (!isProgramStart)
                {
                    MessageBox.Show($"检查更新时发生错误! \n\n错误信息：\n{ex.Message}", LaunchManager.ErrMsg, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
