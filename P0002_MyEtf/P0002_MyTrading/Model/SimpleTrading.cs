using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P0002_MyTrading.Model
{

    /// <summary>
    /// 简单交易记录.
    /// <br/>
    /// 就是 遇到 买入信号时，买入。
    /// 遇到 卖出信号时，卖出。
    /// 不做 只卖出 一半 之类的操作。
    /// </summary>
    [Table("simple_trading", Schema = "my_trading")]
    public class SimpleTrading
    {


        /// <summary>
        /// 交易流水号
        /// </summary>
        [DataMember]
        [Column("trading_id")]
        [Display(Name = "交易流水号")]
        [Key]
        public long TradingID { set; get; }



        /// <summary>
        /// 交易项目代码
        /// </summary>
        [DataMember]
        [Column("trading_item_code")]
        [Display(Name = "交易项目代码")]
        [StringLength(16)]
        [Required]
        public string TradingItemCode { set; get; }




        /// <summary>
        /// 交易数量.
        /// </summary>
        [DataMember]
        [Column("trading_quantity")]
        [Display(Name = "交易数量")]
        public int TradingQuantity { set; get; }




        /// <summary>
        /// 建仓日期
        /// </summary>
        [DataMember]
        [Column("open_date")]
        [Display(Name = "建仓日期")]
        public DateTime OpenDate { set; get; }


        /// <summary>
        /// 建仓价格.
        /// </summary>
        [DataMember]
        [Column("open_price")]
        [Display(Name = "建仓价格")]
        public decimal OpenPrice { set; get; }






        /// <summary>
        /// 平仓日期
        /// </summary>
        [DataMember]
        [Column("close_date")]
        [Display(Name = "平仓日期")]
        public DateTime? CloseDate { set; get; }


        /// <summary>
        /// 平仓价格.
        /// </summary>
        [DataMember]
        [Column("close_price")]
        [Display(Name = "平仓价格")]
        public decimal? ClosePrice { set; get; }






        /// <summary>
        /// 盈利.
        /// </summary>
        [DataMember]
        public decimal Profit
        {
            get
            {
                if(CloseDate == null || ClosePrice == null)
                {
                    // 尚未平仓.
                    return 0;
                }

                if(ClosePrice < OpenPrice)
                {
                    // 亏损.  无盈利.
                    return 0;
                }

                return (ClosePrice.Value - OpenPrice) * TradingQuantity;
            }
        }


        /// <summary>
        /// 亏损.
        /// </summary>
        [DataMember]
        public decimal Loss
        {
            get
            {
                if (CloseDate == null || ClosePrice == null)
                {
                    // 尚未平仓.
                    return 0;
                }

                if (ClosePrice > OpenPrice)
                {
                    // 盈利.  无亏损.
                    return 0;
                }

                return (OpenPrice - ClosePrice.Value) * TradingQuantity;
            }
        }




    }

}
