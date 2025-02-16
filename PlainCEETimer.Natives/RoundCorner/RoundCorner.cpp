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
    DWM_WINDOW_CORNER_PREFERENCE preference = DWMWCP_ROUND;
    DwmSetWindowAttribute(hWnd, DWMWA_WINDOW_CORNER_PREFERENCE, &preference, sizeof(preference));
}