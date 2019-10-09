using System;

using A3001_Json.Sample;

namespace A3001_Json
{
    class Program
    {
        static void Main(string[] args)
        {
            TestJsonSerializer.Test1();
            TestJsonSerializer.Test2();


            TestJsonDocument.Test1();

            Console.WriteLine("Finish!");
            Console.ReadKey();
        }
    }
}
