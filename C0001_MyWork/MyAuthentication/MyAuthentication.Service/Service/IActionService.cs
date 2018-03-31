using System;
using System.Collections.Generic;
using System.Text;

using MyAuthentication.Model;

namespace MyAuthentication.Service
{

    /// <summary>
    /// 动作服务  （仅查询）
    /// </summary>
    public interface IActionService
    {

        /// <summary>
        /// 获取动作列表.
        /// </summary>
        /// <param name="moduleCode"> 模块代码 </param>
        /// <returns></returns>
        List<MyAction> GetActionList(string moduleCode);

    }
}
