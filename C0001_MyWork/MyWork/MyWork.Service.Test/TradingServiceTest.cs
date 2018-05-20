using Microsoft.VisualStudio.TestTools.UnitTesting;


using MyWork.DataAccess;
using MyWork.Model;
using MyWork.ServiceImpl;
using MyFramework.ServiceModel;


namespace MyWork.Service.Test
{
    [TestClass]
    public class TradingServiceTest
    {
        /// <summary>
        /// 交易服务.
        /// </summary>
        private ITradingService tradingService;


        [TestInitialize]
        public void TestInit()
        {
            MyWorkContext context = new MyWorkContext();
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
                TradingDateTime = new System.DateTime(2018, 5, 18)
            };

            return result;
        }





        /// <summary>
        /// 测试不存在的 股票代码.
        /// </summary>
        [TestMethod]
        public void TestNotExistsStockCode()
        {
            Trading trading = this.GetTestTradingData();

            // 修改股票代码为不存在的代码.
            trading.StockCode = "999999";

            // 测试调用交易接口.
            var result = this.tradingService.NewTrading(trading);

            // 结果非空.
            Assert.IsNotNull(result);

            // 结果是不成功的.
            Assert.AreNotEqual(CommonServiceResult.ResultCodeIsSuccess, result.ResultCode);
        }


        /// <summary>
        /// 测试不存在的 账户ID.
        /// </summary>
        [TestMethod]
        public void TestNotExistsAccount()
        {
            Trading trading = this.GetTestTradingData();

            // 修改 账户ID为不存在的ID.
            trading.AccountID = -12345;

            // 测试调用交易接口.
            var result = this.tradingService.NewTrading(trading);

            // 结果非空.
            Assert.IsNotNull(result);

            // 结果是不成功的.
            Assert.AreNotEqual(CommonServiceResult.ResultCodeIsSuccess, result.ResultCode);
        }


        /// <summary>
        /// 测试账户余额不足
        /// </summary>
        [TestMethod]
        public void TestBalanceError()
        {
            Trading trading = this.GetTestTradingData();

            // 修改 手续费为巨大的手续费.
            trading.Fees = 9999999999m;

            // 测试调用交易接口.
            var result = this.tradingService.NewTrading(trading);

            // 结果非空.
            Assert.IsNotNull(result);

            // 结果是不成功的.
            Assert.AreNotEqual(CommonServiceResult.ResultCodeIsSuccess, result.ResultCode);
        }


        /// <summary>
        /// 测试成功完成交易.
        /// </summary>
        [TestMethod]
        public void TestSuccess()
        {
            Trading trading = this.GetTestTradingData();

            // 测试调用交易接口. 完成一次买入操作.
            var result = this.tradingService.NewTrading(trading);

            // 结果非空.
            Assert.IsNotNull(result);

            // 结果是成功的.
            Assert.AreEqual(CommonServiceResult.ResultCodeIsSuccess, result.ResultCode);



            trading = this.GetTestTradingData();
            trading.Quantity = -100;

            // 测试调用交易接口. 完成一次卖出操作.
            result = this.tradingService.NewTrading(trading);

            // 结果非空.
            Assert.IsNotNull(result);

            // 结果是成功的.
            Assert.AreEqual(CommonServiceResult.ResultCodeIsSuccess, result.ResultCode);
        }

    }
}
