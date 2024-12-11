using System.Configuration;

using Quartz;

using B0254_Quartz_Web.Jobs;
using Microsoft.Extensions.Configuration;
using Quartz.Impl.Calendar;
using Quartz.Impl;
using Quartz.Impl.Matchers;
using System.Globalization;
using static System.Formats.Asn1.AsnWriter;



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


            services.AddQuartz(q =>
            {
                // handy when part of cluster or you want to otherwise identify multiple schedulers
                q.SchedulerId = "MyScheduler";
                q.SchedulerName = "MyScheduler";

                // these are the defaults
                q.UseSimpleTypeLoader();


                q.UseMicrosoftDependencyInjectionJobFactory();
                


                q.UseDefaultThreadPool(tp =>
                {
                    tp.MaxConcurrency = 10;
                });

   
                // example of persistent job store using JSON serializer as an example
                q.UsePersistentStore(s =>
                {
                    s.PerformSchemaValidation = true; // default
                    s.UseProperties = true; // preferred, but not default
                    s.RetryInterval = TimeSpan.FromSeconds(15);
                    
                    s.UseMicrosoftSQLite(sqlite =>
                    {

                        sqlite.ConnectionString = "Data Source=quartz.db";
                        // this is the default
                        sqlite.TablePrefix = "QRTZ_";
                    });


                    s.UseSystemTextJsonSerializer();
                    // s.UseNewtonsoftJsonSerializer();                    
                });



                /*

                var jobKey = new JobKey("awesome job", "awesome group");
                q.AddJob<HelloJob>(jobKey, j => j
                    .WithDescription("my awesome job")
                );

                q.AddTrigger(t => t
                    .WithIdentity("Simple Trigger")
                    .ForJob(jobKey)
                    .StartNow()
                    .WithSimpleSchedule(x => x.WithInterval(TimeSpan.FromSeconds(10)).RepeatForever())
                    .WithDescription("my awesome simple trigger")
                );

                */

            });



            services.AddTransient<HelloJob>();


            services.AddQuartzHostedService(options =>
            {
                // when shutting down we want jobs to complete gracefully
                options.WaitForJobsToComplete = true;


                
            });



            /*
            // First we must get a reference to a scheduler
            ISchedulerFactory sf = new StdSchedulerFactory();
            IScheduler scheduler = sf.GetScheduler().Result;
            scheduler.Start();

            services.AddSingleton<IScheduler>(scheduler);

            
            */
        }

    }
}
