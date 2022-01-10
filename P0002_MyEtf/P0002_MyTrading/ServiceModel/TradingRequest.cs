using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P0002_MyTrading.ServiceModel
{

    /// <summary>
    /// 交易请求.
    /// </summary>
    public class TradingRequest
    {

        /// <summary>
        /// 交易项目代码
        /// </summary>
        public string TradingItemCode { set; get; }

        /// <summary>
        /// 交易数量.
        /// </summary>
        public int TradingQuantity { set; get; }

        /// <summary>
        /// 交易日期
        /// </summary>
        public DateTime TradingDate { set; get; }

        /// <summary>
        /// 交易价格.
        /// </summary>
        public decimal TradingPrice { set; get; }



        public override string ToString()
        {
            return $"TradingRequest: Code={this.TradingItemCode}; Date={this.TradingDate:yyyy-MM-dd}; Quantity={this.TradingQuantity}; Price={this.TradingPrice}";
        }


    }
}
