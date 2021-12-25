# P0002_MyEtf

用于本地存储自选的 ETF 的相关数据.
.NET  版本使用 .NET 5.0
数据库使用 postgres




## P0002_MyEtf 项目
类型：类库.
添加 NuGet 引用

Microsoft.EntityFrameworkCore
Microsoft.EntityFrameworkCore.Design
Microsoft.EntityFrameworkCore.Tools
版本 5.0.13

Npgsql.EntityFrameworkCore.PostgreSQL
版本 5.0.10


### 数据访问部分.

添加 Model / DataAccess
编译通过.

Add-Migration MyEtfInit
用来生成命令，生成数据库和表的C#代码

Script-Migration
生成创建表的 SQL 语句.
将内容保存到本地的 sql 文件中.

到目标数据库去执行一下这个 sql 脚本。


### 服务部分.

添加 Service / ServiceImpl
编译通过.





## P0002_MyEtf.SinaReader 项目
类型：控制台.
添加 NuGet 引用

Microsoft.EntityFrameworkCore
版本 5.0.13

Npgsql.EntityFrameworkCore.PostgreSQL
版本 5.0.10

Microsoft.Extensions.Configuration.Json
Microsoft.Extensions.Logging.Console
Microsoft.Extensions.Logging.Debug


添加项目引用
P0002_MyEtf

编写
Program.cs
编译通过.

测试运行.


