

### W4001_BlazorApp


项目类型：Blazor Web App

框架：.NET 8.0
身份验证类型：无
不配置 https

Interactive render mode: Server
Interactivity location: Per page/compoent

包含例子页面
不使用顶级语句。







### W4002_BootstrapBlazorWasm

项目类型：Bootstrap Blazor Project (Wasm)

也就是开始创建项目的时候，就明确，是针对 移动端的，不保持长链接的项目。





### W4003_BootstrapBlazorServerSide

项目类型：Bootstrap Blazor Project (Server-Side)


也就是开始创建项目的时候，就明确，是针对 PC端的，能保持长链接的项目。






### W4004_BootstrapBlazorServerSideWasm

项目类型：Bootstrap Blazor Project (Server-Side, Wasm)

注意：
这里将创建3个项目。一个共享、一个Server-Side, 一个Wasm。


项目名分别为：

W4004_BootstrapBlazorServerSideWasm.Shared
W4004_BootstrapBlazorServerSideWasm.Server
W4004_BootstrapBlazorServerSideWasm.WebAssembly

其中：
Server 与 WebAssembly 项目，都引用 Shared 项目。


也就是业务逻辑，主要写在 Shared 项目里面。

具体发布的时候，看情况，决定是 发布为 Server-Side, 还是 Wasm。

PC端的，能保持长链接的，发布为 Server-Side。
移动端的，不保持长链接的，发布为 Wasm。









### W4005_BootstrapBlazorAutoMode

项目类型：Bootstrap Blazor Project (Web App Auto Mode)

注意：这里将创建2个项目。


项目名分别为：

W4005_BootstrapBlazorAutoMode
W4005_BootstrapBlazorAutoMode.Client


其中：
W4005_BootstrapBlazorAutoMode 引用 W4005_BootstrapBlazorAutoMode.Client


也就是业务逻辑，主要写在 Client 项目里面。


在 VS2022 中，运行 W4005_BootstrapBlazorAutoMode.Client 项目，将报错。
在 VS2022 中，运行 W4005_BootstrapBlazorAutoMode，没有问题。

