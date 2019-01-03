using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Test.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }



        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {


            // 使用的加密方式是ASP.NET Core的Data Protection系统。
            // 如果在多台机器上进行托管、负载平衡或使用Web集群，
            // 则需要配置Data Protection才能使用相同的密钥和应用程序标识符。
            // 由于测试环境是 Windows. 一个 IIS 下面挂多个 测试网站，  这里就简单使用 PersistKeysToFileSystem 存储到 C:\inetpub\share
            // 实际生成环境下， 多台 Web 服务器的，需要指定一个网络的共享目录， 或者 Redis 什么的，来存储这个东西。
            services.AddDataProtection()
                //设置应用程序唯一标识
                .SetApplicationName("test.com")
                // 存储位置， 保存到文件系统 （支持 windows、Linux、macOS ）
                .PersistKeysToFileSystem(new System.IO.DirectoryInfo(@"C:\inetpub\share"));


            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => false;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            // 添加 Cookie 服务
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie(options =>
            {
                options.Cookie.Name = "AuthCookie";
                options.Cookie.Domain = ".test.com";
                options.Cookie.Path = "/";

                // 指定Cookie是否只能由服务器访问。默认为true。
                // 如果您的应用程序具有Cross -Site Scripting(XSS)的问题，更改此值可能会导致Cookie被盗用。
                //options.Cookie.HttpOnly = false;

                // 表示浏览器是否允许Cookie被附加到同一站点或跨站点的请求。
                // 默认为SameSiteMode.Lax
                //options.Cookie.SameSite = SameSiteMode.Lax;

                // 表示创建的Cookie是否应该被限制为HTTPS，HTTP或HTTPS，或与请求相同的协议。默认为SameAsRequest
                //options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;

                options.LoginPath = "/Account/LogIn";
                options.LogoutPath = "/Account/LogOff";
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();


            // 使用Cookie的中间件
            app.UseAuthentication();


            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
