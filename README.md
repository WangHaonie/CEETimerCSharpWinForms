# CEETimerCSharpWinForms
[![GitHub all releases](https://img.shields.io/github/downloads/WangHaonie/CEETimerCSharpWinForms/total?logo=github&label=%E4%B8%8B%E8%BD%BD%E9%87%8F&color=%23DC67A5)](#) [![GitHub release (with filter)](https://img.shields.io/github/v/release/WangHaonie/CEETimerCSharpWinForms?logo=github&label=%E6%9C%80%E6%96%B0%E7%89%88&color=%23178600)](https://github.com/WangHaonie/CEETimerCSharpWinForms/releases/latest/) [![GitHub Repo stars](https://img.shields.io/github/stars/WangHaonie/CEETimerCSharpWinForms?logo=github&label=Stars&color=%23E5B84E)](#) [![GitHub](https://img.shields.io/github/license/WangHaonie/CEETimerCSharpWinForms?logo=github&label=%E8%AE%B8%E5%8F%AF%E8%AF%81&color=%233C9DF8)](https://github.com/WangHaonie/CEETimerCSharpWinForms/blob/main/LICENSE)
## 简介
CEETimerCSharpWinForms，适用于 Windows 系统的高考倒计时，支持自定义考试名称。
> 开发环境：Windows 11 Pro x64 (22635.3350)，Visual Studio 2022 (17.9.4)，C# (12.0)，WinForms，.NET Framework 4.7.2
## 运行截图
> v2.9，Windows 10，1920x1080，125% 缩放

![主窗口](./Screenshot.jpg)
## 主要功能
+ ✅ 自定义考试名称；
+ ✅ 自定义考试开始、结束日期和时间；
+ ✅ 显示在屏幕左上角 (可以拖动，需手动开启)；
+ ✅ 设置是否顶置显示，默认开启 (会被其他后来出现的同样具有顶置属性的窗口遮挡)；
+ ✅ 更改倒计时字体和大小；
+ ✅ 可设置是否只显示天数等 (设置>显示)；
## 其他功能
+ ✅ 防止多开，同时也防止被关闭 (但可以结束进程)；
+ ✅ 设置开机启动；
+ ✅ 自身内存优化 (需手动开启)；
+ ✅ 跟随虚拟桌面移动 (需手动开启，不稳定，估计是系统接口有问题)；
+ ✅ 重启倒计时 (用于更改了缩放后重启以防止窗口模糊)；
+ ✅ 检查更新功能，用户确认后可自动下载并安装；
+ ✅ 同步网络时钟，确保系统时间准确无误 (因涉及到修改系统设置，使用此功能可能会弹出 UAC 对话框，请手动点击允许)；

## 系统要求
+ 适用于 Windows 7 以上的 x64 系统；
+ 安装了 .NET Framework 4.7.2 (点此[链接](https://dotnet.microsoft.com/zh-cn/download/dotnet-framework/thank-you/net472-offline-installer)下载)。
## 食用方法
### 下载安装
+ 先安装 [.NET Framework 4.7.2](https://dotnet.microsoft.com/zh-cn/download/dotnet-framework/thank-you/net472-offline-installer) 运行库 (新版 Windows 10/11 可能内置了 .NET Framework 4.8 或更高版本，可以不用安装)
+ 到 [Releases](https://github.com/WangHaonie/CEETimerCSharpWinForms/releases/latest) 下载 CEETimerCSharpWinForms_X.X_x64_Setup.exe；
+ 按照提示安装就行；
### 首次使用
+ 在主窗口按下鼠标右键，选择 "设置" 即可打开

## 设置窗口介绍
右键单击主窗口选择 "设置" 即可唤出
### 基本
+ **开机时自动启动倒计时**：可设置是否开机启动
+ **将倒计时显示到最上层**：可设置是否顶置显示倒计时
+ **考试名称**：输入考试名称，2~15字，不允许非法字符
+ **考试开始/结束日期和时间**：在控件的数字上左键点击，可使用左右键切换要设置的字段，上下键选择具体数值。也可以点击控件右侧的日历小图标弹出日历进行选择，具体时间需手动输入

> **注意**：若想设置考试结束日期和时间，你需要考虑是否让倒计时显示 "考试还有多久结束" 或者 "考试已过去了多久"，并先到 "显示" 选项卡中勾选相应复选框，然后设置考试结束日期和时间才能生效

### 显示
+ **只显示天数**：可设置是否只显示天数
+ **将不足一天的时间视为一天**：可以使与市面上销售的纸质倒计时达到同样的效果
+ **顶置属性同样使用与本程序的其他窗口**：可设置本程序的其他窗口 (设置、关于、更新器) 是否也跟随倒计时窗口的顶置属性 (需先在 "基本" 选项卡中开启倒计时的顶置属性)
+ **选择字体**：可自定义倒计时的字体以及大小 (仅支持 10~24 磅)
+ **恢复默认**：恢复默认字体
### 工具
+ **同步网络时钟**：一键同步网络时钟，需要管理员权限，同时会将系统默认网络时钟服务器设置为 ntp1.aliyun.com (国内的)，并且还会将 Windows Time 服务设置为自动启动
+ **重启倒计时**：一键重新启动倒计时，由于本程序采用的 WinForms 框架在更改了屏幕缩放后并不会自动调整而使得窗口文字显示模糊，故可以重新启动程序确保文字清晰 (此处有🌈🥚)
+ **允许倒计时窗口被拖动**：开启后可以随意拖动倒计时窗口，防止遮挡屏幕上的某些内容，支持多显示器，并防止拖放到任务栏上被任务栏挡住
### 高级
+ **虚拟桌面支持**：开启后当检测到虚拟桌面切换时，就自动将倒计时窗口移动到当前虚拟桌面上，目前此功能可能会在移动几次后逐渐失效，估计是系统接口有问题
+ **内存优化**：由于 C# 程序普遍存在内存占用问题，开启此功能后会定期检测当内存占用超过12MB才会触发清理
+ **兼容希沃PPT小工具**：经测试当倒计时显示在默认左上角位置 (0, 0) 的地方时会使希沃PPT小工具的内置白板打开后底部工具栏消失的问题，开启后显示位置将调整为 (1, 0)

## 程序其他特性
+ 程序启动时默认在后台检测更新，有更新会弹出更新提示，此特性无法设置
+ 手动点击关于窗口里的版本号可再次触发检查更新
+ 右键单击主窗口，选择 "打开程序文件夹" 可以快速打开程序安装文件夹
+ 当程序遇到 Bug 时，可尝试删除程序文件夹内的 CEETimerCSharpWinForms.dll 配置文件并重新启动程序应该会好
## 开源许可证
CEETimerCSharpWinForms is licensed under the GNU General Public License v3.0 (GPL-3.0)
## 打赏作者
[传送门](https://wanghaonie.github.io/reward/)


