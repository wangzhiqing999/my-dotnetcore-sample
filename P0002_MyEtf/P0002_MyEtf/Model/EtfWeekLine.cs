using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.Serialization;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace P0002_MyEtf.Model
{

    /// <summary>
    /// ETF周线.
    /// </summary>
    [Table("etf_week_line", Schema = "my_etf")]
    public class EtfWeekLine
    {


        #region 一对多.


        /// <summary>
        /// ETF代码
        /// </summary>
        [DataMember]
        [Column("etf_code")]
        [Display(Name = "ETF代码")]
        [StringLength(16)]
        [Required]
        public string EtfCode { set; get; }


        /// <summary>
        /// ETF主数据.
        /// </summary>
        public EtfMaster EtfMasterData { set; get; }


        #endregion



        /// <summary>
        /// 交易日期
        /// </summary>
        [DataMember]
        [Column("trading_date")]
        [Display(Name = "交易日期")]
        [Required]
        public DateTime TradingDate { set; get; }



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




    }
}
