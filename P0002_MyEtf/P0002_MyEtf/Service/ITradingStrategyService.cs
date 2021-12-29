using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using P0002_MyEtf.ServiceModel;

namespace P0002_MyEtf.Service
{

    /// <summary>
    /// 交易策略服务.
    /// </summary>
    public interface ITradingStrategyService
    {

        /// <summary>
        /// 获取交易信号.
        /// </summary>
        /// <param name="etfCode"></param>
        /// <param name="tradingDate"></param>
        /// <returns></returns>
        TradingSignal GetTradingSignal(string etfCode, DateTime tradingDate);

    }
}
