# P0002_MyEtf

用于本地存储自选的 ETF 的相关数据.
.NET  版本使用 .NET 5.0
数据库使用 postgres





项目预期功能

1.每日收盘后，拉取当天的基本行情数据，开盘、收盘、最高、最低。（通过配置作业来定时执行）
根据最基本的行情数据，完成一些简单的技术指标计算。
某些简单的技术指标，发出 买入的信号时，发送邮件通知自己。



2.实际买入操作后，记录下开仓的相关信息。
某些简单的技术指标，发出 卖出的信号，需要做止盈、止损的操作时，发送邮件通知自己。





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






## P0002_MyEtf.TdxDataImport 项目
功能：将 通达信 导出的日线数据，导入到本地。
使用频率：项目初始化的时候，执行一次；  新增一个自选的 ETF 代码的时候，执行一次.
类型：控制台.
添加 NuGet 引用

Microsoft.EntityFrameworkCore
版本 5.0.13

Npgsql.EntityFrameworkCore.PostgreSQL
版本 5.0.10

Microsoft.Extensions.Configuration.Json
Microsoft.Extensions.Logging.Console
Microsoft.Extensions.Logging.Debug

System.Text.Encoding.CodePages


添加项目引用
P0002_MyEtf


注意事项：

最初
File.ReadAllLines(fileName, Encoding.GetEncoding("GB2312"));
会报错：Unhandled exception. System.ArgumentException: 'GB2312' is not a supported encoding name. 

需要 NuGet 引用 System.Text.Encoding.CodePages
并在读取文件前面，添加下面的代码：
Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);





## P0002_MyEtf.SinaReader 项目
功能：从新浪读取今天的收盘数据.
使用频率：交易日每天执行一次.
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





