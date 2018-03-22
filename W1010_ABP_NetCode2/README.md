# 操作步骤



## 创建项目模板.

前往 https://aspnetboilerplate.com/Templates

选择 ASP.NET Core 2.x
选择 .NET Core(Cross Platform)
选择 Multi Page Web Application
保留 Include login,register,user ... 的选择
输入项目名称  W1010_ABP_NetCode2
输入图片验证码
点击 Create my project! 按钮
等待项目压缩包下载
下载完毕后，使用 Visual Studio 2017 打开项目。





## 项目分析.

项目打开后，有2个目录，分别为 src 与 test
src 目录下， 有7个项目，分别为：
    .Application
	.Core
	.EntityFrameworkCore
	.Migrator
	.Web.Core
	.Web.Host
	.Web.Mvc

test 目录下， 有1个项目，为：
    .Tests




## 初始化数据库

1. 修改 .Migrator 项目中的 appsettings.json
更改其 ConnectionStrings 中的 Default 的数据库连接字符串， 为可连接的字符串.

2. 运行	.Migrator 项目。
按 y 生成/ 更新数据库.
	


## 测试运行网站.

1. 修改 .Web.Mvc 项目中的 appsettings.json
将 ConnectionStrings 中的 Default 的数据库连接字符串
修改成 与  .Migrator 项目中一样的字符串。

2. 设置 .Web.Mvc 项目为启动项目。
运行。

3. 初始用户名/密码  admin/123qwe
观察运行结果.





## 测试运行 Web Api 服务.

1. 修改 .Web.Host 项目中的 appsettings.json
将 ConnectionStrings 中的 Default 的数据库连接字符串
修改成 与  .Migrator 项目中一样的字符串。



2. 设置 .Web.Host 项目为启动项目。
运行。


3. 浏览 swagger 的 Web Api 文档页面。






## 测试增加一个模块.

### .Code 项目.
增加一个 Tasks 的目录，用于存储 任务相关的基础类.

修改 /Authorization/PermissionNames.cs
增加 public const string Pages_Tasks = "Pages.Tasks";


数据库中， 需要执行
INSERT INTO [dbo].[AbpPermissions](
	[CreationTime],[CreatorUserId],[Discriminator],
	[IsGranted],[Name],[TenantId],[RoleId],[UserId]
) VALUES (
	GETDATE(),	NULL,	'RolePermissionSetting',
	1,	'Pages.Tasks',		NULL,  1,  NULL
)
GO


修改 /Authorization/W1010_ABP_NetCode2AuthorizationProvider.cs
增加 context.CreatePermission(PermissionNames.Pages_Tasks, L("Tasks"));



修改 /Localization/SourceFiles/ .xml
根据语种， 增加 <text name="Tasks">Tasks</text>




### .EntityFrameworkCore 项目.
DbContext 类， 增加一行
public DbSet<Task> Tasks { get; set; }

PM>Add-Migration AddTasks
PM>Update-Database





### .Application 项目.
增加一个 Tasks 的目录，用于存储 任务相关的 DTO / 服务接口 / 服务实现.


### .Web.Mvc 项目.
增加 Tasks 控制器.
增加 Tasks 相关视图.
增加 Tasks 相关 js 脚本代码.

/Startup/PageNames 类中， 增加 Tasks 页面名称.
/Startup/W1010_ABP_NetCode2NavigationProvider 类中， 增加菜单相关处理.








### 新增通用模块的处理.

对于 Other 类，可以实现下列接口：

ICreationAudited		包含 创建用户 与 创建时间.
IModificationAudited	包含 最后修改用户 与 最后修改时间.
IDeletionAudited		包含 数据是否被删除 与 删除用户 与 删除时间.  （软删除的处理）
IPassivable				包含 状态
IMayHaveTenant			包含 租户代码 （可选）
IMustHaveTenant			包含 租户代码 （必选）


对于 IOtherAppService 接口，继承 IAsyncCrudAppService<OtherDto, Int64, PagedResultRequestDto, CreateOtherDto, OtherDto>
自动定义了基本的 增/删/改/查  的处理接口.


对于 OtherAppService 类， 实现  AsyncCrudAppService<Other, OtherDto, Int64, PagedResultRequestDto, CreateOtherDto, OtherDto>, IOtherAppService
自动包含了基本的 增/删/改/查  的处理实现.




### 关于租户的处理 
W1010_ABP_NetCode2ApplicationModule 类的 PreInitialize() 方法中，加一行
Configuration.MultiTenancy.IsEnabled = true;

对于实现 IMustHaveTenant 的实体。
代码上面，不需要做任何的处理。
查询的时候，会自动忽略 其他租户的数据。
插入的时候，会自动填写当前租户的代码。

对于实现 IMayHaveTenant 的实体。
代码上面，需要做一些额外的处理。
查询的时候，会自动忽略 其他租户的数据。
插入的时候，好像需要自己手动填写当前租户的代码， 否则插入的结果， 租户代码列， 数值还是 NULL.


