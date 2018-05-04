using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using MyFramework.ServiceImpl;
using MyFramework.ServiceModel;
using MyFramework.Util;

using MyWork.Model;
using MyWork.DataAccess;
using MyWork.Service;

using MyWork.ServiceModel;

namespace MyWork.ServiceImpl
{

    /// <summary>
    /// 股票服务.
    /// </summary>
    public class DefaultStockInfoServiceImpl : IStockInfoService
    {

        /// <summary>
        /// 数据服务.
        /// </summary>
        private MyWorkContext context;


        public DefaultStockInfoServiceImpl(MyWorkContext context)
        {
            this.context = context;
        }



        /// <summary>
        /// 获取股票数据.
        /// </summary>
        /// <param name="stockCode"></param>
        /// <returns></returns>
        CommonServiceResult IStockInfoService.GetStockInfo(string stockCode)
        {
            try
            {
                // 查询.
                StockInfo data = context.StockInfos.Find(stockCode);
                if (data == null)
                {
                    // 数据不存在.
                    return WorkServiceResult.StockCodeNotFoundResult;
                }

                CommonServiceResult result = CommonServiceResult.CreateDefaultSuccessResult(data);

                // 返回
                return result;
            }
            catch (Exception ex)
            {
                return new CommonServiceResult(ex);
            }
        }



        /// <summary>
        /// 查询股票列表.
        /// </summary>
        /// <param name="searchText">查询文本</param>
        /// <param name="pageNo">第几页</param>
        /// <param name="pageSize">每页行数</param>
        /// <returns></returns>
        CommonQueryResult<StockInfo> IStockInfoService.SearchStockInfo(string searchText, int pageNo, int pageSize)
        {
            var query =
                from data in context.StockInfos
                where
                    // 股票代码包含 查询文本.
                    data.StockCode.Contains(searchText)
                    // 股票名称包含 查询文本.
                    || data.StockName.Contains(searchText)
                    // 股票拼音包含 查询文本.
                    || data.StockNamePinyin.Contains(searchText)
                select
                    data;

            // 初始化翻页.
            PageInfo pgInfo = new PageInfo(
                pageSize: pageSize,
                pageNo: pageNo,
                rowCount: query.Count());

            // 翻页.
            query = query.OrderBy(p => p.StockCode)
                .Skip(pgInfo.SkipValue)
                .Take(pgInfo.PageSize);

            // 查询.
            List<StockInfo> dataList = query.ToList();

            CommonQueryResult<StockInfo> result = new CommonQueryResult<StockInfo>()
            {
                QueryPageInfo = pgInfo,
                QueryResultData = dataList
            };
            return result;
        }



    }


}
