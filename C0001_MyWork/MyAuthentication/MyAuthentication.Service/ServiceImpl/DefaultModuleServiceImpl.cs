using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;

using MyAuthentication.Model;
using MyAuthentication.DataAccess;
using MyAuthentication.Service;

namespace MyAuthentication.ServiceImpl
{
    /// <summary>
    /// 模块服务  （仅查询）
    /// </summary>
    public class DefaultModuleServiceImpl : IModuleService
    {
        List<MyModule> IModuleService.GetModuleList(string systemCode, string moduleType)
        {
            using (MyAuthenticationContext context = new MyAuthenticationContext())
            {
                var query =
                    from data in context.MyModules
                    select data;

                if (!String.IsNullOrEmpty(systemCode))
                {
                    query = query.Where(p => p.SystemCode == systemCode);
                }
                if (!String.IsNullOrEmpty(moduleType))
                {
                    query = query.Where(p => p.ModuleTypeCode == moduleType);
                }

                List<MyModule> resultList = query.ToList();
                return resultList;
            }
        }



        List<MyModule> IModuleService.GetModuleListWithActions(string systemCode, string moduleType)
        {
            using (MyAuthenticationContext context = new MyAuthenticationContext())
            {
                var query =
                    from data in context.MyModules.Include("Actions")
                    select data;

                if (!String.IsNullOrEmpty(systemCode))
                {
                    query = query.Where(p => p.SystemCode == systemCode);
                }
                if (!String.IsNullOrEmpty(moduleType))
                {
                    query = query.Where(p => p.ModuleTypeCode == moduleType);
                }

                List<MyModule> resultList = query.ToList();
                return resultList;
            }
        }
    }
}
