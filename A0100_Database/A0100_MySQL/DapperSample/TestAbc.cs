using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A0100_MySQL.DapperSample
{

    /// <summary>
    /// 存储数据的类.
    /// </summary>
    public class TestAbc
    {

        public string Id { get; set; }


        public int A { get; set; }

        public int B { get; set; }

        public int C { get; set; }



        public override string ToString()
        {
            return $"TestAbc[id={Id}, a={A}, b={B}, c={C}]";
        }

    }

}
