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
    /// ETF 日 EMA.
    /// </summary>
    [Table("etf_day_ema", Schema = "my_etf")]
    public class EtfDayEma
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
        /// ema12
        /// </summary>
        [DataMember]
        [Column("ema12")]
        [Display(Name = "ema12")]
        public decimal Ema12 { set; get; }


        /// <summary>
        /// ema26
        /// </summary>
        [DataMember]
        [Column("ema26")]
        [Display(Name = "ema26")]
        public decimal Ema26 { set; get; }




    }
}
