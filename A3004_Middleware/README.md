## 中间件 例子代码.

参考网址：
https://docs.microsoft.com/zh-cn/aspnet/core/fundamentals/middleware/?view=aspnetcore-3.0
https://docs.microsoft.com/zh-cn/aspnet/core/fundamentals/middleware/write?view=aspnetcore-3.0


创建 ASP.NET Core web application
选择 类型 Web Application （Model-View-Controller）


中间件代码，写在 Middleware 目录下.
一般是需要写一个中间件，和一个中间件的扩展方法.

中间件，只完成辅助功能，不返回内容的情况下，一般是调用 await _next(context); 来将工作交给下一个中间件。
中间件，自身提供内容，或者到此结束的情况下，则不调用 _next(context), 直接返回内容回去。


中间件的使用，需要注意位置。
因为存在 中间件的调用顺序。
放在前面的中间件，先执行，放在后面的中间件，后执行.

放在 app.UseStaticFiles(); 前面， 意味着 所有的 样式，图片，都将执行这个 中间件。
放在 app.UseEndpoints... 的前面， 意味着，只有走到控制器了，才会执行这个中间件。

