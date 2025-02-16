﻿using PlainCEETimer.Controls;
using PlainCEETimer.Interop;
using PlainCEETimer.Modules;
using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlainCEETimer.Forms
{
    public partial class DownloaderForm : AppForm
    {
        public static string ManualVersion { get; set; } = AppLauncher.AppVersion;

        private bool IsCancelled;
        private CancellationTokenSource cts;
        private string DownloadUrl;
        private string DownloadPath;
        private TaskbarProgress TaskbarProgress;

        public DownloaderForm()
        {
            InitializeComponent();
            AdjustBeforeLoad = true;
        }

        protected override void AdjustUI()
        {
            AlignControlsR(LinkBrowser, ProgressBarMain);
            AlignControlsREx(ButtonRetry, ButtonCancel, ProgressBarMain);
        }

        protected override async void OnLoad()
        {
            TaskbarProgress = new(Handle);
            string LatestVersion = Updater.LatestVersion;
            string SelectedVersion = ManualVersion;

            if (string.IsNullOrWhiteSpace(LatestVersion))
            {
                LatestVersion = SelectedVersion.IsVersionNumber() ? SelectedVersion : AppLauncher.AppVersion;
            }

            TaskbarProgress.SetTaskbarProgressStateEx(TaskbarProgressState.Normal);
            DownloadUrl = string.Format(AppLauncher.UpdateURL, LatestVersion);
            DownloadPath = Path.Combine(Path.GetTempPath(), Path.GetFileName(new Uri(DownloadUrl).AbsolutePath));

            await DownloadUpdate();
        }

        private async Task DownloadUpdate()
        {
            IsCancelled = false;

            using var httpClient = new HttpClient();
            cts = new();
            httpClient.DefaultRequestHeaders.UserAgent.ParseAdd(AppLauncher.RequestUA);

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
                    LinkBrowser.Enabled = false;
                    ProgressBarMain.Value = 100;
                    TaskbarProgress.SetTaskbarProgressValueEx(100UL, 100UL);
                    TaskbarProgress.SetTaskbarProgressStateEx(TaskbarProgressState.Indeterminate);
                    UpdateLabels("下载完成，请稍侯...", null, null);
                    await Task.Delay(2500);
                    IsCancelled = true;
                    Close();
                    ProcessHelper.RunProcess("cmd.exe", $"/c start \"\" \"{DownloadPath}\" /S");
                    AppLauncher.Exit(ExitReason.AppUpdating);
                }
            }
            catch (Exception ex)
            {
                IsCancelled = true;

                if (ex is not TaskCanceledException)
                {
                    MessageX.Error($"无法下载更新文件！{ex.ToMessage()}");
                    UpdateLabels("下载失败，你可以点击 重试 来重新启动下载。", "已下载/总共: N/A", "下载速度: N/A");
                    ButtonRetry.Enabled = true;
                }

                TaskbarProgress.SetTaskbarProgressStateEx(TaskbarProgressState.Error);
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
            UpdateLabels("正在重新下载更新文件，请稍侯...", "已下载/总共: (获取中...)", "下载速度: (获取中...)");

            await DownloadUpdate();
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            if (!IsCancelled && cts != null && !cts.Token.IsCancellationRequested)
            {
                ButtonCancel.Enabled = false;
                cts?.Cancel();
                UpdateLabels("用户已取消下载。", null, null);
                IsCancelled = true;
                TaskbarProgress.SetTaskbarProgressStateEx(TaskbarProgressState.Error);
                MessageX.Warn("你已取消下载！\n\n稍后可以在 关于 窗口点击图标来再次检查更新。");
            }

            Close();
        }

        private async void LinkBrowser_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkBrowser.Enabled = false;
            Process.Start(DownloadUrl);
            await Task.Delay(3000);
            LinkBrowser.Enabled = true;
        }

        private void UpdateUI(long Downloaded, long Total, double Speed, int Progress)
        {
            UpdateLabels(null, $"已下载/总共: {Downloaded} KB / {Total} KB", $"下载速度: {Speed:0.00} KB/s");
            ProgressBarMain.Value = Progress;
            TaskbarProgress.SetTaskbarProgressValueEx((ulong)Downloaded, (ulong)Total);
        }

        private void UpdateLabels(string Info, string Size, string Speed)
        {
            if (!string.IsNullOrEmpty(Info))
            {
                LabelDownloading.Text = Info;
            }

            if (!string.IsNullOrEmpty(Size))
            {
                LabelSize.Text = Size;
            }

            if (!string.IsNullOrEmpty(Speed))
            {
                LabelSpeed.Text = Speed;
            }
        }

        protected override void OnClosing(FormClosingEventArgs e)
        {
            e.Cancel = !IsCancelled;
        }

        protected override void OnClosed()
        {
            if (IsCancelled)
            {
                TaskbarProgress.SetTaskbarProgressStateEx(TaskbarProgressState.None);
                TaskbarProgress.ReleaseTaskbarListEx();
            }
        }
    }
}