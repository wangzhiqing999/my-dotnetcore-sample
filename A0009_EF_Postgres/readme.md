# .Net Core Entity Framework 



## A0009_EF_Postgres
.NET 5.0

### 初始化设置
.Net Core, NuGet 添加下列包：
Microsoft.EntityFrameworkCore
Microsoft.EntityFrameworkCore.Design
Microsoft.EntityFrameworkCore.Tools
版本都是 5.0.13.  因为当前项目是 .NET 5.0 的， 6.0 以上的版本，要求 .NET 6.0

Npgsql.EntityFrameworkCore.PostgreSQL
版本是 5.0.10. 



直接复制之前的 A0005_EF_Mysql 项目的 文件，到本项目。
简单修改一下 namespace

将 .UseMySQL(......)
修改为 .UseNpgsql("PostgreSQL 的数据库连接字符串")



### 基本的处理.

Add-Migration MyFirstMigration
用来生成命令，生成数据库和表的C#代码


Script-Migration
生成创建表的 SQL 语句.
(视情况，保存这个 sql 文件，或者通过下面的 Update-Database 来直接更新数据库)

Update-Database
执行生成的代码，更新数据库表结构.
(前往测试的数据库观察， 显示表已经创建)


### 测试运行.