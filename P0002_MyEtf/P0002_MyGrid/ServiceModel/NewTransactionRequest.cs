using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.Serialization;


namespace P0002_MyGrid.ServiceModel
{

    /// <summary>
    /// 新交易的请求.
    /// </summary>
    public class NewTransactionRequest
    {

        /// <summary>
        /// 项目代码
        /// </summary>
        [DataMember]
        public string ItemCode { set; get; }


        /// <summary>
        /// 网格代码
        /// </summary>
        [DataMember]
        public int GridNo { set; get; }



        /// <summary>
        /// 交易日期.
        /// </summary>
        [DataMember]
        public DateTime TransactionDate { set; get; }



        /// <summary>
        /// 交易价格.
        /// </summary>
        [DataMember]
        public decimal TransactionPrice { set; get; }



        /// <summary>
        /// 交易数量.
        /// <br/>
        /// 正数为买入，负数为卖出.
        /// </summary>
        [DataMember]
        public int TransactionQuantity { set; get; }
    }
}
