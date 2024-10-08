# .Net Core Entity Framework 


### 初始化设置

.Net Core, NuGet 添加下列包：

Microsoft.EntityFrameworkCore.Sqlite
Microsoft.EntityFrameworkCore.Design
Microsoft.EntityFrameworkCore.Tools
Microsoft.EntityFrameworkCore.Tools.DotNet


在此状态下，是 如果数据库不存在， 首次运行， 能够自动创建。
但是一旦数据库创建后， 代码发生变化， 将无法通过代码去更新数据库了。




### 初始化 EF migration

需要完成的后续操作： 编辑 A0006_EF_Sqlite.csproj
将 
    <PackageReference  Include="Microsoft.EntityFrameworkCore.Tools" Version="2.0.0" />
    <PackageReference  Include="Microsoft.EntityFrameworkCore.Tools.DotNet" Version="2.0.0" />
修改为
    <DotNetCliToolReference  Include="Microsoft.EntityFrameworkCore.Tools" Version="2.0.0" />
    <DotNetCliToolReference  Include="Microsoft.EntityFrameworkCore.Tools.DotNet" Version="2.0.0" />

然后在命令行中， 项目的当前目录下， 运行
...\A0006_EF_Sqlite>dotnet ef migrations add FirstMigration

结果将提示：
Done. To undo this action, use 'ef migrations remove'

然后， 在当前目录下，将生成一个 Migrations 目录， 包含相关的生成的代码文件。



然后在命令行中， 项目的当前目录下， 运行
...\A0006_EF_Sqlite>dotnet ef database update

结果将提示：Applying migration '20171102111133_FirstMigration'.
Done.

然后，当前目录下， 将看到已经生成的 sqlite 数据库文件。





### 一对多的测试代码.

添加一对多的相关代码。
主要是 DocumentTypes 与 Documents 形成一对多.

然后在命令行中， 项目的当前目录下， 运行
...\A0006_EF_Sqlite>dotnet ef migrations add AddOneToManyCode

然后在命令行中， 项目的当前目录下， 运行
...\A0006_EF_Sqlite>dotnet ef database update

观察数据库是否正确生成了相关的表.




### 多对多的测试代码.

添加多对多的相关代码。
主要是 MrRole 与 MrUser 形成多对多.  中间关联表为 MrUserRole
实际做法，相当于两个一对多的处理。
中间表需要额外做一个 复合主键的设置。

然后在命令行中， 项目的当前目录下， 运行
...\A0006_EF_Sqlite>dotnet ef migrations add AddManyToManyCode

然后在命令行中， 项目的当前目录下， 运行
...\A0006_EF_Sqlite>dotnet ef database update

观察数据库是否正确生成了相关的表.








# 2021-12-09
创建一个 .Net 6 的项目.


NuGet 引用
Microsoft.EntityFrameworkCore
Microsoft.EntityFrameworkCore.Design
Microsoft.EntityFrameworkCore.Tools
Microsoft.EntityFrameworkCore.Sqlite




复制之前 项目的代码， 编译通过。


执行
Add-Migration MyFirstMigration
完成.
生成代码位于 Migrations 目录下.


Script-Migration
生成创建表的 SQL 语句.

Update-Database
执行生成的代码，更新数据库表结构.



测试运行.







# 2024-09-05
创建一个 .Net 8.0 的项目.

NuGet 引用
Microsoft.EntityFrameworkCore
Microsoft.EntityFrameworkCore.Design
Microsoft.EntityFrameworkCore.Tools
Microsoft.EntityFrameworkCore.Sqlite


从 A0006_EF_Sqlite_V6 项目， 复制 TestData 过来。
增加两个属性，数据类型为 JsonDocument 与 DateTime

增加两个类型转换器.
用于将 JsonDocument 与 DateTime 存储在 TEXT 类型的列里面。


执行
Add-Migration MyFirstMigration
完成.
生成代码位于 Migrations 目录下.

执行
Update-Database
执行生成的代码，更新数据库表结构.



测试运行.

