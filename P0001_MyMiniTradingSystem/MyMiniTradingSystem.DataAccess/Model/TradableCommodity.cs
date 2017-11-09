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
    /// 交易商品.
    /// </summary>
    [Serializable]
    [DataContract(Namespace = "")]
    [Table("mts_commodity")]
    public class TradableCommodity
    {


        /// <summary>
        /// 商品代码.
        /// </summary>
        [DataMember]
        [Column("commodity_code")]
        [Display(Name = "商品代码")]
        [StringLength(32)]
        [Key]
        public string CommodityCode { set; get; }



        /// <summary>
        /// 商品名称.
        /// </summary>
        [DataMember]
        [Column("commodity_name")]
        [Display(Name = "商品名称")]
        [StringLength(32)]
        [Required]
        public string CommodityName { set; get; }






        /// <summary>
        /// 一手的数量
        /// </summary>
        [DataMember]
        [Column("num_of_one_hand")]
        [Display(Name = "一手的数量")]
        public int NumOfOneHand { set; get; }



        /// <summary>
        /// 保证金比例(百分之N).
        /// </summary>
        [DataMember]
        [Column("deposit_ratio")]
        [Display(Name = "保证金比例(百分之N)")]
        [Range(1, 100)]
        [Required]
        public int DepositRatio { set; get; }



        ///// <summary>
        ///// 手续费比例(万分之N)
        ///// </summary>
        //[DataMember]
        //[Column("fees_ratio")]
        //[Display(Name = "手续费比例(万分之N)")]
        //[Range(1, 100)]
        //[Required]
        //public decimal FeesRatio { set; get; }





        #region 一对多 （日线 ）


        /// <summary>
        /// 日线数据.
        /// </summary>
        public virtual List<CommodityPrice> CommodityPrices { set; get; }



        /// <summary>
        /// 每日总结数据.
        /// </summary>
        public virtual List<DailySummary> DailySummarys { set; get; }


        #endregion 一对多 （日线 ）





        #region 一对多关系  (仓位.)


        /// <summary>
        /// 仓位
        /// </summary>
        public virtual List<Position> Positions { set; get; }


        #endregion 一对多关系  (仓位.)


    }


}
