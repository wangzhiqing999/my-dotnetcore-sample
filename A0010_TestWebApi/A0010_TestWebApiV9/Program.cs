using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

using A0010_TestWebApiV9.DataAccess;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using A0010_TestWebApiV9.Transformers;


namespace A0010_TestWebApiV9
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            // 使用内存数据库进行测试.
            builder.Services.AddDbContext<TodoContext>(opt => opt.UseInMemoryDatabase("TodoList"));



            // Add services to the container.

            builder.Services.AddControllers();


            // builder.Services.AddOpenApi();
            builder.Services.AddOpenApi(opt =>
            {
                // 添加一个文档转换器，用于处理特定的安全方案（例如: Bearer Token）。
                opt.AddDocumentTransformer<BearerSecuritySchemeTransformer>();
            });



            // #####  JWT 相关配置. #####

            // 将appsettings.json中的JwtSettings部分文件读取到JwtSettings中，这是给其他地方用的
            builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

            // 由于初始化的时候我们就需要用，所以使用Bind的方式读取配置
            // 将配置绑定到JwtSettings实例中
            var jwtSettings = new JwtSettings();
            builder.Configuration.Bind("JwtSettings", jwtSettings);

            builder.Services.AddAuthentication(options => {
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




            var app = builder.Build();



            if (app.Environment.IsDevelopment())
            {
                
                app.MapOpenApi();


                // 映射自定义的 API 文档路由，并使用 Fluent API 进行配置，例如：设置标题、主题、侧边栏等。
                app.MapScalarApiReference(options =>
                {
                    options
                        .WithTitle("My custom API")
                        .WithTheme(ScalarTheme.Mars)
                        .WithSidebar(true)
                        .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient)
                        .WithPreferredScheme("ApiKey")
                        .WithApiKeyAuthentication(x => x.Token = "my-api-key");
                });
            }


            // Configure the HTTP request pipeline.

            // 这个必须添加在app.UseAuthorization();前面，两个名称很像，注意区别。
            app.UseAuthentication();


            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
