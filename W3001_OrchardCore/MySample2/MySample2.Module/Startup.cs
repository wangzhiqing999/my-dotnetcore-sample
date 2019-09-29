using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;


using Microsoft.EntityFrameworkCore;

using MySample.DataAccess;
using MySample.Service;
using MySample.ServiceImpl;


namespace MySample2.Module
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
            string connString = Configuration.GetConnectionString("MySampleConnection");

            // 数据库配置
            services.AddDbContext<MySampleContext>(options => options.UseSqlServer(connString));


            // ########## 用ASP.NET Core自带依赖注入(DI)注入使用的类 ##########
            // 测试学校服务.
            services.AddScoped(typeof(ITestSchoolService), typeof(DefaultTestSchoolService));
            // 测试老师服务.
            services.AddScoped(typeof(ITestTeacherService), typeof(DefaultTestTeacherService));




            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
