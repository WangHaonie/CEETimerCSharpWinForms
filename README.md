# CEETimerCSharpWinForms
[![GitHub all releases](https://img.shields.io/github/downloads/WangHaonie/CEETimerCSharpWinForms/total?logo=github&label=%E4%B8%8B%E8%BD%BD%E9%87%8F&color=%23DC67A5)](#) [![GitHub release (with filter)](https://img.shields.io/github/v/release/WangHaonie/CEETimerCSharpWinForms?logo=github&label=%E6%9C%80%E6%96%B0%E7%89%88&color=%23178600)](https://github.com/WangHaonie/CEETimerCSharpWinForms/releases/latest/) [![GitHub Repo stars](https://img.shields.io/github/stars/WangHaonie/CEETimerCSharpWinForms?logo=github&label=Stars&color=%23E5B84E)](#) [![GitHub](https://img.shields.io/github/license/WangHaonie/CEETimerCSharpWinForms?logo=github&label=%E8%AE%B8%E5%8F%AF%E8%AF%81&color=%233C9DF8)](https://github.com/WangHaonie/CEETimerCSharpWinForms/blob/main/LICENSE)
## 简介
CEETimerCSharpWinForms，适用于 Windows 系统的高考倒计时，自 v1.6 起不再只是高考倒计时，你可以自定义考试名称。
> 开发环境：Windows 11 Pro x64 (22635.3139)，Visual Studio 2022 (17.8.6)，C# (12.0)，WinForms，.NET Framework 4.7.2
## 运行截图
> v2.5，Windows 10，1920x1080，125% 缩放

![主窗口](./Screenshot.jpg)
## 主要功能
> 说明：✅ 已推出的功能、❌ 不会被考虑的功能

+ ✅ 自定义考试名称；
+ ✅ 自定义考试开始、结束日期和时间；
+ ✅ 一键同步网络时钟，确保系统时间准确无误 (因涉及到修改系统设置，使用此功能可能会弹出 UAC 对话框，请手动点击允许)；
+ ✅ 一键设置开机启动；
+ ✅ 主窗口位于屏幕左上角，可设置是否顶置显示，默认开启 (会被其他后来出现的同样具有顶置属性的窗口遮挡)；
+ ✅ 防止多开，同时也防止被关闭 (但可以结束进程)；
+ ✅ 检查更新功能，用户确认后可自动下载并安装；
+ ❌ 更改倒计时字体；
+ ❌ 更改倒计时文字颜色、大小；
+ ❌ 更改倒计时背景颜色；
+ ❌ 时时刻刻保持顶置，防止被遮挡；
+ ❌ 实时监测屏幕缩放的更改并调整窗口以防止窗体模糊 (可在设置里面手动点击重启)。
## 系统要求
1. 适用于 Windows 7 以上的 x64 系统；
2. 安装了 .NET Framework 4.7.2 (点此[链接](https://dotnet.microsoft.com/zh-cn/download/dotnet-framework/thank-you/net472-offline-installer)下载)。
## 食用方法
1. 安装 [.NET Framework 4.7.2](https://dotnet.microsoft.com/zh-cn/download/dotnet-framework/thank-you/net472-offline-installer) (新版 Windows 10/11 可能内置了 .NET Framework 4.8 或更高版本，可以不用安装)；
2. 到 [Releases](https://github.com/WangHaonie/CEETimerCSharpWinForms/releases/latest) 下载 CEETimerCSharpWinForms_X.X_x64_Setup.exe；
3. 按照提示安装就行；
4. 初次使用需要右键主窗口，选择 设置，然后设置考试名称、开始/结束日期和时间，点击应用；
## 效果展示
1. 当考试未开始时，倒计时会显示红色的 "距离...还有...天...时...分...秒"：<br><span style="color: red; background-color: white; font-size: calc(1rem + 8px); font-weight: bold">距离考试还有0天0时0分0秒</span>
2. 当考试正在进行时，会显示绿色的 "距离...结束还有...天...时...分...秒"：<br><span style="color: green; background-color: white; font-size: calc(1rem + 8px); font-weight: bold">距离考试结束还有0天0时0分0秒</span>
3. 当考试结束后，会显示黑色的 "距离...已经过去了...天...时...分...秒"：<br><span style="color: black; background-color: white; font-size: calc(1rem + 8px); font-weight: bold">距离考试已经过去了0天0时0分0秒</span>
## 开源许可证
CEETimerCSharpWinForms is licensed under the GNU General Public License v3.0 (GPL-3.0).


