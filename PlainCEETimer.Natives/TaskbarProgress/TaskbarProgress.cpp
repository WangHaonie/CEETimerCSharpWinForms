#include <objbase.h>
#include "pch.h"
#include "TaskbarProgress.h"

static ITaskbarList3* _TaskbarList;
static HWND WindowHandle;

void InitilizeTaskbarList(HWND hWnd)
{
	WindowHandle = hWnd;

	if (SUCCEEDED(CoCreateInstance(CLSID_TaskbarList, NULL, CLSCTX_ALL, IID_ITaskbarList3, (void**)&_TaskbarList)))
	{
		_TaskbarList->HrInit();
	}
}

void SetTaskbarProgressState(int tbpFlags)
{
	if (_TaskbarList && WindowHandle)
	{
		_TaskbarList->SetProgressState(WindowHandle, (TBPFLAG)tbpFlags);
	}
}

void SetTaskbarProgressValue(ULONGLONG ullCompleted, ULONGLONG ullTotal)
{
	if (_TaskbarList && WindowHandle)
	{
		_TaskbarList->SetProgressValue(WindowHandle, ullCompleted, ullTotal);
	}
}

void ReleaseTaskbarList()
{
	if (_TaskbarList)
	{
		_TaskbarList->Release();
		_TaskbarList = nullptr;
	}

	WindowHandle = NULL;
}
