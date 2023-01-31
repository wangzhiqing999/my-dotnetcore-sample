using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace P0002_MyGrid.Model
{

    /// <summary>
    /// 网格成交.
    /// </summary>
    [Table("grid_transaction", Schema = "my_grid")]
    public class GridTransaction
    {

        /// <summary>
        /// 网格成交流水号
        /// </summary>
        [DataMember]
        [Column("transaction_id")]
        [Display(Name = "网格成交流水号")]
        [Key]
        public long TransactionID { set; get; }






        /// <summary>
        /// 项目代码
        /// </summary>
        [DataMember]
        [Column("item_code")]
        [Display(Name = "项目代码")]
        [StringLength(16)]
        [Required]
        public string ItemCode { set; get; }


        /// <summary>
        /// 网格代码
        /// </summary>
        [DataMember]
        [Column("grid_no")]
        [Display(Name = "网格代码")]
        public int GridNo { set; get; }



        /// <summary>
        /// 交易日期.
        /// </summary>
        [DataMember]
        [Column("transaction_date")]
        [Display(Name = "交易日期")]
        public DateTime TransactionDate { set; get; }



        /// <summary>
        /// 交易价格.
        /// </summary>
        [DataMember]
        [Column("transaction_price")]
        [Display(Name = "交易价格")]
        public decimal TransactionPrice { set; get; }



        /// <summary>
        /// 交易数量.
        /// <br/>
        /// 正数为买入，负数为卖出.
        /// </summary>
        [DataMember]
        [Column("transaction_quantity")]
        [Display(Name = "交易数量")]
        public int TransactionQuantity { set; get; }



    }
}
