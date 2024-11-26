using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;

namespace A3004_Middleware_V8.Middleware
{

    /// <summary>
    /// 用于测试的 日志 的中间件.
    /// </summary>
    public class MyLoggerMiddleware
    {

        /// <summary>
        /// 日志.
        /// </summary>
		private readonly ILogger<MyLoggerMiddleware> _Logger;


        private readonly RequestDelegate _next;


        public MyLoggerMiddleware(
            ILogger<MyLoggerMiddleware> logger,
            RequestDelegate next)
        {
            _Logger = logger;
            _next = next;
        }



        public async Task InvokeAsync(HttpContext context)
        {

            this._Logger.LogDebug($"########## {context.Request.Path} Start.");            

            // Call the next delegate/middleware in the pipeline
            await _next(context);

            this._Logger.LogDebug($"########## {context.Request.Path} Finish.");
        }



    }



    /// <summary>
    /// 用于测试的 日志 的中间件 扩展方法.
    /// </summary>
    public static class MyLoggerMiddlewareExtensions
    {
        public static IApplicationBuilder UseMyLogger(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MyLoggerMiddleware>();
        }
    }

}
