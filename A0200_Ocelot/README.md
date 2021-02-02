
## 三个项目
A0200_TestUserWebApi : 测试的用户的 Web Api.
A0200_TestOrderWebApi : 测试的订单的 Web Api.
A0200_Ocelot : 网关项目.



### A0200_TestUserWebApi
项目类型： ASP.Net Core Web API 项目
添加一个 UsersController ， 并编写相关 web api 测试代码.
完成 Swagger 相关配置代码. (安装nuget包: Swashbuckle.AspNetCore)
配置此项目，运行在 9091端口上.
测试运行
http://localhost:9091/swagger



### A0200_TestOrderWebApi
项目类型： ASP.Net Core Web API 项目
添加一个 OrdersController ， 并编写相关 web api 测试代码.
完成 Swagger 相关配置代码. (安装nuget包: Swashbuckle.AspNetCore)
配置此项目，运行在 9092端口上.
测试运行
http://localhost:9092/swagger



### A0200_Ocelot :
项目类型： ASP.Net Core Empty
安装nuget包: Ocelot
新增 ocelot.json 配置文件. 填写相关配置路由信息.
配置参考文档：
https://ocelot.readthedocs.io/en/latest/features/configuration.html

复制网站的启动代码.
参考代码：
https://ocelot.readthedocs.io/en/latest/introduction/gettingstarted.html


完成 Swagger 相关配置代码. (安装nuget包: Swashbuckle.AspNetCore)


配置此项目，运行在 9090端口上.
测试运行
http://localhost:9090/swagger



### TODO
当前的例子代码， 所有的路由， 是写死的。
也就是说， 如果 A0200_TestUserWebApi 里面， 增加了 Web 服务。
在 swagger 的页面上，能够看到， 但是不能立即调用。
因为这个新的地址，没有做配置，A0200_Ocelot 不知道要将这个请求，提交到 9091 还是 9092，结果就只能报 404 了。












## 网关项目，升级到 .NET 5.0
之前的 A0200_Ocelot 项目， 框架是 .NET Core 2.0
新增的 A0205_Ocelot 项目， 框架是 .NET 5.0

创建项目类型， Web API 项目.
安装nuget包: Ocelot 版本 17.0.0

从  A0205_Ocelot 项目， 复制 ocelot.json 到 A0205_Ocelot 项目
将 "ReRoutes" 修改为 "Routes"

修改 Startup.cs
测试运行。

对比 A0200_Ocelot 与 A0205_Ocelot 的运行结果。




## Web Api 项目， 升级到  .NET 5.0

### A0205_TestItemWebApi
项目类型： ASP.Net Core Web API 项目，框架是 .NET 5.0
添加一个 ItemController ， 并编写相关 web api 测试代码.
完成 Swagger 相关配置代码. 
配置此项目，运行在 9095端口上.
测试运行
http://localhost:9095/swagger


修改  A0205_Ocelot 项目的 ocelot.json 文件。
测试运行，观察结果。

当前这个节点下：
网关 A0205_Ocelot  		.NET 5.0
A0200_TestUserWebApi  	.NET Core 2.0
A0200_TestOrderWebApi  	.NET Core 2.0
A0205_TestItemWebApi  	.NET 5.0




