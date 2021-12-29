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




预期的开发步骤.

1.将其它软件导出的日线数据，导入到本地。

2.交易日，抓取数据，写入本地。

3.日线数据生成周线数据。

4.根据周线，计算周 MACD.

5.MACD周线金叉，发邮件通知。

6.记录交易。

7.对于有持仓的，MACD周线死叉，发邮件通知。







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


#### 2021-12-24
Add-Migration MyEtfInit
用来生成命令，生成数据库和表的C#代码

Script-Migration
生成创建表的 SQL 语句.
将内容保存到本地的 sql 文件中.

到目标数据库去执行一下这个 sql 脚本。



#### 2021-12-27
Add-Migration MyEtfMacd
Script-Migration -From MyEtfInit -To MyEtfMacd

Add-Migration MyEtfEma
Script-Migration -From MyEtfMacd -To MyEtfEma

注意事项：
如果只是简单 Script-Migration， 不加任何参数的话，将会生成从零开始的，全部的 sql 语句。




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

测试发布到本地的文件夹中.

将本机的 P0002_MyEtf\P0002_MyEtf.SinaReader\bin\Release\net5.0\publish  目录下的，发布好的内容，
复制到测试的 Ubuntu 服务器的 /home/wang/MyETF/SinaReader 目录下.


### 给测试的 Ubuntu 服务器安装 dotnet-runtime-5.0

参考页面：
https://docs.microsoft.com/zh-cn/dotnet/core/install/linux-ubuntu

执行：
wget https://packages.microsoft.com/config/ubuntu/21.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
sudo dpkg -i packages-microsoft-prod.deb
rm packages-microsoft-prod.deb


由于开发时，选择的版本时 .NET 5.0.   下面的安装 运行时的脚本， 最终是 dotnet-runtime-5.0
sudo apt-get update
sudo apt-get install -y apt-transport-https
sudo apt-get update
sudo apt-get install -y dotnet-runtime-5.0

测试运行
dotnet P0002_MyEtf.SinaReader.dll


### 给测试的 Ubuntu 服务器配置定时作业.

在当前目录下，创建一个
start_SinaReader.sh

内容为：
cd /home/wang/MyETF/SinaReader
dotnet P0002_MyEtf.SinaReader.dll

增加可执行权限.
chmod 777 start_SinaReader.sh

配置作业.
运行
crontab -e

在编辑器中，输入：

0 18 * * 1-5 /home/wang/MyETF/SinaReader/start_SinaReader.sh

保存退出










## P0002_MyEtf.Postgrest
本项目的数据库， 使用的 postgres.
这个是尝试使用 Postgrest， 来搭建一个 Web Api 的环境.

## P0002_MyEtf.Html
这个是用于测试 P0002_MyEtf.Postgrest
搭建的一个测试页面。



