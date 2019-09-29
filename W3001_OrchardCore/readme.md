# 使用 OrchardCore 开发 Web 项目

使用 Visual Studio Community 2019 v16.3.1
.Net Core 3.0


## 步骤一. 创建 功能模块

这里创建三层的调用关系
MySample.DataAccess 项目 ：提供数据库服务的一层.
MySample.Service 项目 ：提供业务处理服务的一层.
MySample.Module 项目 ：模块层.


### MySample.DataAccess 项目
项目类型：Class Library(.Net Core)
NuGet 安装
Microsoft.EntityFrameworkCore.SqlServer Version 3.0.0
Microsoft.EntityFrameworkCore.Tools Version 3.0.0

创建 Model 类 与 Context 类.

临时设置 MySample.DataAccess 项目为 【启动项目】
在【Package Manager Console】运行
Add-Migration MySample_Init
生成数据库和表的C#代码

在【Package Manager Console】运行
Update-Database
执行生成的代码，更新数据库表结构.


### MySample.Service 项目
项目类型：Class Library(.Net Core)
引用 MySample.DataAccess 项目

创建 服务接口 与 默认实现.




### MySample.Module 项目
项目类型：ASP.NET Core Web Application
项目模板：Web Application (Model-View-Contorller)

NuGet 安装
Microsoft.EntityFrameworkCore.SqlServer Version 3.0.0
OrchardCore.Module.Targets
当前使用的版本是 1.0.0-rc1-10004

引用 MySample.DataAccess 项目
引用 MySample.Service 项目


编写 测试的 控制器/页面
设置控制器的路由 Route("MySample/[controller]")

将此项目，设置为启动项目，测试运行。

修改项目属性：【Output type】由 Console Application 修改为 Class Library



## 步骤二. W3001_OrchardCore 项目
项目类型：ASP.NET Core Web Application
项目模板：Web Application (Model-View-Contorller)

NuGet 安装
OrchardCore.Application.Mvc.Targets
当前使用的版本是 1.0.0-rc1-10004

引用 MySample.DataAccess 项目
引用 MySample.Service 项目
引用 MySample.Module 项目

编辑 Startup.cs
增加 OrchardCore 的相关配置.
主要是 services.AddOrchardCore().AddMvc() 与  app.UseOrchardCore()

测试运行



## 在已有的体系下，追加一个模块的操作.

### MySample2.Module 项目
项目类型：ASP.NET Core Web Application
项目模板：API

NuGet 安装
OrchardCore.Application.Mvc.Targets
当前使用的版本是 1.0.0-rc1-10004

引用 MySample.DataAccess 项目
引用 MySample.Service 项目

编写 测试的  Web Api 控制器
设置 Web Api 路由标签为 Route("api/MySample2/[controller]")

将此项目，设置为启动项目，测试运行。

测试完毕后
修改项目属性：【Output type】由 Console Application 修改为 Class Library


### 编辑 W3001_OrchardCore 项目

添加引用 引用 MySample2.Module 项目

修改 /Pages/index.cshtml 增加 Web api 的测试链接.

直接运行





## 在已有的体系下，追加一整套业务模块的操作.


### MySimpleAccessCount.DataAccess 项目
项目类型：Class Library(.Net Core)
NuGet 安装
Microsoft.EntityFrameworkCore.Sqlite Version 3.0.0
Microsoft.EntityFrameworkCore.Tools Version 3.0.0

创建 Model 类 与 Context 类.

临时设置 MySimpleAccessCount.DataAccess 项目为 【启动项目】
在【Package Manager Console】运行
Add-Migration MySAC_Init
生成数据库和表的C#代码

在【Package Manager Console】运行
Update-Database MySAC_Init
执行生成的代码，更新数据库表结构.


### MySimpleAccessCount.Service 项目
项目类型：Class Library(.Net Core)
引用 MySimpleAccessCount.DataAccess 项目

创建 服务接口 与 默认实现.


### MySimpleAccessCount.Module 项目
项目类型：ASP.NET Core Web Application
项目模板：Web Application (Model-View-Contorller)

NuGet 安装
Microsoft.EntityFrameworkCore.Sqlite Version 3.0.0
OrchardCore.Module.Targets
当前使用的版本是 1.0.0-rc1-10004

引用 MySimpleAccessCount.DataAccess 项目
引用 MySimpleAccessCount.Service 项目


编写 测试的 控制器/页面
将 MySimpleAccessCount.DataAccess 项目 目录下的 Sqlite 数据库文件复制到本项目目录.

将此项目，设置为启动项目，测试运行。


测试完毕后
修改项目属性：【Output type】由 Console Application 修改为 Class Library



### 编辑 W3001_OrchardCore 项目

添加引用
引用 MySimpleAccessCount.DataAccess 项目
引用 MySimpleAccessCount.Service 项目
引用 MySimpleAccessCount.Module 项目

修改 /Pages/index.cshtml 增加测试链接.

修改 appsettings.json 增加数据库连接字符串的配置信息.
复制 Sqlite 数据库文件到本项目目录.

运行本项目.




## 注意事项.

### 路由设置.
各个模块的控制器，顶部根据需要，定义好自己的路由前缀.


### _Layout.cshtml
主网站，使用主网站的 _Layout.cshtml
各个模块，使用自己模块里面的 _Layout.cshtml


### 数据库连接字符串
定义在各个子模块的 appsettings.json 中的 ConnectionStrings
最终需要复制到主网站的 Web 项目的 appsettings.json 中


