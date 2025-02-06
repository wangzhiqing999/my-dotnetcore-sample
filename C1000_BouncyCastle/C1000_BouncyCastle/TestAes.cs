using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Security;


namespace C1000_BouncyCastle
{



    internal class TestAes
    {


        public static byte[] EncryptAES(string plaintext, byte[] key, byte[] iv)
        {

            IBufferedCipher cipher = CipherUtilities.GetCipher("AES/CBC/PKCS7Padding");
            cipher.Init(true, new ParametersWithIV(new KeyParameter(key), iv));
            return cipher.DoFinal(Encoding.UTF8.GetBytes(plaintext));
        }

        public static string DecryptAES(byte[] ciphertext, byte[] key, byte[] iv)
        {
            IBufferedCipher cipher = CipherUtilities.GetCipher("AES/CBC/PKCS7Padding");
            cipher.Init(false, new ParametersWithIV(new KeyParameter(key), iv));
            byte[] plaintext = cipher.DoFinal(ciphertext);
            return Encoding.UTF8.GetString(plaintext);
        }





        static readonly byte[] aesKey = Encoding.UTF8.GetBytes("1234567890123456");
        static readonly byte[] aesIV = Encoding.UTF8.GetBytes("1234567890123456");




        public static void DoTest(string plaintext = "13000000000")
        {
            Console.WriteLine("---------- AES ----------");

            byte[] ciphertext = EncryptAES(plaintext, aesKey, aesIV);
            string decryptedText = DecryptAES(ciphertext, aesKey, aesIV);

            Console.WriteLine($"原始字符: {plaintext}");
            Console.WriteLine($"AES 加密后结果: {Convert.ToBase64String(ciphertext)}");
            Console.WriteLine($"AES 解密后结果: {decryptedText}");

        }


    }
}
