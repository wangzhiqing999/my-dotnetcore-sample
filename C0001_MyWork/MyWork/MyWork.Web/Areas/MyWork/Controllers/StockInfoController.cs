using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;

using MyFramework.ServiceModel;

using MyAuthentication.ServiceModel;

using MyWork.Web.Filters;

using MyWork.Model;
using MyWork.Service;
using MyWork.ServiceModel;




namespace MyWork.Web.Areas.MyWork.Controllers
{

    /// <summary>
    /// 股票信息.
    /// </summary>
    [EnableCors("AllowCors")]
    [Produces("application/json")]
    [Area("MyWork")]
    public class StockInfoController : Controller
    {

        /// <summary>
        /// 股票服务.
        /// </summary>
        private IStockInfoService _StockInfoService;

        /// <summary>
        /// 构造函数.
        /// </summary>
        /// <param name="stockInfoService"></param>
        public StockInfoController(IStockInfoService stockInfoService)
        {
            this._StockInfoService = stockInfoService;
        }



        /// <summary>
        /// 查询股票信息.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="pageNo"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/MyWork/StockInfo/Search/{text}")]
        public CommonQueryResult<StockInfo> SearchStockInfo(string text, int pageNo = 1, int pageSize = 5)
        {
            var result = this._StockInfoService.SearchStockInfo(text, pageNo, pageSize);
            return result;
        }



        /// <summary>
        /// 获取股票信息.
        /// </summary>
        /// <param name="id">股票代码</param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/MyWork/StockInfo/Get/{id}")]
        public CommonServiceResult Get(string id)
        {
            var result = this._StockInfoService.GetStockInfo(id);
            return result;
        }



    }
}