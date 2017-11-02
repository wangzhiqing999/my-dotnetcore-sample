using System;
using System.IO;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Ini;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.Configuration.Xml;

namespace A0004_Config
{
    class Program
    {
        static void Main(string[] args)
        {
            TestReadIni();

            TestReadJson();

            TestReadXml();


            Console.ReadKey();
        }



        /// <summary>
        /// 测试读取 ini 文件.
        /// </summary>
        static void TestReadIni()
        {
            Console.WriteLine("##### Test read config.ini file #####");

            IConfigurationBuilder builder = new ConfigurationBuilder();
            IniConfigurationSource source = new IniConfigurationSource();
            source.Build(builder);
            source.Path = "config.ini";
            IniConfigurationProvider provider = new IniConfigurationProvider(source);
            provider.Load();


            string segoneCon = null;
            provider.TryGet("SegOne:Con", out segoneCon);
            Console.WriteLine($"SegOne-Con={segoneCon}");

            string segtwoCon = null;
            provider.TryGet("SegTwo:Con", out segtwoCon);
            Console.WriteLine($"SegTwo-Con={segtwoCon}");

            string segtwoExtPort = null;
            provider.TryGet("SegTwo:Ext:Port", out segtwoExtPort);
            Console.WriteLine($"SegTwo-Ext:Port={segtwoExtPort}");

            string segthreeCon = null;
            provider.TryGet("Seg:Three:Con", out segthreeCon);
            Console.WriteLine($"Seg:Three-Con={segthreeCon}");

            Console.WriteLine();
        }





        /// <summary>
        /// 测试读取 json 文件.
        /// </summary>
        static void TestReadJson()
        {
            Console.WriteLine("##### Test read config.json file #####");

            IConfigurationBuilder builder = new ConfigurationBuilder();
            JsonConfigurationSource source = new JsonConfigurationSource();
            source.Build(builder);
            source.Path = "config.json";
            JsonConfigurationProvider provider = new JsonConfigurationProvider(source);
            provider.Load();

            string url = null;
            provider.TryGet("url", out url);
            Console.WriteLine($"url={url}");

            string one = null;
            provider.TryGet("port:one", out one);
            Console.WriteLine($"port-one={one}");


            string two = null;
            provider.TryGet("port:two", out two);
            Console.WriteLine($"port0two={two}");


            Console.WriteLine();
        }



        /// <summary>
        /// 测试读取 XML 文件.
        /// </summary>
        static void TestReadXml()
        {
            Console.WriteLine("##### Test read config.xml file #####");

            IConfigurationBuilder builder = new ConfigurationBuilder();
            XmlConfigurationSource source = new XmlConfigurationSource();
            source.Build(builder);
            source.Path = "config.xml";
            XmlConfigurationProvider provider = new XmlConfigurationProvider(source);
            provider.Load();

            string con = null;
            provider.TryGet("data:con", out con);
            Console.WriteLine($"con={con}");

            string name = null;
            provider.TryGet("inventory:value", out name);
            Console.WriteLine($"value={name}");


            Console.WriteLine();

        }


    }
}