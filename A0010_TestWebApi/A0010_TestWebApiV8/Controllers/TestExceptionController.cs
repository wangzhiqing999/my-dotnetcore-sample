using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace A0010_TestWebApiV8.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestExceptionController : ControllerBase
    {

        /// <summary>
        /// 测试异常.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult TestGetException()
        {
            // 单纯的抛一个异常，目的是测试那个 全局异常的处理.
            throw new Exception("测试异常");
        }

    }
}
