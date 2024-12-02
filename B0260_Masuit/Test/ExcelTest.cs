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


            // 这里使用类来直接生成.
            // 生成的列名，是优先使用类的属性上的[Description]标签里的内容.
            // 如果属性上没有[Description] 标签，则是使用 属性的名称.
            // 顺序是属性声明的顺序.
            list.ToDataTable().ToExcel().SaveFile("test1.xlsx");



            // 这里是 Select 一个对象。
            // 生成的列名，是匿名对象中的属性名.
            // 顺序是匿名对象中的属性声明的顺序.
            using (var steam = list.Select(item => new
            {
                姓名 = item.Name,
                年龄 = item.Age,
                性别 = item.Sex == true ? "男" : "女",
            }).ToDataTable().ToExcel())
            {
                steam.SaveFile("test2.xlsx");
            }
                
        }

    }
}
