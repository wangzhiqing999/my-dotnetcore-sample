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
    /// 持仓日志.
    /// </summary>
    [Table("holding_log", Schema = "my_trading")]
    public class HoldingLog
    {

        /// <summary>
        /// 流水号
        /// </summary>
        [DataMember]
        [Column("id")]
        [Display(Name = "流水号")]
        [Key]
        public long ID { set; get; }




        /// <summary>
        /// 商品代码
        /// </summary>
        [DataMember]
        [Column("item_code")]
        [Display(Name = "商品代码")]
        [StringLength(16)]
        [Required]
        public string ItemCode { set; get; }



        /// <summary>
        /// 持仓主数据.
        /// </summary>
        public Holding HoldingData { set; get; }




        /// <summary>
        /// 日期
        /// </summary>
        [DataMember]
        [Column("log_date")]
        [Display(Name = "日期")]
        [Required]
        public DateTime LogDate { set; get; }




        /// <summary>
        /// 持仓数量.
        /// </summary>
        [DataMember]
        [Column("quantity")]
        [Display(Name = "持仓数量")]
        public int Quantity { get; set; }



        /// <summary>
        /// 价格
        /// </summary>
        [DataMember]
        [Column("price")]
        [Display(Name = "价格")]
        [Required]
        public decimal Price { set; get; }



        /// <summary>
        /// 金额.
        /// </summary>
        [NotMapped]
        [DataMember]
        public decimal Amount
        {
            get
            {
                return Price * Quantity;
            }
        }

    }
}
