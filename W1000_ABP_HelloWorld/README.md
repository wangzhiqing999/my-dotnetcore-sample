# ASP.NET Core & EntityFramework Core Based Startup Template

This template is a simple startup project to start with ABP
using ASP.NET Core and EntityFramework Core.

## Prerequirements

* Visual Studio 2017
* .NET Core SDK
* SQL Server

## How To Run

* Open solution in Visual Studio 2015
* Set .Web project as Startup Project and build the project.
* Run the application.






# 操作步骤



## 创建项目模板.

前往 https://aspnetboilerplate.com/Templates
Target Framework 选择 .NET Core 1.1
Architecture 选择 Multi Page Web Application
取消 Include module zero 的选择
输入项目名称
点击 Create my project! 按钮
等待项目压缩包下载
下载完毕后，使用 Visual Studio 2017 打开项目。



## 项目分析.

项目打开后，有2个目录，分别为 src 与 test
src 目录下， 有4个项目，分别为：
    .Application
	.Core
	.EntityFrameworkCore
	.Web

test 目录下， 有2个项目，分别为：
    .Tests
	.Web.Tests



## 增加业务逻辑的处理步骤


### .Core
初始生成的模板代码，不做修改。
只增加自己业务的代码。
为避免代码混乱，自己业务的代码，单独创建一个目录进行处理。

注意事项：
与微软标准的 EntityFramework Code First 不同。
微软的 EF Code First， 不要求目标类，继承哪一个基类。  对于主键的列， 在指定属性上， 添加 [Key] 标签即可。
ABP 的 Code First， 要求目标类，  继承  Abp.Domain.Entities.Entity<T> 类。  基类指定了 主键的名称为 Id。 主键的数据类型， 由 <T> 决定。
对于  Int / Long / Guid 数据类型的主键，问题不大，简单使用即可。
对于 String 类型的主键，存在一个 字符串长度的问题， 需要额外写代码， 进行配置处理.
        [StringLength(32)]
        [Required]
        public override string Id { set; get; }


### .EntityFrameworkCore
只需要在那个 DbContext.cs 文件里，简单增加相关的 set/get 属性代码。 


### .Web
修改  appsettings.json 文件
将 ConnectionStrings 中的 Default 的数据库连接字符串
将 Server=localhost; 修改为 Server=localhost\\SQLEXPRESS;
（因为本机只安装了 SQL Server Express）


### .EntityFrameworkCore 
运行指令，更新数据库.

注意事项：
.NET Core 下， 使用 Enable-Migrations -EnableAutomaticMigrations 会报错。

需要使用下面的指令， 来进行处理.

Add-Migration Migration名称
用来生成命令，生成数据库和表的C#代码

Update-Database
执行生成的代码，更新数据库表结构.



### .Application 与 .Tests
.Application 项目下， 创建服务的 DTO / 接口 / 实现.
服务接口的 参数， 不能直接使用 .Core， 而是要创建 Dto 类， 来作为参数.

.Tests 项目下， 创建 服务的单元测试。
单元测试代码写好，编译通过，在【测试资源管理器】中，能够看到，并尝试运行.



### .Web
增加页面， 首先去 Startup/PageNames.cs 那里， 增加页面名称.
	public const string SystemConfigType = "SystemConfigType";

然后， 去 Startup/项目名NavigationProvider.cs 那里， 通过 AddItem 方法， 增加菜单项目
	context.Manager.MainMenu
		.AddItem(
			new MenuItemDefinition(
				PageNames.SystemConfigType,
				L("SystemConfigType"),
				url: "SystemConfigType/Index",
				icon: "fa fa-tasks"
				)
		);

注意：这里有 L("SystemConfigType") 这样的方法。  这个实际上是国际化的相关处理方法。
具体的文字信息， 在 .Core 项目的 Localization/SourceFiles 目录下。
如果代码中， 找不到相关的文字信息， 则画面上将会显示有点奇怪的信息。


然后，创建控制器 SystemConfigTypeController
注意：直接创建 空白控制器， 生成代码会报错.  只好简单创建一个类， 然后手动继承 W1000_ABP_HelloWorldControllerBase


