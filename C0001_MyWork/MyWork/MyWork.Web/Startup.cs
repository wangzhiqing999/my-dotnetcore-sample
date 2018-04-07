using System;
using System.Text;
using System.IO;
using System.Reflection;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;

using MyAuthentication.DataAccess;
using MyAuthentication.Service;
using MyAuthentication.ServiceImpl;



using Swashbuckle.AspNetCore.Swagger;
using Microsoft.Extensions.PlatformAbstractions;


namespace MyWork.Web
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

            // ########## 用ASP.NET Core自带依赖注入(DI)注入使用的类 ##########

            // 系统服务.
            services.AddScoped(typeof(ISystemService), typeof(DefaultSystemServiceImpl));
            // 用户服务.
            services.AddScoped(typeof(IUserService), typeof(DefaultUserServiceImpl));            
            // 组织机构服务.
            services.AddScoped(typeof(IOrganizationService), typeof(DefaultOrganizationServiceImpl));
            // 角色服务.
            services.AddScoped(typeof(IRoleService), typeof(DefaultRoleServiceImpl));
            // 模块类型服务.
            services.AddScoped(typeof(IModuleTypeService), typeof(DefaultModuleTypeServiceImpl));
            // 模块服务.
            services.AddScoped(typeof(IModuleService), typeof(DefaultModuleServiceImpl));
            // 动作服务.
            services.AddScoped(typeof(IActionService), typeof(DefaultActionServiceImpl));
            // 角色-模块服务.
            services.AddScoped(typeof(IRoleModuleService), typeof(DefaultRoleModuleServiceImpl));
            // 角色-动作服务.
            services.AddScoped(typeof(IRoleActionService), typeof(DefaultRoleActionServiceImpl));
            // 用户-角色服务.
            services.AddScoped(typeof(IUserRoleService), typeof(DefaultUserRoleServiceImpl));

            // 权限认证服务
            services.AddScoped(typeof(IAuthenticationService), typeof(DefaultAuthenticationService));


            // ##########  JWT 相关配置  ########## 
            // 将appsettings.json中的JwtSettings部分文件读取到JwtSettings中，这是给其他地方用的
            services.Configure<JwtSettings>(Configuration.GetSection("JwtSettings"));

            // 由于初始化的时候我们就需要用，所以使用Bind的方式读取配置
            // 将配置绑定到JwtSettings实例中
            var jwtSettings = new JwtSettings();
            Configuration.Bind("JwtSettings", jwtSettings);

            services.AddAuthentication(options => {
                //认证middleware配置
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(o => {
                //主要是jwt  token参数设置
                o.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    //Token颁发机构
                    ValidIssuer = jwtSettings.Issuer,
                    //颁发给谁
                    ValidAudience = jwtSettings.Audience,
                    //这里的key要进行加密，需要引用Microsoft.IdentityModel.Tokens
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey)),
                    ValidateIssuerSigningKey = true,
                    //是否验证Token有效期，使用当前时间与Token的Claims中的NotBefore和Expires对比
                    ValidateLifetime = true,
                    //允许的服务器时间偏移量
                    ClockSkew = TimeSpan.Zero
                };
            });



            services.AddMvc();
            

            // Web API 允许跨域访问的配置定义.
            services.AddCors(_options =>
            {
                _options.AddPolicy("AllowCors", _builder =>
                {
                    _builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();
                });
            });




            // ##### Swagger 相关配置 #####
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(
                    "v1",
                    new Info
                    {
                        Version = "v1",
                        Title = "MyWork Web API",
                        Description = "MyWork Web API."
                    });


                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetEntryAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

        }



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();



            // ##### Swagger 相关配置 #####

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "MyWork API V1");
            });




            // #####  安全相关配置. #####
            app.UseAuthentication();


            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "MyAuth",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

            });

        }
    }





    public class JwtSettings
    {
        /// <summary>
        /// token是谁颁发的
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// token可以给哪些客户端使用
        /// </summary>
        public string Audience { get; set; }

        /// <summary>
        /// 加密的key
        /// </summary>
        public string SecretKey { get; set; }
    }

}
