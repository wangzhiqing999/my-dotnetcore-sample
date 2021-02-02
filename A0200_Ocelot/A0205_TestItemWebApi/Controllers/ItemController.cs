using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A0205_TestItemWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {


        /// <summary>
        /// 用于测试的 商品信息类.
        /// </summary>
        public class TestItem
        {

            /// <summary>
            /// 商品代码.
            /// </summary>
            public string ItemCode { set; get; }



            /// <summary>
            /// 商品名称.
            /// </summary>
            public string ItemName { set; get; }

        }


        private static List<TestItem> testDataList = new List<TestItem>()
        {
            new TestItem() { ItemCode = "0003", ItemName = "SQL查询的艺术"},
            new TestItem() { ItemCode = "0004", ItemName = "NoSQL数据库原理与应用"},
            new TestItem() { ItemCode = "0005", ItemName = "Redis实战"},
            new TestItem() { ItemCode = "0006", ItemName = "MySQL技术内幕：InnoDB存储引擎(第2版)"},
        };






        // GET api/Item
        /// <summary>
        /// 获取所有商品信息.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<TestItem> Get()
        {
            return testDataList;
        }


        // GET api/Item/0003
        /// <summary>
        /// 获取指定商品信息
        /// </summary>
        /// <param name="id">商品代码</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public TestItem Get(string id)
        {
            return testDataList.FirstOrDefault(p => p.ItemCode == id);
        }


        // POST api/Item
        /// <summary>
        /// 新增商品信息
        /// </summary>
        /// <param name="value"></param>
        [HttpPost]
        public void Post([FromBody] TestItem value)
        {
            testDataList.Add(value);
        }


        // PUT api/Item/0003
        /// <summary>
        /// 更新商品信息.
        /// </summary>
        /// <param name="id">商品代码</param>
        /// <param name="value"></param>
        [HttpPut("{id}")]
        public void Put(string id, [FromBody] string value)
        {
            var item = testDataList.FirstOrDefault(p => p.ItemCode == id);
            if (item != null)
            {
                item.ItemName = value;
            }
        }


        // DELETE api/Item/0003
        /// <summary>
        /// 删除商品信息
        /// </summary>
        /// <param name="id">商品代码</param>
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            var item = testDataList.FirstOrDefault(p => p.ItemCode == id);
            if (item != null)
            {
                testDataList.Remove(item);
            }
        }



    }
}
