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
using P0002_MyTrading.Notice.Util;

namespace P0002_MyTrading.Notice
{
    class Program
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


            IEnumerable<ITradingStrategyService> tradingStrategyServices = serviceProvider.GetServices<ITradingStrategyService>();

            // 获取 MACD 周线的交易策略.
            ITradingStrategyService weekMacdTradingStrategyService = tradingStrategyServices.FirstOrDefault(h => h.GetType() == typeof(WeekMacdTradingStrategyService));

            // ETF主数据服务.
            IEtfMasterService etfMasterService = serviceProvider.GetService<IEtfMasterService>();

            // ETF 周线数据服务.
            IEtfWeekService etfWeekService = serviceProvider.GetService<IEtfWeekService>();

            // 获取ETF主数据
            List<EtfMaster> etfMasters = etfMasterService.GetEtfMasterList();

            // 简单交易服务.
            ISimpleTradingService simpleTradingService = serviceProvider.GetService<ISimpleTradingService>();



            StringBuilder builder = new StringBuilder();


            foreach (var etfMaster in etfMasters)
            {
                string etfCode = etfMaster.EtfCode;

                _Logger.LogInformation(etfCode);

                EtfWeekLine etfWeekLine = etfWeekService.GetLastEtfWeekLines(etfCode);
                if(etfWeekLine == null)
                {
                    _Logger.LogWarning($"{etfCode} Data Not Found!");
                    continue;
                }

                TradingSignal tradingSignal = weekMacdTradingStrategyService.GetTradingSignal(etfCode, etfWeekLine.TradingDate);


                if (tradingSignal == TradingSignal.Buy)
                {
                    // 买入信号.
                    _Logger.LogWarning($"{etfCode} Buy!");


                    builder.AppendLine($"{etfMaster.EtfCode} {etfMaster.EtfName} 发出买入信号！");

                }
                else if (tradingSignal == TradingSignal.Sell)
                {
                    // 查询，是否有持仓.
                    bool hasPosition = simpleTradingService.HasPosition(etfCode);
                    if (hasPosition)
                    {
                        // 有持仓 + 卖出信号.
                        _Logger.LogWarning($"{etfCode} Sell!");

                        builder.AppendLine($"{etfMaster.EtfCode} {etfMaster.EtfName} 发出卖出信号！");
                    }
                }

            }


            string mailText = builder.ToString();

            if(mailText.Length > 10)
            {
                MailSender.SendMail("交易信号与通知！", mailText);
            }


            _Logger.LogInformation($"{DateTime.Today:yyyy-MM-dd} Finish!");

        }






        private static void ConfigureServices(IServiceCollection services)
        {


            // ############################################################
            // 注意：
            // 之前使用 .NET 5.0 正常运行的程序，  在升级到 .NET 6.0 之后，运行会报错。
            // 错误信息是： Cannot write DateTime with Kind=Unspecified to PostgreSQL type 'timestamp with time zone', only UTC is supported......
            // 解决办法是在初始化的时候，先调用下面这一行代码.
            // ############################################################
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);


            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("./config/app.json", false)
                .Build();

            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddConfiguration(configuration.GetSection("Logging"));
                loggingBuilder.AddConsole();
                loggingBuilder.AddDebug();
                loggingBuilder.AddLog4Net();
            });

            // Add access to generic IConfigurationRoot
            services.AddSingleton(configuration);


            // 数据库.
            services.AddDbContext<MyEtfContext>(opt => opt.UseNpgsql(configuration.GetSection("MyEtfContext").Value,
                x => x.MigrationsHistoryTable(HistoryRepository.DefaultTableName, "my_etf")));

            services.AddDbContext<MyTradingContext>(opt => opt.UseNpgsql(configuration.GetSection("MyEtfContext").Value,
                x => x.MigrationsHistoryTable(HistoryRepository.DefaultTableName, "my_trading")));


            // 服务注入.
            services.AddTransient<IEtfMasterService, DefaultEtfMasterService>();
            services.AddTransient<IEtfDayService, DefaultEtfDayService>();
            services.AddTransient<IEtfWeekService, DefaultEtfWeekService>();


            services.AddTransient<ISimpleTradingService, DefaultSimpleTradingService>();




            // #####  一个接口，多个实现的情况. #####
            services.AddTransient<ITradingStrategyService, WeekMacdTradingStrategyService>();
            services.AddTransient<ITradingStrategyService, TwoDayMaTradingStrategyService>();



        }


    }
}
