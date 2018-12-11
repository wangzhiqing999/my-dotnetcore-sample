参考网站页面


### Introduction to Identity on ASP.NET Core
https://docs.microsoft.com/en-us/aspnet/core/security/authentication/identity?tabs=visual-studio%2Caspnetcore2x

### Configure Identity
https://docs.microsoft.com/en-us/aspnet/core/security/authentication/identity-configuration?tabs=aspnetcore2x


操作步骤
1.创建 Web 项目， 选择好认证方式。
2.修改 appsetting.json 中的 DefaultConnection， 为实际使用的数据库连接字符串。
3.程序包管理器控制台下， 运行 Update-Database
4.运行 Web 项目， 在页面中， 注册一个用户。





### Simple Authorization
https://docs.microsoft.com/en-us/aspnet/core/security/authorization/simple


操作步骤
1.创建一个 控制器 TestController
2.TestController 前面加 [Authorize]， 来实现需要登录的处理。
3.TestController 类， Index 方法前， 加 [AllowAnonymous]， 来实现 允许匿名访问的处理。




### Role based Authorization
https://docs.microsoft.com/en-us/aspnet/core/security/authorization/roles


操作步骤

1.插入测试的角色数据.

INSERT INTO [AspNetRoles] (
	[Id], [ConcurrencyStamp], [Name], [NormalizedName]
) VALUES (
	NEWID(), NEWID(), 'Employee', '员工'
);

INSERT INTO [AspNetRoles] (
	[Id], [ConcurrencyStamp], [Name], [NormalizedName]
) VALUES (
	NEWID(), NEWID(), 'Admin', '管理员'
);


2.插入测试的 用户-角色 关联数据.
INSERT INTO [AspNetUserRoles](
	[UserId], [RoleId]
) VALUES (
	-- 下面是本机的 用户ID 与 Employee角色ID.
	'3c86858c-53db-4805-9865-5fe9f0f060c6', '2A835212-0209-4D10-823D-BFBBAFBC851F'
);

3.TestController 类， EmployeeInfo 方法前， 加 [Authorize(Roles = "Employee")]， 来实现 要求 Employee 角色访问的处理。
4.TestController 类， Manager 方法前， 加 [Authorize(Roles = "Admin")]， 来实现 要求 Admin 角色访问的处理。
5.通过更新 [AspNetRoles] 表， 为用户 增加/删除 角色， 来进行测试.




### Claims-Based Authorization
https://docs.microsoft.com/en-us/aspnet/core/security/authorization/claims

操作步骤
1.编辑 /Models/ApplicationUser.cs
增加一个 EmployeeNumber 属性.

2.程序包管理器控制台下， 运行 Add-Migration AddEmployeeNumber

3.程序包管理器控制台下， 运行 Update-Database

4.Startup.cs 中， 设置相关的配置代码.

5.AccountController.cs 的 Login 方法， 当登录成功的情况下，调用 _userManager.AddClaimAsync， 将用户表的 EmployeeNumber 进行插入到  [AspNetUserClaims] 的操作。

6.TestController 类， EmployeeInfo 方法前， 加 [Authorize(Policy = "EmployeeOnly")]， 来实现 要求满足 EmployeeOnly 限定条件，才能访问的处理。
7.TestController 类， SystemData 方法前， 加 [Authorize(Policy = "Founders")]， 来实现 要求满足 Founders 限定条件，才能访问的处理。

8.通过更新 [AspNetUserClaims] 表， 修改用户的 [ClaimValue]， 登出/登入 来进行测试.

注：
  这里的在 ApplicationUser.cs 里面，增加属性， 然后登录成功了，再去 _userManager.AddClaimAsync 的操作， 实在是干了多余的事情。
  实际使用的话，完全可以在管理页面， 对 [AspNetUserClaims] 表进行修改， 来完成相关的操作处理。

