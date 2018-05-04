using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using MyFramework.ServiceModel;
using MyFramework.Util;

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

        private readonly MyAuthenticationContext context;


        public DefaultModuleTypeServiceImpl(MyAuthenticationContext context)
        {
            this.context = context;
        }



        CommonQueryResult<MyModuleType> IModuleTypeService.GetModuleTypeList(int pageNo, int pageSize)
        {
            //using (MyAuthenticationContext context = new MyAuthenticationContext())
            {
                var query =
                    from data in context.MyModuleTypes
                    select data;

                // 初始化翻页.
                PageInfo pgInfo = new PageInfo(
                    pageSize: pageSize,
                    pageNo: pageNo,
                    rowCount: query.Count());

                // 翻页.
                query = query.OrderBy(p => p.ModuleTypeCode)
                    .Skip(pgInfo.SkipValue)
                    .Take(pgInfo.PageSize);


                List<MyModuleType> dataList = query.ToList();


                CommonQueryResult<MyModuleType> result = new CommonQueryResult<MyModuleType>()
                {
                    QueryPageInfo = pgInfo,
                    QueryResultData = dataList
                };

                return result;
            }
        }
    }
}
