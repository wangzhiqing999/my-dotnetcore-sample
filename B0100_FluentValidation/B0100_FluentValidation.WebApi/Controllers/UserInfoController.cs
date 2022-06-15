// #define VALIDATE_BY_HAND

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


using B0100_FluentValidation.Model;
using B0100_FluentValidation.Validator;


namespace B0100_FluentValidation.WebApi.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class UserInfoController : ControllerBase
    {



#if VALIDATE_BY_HAND


        #region 手动指定 Validate 验证处理.

        [HttpPost]
        public IActionResult Register(UserInformation newUser)
        {
            var validator = new UserInformationValidator();
            var validationResult = validator.Validate(newUser);

            if (!validationResult.IsValid)
            {
                // 正常情况下，应该组织一个返回所有错误的格式.
                // 这里只是简单的返回第一个错误文本信息.
                return BadRequest(validationResult.Errors.First().ErrorMessage);
            }

            return Ok();
        }

        #endregion

#else



        #region 使用注入验证器的机制来处理.


        // 使用注入验证器的机制.
        // 需要在 Program.cs 那里， 修改/追加 一些代码：
        // builder.Services.AddControllers().AddFluentValidation();
        // builder.Services.AddTransient<IValidator<UserInformation>, UserInformationValidator>();



        [HttpPost]
        public IActionResult Register(UserInformation newUser)
        {
            return Ok();
        }

#endregion



#endif


    }


}
