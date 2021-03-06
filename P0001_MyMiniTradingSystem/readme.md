# .Net Core Entity Framework + web api


### DataAccess 项目

.Net Core, NuGet 添加下列包：
  Microsoft.EntityFrameworkCore.Sqlite
  Microsoft.EntityFrameworkCore.Design
  Microsoft.EntityFrameworkCore.Tools
  Microsoft.EntityFrameworkCore.Tools.DotNet



### Service 项目

引用 DataAccess 项目.






### Web 项目.

引用 DataAccess，DataAccess 项目.

.Net Core, NuGet 添加下列包：
  Microsoft.EntityFrameworkCore.Sqlite


例子代码中
  ValuesController 控制器， 是新建项目的时候， 自动生成的，啥也没修改。
  CommodityPricesController 控制器，是直接调用 EntityFramework， 完成相关操作的。 这个代码，是 VS 里面生成的，啥也没修改。
  TradableCommoditysController 控制器，是调用自己写的服务， 服务代码去调用 EntityFramework， 完成相关操作的。

注意：CommodityPrice 类，是复合主键的，因此，生成的 CommodityPricesController 控制器， 实际使用的话， 是需要做修改的。


数据库的配置信息， 以及服务/接口的依赖注入的相关配置，写在 Startup.cs 里面




### 发布/运行

在 VS2017 中测试运行后，发布到文件系统的 bin\Release\PublishOutput 目录下。
由于初始情况下， 没有数据， 使用 数据库管理工具， 手动插入几行数据， 进行测试。


#### 控制台运行
...\MyMiniTradingSystem.Web\bin\Release\PublishOutput>dotnet MyMiniTradingSystem.Web.dll
Hosting environment: Production
Content root path: ...\MyMiniTradingSystem.Web\bin\Release\PublishOutput
Now listening on: http://localhost:5000
Application started. Press Ctrl+C to shut down.

然后浏览器中，http://localhost:5000/api/CommodityPrices   查看页面。



#### 发布到 IIS 下。

首先， 在 IIS 什么的，都已经安装完成的情况下。
为了运行 .Net Core，需要额外安装一个 .NET Core Windows Server Hosting bundle
下载地址是 https://aka.ms/dotnetcore.2.0.0-windowshosting
下载后的文件名为 DotNetCore.2.0.0-WindowsHosting.exe
运行这个文件，然后重启 IIS 服务。
IIS 管理器中， 创建网站， 目录为前面发布的 bin\Release\PublishOutput 目录下， 端口 8082
到应用程序池中， 找到这个新创建的网站， 双击， 将 .NET CLR 版本， 修改为 【无托管代码】

然后浏览器中，http://localhost:8082/api/CommodityPrices  去查看页面。

参考页面：
https://docs.microsoft.com/en-us/aspnet/core/publishing/iis?tabs=aspnetcore2x



#### Linux 控制台下运行

测试虚拟机：CentOS-7-x86_64-Minimal-1611.iso 安装.

安装 .NetCore. 参考下面的页面.
http://www.microsoft.com/net/learn/get-started/linuxcentos

在 Program.cs 文件中，WebHost.CreateDefaultBuilder(args)  后面， 追加一行 .UseUrls("http://*:5000")
重新发布一次， 然后将文件复制到 Linux 系统下。

[root@localhost PublishOutput]#dotnet MyMiniTradingSystem.Web.dll
Hosting environment: Production
Content root path: /home/wang/PublishOutput
Now listening on: http://[::]:5000
Application started. Press Ctrl+C to shut down.

然后浏览器中，http://虚拟机ip地址:5000/api/CommodityPrices   查看页面。

