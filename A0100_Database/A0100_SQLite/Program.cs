using System;
using A0100_SQLite.Sample;

namespace A0100_SQLite
{
    class Program
    {
        static void Main(string[] args)
        {

            // 初始化.
            TestInit.DoInit();


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
