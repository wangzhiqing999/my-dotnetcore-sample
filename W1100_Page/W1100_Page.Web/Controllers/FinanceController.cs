using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using W1100_Page.Service;
using W1100_Page.ServiceModel;


namespace W1100_Page.Web.Controllers
{
    public class FinanceController : Controller
    {

        /// <summary>
        /// 财经数据服务.
        /// </summary>
        private IFinanceDataService financeDataService;


        public FinanceController(IFinanceDataService financeDataService)
        {
            this.financeDataService = financeDataService;
        }


        /// <summary>
        /// 目录页.
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }


        /// <summary>
        /// 更多 的方式翻页.
        /// </summary>
        /// <returns></returns>
        public IActionResult More()
        {
            return View();
        }


        /// <summary>
        /// 滚动条 的方式翻页.
        /// </summary>
        /// <returns></returns>
        public IActionResult Scroll()
        {
            return View();
        }


        /// <summary>
        /// 标准翻页 的方式.
        /// </summary>
        /// <returns></returns>
        public IActionResult Page()
        {
            return View();
        }



        /// <summary>
        /// 获取数据的接口.
        /// </summary>
        /// <param name="pageNo"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IActionResult GetFinanceData(int pageNo = 1, int pageSize = 5)
        {
            GetFinanceDataInput input = new GetFinanceDataInput()
            {
                PageNo = pageNo,
                PageSize = pageSize
            };
            FinanceDataListOutput output = this.financeDataService.GetFinanceDataList(input);
            return Json(output);
        }

    }
}