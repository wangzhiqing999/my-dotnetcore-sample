using System.Text;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

using A0009_EF_Postgres_V8.Model;
using A0009_EF_Postgres_V8.DataAccess;

using Npgsql;




namespace A0009_EF_Postgres_V8
{
    internal class Program
    {
        static void Main(string[] args)
        {

            // 注意：这里追加的配置代码，是因为启用了动态 JSON 序列化.
            // 也就是 C# 中，列的数据类型是 Dictionary<string, string>
            // 数据库中，列的数据类型是 jsonb
            // 需要启用动态 JSON 序列化.
            var dataSourceBuilder = new NpgsqlDataSourceBuilder(@"Server=pve001;Port=5432;User ID=postgres;Password=1234567890;Database=postgres;");
            // 启用动态 JSON 序列化
            dataSourceBuilder.EnableDynamicJson();           
            var dataSource = dataSourceBuilder.Build();


            var optionsBuilder = new DbContextOptionsBuilder<TestContext>();
            optionsBuilder.UseNpgsql(dataSource);
            

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


                TestJsonb(context);
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







        private static void TestJsonb(TestContext context)
        {
            Console.WriteLine("------ TestJsonb ------");

            var query = 
                from data in context.Products
                where
                    data.Name == "Ergonomic Chair"
                select data;

            var dbData = query.FirstOrDefault();
            
            if(dbData != null)
            {
                Console.WriteLine("数据已存在！");
                Console.WriteLine(dbData);
            } 
            else
            {
                var newProduct = new Product
                {
                    Name = "Ergonomic Chair",
                    Specifications = new Specifications
                    {
                        Material = "Leather",
                        Color = "Black",
                        Dimensions = "24 x 24 x 35 inches"

                    },
                    Reviews ={
                        new Review
                        {
                            User = "Alice",
                            Content = "Very comfortable",
                            Rating = 5
                        },
                        new Review
                        {
                            User = "Jack",
                            Content = "Not very comfortable",
                            Rating = 4
                        }
                    },
                    Translations ={
                        { "en","Ergonomic Chair"},
                        { "es","Silla Ergonómica"}
                    }
                };

                context.Products.Add(newProduct);
                context.SaveChanges();
            }

            
            // 尝试执行使用Jsonb的查询
            var query1 = 
                from data in context.Products
                where
                    data.Specifications.Material == "Leather"
                select data;

            /*
调试模式下，上面的 query1 的 SQL 语句如下：

SELECT p."Id", p."CreatedAt", p."Name", p."Translations", p."UpdatedAt", p."Reviews", p."Specifications"
FROM product AS p
WHERE (p."Specifications" ->> 'Material') = 'Leather'

             */

            dbData = query1.FirstOrDefault();

            Console.WriteLine("查询 Specifications.Material == Leather ");

            if (dbData != null)
            {
                Console.WriteLine(dbData);
            } 
            else
            {
                Console.WriteLine("数据不存在！");
            }



            /*

            var query2 =
                from data in context.Products
                where
                    data.Reviews.Exists(p=>p.User == "Alice")
                select data;

            dbData = query2.FirstOrDefault();

            上面这个语句，运行出错， 原因是 没法翻译为 sql.

            手动写 SQL， 可以用下面这种写法来写:

SELECT p."Id", p."CreatedAt", p."Name", p."Translations", p."UpdatedAt", p."Reviews", p."Specifications"
FROM product AS p
where (jsonb_path_query_array(p."Reviews",  '$[*] ? (@.User == "Alice")')) != '[]'::jsonb

            */


            var query2 =
                from data in context.Products
                where
                    data.Reviews.First().User == "Alice"
                select data;

            /*
调试模式下，上面的 query2 的 SQL 语句如下：
SELECT p."Id", p."CreatedAt", p."Name", p."Translations", p."UpdatedAt", p."Reviews", p."Specifications"
FROM product AS p
WHERE (
    SELECT r."User"
    FROM ROWS FROM (jsonb_to_recordset(p."Reviews") AS (
        "Content" text,
        "Rating" integer,
        "User" text
    )) WITH ORDINALITY AS r
    LIMIT 1) = 'Alice'
	
            */

            dbData = query2.FirstOrDefault();

            Console.WriteLine("查询 Reviews.First().User == \"Alice\" ");

            if (dbData != null)
            {
                Console.WriteLine(dbData);
            }
            else
            {
                Console.WriteLine("数据不存在！");
            }



            /*

            var query3 =
                from data in context.Products
                where
                    data.Translations["en"] == "Ergonomic Chair"
                select data;

            dbData = query3.FirstOrDefault();

            上面这个语句，运行出错， 原因是 没法翻译为 sql.

            手动写 SQL， 可以用下面这种写法来写:

SELECT p."Id", p."CreatedAt", p."Name", p."Translations", p."UpdatedAt", p."Reviews", p."Specifications"
FROM product AS p
WHERE (p."Translations" ->> 'en') = 'Ergonomic Chair'

            */


        }



    }
}
