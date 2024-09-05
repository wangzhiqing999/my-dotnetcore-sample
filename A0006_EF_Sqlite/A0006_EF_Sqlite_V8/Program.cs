using System;
using System.Linq;

using System.Text;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;

using A0006_EF_Sqlite_V8.Model;
using A0006_EF_Sqlite_V8.DataAccess;




namespace A0006_EF_Sqlite_V8
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");


            var optionsBuilder = new DbContextOptionsBuilder<TestContext>();
            optionsBuilder.UseSqlite(@"Data Source=test.db");


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
                        Console.WriteLine("{0} : {1},{2},{3},{4}", data.Name, data.Phone, data.Address, data.DetailData.RootElement, data.CreatedTime);
                    }

                }
                else
                {
                    Console.WriteLine("数据不存在.");
                    Console.WriteLine("尝试添加...");

                    var datas = new TestData[]
                    {
                        new TestData() { Name = "张三", Phone = "1370000003", Address = "测试地址3", CreatedTime=DateTime.Now },
                        new TestData() { Name = "李四", Phone = "1370000004", Address = "测试地址4", CreatedTime=DateTime.Now},
                        new TestData() { Name = "王五", Phone = "1370000005", Address = "测试地址5", CreatedTime=DateTime.Now},
                        new TestData() { Name = "赵六", Phone = "1370000006", Address = "测试地址6", CreatedTime=DateTime.Now},
                    };

                    foreach (var data in datas)
                    {
                        data.DetailData = JsonDocument.Parse(@"{""id"":12345}");
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
