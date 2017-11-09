using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using Microsoft.EntityFrameworkCore;
using MyMiniTradingSystem.DataAccess;

using MyMiniTradingSystem.Service;
using MyMiniTradingSystem.ServiceImpl;


namespace MyMiniTradingSystem.Web
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
            string connString = Configuration.GetConnectionString("MyMiniTradingSystemConnection");

            // 这里的代码， 是为了【当数据库文件不存在时，自动创建】
            var optionsBuilder = new DbContextOptionsBuilder<MyMiniTradingSystemContext>();
            optionsBuilder.UseSqlite(connString);
            using (MyMiniTradingSystemContext context = new MyMiniTradingSystemContext(optionsBuilder.Options))
            {
                context.Database.EnsureCreated();
            }

            // 数据库配置
            services.AddDbContext<MyMiniTradingSystemContext>(options => options.UseSqlite(connString));

            // 用ASP.NET Core自带依赖注入(DI)注入使用的类
            services.AddScoped(typeof(ITradableCommodityService), typeof(TradableCommodityService));

            services.AddMvc();
        }



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
