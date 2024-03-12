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

        private const string GitHubAPI = "https://wanghaonie.github.io/static-pages/api/software-update/CEETimerCSharpWinForms/get/";
        private static FormDownloader DownloaderForm;

        public static void Start(bool IsProgramStart)
        {
            using var HttpClienMain = new HttpClient();
            HttpClienMain.DefaultRequestHeaders.UserAgent.ParseAdd(LaunchManager.RequestUA);

            try
            {
                string ResponseContent = HttpClienMain.GetAsync(GitHubAPI).Result.EnsureSuccessStatusCode().Content.ReadAsStringAsync().Result;
                LatestVersion = JObject.Parse(ResponseContent)["v"].ToString();

                if (Version.Parse(LatestVersion) > Version.Parse(LaunchManager.AppVersion))
                {
                    FormMain MainForm = Application.OpenForms[0] as FormMain;
                    MainForm.Invoke(new Action(() =>
                    {
                        DialogResult Result = MessageBox.Show($"检测到新版本，是否下载并安装？\n当前版本: v{LaunchManager.AppVersion}\n新版本: v{LatestVersion}", LaunchManager.InfoMsg, MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                        if (Result == DialogResult.Yes)
                        {
                            if (DownloaderForm == null || DownloaderForm.IsDisposed)
                            {
                                DownloaderForm = new FormDownloader();
                            }

                            DownloaderForm.WindowState = FormWindowState.Normal;
                            DownloaderForm.Show();
                            DownloaderForm.Activate();
                        }
                    }));
                }
                else if (!IsProgramStart)
                {
                    MessageBox.Show($"当前 v{LaunchManager.AppVersion} 已是最新版本。\n\n获取到的版本：v{LatestVersion}。", LaunchManager.InfoMsg, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                if (!IsProgramStart)
                {
                    MessageBox.Show($"检查更新时发生错误! \n\n错误信息：\n{ex.Message}\n\n错误详情：\n{ex}", LaunchManager.ErrMsg, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
