using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using P0002_MyEtf.Model;
using P0002_MyEtf.ServiceModel;

namespace P0002_MyEtf.Service
{

    /// <summary>
    /// ETF周线数据服务.
    /// </summary>
    public interface IEtfWeekService
    {

        /// <summary>
        /// 计算 ETF周线数据.
        /// </summary>
        /// <param name="etfCode"></param>
        /// <param name="tradingDate"></param>
        /// <returns></returns>
        ServiceResult CalculateEtfWeekLine(string etfCode, DateTime tradingDate);





        /// <summary>
        /// 获取 MACD周线数据.
        /// <br/>
        /// 区别于 MACD日线数据.
        /// MACD日线数据，是使用 C# 先计算 EMA，后计算 MACD，然后写入到本地表当中的。
        /// MACD周线数据，是使用编写 Postgres 函数的方式，计算出EMA与MACD的，数据没有存储在表里。
        /// </summary>
        /// <param name="etfCode"></param>
        /// <returns></returns>
        List<EtfWeekMacd> GetEtfWeekMacd(string etfCode);

    }
}
