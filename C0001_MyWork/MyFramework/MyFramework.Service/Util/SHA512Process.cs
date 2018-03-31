using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using System.Security.Cryptography;


namespace MyFramework.Util
{
    public class SHA512Process
    {

        // 注意： 
        //      如果允许使用 中文用户名的话， 这里建议使用 UnicodeEncoding 
        //      如果要和 MySQL 数据库中的 md5 运行结果一致，那么建议使用  ASCIIEncoding
        // private static ASCIIEncoding byteConverter = new ASCIIEncoding();
        private static UnicodeEncoding byteConverter = new UnicodeEncoding();


        private static SHA512 shaM = new SHA512Managed();



        /// <summary>
        /// 获取 SHA512 字符串信息.
        /// </summary>
        /// <param name="source"></param>
        public static string GetSHA512String(string source)
        {

            // 源字符串转换为 byte数组.
            byte[] dataToEncrypt = byteConverter.GetBytes(source);

            // SHA512 处理.
            byte[] sha512Result = shaM.ComputeHash(dataToEncrypt);

            // 结果格式化为 十六进制的字符串格式.
            StringBuilder buff = new StringBuilder();

            for (int i = 0; i < sha512Result.Length; i++)
            {
                buff.AppendFormat("{0:X2}", sha512Result[i]);
            }

            return buff.ToString();
        }



    }
}
