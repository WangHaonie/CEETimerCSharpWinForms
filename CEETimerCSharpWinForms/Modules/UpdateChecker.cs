using CEETimerCSharpWinForms.Forms;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Windows.Forms;

namespace CEETimerCSharpWinForms.Modules
{
    public static class UpdateChecker
    {
        public static void CheckUpdate(bool IsProgramStart, Form ParentForm)
        {
            new Updater().CheckUpdate(IsProgramStart, ParentForm);
        }
    }

    public class Updater
    {
        private FormDownloader _FormDownloader;

        public void CheckUpdate(bool IsProgramStart, Form ParentForm)
        {
            using var _HttpClient = new HttpClient();
            _HttpClient.DefaultRequestHeaders.UserAgent.ParseAdd(LaunchManager.RequestUA);

            try
            {
                string ResponseContent = _HttpClient.GetAsync("https://api.github.com/repos/WangHaonie/CEETimerCSharpWinForms/releases/latest").Result.EnsureSuccessStatusCode().Content.ReadAsStringAsync().Result;
                string CurrentLatest = LaunchManager.CurrentLatest = JObject.Parse(ResponseContent)["name"].ToString();
                DateTime.TryParse(JObject.Parse(ResponseContent)["published_at"].ToString(), out DateTime result);
                string PublishTime = result.AddHours(8).ToString("yyyy-MM-dd dddd HH:mm:ss");
                string UpdateLog = JObject.Parse(ResponseContent)["body"].ToString().FormatLog(CurrentLatest);

                if (Version.Parse(CurrentLatest) > Version.Parse(LaunchManager.AppVersion))
                {
                    ParentForm.Invoke(new Action(() =>
                    {
                        if (MessageX.Popup($"检测到新版本，是否下载并安装？\n\n当前版本: v{LaunchManager.AppVersion}\n最新版本: v{CurrentLatest}\n发布日期: {PublishTime}\n\nv{CurrentLatest}更新日志: {UpdateLog}", MessageLevel.Info, ParentForm, Buttons: MessageBoxExButtons.YesNo, Position: IsProgramStart ? FormStartPosition.CenterScreen : FormStartPosition.CenterParent) == DialogResult.Yes)
                        {
                            if (_FormDownloader == null || _FormDownloader.IsDisposed)
                            {
                                _FormDownloader = new FormDownloader();
                            }

                            _FormDownloader.WindowState = FormWindowState.Normal;
                            _FormDownloader.Show();
                            _FormDownloader.Activate();
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
