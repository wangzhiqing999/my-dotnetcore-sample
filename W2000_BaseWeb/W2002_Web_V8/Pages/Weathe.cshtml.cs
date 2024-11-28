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
                city = "������";
            }


            // ��� uapis ������ Program.cs ����.
            var httpClient = _httpClientFactory.CreateClient("uapis");

            // Program.cs ������ ��uapis�� ������ BaseAddress.
            // �������� ��ֻд����ķ���·��.
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
    /// �����������ص����ݽṹ
    /// </summary>
    public class WeatherData
    {
        /// <summary>
        /// ״̬��
        /// </summary>
        public int? code { get; set; }

        public string? msg { get; set; }

        /// <summary>
        /// ʡ
        /// </summary>
        public string? province { get; set; }

        /// <summary>
        /// ��
        /// </summary>
        public string? city { get; set; }


        /// <summary>
        /// �¶�
        /// </summary>
        public string? temperature { get; set; }

        /// <summary>
        /// ����.
        /// </summary>
        public string? weather { get; set; }

        /// <summary>
        /// ����.
        /// </summary>
        public string? wind_direction { get; set; }

        /// <summary>
        /// ����(��λ:��)
        /// </summary>
        public string? wind_power { get; set; }

        /// <summary>
        /// ʪ��.
        /// </summary>
        public string? humidity { get; set; }

        /// <summary>
        /// ���ݸ���ʱ��
        /// </summary>
        public string? reporttime { get; set; }
    }
}
