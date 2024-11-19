# .Net Core Entity Framework 


注意：管理 NuGet 程序包的时候，不能像在 .NET 里面，简单去添加 EntityFramework 这个了。

因为会报

包 EntityFramework 6.1.3 与 netcoreapp1.1 (.NETCoreApp,Version=v1.1) 不兼容。 包 EntityFramework 6.1.3 支持:
  - net40 (.NETFramework,Version=v4.0)
  - net45 (.NETFramework,Version=v4.5)
一个或多个包与 .NETCoreApp,Version=v1.1 不兼容。

.Net Core, 需要添加 Microsoft.EntityFrameworkCore.SqlServer 这个包.

为了使用 Code First， 还需要添加 Microsoft.EntityFrameworkCore.Tools 这个包.
否则在程序包控制台中， Add-Migration 之类的相关命令无法执行。

在别的文档中提示， 还需要添加  Microsoft.EntityFrameworkCore.Design 这个包.


尝试使用 Enable-Migrations -EnableAutomaticMigrations， 结果被拒绝了。
PM> Enable-Migrations -EnableAutomaticMigrations
Enable-Migrations is obsolete. Use Add-Migration to start using Migrations.


Add-Migration MyFirstMigration
用来生成命令，生成数据库和表的C#代码

Update-Database
执行生成的代码，更新数据库表结构.





## 机密管理.

参考文档：
https://learn.microsoft.com/zh-cn/aspnet/core/security/app-secrets?view=aspnetcore-9.0&tabs=windows

避免把 数据库IP、用户名、密码 这些信息，写在源代码或者是配置文件中，然后发布到代码管理器上。


NuGet 引用
Microsoft.Extensions.Configuration
Microsoft.Extensions.Configuration.UserSecrets


在 VS2022 中，项目，右键，选择【管理用户机密】
弹出 secrets.json 。
输入
{
  "TestConnectionString": "Data Source=localhost\SQLEXPRESS;Initial Catalog=Test;User Id=sa;Password=123456;"
}

文件实际存储在当前机器的
C:\Users\当前用户\AppData\Roaming\Microsoft\UserSecrets\b712394a-b9fa-49a6-9acc-e33bcc4221ab
目录下。

代码提交到 git 的时候，是不会把这个文件提交上去的。




