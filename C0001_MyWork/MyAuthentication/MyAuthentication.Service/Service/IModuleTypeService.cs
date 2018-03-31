using System;
using System.Collections.Generic;
using System.Text;

using MyAuthentication.Model;

namespace MyAuthentication.Service
{

    /// <summary>
    /// 模块类型服务 （仅查询）
    /// </summary>
    public interface IModuleTypeService
    {

        /// <summary>
        /// 获取模块类型列表
        /// </summary>
        /// <returns></returns>
        List<MyModuleType> GetModuleTypeList();

    }
}
