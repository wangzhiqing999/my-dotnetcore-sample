using Microsoft.VisualStudio.TestTools.UnitTesting;

using MyAuthentication.DataAccess;
using MyAuthentication.Model;
using MyAuthentication.ServiceImpl;
using MyAuthentication.ServiceModel;

namespace MyAuthentication.Service.Test
{
    [TestClass]
    public class UserServiceTest
    {

        /// <summary>
        /// 用户服务.
        /// </summary>
        private IUserService userService = null; // new DefaultUserServiceImpl();



        [TestInitialize]
        public void TestInit()
        {
            MyAuthenticationContext context = new MyAuthenticationContext();
            userService = new DefaultUserServiceImpl(context);
        }





        [TestMethod]
        public void TestBasicUserFunc()
        {
            // 测试用户.
            MyUser testUser = new MyUser()
            {
                // 组织代码：（注意：这里要执行初始化sql脚本，才有数据.）
                OrganizationID = 1,
                LoginUserCode = "TEST",
                UserName = "测试用户",
                UserPassword = "123456",
                IsActive = true,
            };
            testUser.BeforeInsertOperation("tester");

            // 测试插入.
            var newResult = this.userService.NewUser(testUser);
            // 结果非空.
            Assert.IsNotNull(newResult);
            // 处理成功.
            Assert.IsTrue(newResult.IsSuccess);


            // 测试登录1.
            var loginResult = this.userService.DoLogin("不存在", "TEST", "123456");
            // 结果非空.
            Assert.IsNotNull(loginResult);
            // 处理失败.
            Assert.IsFalse(loginResult.IsSuccess);
            // 错误码 = 组织代码不存在.
            Assert.AreEqual(AuthenticationServiceResult.ResultCodeIsOrganizationCodeNotFound, loginResult.ResultCode);

            // 测试登录2.
            loginResult = this.userService.DoLogin("MANAGER", "TEST123", "123456");
            // 结果非空.
            Assert.IsNotNull(loginResult);
            // 处理失败.
            Assert.IsFalse(loginResult.IsSuccess);
            // 错误码 = 用户名不存在.
            Assert.AreEqual(AuthenticationServiceResult.ResultCodeIsLoginUserCodeNotFound, loginResult.ResultCode);

            // 测试登录3.
            loginResult = this.userService.DoLogin("MANAGER", "TEST", "123456789");
            // 结果非空.
            Assert.IsNotNull(loginResult);
            // 处理失败.
            Assert.IsFalse(loginResult.IsSuccess);
            // 错误码 = 密码不正确.
            Assert.AreEqual(AuthenticationServiceResult.ResultCodeIsPasswordNotMatch, loginResult.ResultCode);


            // 测试登录.
            loginResult = this.userService.DoLogin("MANAGER", "TEST", "123456");
            // 结果非空.
            Assert.IsNotNull(loginResult);
            // 处理成功.
            Assert.IsTrue(loginResult.IsSuccess);

            // 核对登录信息.
            BasicUserInfo basicUserInfo = loginResult.ResultData as BasicUserInfo;
            // 非空.
            Assert.IsNotNull(basicUserInfo);
            // 用户名匹配.
            Assert.AreEqual("测试用户", basicUserInfo.UserName);
            // 用户ID.
            Assert.AreEqual(testUser.UserID, basicUserInfo.UserID);
            // 组织机构ID.
            Assert.AreEqual(1, basicUserInfo.OrganizationID);



            // 测试删除.
            var removeResult = this.userService.RemoveUser(testUser.UserID);
            // 结果非空.
            Assert.IsNotNull(removeResult);
            // 处理成功.
            Assert.IsTrue(removeResult.IsSuccess);



            // 再次删除.
            removeResult = this.userService.RemoveUser(testUser.UserID);
            // 结果非空.
            Assert.IsNotNull(removeResult);
            // 操作失败.
            Assert.IsFalse(removeResult.IsSuccess);
            // 错误码. = ID 不存在.
            Assert.AreEqual(AuthenticationServiceResult.ResultCodeIsUserIDNotFound, removeResult.ResultCode);
        }


    }
}
