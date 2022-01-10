using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using P0002_MyTrading.ServiceModel;

namespace P0002_MyTrading.Service
{

    /// <summary>
    /// 简单交易服务.
    /// </summary>
    public interface ISimpleTradingService
    {

        /// <summary>
        /// 建仓.
        /// </summary>
        /// <param name="tradingRequest"></param>
        /// <returns></returns>
        ServiceResult DoOpen(TradingRequest tradingRequest);


        /// <summary>
        /// 平仓.
        /// </summary>
        /// <param name="tradingRequest"></param>
        /// <returns></returns>
        ServiceResult DoClose(TradingRequest tradingRequest);



        /// <summary>
        /// 是否有持仓.
        /// </summary>
        /// <param name="tradingItemCode"></param>
        /// <returns></returns>
        bool HasPosition(string tradingItemCode);



    }
}
