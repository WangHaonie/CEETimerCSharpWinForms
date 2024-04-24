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
    public partial class FormDownloader : Form
    {
        private bool IsCancelled;
        private CancellationTokenSource CancelRequest;

        public FormDownloader()
        {
            InitializeComponent();
        }

        private async void FormDownloader_Load(object sender, EventArgs e)
        {
            TopMost = FormMain.IsUniTopMost;
            await DownloadUpdate();
        }

        public async Task DownloadUpdate()
        {
            IsCancelled = false;
            string LatestVersion = LaunchManager.CurrentLatest;
            string DownloadUrl = $"https://wanghaonie.github.io/file-storages/github-repos/CEETimerCSharpWinForms/CEETimerCSharpWinForms_{LatestVersion}_x64_Setup.exe";
            string DownloadPath = Path.Combine(Path.GetTempPath(), $"CEETimerCSharpWinForms_{LatestVersion}_x64_Setup.exe");

            using var UpdateChecker = new HttpClient();
            CancelRequest = new CancellationTokenSource();
            UpdateChecker.DefaultRequestHeaders.UserAgent.ParseAdd(LaunchManager.RequestUA);

            try
            {
                using (var response = await UpdateChecker.GetAsync(DownloadUrl, HttpCompletionOption.ResponseHeadersRead, CancelRequest.Token))
                {
                    response.EnsureSuccessStatusCode();
                    using var stream = await response.Content.ReadAsStreamAsync();
                    using var fileStream = new FileStream(DownloadPath, FileMode.Create, FileAccess.Write, FileShare.None);
                    var buffer = new byte[8192];
                    var totalBytesRead = 0L;
                    var totalBytes = response.Content.Headers.ContentLength ?? -1L;
                    var bytesRead = 0L;
                    var sw = Stopwatch.StartNew();

                    while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                    {
                        await fileStream.WriteAsync(buffer, 0, (int)bytesRead);
                        totalBytesRead += bytesRead;
                        var progressPercentage = totalBytes == -1 ? -1 : (int)(totalBytesRead * 100 / totalBytes);

                        LabelSize.Text = $"已下载/总共：{totalBytesRead / 1024} KB / {totalBytes / 1024} KB";
                        LabelSpeed.Text = $"下载速度：{totalBytesRead / sw.Elapsed.TotalSeconds / 1024:0.00} KB/s";
                        ProgressBarMain.Value = progressPercentage;

                        if (CancelRequest.Token.IsCancellationRequested)
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

                    await Task.Delay(1800);
                    ProcessHelper.RunProcess("cmd.exe", $"/c start \"\" \"{DownloadPath}\" /S");
                    Close();
                    Environment.Exit(0);
                }
            }
            catch (Exception ex)
            {
                IsCancelled = true;
                MessageX.Popup("无法下载更新文件!", ex);
                LabelDownloading.Text = "下载失败，你可以点击 重试 来重新启动下载。";
                LabelSize.Text = "已下载/总共：N/A";
                LabelSpeed.Text = "下载速度：N/A";
                ButtonRetry.Enabled = true;
                return;
            }
        }

        private async void ButtonRetry_Click(object sender, EventArgs e)
        {
            ButtonRetry.Enabled = false;
            LabelDownloading.Text = "正在重新下载更新文件，请稍侯...";
            LabelSize.Text = "已下载/总共：(获取中...)";
            LabelSpeed.Text = "下载速度：(获取中...)";

            await DownloadUpdate();
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            if (!IsCancelled && CancelRequest != null && !CancelRequest.Token.IsCancellationRequested)
            {
                ButtonCancel.Enabled = false;
                CancelRequest?.Cancel();
                LabelDownloading.Text = "用户已取消下载。";
                MessageX.Popup($"你已取消下载！\n\n稍后可以在 关于 窗口点击版本号来再次检查更新。", MessageLevel.Warning);
            }

            Close();
        }

        private void FormDownloader_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = !IsCancelled;
        }
    }
}