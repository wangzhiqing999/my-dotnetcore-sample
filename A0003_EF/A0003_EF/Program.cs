using System;
using System.Linq;

using System.Text;


using Microsoft.EntityFrameworkCore;

using A0003_EF.Model;
using A0003_EF.DataAccess;


namespace A0003_EF
{
    class Program
    {
        static void Main(string[] args)
        {
            // 这一行，是为了正确显示中文信息而使用的。
            // 需要 NuGet 引用 System.Text.Encoding.CodePages
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);


            Console.WriteLine("Hello World!");


            var optionsBuilder = new DbContextOptionsBuilder<TestContext>();
            optionsBuilder.UseSqlServer(@"Data Source=localhost\SQLEXPRESS;Initial Catalog=Test;User Id=sa;Password=123456;");
            
            using (TestContext context = new TestContext(optionsBuilder.Options))
            {

                context.Database.EnsureCreated();


                if (context.TestDatas.Any())
                {
                    Console.WriteLine("数据已存在...");

                    var query =
                        from data in context.TestDatas
                        select data;

                    foreach(var data in query)
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
            }

            Console.WriteLine("Finish!!!");
            Console.ReadLine();

        }
    }
}