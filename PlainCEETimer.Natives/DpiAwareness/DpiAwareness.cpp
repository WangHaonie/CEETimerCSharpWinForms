#include "pch.h"
#include "DpiAwareness.h"

void SetProcessDpiAwarenessEx(int windowsid)
{
	switch (windowsid)
	{
		case 1:
			/*SetProcessDpiAwareness(PROCESS_PER_MONITOR_DPI_AWARE);
			break;*/
		case 2:
			/*SetProcessDpiAwarenessContext(DPI_AWARENESS_CONTEXT_PER_MONITOR_AWARE_V2);
			break;*/

			// WinForms ��֧�� Per-Monitor V2��������������
			// �����Ժ���
		case 0:
		default:
			/*SetProcessDPIAware();*/
			break;
	}
}
