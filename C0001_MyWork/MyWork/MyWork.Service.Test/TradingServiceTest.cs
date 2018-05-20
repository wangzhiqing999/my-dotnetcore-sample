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
        /// ���׷���.
        /// </summary>
        private ITradingService tradingService;


        [TestInitialize]
        public void TestInit()
        {
            MyWorkContext context = new MyWorkContext();
            tradingService = new DefaultTradingServiceImpl(context);
        }



        /// <summary>
        /// ��ȡ���ԵĽ�������.
        /// </summary>
        /// <returns></returns>
        private Trading GetTestTradingData()
        {
            Trading result = new Trading()
            {
                // ��Ʊ����.
                StockCode = "600000",

                // �˻�.
                AccountID = 1,

                // ����.
                Quantity = 100,

                // ����.
                UnitPrice = 10.9m,

                // ������.
                Fees = 5,

                // ����ʱ��.
                TradingDateTime = new System.DateTime(2018, 5, 18)
            };

            return result;
        }





        /// <summary>
        /// ���Բ����ڵ� ��Ʊ����.
        /// </summary>
        [TestMethod]
        public void TestNotExistsStockCode()
        {
            Trading trading = this.GetTestTradingData();

            // �޸Ĺ�Ʊ����Ϊ�����ڵĴ���.
            trading.StockCode = "999999";

            // ���Ե��ý��׽ӿ�.
            var result = this.tradingService.NewTrading(trading);

            // ����ǿ�.
            Assert.IsNotNull(result);

            // ����ǲ��ɹ���.
            Assert.AreNotEqual(CommonServiceResult.ResultCodeIsSuccess, result.ResultCode);
        }


        /// <summary>
        /// ���Բ����ڵ� �˻�ID.
        /// </summary>
        [TestMethod]
        public void TestNotExistsAccount()
        {
            Trading trading = this.GetTestTradingData();

            // �޸� �˻�IDΪ�����ڵ�ID.
            trading.AccountID = -12345;

            // ���Ե��ý��׽ӿ�.
            var result = this.tradingService.NewTrading(trading);

            // ����ǿ�.
            Assert.IsNotNull(result);

            // ����ǲ��ɹ���.
            Assert.AreNotEqual(CommonServiceResult.ResultCodeIsSuccess, result.ResultCode);
        }


        /// <summary>
        /// �����˻�����
        /// </summary>
        [TestMethod]
        public void TestBalanceError()
        {
            Trading trading = this.GetTestTradingData();

            // �޸� ������Ϊ�޴��������.
            trading.Fees = 9999999999m;

            // ���Ե��ý��׽ӿ�.
            var result = this.tradingService.NewTrading(trading);

            // ����ǿ�.
            Assert.IsNotNull(result);

            // ����ǲ��ɹ���.
            Assert.AreNotEqual(CommonServiceResult.ResultCodeIsSuccess, result.ResultCode);
        }


        /// <summary>
        /// ���Գɹ���ɽ���.
        /// </summary>
        [TestMethod]
        public void TestSuccess()
        {
            Trading trading = this.GetTestTradingData();

            // ���Ե��ý��׽ӿ�. ���һ���������.
            var result = this.tradingService.NewTrading(trading);

            // ����ǿ�.
            Assert.IsNotNull(result);

            // ����ǳɹ���.
            Assert.AreEqual(CommonServiceResult.ResultCodeIsSuccess, result.ResultCode);



            trading = this.GetTestTradingData();
            trading.Quantity = -100;

            // ���Ե��ý��׽ӿ�. ���һ����������.
            result = this.tradingService.NewTrading(trading);

            // ����ǿ�.
            Assert.IsNotNull(result);

            // ����ǳɹ���.
            Assert.AreEqual(CommonServiceResult.ResultCodeIsSuccess, result.ResultCode);
        }

    }
}
