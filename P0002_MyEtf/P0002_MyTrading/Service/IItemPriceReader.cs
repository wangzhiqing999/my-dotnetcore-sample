using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using P0002_MyTrading.ServiceModel;


namespace P0002_MyTrading.Service
{

    /// <summary>
    /// 读取商品价格.
    /// </summary>
    public interface IItemPriceReader
    {

        /// <summary>
        /// 读取价格.
        /// </summary>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        ServiceResult ReadPrice(string itemCode);

    }

}
