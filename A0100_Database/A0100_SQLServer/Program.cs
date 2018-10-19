using System;


using A0100_SQLServer.Sample;


namespace A0100_SQLServer
{


    // 本测试项目， 为 .net core 访问 mssql 数据库的例子代码.
    // 项目需要 nuget  引用下面两个包
    // System.Data.Common
    // System.Data.SqlClient
    class Program
    {



        static void Main(string[] args)
        {
            // 测试简单读取.
            TestRead.DoTest();

            // 测试简单更新.
            TestWrite.DoTest();

            // 测试参数的处理.
            TestParam.DoTest("TEST");

            Console.WriteLine("Finish!");
            Console.ReadLine();
        }
    }
}
