


### A0050_BlazorApp
创建 Blazor WebAssembly 项目.

框架： .NET 6.0
身份验证类型： 无

配置 HTTPS 不打勾
ASP.NET Core 托管 不打勾
渐进式 Web 应用程序 不打勾
Do not use top-level statement 打勾


创建结果：
一个项目

默认生成的例子代码中。
前端，向后端发起请求的时候，实际访问的， 是 \wwwroot\sample-data\weather.json 文件。
并不是什么 Web Api 接口.


也就是，如果单纯的写 前端的代码，  Web Api 是其他人写的话， 可以尝试用这种操作。



### A0051_BlazorApp
创建 Blazor WebAssembly 项目.

框架： .NET 6.0
身份验证类型： 无

配置 HTTPS 不打勾
ASP.NET Core 托管 打勾
渐进式 Web 应用程序 不打勾
Do not use top-level statement 打勾


创建结果：
三个项目

一个 Shared （基本 Model 数据）
一个 Client （页面）
一个 Server （Web Api）

设置 Server 项目为 启动项目。
因为项目的引用关系，  Server 项目引用了 Client 项目。
所以运行的时候， 能看到页面。
Client项目的前端，向Server 项目的后端发起请求的时候，也能够正常运行。


如果前端/后端的代码， 都是自己写的话，使用这种方式，比较好划分。







### A0058_BlazorAppGrpc

参考
https://mp.weixin.qq.com/s/4PZU49u_AY-_8dCK-9U47A



先创建一个 ASP.NET Core gRPC 服务的项目 ： A0058_GrpcService

框架： .NET 6.0
启用 Docker 不打勾
Do not use top-level statement 打勾


项目添加 NuGet 引用 Grpc.AspNetCore.Web

Program.cs 追加

允许跨域的代码.


以及
app.UseGrpcWeb();


将例子代码的
app.MapGrpcService<GreeterService>();
变更为
app.MapGrpcService<GreeterService>().EnableGrpcWeb();




然后，创建 Blazor WebAssembly 项目 ：A0058_BlazorApp

框架： .NET 6.0
身份验证类型： 无

配置 HTTPS 不打勾
ASP.NET Core 托管 不打勾
渐进式 Web 应用程序 不打勾
Do not use top-level statement 打勾


项目添加 NuGet 引用
Google.Protobuf
Grpc.Net.Client
Grpc.Net.Client.Web
Grpc.Tools


修改项目文件
添加  \A0058_GrpcService\Protos\*.proto  的引用.


修改 Program.cs 
追加 Grpc 的配置.
注意：ForAddress("https://localhost:7128"...   如果不使用 https, 而是使用 http://localhost:5128 的话，运行的时候，会得不到结果。


修改 Pages/Index.razor
增加 调用 GreeterClient.SayHelloAsync 的方法。



最后，解决方案上，右键-【属性】
启动项目--【多个启动项目】
A0058_GrpcService 与 A0058_BlazorApp 选择启动。
运行后，观察执行结果。