
# EF Core 使用已有的数据库表

参考页面
https://docs.microsoft.com/zh-cn/ef/core/get-started/aspnetcore/existing-db?view=aspnetcore-2.1


需求：
很多项目模块，之前是用 .Net Framework 4.5 + EF 6 开发的。
现状，希望将之前的 .Net Framework 下面的模块， 一点一点， 逐步的移植到 .Net Core 下。

预期，是对于旧的模块，不用 EF Core 的 Update-Database 去生成数据库表了。
直接使用以前 .Net Framework 4.5 + EF 6  生成好的表.


本例子，使用的旧项目是
https://github.com/wangzhiqing999/my-csharp-project/tree/master/MyCustomAction.DataAccess

这个项目所创建的 数据库表.
这个是  .Net Framework 4.5 + EF 6 ， Code First 生成表的操作。


### 添加引用

NuGet 添加下列包：

Microsoft.EntityFrameworkCore
Microsoft.EntityFrameworkCore.SqlServer
Microsoft.EntityFrameworkCore.Tools







## 方案一.通过已有的数据库表结构，生成代码.

【程序包管理器控制台】
运行以下命令以从现有数据库创建模型：

Scaffold-DbContext "Server=localhost\SQLEXPRESS;Initial Catalog=MyCustomAction;Integrated Security=True;Connect Timeout=30;Encrypt=False;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Model


### 代码整理.

移除 MigrationHistory 类 （这个是  EF 6  的， 这里不需要对该表做操作）

在 MyCustomActionContext 中，移除 MigrationHistory 的相关代码.



### 结果总结

1.根据数据库表结构，生成的代码， 没有注释， 需要手动从 之前的 .Net Framework 4.5 + EF 6 项目中， 逐字段复制粘贴过来。


2.之前的在 EF 6 的代码，是 [Table("mca_service_module")] / [Column("module_code")] 的方式，来定义 表名/列名。
生成的 EF Core 的代码， 是全部定义在 DbContext 类的 OnModelCreating 方法中。
也就是最终生成的 .Net Core 类名/属性名，可能与之前 .Net Framework 4.5 下面的 类名/属性名，有略微的不一致的地方。
要做到一致的话，需要手动 【重命名】一下。

3.一对多的属性名， 可能需要手动【重命名】一下。





## 方案二.复制之前项目的代码，稍作修改.

### Model
简单复制，没有太多需要做修改的地方

### Context
using 变更， using Microsoft.EntityFrameworkCore;
构造函数变更
追加 OnConfiguring 函数
修改 OnModelCreating 函数
（之前写在 Config 里面的 一对多定义， 需要手工， 按照新的语法， 移植在这里。偷懒的话，可以直接复制 Scaffold-DbContext 一对多定义的那一行代码 ）
（需要注意的是，多对多的情况下， EntityFrameworkCore 需要增加一个中间表的类 ）

### 结果总结
1.复制的 Model， 有注释.
2.基本没有需要做【重命名】的操作

