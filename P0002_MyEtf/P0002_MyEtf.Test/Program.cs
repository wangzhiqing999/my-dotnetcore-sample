using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using System.Net;
using System.Text;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using P0002_MyEtf.DataAccess;
using P0002_MyEtf.Service;
using P0002_MyEtf.ServiceImpl;
using P0002_MyEtf.Model;


namespace P0002_MyEtf.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            // 构建容器
            IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();



            // ETF日线数据服务.
            IEtfDayService etfDayService = serviceProvider.GetService<IEtfDayService>();


            // 测试生成 日 EMA.
            // etfDayService.CalculateEtfDayEma("SH516920", DateTime.Today);

            // 测试生成 日 MACD.
            // etfDayService.CalculateEtfDayMacd("SH516920", DateTime.Today);


            Console.WriteLine("##### Finish！#####");
            Console.ReadLine();
        }



        private static void ConfigureServices(IServiceCollection services)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("./config/app.json", false)
                .Build();

            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddConfiguration(configuration.GetSection("Logging"));
                loggingBuilder.AddConsole();
                loggingBuilder.AddDebug();
            });

            // Add access to generic IConfigurationRoot
            services.AddSingleton(configuration);


            // 数据库.
            services.AddDbContext<MyEtfContext>(opt => opt.UseNpgsql(configuration.GetSection("MyEtfContext").Value,
                x => x.MigrationsHistoryTable(HistoryRepository.DefaultTableName, "my_etf")));


            // 服务注入.
            services.AddTransient<IEtfMasterService, DefaultEtfMasterService>();
            services.AddTransient<IEtfDayService, DefaultEtfDayService>();
            services.AddTransient<IEtfWeekService, DefaultEtfWeekService>();


        }



    }
}
