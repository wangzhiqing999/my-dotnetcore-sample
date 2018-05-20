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
    /// 交易.
    /// </summary>
    [Serializable]
    [Table("mw_trading")]
    public class Trading
    {

        /// <summary>
        /// 交易流水.
        /// </summary>
        [Column("trading_id")]
        [Display(Name = "交易流水")]
        [Key]
        public long TradingID { set; get; }





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
        /// 交易日期时间.
        /// </summary>
        [Column("trading_datetime")]
        [Display(Name = "交易日期时间")]
        public DateTime TradingDateTime { set; get; }



        /// <summary>
        /// 单价
        /// </summary>
        [Column("unit_price")]
        [Display(Name = "单价")]
        public decimal UnitPrice { set; get; }



        /// <summary>
        /// 数量
        /// </summary>
        [Column("quantity")]
        [Display(Name = "数量")]
        public int Quantity { set; get; }




        /// <summary>
        /// 交易手续费
        /// </summary>
        [Column("trading_fees")]
        [Display(Name = "交易手续费")]
        public decimal Fees { set; get; }




        /// <summary>
        /// 成本
        /// </summary>
        [NotMapped]
        public decimal Cost
        {
            get
            {
                // 买入时：  成本 = 数量*单价 + 金额.
                // 卖出时：  成本 = 负数的数量*单价 + 金额.
                return this.Quantity * this.UnitPrice + this.Fees;
            }
        }



    }

}
