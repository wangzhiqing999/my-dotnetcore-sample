using System;
using System.Collections.Generic;
using System.Text;

using MyFramework.ServiceModel;

namespace MyAuthentication.ServiceModel
{

    /// <summary>
    /// 认证模块的执行结果.
    /// </summary>
    [Serializable]
    public class AuthenticationServiceResult : CommonServiceResult
    {


        #region 角色相关.


        /// <summary>
        /// 角色代码不存在.
        /// </summary>
        public const string ResultCodeIsRoleCodeNotFound = "AUTH_ROLE_CODE_NOT_FOUND";

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
        public const string ResultCodeIsRoleCodeHadExists = "AUTH_ROLE_CODE_HAD_EXISTS";

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
        public const string ResultCodeIsOrganizationCodeNotFound = "AUTH_ORGANIZATION_CODE_NOT_FOUND";

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
        public const string ResultCodeIsOrganizationCodeHadExists = "AUTH_ORGANIZATION_CODE_HAD_EXISTS";

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
        public const string ResultCodeIsOrganizationCodeModify = "AUTH_ORGANIZATION_CODE_MODIFY";

        /// <summary>
        /// 不允许修改组织代码的执行结果.
        /// </summary>
        public static readonly CommonServiceResult OrganizationCodeModifyResult = new CommonServiceResult()
        {
            ResultCode = ResultCodeIsOrganizationCodeModify,
            ResultMessage = "不允许修改组织代码",
        };



        /// <summary>
        /// 不允许修改组织ID.
        /// </summary>
        public const string ResultCodeIsOrganizationIDModify = "AUTH_ORGANIZATION_ID_MODIFY";

        /// <summary>
        /// 不允许修改组织ID的执行结果.
        /// </summary>
        public static readonly CommonServiceResult OrganizationIDModifyResult = new CommonServiceResult()
        {
            ResultCode = ResultCodeIsOrganizationIDModify,
            ResultMessage = "不允许修改组织ID",
        };



        /// <summary>
        /// 错误的组织ID.
        /// </summary>
        public const string ResultCodeIsOrganizationIDError = "AUTH_ORGANIZATION_ID_ERROR";

        /// <summary>
        /// 错误的组织ID的执行结果.
        /// </summary>
        public static readonly CommonServiceResult OrganizationIDErrorResult = new CommonServiceResult()
        {
            ResultCode = ResultCodeIsOrganizationIDError,
            ResultMessage = "错误的组织ID",
        };



        /// <summary>
        /// 组织ID不存在.
        /// </summary>
        public const string ResultCodeIsOrganizationIDNotFound = "AUTH_ORGANIZATION_ID_NOT_FOUND";

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
        public const string ResultCodeIsOrganizationIDHadExists = "AUTH_ORGANIZATION_ID_HAD_EXISTS";

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
        public const string ResultCodeIsOrganizationExistsSubUser = "AUTH_ORGANIZATION_EXISTS_SUB_USER";

        /// <summary>
        /// 组织下存在有用户数据的结果.
        /// </summary>
        public static readonly CommonServiceResult OrganizationExistsSubUserResult = new CommonServiceResult()
        {
            ResultCode = ResultCodeIsOrganizationExistsSubUser,
            ResultMessage = "组织下存在有用户数据",
        };




        /// <summary>
        /// 无效的组织
        /// </summary>
        public const string ResultCodeIsOrganizationIsInactive = "AUTH_ORGANIZATION_IS_INACTIVE";

        /// <summary>
        /// 无效的组织的结果.
        /// </summary>
        public static readonly CommonServiceResult OrganizationIsInactive = new CommonServiceResult()
        {
            ResultCode = ResultCodeIsOrganizationIsInactive,
            ResultMessage = "无效的组织",
        };




        /// <summary>
        /// 组织不匹配.
        /// </summary>
        public const string ResultCodeIsOrganizationNotMatch = "AUTH_ORGANIZATION_NOT_MATCH";

        /// <summary>
        /// 组织不匹配的执行结果.
        /// </summary>
        public static readonly CommonServiceResult OrganizationNotMatchResult = new CommonServiceResult()
        {
            ResultCode = ResultCodeIsOrganizationNotMatch,
            ResultMessage = "组织不匹配",
        };




        #endregion




        #region 用户相关.


        /// <summary>
        /// 用户代码不存在.
        /// </summary>
        public const string ResultCodeIsUserIDNotFound = "AUTH_USER_ID_NOT_FOUND";

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
        public const string ResultCodeIsLoginUserCodeNotFound = "AUTH_LOGIN_USER_CODE_NOT_FOUND";

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
        public const string ResultCodeIsLoginUserCodeHadExists = "AUTH_LOGIN_USER_CODE_HAD_EXISTS";

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
        public const string ResultCodeIsLoginUserCodeModify = "AUTH_LOGIN_USER_CODE_MODIFY";

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
        public const string ResultCodeIsPasswordNotMatch = "AUTH_PASSWORD_NOT_MATCH";

        /// <summary>
        /// 登录密码不匹配的执行结果.
        /// </summary>
        public static readonly CommonServiceResult PasswordNotMatchResult = new CommonServiceResult()
        {
            ResultCode = ResultCodeIsPasswordNotMatch,
            ResultMessage = "登录密码不正确",
        };




        #endregion 用户相关.



    }
}
