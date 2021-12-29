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
    /// ETF 周 MACD.
    /// </summary>
    [Table("etf_week_macd", Schema = "my_etf")]
    public class EtfWeekMacd
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
        /// DIFF
        /// <br/>
        /// EMA(12) - EMA(26)
        /// </summary>
        [DataMember]
        [Column("diff")]
        [Display(Name = "DIFF")]
        public decimal Diff { set; get; }


        /// <summary>
        /// DEA
        /// <br/>
        /// 2/(9+1) * 今日DIFF + 8/(9+1) * 昨日DEA
        /// </summary>
        [DataMember]
        [Column("dea")]
        [Display(Name = "DEA")]
        public decimal Dea { set; get; }


        /// <summary>
        /// MACD
        /// <br/>
        /// 2 * (DIFF-DEA)
        /// </summary>
        [DataMember]
        [Column("macd")]
        [Display(Name = "MACD")]
        public decimal Macd { set; get; }




        public override string ToString()
        {
            return $"{this.EtfCode} - {this.TradingDate:yyyy-MM-dd} : Diff={Math.Round(Diff, 2)}; Dea={Math.Round(Dea, 2)}; Macd={Math.Round(Macd, 2)}";
        }


    }
}
