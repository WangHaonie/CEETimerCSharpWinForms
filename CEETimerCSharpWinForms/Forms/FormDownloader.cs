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
        private string latestVersion;
        private HttpClient httpClient;
        private string downloadUrl;
        private string downloadPath;
        private bool isCancelled = false;
        public FormDownloader()
        {
            InitializeComponent();
        }
        private async void FormDownloader_Load(object sender, EventArgs e)
        {
            httpClient = new HttpClient();
            try
            {
                latestVersion = CheckForUpdate.LatestVersion;
                downloadUrl = $"https://wanghaonie.github.io/file-storages/github-repos/CEETimerCSharpWinForms/CEETimerCSharpWinForms_{latestVersion}_x64_Setup.exe";
                downloadPath = Path.Combine(Path.GetTempPath(), $"CEETimerCSharpWinForms_{latestVersion}_x64_Setup.exe");
                await DownloadFile(downloadUrl, downloadPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"更新文件下载失败! \n\n系统信息: \n{ex.Message}", $"{LaunchManager.ErrMsg}", MessageBoxButtons.OK, MessageBoxIcon.Error);
                FrmDlL1.Text = "下载失败，你可以点击 链接 跳转到浏览器进行手动下载。";
                FrmDlBtnR.Enabled = true;
                return;
            }
            finally
            {
                httpClient.Dispose();
            }
        }
        private CancellationTokenSource cancelRequest;
        private async Task DownloadFile(string url, string filePath)
        {
            cancelRequest = new CancellationTokenSource();
            httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/119.0.0.0 Safari/537.36 Edg/119.0.0.0");
            using (var response = await httpClient.GetAsync(url, HttpCompletionOption.ResponseHeadersRead, cancelRequest.Token))
            {
                response.EnsureSuccessStatusCode();

                using var stream = await response.Content.ReadAsStreamAsync();
                using var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None);
                var buffer = new byte[8192];
                var totalBytesRead = 0L;
                var totalBytes = response.Content.Headers.ContentLength ?? -1L;
                var bytesRead = 0L;
                var sw = Stopwatch.StartNew();

                while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                {
                    await fileStream.WriteAsync(buffer, 0, (int)bytesRead);
                    totalBytesRead += bytesRead;

                    var progressPercentage = (int)(totalBytesRead * 100 / totalBytes);
                    FrmDlL3.Text = $"已下载/总共：{totalBytesRead / 1024} KB / {totalBytes / 1024} KB";
                    FrmDlL4.Text = $"下载速度：{totalBytesRead / sw.Elapsed.TotalSeconds / 1024 :N2} KB/s";
                    FrmDlPb.Value = progressPercentage;

                    if (cancelRequest.Token.IsCancellationRequested)
                    {
                        isCancelled = true;
                        fileStream.Close();
                        if (File.Exists(filePath))
                        {
                            File.Delete(filePath);
                        }
                        return;
                    }
                }
            }
            if (!isCancelled)
            {
                ProcessStartInfo processStartInfo = new()
                {
                    FileName = "cmd.exe",
                    Arguments = "/c start " + "\"\" \"" + filePath + "\" /S",
                    CreateNoWindow = true,
                    WindowStyle = ProcessWindowStyle.Hidden
                };
                Process.Start(processStartInfo);
                Close();
            }
        }
        private void FrmDlBtnC_Click(object sender, EventArgs e)
        {
            if (cancelRequest != null && !cancelRequest.Token.IsCancellationRequested)
            {
                cancelRequest?.Cancel();
                MessageBox.Show($"你已取消下载！\n\n你稍后可以在 关于 窗口点击版本号来再次检查更新。", $"{LaunchManager.WarnMsg}", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            Close();
        }
        private void FrmDlBtnR_Click(object sender, EventArgs e)
        {
            Process.Start(downloadUrl);
            Close();
        }
    }
}