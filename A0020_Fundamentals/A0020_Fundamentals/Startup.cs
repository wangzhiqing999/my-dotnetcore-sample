using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Rewrite;
using System.Net.WebSockets;
using System.Threading;

using A0020_Fundamentals.Filters;


namespace A0020_Fundamentals
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // 添加目录浏览的服务.
            services.AddDirectoryBrowser();

            // 添加路由的服务.
            services.AddRouting();


            // Adds a default in-memory implementation of IDistributedCache.
            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                // 这里设置一个10秒，目的是为了方便测试 空闲超时.
                options.IdleTimeout = TimeSpan.FromSeconds(10);
                options.Cookie.HttpOnly = true;
            });


            // 添加 MVC 的服务.
            services.AddMvc();


            services.AddScoped<HelloWorldActionFilter>();
            services.AddScoped<HelloWorldResultFilter>();
        }



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }



            // With UseDefaultFiles, requests to a folder will search for:
            // default.htm
            // default.html
            // index.htm
            // index.html
            app.UseDefaultFiles();

            // 自定义错误页面.
            app.UseExceptionHandler("/errors/500");
            app.UseStatusCodePagesWithReExecute("/errors/{0}");


            // ########## Static files 设置 ##########
            // 参考页面  https://docs.microsoft.com/en-us/aspnet/core/fundamentals/static-files

            // 默认的 Static files 设置。
            // 将 / 设置到 wwwroot
            app.UseStaticFiles();

            // ### UseStaticFiles
            // 将 /StaticFiles 设置到  wwwroot/MyStaticFiles 目录下.
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot", "MyStaticFiles")),
                RequestPath = new PathString("/StaticFiles")
            });

            // ### UseDirectoryBrowser 
            // 将 /MyDirectory 设置到  wwwroot/MyStaticFiles 目录下.
            // 注意：如果只对目录使用 UseDirectoryBrowser,  不对目录设置 UseStaticFiles。
            // 结果将导致， 仅仅能浏览目录， 但是点击链接，访问文件时，将报错.
            app.UseDirectoryBrowser(new DirectoryBrowserOptions()
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot", "MyStaticFiles")),
                RequestPath = new PathString("/MyDirectory")
            });


            // UseFileServer
            // UseFileServer 相当于对目录进行了 UseDirectoryBrowser 与 UseStaticFiles 的操作。
            // 也就是既能浏览目录，又能访问目录下面的文件内容。
            app.UseFileServer(new FileServerOptions()
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot", "MyStaticFiles")),
                RequestPath = new PathString("/MyFileServer"),
                EnableDirectoryBrowsing = true
            });





            // ########## Routing 设置 ##########.
            // 参考页面  https://docs.microsoft.com/en-us/aspnet/core/fundamentals/routing

            var trackPackageRouteHandler = new RouteHandler(context =>
            {
                var routeValues = context.GetRouteData().Values;
                return context.Response.WriteAsync(
                    $"Hello! Route values: {string.Join(", ", routeValues)}");
            });

            var routeBuilder = new RouteBuilder(app, trackPackageRouteHandler);

            routeBuilder.MapRoute(
                "Track Package Route",
                "package/{operation:regex(^(track|create|detonate)$)}/{id:int}");

            routeBuilder.MapGet("hello/{name}", context =>
            {
                var name = context.GetRouteValue("name");
                // This is the route handler when HTTP GET "hello/<anything>"  matches
                // To match HTTP GET "hello/<anything>/<anything>,
                // use routeBuilder.MapGet("hello/{*name}"
                return context.Response.WriteAsync($"Hi, {name}!");
            });

            var routes = routeBuilder.Build();
            app.UseRouter(routes);






            // ########## URL Rewriting 设置 ##########.
            // 参考页面  https://docs.microsoft.com/en-us/aspnet/core/fundamentals/url-rewriting?tabs=aspnetcore2x

            var options = new RewriteOptions()
                .AddRedirect("redirect-rule/(.*)", "hello/$1");

            app.UseRewriter(options);





            // ########## Session 设置 ##########.
            // 参考页面 https://docs.microsoft.com/en-us/aspnet/core/fundamentals/app-state?tabs=aspnetcore2x
            app.UseSession();



            


            // 使用 mvc 默认路由.
            app.UseMvcWithDefaultRoute();





            // ########## WebSockets  设置 ##########.
            // 参考页面 https://docs.microsoft.com/en-us/aspnet/core/fundamentals/websockets

            app.UseWebSockets();

            app.Use(async (context, next) =>
            {
                if (context.Request.Path == "/ws")
                {
                    if (context.WebSockets.IsWebSocketRequest)
                    {
                        WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync();
                        await Echo(context, webSocket);
                    }
                    else
                    {
                        context.Response.StatusCode = 400;
                    }
                }
                else
                {
                    await next();
                }
            });





            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});





        }



		// WebSocket 处理响应的方法.
        private async Task Echo(HttpContext context, WebSocket webSocket)
        {
            var buffer = new byte[1024 * 4];
            WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            while (!result.CloseStatus.HasValue)
            {
                await webSocket.SendAsync(new ArraySegment<byte>(buffer, 0, result.Count), result.MessageType, result.EndOfMessage, CancellationToken.None);

                result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            }
            await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
        }

    }
}
