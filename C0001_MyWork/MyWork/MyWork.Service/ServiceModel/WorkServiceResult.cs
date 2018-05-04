using System;
using System.Collections.Generic;
using System.Text;

using MyFramework.ServiceModel;



namespace MyWork.ServiceModel
{

    /// <summary>
    /// 工作模块的执行结果.
    /// </summary>
    [Serializable]
    public class WorkServiceResult : CommonServiceResult
    {


        #region 股票池相关.


        /// <summary>
        /// 股票池名称已存在.
        /// </summary>
        public const string ResultCodeIsStockPoolNameHadExists = "WORK_STOCK_POOL_NAME_HAD_EXISTS";

        /// <summary>
        /// 股票池名称已存在的执行结果.
        /// </summary>
        public static readonly CommonServiceResult StockPoolNameHadExistsResult = new CommonServiceResult()
        {
            ResultCode = ResultCodeIsStockPoolNameHadExists,
            ResultMessage = "股票池名称已存在",
        };



        /// <summary>
        /// 股票池ID不存在.
        /// </summary>
        public const string ResultCodeIsStockPoolIDNotFound = "WORK_STOCK_POOL_ID_NOT_FOUND";

        /// <summary>
        /// 股票池ID不存在的执行结果.
        /// </summary>
        public static readonly CommonServiceResult StockPoolIDNotFoundResult = new CommonServiceResult()
        {
            ResultCode = ResultCodeIsStockPoolIDNotFound,
            ResultMessage = "股票池ID不存在",
        };




        /// <summary>
        /// 股票代码已存在.
        /// </summary>
        public const string ResultCodeIsStockCodeHadExists = "WORK_STOCK_CODE_HAD_EXISTS";

        /// <summary>
        /// 股票代码已存在的执行结果.
        /// </summary>
        public static readonly CommonServiceResult StockCodeHadExistsResult = new CommonServiceResult()
        {
            ResultCode = ResultCodeIsStockCodeHadExists,
            ResultMessage = "股票代码已存在",
        };

        /// <summary>
        /// 股票代码不存在.
        /// </summary>
        public const string ResultCodeIsStockCodeNotFound = "WORK_STOCK_CODE_NOT_FOUND";

        /// <summary>
        /// 股票代码不存在的执行结果.
        /// </summary>
        public static readonly CommonServiceResult StockCodeNotFoundResult = new CommonServiceResult()
        {
            ResultCode = ResultCodeIsStockCodeNotFound,
            ResultMessage = "股票代码不存在",
        };


        #endregion



    }
}
