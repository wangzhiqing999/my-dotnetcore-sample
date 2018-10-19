using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace A0200_TestOrderWebApi.Controllers
{

    /// <summary>
    /// 订单服务.
    /// </summary>
    [Produces("application/json")]
    [Route("api/Orders")]
    public class OrdersController : Controller
    {

        /// <summary>
        /// 测试的订单数据类.
        /// </summary>
        public class TestOrderData
        {
            /// <summary>
            /// 订单流水.
            /// </summary>
            public string OrderID { set; get; }

            /// <summary>
            /// 订单日期.
            /// </summary>
            public DateTime OrderDateTime { set; get; }


            /// <summary>
            /// 订单价格.
            /// </summary>
            public decimal OrderPrice { set; get; }


            /// <summary>
            /// 订单用户代码.
            /// </summary>
            public string OrderUserCode { set; get; }
        }


        private static List<TestOrderData> testDataList = new List<TestOrderData>()
        {
            new TestOrderData() { OrderID = "201810190001", OrderDateTime = new DateTime(2018,10,19, 12,15,15), OrderPrice = 128, OrderUserCode = "0003"},
            new TestOrderData() { OrderID = "201810190002", OrderDateTime = new DateTime(2018,10,19, 15,25,25), OrderPrice = 256, OrderUserCode = "0004"},
            new TestOrderData() { OrderID = "201810190003", OrderDateTime = new DateTime(2018,10,19, 17,35,35), OrderPrice = 512, OrderUserCode = "0005"},
            new TestOrderData() { OrderID = "201810190004", OrderDateTime = new DateTime(2018,10,19, 19,45,45), OrderPrice = 1024, OrderUserCode = "0006"},
        };


        // GET api/Orders
        /// <summary>
        /// 获取所有订单信息.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<TestOrderData> Get()
        {
            return testDataList;
        }


        // GET api/Orders/201810190001
        /// <summary>
        /// 获取指定订单信息.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public TestOrderData Get(string id)
        {
            return testDataList.FirstOrDefault(p => p.OrderID == id);
        }

        // POST api/Orders
        /// <summary>
        /// 新增订单信息
        /// </summary>
        /// <param name="value"></param>
        [HttpPost]
        public void Post([FromBody]TestOrderData value)
        {
            testDataList.Add(value);
        }

    }
}