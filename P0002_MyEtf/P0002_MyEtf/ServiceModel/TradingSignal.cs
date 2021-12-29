using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P0002_MyEtf.ServiceModel
{

    /// <summary>
    /// 交易信号.
    /// </summary>
    public enum TradingSignal
    {
        /// <summary>
        /// 啥信号也没有.
        /// </summary>
        None = 0,

        /// <summary>
        /// 买入信号.
        /// </summary>
        Buy = 1,

        /// <summary>
        /// 卖出信号.
        /// </summary>
        Sell = 2,

    }
}
