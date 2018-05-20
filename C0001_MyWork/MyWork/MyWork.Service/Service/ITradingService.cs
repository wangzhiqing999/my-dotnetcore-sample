using System;
using System.Collections.Generic;
using System.Text;

using MyWork.Model;
using MyFramework.ServiceModel;

namespace MyWork.Service
{

    /// <summary>
    /// 交易 服务.
    /// </summary>
    public interface ITradingService
    {

        /// <summary>
        /// 新增交易.
        /// </summary>
        /// <param name="tradingData"></param>
        /// <returns></returns>
        CommonServiceResult NewTrading(Trading tradingData);

    }
}
