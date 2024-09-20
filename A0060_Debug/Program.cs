using System.Diagnostics;

namespace A0060_Debug
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            

            TestClass.TestMethod1();
            TestClass.TestMethod2();
            TestClass.TestMethod3();


            Console.WriteLine("Finish !");
            Console.ReadKey();
        }
    }





    class TestClass
    {
        public static void TestMethod1()
        {
            Debug.WriteLine("这是一个调试信息");
            Debug.WriteLine("调试信息", "类别");
        }


        public static void TestMethod2()
        {
            Debug.Assert(true, "断言成功");


            // 在 VS 开发环境里，断言失败的情况下，会停在这里，不执行了。
            // 直接运行 .exe ，断言失败的情况下，会直接退出。
            Debug.Assert(false, "断言失败");


            // 使用Debug.Assert()来验证关键业务逻辑的前提条件和后置条件。这可以帮助确保代码在预期的状态下运行。
            // 例如：一个下订单的操作.
            // 当前登录用户必须是 非空的， 否则下单失败。
            // Debug.Assert(loginUser != null, "登录用户对象不能为空");
        }



        public static void TestMethod3()
        {
            try
            {
                int a = 1;
                int b = 0;
                int c = a / b;
            } 
            catch (Exception ex) 
            {
                // 这个实际工作中，是将错误信息写入到日志文件，方便后续排查问题。
                Debug.Fail(ex.Message);
            }            
        }

    }


}
