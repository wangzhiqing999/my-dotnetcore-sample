using Microsoft.VisualStudio.TestTools.UnitTesting;


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
        private IRoleService roleService = new DefaultRoleServiceImpl();


        [TestMethod]
        public void TestGetAllRoles()
        {
            // ��ȡȫ����ɫ.
            var allRole = roleService.GetAllRoles();
            // ����ǿ�.
            Assert.IsNotNull(allRole);
            // ����1����ɫ.
            Assert.IsTrue(allRole.Count > 0);
        }



        [TestMethod]
        public void TestBasicRoleFunc()
        {

            // ���Բ�ѯ������ɫ.
            var oneRole = roleService.GetRole("TEST");
            // ���Ϊ��.
            Assert.IsNull(oneRole);


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
            // ����ɹ�.
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
            Assert.AreEqual("���Խ�ɫ", oneRole.RoleName);

            // �����޸�.
            oneRole.RoleName = "���Խ�ɫ_100";


            // ���Ա���.
            testResult = this.roleService.UpdateRole(oneRole);
            // ����ǿ�.
            Assert.IsNotNull(testResult);
            // ����ɹ�.
            Assert.IsTrue(testResult.IsSuccess);


            // �ٴβ�ѯ.
            var oneRole2 = roleService.GetRole("TEST");
            // ����ǿ�.
            Assert.IsNotNull(oneRole2);
            // ���ݺ˶�.
            Assert.AreEqual("���Խ�ɫ_100", oneRole2.RoleName);


            // ����ɾ��.
            testResult = this.roleService.RemoveRole("TEST");
            // ����ǿ�.
            Assert.IsNotNull(testResult);
            // ����ɹ�.
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
