using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Paddings;
using Org.BouncyCastle.Crypto.Parameters;


namespace C1000_BouncyCastle
{


    /// <summary>
    /// SM4：对称加密.
    /// 
    /// SM4 是中国国家密码管理局发布的对称分组加密算法，也称为 SMS4。
    /// 它是一种分组密码算法，采用 128 位的分组长度和 128 位的密钥长度，通过 32 轮非线性迭代实现加密和解密。
    /// 
    /// SM4 工作模式 SM4 支持多种工作模式，适用于不同的应用场景： 
    /// ECB（电子密码本模式）： 独立加密每个数据块，相同的明文块产生相同的密文块。 适用于加密大量重复数据块，但安全性较低。 
    /// CBC（密码块链接模式）： 使用前一个块的密文与当前块的明文进行 XOR 操作后再加密。 适用于需要较高安全性的场合，如文件加密和网络通信。 
    /// CFB（密码反馈模式）： 将加密算法当作流密码使用，适用于加密字节流或实时数据传输。 
    /// OFB（输出反馈模式）： 生成密钥流与明文进行 XOR 操作，适用于加密大量数据。 
    /// CTR（计数器模式）： 使用递增的计数器与密钥一起加密固定值，然后与明文进行 XOR 操作。 适用于大数据量的加密，具有高安全性和高效率。
    /// 
    /// </summary>
    internal class TestSm4
    {

        public static byte[] SM4Encrypt(byte[] plainText, byte[] key, byte[] iv)
        {
            var cipher = new PaddedBufferedBlockCipher(new CbcBlockCipher(new SM4Engine()), new Pkcs7Padding());
            cipher.Init(true, new ParametersWithIV(new KeyParameter(key), iv));
            return cipher.DoFinal(plainText);
        }

        public static byte[] SM4Decrypt(byte[] cipherText, byte[] key, byte[] iv)
        {
            var cipher = new PaddedBufferedBlockCipher(new CbcBlockCipher(new SM4Engine()), new Pkcs7Padding());
            cipher.Init(false, new ParametersWithIV(new KeyParameter(key), iv));
            return cipher.DoFinal(cipherText);
        }



        public static void DoTest()
        {
            Console.WriteLine("---------- SM4 ----------");


            string plainText = "Hello, SM4!";
            byte[] key = Encoding.UTF8.GetBytes("0123456789abcdef"); // 16 字节密钥
            byte[] iv = Encoding.UTF8.GetBytes("0123456789abcdef"); // 16 字节 IV
            byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);

            // 加密
            byte[] encrypted = SM4Encrypt(plainBytes, key, iv);
            Console.WriteLine("SM4 加密结果（Base64）: " + Convert.ToBase64String(encrypted));

            // 解密
            byte[] decrypted = SM4Decrypt(encrypted, key, iv);
            Console.WriteLine("SM4 解密结果: " + Encoding.UTF8.GetString(decrypted));
        }



    }
}
