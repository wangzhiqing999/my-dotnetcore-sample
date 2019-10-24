using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;


namespace A3004_Middleware.Middleware
{

    /// <summary>
    /// 用于测试的 计算请求时间 的中间件
    /// </summary>
    public class ResponseTimeMiddleware
    {
        // Name of the Response Header, Custom Headers starts with "X-"
        private const string RESPONSE_HEADER_RESPONSE_TIME = "X-Response-Time-ms";

        // Handle to the next Middleware in the pipeline
        private readonly RequestDelegate _next;

        public ResponseTimeMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task InvokeAsync(HttpContext context)
        {
            // Start the Timer using Stopwatch
            var watch = new Stopwatch();
            watch.Start();
            context.Response.OnStarting(() => {
                // Stop the timer information and calculate the time
                watch.Stop();
                var responseTimeForCompleteRequest = watch.ElapsedMilliseconds;
                // Add the Response time information in the Response headers.
                context.Response.Headers[RESPONSE_HEADER_RESPONSE_TIME] = responseTimeForCompleteRequest.ToString();
                return Task.CompletedTask;
            });
            // Call the next delegate/middleware in the pipeline
            return this._next(context);
        }
    }


    /// <summary>
    /// 用于测试的 计算请求时间 的中间件 扩展方法.
    /// </summary>
    public static class ResponseTimeMiddlewareExtensions
    {
        public static IApplicationBuilder UseResponseTime(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ResponseTimeMiddleware>();
        }
    }

}
