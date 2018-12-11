# .Net Core Entity Framework QueryFilter 例子代码.

项目基于 .Net Core v2.2.0


### 添加引用

NuGet 添加下列包：

Microsoft.EntityFrameworkCore
Microsoft.EntityFrameworkCore.SqlServer
Microsoft.EntityFrameworkCore.Tools


注：
Microsoft.EntityFrameworkCore 是最基本的 EF 的库.
Microsoft.EntityFrameworkCore.SqlServer 是 EF 的 SQL Server 支持的包. 有这个包，才能在代码里面，写 optionsBuilder.UseSqlServer(...)
Microsoft.EntityFrameworkCore.Tools 是用于 运行  Add-Migration / Update-Database 相关命令的工具包.



### 编写测试代码.

QueryFilter 的代码， 主要写在 TestContext 的 OnModelCreating 方法中.
本例子是 modelBuilder.Entity<DocumentType>().HasQueryFilter(e => e.Status == CommonData.STATUS_IS_ACTIVE);




### 更新数据库的操作.

Add-Migration MyFirstMigration
用来生成命令，生成数据库和表的C#代码

Update-Database
执行生成的代码，更新数据库表结构.



### 存在的问题.

加了 QueryFilter 之后， 查询的时候， 会自动增加条件。
例子代码中，逻辑删除之后，将无法再次查询出来。

问题1.
当遇到有外键约束的 父子关系时， 主表数据被 逻辑删除， 看不到了。
子表的数据，依然能被检索到. 只是 子表的 主表属性 为 null.

问题2.
当遇到有外键约束的 父子关系时， 子表数据被 逻辑删除， 看不到了。
将无法对 父表的数据， 进行 物理删除。

