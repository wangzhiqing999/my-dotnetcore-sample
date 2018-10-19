using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using Ocelot.DependencyInjection;
using Ocelot.Middleware;




namespace A0200_Ocelot
{
    public class Program
    {


        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseUrls("http://localhost:9090")
                .Build();


        /*
        public static void Main(string[] args)
        {
            new WebHostBuilder()
                        .UseKestrel()
                        .UseUrls("http://localhost:9090")
                        .UseContentRoot(Directory.GetCurrentDirectory())
                        .ConfigureAppConfiguration((hostingContext, config) =>
                        {
                            config
                                .SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
                                .AddJsonFile("appsettings.json", true, true)
                                .AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", true, true)
                                .AddJsonFile("ocelot.json")
                                .AddEnvironmentVariables();
                        })
                        .ConfigureServices(s => {
                            s.AddOcelot();
                        })
                        .ConfigureLogging((hostingContext, logging) =>
                        {
                            //add your logging
                            logging.AddConsole();
                        })
                        .Build()
                        .Run();

        }
        */

    }
}
