using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using MyAuthentication.Model;
using MyAuthentication.DataAccess;
using MyAuthentication.Service;

namespace MyAuthentication.ServiceImpl
{

    /// <summary>
    ///  模块类型服务 （仅查询）
    /// </summary>
    public class DefaultModuleTypeServiceImpl : IModuleTypeService
    {
        List<MyModuleType> IModuleTypeService.GetModuleTypeList()
        {
            using (MyAuthenticationContext context = new MyAuthenticationContext())
            {
                var query =
                    from data in context.MyModuleTypes
                    select data;

                List<MyModuleType> resultList = query.ToList();
                return resultList;
            }
        }
    }
}
