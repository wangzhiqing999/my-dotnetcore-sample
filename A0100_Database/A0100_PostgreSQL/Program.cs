using A0100_PostgreSQL.Notify;
using A0100_PostgreSQL.Sample;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Npgsql;

namespace A0100_PostgreSQL
{
    internal class Program
    {
        static void Main(string[] args)
        {

            HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);


            // Database
            builder.Services.AddSingleton<NpgsqlDataSource>((sp) =>
            {
                var connectionString = builder.Configuration.GetConnectionString("ApplicationDatabase");

                if (connectionString == null)
                {
                    throw new InvalidOperationException("No ConnectionString named 'ApplicationDatabase' was found");
                }

                // Since version 7.0, NpgsqlDataSource is the recommended way to use Npgsql. When using NpsgqlDataSource,
                // NodaTime currently has to be configured twice - once at the EF level, and once at the underlying ADO.NET
                // level (there are plans to improve this):
                var dataSourceBuilder = new NpgsqlDataSourceBuilder(connectionString);

                return dataSourceBuilder.Build();
            });


            // Handles incoming Postgres Notifications
            builder.Services.AddSingleton<IPostgresNotificationHandler, TestPostgresNotificationHandler>();

            // Add Notification Service
            builder.Services.Configure<PostgresNotificationServiceOptions>(o => o.ChannelName = "test_event");

            builder.Services.AddHostedService<PostgresNotificationService>();

            IHost host = builder.Build();

            host.Run();

/*
            // 测试简单读取.
            TestRead.DoTest();

            Console.WriteLine("Finish!");
            Console.ReadLine();
*/
        }
    }
}
