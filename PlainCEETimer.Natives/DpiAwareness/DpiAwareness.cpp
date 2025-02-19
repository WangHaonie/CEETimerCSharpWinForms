#include "pch.h"
#include "DpiAwareness.h"

//
// ͨ���������� DPI ��֪ �ο���
//
// Ϊ��������Ĭ�� DPI ��֪ (Windows) - Win32 apps | Microsoft Learn
// https://learn.microsoft.com/zh-cn/windows/win32/hidpi/setting-the-default-dpi-awareness-for-a-process
//

void SetProcessDpiAwarenessEx(int windowsid)
{
	switch (windowsid)
	{
		case 1:
			SetProcessDpiAwareness(PROCESS_PER_MONITOR_DPI_AWARE);
			break;
		case 2:
			SetProcessDpiAwarenessContext(DPI_AWARENESS_CONTEXT_PER_MONITOR_AWARE_V2);
			break;
		case 0:
		default:
			SetProcessDPIAware();
			break;
	}
}