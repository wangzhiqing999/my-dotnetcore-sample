using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Masuit.Tools;

namespace B0260_Masuit.Test
{
    internal class NumberTest
    {

        public static void Test1()
        {
            Console.WriteLine("===== NumberTest.Test1 =====");

            // 大写数字.
            decimal x = 123.45m;
            Console.WriteLine($"{x} 的大写金额： {x.ToChineseMoney()}");
            Console.WriteLine($"{x} 的大写数字： {x.ToChineseNumber()}");
        }


    }
}
