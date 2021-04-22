using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace TestWebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();



                    // webBuilder.UseUrls("http://localhost:8081");
                    // 注意：写了上面这句话，将导致 Docker 里面，运行出错.
                    // Unable to bind to http://localhost:8081 on the IPv6 loopback interface: 'Cannot assign requested address'.

                });
    }
}
