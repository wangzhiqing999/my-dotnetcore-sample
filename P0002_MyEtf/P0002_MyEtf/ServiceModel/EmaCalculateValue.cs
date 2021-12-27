using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P0002_MyEtf.ServiceModel
{

    /// <summary>
    /// EMA 计算的中间值.
    /// </summary>
    internal class EmaCalculateValue
    {

        public DateTime TradingDate { set; get; }



        public decimal EmaValue { set; get; }

    }
}
