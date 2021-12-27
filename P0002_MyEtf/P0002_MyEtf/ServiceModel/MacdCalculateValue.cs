using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P0002_MyEtf.ServiceModel
{

    /// <summary>
    /// MACD 计算的中间值.
    /// </summary>
    internal class MacdCalculateValue
    {

        public DateTime TradingDate { set; get; }



        public decimal EmaFast { set; get; }



        public decimal EmaSlow { set; get; }




    }
}
