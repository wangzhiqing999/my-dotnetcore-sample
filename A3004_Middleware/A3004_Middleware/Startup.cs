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

using A3004_Middleware.Service;
using A3004_Middleware.ServiceImpl;

using A3004_Middleware.Middleware;

namespace A3004_Middleware
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

            // �Լ�д�ķ���� ����ע�� ����.
            services.AddScoped(typeof(IAccessAbleService), typeof(DefaultAccessAbleService));


            services.AddControllersWithViews();
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


            

            // ���ڲ��Ե� ��������ʱ�� ���м��
            // ������������ Http Header ���￴ ��X-Response-Time-ms�� ��ֵ.
            app.UseResponseTime();




            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();




            // ���ڲ��Ե� ��־ ���м��.
            // ����� VS �ġ����������鿴
            app.UseMyLogger();

            // ���ڲ��Ե� �ж��Ƿ�ɷ��� ���м��.
            app.UseMyAccessable();




            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
