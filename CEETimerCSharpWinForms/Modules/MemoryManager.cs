using System.Diagnostics;

namespace CEETimerCSharpWinForms.Modules
{
    public class MemoryManager
    {
        public static void OptimizeMemory(object state)
        {
            Process ProcessGetCurrentMemory = Process.Start(new ProcessStartInfo
            {
                FileName = "tasklist.exe",
                Arguments = $"/fi \"PID eq {Process.GetCurrentProcess().Id}\" /fo csv /nh",
                WindowStyle = ProcessWindowStyle.Hidden,
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                UseShellExecute = false
            });

            var Output = ProcessGetCurrentMemory.StandardOutput.ReadToEnd().Trim().Split('"');
            ProcessGetCurrentMemory.WaitForExit();
            int MemoryUsage = int.Parse(Output[9].Replace(",", "").Replace("K", "").Trim());

            if (MemoryUsage > 12288)
            {
                WindowsAPI.EmptyWorkingSet(Process.GetCurrentProcess().Handle);
            }
        }
    }
}
