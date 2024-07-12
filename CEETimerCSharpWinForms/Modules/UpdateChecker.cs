using CEETimerCSharpWinForms.Forms;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Windows.Forms;

namespace CEETimerCSharpWinForms.Modules
{
    public static class UpdateChecker
    {
        public static void CheckUpdate(bool IsProgramStart, Form OwnerForm)
        {
            new Updater().CheckUpdate(IsProgramStart, OwnerForm);
        }
    }

    public class Updater
    {
        public static string CurrentLatest { get; private set; }
        public static long UpdateSize { get; private set; } = 0;

        private DownloaderForm FormDownloader;

        public void CheckUpdate(bool IsProgramStart, Form OwnerForm)
        {
            using var _HttpClient = new HttpClient();
            _HttpClient.DefaultRequestHeaders.UserAgent.ParseAdd(LaunchManager.RequestUA);

            try
            {
                string ResponseContent = _HttpClient.GetAsync("https://gitee.com/wanghaonie/CEETimerCSharpWinForms/raw/main/api/github.json").Result.EnsureSuccessStatusCode().Content.ReadAsStringAsync().Result;
                string CurrentLatest = CurrentLatest = JObject.Parse(ResponseContent)["name"].ToString();
                DateTime.TryParse(JObject.Parse(ResponseContent)["published_at"].ToString(), out DateTime result);
                string PublishTime = result.AddHours(8).ToString(LaunchManager.DateTimeFormat);
                string UpdateLog = JObject.Parse(ResponseContent)["body"].ToString().FormatLog(CurrentLatest);
                UpdateSize = int.Parse(JObject.Parse(ResponseContent)["size"].ToString());

                if (Version.Parse(CurrentLatest) > Version.Parse(LaunchManager.AppVersion))
                {
                    OwnerForm.Invoke(() =>
                    {
                        if (MessageX.Popup($"检测到新版本，是否下载并安装？\n\n当前版本: v{LaunchManager.AppVersion}\n最新版本: v{CurrentLatest}\n发布日期: {PublishTime}\n\nv{CurrentLatest}更新日志: {UpdateLog}", MessageLevel.Info, OwnerForm, Buttons: MessageBoxExButtons.YesNo, Position: IsProgramStart ? FormStartPosition.CenterScreen : FormStartPosition.CenterParent) == DialogResult.Yes)
                        {
                            if (FormDownloader == null || FormDownloader.IsDisposed)
                            {
                                FormDownloader = new();
                            }

                            FormDownloader.ReActivate();
                        }
                    });
                }
                else if (!IsProgramStart)
                {
                    MessageX.Popup($"当前 v{LaunchManager.AppVersion} 已是最新版本。\n\n获取到的版本: v{CurrentLatest}\n发布日期: {PublishTime}\n\n当前版本更新日志: {UpdateLog}", MessageLevel.Info, OwnerForm);
                }
            }
            catch (Exception ex)
            {
                if (!IsProgramStart)
                {
                    MessageX.Popup($"检查更新时发生错误! {ex.ToMessage()}", MessageLevel.Error, OwnerForm);
                }
            }
        }
    }
}
