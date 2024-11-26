using A3004_Middleware_V8.Middleware;

namespace A3004_Middleware_V8
{
    public class Program
    {


        // ��Դ�����������
        private const string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";


        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            // ���ÿ�Դ���� (CORS) �м��. 
            // https://learn.microsoft.com/zh-cn/aspnet/core/security/cors?view=aspnetcore-8.0#cpo6
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                    policy =>
                    {
                        // ����򵥵���������������.
                        // ʵ��ҵ���У���Ҫ����ʵ��ҵ���������.

                        // ���������Դ.
                        policy.AllowAnyOrigin();

                        // ��������� HTTP ����.
                        policy.AllowAnyMethod();

                        // �������������ͷ.
                        policy.AllowAnyHeader();
                    });
            });



            // ### ��������м��. ###
            // https://learn.microsoft.com/zh-cn/aspnet/core/performance/caching/overview?view=aspnetcore-8.0#output-caching
            builder.Services.AddOutputCache();


            // ### ��Ӧ�����м��. ###
            // https://learn.microsoft.com/zh-cn/aspnet/core/performance/caching/middleware?view=aspnetcore-8.0
            builder.Services.AddResponseCaching();


            // Add services to the container.
            builder.Services.AddRazorPages();


            var app = builder.Build();



            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
            }



            // ���ڲ��Ե� ��������ʱ�� ���м��
            // ������������ Http Header ���￴ ��X-Response-Time-ms�� ��ֵ.
            app.UseResponseTime();




            // ��̬�ļ����м��.
            // https://learn.microsoft.com/zh-cn/aspnet/core/fundamentals/static-files?view=aspnetcore-8.0
            app.UseStaticFiles();



            // ·���м��.
            // https://learn.microsoft.com/zh-cn/aspnet/core/fundamentals/routing?view=aspnetcore-8.0
            app.UseRouting();





            // ʹ����Ӧ�����м��ʱ������ UseResponseCaching ֮ǰ���� UseCors��
            // �����ս��·�ɣ�CORS �м����������Ϊ�ڶ� UseRouting �� UseEndpoints �ĵ���֮��ִ�С�
            app.UseCors(MyAllowSpecificOrigins);






            // ### ��������м��. ###
            // ��ʹ�� CORS �м����Ӧ���У�UseOutputCache ������ UseCors ֮����á�
            // �� Razor Pages Ӧ�ú;��п�������Ӧ���У�UseOutputCache ������ UseRouting ֮����á�
            app.UseOutputCache();




            app.UseAuthorization();





            // ���ڲ��Ե� ��־ ���м��.
            // ����� VS �ġ����������鿴
            app.UseMyLogger();




            // ### ��Ӧ�����м��. ###
            app.UseResponseCaching();





            app.MapRazorPages();

            app.Run();
        }
    }
}
