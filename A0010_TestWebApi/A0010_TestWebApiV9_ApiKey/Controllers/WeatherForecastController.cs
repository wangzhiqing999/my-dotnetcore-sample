using Microsoft.AspNetCore.Mvc;

namespace A0010_TestWebApiV9_ApiKey.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }


        [HttpPost]
        public IActionResult Post()
        {
            // ��������ʾ�����ҵ���У����ڽ�ɫ�жϵ�����
            // ��ô�����м���������ɫ����ֵ���ŵ� HttpContext.Items �С�
            // ����� HttpContext.Items �л�ȡ��ɫ����ֵ��
            var userRole = HttpContext.Items["UserRole"] as string;

            Response.Headers["UserRole"] = userRole;

            // �޸���������� �ɹ� ���� ʧ�� �������
            if (userRole == "Admin")
            {
                return Ok("Welcome, Admin!");
            }

            // ����� ��ɫ�����������ģ������׳��쳣�������� ͨ�õĴ����ʽ�����ߵ����ߣ���ɫ������������
            return BadRequest();
        }

    }
}
