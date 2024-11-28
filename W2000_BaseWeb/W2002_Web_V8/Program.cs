using Microsoft.AspNetCore.Mvc;
using W2002_Web_V8.Options;

namespace W2002_Web_V8
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);



            Console.WriteLine($"Application Name: {builder.Environment.ApplicationName}");
            Console.WriteLine($"Environment Name: {builder.Environment.EnvironmentName}");
            Console.WriteLine($"ContentRoot Path: {builder.Environment.ContentRootPath}");
            Console.WriteLine($"WebRootPath: {builder.Environment.WebRootPath}");



            // Add services to the container.


            // ���� AddHttpClient ��ע�� IHttpClientFactory 
            // https://learn.microsoft.com/zh-cn/aspnet/core/fundamentals/http-requests?view=aspnetcore-9.0

            builder.Services.AddHttpClient("uapis", httpClient =>
            {
                httpClient.BaseAddress = new Uri("https://uapis.cn/");
            });



            // Razor Pages �ɷ�α����֤����,FormTagHelper ����α������ע�� HTML ����Ԫ�أ���ֹ��վ������α�� (XSRF/CSRF)��
            // Ҳ������ǰ���Լ� ajax POST �ύ���ģ����ڱ�����Ϸ�α�����Ʋ����ύ��
            // Ĭ������£���α������ͨ�� HTTP �����ͷ���ͣ��ñ�ͷ����Ϊ "XSRF-TOKEN"��
            builder.Services.AddAntiforgery(o => o.HeaderName = "XSRF-TOKEN");

            // builder.Services.AddRazorPages();


            // ����������Ӧ�ã�һ�㲻���� CSRF ������
            // ����������� API ����ġ�
            // û���ȼ���ҳ�棬�õ� token�� Ȼ���� ���� token ȥ ajax POST ����Ļ���
            // �Ǿ�ͨ���������ַ�ʽ�����Է�α�����ơ�
            builder.Services.AddRazorPages()
                .AddRazorPagesOptions(o => { 
                    o.Conventions.ConfigureFilter(new IgnoreAntiforgeryTokenAttribute()); 
                });




            // ���ò��Ե�ѡ��.
            // ������Ϣ�������� appsettings.json �С�
            // https://learn.microsoft.com/zh-cn/aspnet/core/fundamentals/configuration/options?view=aspnetcore-9.0
            builder.Services.Configure<TestOptions>(
                builder.Configuration.GetSection(TestOptions.Test));




            // ���� Session ������.
            // https://learn.microsoft.com/zh-cn/aspnet/core/fundamentals/app-state?view=aspnetcore-9.0
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(10);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });




            // ����״�����
            // https://learn.microsoft.com/zh-cn/aspnet/core/host-and-deploy/health-checks?view=aspnetcore-9.0
            builder.Services.AddHealthChecks();



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();



            // ���� Session.
            // https://learn.microsoft.com/zh-cn/aspnet/core/fundamentals/app-state?view=aspnetcore-9.0
            app.UseSession();


            app.MapRazorPages();



            // ��������״�����.
            // https://learn.microsoft.com/zh-cn/aspnet/core/host-and-deploy/health-checks?view=aspnetcore-9.0
            app.MapHealthChecks("/healthz");


            app.Run();
        }
    }
}
