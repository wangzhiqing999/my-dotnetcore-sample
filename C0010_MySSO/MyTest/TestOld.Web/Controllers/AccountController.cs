using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

using Refit;

using TestOld.Web.Service;
using TestOld.Web.ServiceModel;

namespace TestOld.Web.Controllers
{
    public class AccountController : Controller
    {

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        public ActionResult Login(string returnUrl)
        {
            if (String.IsNullOrEmpty(returnUrl))
            {
                returnUrl = "/";
            }

            // 将 returnUrl 存储在 Session 中， 这个 url 不跳转给 SSO 登录页了.
            Session["RETURN_URL"] = returnUrl;


            // 最终要从 登录网站（reg.test.com）， 跳转至本网站（b.test.com）的地址.
            string myUrl = "http://b.test.com/Account/LoginResult";

            string gotoUrl = String.Format("http://reg.test.com/Account/Login?crossdomain=true&returnUrl={0}", Server.UrlEncode(myUrl));

            // 直接跳转至 登录网站的 登录页
            return Redirect(gotoUrl);
        }


        /// <summary>
        /// 登录结果.
        /// </summary>
        /// <param name="token"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        public ActionResult LoginResult(string authToken)
        {
            // 检查 token 是否有效.
            Guid tokenID;
            if (!Guid.TryParse(authToken, out tokenID))
            {
                // 数据无效.
                return Content("无效的Token数据。");
            }

            var service = RestService.For<ILoginService>("http://reg.test.com/");
            CommonServiceResult<LoginResultData> loginResult = service.IsLogin(tokenID).GetAwaiter().GetResult();
            if (!loginResult.IsSuccess)
            {
                // 未登录.
                return Content("未登录。");
            }

            LoginResultData resultData = loginResult.ResultData;
            Session["USER_ID"] = resultData.UserID;
            Session["TOKEN_ID"] = resultData.TokenID;

            // 将当前会话， 标记为已登录.
            FormsAuthentication.SetAuthCookie(authToken, true);

            string returnUrl = Session["RETURN_URL"] as string;
            if (String.IsNullOrEmpty(returnUrl))
            {
                returnUrl = "/";
            }
            return Redirect(returnUrl);
        }


        public ActionResult LogOff()
        {
            // 本机登出.
            FormsAuthentication.SignOut();
            Session.Abandon();

            // SSO 登出.
            string gotoUrl = String.Format("http://reg.test.com/Account/LogOff");

            // 直接跳转至 登录网站的 登出页
            return Redirect(gotoUrl);
        }


    }
}