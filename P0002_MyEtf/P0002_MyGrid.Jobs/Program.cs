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


using P0002_MyGrid.DataAccess;
using P0002_MyGrid.Service;
using P0002_MyGrid.ServiceImpl;
using P0002_MyGrid.ServiceModel;



namespace P0002_MyGrid.Jobs
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




            // TestInit(serviceProvider);
            // TestTransaction_SZ159939(serviceProvider);
            // TestGetTodo_SZ159939(serviceProvider);



            IEtfDayService etfDayService = serviceProvider.GetService<IEtfDayService>();
            IGridService gridService = serviceProvider.GetService<IGridService>();


            List<string> itemCodeList = gridService.GetItemCodes();
            foreach(string itemCode in itemCodeList )
            {
                EtfDayLine lastEtfDayLine = etfDayService.GetLastEtfDayLines(itemCode);

                GetTodoRequest request = new GetTodoRequest()
                {
                    ItemCode = itemCode,
                    CurrentPrice = lastEtfDayLine.ClosePrice
                };

                Console.WriteLine($"## {itemCode}:{lastEtfDayLine.ClosePrice}");
                var todoList = gridService.GetTodoList(request);
                foreach (var todo in todoList)
                {
                    Console.WriteLine(todo);
                }
            }



            Console.WriteLine("--- Finish ---");
            Console.ReadLine();
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

            services.AddDbContext<MyGridContext>(opt => opt.UseNpgsql(configuration.GetSection("MyGridContext").Value,
                x => x.MigrationsHistoryTable(HistoryRepository.DefaultTableName, "my_grid")));


            // 服务注入.
            services.AddTransient<IEtfMasterService, DefaultEtfMasterService>();
            services.AddTransient<IEtfDayService, DefaultEtfDayService>();
            services.AddTransient<IEtfWeekService, DefaultEtfWeekService>();






            services.AddTransient<IGridService, DefaultGridService>();

        }










        #region 测试初始化.



        /// <summary>
        /// 请求列表.
        /// </summary>
        private static List<InitGridRequest> requests = new List<InitGridRequest>()
        {
            new InitGridRequest()
            {
                // SZ159939	信息技术ETF
                ItemCode = "SZ159939",
                MinPrice = 0.890m,
                MaxPrice = 1.140m,
            },

            new InitGridRequest()
            {
                // SH515260	电子ETF
                ItemCode = "SH515260",
                MinPrice = 0.715m,
                MaxPrice = 0.965m,
            },

            new InitGridRequest()
            {
                // SZ159938	医药卫生ETF
                ItemCode = "SZ159938",
                MinPrice = 0.623m,
                MaxPrice = 0.873m,
            },

            new InitGridRequest()
            {
                // SH512980	传媒ETF
                ItemCode = "SH512980",
                MinPrice = 0.450m,
                MaxPrice = 0.7m,
            },

            new InitGridRequest()
            {
                // SH512070	证券保险ETF
                ItemCode = "SH512070",
                MinPrice = 0.543m,
                MaxPrice = 0.748m,
            }
        };




        private static void TestInit(IServiceProvider serviceProvider)
        {
            IGridService gridService = serviceProvider.GetService<IGridService>();


            foreach(var request in requests)
            {
                var grids = gridService.GetInitGrids(request);
                foreach (var grid in grids)
                {
                    Console.WriteLine(grid);
                }

                gridService.SaveGrids(grids);
            }

            
        }



        #endregion





        #region 测试网格交易.



        



        /// <summary>
        /// 测试交易 SZ159939	信息技术ETF
        /// </summary>
        /// <param name="serviceProvider"></param>
        private static void TestTransaction_SZ159939(IServiceProvider serviceProvider)
        {
            IGridService gridService = serviceProvider.GetService<IGridService>();


            var result = gridService.NewTransaction(new NewTransactionRequest() { 
                ItemCode = "SZ159939",
                GridNo = 10,
                TransactionDate = new DateTime(2022, 4, 8),
                TransactionQuantity = 2000,
                TransactionPrice = 1.115m,
            });
            Console.WriteLine(result);


            result = gridService.NewTransaction(new NewTransactionRequest()
            {
                ItemCode = "SZ159939",
                GridNo = 9,
                TransactionDate = new DateTime(2022, 4, 11),
                TransactionQuantity = 2000,
                TransactionPrice = 1.077m,
            });
            Console.WriteLine(result);


            result = gridService.NewTransaction(new NewTransactionRequest()
            {
                ItemCode = "SZ159939",
                GridNo = 8,
                TransactionDate = new DateTime(2022, 4, 15),
                TransactionQuantity = 2000,
                TransactionPrice = 1.054m,
            });
            Console.WriteLine(result);



            result = gridService.NewTransaction(new NewTransactionRequest()
            {
                ItemCode = "SZ159939",
                GridNo = 7,
                TransactionDate = new DateTime(2022, 4, 15),
                TransactionQuantity = 2000,
                TransactionPrice = 1.025m,
            });
            Console.WriteLine(result);



            result = gridService.NewTransaction(new NewTransactionRequest()
            {
                ItemCode = "SZ159939",
                GridNo = 6,
                TransactionDate = new DateTime(2022, 4, 25),
                TransactionQuantity = 2000,
                TransactionPrice = 0.973m,
            });
            Console.WriteLine(result);


            result = gridService.NewTransaction(new NewTransactionRequest()
            {
                ItemCode = "SZ159939",
                GridNo = 6,
                TransactionDate = new DateTime(2022, 6, 2),
                TransactionQuantity = -2000,
                TransactionPrice = 1.077m,
            });
            Console.WriteLine(result);



            result = gridService.NewTransaction(new NewTransactionRequest()
            {
                ItemCode = "SZ159939",
                GridNo = 7,
                TransactionDate = new DateTime(2022, 7, 28),
                TransactionQuantity = -2000,
                TransactionPrice = 1.121m,
            });
            Console.WriteLine(result);



            result = gridService.NewTransaction(new NewTransactionRequest()
            {
                ItemCode = "SZ159939",
                GridNo = 7,
                TransactionDate = new DateTime(2022, 9, 19),
                TransactionQuantity = 2000,
                TransactionPrice = 1.034m,
            });
            Console.WriteLine(result);


            result = gridService.NewTransaction(new NewTransactionRequest()
            {
                ItemCode = "SZ159939",
                GridNo = 7,
                TransactionDate = new DateTime(2022, 11, 11),
                TransactionQuantity = -2000,
                TransactionPrice = 1.065m,
            });
            Console.WriteLine(result);



            result = gridService.NewTransaction(new NewTransactionRequest()
            {
                ItemCode = "SZ159939",
                GridNo = 7,
                TransactionDate = new DateTime(2022, 11, 28),
                TransactionQuantity = 2000,
                TransactionPrice = 1.027m,
            });
            Console.WriteLine(result);



            result = gridService.NewTransaction(new NewTransactionRequest()
            {
                ItemCode = "SZ159939",
                GridNo = 7,
                TransactionDate = new DateTime(2023, 1, 17),
                TransactionQuantity = -2000,
                TransactionPrice = 1.075m,
            });
            Console.WriteLine(result);

        }


        #endregion





        #region 测试获取 TODO.



        private static void TestGetTodo_SZ159939(IServiceProvider serviceProvider)
        {
            IEtfDayService etfDayService = serviceProvider.GetService<IEtfDayService>();

            IGridService gridService = serviceProvider.GetService<IGridService>();


            EtfDayLine lastEtfDayLine = etfDayService.GetLastEtfDayLines("SZ159939");

            GetTodoRequest request = new GetTodoRequest()
            {
                ItemCode = "SZ159939",
                CurrentPrice = lastEtfDayLine.ClosePrice
            };

            var todoList = gridService.GetTodoList(request);
            foreach(var todo in todoList )
            {
                Console.WriteLine(todo);
            }
        }

        #endregion




    }
}