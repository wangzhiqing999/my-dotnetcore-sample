using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P0002_MyTrading.ServiceModel
{

    /// <summary>
    /// 商品价格数据.
    /// </summary>
    public class ItemPriceData
    {

        /// <summary>
        /// 日期.
        /// </summary>
        public string Date { set; get; }


        /// <summary>
        /// 价格.
        /// </summary>
        public string Price { set; get; }




        public override string ToString()
        {
            return $"{this.Date:yyyy-MM-dd} {this.Price}";
        }
    }
}
