using System;
using System.Collections.Generic;
using System.Text;

using MySSO.ServiceModel;

namespace MySSO.Service
{

    /// <summary>
    /// 登录服务.
    /// </summary>
    public interface ILoginService
    {
        /// <summary>
        /// 登录操作.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        CommonServiceResult<LoginResultData> DoLogin(string username, string password);


        /// <summary>
        /// 查询 Token 是否已登录.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        CommonServiceResult<LoginResultData> IsLogin(Guid token);


        /// <summary>
        /// 登出.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        CommonServiceResult Logout(Guid token);
    }

}
