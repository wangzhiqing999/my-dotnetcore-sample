## 项目
TestWebApi : 用于测试的 Web Api 项目.
RefitClient ：使用 Refit 来编写 Web Api 客户端.
TestWebApiClient ：使用 WebApiClient.JIT 来编写 Web Api 客户端.



### TestWebApi
一个用于测试的 Web Api 项目.
客户端将调用这个 Web Api.



### RefitClient
通过 Nuget 引用 Refit .
客户端只需要编写基本的 Web Api 的接口， 以及相应的数据类.

然后，通过 RestService.For<定义的接口>(Web Api 的主机地址);
来获取服务，进行方法的调用。

Refit 的网站
https://reactiveui.github.io/refit/



### TestWebApiClient
通过 Nuget 引用 WebApiClient.JIT
客户端只需要编写基本的 Web Api 的接口， 以及相应的数据类.

然后，通过 HttpApiClient.Create<定义的接口>(config)
来获取服务，进行方法的调用。
