using System;
using System.Linq;

using System.Text;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

using A0005_EF_Mysql_V6.Model;
using A0005_EF_Mysql_V6.DataAccess;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;
using MySql.EntityFrameworkCore.Extensions;

namespace A0005_EF_Mysql_V6
{


    /// <summary>
    /// 这个类是为了处理  Add-Migration MyFirstMigration 出错的 Bug。
    /// 加上这个类，就能正常的完成 Add-Migration 的相关处理。
    /// </summary>
    public class MysqlEntityFrameworkDesignTimeServices : IDesignTimeServices
    {
        public void ConfigureDesignTimeServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddEntityFrameworkMySQL();
            new EntityFrameworkRelationalDesignServicesBuilder(serviceCollection)
                .TryAddCoreServices();
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            // 这一行，是为了正确显示中文信息而使用的。
            // 需要 NuGet 引用 System.Text.Encoding.CodePages
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            Console.WriteLine("Hello World!");


            var optionsBuilder = new DbContextOptionsBuilder<TestContext>();
            optionsBuilder.UseMySQL(connectionString: @"Server=pve002;Database=test2;Uid=root;Pwd=123456;CharSet=utf8");

            using (TestContext context = new TestContext(optionsBuilder.Options))
            {

                context.Database.EnsureCreated();



                if (context.TestDatas.Any())
                {
                    Console.WriteLine("数据已存在...");

                    var query =
                        from data in context.TestDatas
                        select data;

                    foreach (var data in query)
                    {
                        Console.WriteLine("{0} : {1},  {2}", data.Name, data.Phone, data.Address);
                    }

                }
                else
                {
                    Console.WriteLine("数据不存在.");
                    Console.WriteLine("尝试添加...");

                    var datas = new TestData[]
                    {
                    new TestData() { Name = "张三", Phone = "1370000003", Address = "测试地址3"},
                    new TestData() { Name = "李四", Phone = "1370000004", Address = "测试地址4"},
                    new TestData() { Name = "王五", Phone = "1370000005", Address = "测试地址5"},
                    new TestData() { Name = "赵六", Phone = "1370000006", Address = "测试地址6"},
                    };

                    foreach (var data in datas)
                    {
                        context.TestDatas.Add(data);
                    }
                    context.SaveChanges();

                }




                TestOneToMany(context);


                TestManyToMany(context);


                TestOneToOne(context);
            }

            Console.WriteLine("Finish!!!");
            Console.ReadLine();

        }




        private static void TestOneToMany(TestContext context)
        {
            Console.WriteLine("------ TestOneToMany ------");

            var docType = context.DocumentTypes.Find("TEST");

            if (docType == null)
            {
                docType = new DocumentType()
                {
                    DocumentTypeCode = "TEST",
                    DocumentTypeName = "测试文档类型",
                };
                context.DocumentTypes.Add(docType);


                var doc1 = new Document() { DocumentTitle = "TEST1", DocumentContent = "测试文档1", DocumentType = docType };
                var doc2 = new Document() { DocumentTitle = "TEST2", DocumentContent = "测试文档2", DocumentType = docType };
                var doc3 = new Document() { DocumentTitle = "TEST3", DocumentContent = "测试文档3", DocumentType = docType };

                context.Documents.Add(doc1);
                context.Documents.Add(doc2);
                context.Documents.Add(doc3);

                context.SaveChanges();
            }


            docType = context.DocumentTypes.Include("DocumentList").FirstOrDefault(p => p.DocumentTypeCode == "TEST");
            Console.WriteLine(docType);

            foreach (var doc in docType.DocumentList)
            {
                Console.WriteLine(doc);
            }
        }





        private static void TestManyToMany(TestContext context)
        {
            Console.WriteLine("------ TestManyToMany ------");
            for (int i = 1; i <= 3; i++)
            {
                // 角色.
                string roleCode = "TEST_ROLE_" + i;
                var role = context.MrRoles.Find(roleCode);
                if (role == null)
                {
                    role = new MrRole()
                    {
                        RoleCode = roleCode,
                        RoleName = "测试角色" + i
                    };
                    context.MrRoles.Add(role);
                }
                // 用户.
                string userCode = "TEST_USER_" + i;
                var user = context.MrUsers.Find(userCode);
                if (user == null)
                {
                    user = new MrUser()
                    {
                        UserCode = userCode,
                        UserName = "测试用户" + i,
                    };
                    context.MrUsers.Add(user);
                }
            }
            context.SaveChanges();

            for (int i = 1; i <= 3; i++)
            {
                // 角色.
                string roleCode = "TEST_ROLE_" + i;
                var role = context.MrRoles.Include("UserRoles").FirstOrDefault(p => p.RoleCode == roleCode);
                if (role.UserRoles.Count == 0)
                {
                    for (int j = 1; j <= i; j++)
                    {
                        string userCode = "TEST_USER_" + j;
                        MrUserRole userRole = new MrUserRole()
                        {
                            RoleCode = roleCode,
                            UserCode = userCode,
                        };
                        context.MrUserRoles.Add(userRole);
                    }
                }
            }
            context.SaveChanges();



            Console.WriteLine("以角色为主，进行查询！");
            for (int i = 1; i <= 3; i++)
            {
                // 角色.
                string roleCode = "TEST_ROLE_" + i;
                var role = context.MrRoles.Include("UserRoles.User").FirstOrDefault(p => p.RoleCode == roleCode);

                Console.WriteLine("角色：{0} / {1}", role.RoleCode, role.RoleName);
                foreach (var item in role.UserRoles)
                {
                    var user = item.User;
                    Console.WriteLine("--- 用户：{0} / {1}", user.UserCode, user.UserName);
                }
            }

            Console.WriteLine("以用户为主，进行查询！");
            for (int i = 1; i <= 3; i++)
            {
                string userCode = "TEST_USER_" + i;
                var user = context.MrUsers.Include("UserRoles.Role").FirstOrDefault(p => p.UserCode == userCode);

                Console.WriteLine("用户：{0} / {1}", user.UserCode, user.UserName);
                foreach (var item in user.UserRoles)
                {
                    var role = item.Role;
                    Console.WriteLine("--- 角色：{0} / {1}", role.RoleCode, role.RoleName);
                }
            }


        }






        private static void TestOneToOne(TestContext context)
        {
            Console.WriteLine("------ TestOneToOne ------");

            for (int i = 1; i <= 3; i += 2)
            {
                string userCode = "TEST_USER_" + i;

                MrUserExp exp = context.MrUserExps.Find(userCode);

                if (exp == null)
                {
                    exp = new MrUserExp()
                    {
                        UserCode = userCode,
                        UserExpData = "USER_EXP_" + i
                    };

                    context.MrUserExps.Add(exp);
                }
            }
            context.SaveChanges();


            for (int i = 1; i <= 3; i++)
            {
                string userCode = "TEST_USER_" + i;
                var user = context.MrUsers.Include("UserExp").FirstOrDefault(p => p.UserCode == userCode);

                Console.WriteLine("用户：{0} / {1}", user.UserCode, user.UserName);
                if (user.UserExp != null)
                {
                    Console.WriteLine("--- 存在用户扩展数据： {0} ", user.UserExp.UserExpData);
                }

            }
        }


    }
}
