using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Ini;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.Configuration.Xml;
using Yt.Extensions.Configuration.Properties;
using Yt.Extensions.Configuration.Yaml;
using Yt.Extensions.Configuration.Consul;
using Yt.Extensions.Configuration.Etcd;

namespace A0004_Config_V5
{
    class Program
    {
        static void Main(string[] args)
        {
            TestReadIni();
            TestReadIni2();


            TestReadJson();
            TestReadJson2();


            TestReadXml();
            TestReadXml2();


            TestReadProperties2();


            TestReadYaml2();

            TestReadConsul2();

            TestReadEtcd2();

            Console.ReadKey();
        }




        #region Ini

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

        static void TestReadIni2()
        {
            Console.WriteLine("##### Test read config.ini file All #####");

            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddIniFile("config.ini");

            IConfiguration config = builder.Build();
            foreach (var item in config.AsEnumerable())
            {
                Console.WriteLine($"Key:{item.Key}----Value:{item.Value}");
            }
            Console.WriteLine();
        }


        #endregion






        #region json



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


        static void TestReadJson2()
        {
            Console.WriteLine("##### Test read config.json file All #####");

            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("config.json");

            IConfiguration config = builder.Build();
            foreach (var item in config.AsEnumerable())
            {
                Console.WriteLine($"Key:{item.Key}----Value:{item.Value}");
            }
            Console.WriteLine();
        }


        #endregion






        #region XML

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




        static void TestReadXml2()
        {
            Console.WriteLine("##### Test read config.xml file All #####");

            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddXmlFile("config.xml");

            IConfiguration config = builder.Build();
            foreach (var item in config.AsEnumerable())
            {
                Console.WriteLine($"Key:{item.Key}----Value:{item.Value}");
            }
            Console.WriteLine();
        }

        #endregion



        #region Properties


        static void TestReadProperties2()
        {
            Console.WriteLine("##### Test read config.properties file All #####");

            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddPropertiesFile("config.properties");

            IConfiguration config = builder.Build();
            foreach (var item in config.AsEnumerable())
            {
                Console.WriteLine($"Key:{item.Key}----Value:{item.Value}");
            }
            Console.WriteLine();
        }



        #endregion





        #region Yaml


        static void TestReadYaml2()
        {
            Console.WriteLine("##### Test read config.yaml file All #####");

            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddYamlFile("config.yaml");

            IConfiguration config = builder.Build();
            foreach (var item in config.AsEnumerable())
            {
                Console.WriteLine($"Key:{item.Key}----Value:{item.Value}");
            }
            Console.WriteLine();
        }



        #endregion







        #region consul


        static void TestReadConsul2()
        {
            string serverUrl = @"http://192.168.1.153:8500/";

            Console.WriteLine($"##### Test read consul {serverUrl}  key/value All #####");

            IConfigurationBuilder builder = new ConfigurationBuilder();
            builder.AddConsul(serverUrl, "TestCode", true, 10 * 1000);


            IConfiguration config = builder.Build();
            foreach (var item in config.AsEnumerable())
            {
                Console.WriteLine($"Key:{item.Key}----Value:{item.Value}");
            }
            Console.WriteLine();
        }


        #endregion





        #region Etcd


        static void TestReadEtcd2()
        {
            string serverUrl = @"http://192.168.1.153:2379/";

            Console.WriteLine($"##### Test read Etcd {serverUrl}  key/value All #####");

            IConfigurationBuilder builder = new ConfigurationBuilder();
            builder.AddEtcd(serverUrl, "e3w_test/ABC", true);


            IConfiguration config = builder.Build();
            foreach (var item in config.AsEnumerable())
            {
                Console.WriteLine($"Key:{item.Key}----Value:{item.Value}");
            }
            Console.WriteLine();
        }


        #endregion


    }
}
