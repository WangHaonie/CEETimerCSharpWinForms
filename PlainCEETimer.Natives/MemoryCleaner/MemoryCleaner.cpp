#include "pch.h"
#include "MemoryCleaner.h"

static HANDLE hwProc;

void CleanMemory(int threshold)
{
    PROCESS_MEMORY_COUNTERS_EX pmc = {};

    if (!hwProc)
    {
        hwProc = GetCurrentProcess();
    }

    if (GetProcessMemoryInfo(hwProc, (PROCESS_MEMORY_COUNTERS*)&pmc, sizeof(pmc)))
    {
        if (pmc.PrivateUsage > threshold)
        {
            EmptyWorkingSet(hwProc);
        }
    }
}
