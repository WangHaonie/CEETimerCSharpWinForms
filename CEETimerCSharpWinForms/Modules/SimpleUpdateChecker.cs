using CEETimerCSharpWinForms.Forms;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Windows.Forms;

namespace CEETimerCSharpWinForms.Modules
{
    public class SimpleUpdateChecker
    {
        public static string CurrentLatest { get; private set; }
        private const string GitHubAPI = "https://api.github.com/repos/WangHaonie/CEETimerCSharpWinForms/releases/latest";

        public static void CheckUpdate(bool IsProgramStart, Form ParentForm)
        {
            using var HttpClienMain = new HttpClient();
            HttpClienMain.DefaultRequestHeaders.UserAgent.ParseAdd(LaunchManager.RequestUA);

            try
            {
                string ResponseContent = HttpClienMain.GetAsync(GitHubAPI).Result.EnsureSuccessStatusCode().Content.ReadAsStringAsync().Result;
                CurrentLatest = JObject.Parse(ResponseContent)["name"].ToString();
                DateTime.TryParse(JObject.Parse(ResponseContent)["published_at"].ToString(), out DateTime result);
                string PublishTime = result.AddHours(8).ToString("yyyy-MM-dd dddd HH:mm:ss");
                string UpdateLog = JObject.Parse(ResponseContent)["body"].ToString().RemoveInvalidLogChars(CurrentLatest);

                if (Version.Parse(CurrentLatest) > Version.Parse(LaunchManager.AppVersion))
                {
                    FormMain MainForm = Application.OpenForms[0] as FormMain;
                    MainForm.Invoke(new Action(() =>
                    {
                        if (MessageX.Popup($"检测到新版本，是否下载并安装？\n\n当前版本: v{LaunchManager.AppVersion}\n最新版本: v{CurrentLatest}\n发布日期: {PublishTime}\n\nv{CurrentLatest}更新日志: {UpdateLog}", MessageLevel.Info, Buttons: MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            SingleInstanceRunner<FormDownloader>.GetInstance().Show();
                        }
                    }));
                }
                else if (!IsProgramStart)
                {
                    MessageX.Popup($"当前 v{LaunchManager.AppVersion} 已是最新版本。\n\n获取到的版本：v{CurrentLatest}\n发布日期: {PublishTime}\n\n当前版本更新日志: {UpdateLog}", MessageLevel.Info, ParentForm);
                }
            }
            catch (Exception ex)
            {
                if (!IsProgramStart)
                {
                    MessageX.Popup($"检查更新时发生错误! ", ex, ParentForm);
                }
            }
        }
    }
}
