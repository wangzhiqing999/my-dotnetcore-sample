using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace W2002_Web_V8.Pages
{
    public class WeatheModel : PageModel
    {

        private readonly IHttpClientFactory _httpClientFactory;

        public WeatheModel(IHttpClientFactory httpClientFactory) =>
            _httpClientFactory = httpClientFactory;




        public WeatherData? _WeatherData { get; set;}



        public async Task OnGet()
        {
            string city;

            if(Request.Query.TryGetValue("name", out var name))
            {
                city = name;
            }
            else
            {
                city = "北京市";
            }


            // 这个 uapis 定义在 Program.cs 那里.
            var httpClient = _httpClientFactory.CreateClient("uapis");

            // Program.cs 那里，针对 “uapis” 定义了 BaseAddress.
            // 这里请求， 就只写后面的访问路径.
            var httpResponseMessage = await httpClient.GetAsync(
                $"api/weather?name={city}");

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                using (var contentStream =
                    await httpResponseMessage.Content.ReadAsStreamAsync())
                {
                    _WeatherData = await JsonSerializer.DeserializeAsync<WeatherData>(contentStream);
                }                    
            }
        }
    }

    /// <summary>
    /// 请求天气返回的数据结构
    /// </summary>
    public class WeatherData
    {
        /// <summary>
        /// 状态码
        /// </summary>
        public int? code { get; set; }

        public string? msg { get; set; }

        /// <summary>
        /// 省
        /// </summary>
        public string? province { get; set; }

        /// <summary>
        /// 市
        /// </summary>
        public string? city { get; set; }


        /// <summary>
        /// 温度
        /// </summary>
        public string? temperature { get; set; }

        /// <summary>
        /// 天气.
        /// </summary>
        public string? weather { get; set; }

        /// <summary>
        /// 风向.
        /// </summary>
        public string? wind_direction { get; set; }

        /// <summary>
        /// 风速(单位:级)
        /// </summary>
        public string? wind_power { get; set; }

        /// <summary>
        /// 湿度.
        /// </summary>
        public string? humidity { get; set; }

        /// <summary>
        /// 数据更新时间
        /// </summary>
        public string? reporttime { get; set; }
    }
}
