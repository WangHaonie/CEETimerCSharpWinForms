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
        private bool IsCancelled = false;
        private CancellationTokenSource CancelRequest;

        public FormDownloader()
        {
            InitializeComponent();
            FormSettings.ConfigChanged += RefreshSettings;
        }

        private async void FormDownloader_Load(object sender, EventArgs e)
        {
            RefreshSettings(sender, e);
            await DownloadUpdate();
        }

        private void RefreshSettings(object sender, EventArgs e)
        {
            ConfigManager.SetTopMost(this);
        }

        public async Task DownloadUpdate()
        {
            string LatestVersion = SimpleUpdater.CurrentLatest;
            string DownloadUrl = $"https://wanghaonie.github.io/file-storages/github-repos/CEETimerCSharpWinForms/CEETimerCSharpWinFoms_{LatestVersion}_x64_Setup.exe";
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
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = "cmd.exe",
                        Arguments = $"/c start \"\" \"{DownloadPath}\" /S",
                        CreateNoWindow = true,
                        WindowStyle = ProcessWindowStyle.Hidden
                    });
                    Close();
                    Environment.Exit(0);
                }
            }
            catch (Exception ex)
            {
                MessageX.Popup($"无法下载更新文件! \n\n错误信息: \n{ex.Message}\n\n错误详情: \n{ex}", MessageLevel.Error);
                LabelDownloading.Text = "下载失败，你可以点击 重试 重新启动下载。";
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
            if (CancelRequest != null && !CancelRequest.Token.IsCancellationRequested)
            {
                MessageX.Popup($"你已取消下载！\n\n你稍后可以在 关于 窗口点击版本号来再次检查更新。", MessageLevel.Warning);
                CancelRequest?.Cancel();
                LabelDownloading.Text = "用户已取消下载。";
            }

            FormClosing -= FormDownloader_FormClosing;
            Close();
        }

        private void FormDownloader_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
        }
    }
}