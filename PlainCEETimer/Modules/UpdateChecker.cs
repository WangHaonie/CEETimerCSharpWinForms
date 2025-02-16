using Newtonsoft.Json.Linq;
using PlainCEETimer.Controls;
using PlainCEETimer.Forms;
using System;
using System.Net.Http;
using System.Windows.Forms;

namespace PlainCEETimer.Modules
{
    public static class UpdateChecker
    {
        public static void CheckUpdate(bool IsProgramStart, AppForm OwnerForm)
        {
            new Updater().CheckUpdate(IsProgramStart, OwnerForm);
        }
    }

    public class Updater
    {
        public static string LatestVersion { get; private set; }
        public static long UpdateSize { get; private set; } = 0;

        private DownloaderForm FormDownloader;

        public void CheckUpdate(bool IsProgramStart, AppForm OwnerForm)
        {
            var MessageX = new MessageBoxHelper(OwnerForm);
            using var _HttpClient = new HttpClient();
            _HttpClient.DefaultRequestHeaders.UserAgent.ParseAdd(AppLauncher.RequestUA);

            try
            {
                string ResponseContent = _HttpClient.GetAsync(AppLauncher.UpdateAPI).Result.EnsureSuccessStatusCode().Content.ReadAsStringAsync().Result;
                string CurrentLatest = LatestVersion = JObject.Parse(ResponseContent)["name"].ToString();
                DateTime.TryParse(JObject.Parse(ResponseContent)["published_at"].ToString(), out DateTime result);
                string PublishTime = result.AddHours(8).ToString(AppLauncher.DateTimeFormat);
                string UpdateLog = JObject.Parse(ResponseContent)["body"].ToString().FormatLog(CurrentLatest);
                UpdateSize = int.Parse(JObject.Parse(ResponseContent)["size"].ToString());

                if (Version.Parse(CurrentLatest) > Version.Parse(AppLauncher.AppVersion))
                {
                    if (MessageX.Info($"检测到新版本，是否下载并安装？\n\n当前版本: v{AppLauncher.AppVersion}\n最新版本: v{CurrentLatest}\n发布日期: {PublishTime}\n\nv{CurrentLatest}更新日志: {UpdateLog}", Buttons: MessageBoxExButtons.YesNo) == DialogResult.Yes)
                    {
                        if (FormDownloader == null || FormDownloader.IsDisposed)
                        {
                            FormDownloader = new();
                        }

                        FormDownloader.ReActivate(OwnerForm);
                    }
                }
                else if (!IsProgramStart)
                {
                    MessageX.Info($"当前 v{AppLauncher.AppVersion} 已是最新版本。\n\n获取到的版本: v{CurrentLatest}\n发布日期: {PublishTime}\n\n当前版本更新日志: {UpdateLog}");
                }
            }
            catch (Exception ex)
            {
                if (!IsProgramStart)
                {
                    MessageX.Error($"检查更新时发生错误! {ex.ToMessage()}");
                }
            }
        }
    }
}
