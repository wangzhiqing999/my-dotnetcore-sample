


## B0250_Quartz
是从 my-csharp-sample 那里复制过来，稍作修改。
测试 在 .Net Framework 下面执行的， 迁移到 .Net Core 有没有问题。



## B0251_QuartzConfig

使用配置文件的.

.Net Framework 版本的， 配置定义在  app.config 里面
.Net Core 这里， 就定义在 quartz.config 里面。

配置作业的  quartz_jobs.xml，  两边都一样。




## B0253_QuartzAdoJobStore

.Net Framework 那里，数据库使用的是 MySQL 的。
在这里，尝试使用一下 SQLite





## 关于数据库的 配置.
需要先去下载源码
https://github.com/quartznet/quartznet.git


然后，在 
quartznet\database\tables

下面，找到响应的数据库的脚本，执行。


