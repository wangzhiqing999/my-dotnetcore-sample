using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Masuit.Tools;
using Masuit.Tools.Database;
using Masuit.Tools.Excel;
using Masuit.Tools.Files;



namespace B0260_Masuit.Test
{
    internal class ExcelTest
    {


        public static void Test1()
        {
            Console.WriteLine("===== ExcelTest.Test1 =====");


            List<TestClass> list = new List<TestClass>();
            list.Add(new TestClass() { 
                Name = "张三",
                Age = 23,
                Sex = true,
            });
            list.Add(new TestClass()
            {
                Name = "李四",
                Age = 24,
                Sex = false,
            });
            list.Add(new TestClass()
            {
                Name = "王五",
                Age = 25,
                Sex = true,
            });
            list.Add(new TestClass()
            {
                Name = "赵六",
                Age = 26,
                Sex = false,
            });

            var steam = list.Select(item=>new
            {
                姓名 = item.Name,
                年龄 = item.Age,
                性别 = item.Sex == true ? "男" : "女",
            }).ToDataTable().ToExcel();

            steam.SaveFile("test.xlsx");
        }

    }
}
