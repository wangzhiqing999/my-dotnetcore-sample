using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace P0002_MyTrading.Model
{


    /// <summary>
    /// 持仓.
    /// </summary>
    [Table("holding", Schema = "my_trading")]
    public class Holding
    {


        /// <summary>
        /// 商品代码
        /// </summary>
        [DataMember]
        [Column("item_code")]
        [Display(Name = "商品代码")]
        [StringLength(16)]
        [Key]
        public string ItemCode { set; get; }



        /// <summary>
        /// 商品名称
        /// </summary>
        [DataMember]
        [Column("item_name")]
        [Display(Name = "商品名称")]
        [StringLength(64)]
        public string ItemName { set; get; }



        /// <summary>
        /// 来源名称
        /// </summary>
        [DataMember]
        [Column("source_name")]
        [Display(Name = "来源名称")]
        [StringLength(64)]
        public string SourceName { set; get; }




        /// <summary>
        /// 读取器名称
        /// </summary>
        [DataMember]
        [Column("reader_name")]
        [Display(Name = "读取器名称")]
        [StringLength(128)]
        public string ReaderName { set; get; }



        public List<HoldingLog> HoldingLogList { get; set; }


    }
}
