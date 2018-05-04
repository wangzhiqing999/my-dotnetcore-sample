using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Newtonsoft.Json;

using MyFramework.Model;

using MyAuthentication.IModel;


namespace MyWork.Model
{
    /// <summary>
    /// 股票池.
    /// </summary>
    [Serializable]
    [Table("mw_stock_pool")]
    public class StockPool : CommonData, IOrganizationManagerAble
    {


        /// <summary>
        /// 获取主键.
        /// </summary>
        [NotMapped]
        public override dynamic PrimaryKey
        {
            get
            {
                return this.StockPoolID;
            }
        }


        /// <summary>
        /// 股票池流水号
        /// </summary>
        [Column("stock_pool_id")]
        [Key]
        [Display(Name = "股票池流水号")]
        public long StockPoolID { set; get; }



        /// <summary>
        /// 组织ID
        /// </summary>
        [Column("organization_id")]
        [Display(Name = "组织ID")]
        public long OrganizationID {
            get;
            set;
        }



        /// <summary>
        /// 股票池名称
        /// </summary>
        [Column("stock_pool_name")]
        [Display(Name = "股票池名称")]
        public string StockPoolName { set; get; }





        #region 一对多的部分.  (股票池-股票关系)

        /// <summary>
        /// 股票池-股票关系
        /// </summary>
        [JsonIgnore]
        public virtual List<StockPoolDetail> StockPoolDetails { set; get; }


        #endregion

    }
}
