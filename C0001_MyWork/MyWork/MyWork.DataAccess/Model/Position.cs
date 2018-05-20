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
    /// 持仓.
    /// </summary>
    [Serializable]
    [Table("mw_position")]
    public class Position : CommonData
    {


        /// <summary>
        /// 获取主键.
        /// </summary>
        [NotMapped]
        public override dynamic PrimaryKey
        {
            get
            {
                return this.PositionID;
            }
        }



        /// <summary>
        /// 仓位流水.
        /// </summary>
        [Column("position_id")]
        [Display(Name = "仓位流水")]
        [Key]
        public long PositionID { set; get; }




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





        ///// <summary>
        ///// 做多/做空
        ///// </summary>
        //[Column("is_long")]
        //[Display(Name = "做多/做空")]
        //public bool IsLong { set; get; }




        /// <summary>
        /// 数量
        /// </summary>
        [Column("quantity")]
        [Display(Name = "数量")]
        public int Quantity { set; get; }



        /// <summary>
        /// 成本。
        /// </summary>
        [Column("cost")]
        [Display(Name = "成本")]
        public decimal Cost { set; get; }

    }

}
