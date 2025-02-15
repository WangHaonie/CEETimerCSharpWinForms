#pragma once

#include <ShObjIdl.h>
#include <Windows.h>

extern "C"
{
	__declspec(dllexport) void __stdcall InitilizeTaskbarList(HWND hWnd);
	__declspec(dllexport) void __stdcall SetTaskbarListState(int tbpFlags);
	__declspec(dllexport) void __stdcall SetTaskbarListProgress(ULONGLONG ullCompleted, ULONGLONG ullTotal);
	__declspec(dllexport) void __stdcall ReleaseTaskbarList();
}