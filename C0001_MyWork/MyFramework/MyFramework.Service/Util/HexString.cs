using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyFramework.Util
{


    public class HexString
    {

        /// <summary>
        /// byte 数组 转换为 十六进制字符串.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string BytesToHexString(byte[] input)
        {
            StringBuilder hexString = new StringBuilder(64);

            for (int i = 0; i < input.Length; i++)
            {
                hexString.Append(String.Format("{0:X2}", input[i]));
            }
            return hexString.ToString();
        }



        /// <summary>
        /// 十六进制字符串 转换为  byte 数组.
        /// </summary>
        /// <param name="hex"></param>
        /// <returns></returns>
        public static byte[] HexStringToBytes(string hex)
        {
            if (hex.Length == 0)
            {
                return new byte[] { 0 };
            }

            if (hex.Length % 2 == 1)
            {
                hex = "0" + hex;
            }

            byte[] result = new byte[hex.Length / 2];

            for (int i = 0; i < hex.Length / 2; i++)
            {
                result[i] = byte.Parse(hex.Substring(2 * i, 2), System.Globalization.NumberStyles.AllowHexSpecifier);
            }

            return result;
        }


    }
}
