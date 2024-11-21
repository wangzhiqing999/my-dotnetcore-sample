# my-dotnetcore-sample


### A0001_HelloWorld
最简单的 HelloWorld 程序.
[2021-12-07 更新 使用 Visual Studio 2022， 框架升级为  .NET 6.0]


### A0002_HelloWorldMvc
最简单的 HelloWorld  asp.net core 程序.


### A0003_EF
使用 Visual Studio 2017， 开发 EntityFramework Code First 的例子.  (使用 SQL Server 数据库)
[2021-02-01 更新 使用 Visual Studio 2019， 框架升级为  .NET 5.0]
[2024-11-19 更新 使用 Visual Studio 2022， 框架升级为  .NET 8.0]


### A0004_Config
.NetCode 2.0 环境下， 读取配置文件的例子。（读取 ini / json / xml 文件）



### A0005_EF_Mysql
使用 Visual Studio 2017， 开发 EntityFramework Code First 的例子.  (使用 Mysql 数据库)
[2021-12-09 增加 使用 Visual Studio 2022， 框架升级为  .NET 6.0]

### A0006_EF_Sqlite
使用 Visual Studio 2017， 开发 EntityFramework Code First 的例子.  (使用 Sqlite 数据库)
[2021-12-09 增加 使用 Visual Studio 2022， 框架升级为  .NET 6.0]


### A0007_EF_QueryFilter
EntityFramework Code 中， 使用 QueryFilter， 实现 逻辑删除的例子. (使用 SQL Server 数据库)


### A0008_EF_ExistingDb
使用 以前 .Net Framework 4.5 + EF 6 生成的数据库表结构， 编写新的 .Net Core + EF Core 的代码.


### A0009_EF_Postgres
EntityFramework Code First 的例子.  (使用 Postgres 数据库)
包含下面的版本：
使用 Visual Studio 2019， 框架为 .NET 5.0
使用 Visual Studio 2022， 框架为 .NET 6.0
使用 Visual Studio 2022， 框架为 .NET 8.0



### A0010_TestWebApi
使用 Visual Studio 2017， 创建一个 Web Api 项目的例子. (DotNet Core 2.0)
[2021-02-01 更新 使用 Visual Studio 2019， 框架升级为  .NET 5.0]
[2021-12-09 更新 使用 Visual Studio 2022， 框架升级为  .NET 6.0]
[2024-09-20 更新 使用 Visual Studio 2022， 框架升级为  .NET 8.0]
[2024-11-21 更新 使用 Visual Studio 2022， 框架升级为  .NET 9.0]


### A0020_Fundamentals
使用 Visual Studio 2017， 创建一个 Web 项目，测试 静态文件、路由配置、URL Rewriting、Session、WebSockets 的例子. (DotNet Core 2.0)


### A0030_Authentication
使用 Visual Studio 2017， 创建一个 Web 项目，测试安全相关的例子。(DotNet Core 2.0)


### A0040_React
使用 Visual Studio 2017， 创建一个 Web 项目，测试 React 的使用。(DotNet Core 2.0)



### A0050_BlazorApp
使用 Visual Studio 2022， 创建 BlazorApp 项目的例子。（ .NET 6.0  / .NET 8.0 ）



### A0060_Debug
System.Diagnostics.Debug 的一些简单的用法。（ .NET 8.0）


### A0062_StackTrace
System.Diagnostics.StackTrace 的一些简单的用法。（ .NET 8.0）


### A0065_Record
record 使用的例子。（ .NET 8.0）




### A0100_Database
基本的访问数据库的例子代码.  （ .NET 8.0）
（包含使用 Dapper 来访问的例子）


### A0110_Excel
使用 NPOI 读写 Excel 的例子代码。
（NPOI 库支持  .Net Core 与 .Net Framework ， 直接从 my-csharp-sample 那里复制之前的例子代码过来，测试可运行 ）




### A0200_Ocelot
Ocelot 网关使用的例子代码.


### A0210_WebApiClient
调用 Web Api 的客户端写法. (使用 Refit 或者 WebApiClient.JIT)



### A3001_Json
System.Text.Json 使用的例子 (DotNet Core 3.0)


### A3002_Logger
Microsoft.Extensions.Logging.ILogger 使用的例子 (DotNet Core 3.0)
包含默认的，独立的使用
以及与 Log4Net 进行整合的使用。
[2024-11-15 更新 使用 Visual Studio 2022， 框架升级为  .NET 8.0]




### A3003_Authentication
ASP.Net Code，基本登录的测试 (DotNet Core 3.0)
测试下来，基本操作，与 DotNet Core 2.0 一样.
[2021-02-01 更新 使用 Visual Studio 2019， 框架升级为  .NET 5.0]



### A3004_Middleware
ASP.Net Code，中间件的例子代码 (DotNet Core 3.0)
[2021-02-01 更新 使用 Visual Studio 2019， 框架升级为  .NET 5.0]



### A5001_MLNET
学习使用 ML.NET 的代码.




### B0100_FluentValidation

使用 FluentValidation，来校验数据的例子。（.NET 6.0）



