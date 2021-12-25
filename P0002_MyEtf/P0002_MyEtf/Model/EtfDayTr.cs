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
    /// ETF 日波幅.
    /// </summary>
    [Table("etf_day_tr", Schema = "my_etf")]
    public class EtfDayTr
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
        /// 波幅
        /// </summary>
        [DataMember]
        [Column("tr")]
        [Display(Name = "波幅")]
        public decimal Tr { set; get; }



        /// <summary>
        /// 真实波幅
        /// </summary>
        [DataMember]
        [Column("atr")]
        [Display(Name = "真实波幅")]
        public decimal Atr { set; get; }



    }
}
