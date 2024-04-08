using System;
using System.Diagnostics;

namespace CEETimerCSharpWinForms.Modules
{
    public class MemoryManager
    {
        public static void OptimizeMemory(object state)
        {
            try
            {
                Process ProcessGetCurrentMemory = Process.Start(new ProcessStartInfo
                {
                    FileName = "powershell.exe",
                    Arguments = $"-Command (Get-Counter \\\"\\Process({LaunchManager.OriginalFileName.Replace(".exe", "")})\\Working Set - Private\\\").CounterSamples.CookedValue",
                    WindowStyle = ProcessWindowStyle.Hidden,
                    CreateNoWindow = true,
                    RedirectStandardOutput = true,
                    UseShellExecute = false
                });

                ProcessGetCurrentMemory.WaitForExit();
                int MemoryUsage = int.Parse(ProcessGetCurrentMemory.StandardOutput.ReadToEnd().Trim());

                if (MemoryUsage > 9437184) // 9 MB
                {
                    throw new Exception();
                }
            }
            catch
            {
                WindowsAPI.EmptyWorkingSet(Process.GetCurrentProcess().Handle);
            }
        }
    }
}
