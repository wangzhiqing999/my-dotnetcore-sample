using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Microsoft.EntityFrameworkCore;

using MySSO.DataAccess;
using MySSO.Service;
using MySSO.ServiceImpl;


namespace TestDomain.Web
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


            // 数据库连接字符串，定义在 appsettings.json 文件中.
            string connString = Configuration.GetConnectionString("MySSOConnection");
            // 数据库配置
            services.AddDbContext<MySSOContext>(options => options.UseSqlServer(connString));

            // ########## 用ASP.NET Core自带依赖注入(DI)注入使用的类 ##########

            // 登录服务.
            services.AddScoped(typeof(ILoginService), typeof(DefaultLoginService));





            // 登录网站是 reg.test.com
            // 本网站是 c.test123.com
            // 由于是跨域的操作， 这里不写 services.AddDataProtection() 的相关代码了.

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
                options.Cookie.Domain = ".test123.com";
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



            // 启用 Session (In-memory)
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.Cookie.Name = ".test123.Session";
                // 设置session的过期时间.
                options.IdleTimeout = TimeSpan.FromSeconds(600); 
                // 设置在浏览器不能通过js获得该cookie的值.
                options.Cookie.HttpOnly = true; 
            });
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
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();


            // 使用Cookie的中间件
            app.UseAuthentication();

            // 使用 Session. (必须在 UseMvc 之前调用)
            app.UseSession();


            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
