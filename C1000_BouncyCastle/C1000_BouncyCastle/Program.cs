namespace C1000_BouncyCastle
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TestAes.DoTest();


            TestSm2.DoTest1();
            TestSm2.DoTest2();


            TestSm3.DoTest();

            TestSm4.DoTest();

            Console.ReadLine();
        }
    }
}
