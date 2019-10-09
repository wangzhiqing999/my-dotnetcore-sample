# .Net Core 日志的使用与配置.


## 默认的日志

创建一个 ASP.Net Core Web 项目
项目名称 A3002_Logger
选择 Web Application(Model-View-Contorller) 的类型。

项目创建完毕后，修改 HomeController 。
分别在 Index 方法 与 Privacy 方法中增加 _logger.LogDebug 的测试代码.

立即运行，尝试访问首页 与 Privacy Policy 页面.
在 VS2019 的输出窗口.
观察，能够看到

A3002_Logger.Controllers.HomeController: Debug: Home.Index
A3002_Logger.Controllers.HomeController: Debug: Home.Privacy

这样的输出信息.





## 与 Log4Net 整合

创建一个 ASP.Net Core Web 项目
项目名称 A3002_Logger_Log4Net
选择 Web Application(Model-View-Contorller) 的类型。

项目创建完毕后，修改 HomeController 。
分别在 Index 方法 与 Privacy 方法中增加 _logger.LogDebug 的测试代码.

NuGet 添加引用
log4net
Microsoft.Extensions.Logging.Log4Net.AspNetCore

修改 Program.cs
追加下面的代码.
.ConfigureLogging((hostingContext, logging)=> {
	logging.AddFilter("Microsoft.Hosting.Lifetime", LogLevel.Warning);
	logging.AddFilter("Microsoft.AspNetCore", LogLevel.Warning);
	logging.AddLog4Net();
})

从以前在 .Net Framework 项目中，将 log4net.config 复制到本项目的目录下.
测试运行，观察 LogFile 目录下，是否有相应的 日志文件，以及日志的输出.

