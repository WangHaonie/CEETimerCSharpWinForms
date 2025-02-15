#pragma once

#include <Windows.h>
#include <WinUser.h>
#include <ShellScalingApi.h>

extern "C"
{
	__declspec(dllexport) void __stdcall SetProcessDpiAwarenessEx(int windowsid);
}