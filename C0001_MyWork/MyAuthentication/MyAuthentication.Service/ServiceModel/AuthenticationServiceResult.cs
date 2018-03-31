using System;
using System.Collections.Generic;
using System.Text;

using MyFramework.ServiceModel;

namespace MyAuthentication.ServiceModel
{
    public class AuthenticationServiceResult : CommonServiceResult
    {


        #region 角色相关.


        /// <summary>
        /// 角色代码不存在.
        /// </summary>
        public const int ResultCodeIsRoleCodeNotFound = -1001;

        /// <summary>
        /// 角色代码不存在的执行结果.
        /// </summary>
        public static readonly CommonServiceResult RoleCodeNotFoundResult = new CommonServiceResult()
        {
            ResultCode = ResultCodeIsRoleCodeNotFound,
            ResultMessage = "角色代码不存在",
        };



        /// <summary>
        /// 角色代码已存在.
        /// </summary>
        public const int ResultCodeIsRoleCodeHadExists = -1002;

        /// <summary>
        /// 角色代码已存在的执行结果.
        /// </summary>
        public static readonly CommonServiceResult RoleCodeHadExistsResult = new CommonServiceResult()
        {
            ResultCode = ResultCodeIsRoleCodeHadExists,
            ResultMessage = "角色代码已存在",
        };

        #endregion




        #region 组织机构相关.


        /// <summary>
        /// 组织代码不存在.
        /// </summary>
        public const int ResultCodeIsOrganizationCodeNotFound = -10001;

        /// <summary>
        /// 组织代码不存在的执行结果.
        /// </summary>
        public static readonly CommonServiceResult OrganizationCodeNotFoundResult = new CommonServiceResult()
        {
            ResultCode = ResultCodeIsOrganizationCodeNotFound,
            ResultMessage = "组织代码不存在",
        };



        /// <summary>
        /// 组织代码已存在.
        /// </summary>
        public const int ResultCodeIsOrganizationCodeHadExists = -10002;

        /// <summary>
        /// 组织代码已存在的执行结果.
        /// </summary>
        public static readonly CommonServiceResult OrganizationCodeHadExistsResult = new CommonServiceResult()
        {
            ResultCode = ResultCodeIsOrganizationCodeHadExists,
            ResultMessage = "组织代码已存在",
        };



        /// <summary>
        /// 不允许修改组织代码.
        /// </summary>
        public const int ResultCodeIsOrganizationCodeModify = -10003;

        /// <summary>
        /// 不允许修改组织代码的执行结果.
        /// </summary>
        public static readonly CommonServiceResult OrganizationCodeModifyResult = new CommonServiceResult()
        {
            ResultCode = ResultCodeIsOrganizationCodeModify,
            ResultMessage = "不允许修改组织代码",
        };




        /// <summary>
        /// 组织ID不存在.
        /// </summary>
        public const int ResultCodeIsOrganizationIDNotFound = -10005;

        /// <summary>
        /// 组织ID不存在的执行结果.
        /// </summary>
        public static readonly CommonServiceResult OrganizationIDNotFoundResult = new CommonServiceResult()
        {
            ResultCode = ResultCodeIsOrganizationIDNotFound,
            ResultMessage = "组织ID不存在",
        };


        /// <summary>
        /// 组织ID已存在.
        /// </summary>
        public const int ResultCodeIsOrganizationIDHadExists = -10006;

        /// <summary>
        /// 组织ID已存在的执行结果.
        /// </summary>
        public static readonly CommonServiceResult OrganizationIDHadExistsResult = new CommonServiceResult()
        {
            ResultCode = ResultCodeIsOrganizationIDHadExists,
            ResultMessage = "组织ID已存在",
        };



        /// <summary>
        /// 组织下存在有用户数据.
        /// </summary>
        public const int ResultCodeIsOrganizationExistsSubUser = -10008;

        /// <summary>
        /// 组织下存在有用户数据的结果.
        /// </summary>
        public static readonly CommonServiceResult OrganizationExistsSubUserResult = new CommonServiceResult()
        {
            ResultCode = ResultCodeIsOrganizationExistsSubUser,
            ResultMessage = "组织下存在有用户数据",
        };

        #endregion




        /// <summary>
        /// 用户代码不存在.
        /// </summary>
        public const int ResultCodeIsUserIDNotFound = -10011;

        /// <summary>
        /// 用户代码不存在的执行结果.
        /// </summary>
        public static readonly CommonServiceResult UserIDNotFoundResult = new CommonServiceResult()
        {
            ResultCode = ResultCodeIsUserIDNotFound,
            ResultMessage = "用户代码不存在",
        };



        /// <summary>
        /// 登录用户代码不存在.
        /// </summary>
        public const int ResultCodeIsLoginUserCodeNotFound = -10012;

        /// <summary>
        /// 登录用户代码不存在的执行结果.
        /// </summary>
        public static readonly CommonServiceResult LoginUserCodeNotFoundResult = new CommonServiceResult()
        {
            ResultCode = ResultCodeIsLoginUserCodeNotFound,
            ResultMessage = "登录用户代码不存在",
        };




        /// <summary>
        /// 登录用户代码已存在.
        /// </summary>
        public const int ResultCodeIsLoginUserCodeHadExists = -10013;

        /// <summary>
        /// 登录用户代码不存在的执行结果.
        /// </summary>
        public static readonly CommonServiceResult LoginUserCodeHadExistsResult = new CommonServiceResult()
        {
            ResultCode = ResultCodeIsLoginUserCodeHadExists,
            ResultMessage = "登录用户代码已存在",
        };


        /// <summary>
        /// 不允许修改登录用户代码.
        /// </summary>
        public const int ResultCodeIsLoginUserCodeModify = -10013;

        /// <summary>
        /// 不允许修改登录用户代码的执行结果.
        /// </summary>
        public static readonly CommonServiceResult LoginUserCodeModifyResult = new CommonServiceResult()
        {
            ResultCode = ResultCodeIsLoginUserCodeModify,
            ResultMessage = "不允许修改登录用户代码",
        };





        /// <summary>
        /// 登录密码不匹配.
        /// </summary>
        public const int ResultCodeIsPasswordNotMatch = -10021;

        /// <summary>
        /// 登录用户代码不存在的执行结果.
        /// </summary>
        public static readonly CommonServiceResult PasswordNotMatchResult = new CommonServiceResult()
        {
            ResultCode = ResultCodeIsPasswordNotMatch,
            ResultMessage = "登录密码不正确",
        };

    }
}
