# .Net Core Entity Framework

.Net Core, NuGet 添加下列包：

MySql.Data.EntityFrameworkCore
MySql.Data.EntityFrameworkCore.Design
Microsoft.EntityFrameworkCore.Design
Microsoft.EntityFrameworkCore.Tools
Microsoft.EntityFrameworkCore.Tools.DotNet


在此状态下，是如果数据库不存在， 首次运行， 能够自动创建。
但是一旦数据库创建后， 代码发生变化， 将无法通过代码去更新数据库了。



### 基本的处理.

Add-Migration MyFirstMigration
用来生成命令，生成数据库和表的C#代码

Script-Migration
生成创建表的 SQL 语句.

Update-Database
执行生成的代码，更新数据库表结构.


注意：执行 Update-Database 的时候，可能会报错， 提示  __EFMigrationsHistory' doesn't exist

处理办法：
运行 Script-Migration 生成创建表的 SQL 语句.
手动创建 __EFMigrationsHistory 表. 并手动插入相关数据.

CREATE TABLE `__EFMigrationsHistory` (
    `MigrationId` varchar(150) NOT NULL,
    `ProductVersion` varchar(32) NOT NULL,
    PRIMARY KEY (`MigrationId`)
);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`) ......



### 一对多的测试代码.

添加一对多的相关代码。
主要是 DocumentTypes 与 Documents 形成一对多.

Add-Migration AddOneToManyCode
Update-Database





### 多对多的测试代码.

添加多对多的相关代码。
主要是 MrRole 与 MrUser 形成多对多.  中间关联表为 MrUserRole
实际做法，相当于两个一对多的处理。
中间表需要额外做一个 复合主键的设置。

Add-Migration AddManyToManyCode
Update-Database




### 一对一的测试代码.
添加一对一的相关代码。
主要是 MrUser 与 MrUserExp 形成一对一

Add-Migration AddOneToOneCode
Update-Database


