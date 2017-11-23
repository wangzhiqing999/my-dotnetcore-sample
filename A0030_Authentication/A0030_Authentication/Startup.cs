using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using A0030_Authentication.Data;
using A0030_Authentication.Models;
using A0030_Authentication.Services;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace A0030_Authentication
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));


            // 权限认证使用的数据库.
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
                {
                    // 注册的时候，密码强度的设置.
                    options.Password.RequireDigit = true;
                    options.Password.RequiredLength = 8;
                    options.Password.RequireNonAlphanumeric = true;
                    options.Password.RequireUppercase = true;
                    options.Password.RequireLowercase = true;
                    options.Password.RequiredUniqueChars = 2;


                    // 注册的时候，电子邮件必须要唯一.
                    options.User.RequireUniqueEmail = true;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();


            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();


            // Cookie 的设置.
            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Name = "A0030_Authentication";
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/Account/Logout";
                options.AccessDeniedPath = "/Account/AccessDenied";
                options.SlidingExpiration = true;
                // Requires `using Microsoft.AspNetCore.Authentication.Cookies;`
                options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
            });


            services.AddMvc();


            // 用于 Claims-Based Authorization 的设置.
            services.AddAuthorization(options =>
            {
                // EmployeeOnly : AspNetUserClaims 中， 当前用户有 ClaimType = EmployeeNumber 的数据.
                options.AddPolicy("EmployeeOnly", policy => policy.RequireClaim("EmployeeNumber"));

                // Founders : AspNetUserClaims 中， 当前用户有 ClaimType = EmployeeNumber 的数据. 并且  ClaimValue 为 "001", "002", "003", "004", "005" 中的一个.
                options.AddPolicy("Founders", policy =>
                          policy.RequireClaim("EmployeeNumber", "001", "002", "003", "004", "005"));
            });
        }




        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

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
