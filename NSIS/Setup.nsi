ManifestDPIAware true
SetFont "Segoe UI" 8
!include "MUI2.nsh"
!define PRODUCT_NAME "高考倒计时"
!define PRODUCT_VERSION "v2.2"
!define SETUP_FILENAME_NO_V "2.2"
!define PRODUCT_PUBLISHER "WangHaonie"
!define PRODUCT_WEB_SITE "https://github.com/WangHaonie/CEETimerCSharpWinForms"
!define PRODUCT_UNINST_KEY "Software\Microsoft\Windows\CurrentVersion\Uninstall\${PRODUCT_NAME}"
!define PRODUCT_UNINST_ROOT_KEY "HKCU"
!define MUI_ABORTWARNING
!define MUI_ICON "${NSISDIR}\Contrib\Graphics\Icons\nsis3-install.ico"
!define MUI_UNICON "${NSISDIR}\Contrib\Graphics\Icons\nsis3-uninstall.ico"
!define MUI_LICENSEPAGE_CHECKBOX

SetCompressor lzma

!insertmacro MUI_PAGE_WELCOME
!insertmacro MUI_PAGE_LICENSE "..\LICENSE"
!insertmacro MUI_PAGE_DIRECTORY
!insertmacro MUI_PAGE_INSTFILES
!insertmacro MUI_PAGE_FINISH
!insertmacro MUI_UNPAGE_INSTFILES
!insertmacro MUI_LANGUAGE "SimpChinese"

Name "${PRODUCT_NAME} ${PRODUCT_VERSION}"
OutFile "CEETimerCSharpWinForms_${SETUP_FILENAME_NO_V}_x64_Setup.exe"
InstallDir "$PROFILE\AppData\Local\CEETimerCSharpWinForms"
InstallDirRegKey HKCU "${PRODUCT_UNINST_KEY}" "UninstallString"
ShowInstDetails show
ShowUnInstDetails show
BrandingText "Copyright (C) 2023 WangHaonie"

Section -POST
  SetOverwrite on
  SetOutPath "$INSTDIR"
  nsExec::Exec '"taskkill" /F /IM "CEETimerCSharpWinForms.exe"'
  Delete "$INSTDIR\CEETimerCSharpWinForms.exe"
  Delete "$INSTDIR\${PRODUCT_NAME}.url"
  Delete "$INSTDIR\uninst.exe"
  Delete "$INSTDIR\Newtonsoft.Json.dll"
  Delete "$INSTDIR\CEETimerCSharpWinForms.exe.config"
  Delete "$INSTDIR\CEETimerCSharpWinFormsConfig.db"
  Delete "$INSTDIR\CEETimerCSharpWinForms.exe"
  WriteUninstaller "$INSTDIR\uninst.exe"
  CreateDirectory "$SMPROGRAMS\高考倒计时"
  CreateShortCut "$SMPROGRAMS\高考倒计时\高考倒计时.lnk" "$INSTDIR\CEETimerCSharpWinForms.exe"
  CreateShortCut "$DESKTOP\高考倒计时.lnk" "$INSTDIR\CEETimerCSharpWinForms.exe"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "DisplayName" "$(^Name)"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "UninstallString" "$INSTDIR\uninst.exe"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "DisplayIcon" "$INSTDIR\CEETimerCSharpWinForms.exe"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "DisplayVersion" "${PRODUCT_VERSION}"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "URLInfoAbout" "${PRODUCT_WEB_SITE}"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "Publisher" "${PRODUCT_PUBLISHER}"
  WriteIniStr "$INSTDIR\GitHub.url" "InternetShortcut" "URL" "${PRODUCT_WEB_SITE}"
  File "..\CEETimerCSharpWinForms\bin\x64\Debug\Newtonsoft.Json.dll"
  File "..\CEETimerCSharpWinForms\bin\x64\Debug\CEETimerCSharpWinForms.exe"
  CreateShortCut "$SMPROGRAMS\高考倒计时\GitHub.lnk" "$INSTDIR\${PRODUCT_NAME}.url"
  CreateShortCut "$SMPROGRAMS\高考倒计时\卸载 高考倒计时.lnk" "$INSTDIR\uninst.exe"
SectionEnd

Section Uninstall
  nsExec::Exec '"taskkill" /F /IM "CEETimerCSharpWinForms.exe"'
  Delete "$INSTDIR\${PRODUCT_NAME}.url"
  Delete "$INSTDIR\uninst.exe"
  Delete "$INSTDIR\Newtonsoft.Json.dll"
  Delete "$INSTDIR\CEETimerCSharpWinForms.exe.config"
  Delete "$INSTDIR\CEETimerCSharpWinForms.exe"
  Delete "$INSTDIR\CEETimerCSharpWinForms.dll"
  Delete "$SMPROGRAMS\高考倒计时\卸载 高考倒计时.lnk"
  Delete "$SMPROGRAMS\高考倒计时\GitHub.lnk"
  Delete "$DESKTOP\高考倒计时.lnk"
  Delete "$SMPROGRAMS\高考倒计时\高考倒计时.lnk"
  RMDir "$SMPROGRAMS\高考倒计时"
  RMDir "$INSTDIR"
  DeleteRegKey ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}"
  SetAutoClose true
SectionEnd

Function .onInstSuccess
  Exec "$INSTDIR\CEETimerCSharpWinForms.exe"
FunctionEnd