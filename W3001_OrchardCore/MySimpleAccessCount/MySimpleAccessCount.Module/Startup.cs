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

            // ���ݿ������ַ����������� appsettings.json �ļ���.
            string connString = Configuration.GetConnectionString("MySimpleAccessCountConnection");

            // ���ݿ�����
            services.AddDbContext<MySimpleAccessCountContext>(options => options.UseSqlite(connString));

            // ########## ��ASP.NET Core�Դ�����ע��(DI)ע��ʹ�õ��� ##########
            // �򵥷��ʼ�������.
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
