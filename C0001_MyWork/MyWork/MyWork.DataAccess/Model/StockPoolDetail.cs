using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MyWork.Model
{
    /// <summary>
    /// 股票池 -- 股票  多对多 中间表.
    /// </summary>
    [Serializable]
    [Table("mw_stock_pool_detail")]
    public class StockPoolDetail
    {

        #region 一对多的部分.  (与股票池)

        /// <summary>
        /// 股票池流水号
        /// </summary>
        [Column("stock_pool_id")]
        [Display(Name = "股票池流水号")]
        public long StockPoolID { set; get; }




        /// <summary>
        /// 股票池.
        /// </summary>        
        public StockPool Pool { set; get; }


        #endregion




        #region 一对多的部分.  (与股票)

        /// <summary>
        /// 股票代码
        /// </summary>
        [Column("stock_code")]
        [Display(Name = "股票代码")]
        [StringLength(16)]
        [Required]
        public string StockCode { set; get; }



        /// <summary>
        /// 股票
        /// </summary>        
        public StockInfo Stock { set; get; }


        #endregion

    }

}
