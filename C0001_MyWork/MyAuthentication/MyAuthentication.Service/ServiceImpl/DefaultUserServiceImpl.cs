using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;

using MyFramework.ServiceModel;
using MyFramework.Util;

using MyAuthentication.Model;
using MyAuthentication.DataAccess;
using MyAuthentication.Service;
using MyAuthentication.ServiceModel;


namespace MyAuthentication.ServiceImpl
{

    /// <summary>
    /// 用户服务.
    /// </summary>
    public class DefaultUserServiceImpl : IUserService
    {


        /// <summary>
        /// 登录.
        /// </summary>
        /// <param name="organizationCode">组织代码</param>
        /// <param name="userName">登录用户名</param>
        /// <param name="password">登录密码</param>
        /// <returns></returns>
        public CommonServiceResult DoLogin(string organizationCode, string userName, string password)
        {
            using (MyAuthenticationContext context = new MyAuthenticationContext())
            {
                // 查询 组织信息.
                MyOrganization org = context.MyOrganizations.FirstOrDefault(p => p.LoginOrganizationCode == organizationCode);
                if (org == null)
                {
                    // 组织代码不存在.
                    return AuthenticationServiceResult.OrganizationCodeNotFoundResult;
                }

                // 查询用户信息.
                MyUser user = context.MyUsers.FirstOrDefault(p => p.OrganizationID == org.OrganizationID && p.LoginUserCode == userName);
                if (user == null)
                {
                    // 登录用户代码不存在
                    return AuthenticationServiceResult.LoginUserCodeNotFoundResult;
                }

                // TODO. 密码计算 / 比较.
                if (!String.Equals(user.UserPassword, password, StringComparison.CurrentCultureIgnoreCase))
                {
                    // 密码不正确.
                    return AuthenticationServiceResult.PasswordNotMatchResult;
                }


                // 执行成功.
                CommonServiceResult result = new CommonServiceResult()
                {
                    ResultCode = 0,
                    ResultData = new BasicUserInfo()
                    {
                        UserName = user.UserName,
                        OrganizationID = user.OrganizationID,
                        UserID = user.UserID
                    }
                };
                return result;
            }
        }



        /// <summary>
        /// 查询用户.
        /// </summary>
        /// <param name="pageNo"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public CommonQueryResult<MyUser> Query(long? organizationID, int pageNo, int pageSize)
        {
            using (MyAuthenticationContext context = new MyAuthenticationContext())
            {
                var query =
                    from data in context.MyUsers.Include("Organization")
                    select data;

                if(organizationID != null)
                {
                    query = query.Where(p => p.OrganizationID == organizationID);
                }


                // 初始化翻页.
                PageInfo pgInfo = new PageInfo(
                    pageSize: pageSize,
                    pageNo: pageNo,
                    rowCount: query.Count());

                // 翻页.
                query = query.OrderByDescending(p => p.LastUpdateTime)
                    .Skip(pgInfo.SkipValue)
                    .Take(pgInfo.PageSize);

                List<MyUser> dataList = query.ToList();


                CommonQueryResult<MyUser> result = new CommonQueryResult<MyUser>()
                {
                    QueryPageInfo = pgInfo,
                    QueryResultData = dataList
                };

                return result;
            }
        }



        /// <summary>
        /// 获取用户.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public CommonServiceResult GetUser(long id)
        {
            using (MyAuthenticationContext context = new MyAuthenticationContext())
            {
                var query =
                    from data in context.MyUsers.Include("Organization")
                    where
                        data.UserID == id
                    select data;

                MyUser userData = query.FirstOrDefault();

                if (userData == null)
                {
                    // 数据不存在.
                    return CommonServiceResult.DataNotFoundResult;
                }


                CommonServiceResult result = CommonServiceResult.CreateDefaultSuccessResult(userData);
                return result;
            }
        }



        /// <summary>
        /// 新增用户.
        /// </summary>
        /// <param name="userData"></param>
        /// <returns></returns>
        public CommonServiceResult NewUser(MyUser userData)
        {
            using (MyAuthenticationContext context = new MyAuthenticationContext())
            {
                // 查询 组织信息.
                MyOrganization org = context.MyOrganizations.Find(userData.OrganizationID);
                if (org == null)
                {
                    // 组织代码不存在.
                    return AuthenticationServiceResult.OrganizationCodeNotFoundResult;
                }

                // 查询用户信息.
                MyUser user = context.MyUsers.FirstOrDefault(p => p.OrganizationID == org.OrganizationID && p.LoginUserCode == userData.LoginUserCode);
                if (user != null)
                {
                    // 同一个组织下， 已经存在有相同的登录名.
                    return AuthenticationServiceResult.LoginUserCodeHadExistsResult;
                }

                // TODO. 密码计算


                // 插入
                context.MyUsers.Add(userData);

                // 保存.
                context.SaveChanges();
            }
            return CommonServiceResult.DefaultSuccessResult;
        }


        /// <summary>
        /// 编辑用户.
        /// </summary>
        /// <param name="userData"></param>
        /// <returns></returns>
        public CommonServiceResult UpdateUser(MyUser userData)
        {
            using (MyAuthenticationContext context = new MyAuthenticationContext())
            {
                // 查询用户信息.
                MyUser user = context.MyUsers.Find(userData.UserID);
                if (user == null)
                {
                    // 用户不存在.
                    return AuthenticationServiceResult.UserIDNotFoundResult;
                }

                // 基本数据检查.
                if(user.OrganizationID != userData.OrganizationID)
                {
                    // 不允许修改用户的 组织代码.
                    return AuthenticationServiceResult.OrganizationCodeModifyResult;
                }
                if(user.LoginUserCode != userData.LoginUserCode)
                {
                    // 不允许修改登录用户名.
                    return AuthenticationServiceResult.LoginUserCodeModifyResult;
                }


                // 可修改的部分.
                user.UserName = userData.UserName;
                
                


                // 保存.
                context.SaveChanges();
            }
            return CommonServiceResult.DefaultSuccessResult;
        }


        /// <summary>
        /// 删除用户.
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public CommonServiceResult RemoveUser(long userID)
        {
            using (MyAuthenticationContext context = new MyAuthenticationContext())
            {
                // 查询用户信息.
                MyUser user = context.MyUsers.Find(userID);
                if (user == null)
                {
                    // 用户不存在.
                    return AuthenticationServiceResult.UserIDNotFoundResult;
                }

                // 删除.
                context.MyUsers.Remove(user);

                // 保存.
                context.SaveChanges();
            }
            return CommonServiceResult.DefaultSuccessResult;
        }


    }
}
