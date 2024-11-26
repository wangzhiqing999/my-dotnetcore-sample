using A3004_Middleware_V8.Middleware;

namespace A3004_Middleware_V8
{
    public class Program
    {


        // 跨源请求策略名称
        private const string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";


        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            // 启用跨源请求 (CORS) 中间件. 
            // https://learn.microsoft.com/zh-cn/aspnet/core/security/cors?view=aspnetcore-8.0#cpo6
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                    policy =>
                    {
                        // 这里简单地配置了允许所有.
                        // 实际业务中，需要根据实际业务进行配置.

                        // 设置允许的源.
                        policy.AllowAnyOrigin();

                        // 设置允许的 HTTP 方法.
                        policy.AllowAnyMethod();

                        // 设置允许的请求头.
                        policy.AllowAnyHeader();
                    });
            });



            // ### 输出缓存中间件. ###
            // https://learn.microsoft.com/zh-cn/aspnet/core/performance/caching/overview?view=aspnetcore-8.0#output-caching
            builder.Services.AddOutputCache();


            // ### 响应缓存中间件. ###
            // https://learn.microsoft.com/zh-cn/aspnet/core/performance/caching/middleware?view=aspnetcore-8.0
            builder.Services.AddResponseCaching();


            // Add services to the container.
            builder.Services.AddRazorPages();


            var app = builder.Build();



            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
            }



            // 用于测试的 计算请求时间 的中间件
            // 结果在浏览器的 Http Header 那里看 【X-Response-Time-ms】 的值.
            app.UseResponseTime();




            // 静态文件的中间件.
            // https://learn.microsoft.com/zh-cn/aspnet/core/fundamentals/static-files?view=aspnetcore-8.0
            app.UseStaticFiles();



            // 路由中间件.
            // https://learn.microsoft.com/zh-cn/aspnet/core/fundamentals/routing?view=aspnetcore-8.0
            app.UseRouting();





            // 使用响应缓存中间件时，请在 UseResponseCaching 之前调用 UseCors。
            // 对于终结点路由，CORS 中间件必须配置为在对 UseRouting 和 UseEndpoints 的调用之间执行。
            app.UseCors(MyAllowSpecificOrigins);






            // ### 输出缓存中间件. ###
            // 在使用 CORS 中间件的应用中，UseOutputCache 必须在 UseCors 之后调用。
            // 在 Razor Pages 应用和具有控制器的应用中，UseOutputCache 必须在 UseRouting 之后调用。
            app.UseOutputCache();




            app.UseAuthorization();





            // 用于测试的 日志 的中间件.
            // 结果在 VS 的【输出】那里查看
            app.UseMyLogger();




            // ### 响应缓存中间件. ###
            app.UseResponseCaching();





            app.MapRazorPages();

            app.Run();
        }
    }
}
