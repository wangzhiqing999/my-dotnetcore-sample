using System;
using System.Collections.Generic;
using System.Text;

using System.Text.Json;

using A3001_Json.Model;


namespace A3001_Json.Sample
{
    public class TestJsonSerializer
    {

        public static void Test1()
        {
            Console.WriteLine();
            Console.WriteLine("----- TestJsonSerializer.Test1 -----");

            TestData testData = new TestData()
            {
                TestCode = "Z3",
                TestName = "张三",
                TestAge = 33,
                TestList = new List<string> (),
                TestSet = new HashSet<string> ()
            };

            testData.TestList.Add("A");
            testData.TestList.Add("B");
            testData.TestList.Add("C");

            testData.TestSet.Add("1");
            testData.TestSet.Add("2");
            testData.TestSet.Add("3");

            Console.WriteLine(testData);

            // 数据 序列化为 json.
            string json = JsonSerializer.Serialize<TestData>(testData);

            Console.WriteLine(json);


            // json 反序列化为 对象.
            TestData testData2 = JsonSerializer.Deserialize<TestData>(json);

            Console.WriteLine(testData2);
        }



        public static void Test2()
        {
            Console.WriteLine();
            Console.WriteLine("----- TestJsonSerializer.Test2 -----");

            JsonSerializerOptions options = new JsonSerializerOptions();
            
            // 忽略空值.
            options.IgnoreNullValues = true;

            // 忽略只读属性.
            options.IgnoreReadOnlyProperties = true;



            TestData testData = new TestData()
            {
                TestCode = "Z3",
                TestName = "张三",
                TestAge = 33,
                TestList = new List<string>(),
                TestSet = new HashSet<string>()
            };

            testData.TestList.Add("A");
            testData.TestList.Add("B");
            testData.TestList.Add("C");

            testData.TestSet.Add("1");
            testData.TestSet.Add("2");
            testData.TestSet.Add("3");

            Console.WriteLine(testData);

            // 数据 序列化为 json.
            string json = JsonSerializer.Serialize<TestData>(testData, options);

            Console.WriteLine(json);


            // json 反序列化为 对象.
            TestData testData2 = JsonSerializer.Deserialize<TestData>(json, options);

            Console.WriteLine(testData2);
        }

    }
}
