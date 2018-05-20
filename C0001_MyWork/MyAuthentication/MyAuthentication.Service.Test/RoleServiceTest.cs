using Microsoft.VisualStudio.TestTools.UnitTesting;

using MyAuthentication.DataAccess;
using MyAuthentication.Model;
using MyAuthentication.ServiceImpl;


namespace MyAuthentication.Service.Test
{
    [TestClass]
    public class RoleServiceTest
    {

        /// <summary>
        /// ��ɫ����.
        /// </summary>
        private IRoleService roleService;



        [TestInitialize]
        public void TestInit()
        {
            MyAuthenticationContext context = new MyAuthenticationContext();
            roleService = new DefaultRoleServiceImpl(context);
        }




        [TestMethod]
        public void TestGetAllRoles()
        {
            // ��ȡȫ����ɫ.
            var allRole = roleService.Query(null);
            // ����ǿ�.
            Assert.IsNotNull(allRole);
            // ����1����ɫ.
            Assert.IsTrue(allRole.QueryResultData.Count > 0);
        }



        [TestMethod]
        public void TestBasicRoleFunc()
        {

            // ���Բ�ѯ������ɫ.
            var oneRole = roleService.GetRole("TEST");
            // ����ǿ�.
            Assert.IsNotNull(oneRole);
            // ���Ϊ���ɹ�.
            Assert.IsFalse(oneRole.IsSuccess);
            


            MyRole testRole = new MyRole()
            {
                RoleCode = "TEST",
                RoleName = "���Խ�ɫ",
                SystemCode = "MYAUTH",
                IsActive = true,
            };
            testRole.BeforeInsertOperation("�����û�");

            // ���Բ���.
            var testResult = this.roleService.NewRole(testRole);
            // ����ǿ�.
            Assert.IsNotNull(testResult);
            // �����ɹ�.
            Assert.IsTrue(testResult.IsSuccess);

            // �����ظ�����.
            testResult = this.roleService.NewRole(testRole);
            // ����ǿ�.
            Assert.IsNotNull(testResult);
            // ����ʧ��.
            Assert.IsFalse(testResult.IsSuccess);



            // ���Բ�ѯ������ɫ.
            oneRole = roleService.GetRole("TEST");
            // ����ǿ�.
            Assert.IsNotNull(oneRole);
            // ���ݺ˶�.
            Assert.AreEqual("���Խ�ɫ", oneRole.ResultData.RoleName);

            // �����޸�.
            oneRole.ResultData.RoleName = "���Խ�ɫ_100";


            // ���Ա���.
            testResult = this.roleService.UpdateRole(oneRole.ResultData);
            // ����ǿ�.
            Assert.IsNotNull(testResult);
            // �����ɹ�.
            Assert.IsTrue(testResult.IsSuccess);


            // �ٴβ�ѯ.
            var oneRole2 = roleService.GetRole("TEST");
            // ����ǿ�.
            Assert.IsNotNull(oneRole2);
            Assert.IsNotNull(oneRole2.ResultData);
            // ���ݺ˶�.
            Assert.AreEqual("���Խ�ɫ_100", oneRole2.ResultData.RoleName);


            // ����ɾ��.
            testResult = this.roleService.RemoveRole("TEST");
            // ����ǿ�.
            Assert.IsNotNull(testResult);
            // �����ɹ�.
            Assert.IsTrue(testResult.IsSuccess);
            
            // �����ظ�ɾ��.
            testResult = this.roleService.RemoveRole("TEST");
            // ����ǿ�.
            Assert.IsNotNull(testResult);
            // ����ʧ��.
            Assert.IsFalse(testResult.IsSuccess);

        }
    }
}