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
using P0002_MyEtf.ServiceModel;

using P0002_MyTrading.DataAccess;
using P0002_MyTrading.Service;
using P0002_MyTrading.ServiceImpl;
using P0002_MyTrading.ServiceModel;
using P0002_MyTrading.Model;

namespace P0002_MyTrading.Jobs
{
    internal class Program
    {
        private static ILogger<Program> _Logger;


        static void Main(string[] args)
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            // 构建容器
            IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

            _Logger = serviceProvider.GetService<ILogger<Program>>();




            _Logger.LogInformation($"{DateTime.Today:yyyy-MM-dd} Start!");




            IHoldingService holdingService = serviceProvider.GetService<IHoldingService>();
            IItemPriceReader itemPriceReader = serviceProvider.GetService<IItemPriceReader>();




            List<HoldingReport> holdingReports = holdingService.GetLastHoldingReport();
            foreach (HoldingReport report in holdingReports)
            {
                if(report.ReaderName == "eastmoney")
                {

                    var priceResult = itemPriceReader.ReadPrice(report.ItemCode);

                    Console.WriteLine($"{report.ItemCode} - {priceResult}");


                    if (priceResult.IsSuccess)
                    {
                        ItemPriceData itemPriceData = priceResult.ResultData;
                        HoldingLog holdingLog = new HoldingLog()
                        {
                            ItemCode = report.ItemCode,
                            LogDate = Convert.ToDateTime(itemPriceData.Date.Trim(')')),
                            Price = Convert.ToDecimal(itemPriceData.Price),
                            Quantity = report.Quantity,
                        };
                       
                        holdingService.SaveHoldingLog(holdingLog);
                    }


                    // 避免高频访问被网站咔嚓.
                    Thread.Sleep(2345);
                }


                
            }


            _Logger.LogInformation($"{DateTime.Today:yyyy-MM-dd} Finish!");



            // Console.ReadLine();
        }









        private static void ConfigureServices(IServiceCollection services)
        {

            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("./config/app.json", false)
                .Build();

            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddConfiguration(configuration.GetSection("Logging"));
            });

            // Add access to generic IConfigurationRoot
            services.AddSingleton(configuration);


            // 数据库.
            services.AddDbContext<MyEtfContext>(opt => opt.UseNpgsql(configuration.GetSection("MyEtfContext").Value,
                x => x.MigrationsHistoryTable(HistoryRepository.DefaultTableName, "my_etf")));

            services.AddDbContext<MyTradingContext>(opt => opt.UseNpgsql(configuration.GetSection("MyEtfContext").Value,
                x => x.MigrationsHistoryTable(HistoryRepository.DefaultTableName, "my_trading")));






            services.AddTransient<IHoldingService, DefaultHoldingService>();
            services.AddTransient<IItemPriceReader, EastmoneyItemPriceReader>();


        }



    }

}