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