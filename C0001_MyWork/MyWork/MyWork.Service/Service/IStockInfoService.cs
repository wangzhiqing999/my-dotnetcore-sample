using System;
using System.Collections.Generic;
using System.Text;

using MyWork.Model;
using MyFramework.ServiceModel;


namespace MyWork.Service
{

    /// <summary>
    /// 股票服务.
    /// </summary>
    public interface IStockInfoService
    {

        /// <summary>
        /// 获取股票数据.
        /// </summary>
        /// <param name="stockCode"></param>
        /// <returns></returns>
        CommonServiceResult GetStockInfo(string stockCode);


        /// <summary>
        /// 查询股票列表.
        /// </summary>
        /// <param name="searchText">查询文本</param>
        /// <param name="pageNo">第几页</param>
        /// <param name="pageSize">每页行数</param>
        /// <returns></returns>
        CommonQueryResult<StockInfo> SearchStockInfo(string searchText, int pageNo = 1, int pageSize = 10);

    }


}
