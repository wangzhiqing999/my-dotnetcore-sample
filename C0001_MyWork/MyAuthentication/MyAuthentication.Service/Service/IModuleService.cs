using System;
using System.Collections.Generic;
using System.Text;

using MyAuthentication.Model;
using MyFramework.ServiceModel;


namespace MyAuthentication.Service
{

    /// <summary>
    /// 模块服务  （仅查询）
    /// </summary>
    public interface IModuleService
    {

        /// <summary>
        /// 获取模块列表.
        /// </summary>
        /// <param name="systemCode"> 系统代码 </param>
        /// <param name="moduleType"> 模块类型 </param>
        /// <returns></returns>
        CommonQueryResult<MyModule> GetModuleList(string systemCode, string moduleType, int pageNo = 1, int pageSize = 10);



        /// <summary>
        /// 获取模块列表 （包含动作）
        /// </summary>
        /// <param name="systemCode">系统代码</param>
        /// <param name="moduleType">模块类型</param>
        /// <returns></returns>
        CommonQueryResult<MyModule> GetModuleListWithActions(string systemCode, string moduleType, int pageNo = 1, int pageSize = 10);


    }
}
