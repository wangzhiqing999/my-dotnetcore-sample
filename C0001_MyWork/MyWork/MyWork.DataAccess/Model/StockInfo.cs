using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Newtonsoft.Json;

using MyFramework.Model;

namespace MyWork.Model
{
    /// <summary>
    /// 股票.
    /// </summary>
    [Serializable]
    [Table("mw_stock_info")]
    public class StockInfo
    {
        /// <summary>
        /// 股票代码
        /// </summary>
        [Column("stock_code")]
        [Key]
        [Display(Name = "股票代码")]
        [StringLength(16)]
        [Required]
        public string StockCode { set; get; }


        /// <summary>
        /// 股票名称.
        /// </summary>
        [Column("stock_name")]
        [Display(Name = "股票名称")]
        [StringLength(32)]
        public string StockName { set; get; }

        /// <summary>
        /// 股票名拼音缩写.
        /// </summary>
        [Column("stock_name_pinyin")]
        [Display(Name = "股票名拼音缩写")]
        [StringLength(32)]
        public string StockNamePinyin { set; get; }

        /// <summary>
        /// 所属市场
        /// </summary>
        [Column("market")]
        [Display(Name = "所属市场")]
        [StringLength(32)]
        public string Market { set; get; }





        #region 一对多的部分.  (股票池-股票关系)

        /// <summary>
        /// 股票池-股票关系
        /// </summary>
        [JsonIgnore]
        public virtual List<StockPoolDetail> StockPoolDetails { set; get; }


        #endregion


    }
}
