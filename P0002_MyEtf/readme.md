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
Microsoft.EntityFrameworkCore.Relational
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
Microsoft.EntityFrameworkCore.Relational
版本 5.0.13

Npgsql.EntityFrameworkCore.PostgreSQL
版本 5.0.10

Microsoft.Extensions.Configuration.Json
Microsoft.Extensions.Logging.Console
Microsoft.Extensions.Logging.Debug
Microsoft.Extensions.Logging.Log4Net.AspNetCore

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



额外增加的处理：
增加判断，数据已存在，并且相同，则忽略的处理。 准备 1天多执行1-2次。
避免误操作，于收盘前， 先执行了一次，导致收盘后再执行，没有效果。
避免短时网络连接异常。然后当天的数据，进不去，导致缺一天的数据。
以及避免 遇到 周一 不是交易日（2022-01-03）这种， 还要强行更新数据的情况。










### 测试将项目发布到 Docker 当中.


复制 P0002_MyEtf 与 P0002_MyEtf.SinaReader 这两个目录， 到 /home/wang/MyETF 目录下.
在/home/wang/MyETF 目录下运行

docker build -t etf-sina-reader -f ./P0002_MyEtf.SinaReader/Dockerfile .


docker images
REPOSITORY                         TAG          IMAGE ID       CREATED         SIZE
etf-sina-reader                    latest       571f9c6133bb   3 minutes ago   193MB


创建容器.
（注意：这里使用 --net host 的原因，是因为配置文件里面， 数据库Server 填写的是 机器名。 ）
docker create --name etf-sina-reader \
  -v /home/wang/MyETF/P0002_MyEtf.SinaReader/config:/app/config \
  --net host \
  etf-sina-reader


测试运行
docker start etf-sina-reader



配置作业.
运行
crontab -e


在编辑器中，输入：

0 17-18 * * 1-5 docker start etf-sina-reader

保存退出




注意：
需要先在 服务器上， 执行 
sudo usermod -a --groups docker wang
将用户 wang 加入 docker 组。

这样， docker 相关命令前， 不需要加 sudo.
否则，其他的命令，可以手动加 sudo,  但是配置的作业，就没法 sudo 了。







## P0002_MyEtf.Postgrest
本项目的数据库， 使用的 postgres.
这个是尝试使用 Postgrest， 来搭建一个 Web Api 的环境.

## P0002_MyEtf.Html
这个是用于测试 P0002_MyEtf.Postgrest
搭建的一个测试页面。








## P0002_MyTrading 项目
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


#### 2022-01-10
Add-Migration MyTradingInit
用来生成命令，生成数据库和表的C#代码

Script-Migration
生成创建表的 SQL 语句.
将内容保存到本地的 sql 文件中.

到目标数据库去执行一下这个 sql 脚本。






## P0002_MyTrading.Notice 项目
功能：MACD周线金叉，发邮件通知；对于有持仓的，MACD周线死叉，发邮件通知。
使用频率：每个周末执行一次.
类型：控制台.
添加 NuGet 引用

Microsoft.EntityFrameworkCore
Microsoft.EntityFrameworkCore.Relational
版本 5.0.13

Npgsql.EntityFrameworkCore.PostgreSQL
版本 5.0.10

Microsoft.Extensions.Configuration.Json
Microsoft.Extensions.Logging.Console
Microsoft.Extensions.Logging.Debug
Microsoft.Extensions.Logging.Log4Net.AspNetCore

添加项目引用
P0002_MyEtf
P0002_MyTrading




编写
Program.cs
编译通过.

测试运行.




### 测试将项目发布到 Docker 当中.


复制 P0002_MyEtf、P0002_MyTrading 与 P0002_MyTrading.Notice 这三个目录， 到 /home/wang/MyETF 目录下.
在/home/wang/MyETF 目录下运行

docker build -t etf-trading-notice -f ./P0002_MyTrading.Notice/Dockerfile .


docker images
REPOSITORY                         TAG          IMAGE ID       CREATED         SIZE
etf-trading-notice                 latest       6c80f6e55bca   4 minutes ago   193MB


创建容器.
（注意：这里使用 --net host 的原因，是因为配置文件里面， 数据库Server 填写的是 机器名。 ）
docker create --name etf-trading-notice \
  -v /home/wang/MyETF/P0002_MyTrading.Notice/config:/app/config \
  --net host \
  etf-trading-notice


测试运行
docker start etf-trading-notice





配置作业.
运行
crontab -e


在编辑器中，输入：

15 18 * * 5 docker start etf-trading-notice

保存退出
(预期是每周五的 18:15运行)








### TestPostgrest 测试使用 postgrest-csharp 包的项目.

项目使用的数据库，为 PostgreSQL.
额外运行一个 postgrest，复杂度不大.
尝试 NuGet 引用 postgrest-csharp 包， 测试做一些基本的数据处理。













### 2022年10月修改

将各个项目的 .NET 版本， 由 .NET 5.0 变更为 .NET 6.0
管理 NuGet 程序包， 更新引用。

其中，P0002_MyEtf.SinaReader 与  P0002_MyTrading.Notice 项目，移除掉之前的 Dockerfile， 重新添加 Docker 支持。











## 2023年1月追加.

P0002_MyGrid 追加网格。
P0002_MyTrading 追加基金的持仓。




抓取基金净值的作业.

发布到 Docker 上.
docker build -t etf-trading-jobs -f ./P0002_MyTrading.Jobs/Dockerfile .


docker images
REPOSITORY                         TAG          IMAGE ID       CREATED         SIZE
etf-trading-jobs                   latest    359e846ae977   4 minutes ago   194MB


创建容器.
（注意：这里使用 --net host 的原因，是因为配置文件里面， 数据库Server 填写的是 机器名。 ）
docker create --name etf-trading-jobs \
  -v /home/wang/MyETF/P0002_MyTrading.Jobs/config:/app/config \
  --net host \
  etf-trading-jobs



测试运行
docker start etf-trading-jobs



配置作业.
运行
crontab -e


在编辑器中，输入：

30 18 * * 6 docker start etf-trading-jobs

保存退出
(预期是每周六的 18:30运行)

