using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.BouncyCastle.Crypto.Digests;


namespace C1000_BouncyCastle
{

    /// <summary>
    /// SM3 是一种密码哈希算法，类似于 SHA-256，但具有更高的安全性。
    /// </summary>
    internal class TestSm3
    {

        public static byte[] CalculateSM3Hash(byte[] data)
        {
            // 创建 SM3 摘要实例
            var digest = new SM3Digest();

            // 更新摘要，将输入字节数组传入
            digest.BlockUpdate(data, 0, data.Length);

            // 创建用于存储哈希结果的字节数组
            byte[] result = new byte[digest.GetDigestSize()];

            // 完成哈希计算，将结果存储到 result 数组中
            digest.DoFinal(result, 0);

            return result;
        }




        public static void DoTest()
        {
            Console.WriteLine("---------- SM3 ----------");

            string input = "Hello, SM3!";
            
            byte[] data = Encoding.UTF8.GetBytes(input);
            
            // 计算SM3哈希
            byte[] hash = CalculateSM3Hash(data);

            Console.WriteLine("SM3 Hash: " + BitConverter.ToString(hash).Replace("-", "").ToLower());

        }


    }
}
