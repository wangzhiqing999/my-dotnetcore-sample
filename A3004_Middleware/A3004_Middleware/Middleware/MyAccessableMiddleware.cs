using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;


using A3004_Middleware.Service;


namespace A3004_Middleware.Middleware
{

    /// <summary>
    /// 用于测试的 判断是否可访问 的中间件.
    /// </summary>
    public class MyAccessableMiddleware
    {

        /// <summary>
        /// 日志.
        /// </summary>
        private readonly ILogger<MyAccessableMiddleware> _Logger;

        // Handle to the next Middleware in the pipeline
        private readonly RequestDelegate _next;

        public MyAccessableMiddleware(
            ILogger<MyAccessableMiddleware> logger,
            RequestDelegate next)
        {
            _Logger = logger;
            _next = next;
        }


        // 注意： 自己写的接口/类，  在中间件的 依赖注入时， 和 mvc 中的控制器的玩法， 不一样.
        // 自己写的服务， 注入时， 直接写在方法的参数中， 而不是构造函数的参数中.
        public Task InvokeAsync(HttpContext context, IAccessAbleService _AccessAbleService)
        {

            string path = context.Request.Path;


            // 这里简单的判断路径.
            // 正常情况下，是需要判断 当前登录用户， 是否有权限，访问当前路径的操作.
            if(_AccessAbleService.IsAccessAble(path) == false)
            {                
                context.Response.Redirect($"/Home/AccessDisable?path={path}");
                return context.Response.CompleteAsync();
            }

            // Call the next delegate/middleware in the pipeline
            return this._next(context);
        }

    }


    /// <summary>
    /// 用于测试的 判断是否可访问 的中间件 扩展方法.
    /// </summary>
    public static class MyAccessableMiddlewareExtensions
    {
        public static IApplicationBuilder UseMyAccessable(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MyAccessableMiddleware>();
        }
    }

}
