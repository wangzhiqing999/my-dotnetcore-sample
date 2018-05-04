using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;

using MyAuthentication.IModel;
using System.Security.Claims;

namespace MyWork.Web.Filters
{

    /// <summary>
    /// 创建的时候，自动填写组织ID的 Filter
    /// </summary>
    public class WithOrganizationAttribute : Attribute, IActionFilter
    {

        /// <summary>
        /// Action执行之前 相关处理.
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            // 获取令牌内的详细信息.
            var claimsIdentity = context.HttpContext.User.Identity as ClaimsIdentity;
            if (claimsIdentity == null)
            {
                // 没令牌的话， 啥事情也干不了.
                // 忽略后续操作.
                return;
            }

            // 获取组织ID.
            long orgID = Convert.ToInt64(claimsIdentity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.GroupSid).Value);

            // 获取首个参数.
            foreach (var kv in context.ActionArguments)
            {
                // 忽略 Key， 只看 Value.
                var value = kv.Value;
                if (value is IOrganizationManagerAble)
                {
                    // 可由组织管理的数据的接口.
                    IOrganizationManagerAble data = (IOrganizationManagerAble)value;
                    // 组织ID.
                    data.OrganizationID = orgID;
                }
            }
        }


        /// <summary>
        /// Action执行之后 相关处理.
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuted(ActionExecutedContext context)
        {

        }


    }
}
