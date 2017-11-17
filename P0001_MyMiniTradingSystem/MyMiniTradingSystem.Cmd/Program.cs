using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.CommandLine;
using Microsoft.EntityFrameworkCore;

using log4net;
using log4net.Repository;
using log4net.Config;

using MyMiniTradingSystem.DataAccess;
using MyMiniTradingSystem.Model;
using MyMiniTradingSystem.Service;
using MyMiniTradingSystem.ServiceImpl;
using System.Collections.Generic;

namespace MyMiniTradingSystem.Cmd
{
    class Program
    {
        /// <summary>
        /// 日志处理类.
        /// </summary>
        static ILog logger = null;


        static void Main(string[] args)
        {            
            using (MyMiniTradingSystemContext context = new MyMiniTradingSystemContext())
            {
                // 数据库不存在的情况下，创建它.
                context.Database.EnsureCreated();
            }

            // Log4Net 配置.
            ILoggerRepository repository = LogManager.CreateRepository("NETCoreRepository");
            // 默认简单配置，输出至控制台
            XmlConfigurator.Configure(repository, new FileInfo("log4net.config"));
            logger = LogManager.GetLogger(repository.Name, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            

            // 命令行处理.
            var cmdLineConfig = new CommandLineConfigurationProvider(args);
            cmdLineConfig.Load();

            // 操作.
            string option = null;
            cmdLineConfig.TryGet("op", out option);
           
            // 股票代码.
            string code = null;
            cmdLineConfig.TryGet("code", out code);

            if (logger.IsInfoEnabled)
            {
                logger.Info($"Option = {option};  Code = {code}");
            }


            if(String.IsNullOrEmpty(option))
            {
                logger.Warn("非法的命令格式！");
                return;
            }


            // 命令大写.
            option = option.ToLower();


            switch(option)
            {
                case "create":
                    // 创建.
                    DoCreateOption(code, cmdLineConfig);
                    break;

                case "sinaimport":
                    // 新浪行情导入
                    DoSinaImportOption(code, cmdLineConfig);
                    break;

                case "open":
                    // 开仓命令
                    DoOpenOption(code, cmdLineConfig);
                    break;

                case "close":
                    // 平仓命令
                    DoCloseOption(code, cmdLineConfig);
                    break;

                case "summary":
                    // 统计命令
                    DoDailySummaryOption(code, cmdLineConfig);
                    break;

                default:
                    logger.Warn($"未知的命令: {option} ！");
                    break;
            }



            Console.ReadKey();
        }



        /// <summary>
        /// 新增.
        /// /op=create /code=sh601006 /name=大秦铁路
        /// </summary>
        /// <param name="code"></param>
        /// <param name="config"></param>
        static void DoCreateOption(string code, ConfigurationProvider config)
        {
            // 股票名称.
            string name = null;
            config.TryGet("name", out name);
            if (String.IsNullOrEmpty(name))
            {
                logger.Warn("未填写名称参数 [name] ！");
                return;
            }

            TradableCommodityService service = new TradableCommodityService();
            TradableCommodity newData = new TradableCommodity()
            {
                // 代码.
                CommodityCode = code,
                // 名称.
                CommodityName = name,
                // 1手 = 100股.
                NumOfOneHand = 100,
                // 保证金 100% == 无杠杆.
                DepositRatio = 100,
            };

            ServiceResult result = service.CreateTradableCommodity(newData);
            if (logger.IsDebugEnabled)
            {
                logger.Debug($"CreateTradableCommodity [{code}, {name}], Result = {result}");
            }
        }



        /// <summary>
        /// 新浪行情导入.
        /// /op=sinaimport /code=sh601006
        /// </summary>
        /// <param name="code"></param>
        /// <param name="config"></param>
        static void DoSinaImportOption(string code, ConfigurationProvider config)
        {
            SinaCommodityPriceReader reader = new SinaCommodityPriceReader();
            List<CommodityPrice> dataList = reader.GetCommodityPriceList(code, DateTime.Today, DateTime.Today);
            CommodityPrice newData = dataList[0];
            // 代码
            newData.CommodityCode = code;

            CommodityPriceService service = new CommodityPriceService();
            ServiceResult result = service.InsertOrUpdateCommodityPrice(newData);
            if (logger.IsDebugEnabled)
            {
                logger.Debug($"InsertOrUpdateCommodityPrice [{newData}], Result = {result}");
            }
        }



        /// <summary>
        /// 开仓命令.
        /// /op=open /code=sh601006 /u=000000 /q=100
        /// </summary>
        /// <param name="code"></param>
        /// <param name="config"></param>
        static void DoOpenOption(string code, ConfigurationProvider config)
        {
            // 帐户代码.
            string user = null;
            config.TryGet("u", out user);
            if (String.IsNullOrEmpty(user))
            {
                logger.Warn("未填写账户参数 [u] ！");
                return;
            }

            string quantity = null;
            config.TryGet("q", out quantity);
            if (String.IsNullOrEmpty(quantity))
            {
                logger.Warn("未填写开仓的数量 [q] ！");
                return;
            }

            int iQuantity;
            if(!Int32.TryParse(quantity, out iQuantity))
            {
                logger.Warn("无效的开仓数量 [q] ！");
                return;
            }

            PositionService service = new PositionService();

            Position newData = new Position()
            {
                // 代码.
                CommodityCode = code,

                // 用户.
                UserCode = user,

                // 数量.
                Quantity = iQuantity,

                // 做多.
                IsLong = true,
            };

            ServiceResult result = service.OpenPosition(newData);
            if (logger.IsDebugEnabled)
            {
                logger.Debug($"OpenPosition [{newData}], Result = {result}");
            }
        }



        /// <summary>
        /// 平仓命令
        /// /op=close /code=sh601006 /u=000000 /q=100
        /// </summary>
        /// <param name="code"></param>
        /// <param name="config"></param>
        static void DoCloseOption(string code, ConfigurationProvider config)
        {
            // 帐户代码.
            string user = null;
            config.TryGet("u", out user);
            if (String.IsNullOrEmpty(user))
            {
                logger.Warn("未填写账户参数 [u] ！");
                return;
            }

            string quantity = null;
            config.TryGet("q", out quantity);
            if (String.IsNullOrEmpty(quantity))
            {
                logger.Warn("未填写平仓的数量 [q] ！");
                return;
            }

            int iQuantity;
            if (!Int32.TryParse(quantity, out iQuantity))
            {
                logger.Warn("无效的平仓数量 [q] ！");
                return;
            }

            PositionService service = new PositionService();

            Position newData = new Position()
            {
                // 代码.
                CommodityCode = code,

                // 用户.
                UserCode = user,

                // 数量.
                Quantity = iQuantity,

                // 做多.
                IsLong = true,
            };

            ServiceResult result = service.ClosePosition(newData);
            if (logger.IsDebugEnabled)
            {
                logger.Debug($"ClosePosition [{newData}], Result = {result}");
            }
        }



        /// <summary>
        /// 统计命令
        /// /op=summary /u=000000 /d=2017-11-17
        /// </summary>
        /// <param name="code"></param>
        /// <param name="config"></param>
        static void DoDailySummaryOption(string code, ConfigurationProvider config)
        {
            // 帐户代码.
            string user = null;
            config.TryGet("u", out user);
            if (String.IsNullOrEmpty(user))
            {
                logger.Warn("未填写账户参数 [u] ！");
                return;
            }

            string date = null;
            config.TryGet("d", out date);
            if (String.IsNullOrEmpty(date))
            {
                logger.Warn("未填写统计日期 [d] ！");
                return;
            }

            DateTime summaryDate;
            if (!DateTime.TryParse(date, out summaryDate))
            {
                logger.Warn("无效的统计日期 [d] ！");
                return;
            }

            DailySummaryService service = new DailySummaryService();

            ServiceResult result = service.BuildOneUserDailySummary(user, summaryDate);
            if (logger.IsDebugEnabled)
            {
                logger.Debug($"BuildOneUserDailySummary [{user}  {summaryDate}], Result = {result}");
            }
        }


    }
}
