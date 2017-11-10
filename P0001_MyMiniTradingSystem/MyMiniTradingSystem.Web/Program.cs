using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MyMiniTradingSystem.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }



        // 注意：这里额外增加的 .UseUrls("http://*:5000") 。 
        // 是为了在 Linux 下面，能够正常的测试运行。
        // 而不是去报 
        // warn: Microsoft.AspNetCore.Server.Kestrel[0]
        // Unable to bind to http://localhost:5000 on the IPv6 loopback interface: 'Error -99 EADDRNOTAVAIL address not available'.
        // 这样的错误.
        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)                
                .UseUrls("http://*:5000")
                .UseStartup<Startup>()
                .Build();



    }
}
