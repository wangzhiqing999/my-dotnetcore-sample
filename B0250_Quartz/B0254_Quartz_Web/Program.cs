using System.Configuration;

using Quartz;

using B0254_Quartz_Web.Jobs;
using Microsoft.Extensions.Configuration;
using Quartz.Impl.Calendar;
using Quartz.Impl;



namespace B0254_Quartz_Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddAntiforgery(o => o.HeaderName = "XSRF-TOKEN");

            // Add services to the container.
            builder.Services.AddRazorPages();



            ConfigureQuartz(builder.Services);

           

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



        static void ConfigureQuartz(IServiceCollection services)
        {
            // First we must get a reference to a scheduler
            ISchedulerFactory sf = new StdSchedulerFactory();
            IScheduler scheduler = sf.GetScheduler().Result;
            scheduler.Start();

            services.AddSingleton<IScheduler>(scheduler);
        }

    }
}
