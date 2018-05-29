using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace MyWork.Service
{



    /// <summary>
    /// 行情数据读取服务.
    /// </summary>
    public interface IPriceReader
    {
        /// <summary>
        /// 获取指定股票的当日收盘价.
        /// </summary>
        /// <param name="stockCode"></param>
        /// <returns></returns>
        Decimal GetClosePrice(string stockCode);
    }
}
