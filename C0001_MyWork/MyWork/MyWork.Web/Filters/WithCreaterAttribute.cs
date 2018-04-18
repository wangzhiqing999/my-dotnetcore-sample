using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Filters;

using MyFramework.IModel;
using System.Security.Claims;

namespace MyWork.Web.Filters
{

    /// <summary>
    /// 创建的时候，填写更新记录信息的 Filter
    /// </summary>
    public class WithCreaterAttribute : Attribute, IActionFilter
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

            // 获取用户名.
            string username = claimsIdentity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name).Value;

            // 获取首个参数.
            foreach (var kv in context.ActionArguments)
            {
                // 忽略 Key， 只看 Value.
                var value = kv.Value;
                if(value is IUpdateLogAble)
                {
                    // 需要记录更新日志的接口.
                    IUpdateLogAble data = (IUpdateLogAble)value;
                    // 创建人.
                    data.CreateUser = username;
                    // 最后更新人.
                    data.LastUpdateUser = username;
                    // 创建时间.
                    data.CreateTime = DateTime.Now;
                    // 最后更新时间.
                    data.LastUpdateTime = DateTime.Now;
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
