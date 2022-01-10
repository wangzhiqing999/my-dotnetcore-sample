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

            /*
            // 测试生成 日 EMA.
            etfDayService.CalculateEtfDayEma("SH516920", DateTime.Today);

            // 测试生成 日 MACD.
            etfDayService.CalculateEtfDayMacd("SH516920", DateTime.Today);
            */






            // ETF 周线数据服务.
            IEtfWeekService etfWeekService = serviceProvider.GetService<IEtfWeekService>();

            /*
            // 测试 调用数据库的函数，来获取 周 MACD.
            List<EtfWeekMacd> etfWeekMacds = etfWeekService.GetEtfWeekMacd("SH510050");
            foreach(var item in etfWeekMacds)
            {
                Console.WriteLine(item);
            }
            */










            // #####  一个接口，多个实现的情况. #####
            IEnumerable<ITradingStrategyService> tradingStrategyServices = serviceProvider.GetServices<ITradingStrategyService>();

            /*

            Console.WriteLine("========== TWO MA ==========");

            // 获取 双日均线的交易策略.
            ITradingStrategyService twoDayMaTradingStrategyService = tradingStrategyServices.FirstOrDefault(h => h.GetType() == typeof(TwoDayMaTradingStrategyService));

            string testEtfCode = "SH510300";
            // 获取日线数据.
            List<EtfDayLine> dayLineList = etfDayService.GetEtfDayLines(testEtfCode);
            foreach(var dayItem in dayLineList)
            {
                TradingSignal tradingSignal = twoDayMaTradingStrategyService.GetTradingSignal(testEtfCode, dayItem.TradingDate);

                if (tradingSignal != TradingSignal.None)
                {
                    Console.WriteLine($"{testEtfCode} : {dayItem.TradingDate:yyyy-MM-dd} -- {tradingSignal.ToString()} -- {dayItem.ClosePrice}");
                }
            }







            Console.WriteLine("========== MACD WEEK ==========");

            // 获取 MACD 周线的交易策略.
            ITradingStrategyService weekMacdTradingStrategyService = tradingStrategyServices.FirstOrDefault(h => h.GetType() == typeof(WeekMacdTradingStrategyService));


            // 获取周线数据.
            List<EtfWeekLine> weekLineList = etfWeekService.GetEtfWeekLines(testEtfCode);
            foreach (var weekItem in weekLineList)
            {
                TradingSignal tradingSignal = weekMacdTradingStrategyService.GetTradingSignal(testEtfCode, weekItem.TradingDate);

                if (tradingSignal != TradingSignal.None)
                {
                    Console.WriteLine($"{testEtfCode} : {weekItem.TradingDate:yyyy-MM-dd} -- {tradingSignal.ToString()} -- {weekItem.ClosePrice}");
                }
            }

            */



            TestTrading(serviceProvider);

            Console.WriteLine("##### Finish！#####");
            Console.ReadLine();
        }








        private static void TestTrading(IServiceProvider serviceProvider)
        {

            Console.WriteLine("##### TestTrading！#####");

            IEnumerable<ITradingStrategyService> tradingStrategyServices = serviceProvider.GetServices<ITradingStrategyService>();

            // 获取 MACD 周线的交易策略.
            ITradingStrategyService weekMacdTradingStrategyService = tradingStrategyServices.FirstOrDefault(h => h.GetType() == typeof(WeekMacdTradingStrategyService));

            // ETF主数据服务.
            IEtfMasterService etfMasterService = serviceProvider.GetService<IEtfMasterService>();

            // ETF 周线数据服务.
            IEtfWeekService etfWeekService = serviceProvider.GetService<IEtfWeekService>();

            // 简单交易服务.
            ISimpleTradingService simpleTradingService = serviceProvider.GetService<ISimpleTradingService>();


            // 获取ETF主数据
            List<EtfMaster> etfMasters = etfMasterService.GetEtfMasterList();


            DateTime startDate = new DateTime(2020, 1, 1);

            int testTradingQuantity = 1000;


            foreach(var etfMaster in etfMasters)
            {
                string testEtfCode = etfMaster.EtfCode;

                // 获取周线数据.
                List<EtfWeekLine> weekLineList = etfWeekService.GetEtfWeekLines(testEtfCode);

                weekLineList = weekLineList.Where(p => p.TradingDate > startDate).ToList();

                for (int i = 0; i < weekLineList.Count; i ++)
                {
                    var weekItem = weekLineList[i];

                    TradingSignal tradingSignal = weekMacdTradingStrategyService.GetTradingSignal(testEtfCode, weekItem.TradingDate);



                    if (i == weekLineList.Count - 1)
                    {
                        // 最后一周，强行平仓（如果有的话）

                        TradingRequest tradingRequest = new TradingRequest()
                        {
                            TradingItemCode = testEtfCode,
                            TradingQuantity = testTradingQuantity,

                            TradingDate = weekLineList[i].TradingDate,
                            TradingPrice = weekLineList[i].ClosePrice,
                        };

                        simpleTradingService.DoClose(tradingRequest);

                        continue;
                    }


                    if (tradingSignal == TradingSignal.Buy)
                    {

                        TradingRequest tradingRequest = new TradingRequest()
                        {
                            TradingItemCode = testEtfCode,
                            TradingQuantity = testTradingQuantity,

                            TradingDate = weekLineList[i+1].TradingDate,
                            TradingPrice = weekLineList[i + 1].OpenPrice,
                        };

                        simpleTradingService.DoOpen(tradingRequest);
                    } 
                    else if (tradingSignal == TradingSignal.Sell)
                    {
                        TradingRequest tradingRequest = new TradingRequest()
                        {
                            TradingItemCode = testEtfCode,
                            TradingQuantity = testTradingQuantity,

                            TradingDate = weekLineList[i + 1].TradingDate,
                            TradingPrice = weekLineList[i + 1].OpenPrice,
                        };

                        simpleTradingService.DoClose(tradingRequest);
                    }


                }

            }

            

            
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
