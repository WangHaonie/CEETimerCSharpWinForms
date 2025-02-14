#include "pch.h"
#include "RoundCorner.h"

void SetRoundCornerRegion(HWND hWnd, int nRightRect, int nBottomRect, int radius)
{
    SetWindowRgn(
        hWnd,
        CreateRoundRectRgn(0, 0, nRightRect, nBottomRect, radius, radius),
        TRUE);
}

void SetRoundCornerModern(HWND hWnd)
{
    HMODULE hDwmApi = LoadLibrary(L"dwmapi.dll");
    if (hDwmApi)
    {
        DwmSetWindowAttributeProc pDwmSetWindowAttribute = (DwmSetWindowAttributeProc)GetProcAddress(hDwmApi, "DwmSetWindowAttribute");

        if (pDwmSetWindowAttribute)
        {
            const int DWMWA_WINDOW_CORNER_PREFERENCE = 33;
            const int DWMWCP_ROUND = 2;
            pDwmSetWindowAttribute(hWnd, DWMWA_WINDOW_CORNER_PREFERENCE, &DWMWCP_ROUND, sizeof(DWMWCP_ROUND));
        }

        FreeLibrary(hDwmApi);
    }
}