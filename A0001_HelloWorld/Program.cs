using System;

namespace A0001_HelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            for(int i=0; i < 10; i ++) {
                Console.WriteLine("Hello {0}", i);
            }

            Console.ReadLine();
        }
    }
}
