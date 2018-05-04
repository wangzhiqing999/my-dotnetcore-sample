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
    /// 股票池
    /// </summary>
    [EnableCors("AllowCors")]
    [Produces("application/json")]
    [Area("MyWork")]
    [Authorize]
    public class StockPoolController : TokenAbleController
    {
        /// <summary>
        /// 股票池服务.
        /// </summary>
        private IStockPoolService _StockPoolService;


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="stockPoolService"></param>
        public StockPoolController(IStockPoolService stockPoolService)
        {
            // 股票池服务
            this._StockPoolService = stockPoolService;
        }



        /// <summary>
        /// 查询股票池列表
        /// </summary>
        /// <param name="pageNo">第几页</param>
        /// <param name="pageSize">每页几行</param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/MyWork/StockPool")]
        public CommonQueryResult<StockPool> Query(int pageNo = 1, int pageSize = 10)
        {
            // 获取当前登录者的 组织ID.
            BasicUserInfo userInfo = GetUserInfoFromToken();
            if(userInfo == null)
            {
                // 无 Token 信息的情况下，返回空白列表.
                return CommonQueryResult<StockPool>.GetEmptyResult();
            }

            var result = this._StockPoolService.GetStockPoolList(userInfo.OrganizationID, pageNo, pageSize);
            return result;
        }


        /// <summary>
        /// 获取股票池.
        /// </summary>
        /// <param name="id">股票池代码</param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/MyWork/StockPool/Get/{id}")]
        public CommonServiceResult Get(long id)
        {
            // 获取当前登录者的 组织ID.
            BasicUserInfo userInfo = GetUserInfoFromToken();
            if (userInfo == null)
            {
                // 无 Token 信息的情况下，返回空白.
                return CommonServiceResult.DataNotFoundResult;
            }

            var result = this._StockPoolService.GetStockPool(userInfo.OrganizationID, id);
            return result;
        }



        /// <summary>
        /// 新增股票池.
        /// </summary>
        /// <param name="data">股票池数据</param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/MyWork/StockPool/Create")]
        [WithCreater]
        [WithOrganization]
        public CommonServiceResult Create([FromBody]StockPool data)
        {
            var result = this._StockPoolService.CreateStockPool(data);
            return result;
        }


        /// <summary>
        /// 更新股票池.
        /// </summary>
        /// <param name="data">股票池数据</param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/MyWork/StockPool/Update")]
        [WithLastUpdater]
        [WithOrganization]
        public CommonServiceResult Update([FromBody]StockPool data)
        {
            var result = this._StockPoolService.UpdateStockPool(data);
            return result;
        }



        /// <summary>
        /// 删除股票池.
        /// </summary>
        /// <param name="data">股票池代码</param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/MyWork/StockPool/Delete")]
        public CommonServiceResult Delete([FromBody]RemoveRequest data)
        {
            // 获取当前登录者的 组织ID.
            BasicUserInfo userInfo = GetUserInfoFromToken();
            if (userInfo == null)
            {
                // 无 Token 信息的情况下，返回空白.
                return CommonServiceResult.DataNotFoundResult;
            }

            long id = Convert.ToInt64(data.id);
            var result = this._StockPoolService.DeleteStockPool(userInfo.OrganizationID, id);
            return result;
        }







        /// <summary>
        /// 查询股票池股票列表
        /// </summary>
        /// <param name="id">股票池代码</param>
        /// <param name="pageNo">第几页</param>
        /// <param name="pageSize">每页几行</param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/MyWork/StockPool/QueryStock/{id}")]
        public CommonQueryResult<StockInfo> QueryStock(long id, int pageNo = 1, int pageSize = 10)
        {
            // 获取当前登录者的 组织ID.
            BasicUserInfo userInfo = GetUserInfoFromToken();
            if (userInfo == null)
            {
                // 无 Token 信息的情况下，返回空白列表.
                return CommonQueryResult<StockInfo>.GetEmptyResult();
            }

            var result = this._StockPoolService.GetStockInfoInPool(userInfo.OrganizationID, id, pageNo, pageSize);
            return result;
        }



        /// <summary>
        /// 向股票池中添加股票数据
        /// </summary>
        /// <param name="id"></param>
        /// <param name="stockCode"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/MyWork/StockPool/AddStockToPool/{id}")]
        public CommonServiceResult AddStockToPool(long id, [FromBody]string stockCode)
        {
            // 获取当前登录者的 组织ID.
            BasicUserInfo userInfo = GetUserInfoFromToken();
            if (userInfo == null)
            {
                // 无 Token 信息的情况下，返回空白.
                return CommonServiceResult.DataNotFoundResult;
            }

            var result = this._StockPoolService.AddStockToPool(userInfo.OrganizationID, id, stockCode);
            return result;
        }



        /// <summary>
        /// 从股票池中移除股票数据
        /// </summary>
        /// <param name="id"></param>
        /// <param name="stockCode"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/MyWork/StockPool/RemoveStockFromPool/{id}")]
        public CommonServiceResult RemoveStockFromPool(long id, [FromBody]string stockCode)
        {
            // 获取当前登录者的 组织ID.
            BasicUserInfo userInfo = GetUserInfoFromToken();
            if (userInfo == null)
            {
                // 无 Token 信息的情况下，返回空白.
                return CommonServiceResult.DataNotFoundResult;
            }

            var result = this._StockPoolService.RemoveStockFromPool(userInfo.OrganizationID, id, stockCode);
            return result;
        }


    }
}