using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Server.HttpSys;

namespace A0020_Fundamentals
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }



        // 下面的 .UseKestrel， 参考页面 https://docs.microsoft.com/en-us/aspnet/core/fundamentals/servers/kestrel?tabs=aspnetcore2x
        // 下面的 .UseHttpSys， 参考页面 https://docs.microsoft.com/en-us/aspnet/core/fundamentals/servers/httpsys
        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .UseKestrel(options =>
                {
                    options.Limits.MaxConcurrentConnections = 100;
                    options.Limits.MaxConcurrentUpgradedConnections = 100;
                    options.Limits.MaxRequestBodySize = 10 * 1024;
                })
                 //.UseHttpSys(options =>
                 //{
                 //    options.Authentication.Schemes = AuthenticationSchemes.None;
                 //    options.Authentication.AllowAnonymous = true;
                 //    options.MaxConnections = 100;
                 //    options.MaxRequestBodySize = 30000000;
                 //})
                .Build();
    }
}
