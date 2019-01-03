using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;

using MySSO.Model;
using MySSO.DataAccess;

using MySSO.Service;
using MySSO.ServiceModel;

namespace MySSO.ServiceImpl
{
    public class DefaultLoginService : ILoginService
    {

        private readonly MySSOContext _Context;

        public DefaultLoginService(MySSOContext context)
        {
            this._Context = context;
        }


        /// <summary>
        /// 登录.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public CommonServiceResult<LoginResultData> DoLogin(string username, string password)
        {
            try
            {
                SystemUser user = this._Context.SystemUsers.SingleOrDefault(p => p.UserName == username);
                if(user == null)
                {
                    // 用户不存在.
                    return CommonServiceResult<LoginResultData>.DataNotFoundResult;
                }

                // TODO 测试项目，这里的密码，就明文存储了。 实际的项目，需要加密处理。
                if(password != user.UserPassword)
                {
                    // 密码不正确.
                    return CommonServiceResult<LoginResultData>.DataNotFoundResult;
                }

                // 执行到这里， 说明 用户名存在，且密码匹配.

                // 创建 Token.
                SystemUserToken tokenData = new SystemUserToken()
                {
                    // 令牌ID.
                    TokenID = Guid.NewGuid(),

                    // 用户ID.
                    UserID = user.UserID,

                    // 令牌启用时间
                    StartTime = DateTime.Now,

                    // 令牌过期时间
                    ExpiredTime = DateTime.Now.AddDays(2),
                };

                // 插入 Token 数据.
                this._Context.SystemUserTokens.Add(tokenData);
                // 物理保存.
                this._Context.SaveChanges();

                // 创建结果数据.
                LoginResultData data = new LoginResultData()
                {
                    UserID = tokenData.UserID,
                    TokenID = tokenData.TokenID
                };

                return CommonServiceResult<LoginResultData>.CreateDefaultSuccessResult(data);
            }
            catch (Exception ex)
            {
                return new CommonServiceResult<LoginResultData>(ex);
            }
        }



        /// <summary>
        /// 是否登录.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public CommonServiceResult<LoginResultData> IsLogin(Guid token)
        {
            try
            {
                // 查询  Token.
                SystemUserToken tokenData = this._Context.SystemUserTokens.Find(token);

                if(tokenData == null)
                {
                    // Token 数据不存在， 意味着， 未登录.
                    return CommonServiceResult<LoginResultData>.DataNotFoundResult;
                }

                if (!tokenData.IsUseable)
                {
                    // Token 超时.
                    return CommonServiceResult<LoginResultData>.CreateFailResult("TOKEN_TIMEOUT", "Token 已过期");
                }

                // 执行到这里， 说明 Token 存在， 且有效.

                // 创建结果数据.
                LoginResultData data = new LoginResultData()
                {
                    UserID = tokenData.UserID,
                    TokenID = tokenData.TokenID
                };

                return CommonServiceResult<LoginResultData>.CreateDefaultSuccessResult(data);
            }
            catch (Exception ex)
            {
                return new CommonServiceResult<LoginResultData>(ex);
            }
        }



        /// <summary>
        /// 登出.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public CommonServiceResult Logout(Guid token)
        {
            try
            {
                // 查询  Token.
                SystemUserToken tokenData = this._Context.SystemUserTokens.Find(token);

                if (tokenData == null)
                {
                    // Token 数据不存在， 意味着， 未登录.
                    return CommonServiceResult.DataNotFoundResult;
                }

                // 删除.
                this._Context.SystemUserTokens.Remove(tokenData);
                // 物理保存.
                this._Context.SaveChanges();

                // 返回默认的成功值.
                return CommonServiceResult.DefaultSuccessResult;
            }
            catch (Exception ex)
            {
                return new CommonServiceResult(ex);
            }
        }
    }
}
