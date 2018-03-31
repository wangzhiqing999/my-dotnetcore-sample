using System;
using System.Collections.Generic;
using System.Text;

using MyFramework.ServiceModel;
using MyAuthentication.Model;

namespace MyAuthentication.Service
{
    /// <summary>
    /// 用户服务.
    /// </summary>
    public interface IUserService
    {

        /// <summary>
        /// 登录.
        /// </summary>
        /// <param name="organizationCode">组织代码</param>
        /// <param name="userName">登录用户名</param>
        /// <param name="password">登录密码</param>
        /// <returns></returns>
        CommonServiceResult DoLogin(string organizationCode, string userName, string password);



        /// <summary>
        /// 新增用户.
        /// </summary>
        /// <param name="userData"></param>
        /// <returns></returns>
        CommonServiceResult NewUser(MyUser userData);

        /// <summary>
        /// 更新用户.
        /// </summary>
        /// <param name="userData"></param>
        /// <returns></returns>
        CommonServiceResult UpdateUser(MyUser userData);

        /// <summary>
        /// 删除用户.
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        CommonServiceResult RemoveUser(long userID);

    }
}
