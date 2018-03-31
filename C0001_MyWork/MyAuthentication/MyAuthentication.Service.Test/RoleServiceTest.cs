using Microsoft.VisualStudio.TestTools.UnitTesting;


using MyAuthentication.Model;
using MyAuthentication.ServiceImpl;


namespace MyAuthentication.Service.Test
{
    [TestClass]
    public class RoleServiceTest
    {

        /// <summary>
        /// 角色服务.
        /// </summary>
        private IRoleService roleService = new DefaultRoleServiceImpl();


        [TestMethod]
        public void TestGetAllRoles()
        {
            // 获取全部角色.
            var allRole = roleService.GetAllRoles();
            // 结果非空.
            Assert.IsNotNull(allRole);
            // 至少1个角色.
            Assert.IsTrue(allRole.Count > 0);
        }



        [TestMethod]
        public void TestBasicRoleFunc()
        {

            // 测试查询单个角色.
            var oneRole = roleService.GetRole("TEST");
            // 结果为空.
            Assert.IsNull(oneRole);


            MyRole testRole = new MyRole()
            {
                RoleCode = "TEST",
                RoleName = "测试角色",
                SystemCode = "MYAUTH",
                IsActive = true,
            };
            testRole.BeforeInsertOperation("测试用户");

            // 测试插入.
            var testResult = this.roleService.NewRole(testRole);
            // 结果非空.
            Assert.IsNotNull(testResult);
            // 处理成功.
            Assert.IsTrue(testResult.IsSuccess);

            // 测试重复插入.
            testResult = this.roleService.NewRole(testRole);
            // 结果非空.
            Assert.IsNotNull(testResult);
            // 处理失败.
            Assert.IsFalse(testResult.IsSuccess);



            // 测试查询单个角色.
            oneRole = roleService.GetRole("TEST");
            // 结果非空.
            Assert.IsNotNull(oneRole);
            // 数据核对.
            Assert.AreEqual("测试角色", oneRole.RoleName);

            // 测试修改.
            oneRole.RoleName = "测试角色_100";


            // 测试保存.
            testResult = this.roleService.UpdateRole(oneRole);
            // 结果非空.
            Assert.IsNotNull(testResult);
            // 处理成功.
            Assert.IsTrue(testResult.IsSuccess);


            // 再次查询.
            var oneRole2 = roleService.GetRole("TEST");
            // 结果非空.
            Assert.IsNotNull(oneRole2);
            // 数据核对.
            Assert.AreEqual("测试角色_100", oneRole2.RoleName);


            // 测试删除.
            testResult = this.roleService.RemoveRole("TEST");
            // 结果非空.
            Assert.IsNotNull(testResult);
            // 处理成功.
            Assert.IsTrue(testResult.IsSuccess);
            
            // 测试重复删除.
            testResult = this.roleService.RemoveRole("TEST");
            // 结果非空.
            Assert.IsNotNull(testResult);
            // 处理失败.
            Assert.IsFalse(testResult.IsSuccess);

        }
    }
}
