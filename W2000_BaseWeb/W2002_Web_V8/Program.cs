using Microsoft.AspNetCore.Mvc;

namespace W2002_Web_V8
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            

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


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}
