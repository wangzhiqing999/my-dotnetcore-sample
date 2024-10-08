﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Masuit.Tools;


namespace B0260_Masuit.Test
{
    internal class ObjectTest
    {

        public static void Test1()
        {
            Console.WriteLine("===== ObjectTest.Test1 =====");

            var a = new TestClass()
            {
                Name = "张三"
            };
            var b = new TestClass()
            {
                Sex = true
            };
            var c = new TestClass()
            {
                Age = 3
            };
            var merge = a.Merge(b, c);

            Console.WriteLine(merge);
        }

    }


    public class TestClass
    {
        public string Name { get; set; }

        public bool? Sex { get; set; }

        public int? Age { get; set; }

        public override string ToString()
        {
            return $"Name:{Name},Sex:{Sex},Age:{Age}";
        }
    }

}
