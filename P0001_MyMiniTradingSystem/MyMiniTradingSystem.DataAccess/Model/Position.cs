using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using System.Runtime.Serialization;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MyMiniTradingSystem.Model
{

    /// <summary>
    /// 仓位.
    /// </summary>
    [Serializable]
    [DataContract(Namespace = "")]
    [Table("mts_position")]        
    public class Position
    {

        /// <summary>
        /// 仓位流水.
        /// </summary>
        [DataMember]
        [Column("position_id")]
        [Display(Name = "仓位流水")]
        [Key]
        public long PositionID { set; get; }




        #region 一对多关系  (用户帐户.)

        /// <summary>
        /// 用户代码.
        /// </summary>
        [DataMember]
        [Column("user_code")]
        [Display(Name = "用户代码")]
        [StringLength(32)]
        [Required]
        public string UserCode { set; get; }


        /// <summary>
        /// 用户帐户.
        /// </summary>
        public virtual UserAccount UserAccountData { set; get; }



        #endregion 一对多关系  (用户帐户.)


        #region 一对多关系  (交易商品.)

        /// <summary>
        /// 商品代码.
        /// </summary>
        [DataMember]
        [Column("commodity_code")]
        [Display(Name = "商品代码")]
        [StringLength(32)]
        [Required]
        public string CommodityCode { set; get; }



        /// <summary>
        /// 交易商品.
        /// </summary>
        public virtual TradableCommodity TradableCommodityData { set; get; }



        #endregion 一对多关系  (交易商品.)



        /// <summary>
        /// 做多/做空
        /// </summary>
        [DataMember]
        [Column("is_long")]
        [Display(Name = "做多/做空")]
        public bool IsLong { set; get; }




        /// <summary>
        /// 数量
        /// </summary>
        [DataMember]
        [Column("quantity")]
        [Display(Name = "数量")]
        public int Quantity { set; get; }


    }


}
