using Microsoft.VisualStudio.TestTools.UnitTesting;

using MyAuthentication.DataAccess;
using MyAuthentication.Model;
using MyAuthentication.ServiceImpl;
using MyAuthentication.ServiceModel;

namespace MyAuthentication.Service.Test
{
    [TestClass]
    public class OrganizationServiceTest
    {

        /// <summary>
        /// 组织机构服务.
        /// </summary>
        private IOrganizationService organizationService;


        [TestInitialize]
        public void TestInit()
        {
            MyAuthenticationContext context = new MyAuthenticationContext();
            organizationService = new DefaultOrganizationServiceImpl(context);
        }





        [TestMethod]
        public void TestBasicOrganizationFunc()
        {

            // 测试查询.
            var queryResult = this.organizationService.Query();

            // 结果非空.
            Assert.IsNotNull(queryResult);
            Assert.IsNotNull(queryResult.QueryPageInfo);
            Assert.IsNotNull(queryResult.QueryResultData);


            // 测试插入.
            MyOrganization org = new MyOrganization()
            {
                OrganizationName = "测试组织机构.",
                LoginOrganizationCode = "TEST",
                IsActive = true,
            };
            org.BeforeInsertOperation("tester");

            var newResult = this.organizationService.NewOrganization(org);
            // 结果非空.
            Assert.IsNotNull(newResult);
            // 操作成功.
            Assert.IsTrue(newResult.IsSuccess);



            // 尝试获取单行数据.
            var oneOrg = this.organizationService.GetOrganization(-1);
            // 数据不存在.
            // 结果非空.
            Assert.IsNotNull(oneOrg);
            // 结果为不成功.
            Assert.IsFalse(oneOrg.IsSuccess);


            oneOrg = this.organizationService.GetOrganization(org.OrganizationID);
            // 数据存在.
            Assert.IsNotNull(oneOrg);
            // 结果为成功.
            Assert.IsTrue(oneOrg.IsSuccess);



            // 测试插入  重复 ID 的数据.
            MyOrganization org2 = new MyOrganization()
            {
                OrganizationID = org.OrganizationID,
                OrganizationName = "测试组织机构.",
                LoginOrganizationCode = "TEST",
                IsActive = true,
            };
            org2.BeforeInsertOperation("tester");
            newResult = this.organizationService.NewOrganization(org2);
            // 结果非空.
            Assert.IsNotNull(newResult);
            // 操作失败.
            Assert.IsFalse(newResult.IsSuccess);
            // 错误码 = ID 已存在.
            Assert.AreEqual(AuthenticationServiceResult.ResultCodeIsOrganizationIDHadExists, newResult.ResultCode);


            org2.OrganizationID = 0;
            newResult = this.organizationService.NewOrganization(org2);
            // 结果非空.
            Assert.IsNotNull(newResult);
            // 操作失败.
            Assert.IsFalse(newResult.IsSuccess);
            // 错误码. = 代码已存在.
            Assert.AreEqual(AuthenticationServiceResult.ResultCodeIsOrganizationCodeHadExists, newResult.ResultCode);




            // 修改属性.
            org.OrganizationName = "测试组织机构2";
            // 测试更新.
            var updateResult = this.organizationService.UpdateOrganization(org);
            // 结果非空.
            Assert.IsNotNull(updateResult);
            // 操作成功.
            Assert.IsTrue(updateResult.IsSuccess);


            // 修改不应修改的属性.
            org.LoginOrganizationCode = "TEST2";
            // 测试更新.
            updateResult = this.organizationService.UpdateOrganization(org);
            // 结果非空.
            Assert.IsNotNull(updateResult);
            // 操作失败.
            Assert.IsFalse(updateResult.IsSuccess);
            // 错误码. = 代码不允许修改.
            Assert.AreEqual(AuthenticationServiceResult.ResultCodeIsOrganizationCodeModify, updateResult.ResultCode);



            // 测试删除.
            var removeResult = this.organizationService.RemoveOrganization(org.OrganizationID);
            // 结果非空.
            Assert.IsNotNull(removeResult);
            // 操作成功.
            Assert.IsTrue(removeResult.IsSuccess);


            // 再次删除.
            removeResult = this.organizationService.RemoveOrganization(org.OrganizationID);
            // 结果非空.
            Assert.IsNotNull(removeResult);
            // 操作失败.
            Assert.IsFalse(removeResult.IsSuccess);
            // 错误码. = ID 不存在.
            Assert.AreEqual(AuthenticationServiceResult.ResultCodeIsOrganizationIDNotFound, removeResult.ResultCode);

        }



    }
}
