using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using W1100_Page.Model;
using W1100_Page.DataAccess;
using W1100_Page.Service;
using W1100_Page.ServiceModel;

namespace W1100_Page.ServiceImpl
{
    public class DefaultFinanceDataService : IFinanceDataService
    {
        public FinanceDataListOutput GetFinanceDataList(GetFinanceDataInput input)
        {
            using(TestContext context = new TestContext())
            {
                var query =
                    from data in context.FinanceDatas
                    select data;


                // 初始化翻页.
                PageInfo pgInfo = new PageInfo(
                    pageSize: input.PageSize,
                    pageNo: input.PageNo,
                    rowCount: query.Count());

                // 翻页.
                query = query.OrderBy(p => p.ID)
                    .Skip(pgInfo.SkipValue)
                    .Take(pgInfo.PageSize);


                FinanceDataListOutput result = new FinanceDataListOutput()
                {
                    PageIndex = pgInfo.PageIndex,
                    PageSize = pgInfo.PageSize,
                    RowCount = pgInfo.RowCount,
                    FinanceDataList = query.ToList()
                };

                return result;
            }
        }
    }
}
