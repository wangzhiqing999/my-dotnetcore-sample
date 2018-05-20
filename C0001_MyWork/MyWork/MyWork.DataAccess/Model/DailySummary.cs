using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Newtonsoft.Json;

using MyFramework.Model;

using MyAuthentication.IModel;


namespace MyWork.Model
{

    /// <summary>
    /// 每日总结.
    /// </summary>
    [Serializable]
    [Table("mw_daily_summary")]
    public class DailySummary
    {

        /// <summary>
        /// 每日总结流水.
        /// </summary>
        [Column("daily_summary_id")]
        [Display(Name = "每日总结流水")]
        [Key]
        public long DailySummaryID { set; get; }


        /// <summary>
        /// 每日总结日期.
        /// </summary>
        [Column("daily_summary_date")]
        [Display(Name = "每日总结日期")]
        public DateTime DailySummaryDate { set; get; }







        #region 一对多关系  (帐户.)

        /// <summary>
        /// 账户ID
        /// </summary>
        [Column("account_id")]
        [Display(Name = "账户ID")]
        public long AccountID { set; get; }



        /// <summary>
        /// 账户.
        /// </summary>
        public Account AccountData { set; get; }



        #endregion






        #region 一对多关系  (股票.)

        /// <summary>
        /// 股票代码
        /// </summary>
        [Column("stock_code")]
        [Display(Name = "股票代码")]
        [StringLength(16)]
        [Required]
        public string StockCode { set; get; }




        /// <summary>
        /// 股票.
        /// </summary>
        public StockInfo StockInfoData { set; get; }



        #endregion





        /// <summary>
        /// 持仓数量
        /// </summary>
        [Column("position_quantity")]
        [Display(Name = "持仓数量")]
        public int PositionQuantity { set; get; }





        /// <summary>
        /// 收盘价
        /// </summary>
        [Column("close_price")]
        [Display(Name = "收盘价")]
        public decimal ClosePrice { set; get; }



        /// <summary>
        /// 持仓市值
        /// </summary>
        [Column("position_value")]
        [Display(Name = "持仓市值")]
        public decimal PositionValue { set; get; }





        /// <summary>
        /// 止损价
        /// </summary>
        [Column("stop_loss_price")]
        [Display(Name = "止损价")]
        public decimal StopLossPrice { set; get; }





        /// <summary>
        /// 待办事宜.
        /// </summary>
        [Column("todo")]
        [Display(Name = "待办事宜")]
        [StringLength(64)]
        public string Todo { set; get; }


    }
}
