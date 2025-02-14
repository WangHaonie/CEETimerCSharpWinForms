#pragma once

typedef HRESULT(__stdcall* DwmSetWindowAttributeProc)(HWND, DWORD, LPCVOID, DWORD);

extern "C"
{
	__declspec(dllexport) void __stdcall SetRoundCornerRegion(HWND hWnd, int nRightRect, int nBottomRect, int radius);
	__declspec(dllexport) void __stdcall SetRoundCornerModern(HWND hWnd);
}