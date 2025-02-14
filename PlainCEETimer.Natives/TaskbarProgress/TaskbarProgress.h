#pragma once

#include <ShObjIdl.h>
#include <Windows.h>

extern "C"
{
	__declspec(dllexport) void InitilizeTaskbarList(HWND hWnd);
	__declspec(dllexport) void SetTaskbarListState(int tbpFlags);
	__declspec(dllexport) void SetTaskbarListProgress(ULONGLONG ullCompleted, ULONGLONG ullTotal);
	__declspec(dllexport) void ReleaseTaskbarList();
}