using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFramework.Util
{
    public class KenceryValidateCode
    {

        public static readonly char[] UseAbleChar = { 
                                          '0','1','2','3','4','5','6','7','8','9',
                                          'A','B','C','D','E','F','G',
                                          'H','I','J','K','L','N',
                                          'O','P','Q','R','S','T','U',
                                          'V','X','Y','Z'
                                          };




        /// <summary>
        /// 验证码的最大长度
        /// </summary>
        public int MaxLength
        {

            get { return 10; }

        }



        /// <summary>
        /// 验证码的最小长度
        /// </summary>
        public int MinLength
        {

            get { return 1; }

        }




        /// <summary>
        /// 生成验证码
        /// </summary>
        /// <param name="length">指定验证码的长度</param>
        /// <returns></returns>
        public string CreateValidateCode(int length)
        {

            int[] randMembers = new int[length];

            char[] validateNums = new char[length];

            string validateNumberStr = "";

            //生成起始序列值

            int seekSeek = unchecked((int)DateTime.Now.Ticks);

            Random seekRand = new Random(seekSeek);

            int beginSeek = (int)seekRand.Next(0, Int32.MaxValue - length * 10000);

            int[] seeks = new int[length];

            for (int i = 0; i < length; i++)
            {

                beginSeek += 10000;

                seeks[i] = beginSeek;

            }

            //生成随机数字

            for (int i = 0; i < length; i++)
            {

                Random rand = new Random(seeks[i]);

                int pownum = 1 * (int)Math.Pow(10, length);

                randMembers[i] = rand.Next(pownum, Int32.MaxValue);

            }

            //抽取随机数字

            for (int i = 0; i < length; i++)
            {
                validateNums[i] = UseAbleChar[randMembers[i] % UseAbleChar.Length];
            }

            //生成验证码

            for (int i = 0; i < length; i++)
            {
                validateNumberStr += validateNums[i].ToString();
            }

            return validateNumberStr;

        }



    }


}
