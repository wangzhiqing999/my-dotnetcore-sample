


## B0250_Quartz
.NET 8.0
是从 my-csharp-sample 那里复制过来，稍作修改。
测试 在 .Net Framework 下面执行的， 迁移到 .Net Core 有没有问题。




将配置变更为使用数据库之后。
尝试同时运行 .NET 8.0  与  .Net Framework 4.6.2  的程序。
在 Job 的全路径名一样的情况下。


### 两个程序的测试。
.NET 8.0 的程序，以管理员模式运行，配置一个 HelloJob，一个 ReportWorkJob 以 Cron 方式运行。
.Net Framework 4.6.2 的程序下面，能够运行 Job 。


### 三个程序的测试。
.NET 8.0 的程序，以管理员模式运行，配置一个 HelloJob，一个 ReportWorkJob 以 Cron 方式运行。
.NET 8.0 的程序，普通模式运行。能够运行 Job。
.Net Framework 4.6.2 的程序普通运行。能够运行 Job。

一个作业，只会在一个程序上运行。
例如 HelloJob， 配置为 "0 * * * * ? *" ， 也就是 0 秒的时候运行。

在 16:00:00 的时候。
如果.NET 8.0 的程序，那里输出了 HelloJob， 那么 .Net Framework 4.6.2 的程序 就不会输出。

在 16:01:00 的时候。
如果.Net Framework 4.6.2 的程序，那里输出了 HelloJob， 那么 .NET 8.0 的程序 就不会输出。


但是，测试代码中的 TestRecoveryJob() ， 一下子，创建了 5个作业的。
```
.RequestRecovery(true)
```
这种的， 管理程序，创建任务后，最终
.NET 8.0 的程序，与 .Net Framework 4.6.2 的程序都执行了。







## B0251_QuartzConfig
.NET 8.0
使用配置文件的.

.Net Framework 版本的， 配置定义在  app.config 里面
.Net Core 这里， 就定义在 quartz.config 里面。

配置作业的  quartz_jobs.xml，  两边都一样。





## B0252_QuartzConfigDocker
.NET 8.0
使用配置文件的.

是用于 发布在 Docker 上面运行的.

配置作业的  quartz_jobs.xml
存储在 config 目录下， 用于 Docker run 的时候，允许通过 -v 参数， 将配置文件存储在外部。

代码基本上直接 复制 B0251_QuartzConfig 项目的代码。

Program.cs 做小幅修改
因为在 Docker 里面
Console.ReadLine();
无法实现阻塞的功能。
docker run 就直接过去了，最终导致 sudo docker ps -a 查询的时候， STATUS 都是 Exited (0)


发布的处理：


代码复制到 Ubuntu 机器上.

在 ~/B0250_Quartz/ 目录下编译，创建 镜像.
sudo docker build -t quartz-sample -f ./B0252_QuartzConfigDocker/Dockerfile .

查询镜像的列表
sudo docker image ls

创建容器.
sudo docker create --name quartz-sample quartz-sample

进入容器，查询是否缺文件. （exit 退出）
sudo docker run -it --rm --entrypoint "bash" quartz-sample

启动容器.
sudo docker start quartz-sample

后续没太大问题的情况下， 可以直接 run ， 相当于  create + start
sudo docker run --name quartz-sample  -d  quartz-sample

查看日志
sudo docker logs quartz-sample

查询运行状态.
sudo docker ps -a

停止容器
sudo docker stop quartz-sample

删除容器
sudo docker rm quartz-sample

删除镜像
sudo docker rmi quartz-sample






## 注意事项：
与时间相关的处理，需要考虑好 时区的问题。
例如，你预期 早上9点跑个报表的， 结果，由于时区问题， 变成 17:00 才发了。


解决办法：

### 在生成镜像的时候设置
Dockerfile 的地方，增加一行
ENV TZ=Asia/Shanghai



### 在生成容器的时候设置.
增加一个  -e TZ="Asia/Shanghai"  参数。
例如：
sudo docker run --name quartz-sample  -d   -e TZ="Asia/Shanghai"  quartz-sample











## B0253_QuartzAdoJobStore
.NET 8.0
.Net Framework 那里，数据库使用的是 MySQL 的。
在这里，尝试使用一下 SQLite






## B0254_Quartz_Web
.NET 8.0
创建一个 Web 项目，来管理作业.
数据库使用 B0253_QuartzAdoJobStore 一样的配置.

由于是 Asp.Net Core Web 项目，需要额外引用
Quartz.AspNetCore

依赖注入的关系，需要引用
Quartz.Extensions.DependencyInjection











## 关于数据库的 配置.
需要先去下载源码
https://github.com/quartznet/quartznet.git


然后，在 
quartznet\database\tables

下面，找到响应的数据库的脚本，执行。


