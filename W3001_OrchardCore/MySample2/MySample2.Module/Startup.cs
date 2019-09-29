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


            // ���ݿ������ַ����������� appsettings.json �ļ���.
            string connString = Configuration.GetConnectionString("MySampleConnection");

            // ���ݿ�����
            services.AddDbContext<MySampleContext>(options => options.UseSqlServer(connString));


            // ########## ��ASP.NET Core�Դ�����ע��(DI)ע��ʹ�õ��� ##########
            // ����ѧУ����.
            services.AddScoped(typeof(ITestSchoolService), typeof(DefaultTestSchoolService));
            // ������ʦ����.
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
