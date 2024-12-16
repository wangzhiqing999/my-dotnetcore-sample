using System.Text.Json;
using Microsoft.Extensions.Logging;
using Quartz;



namespace B0254_Quartz_Web.Jobs
{
    public class WeatherJob : IJob
    {

        private readonly ILogger<WeatherJob> _logger;


        private readonly IHttpClientFactory _httpClientFactory;



        public WeatherJob(ILogger<WeatherJob> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;

            _httpClientFactory = httpClientFactory;
        }



        public async Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("--- WeatherJob Start! ---");

            string city = "北京市";

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
                    var _WeatherData = await JsonSerializer.DeserializeAsync<WeatherData>(contentStream);

                    if(_WeatherData != null)
                    {
                        _logger.LogInformation(_WeatherData.ToString());
                    }                    
                }
            }
        }


    }


    /// <summary>
    /// 请求天气返回的数据结构
    /// </summary>
    public record WeatherData
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
