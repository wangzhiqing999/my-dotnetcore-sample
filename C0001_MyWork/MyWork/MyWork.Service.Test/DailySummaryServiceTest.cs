using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using System.Collections.Generic;
using System.Text;

using MyWork.DataAccess;
using MyWork.Model;
using MyWork.ServiceImpl;
using MyFramework.ServiceModel;


namespace MyWork.Service.Test
{
    [TestClass]
    public class DailySummaryServiceTest
    {
        /// <summary>
        /// 交易服务.
        /// </summary>
        private ITradingService tradingService;

        /// <summary>
        /// 日结服务
        /// </summary>
        private IDailySummaryService dailySummaryService;


        /// <summary>
        /// 价格读取.
        /// </summary>
        private IPriceReader priceReader;


        [TestInitialize]
        public void TestInit()
        {
            MyWorkContext context = new MyWorkContext();
            priceReader = new SinaPriceReader();
            dailySummaryService = new DefaultDailySummaryServiceImpl(context, priceReader);

            tradingService = new DefaultTradingServiceImpl(context);
        }




        /// <summary>
        /// 获取测试的交易数据.
        /// </summary>
        /// <returns></returns>
        private Trading GetTestTradingData()
        {
            Trading result = new Trading()
            {
                // 股票代码.
                StockCode = "600000",

                // 账户.
                AccountID = 1,

                // 数量.
                Quantity = 100,

                // 单价.
                UnitPrice = 10.9m,

                // 手续费.
                Fees = 5,

                // 交易时间.
                TradingDateTime = DateTime.Today
            };

            return result;
        }





        /// <summary>
        /// 测试交易 与 日结报表.
        /// </summary>
        [TestMethod]
        public void TestTradingAndSummary()
        {
            Trading trading = this.GetTestTradingData();

            // 测试调用交易接口. 完成一次买入操作.
            var result = this.tradingService.NewTrading(trading);

            // 结果非空.
            Assert.IsNotNull(result);

            // 结果是成功的.
            Assert.AreEqual(CommonServiceResult.ResultCodeIsSuccess, result.ResultCode);



            // 测试日结操作.
            result = this.dailySummaryService.BuildDailySummary(DateTime.Today);

            // 结果非空.
            Assert.IsNotNull(result);
            // 结果是成功的.
            Assert.AreEqual(CommonServiceResult.ResultCodeIsSuccess, result.ResultCode);



            // 测试报表.
            result = this.dailySummaryService.BuildDailyReport(DateTime.Today);

            // 结果非空.
            Assert.IsNotNull(result);
            // 结果是成功的.
            Assert.AreEqual(CommonServiceResult.ResultCodeIsSuccess, result.ResultCode);
        }





    }
}
