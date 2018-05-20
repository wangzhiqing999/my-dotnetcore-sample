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

using MyWork.Web.Controllers;


namespace MyWork.Web.Areas.MyWork.Controllers
{


    /// <summary>
    /// 交易.
    /// </summary>
    [EnableCors("AllowCors")]
    [Produces("application/json")]
    [Area("MyWork")]
    [Authorize]
    public class TradingController : TokenAbleController
    {
        /// <summary>
        /// 交易服务.
        /// </summary>
        private ITradingService _TradingService;


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="tradingService"></param>
        public TradingController(ITradingService tradingService)
        {
            // 股票池服务
            this._TradingService = tradingService;
        }




        /// <summary>
        /// 新增交易.
        /// </summary>
        /// <param name="data">交易数据</param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/MyWork/Trading/Create")]
        public CommonServiceResult Create([FromBody]Trading data)
        {
            var result = this._TradingService.NewTrading(data);
            return result;
        }


    }
}