using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;

using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;

using MyFramework.ServiceModel;

using MyAuthentication.Model;
using MyAuthentication.Service;
using MyAuthentication.ServiceModel;

using MyWork.Web.Models;


namespace MyWork.Web.Controllers
{

    [Produces("application/json")]
    [Route("api/Authentication")]
    public class AuthenticationController : TokenAbleController
    {

        private JwtSettings _jwtSettings;

        /// <summary>
        /// 账户服务
        /// </summary>
        private IUserService _AccountService;





        public AuthenticationController(IUserService accountService, IOptions<JwtSettings> _jwtSettingsAccesser)
        {
            _AccountService = accountService;
            _jwtSettings = _jwtSettingsAccesser.Value;
        }



        [HttpPost]
        public IActionResult Token([FromBody]LoginViewModel viewModel)
        {
            if (ModelState.IsValid)//判断是否合法
            {

                CommonServiceResult loginReslt = this._AccountService.DoLogin(viewModel.Organization, viewModel.User, viewModel.Password);

                if (!loginReslt.IsSuccess)
                {
                    return BadRequest();
                }


                BasicUserInfo userData = loginReslt.ResultData as BasicUserInfo;


                var claim = new Claim[]{
                    // 用户名.
                    new Claim(ClaimTypes.Name, userData.UserName),
                    // 组织机构ID.
                    new Claim(ClaimTypes.GroupSid, userData.OrganizationID.ToString()),
                    // 用户ID.
                    new Claim(ClaimTypes.Sid, userData.UserID.ToString()),
                };

                //对称秘钥
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
                //签名证书(秘钥，加密算法)
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                //生成token  [注意]需要nuget添加Microsoft.AspNetCore.Authentication.JwtBearer包，并引用System.IdentityModel.Tokens.Jwt命名空间
                var token = new JwtSecurityToken(_jwtSettings.Issuer, _jwtSettings.Audience, claim, DateTime.Now, DateTime.Now.AddMinutes(30), creds);

                return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });

            }

            return BadRequest();
        }





        //[Authorize]
        //[HttpGet]
        //public IActionResult Get()
        //{
        //}


    }
}