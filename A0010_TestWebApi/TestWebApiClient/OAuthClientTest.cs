using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.IO;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TestWebApiClient
{
    class OAuthClientTest
    {

        
        /// <summary>
        /// 测试基础服务的 URL.
        /// </summary>
        private string url = "http://localhost:5296/api/values";

        /// <summary>
        /// 获取 Token 的 URL.
        /// </summary>
        private string tokenUrl = "http://localhost:5296/api/Authorize";

        /// <summary>
        /// 获取当前登录信息的 URL.
        /// </summary>
        private string userInfoUrl = "http://localhost:5296/api/UserInfo";




        public OAuthClientTest()
        {
        }


        /// <summary>
        /// 直接访问授权的 web api.
        /// </summary>
        /// <returns></returns>
        public void Call_WebAPI_Without_Access_Token()
        {           
            Console.WriteLine("使用 WebClient 获取 {0} 数据......", url);
            try
            {
                using (WebClient client = new WebClient())
                {
                    client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
                    Stream s = client.OpenRead(url);

                    using (StreamReader sr = new StreamReader(s, Encoding.UTF8))
                    {
                        string line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            Console.WriteLine(line);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }




        /// <summary>
        /// 使用 Access Token 访问授权的 web api.
        /// </summary>
        /// <returns></returns>
        public void Call_WebAPI_By_Access_Token()
        {
            // 取得 Access Token.
            var token = GetAccessToken();

            if (String.IsNullOrEmpty(token))
            {
                Console.WriteLine("获取 Token 失败...");
                return;
            }

            Console.WriteLine(token);

            

            
            try
            {
                using (WebClient client = new WebClient())
                {
                    client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
                    client.Headers.Add("Authorization", "Bearer " + token);

                    Console.WriteLine("使用 WebClient 获取 {0} 数据......", url);
                    using (Stream s = client.OpenRead(url))
                    {
                        using (StreamReader sr = new StreamReader(s, Encoding.UTF8))
                        {
                            string line;
                            while ((line = sr.ReadLine()) != null)
                            {
                                Console.WriteLine(line);
                            }
                        }
                    }

                    Console.WriteLine("使用 WebClient 获取 {0} 数据......", userInfoUrl);
                    using (Stream s = client.OpenRead(userInfoUrl))
                    {
                        using (StreamReader sr = new StreamReader(s, Encoding.UTF8))
                        {
                            string line;
                            while ((line = sr.ReadLine()) != null)
                            {
                                Console.WriteLine(line);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }



        /// <summary>
        /// 取得 Access Token.
        /// </summary>
        /// <returns></returns>
        private string GetAccessToken()
        {
            // 对象.
            var userInfo = new
            {
                User = "test",
                Password = "123456"
            };
            // json 字符串.
            string queryString = JsonConvert.SerializeObject(userInfo);
            // byte 数组.
            byte[] postData = Encoding.UTF8.GetBytes(queryString);

            Console.WriteLine("使用 WebClient 提交登录请求数据......");
            try
            {
                using (WebClient client = new WebClient())
                {
                    // 请求消息头.
                    client.Headers.Add("Content-Type", "application/json-patch+json");
                    client.Headers.Add("Accept", "application/json");


                    //得到返回字符流  
                    byte[] responseData = client.UploadData(tokenUrl, "POST", postData);

                    //解码 
                    string srcString = Encoding.UTF8.GetString(responseData);

                    return JObject.Parse(srcString)["token"].Value<string>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }


    }
}
