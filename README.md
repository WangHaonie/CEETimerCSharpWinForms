# CEETimerCSharpWinForms
[![GitHub all releases](https://img.shields.io/github/downloads/WangHaonie/CEETimerCSharpWinForms/total?logo=github&label=%E4%B8%8B%E8%BD%BD%E9%87%8F&color=%23DC67A5)](#) [![GitHub release (with filter)](https://img.shields.io/github/v/release/WangHaonie/CEETimerCSharpWinForms?logo=github&label=%E6%9C%80%E6%96%B0%E7%89%88&color=%23178600)](https://github.com/WangHaonie/CEETimerCSharpWinForms/releases/latest/) [![GitHub Repo stars](https://img.shields.io/github/stars/WangHaonie/CEETimerCSharpWinForms?logo=github&label=Stars&color=%23E5B84E)](#) [![GitHub](https://img.shields.io/github/license/WangHaonie/CEETimerCSharpWinForms?logo=github&label=%E8%AE%B8%E5%8F%AF%E8%AF%81&color=%233C9DF8)](https://github.com/WangHaonie/CEETimerCSharpWinForms/blob/main/LICENSE)
## 简介
CEETimerCSharpWinForms，适用于 Windows 系统的高考倒计时，自 v1.6 起不再只是高考倒计时，你可以自定义考试名称。
> 开发环境：Windows 11，Visual Studio 2022，C#，WinForms，.NET Framework 4.7.2
## 运行截图
![主窗口](./ReadmeImgs/Windows%2010-2023-12-30-10-55-03.png)
## 主要特征和功能
> 说明：✅ 已推出的功能、⭕ 正在开发的功能、❌ 不会被考虑的功能

+ ✅ 主窗口位于屏幕左上角，并显示到最上层 (可能会被其他后来出现的同样具有顶置属性的窗口遮挡)；
+ ✅ 防止多开，同时也防止被关闭 (但可以结束进程)；
+ ✅ 一键设置开机启动；
+ ✅ 自定义考试名称；
+ ✅ 自定义开始、结束日期和时间；
+ ✅ 一键同步网络时钟，确保系统时间准确无误 (因涉及到修改系统设置，使用此功能可能会弹出 UAC 对话框，请手动点击允许)；
+ ⭕ 修复与希沃PPT小工具冲突；
+ ✅ 适配高分辨率以及各种缩放的屏幕；
+ ⭕ 实时监测系统分辨率/缩放更改，并自动调整程序自身的缩放以确保不会模糊 (目前为止只能在设置里重启程序才不会出现模糊)；
+ ✅ 代码优化，目前屎山和设计缺陷太多了；
+ ✅ 检查更新功能，自动下载并安装；
+ ❌ 更改倒计时字体；
+ ❌ 更改倒计时文字颜色、大小；
+ ❌ 更改倒计时背景颜色；
+ ❌ 时时刻刻保持顶置，防止被遮挡；
## 系统要求
1. Windows x64 系统
2. .NET Framework 4.7.2 (高版本 Windows 10/11 可能预装了 4.8 或更高版本，可以不用安装。但如果你正在使用 Windows 7/8/8.1 或者虽然是 Windows 10/11 但却无法运行，请点击[此链接](https://dotnet.microsoft.com/zh-cn/download/dotnet-framework/thank-you/net472-offline-installer)下载并安装 .NET Framework 4.7.2)
## 食用方法
1. 安装运行库，详见上方 "系统要求"
2. 到 [Releases](https://github.com/WangHaonie/CEETimerCSharpWinForms/releases/latest) 下载 CEETimerCSharpWinForms_X.X_x64_Setup.exe
3. 安装就行
> 程序文件夹里面的 CEETimerCSharpWinForms.dll (不是真的 dll，只是一个文本文件而已) 是配置文件，请不要删除，更不要随意修改其中的数据。
## 已知的大问题
+ 已确认与希沃PPT小工具 (版本1.0.0.795) 有冲突，可能是由于双方都具有 TopMost(显示在最上层) 属性，当你在PPT小工具的工具栏上，点击内置白板的时候，白板可以正常打开，但是底部的工具栏也会随之消失，然后你收起白板，工具栏又会出现，这会导致无法方便的切换画笔颜色和使用橡皮擦。由于希沃PPT小工具没有开源，所以无法分析具体原因，已向希沃客服反馈。<br><br>临时解决方案:<br>① 若想更换画笔颜色，可先收起白板，然后在工具栏上选择颜色，再点开白板。若想使用橡皮擦，也需先收回白板，用笔在当前正在播放的幻灯片里留下一个墨迹，然后才能点橡皮擦，然后再打开白板；<br>② 使用希沃白板5而不是PPT小工具里面自带的白板，在PPT放映过程中，你可以点击希沃白板5的悬浮窗来打开白板。
## 开源许可证
CEETimerCSharpWinForms is licensed under the GNU General Public License v3.0 (GPL-3.0).