using Microsoft.AspNetCore.Mvc;
using W2002_Web_V8.Options;

namespace W2002_Web_V8
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);



            Console.WriteLine($"Application Name: {builder.Environment.ApplicationName}");
            Console.WriteLine($"Environment Name: {builder.Environment.EnvironmentName}");
            Console.WriteLine($"ContentRoot Path: {builder.Environment.ContentRootPath}");
            Console.WriteLine($"WebRootPath: {builder.Environment.WebRootPath}");



            // Add services to the container.


            // 调用 AddHttpClient 来注册 IHttpClientFactory 
            // https://learn.microsoft.com/zh-cn/aspnet/core/fundamentals/http-requests?view=aspnetcore-9.0

            builder.Services.AddHttpClient("uapis", httpClient =>
            {
                httpClient.BaseAddress = new Uri("https://uapis.cn/");
            });



            // Razor Pages 由防伪造验证保护,FormTagHelper 将防伪造令牌注入 HTML 窗体元素，防止跨站点请求伪造 (XSRF/CSRF)。
            // 也就是以前可以简单 ajax POST 提交表单的，现在必须配合防伪造令牌才能提交。
            // 默认情况下，防伪造令牌通过 HTTP 请求标头发送，该标头名称为 "XSRF-TOKEN"。
            builder.Services.AddAntiforgery(o => o.HeaderName = "XSRF-TOKEN");

            // builder.Services.AddRazorPages();


            // 对于内网的应用，一般不会做 CSRF 防护。
            // 如果是做成像 API 服务的。
            // 没有先加载页面，拿到 token， 然后再 带着 token 去 ajax POST 请求的话。
            // 那就通过下面这种方式，忽略防伪造令牌。
            builder.Services.AddRazorPages()
                .AddRazorPagesOptions(o => { 
                    o.Conventions.ConfigureFilter(new IgnoreAntiforgeryTokenAttribute()); 
                });




            // 配置测试的选项.
            // 配置信息，定义在 appsettings.json 中。
            // https://learn.microsoft.com/zh-cn/aspnet/core/fundamentals/configuration/options?view=aspnetcore-9.0
            builder.Services.Configure<TestOptions>(
                builder.Configuration.GetSection(TestOptions.Test));




            // 启用 Session 的配置.
            // https://learn.microsoft.com/zh-cn/aspnet/core/fundamentals/app-state?view=aspnetcore-9.0
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(10);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });




            // 运行状况检查
            // https://learn.microsoft.com/zh-cn/aspnet/core/host-and-deploy/health-checks?view=aspnetcore-9.0
            builder.Services.AddHealthChecks();



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();



            // 启用 Session.
            // https://learn.microsoft.com/zh-cn/aspnet/core/fundamentals/app-state?view=aspnetcore-9.0
            app.UseSession();


            app.MapRazorPages();



            // 启用运行状况检查.
            // https://learn.microsoft.com/zh-cn/aspnet/core/host-and-deploy/health-checks?view=aspnetcore-9.0
            app.MapHealthChecks("/healthz");


            app.Run();
        }
    }
}
