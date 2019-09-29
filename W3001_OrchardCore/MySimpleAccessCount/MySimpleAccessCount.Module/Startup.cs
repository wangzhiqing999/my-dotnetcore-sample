using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


using Microsoft.EntityFrameworkCore;

using MySimpleAccessCount.DataAccess;
using MySimpleAccessCount.Service;
using MySimpleAccessCount.ServiceImpl;



namespace MySimpleAccessCount.Module
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
            string connString = Configuration.GetConnectionString("MySimpleAccessCountConnection");

            // 数据库配置
            services.AddDbContext<MySimpleAccessCountContext>(options => options.UseSqlite(connString));

            // ########## 用ASP.NET Core自带依赖注入(DI)注入使用的类 ##########
            // 简单访问计数服务.
            services.AddScoped(typeof(ISimpleAccessCountService), typeof(DatabaseSimpleAccessCountService));



            services.AddControllersWithViews();
            // services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                // endpoints.MapControllers();
            });
        }
    }
}
