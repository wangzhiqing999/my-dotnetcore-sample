using System;

namespace MyWebCrawler.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            TestReadRl test = new TestReadRl();
            test.TestRead();

            Console.WriteLine("===== Finish =====");
            Console.ReadKey();
        }
    }
}
