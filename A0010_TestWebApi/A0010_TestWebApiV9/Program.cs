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


            // ʹ���ڴ����ݿ���в���.
            builder.Services.AddDbContext<TodoContext>(opt => opt.UseInMemoryDatabase("TodoList"));



            // Add services to the container.

            builder.Services.AddControllers();


            // builder.Services.AddOpenApi();
            builder.Services.AddOpenApi(opt =>
            {
                // ���һ���ĵ�ת���������ڴ����ض��İ�ȫ����������: Bearer Token����
                opt.AddDocumentTransformer<BearerSecuritySchemeTransformer>();
            });



            // #####  JWT �������. #####

            // ��appsettings.json�е�JwtSettings�����ļ���ȡ��JwtSettings�У����Ǹ������ط��õ�
            builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

            // ���ڳ�ʼ����ʱ�����Ǿ���Ҫ�ã�����ʹ��Bind�ķ�ʽ��ȡ����
            // �����ð󶨵�JwtSettingsʵ����
            var jwtSettings = new JwtSettings();
            builder.Configuration.Bind("JwtSettings", jwtSettings);

            builder.Services.AddAuthentication(options => {
                //��֤middleware����
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(o => {
                //��Ҫ��jwt  token��������
                o.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    //Token�䷢����
                    ValidIssuer = jwtSettings.Issuer,
                    //�䷢��˭
                    ValidAudience = jwtSettings.Audience,
                    //�����keyҪ���м��ܣ���Ҫ����Microsoft.IdentityModel.Tokens
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey)),
                    ValidateIssuerSigningKey = true,
                    //�Ƿ���֤Token��Ч�ڣ�ʹ�õ�ǰʱ����Token��Claims�е�NotBefore��Expires�Ա�
                    ValidateLifetime = true,
                    //����ķ�����ʱ��ƫ����
                    ClockSkew = TimeSpan.Zero
                };
            });




            var app = builder.Build();



            if (app.Environment.IsDevelopment())
            {
                
                app.MapOpenApi();


                // ӳ���Զ���� API �ĵ�·�ɣ���ʹ�� Fluent API �������ã����磺���ñ��⡢���⡢������ȡ�
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

            // ������������app.UseAuthorization();ǰ�棬�������ƺ���ע������
            app.UseAuthentication();


            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
