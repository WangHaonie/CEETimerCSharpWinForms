#pragma once

#include <dwmapi.h>

extern "C"
{
	__declspec(dllexport) void __stdcall SetRoundCornerRegion(HWND hWnd, int nRightRect, int nBottomRect, int radius);
	__declspec(dllexport) void __stdcall SetRoundCornerModern(HWND hWnd);
}