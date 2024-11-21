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











## 2024-09-20 更新 使用 Visual Studio 2022， 框架升级为  .NET 8.0


创建一个新的 Web Api 项目  A0010_TestWebApiV8


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
简单从 A0010_TestWebApiV6 项目那里， 把相关的代码，复制过来。



测试客户端 TestWebApiClient 项目， 升级到 .NET 8.0



### 配置全局异常处理.

添加 GlobalExceptionHanlder 类，用于处理全局异常.

Program.cs 中，添加

// 配置全局异常处理.
builder.Services.AddExceptionHandler<GlobalExceptionHanlder>();
builder.Services.AddProblemDetails();
与
// 配置使用全局异常处理.
app.UseExceptionHandler();


添加一个 TestExceptionController 的控制器，测试方法中，简单 抛出异常。

控制台运行项目.

浏览器访问
http://localhost:5000/api/TestException

得到反馈：
{
    "title": "Internal Server Error",
    "status": 500,
    "detail": "API Error 测试异常",
    "instance": "API"
}








## 2024-11-21 更新 使用 Visual Studio 2022， 框架升级为  .NET 9.0
Visual Studio 2022 版本：17.21.1

创建一个新的 Web Api 项目  A0010_TestWebApiV9

框架：.NET 9.0
身份验证类型：无
不配置 HTTPS
不启用容器支持
不启用 OpenAPI 支持
不使用顶级语句
使用控制器
不在 .NET Aspire 业务流程中登记。


创建完 Web API 项目后，啥事情不干，直接运行。
因为 Microsoft 已从 .NET 9 中删除内置的 Swagger 支持 （Swashbuckle）。
本次又没有启用 OpenAPI 支持.
所以，是没有一个浏览的页面的。

输出窗口显示：
Microsoft.Hosting.Lifetime: Information: Now listening on: http://localhost:5164
Microsoft.Hosting.Lifetime: Information: Application started. Press Ctrl+C to shut down.
Microsoft.Hosting.Lifetime: Information: Hosting environment: Development

打开浏览器，测试访问：
http://localhost:5164/WeatherForecast



### Swagger 的替代方案：Scalar.AspNetCore

1、安装 nuget 包
Scalar.AspNetCore

2、修改 Program.cs

添加：
builder.Services.AddOpenApi();

与 
if (app.Environment.IsDevelopment())
{
    app.MapScalarApiReference(); // scalar/v1
    app.MapOpenApi();
}

运行

打开浏览器，测试访问：
http://localhost:5164/scalar/v1




### 数据库相关

NuGet 引用
Microsoft.EntityFrameworkCore
Microsoft.EntityFrameworkCore.InMemory

从 A0010_TestWebApi 项目
复制 
DataAccess/TodoContext.cs
Models/TodoItem.cs
Controllers/TodoController.cs

修改命名空间

修改 Program.cs

测试运行

打开浏览器，测试访问：
http://localhost:5164/scalar/v1






### JWT 相关

安装nuget包
Microsoft.AspNetCore.Authentication.JwtBearer

后续操作，与之前的版本，差别不大。
简单从 A0010_TestWebApiV8 项目那里， 把相关的代码，复制过来。

复制的代码包含：
/Controllers/AuthorizeController.cs
/Controllers/ValuesController.cs
/Models/LoginViewModel.cs
/JwtSettings.cs

复制后，修改命名空间。
控制器修改一下顶部的标签。

修改 appsettings.json

修改 Program.cs


测试运行

打开浏览器，测试访问：
http://localhost:5164/scalar/v1



先直接测试 /Values  返回没有权限的错误。
然后再调用 /Authorize 获取 token

再次调用 /Values 
在 Http Headers 的地方，添加 
Key = Authorization
Value = Bearer 调用/Authorize 获得的 token






