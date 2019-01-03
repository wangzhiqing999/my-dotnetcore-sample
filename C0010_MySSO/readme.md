
## 基本环境.
Visual Studio Community 2017 v 15.9.3
.Net Core 2.2 




## 安装 .NET Core 托管捆绑包

由于本次测试，是在 Windows 下的  IIS 上测试的。
因此，需要额外安装 .NET Core 托管捆绑包

文档页
https://docs.microsoft.com/zh-cn/aspnet/core/host-and-deploy/iis/index?view=aspnetcore-2.1

下载页
https://dotnet.microsoft.com/download/thank-you/dotnet-runtime-2.2.0-windows-hosting-bundle-installer

下载 dotnet-hosting-2.2.0-win.exe
并安装。




## 修改本机 host 文件
增加
127.0.0.1			reg.test.com
127.0.0.1			a.test.com
127.0.0.1			b.test.com
127.0.0.1			c.test123.com



## 测试网站说明

reg.test.com 专门用于 登录的网站.  （ ASP.NET Core 网站 ）
项目：MySSO\MySSO.Web

a.test.com 用于测试的 业务网站. （ ASP.NET Core 网站 / 相同主域名 test.com ）
项目：MyTest\Test.Web

b.test.com 用于测试的 业务网站. （ ASP.NET 网站 / 相同主域名 test.com）
项目：todo ...

c.test123.com 用于测试的 业务网站. （ ASP.NET Core 网站 / 不同主域名 test123.com  ）
项目：todo ...



## IIS 创建网站

IIS 管理器， 分别创建网站

注意：将项目发布以后才能部署到IIS，不能直接指定项目的物理路径。
发到文件系统默认的路径应该是 bin\Release\PublishOutput，应该指定这个路径。

