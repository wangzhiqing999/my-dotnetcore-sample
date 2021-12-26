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

namespace P0002_MyEtf.TdxDataImport
{
    class Program
    {
        static void Main(string[] args)
        {

            if(args.Length != 1)
            {
                Console.WriteLine("命令行参数不正确！");
                Console.WriteLine("格式：");
                Console.WriteLine("P0002_MyEtf.TdxDataImport.exe 通达信导出的文件名");
                Console.WriteLine("例子：");
                Console.WriteLine(@"P0002_MyEtf.TdxDataImport.exe D:\data\SH#510050.txt");
                return;
            }


            IServiceCollection serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            // 构建容器
            IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();


            // ETF日线数据服务.
            IEtfDayService etfDayService = serviceProvider.GetService<IEtfDayService>();



            // 获取 ETF 日线数据.
            List<EtfDayLine> etfDayLines = ReadEtfDayLine(args[0]);

            // 遍历结果.
            foreach (EtfDayLine etfDayLine in etfDayLines)
            {
                // 插入 ETF日线数据.
                etfDayService.InsertEtfDayLine(etfDayLine);
            }


            Console.WriteLine("##### Finish！#####");
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





        private static List<EtfDayLine> ReadEtfDayLine(string fileName)
        {
            List<EtfDayLine> resultList = new List<EtfDayLine>();

            if (!File.Exists(fileName))
            {
                // 文件不存在，返回空白列表.
                return resultList;
            }

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            // 通达信 导出的文件， 编码是 GB2312.
            string[] dataLines = File.ReadAllLines(fileName, Encoding.GetEncoding("GB2312"));
            if(dataLines == null || dataLines.Length == 0)
            {
                // 空白文件，返回空白列表.
                return resultList;
            }


            // 第一行格式：
            // 510050 上证50ETF 日线 前复权
            string etfCode = dataLines[0].Substring(0, 6);
            if (fileName.Contains("SH#"))
            {
                etfCode = $"SH{etfCode}";
            } 
            else if (fileName.Contains("SZ#"))
            {
                etfCode = $"SZ{etfCode}";
            }

            // 第二行是标题行，忽略.
            //       日期	    开盘	    最高	    最低	    收盘	    成交量	    成交额
            // 从第三行开始处理数据.
            for (int i =2; i < dataLines.Length; i++)
            {
                string line = dataLines[i];
                if (line.Contains("数据来源:通达信"))
                {
                    // 这个是最后一行，结束了.
                    // 后面没有了.
                    break;
                }

                // 软件那里导出数据的时候，分隔符选择了 Tab
                // 所以，这里是用 \t 来分隔.
                string[] dataInfo = line.Split('\t');


                // 一行一个数据.
                EtfDayLine result = new EtfDayLine()
                {
                    EtfCode = etfCode,
                    TradingDate = Convert.ToDateTime(dataInfo[0]),
                    OpenPrice = Convert.ToDecimal(dataInfo[1]),
                    HighestPrice = Convert.ToDecimal(dataInfo[2]),
                    LowestPrice = Convert.ToDecimal(dataInfo[3]),
                    ClosePrice = Convert.ToDecimal(dataInfo[4]),
                    Volume = Convert.ToInt64(dataInfo[5])
                };

                // 加入结果列表.
                resultList.Add(result);
            }

            return resultList;
        }



    }
}
