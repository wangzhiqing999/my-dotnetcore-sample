using System;
using System.Collections.Generic;
using System.Text;

using MyWork.Model;
using MyFramework.ServiceModel;


namespace MyWork.Service
{

    /// <summary>
    /// 股票池服务.
    /// </summary>
    public interface IStockPoolService
    {

        /// <summary>
        /// 创建股票池.
        /// </summary>
        /// <param name="stockPool"></param>
        /// <returns></returns>
        CommonServiceResult CreateStockPool(StockPool stockPool);

        /// <summary>
        /// 更新股票池.
        /// </summary>
        /// <param name="stockPool"></param>
        /// <returns></returns>
        CommonServiceResult UpdateStockPool(StockPool stockPool);


        /// <summary>
        /// 删除股票池.
        /// </summary>
        /// <param name="stockPoolID"></param>
        /// <returns></returns>
        CommonServiceResult DeleteStockPool(long organizationID, long stockPoolID);


        /// <summary>
        /// 获取股票池数据.
        /// </summary>
        /// <param name="stockPoolID"></param>
        /// <returns></returns>
        CommonServiceResult GetStockPool(long organizationID, long stockPoolID);


        /// <summary>
        /// 获取股票池列表.
        /// </summary>
        /// <param name="organizationID"></param>
        /// <returns></returns>
        CommonQueryResult<StockPool> GetStockPoolList(long organizationID, int pageNo = 1, int pageSize = 10);








        /// <summary>
        /// 获取指定股票池中的股票列表.
        /// </summary>
        /// <param name="stockPoolID"></param>
        /// <returns></returns>
        CommonQueryResult<StockInfo> GetStockInfoInPool(long organizationID, long stockPoolID, int pageNo = 1, int pageSize = 10);


        /// <summary>
        /// 向股票池中添加股票数据.
        /// </summary>
        /// <param name="stockPoolID"></param>
        /// <param name="stockCode"></param>
        /// <returns></returns>
        CommonServiceResult AddStockToPool(long organizationID, long stockPoolID, string stockCode);


        /// <summary>
        /// 从股票池中移除股票数据.
        /// </summary>
        /// <param name="organizationID"></param>
        /// <param name="stockPoolID"></param>
        /// <param name="stockCode"></param>
        /// <returns></returns>
        CommonServiceResult RemoveStockFromPool(long organizationID, long stockPoolID, string stockCode);

    }
}
