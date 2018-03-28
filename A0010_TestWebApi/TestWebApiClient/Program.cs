using System;
using System.Threading.Tasks;


namespace TestWebApiClient
{
    class Program
    {
        static void Main(string[] args)
        {

            var f = TestFail();
            Console.ReadKey();


            Console.WriteLine();


            var s = testSuccess();
            Console.ReadKey();


        }



        static async Task TestFail()
        {
            Console.WriteLine("### 直接访问.");

            OAuthClientTest test = new OAuthClientTest();
            test.Call_WebAPI_Without_Access_Token();

            Console.WriteLine("### 直接访问完成! 按任意键继续.");
        }




        static async Task testSuccess()
        {
            Console.WriteLine("### 使用 Access Token 访问.");

            OAuthClientTest test = new OAuthClientTest();
            test.Call_WebAPI_By_Access_Token();

            Console.WriteLine("### 使用 Access Token 访问完成! 按任意键继续.");
        }



    }
}
