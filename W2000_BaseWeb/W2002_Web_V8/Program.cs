using Microsoft.AspNetCore.Mvc;

namespace W2002_Web_V8
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            

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


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}
