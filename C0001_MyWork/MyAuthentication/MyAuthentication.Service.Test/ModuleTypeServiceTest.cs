using Microsoft.VisualStudio.TestTools.UnitTesting;

using MyAuthentication.Model;
using MyAuthentication.ServiceImpl;


namespace MyAuthentication.Service.Test
{
    [TestClass]
    public class ModuleTypeServiceTest
    {

        /// <summary>
        /// 模块类型服务
        /// </summary>
        private IModuleTypeService moduleTypeService = new DefaultModuleTypeServiceImpl();


        [TestMethod]
        public void TestGetModuleTypeList()
        {
            // 查询模块类型.
            var result = moduleTypeService.GetModuleTypeList();

            // 结果非空.
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.QueryResultData);

            // 2个结果.
            Assert.AreEqual(2, result.QueryResultData.Count);

        }

    }
}
