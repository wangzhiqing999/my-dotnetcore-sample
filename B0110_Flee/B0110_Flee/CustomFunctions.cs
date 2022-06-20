using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B0110_Flee
{


    /// <summary>
    /// 自定义的函数.
    /// 
    /// 这个用来模拟， 实际的公式当中， 存在有 特殊的计算逻辑。
    /// 
    /// 
    /// 注意: 这里的方法一定要是静态的、公共的
    /// </summary>
    public class CustomFunctions
    {

        /// <summary>
        /// 用于模拟 +
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static int Add(int a, int b)
        {
            return a + b;
        }

        /// <summary>
        /// 用于模拟 -
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static int Sub(int a, int b)
        {
            return a - b;
        }


        /// <summary>
        /// 用于模拟 *
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static int Mul(int a, int b)
        {
            return a * b;
        }





        /// <summary>
        /// 用于模拟 可变参数的情况.
        /// 也就是：  参数的个数， 是不确定的.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static int Sum(params int[] args)
        {
            int sum = 0;
            foreach(int item in args)
            {
                sum += item; 
            }
            return sum;
        }


    }
}
