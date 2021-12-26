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


namespace P0002_MyEtf.SinaReader
{
    class Program
    {
        static void Main(string[] args)
        {

            IServiceCollection serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            
            // 构建容器
            IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();





            // ETF主数据服务.
            IEtfMasterService etfMasterService = serviceProvider.GetService<IEtfMasterService>();
            // ETF日线数据服务.
            IEtfDayService etfDayService = serviceProvider.GetService<IEtfDayService>();
            // ETF周线数据服务.
            IEtfWeekService etfWeekService = serviceProvider.GetService<IEtfWeekService>();


            // 获取ETF主数据
            List<EtfMaster> etfMasters = etfMasterService.GetEtfMasterList();

            // 获取 ETF 日线数据.
            List<EtfDayLine> etfDayLines = ReadEtfDayLine(etfMasters);

            // 遍历结果.
            foreach(EtfDayLine etfDayLine in etfDayLines)
            {
                // 插入 ETF日线数据.
                etfDayService.InsertEtfDayLine(etfDayLine);

                // 计算  ETF周线数据
                etfWeekService.CalculateEtfWeekLine(etfDayLine.EtfCode, etfDayLine.TradingDate);
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









        /// <summary>
        /// 读取一行数据.
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        private static EtfDayLine GetOneEtfDayLine(string line)
        {

            // 数据格式：
            // var hq_str_sh510050="50ETF,3.275,3.275,3.273,3.289,3.261,3.273,3.274,647059066,2118664423.000,93000,3.273,211000,3.272,375000,3.271,1568400,3.270,648700,3.269,869700,3.274,4506000,3.275,1383100,3.276,624700,3.277,688000,3.278,2021-12-24,15:00:00,00,";


            line = line.Trim();


            string etfCode = line.Substring(11, 8);

            int eqIndex = line.IndexOf('=');           
            line = line.Substring(eqIndex + 2);
            line = line.Substring(0, line.Length - 2);


            string[] itemArray = line.Split(',');

            // 处理日期.
            string dateStr = "";
            for(int i = itemArray.Length - 1; i >0; i--)
            {
                dateStr = itemArray[i];
                if(dateStr.IndexOf("-") == 4)
                {
                    break;
                }
            }            
            DateTime processDate = Convert.ToDateTime(dateStr);


            EtfDayLine result = new EtfDayLine()
            {
                // 代码.
                EtfCode = etfCode.ToUpper(),
                // 日期.
                TradingDate = processDate,
                // 开.
                OpenPrice = Convert.ToDecimal(itemArray[1]),
                // 高.
                HighestPrice = Convert.ToDecimal(itemArray[4]),
                // 低.
                LowestPrice = Convert.ToDecimal(itemArray[5]),
                // 平.
                ClosePrice = Convert.ToDecimal(itemArray[3]),
                // 成交.
                Volume = Convert.ToInt64(itemArray[8])
            };

            return result;
        }


        private static List<EtfDayLine> ReadEtfDayLine(List<EtfMaster> etfMasters)
        {

            List<EtfDayLine> resultList = new List<EtfDayLine>();


            string codeString = string.Join(',', etfMasters.Select(p => p.EtfCode.ToLower()).ToArray());

            string url = $"http://hq.sinajs.cn/list={codeString}";


            //访问该链接
            WebRequest request = WebRequest.Create(url);
            //获得返回值
            WebResponse response = request.GetResponse();
            // 从 Internet 资源返回数据流。
            Stream s = response.GetResponseStream();

            using (StreamReader sr = new StreamReader(s, Encoding.UTF8))
            {
                int lineIndex = 0;
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    lineIndex++;

                    if (String.IsNullOrWhiteSpace(line))
                    {
                        // 只有空白， 结束.
                        break;
                    }

                    // 读取一行.
                    EtfDayLine oneResult = GetOneEtfDayLine(line);

                    // 加入列表.
                    resultList.Add(oneResult);
                }
            }



            return resultList;
        }







    }
}
