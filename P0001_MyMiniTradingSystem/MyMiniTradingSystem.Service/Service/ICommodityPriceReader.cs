using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MyMiniTradingSystem.Model;




namespace MyMiniTradingSystem.Service
{



    /// <summary>
    /// 行情数据读取服务.
    /// </summary>
    public interface ICommodityPriceReader
    {
        /// <summary>
        /// 读取产品的行情信息.
        /// </summary>
        /// <param name="stockCode"></param>
        /// <param name="startDate"></param>
        /// <param name="finishDate"></param>
        /// <returns></returns>
        List<CommodityPrice> GetCommodityPriceList(string stockCode, DateTime startDate, DateTime finishDate);
    }
}
