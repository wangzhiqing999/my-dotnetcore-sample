
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

reg.test.com 专门用于 登录的网站.  （ ASP.NET Core 网站 .Net Core 2.2）
项目：MySSO\MySSO.Web

a.test.com 用于测试的 同域的业务网站. （ ASP.NET Core 网站  .Net Core 2.2 / 相同主域名 test.com ）
项目：MyTest\Test.Web

b.test.com 用于测试的 老版本项目的业务网站. （ ASP.NET 网站  .Net Frameword 4.5 / 相同主域名 test.com）
项目：MyTest\TestOld.Web

c.test123.com 用于测试的 跨域的业务网站. （ ASP.NET Core 网站 .Net Core 2.2 / 不同主域名 test123.com  ）
项目：MyTest\TestDomain.Web




## IIS 创建网站

IIS 管理器， 分别创建网站

注意：将项目发布以后才能部署到IIS，不能直接指定项目的物理路径。
发到文件系统默认的路径应该是 bin\Release\PublishOutput，应该指定这个路径。

对于 .Net Core 的网站， 网站创建完毕后，到应用程序池， 将 .NET CLR 版本， 修改为 “无托管代码”




## 数据库初始化

针对 MySSO\MySSO.DataAccess 项目

Script-Migration
生成创建表的 SQL 语句. 然后自己去数据库客户端，执行脚本。

Update-Database
直接更新数据库表结构.

执行 MySSO\MySSO.Schema 下的 TestData.sql
插入测试用户数据.




## 测试

### 同域测试

访问 http://a.test.com
登录时，将自动跳转至 reg.test.com 的登录页
登录完成后，自动跳转回 a.test.com

同一个浏览器，同时开两个页面，分别为 a.test.com 与 reg.test.com
reg.test.com 登录后， 刷新 a.test.com ， 也是登录状态。
reg.test.com 登出后， 刷新 a.test.com ， 也是登出状态。



### 跨域测试

访问 http://c.test123.com
登录时，将自动跳转至 reg.test.com 的登录页
登录完成后，自动跳转回 c.test123.com

同一个浏览器，同时开两个页面，分别为 c.test123.com 与 reg.test.com
reg.test.com 登录后，要 c.test123.com 访问特定页面，触发到 “需要登录” 的请求时，才会触发 c.test123.com 进入登录状态。

在 c.test123.com 点击登出按钮时，会先在 c.test123.com 进行登出，然后跳转至 reg.test.com 完成登出，最后再跳转回 c.test123.com
在 reg.test.com 点击登出按钮时，暂时没有去 c.test123.com 执行自动登出的操作。



### 老版本项目的测试.

ASP.NET 网站  .Net Frameword 4.5 的网站.

由于 将 .Net Core 的 AddDataProtection / AddAuthentication 一套东西， 移植到 .Net Framework 的操作， 尚不明确。
这里是简单按照 跨域的操作， 进行处理。
最终测试结果， 与前面的 跨域测试 的结果相同。


