using CEETimerCSharpWinForms.Controls;
using CEETimerCSharpWinForms.Modules;
using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CEETimerCSharpWinForms.Forms
{
    public partial class DownloaderForm : TrackableForm
    {
        public static string ManualVersion { get; set; } = LaunchManager.AppVersion;

        private bool IsCancelled;
        private CancellationTokenSource cts;
        private string DownloadUrl;
        private string DownloadPath;

        public DownloaderForm()
        {
            InitializeComponent();
        }

        protected override async void OnTrackableFormLoad()
        {
            base.OnTrackableFormLoad();

            UIHelper.AlignControlsR(LinkBroswer, ProgressBarMain);
            UIHelper.AlignControlsREx(ButtonRetry, ButtonCancel, ProgressBarMain);

            string LatestVersion = Updater.LatestVersion;
            string SelectedVersion = ManualVersion;

            if (string.IsNullOrWhiteSpace(LatestVersion))
            {
                LatestVersion = SelectedVersion.IsVersionNumber() ? SelectedVersion : LaunchManager.AppVersion;
            }

            DownloadUrl = string.Format("https://gitee.com/WangHaonie/CEETimerCSharpWinForms/raw/main/download/CEETimerCSharpWinForms_{0}_x64_Setup.exe", LatestVersion);
            DownloadPath = Path.Combine(Path.GetTempPath(), Path.GetFileName(new Uri(DownloadUrl).AbsolutePath));

            await DownloadUpdate();
        }

        private async Task DownloadUpdate()
        {
            IsCancelled = false;

            using var httpClient = new HttpClient();
            cts = new();
            httpClient.DefaultRequestHeaders.UserAgent.ParseAdd(LaunchManager.RequestUA);

            try
            {
                using (var response = await httpClient.GetAsync(DownloadUrl, HttpCompletionOption.ResponseHeadersRead, cts.Token))
                {
                    response.EnsureSuccessStatusCode();
                    using var stream = await response.Content.ReadAsStreamAsync();
                    using var fileStream = new FileStream(DownloadPath, FileMode.Create, FileAccess.Write, FileShare.None);
                    var buffer = new byte[8192];
                    var totalBytesRead = 0L;
                    var bytesRead = 0L;
                    var sw = Stopwatch.StartNew();
                    var size = Updater.UpdateSize;
                    var totalBytes = response.Content.Headers.ContentLength ?? (size == 0 ? 378880L : size);

                    while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                    {
                        await fileStream.WriteAsync(buffer, 0, (int)bytesRead);
                        totalBytesRead += bytesRead;

                        UpdateUI(totalBytesRead / 1024, totalBytes / 1024, totalBytesRead / sw.Elapsed.TotalSeconds / 1024, (int)(totalBytesRead * 100 / totalBytes));

                        if (cts.Token.IsCancellationRequested)
                        {
                            IsCancelled = true;
                            fileStream.Close();
                            if (File.Exists(DownloadPath))
                            {
                                File.Delete(DownloadPath);
                            }
                            return;
                        }
                    }
                }
                if (!IsCancelled)
                {
                    ButtonCancel.Enabled = false;
                    ButtonRetry.Enabled = false;
                    LinkBroswer.Enabled = false;
                    ProgressBarMain.Value = 100;

                    await Task.Delay(1800);
                    ProcessHelper.RunProcess("cmd.exe", $"/c start \"\" \"{DownloadPath}\" /S");
                    Close();
                    Environment.Exit(0);
                }
            }
            catch (Exception ex)
            {
                IsCancelled = true;

                if (ex is not TaskCanceledException)
                {
                    MessageX.Popup($"无法下载更新文件！{ex.ToMessage()}", MessageLevel.Error);
                    LabelDownloading.Text = "下载失败，你可以点击 重试 来重新启动下载。";
                    LabelSize.Text = "已下载/总共: N/A";
                    LabelSpeed.Text = "下载速度: N/A";
                    ButtonRetry.Enabled = true;
                }

                return;
            }
            finally
            {
                cts?.Dispose();
            }
        }

        private async void ButtonRetry_Click(object sender, EventArgs e)
        {
            ButtonRetry.Enabled = false;
            ProgressBarMain.Value = 0;
            LabelDownloading.Text = "正在重新下载更新文件，请稍侯...";
            LabelSize.Text = "已下载/总共: (获取中...)";
            LabelSpeed.Text = "下载速度: (获取中...)";

            await DownloadUpdate();
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            if (!IsCancelled && cts != null && !cts.Token.IsCancellationRequested)
            {
                ButtonCancel.Enabled = false;
                cts?.Cancel();
                LabelDownloading.Text = "用户已取消下载。";
                IsCancelled = true;
                MessageX.Popup("你已取消下载！\n\n稍后可以在 关于 窗口点击图标来再次检查更新。", MessageLevel.Warning);
            }

            Close();
        }

        private async void LinkBroswer_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkBroswer.Enabled = false;
            Process.Start(DownloadUrl);
            await Task.Delay(3000);
            LinkBroswer.Enabled = true;
        }

        private void UpdateUI(long Downloaded, long Total, double Speed, int Progress)
        {
            LabelSize.Text = $"已下载/总共: {Downloaded} KB / {Total} KB";
            LabelSpeed.Text = $"下载速度: {Speed:0.00} KB/s";
            ProgressBarMain.Value = Progress;
        }

        protected override void OnTrackableFormClosing(FormClosingEventArgs e)
        {
            e.Cancel = !IsCancelled;
        }
    }
}