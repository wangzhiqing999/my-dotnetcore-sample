using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace A0200_TestUserWebApi.Controllers
{

    /// <summary>
    /// 用户服务.
    /// </summary>
    [Produces("application/json")]
    [Route("api/Users")]
    public class UsersController : Controller
    {

        /// <summary>
        /// 测试的用户信息类.
        /// </summary>
        public class TestUserInfo
        {
            /// <summary>
            /// 用户代码.
            /// </summary>
            public string Code { set; get; }

            /// <summary>
            /// 用户姓名.
            /// </summary>
            public string Name { set; get; }

        }


        private static List<TestUserInfo> testDataList = new List<TestUserInfo>()
        {
            new TestUserInfo() { Code = "0003", Name = "张三"},
            new TestUserInfo() { Code = "0004", Name = "李四"},
            new TestUserInfo() { Code = "0005", Name = "王五"},
            new TestUserInfo() { Code = "0006", Name = "赵六"},
        };



        // GET api/Users
        /// <summary>
        /// 获取所有用户信息.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<TestUserInfo> Get()
        {
            return testDataList;
        }


        // GET api/Users/0003
        /// <summary>
        /// 获取指定用户信息
        /// </summary>
        /// <param name="id">用户代码</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public TestUserInfo Get(string id)
        {
            return testDataList.FirstOrDefault(p=>p.Code == id);
        }


        // POST api/Users
        /// <summary>
        /// 新增用户信息
        /// </summary>
        /// <param name="value"></param>
        [HttpPost]
        public void Post([FromBody]TestUserInfo value)
        {
            testDataList.Add(value);
        }


        // PUT api/Users/0003
        /// <summary>
        /// 更新用户信息.
        /// </summary>
        /// <param name="id">用户代码</param>
        /// <param name="value"></param>
        [HttpPut("{id}")]
        public void Put(string id, [FromBody]string value)
        {
            var item = testDataList.FirstOrDefault(p => p.Code == id);
            if (item != null)
            {
                item.Name = value;
            }
        }


        // DELETE api/Users/0003
        /// <summary>
        /// 删除用户信息
        /// </summary>
        /// <param name="id">用户代码</param>
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            var item = testDataList.FirstOrDefault(p => p.Code == id);
            if(item != null)
            {
                testDataList.Remove(item);
            }
        }

    }


    
}