### B0110_Flee

使用 Flee， 来完成 表达式的动态解析和计算。（.NET 6.0）


### B0120_DynamicExpresso

使用 DynamicExpresso， 来完成 表达式的动态解析和计算。（.NET 8.0）



### B0250_Quartz
使用 Visual Studio 2019， 创建一个 控制台 项目 （.NET 5.0）
用于测试使用 Quartz 的例子.


### B0260_Masuit
使用 Masuit.Tools 的一些例子（.NET 8.0）
很多小类的工具包，可以视情况来使用。


### B0270_HtmlSanitizer
使用 HtmlSanitizer 的例子 （.NET 8.0）
主要用于防止 Html跨站脚本攻击（XSS）
也就是前端页面的留言功能，提交的是 html 的情况下。
后端接收到输入的 html 后，需要做处理， 移除掉 不安全的 标签与属性， 比如 script 以及 onclick 之类的。




### C0001_MyWork
使用 Visual Studio 2017， 创建 Web Api . (DotNet Core 2.0)
使用 framework7， 调用 Web Api.


### C0010_MySSO
使用 Visual Studio 2017， 创建 单点登录（SSO）的网站.  (DotNet Core 2.2)


### D0001_Docker_HelloWorld
使用 Visual Studio 2017，创建一个 Web 项目 （本例子仅仅为了测试在 Docker 下发布，因此，啥代码也没有写）。
然后，在 Ubuntu 下面，创建 Docker 镜像，并运行的处理步骤。



### D0001_Docker_HelloWorld_Net8
使用 Visual Studio 2022，创建一个 Web 项目（.NET 8.0） （本例子仅仅为了测试在 Windows 的 Docker Desktop 下运行， 以及 Ubuntu 的 Docker 下发布，因此，啥代码也没有写）。
测试在 Windows 下面，Docker Desktop 的整合使用。
然后，在 Ubuntu 下面，创建 Docker 镜像，并运行的处理步骤。



### D0002_Docker_Compose_HelloWorld
使用 Visual Studio 2019， 创建两个 Web 项目（一个 Web 一个 Api） （.NET 5.0）
然后，在 Ubuntu 下面
创建 Docker 镜像，单独运行的处理步骤。
以及在 Docker Compose 下，一起运行的处理步骤。




### D0003_Docker_Console
使用 Visual Studio 2019，创建一个控制台项目 （测试功能：读取配置文件，输出到屏幕，输出到文件）
然后，在 Ubuntu 下面
创建 Docker 镜像
运行的时候，测试 volume 使用的两种方式。
（一种是作为配置文件目录，主机填写配置文件，容器运行。  一种是作为数据输出目录，容器的中输出的文件，输出到主机的指定目录下）

[2021-12-07 使用 Visual Studio 2022， 创建一个  D0003_Docker_Console_V6， 框架为  .NET 6.0， 对比一下与之前的差异]
[2024-10-12 使用 Visual Studio 2022， 创建一个  D0003_Docker_Console_V8， 框架为  .NET 8.0， 对比一下与之前的差异]




### D0010_MyTodo
使用 Visual Studio 2022, 创建的 Web Api 项目。
使用 EntityFramework Core
使用 Sqlite 数据库

分别测试
在 Windows 的命令行下运行.
发布到 IIS 下.
在 Ubuntu 的命令行下运行.
发布到 Docker 上运行.




### MyWebCrawler
一个 netstandard2.0 的类库的项目.
用于测试发布到 自己搭建的私有的 NuGet 服务器上.
然后，自己的项目，进行调用.



### P0001_MyMiniTradingSystem
练习，尝试将 .NET Framework 的项目， 迁移到 DotNet Core 2.0


### P0002_MyEtf
使用 Visual Studio 2019， 创建一组练习的项目 （.NET 5.0）
使用 EntityFramework Code First
使用 Postgres 数据库


### W1000_ABP_HelloWorld
使用 ABP 创建的 HelloWorld 项目.


### W1001_ABP_With_Zero
使用 ABP 创建的项目. (引用了 module zero)


### W1010_ABP_NetCode2
使用 ABP 创建的项目. (.NET Core 2.x 版本)


### W1100_Page
Ajax 数据翻页的例子.
[2021-02-01 更新 使用 Visual Studio 2019， 框架升级为  .NET 5.0]
[2022-11-21 更新 使用 Visual Studio 2022， 框架升级为  .NET 8.0]


### W2000_BaseWeb
一些 Web 项目，常用的例子代码。 （.NET 5.0 / .NET 8.0 ）


### W3001_OrchardCore
使用 OrchardCore 开发 Web 项目的例子  (.NET Core 3.0 版本)



### W4000_BlazorApp

安装 BootstrapBlazor.UITemplate.vsix 插件之后，使用模板，创建的项目。 （.NET 8.0）



### W4101_AntDesignApp

使用 AntDesign.Templates 来创建项目。 （.NET 8.0）
VS2022中，使用模板 “Ant Design Pro Blazor App (AntDesign Blazor Team)” 来创建项目。 （.NET 8.0）

