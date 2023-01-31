using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace P0002_MyGrid.Model
{

    /// <summary>
    /// 网格.
    /// </summary>
    [Table("grid", Schema = "my_grid")]
    public class Grid
    {


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
        /// 买入价格
        /// </summary>
        [DataMember]
        [Column("buy_price")]
        [Display(Name = "买入价格")]
        public decimal BuyPrice { set; get; }


        /// <summary>
        /// 卖出价格
        /// </summary>
        [DataMember]
        [Column("sell_price")]
        [Display(Name = "卖出价格")]
        public decimal SellPrice { set; get; }



        /// <summary>
        /// 持仓数量.
        /// </summary>
        [DataMember]
        [Column("hold")]
        [Display(Name = "持仓数量")]
        public int Hold { set; get; }




        public override string ToString()
        {
            return $"ItemCode={ItemCode}; GridNo={GridNo}; Buy={BuyPrice}; Sell={SellPrice}; Hold={Hold}";
        }

    }
}
