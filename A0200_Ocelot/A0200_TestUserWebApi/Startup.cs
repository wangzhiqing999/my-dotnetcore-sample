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

using Swashbuckle.AspNetCore.Swagger;
using Microsoft.Extensions.PlatformAbstractions;
using System.IO;

namespace A0200_TestUserWebApi
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


            // ##### Swagger 相关配置 #####
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(
                    "UserApi",
                    new Info
                    {
                        Version = "v1",
                        Title = "My Test User Web API",
                        Description = "测试的 用户服务 Web API."
                    });


                // Set the comments path for the Swagger JSON and UI.
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, "A0200_TestUserWebApi.xml");
                c.IncludeXmlComments(xmlPath);
            });



            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // ##### Swagger 相关配置 #####
            app.UseMvc()
                .UseSwagger(c =>
                {
                    c.RouteTemplate = "{documentName}/swagger.json";
                }).UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/UserApi/swagger.json", "UserApi");
                });
        }
    }
}
