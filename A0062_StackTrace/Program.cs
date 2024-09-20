using System;
using System.Diagnostics;

namespace A0062_StackTrace
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");


            try
            {
                TestClass.TestMethod1();
            }
            catch (Exception ex)
            {
                LogException(ex);
            }

            Console.WriteLine("Finish !");
            Console.ReadKey();
        }



        static void LogException(Exception ex)
        {

            // 简单的输出.
            Console.Write(ex.Message);
            Console.WriteLine(ex.ToString());
            Console.WriteLine();


            // 输出堆栈信息
            StackTrace stackTrace = new StackTrace(ex, true);
            foreach (StackFrame frame in stackTrace.GetFrames())
            {
                var method = frame.GetMethod();
                var fileName = frame.GetFileName();
                var lineNumber = frame.GetFileLineNumber();

                // 过滤无关帧
                if (method.DeclaringType.Namespace.StartsWith("System"))
                {
                    continue;
                }

                Console.WriteLine($"Method: {method.Name}");
                Console.WriteLine($"File: {fileName}");
                Console.WriteLine($"Line: {lineNumber}");


                
            }
        }

    }



    class TestClass
    {
        public static void TestMethod1()
        {
            TestMethod2();
        }


        public static void TestMethod2()
        {
            // 这里将抛出一个除零异常.

            int a = 1;
            int b = 0;
            int c = a / b;

            Console.WriteLine(c);
        }
    }

}
