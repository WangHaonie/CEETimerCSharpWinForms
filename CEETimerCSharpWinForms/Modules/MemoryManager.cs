using System;
using System.Diagnostics;

namespace CEETimerCSharpWinForms.Modules
{
    public static class MemoryManager
    {
        public static void OptimizeMemory(object state)
        {
            try
            {
                Process ProcessGetCurrentMemory = ProcessHelper.RunProcess("powershell.exe", $"-Command (Get-Counter \\\"\\Process({LaunchManager.OriginalFileName.Replace(".exe", "")})\\Working Set - Private\\\").CounterSamples.CookedValue", RedirectOutput: true);

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
