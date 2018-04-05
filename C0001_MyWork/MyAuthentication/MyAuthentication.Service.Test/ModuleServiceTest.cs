using Microsoft.VisualStudio.TestTools.UnitTesting;

using MyAuthentication.Model;
using MyAuthentication.ServiceImpl;


namespace MyAuthentication.Service.Test
{
    [TestClass]
    public class ModuleServiceTest
    {


        /// <summary>
        /// 模块服务
        /// </summary>
        private IModuleService moduleService = new DefaultModuleServiceImpl();




        [TestMethod]
        public void TestGetModuleList()
        {
            // 查询模块类型.
            var result = moduleService.GetModuleList("MYAUTH", "API");
            // 结果非空.
            Assert.IsNotNull(result);
            // 有结果.
            Assert.IsTrue(result.QueryResultData.Count > 0);
        }

    }
}
