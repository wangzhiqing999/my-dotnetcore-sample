using A0010_TestWebApiV9_ApiKey.Middleware;

namespace A0010_TestWebApiV9_ApiKey
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();



            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseAuthorization();

            



            // �����Լ��ġ�API ��Կ�����֤�м����
            app.UseMiddleware<ApiKeyMiddleware>();


            app.MapControllers();

            app.Run();
        }
    }
}
