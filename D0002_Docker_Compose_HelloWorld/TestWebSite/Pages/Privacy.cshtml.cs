using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace TestWebSite.Pages
{
    public class PrivacyModel : PageModel
    {
        private readonly ILogger<PrivacyModel> _logger;

        public PrivacyModel(ILogger<PrivacyModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            ViewData["Message"] = "Hello from webfrontend";

            try
            {

                using (var httpClient = new System.Net.Http.HttpClient())
                {
                    var httpRequestMessage = new HttpRequestMessage
                    {
                        Method = HttpMethod.Get,

                        // 注意：
                        // 这里的 testwebapi
                        // 是配置在 docker-compose.yml 中的 services name.
                        // 8081 是内部的端口号.
                        // 这里预期能正常执行.
                        RequestUri = new Uri("http://testwebapi:8081/api/HelloWorld/13579"),
                    };
                    var resp = httpClient.Send(httpRequestMessage);
                    string resp_body = resp.Content.ReadAsStringAsync().Result;
                    ViewData["Message"] += " and 8081 return " + resp_body;
                }
            } catch(Exception ex)
            {
                ViewData["Message"] += " and 8081 return " + ex.Message;
            }


            try
            {

                using (var httpClient = new System.Net.Http.HttpClient())
                {
                    var httpRequestMessage = new HttpRequestMessage
                    {
                        Method = HttpMethod.Get,

                        // 注意：
                        // 这里的 testwebapi
                        // 是配置在 docker-compose.yml 中的 services name.
                        // 9091 是映射到外部的端口号.
                        // 这里预期是执行失败.
                        RequestUri = new Uri("http://testwebapi:9091/api/HelloWorld/abcdefg"),
                    };
                    var resp = httpClient.Send(httpRequestMessage);
                    string resp_body = resp.Content.ReadAsStringAsync().Result;
                    ViewData["Message"] += " and 9091 return " + resp_body;
                }
            }
            catch (Exception ex)
            {
                ViewData["Message"] += " and 9091 return " + ex.Message;
            }
        }
    }
}
