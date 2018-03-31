using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Security.Cryptography;


namespace MyFramework.Util
{
    public class MD5Process
    {


        
        // 注意： 
        //      如果允许使用 中文用户名的话， 这里建议使用 UnicodeEncoding 
        //      如果要和 MySQL 数据库中的 md5 运行结果一致，那么建议使用  ASCIIEncoding
        private static ASCIIEncoding byteConverter = new ASCIIEncoding();
        // private static UnicodeEncoding byteConverter = new UnicodeEncoding();


        private static MD5 md5 = new MD5CryptoServiceProvider();



        /// <summary>
        /// 获取 MD5 字符串信息.
        /// </summary>
        /// <param name="source"></param>
        public static string GetMD5String(string source) {

            // 源字符串转换为 byte数组.
            byte[] dataToEncrypt = byteConverter.GetBytes(source);

            // MD5 处理.
            byte[] md5Result = md5.ComputeHash(dataToEncrypt);

            // 结果格式化为 十六进制的字符串格式.
            StringBuilder buff = new StringBuilder ();

            for (int i = 0; i < md5Result.Length; i++) {
                buff.AppendFormat("{0:X2}", md5Result[i]);
            }

            return buff.ToString();
        }


    }
}
