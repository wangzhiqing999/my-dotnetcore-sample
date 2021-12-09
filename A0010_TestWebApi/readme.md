参考网站页面


### 创建 Web Api 的操作
https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-web-api





### 整合 Swagger 的操作
https://docs.microsoft.com/en-us/aspnet/core/tutorials/web-api-help-pages-using-swagger?tabs=visual-studio


安装nuget包
Swashbuckle.AspNetCore


编辑 Startup.cs
添加相关配置代码.

配置项目属性，要求生成 xml 文档.

尝试访问 api 文档页面.
http://localhost:端口号/swagger/




### 在 ASP.NET Core 中使用JWT

安装nuget包
System.IdentityModel.Tokens.Jwt
Microsoft.AspNetCore.Authentication.JwtBearer


步骤1.
编辑  Startup.cs
添加相关配置代码.

编辑 appsettings.json
添加相关配置信息.


步骤2.
对于需要有身份验证的 Web Api 控制器. （这里使用 ValuesController 作为测试目标）
添加
using Microsoft.AspNetCore.Authorization;
与
[Authorize]

此时，测试简单调用这个 Web Api，将返回错误信息。


步骤3.
编写 AuthorizeController


步骤4.
客户端先 获取 Token， 然后在 Http Header 中设置 Token，然后再调用 Web Api， 能够成功调用。



步骤5.
测试 通过令牌， 获取【当前登录用户】的详细信息。 （这里使用 UserInfoController 作为测试目标）













## 2021-02-01 更新 使用 Visual Studio 2019， 框架升级为  .NET 5.0


创建一个新的 Web Api 项目  A0010_TestWebApiV5



### 创建 Web Api 的操作
https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-5.0&tabs=visual-studio


创建完 Web API 项目后，啥事情不干，直接运行。
应该能看到一个默认的 swagger 页面。
http://localhost:62255/swagger/index.html

包含一个 /WeatherForecast Web API


也就是，创建项目的时候， Swashbuckle.AspNetCore 就已经被自动添加了，不需要自己手动再添加了。




### 数据库相关

NuGet 引用
Microsoft.EntityFrameworkCore
Microsoft.EntityFrameworkCore.InMemory

从 A0010_TestWebApi 项目
复制 
DataAccess/TodoContext.cs
Models/TodoItem.cs
Controllers/TodoController.cs

修改 Startup.cs

测试运行



### JWT 相关

安装nuget包
Microsoft.AspNetCore.Authentication.JwtBearer

后续操作，与之前的版本，差别不大。












## 2021-12-09 更新 使用 Visual Studio 2022， 框架升级为  .NET 6.0


创建一个新的 Web Api 项目  A0010_TestWebApiV6


### 创建 Web Api 的操作
https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-6.0&tabs=visual-studio


和之前的 .NET 5.0 版本的一样。
创建完 Web API 项目后，啥事情不干，直接运行。
应该能看到一个默认的 swagger 页面。





### 数据库相关

NuGet 引用
Microsoft.EntityFrameworkCore
Microsoft.EntityFrameworkCore.InMemory

从 A0010_TestWebApi 项目
复制 
DataAccess/TodoContext.cs
Models/TodoItem.cs
Controllers/TodoController.cs

修改 Program.cs

测试运行





### JWT 相关

安装nuget包
Microsoft.AspNetCore.Authentication.JwtBearer

后续操作，与之前的版本，差别不大。
就是以前是修改 Startup.cs
现在是修改 Program.cs



测试客户端 TestWebApiClient 项目， 升级到 .NET 6.0




