using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.Serialization;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MyMiniTradingSystem.Model
{
    /// <summary>
    /// 商品行情.
    /// </summary>
    [Serializable]
    [DataContract(Namespace = "")]
    [Table("mts_commodity_price")]
    public class CommodityPrice
    {


        /// <summary>
        /// 商品代码.
        /// </summary>
        [DataMember]
        [Column("commodity_code")]
        [Display(Name = "商品代码")]
        [StringLength(32)]
        public string CommodityCode { set; get; }



        /// <summary>
        /// 交易开始日期
        /// </summary>
        [DataMember]
        [Column("trading_start_date")]
        [Display(Name = "交易开始日期")]
        [Required]
        public DateTime TradingStartDate { set; get; }


        /// <summary>
        /// 交易结束日期
        /// </summary>
        [DataMember]
        [Column("trading_finish_date")]
        [Display(Name = "交易结束日期")]
        [Required]
        public DateTime TradingFinishDate { set; get; }



        /// <summary>
        /// 开盘价
        /// </summary>
        [DataMember]
        [Column("open_price")]
        [Display(Name = "开盘价")]
        [Required]
        public decimal OpenPrice { set; get; }


        /// <summary>
        /// 收盘价
        /// </summary>
        [DataMember]
        [Column("close_price")]
        [Display(Name = "收盘价")]
        [Required]
        public decimal ClosePrice { set; get; }



        /// <summary>
        /// 最高价
        /// </summary>
        [DataMember]
        [Column("highest_price")]
        [Display(Name = "最高价")]
        [Required]
        public decimal HighestPrice { set; get; }


        /// <summary>
        /// 最低价
        /// </summary>
        [DataMember]
        [Column("lowest_price")]
        [Display(Name = "最低价")]
        [Required]
        public decimal LowestPrice { set; get; }




        /// <summary>
        /// 成交量
        /// </summary>
        [DataMember]
        [Column("volume")]
        [Display(Name = "成交量")]
        public long Volume { set; get; }




        /// <summary>
        /// 波幅
        /// </summary>
        [DataMember]
        [Column("tr")]
        [Display(Name = "波幅")]
        public decimal Tr { set; get; }



        /// <summary>
        /// 真实波幅
        /// </summary>
        [DataMember]
        [Column("atr")]
        [Display(Name = "真实波幅")]
        public decimal Atr { set; get; }





        public override string ToString()
        {

            StringBuilder buff = new StringBuilder("商品行情［");

            buff.AppendFormat("商品代码={0};", this.CommodityCode);
            buff.AppendFormat("日期={0}～{1};", this.TradingStartDate, this.TradingFinishDate);

            buff.AppendFormat("开盘={0};", this.OpenPrice);
            buff.AppendFormat("收盘={0};", this.ClosePrice);

            buff.AppendFormat("最高={0};", this.HighestPrice);
            buff.AppendFormat("最低={0};", this.LowestPrice);

            buff.AppendFormat("成交量={0};", this.Volume);

            buff.AppendFormat("真实波幅={0};", this.Atr);

            buff.Append("］");

            return buff.ToString();
        }





        #region 一对多  （产品）


        /// <summary>
        /// 商品.
        /// </summary>
        public virtual TradableCommodity TradableCommodityData { set; get; }


        #endregion 一对多  （产品）


    }


}
