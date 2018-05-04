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


namespace MyAuthentication.ServiceImpl
{
    /// <summary>
    /// 模块服务  （仅查询）
    /// </summary>
    public class DefaultModuleServiceImpl : IModuleService
    {

        private readonly MyAuthenticationContext context;


        public DefaultModuleServiceImpl(MyAuthenticationContext context)
        {
            this.context = context;
        }



        CommonQueryResult<MyModule> IModuleService.GetModuleList(string systemCode, string moduleType, int pageNo, int pageSize)
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


            // 初始化翻页.
            PageInfo pgInfo = new PageInfo(
                pageSize: pageSize,
                pageNo: pageNo,
                rowCount: query.Count());

            // 翻页.
            query = query.OrderBy(p => p.ModuleCode)
                .Skip(pgInfo.SkipValue)
                .Take(pgInfo.PageSize);


            List<MyModule> dataList = query.ToList();


            CommonQueryResult<MyModule> result = new CommonQueryResult<MyModule>()
            {
                QueryPageInfo = pgInfo,
                QueryResultData = dataList
            };

            return result;

        }



        CommonQueryResult<MyModule> IModuleService.GetModuleListWithActions(string systemCode, string moduleType, int pageNo, int pageSize)
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


            // 初始化翻页.
            PageInfo pgInfo = new PageInfo(
                pageSize: pageSize,
                pageNo: pageNo,
                rowCount: query.Count());

            // 翻页.
            query = query.OrderBy(p => p.ModuleCode)
                .Skip(pgInfo.SkipValue)
                .Take(pgInfo.PageSize);


            List<MyModule> dataList = query.ToList();


            CommonQueryResult<MyModule> result = new CommonQueryResult<MyModule>()
            {
                QueryPageInfo = pgInfo,
                QueryResultData = dataList
            };

            return result;

        }
    }
}
