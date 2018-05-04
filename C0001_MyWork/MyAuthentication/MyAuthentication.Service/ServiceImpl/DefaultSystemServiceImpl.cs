using System;
using System.Collections.Generic;
using System.Linq;

using MyFramework.ServiceImpl;
using MyFramework.ServiceModel;
using MyFramework.Util;

using MyAuthentication.Model;
using MyAuthentication.DataAccess;
using MyAuthentication.Service;


namespace MyAuthentication.ServiceImpl
{
    /// <summary>
    /// 系统服务.
    /// </summary>
    public class DefaultSystemServiceImpl : DefaultCommonService, ISystemService
    {


        private readonly MyAuthenticationContext context;


        public DefaultSystemServiceImpl(MyAuthenticationContext context)
        {
            this.context = context;
        }


        CommonQueryResult<MySystem> ISystemService.Query(int pageNo, int pageSize)
        {
            //using (MyAuthenticationContext context = new MyAuthenticationContext())
            {
                var query =
                    from data in context.MySystems
                    select data;


                // 初始化翻页.
                PageInfo pgInfo = new PageInfo(
                    pageSize: pageSize,
                    pageNo: pageNo,
                    rowCount: query.Count());

                // 翻页.
                query = query.OrderBy(p => p.SystemCode)
                    .Skip(pgInfo.SkipValue)
                    .Take(pgInfo.PageSize);

                List<MySystem> dataList = query.ToList();


                CommonQueryResult<MySystem> result = new CommonQueryResult<MySystem>()
                {
                    QueryPageInfo = pgInfo,
                    QueryResultData = dataList
                };

                return result;
            }
        }
    }
}
