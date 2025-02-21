#pragma once

#include <windows.h>
#include <psapi.h>

extern "C"
{
	__declspec(dllexport) void __stdcall CleanMemory(int threshold);
}