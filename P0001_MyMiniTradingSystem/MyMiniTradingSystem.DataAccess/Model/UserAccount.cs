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
    /// 用户帐户.
    /// </summary>
    [Serializable]
    [DataContract(Namespace = "")]
    [Table("mts_user_account")]
    public class UserAccount
    {


        /// <summary>
        /// 用户代码.
        /// </summary>
        [DataMember]
        [Column("user_code")]
        [Display(Name = "用户代码")]
        [StringLength(32)]
        [Key]
        public string UserCode { set; get; }



        /// <summary>
        /// 用户名.
        /// </summary>
        [DataMember]
        [Column("user_name")]
        [Display(Name = "用户名")]
        [StringLength(32)]
        [Required]
        public string UserName { set; get; }





        #region 一对多关系  (仓位.)


        /// <summary>
        /// 仓位
        /// </summary>
        public virtual List<Position> Positions { set; get; }


        #endregion 一对多关系  (仓位.)







        #region 一对多关系  (每日总结.)


        /// <summary>
        /// 仓位
        /// </summary>
        public virtual List<DailySummary> DailySummarys { set; get; }


        #endregion 一对多关系  (每日总结.)

    }


}
