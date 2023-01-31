using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace P0002_MyTrading.ServiceModel
{

    /// <summary>
    /// 持仓报告.
    /// </summary>
    public class HoldingReport
    {

        /// <summary>
        /// 商品代码
        /// </summary>
        [DataMember]
        [Display(Name = "商品代码")]
        public string ItemCode { set; get; }





        /// <summary>
        /// 商品名称
        /// </summary>
        [DataMember]
        [Display(Name = "商品名称")]
        public string ItemName { set; get; }



        /// <summary>
        /// 读取器名称
        /// </summary>
        [DataMember]
        [Display(Name = "读取器名称")]
        public string ReaderName { set; get; }



        /// <summary>
        /// 日期
        /// </summary>
        [DataMember]
        [Display(Name = "日期")]
        public DateTime LogDate { set; get; }




        /// <summary>
        /// 持仓数量.
        /// </summary>
        [DataMember]
        [Display(Name = "持仓数量")]
        public int Quantity { get; set; }




        /// <summary>
        /// 价格
        /// </summary>
        [DataMember]
        [Display(Name = "价格")]
        public decimal Price { set; get; }




        /// <summary>
        /// 金额.
        /// </summary>
        [DataMember]
        [Display(Name = "金额")]
        public decimal Amount
        {
            get
            {
                return Price * Quantity;
            }

            set
            {
                // DoNothing.
            }
        }
    }
}
