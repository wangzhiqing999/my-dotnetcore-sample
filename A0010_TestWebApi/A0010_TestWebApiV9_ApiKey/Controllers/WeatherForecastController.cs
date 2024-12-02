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
            // 这里是演示，如果业务中，存在角色判断的需求。
            // 那么，在中间件那里，将角色的数值，放到 HttpContext.Items 中。
            // 这里，从 HttpContext.Items 中获取角色的数值。
            var userRole = HttpContext.Items["UserRole"] as string;

            Response.Headers["UserRole"] = userRole;

            // 修改这里，来测试 成功 或者 失败 的情况。
            if (userRole == "Admin")
            {
                return Ok("Welcome, Admin!");
            }

            // 如果是 角色不满足条件的，可以抛出异常，或者是 通用的错误格式，告诉调用者，角色不满足条件。
            return BadRequest();
        }

    }
}
