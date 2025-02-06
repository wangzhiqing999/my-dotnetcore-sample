using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Org.BouncyCastle.Asn1.GM;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Math;


namespace C1000_BouncyCastle
{


    /// <summary>
    /// SM2 是一种基于椭圆曲线密码学的非对称加密算法，常用于数字签名和密钥交换。
    /// </summary>
    internal class TestSm2
    {

        public static AsymmetricCipherKeyPair GenerateSm2KeyPair()
        {
            // 获取 SM2 椭圆曲线参数
            var ecParams = GMNamedCurves.GetByName("sm2p256v1");
            var domainParams = new ECDomainParameters(ecParams.Curve, ecParams.G, ecParams.N, ecParams.H);

            // 初始化密钥生成器
            var keyGen = new ECKeyPairGenerator("EC"); // 使用 "EC" 作为算法名称
            var keyGenParams = new ECKeyGenerationParameters(domainParams, new SecureRandom());
            keyGen.Init(keyGenParams);

            // 生成密钥对
            return keyGen.GenerateKeyPair();
        }


        public static (AsymmetricKeyParameter privateKey, AsymmetricKeyParameter publicKey) GenerateKeyPair()
        {
            // 创建 SM2 密钥对生成器
            var generator = new ECKeyPairGenerator();
            var sm2Spec = CustomSM2DomainParameters.Instance;
            var keyParams = new ECKeyGenerationParameters(sm2Spec, new SecureRandom());
            generator.Init(keyParams);

            // 生成密钥对
            var keyPair = generator.GenerateKeyPair();
            return (keyPair.Private, keyPair.Public);
        }





        public static byte[] Encrypt(byte[] plainText, AsymmetricKeyParameter publicKey)
        {
            var sm2Engine = new SM2Engine();
            sm2Engine.Init(true, new ParametersWithRandom(publicKey, new SecureRandom()));
            return sm2Engine.ProcessBlock(plainText, 0, plainText.Length);
        }

        public static byte[] Decrypt(byte[] cipherText, AsymmetricKeyParameter privateKey)
        {
            var sm2Engine = new SM2Engine();
            sm2Engine.Init(false, privateKey);
            return sm2Engine.ProcessBlock(cipherText, 0, cipherText.Length);
        }







        public static void DoTest1(string plaintext = "13000000000")
        {

            Console.WriteLine("---------- SM2 ----------");

            // 生成密钥对
            var keyPair = GenerateSm2KeyPair();
            var privateKey = (ECPrivateKeyParameters)keyPair.Private;
            var publicKey = (ECPublicKeyParameters)keyPair.Public;


            Console.WriteLine($"Private Key:{privateKey.D}");
            Console.WriteLine($"Public Key X:{publicKey.Q.XCoord}");
            Console.WriteLine($"Public Key Y:{publicKey.Q.YCoord}");



            // 待加密的明文
            byte[] plainBytes = Encoding.UTF8.GetBytes(plaintext);
            Console.WriteLine($"原始字符: {plaintext}");


            // 加密
            byte[] cipherBytes = Encrypt(plainBytes, publicKey);
            Console.WriteLine($"SM2 加密后结果: {BitConverter.ToString(cipherBytes).Replace("-", "")}");

            // 解密
            byte[] decryptedBytes = Decrypt(cipherBytes, privateKey);
            string decryptedText = Encoding.UTF8.GetString(decryptedBytes);
            Console.WriteLine($"SM2 解密后结果: {decryptedText}");


        }



        public static void DoTest2(string plaintext = "13000000000")
        {

            Console.WriteLine("---------- SM2 ----------");

            // 生成密钥对
            var (privateKey, publicKey) = GenerateKeyPair();


            // 待加密的明文
            byte[] plainBytes = Encoding.UTF8.GetBytes(plaintext);
            Console.WriteLine($"原始字符: {plaintext}");


            // 加密
            byte[] cipherBytes = Encrypt(plainBytes, publicKey);
            Console.WriteLine($"SM2 加密后结果: {BitConverter.ToString(cipherBytes).Replace("-", "")}");

            // 解密
            byte[] decryptedBytes = Decrypt(cipherBytes, privateKey);
            string decryptedText = Encoding.UTF8.GetString(decryptedBytes);
            Console.WriteLine($"SM2 解密后结果: {decryptedText}");


        }
    }



    public class CustomSM2DomainParameters
    {
        public static ECDomainParameters Instance
        {
            get
            {
                var p = new BigInteger("FFFFFFFEFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF00000000FFFFFFFFFFFFFFFF", 16);
                var a = new BigInteger("FFFFFFFEFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF00000000FFFFFFFFFFFFFFFC", 16);
                var b = new BigInteger("28E9FA9E9D9F5E344D5A9E4BCF6509A7F39789F515AB8F92DDBCBD414D940E93", 16);
                var gX = new BigInteger("32C4AE2C1F1981195F9904466A39C9948FE30BBFF2660BE1715A4589334C74C7", 16);
                var gY = new BigInteger("BC3736A2F4F6779C59BDCEE36B692153D0A9877CC62A474002DF32E52139F0A0", 16);
                var n = new BigInteger("FFFFFFFEFFFFFFFFFFFFFFFFFFFFFFFF7203DF6B21C6052B53BBF40939D54123", 16);

                var curve = new Org.BouncyCastle.Math.EC.FpCurve(p, a, b);
                var g = curve.CreatePoint(gX, gY);

                return new ECDomainParameters(curve, g, n);
            }
        }
    }

}
