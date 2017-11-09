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
    /// 每日总结.
    /// </summary>
    [Serializable]
    [DataContract(Namespace = "")]
    [Table("mts_daily_summary")]
    public class DailySummary
    {


        /// <summary>
        /// 每日总结流水.
        /// </summary>
        [DataMember]
        [Column("daily_summary_id")]
        [Display(Name = "每日总结流水")]
        [Key]
        public long DailySummaryID { set; get; }




        /// <summary>
        /// 每日总结日期.
        /// </summary>
        [DataMember]
        [Column("daily_summary_date")]
        [Display(Name = "每日总结日期")]
        public DateTime DailySummaryDate { set; get; }




        #region 一对多关系  (持仓商品.)

        /// <summary>
        /// 持仓商品代码.
        /// </summary>
        [DataMember]
        [Column("position_commodity_code")]
        [Display(Name = "持仓商品代码")]
        [StringLength(32)]
        [Required]
        public string PositionCommodityCode { set; get; }



        /// <summary>
        /// 持仓商品.
        /// </summary>
        public virtual TradableCommodity PositionTradableCommodity { set; get; }



        #endregion 一对多关系  (持仓商品.)




        #region 一对多关系  (用户帐户.)

        /// <summary>
        /// 用户代码.
        /// </summary>
        [DataMember]
        [Column("user_code")]
        [Display(Name = "用户代码")]
        [StringLength(32)]
        [Required]
        public string UserCode { set; get; }


        /// <summary>
        /// 用户帐户.
        /// </summary>
        public virtual UserAccount UserAccountData { set; get; }



        #endregion 一对多关系  (用户帐户.)






        /// <summary>
        /// 持仓数量
        /// </summary>
        [DataMember]
        [Column("position_quantity")]
        [Display(Name = "持仓数量")]
        public int PositionQuantity { set; get; }





        /// <summary>
        /// 收盘价
        /// </summary>
        [DataMember]
        [Column("close_price")]
        [Display(Name = "收盘价")]
        public decimal ClosePrice { set; get; }



        /// <summary>
        /// 持仓市值
        /// </summary>
        [DataMember]
        [Column("position_value")]
        [Display(Name = "持仓市值")]
        public decimal PositionValue { set; get; }





        /// <summary>
        /// 止损价
        /// </summary>
        [DataMember]
        [Column("stop_loss_price")]
        [Display(Name = "止损价")]
        public decimal StopLossPrice { set; get; }





        /// <summary>
        /// 待办事宜.
        /// </summary>
        [DataMember]
        [Column("todo")]
        [Display(Name = "待办事宜")]
        [StringLength(64)]
        public string Todo { set; get; }



    }


}
