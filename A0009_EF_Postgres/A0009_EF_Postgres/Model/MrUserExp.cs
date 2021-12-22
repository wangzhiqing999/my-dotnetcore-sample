using System;
using System.Collections.Generic;
using System.Text;

using System.Runtime.Serialization;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace A0009_EF_Postgres.Model
{

    /// <summary>
    /// 用户扩展信息
    /// </summary>
    [Serializable]
    [DataContract(Namespace = "")]
    [Table("mr_user_exp")]
    public class MrUserExp
    {


        /// <summary>
        /// 用户代码
        /// </summary>
        [DataMember]
        [Column("user_code")]
        [Key]
        [Display(Name = "用户代码")]
        [StringLength(32)]
        [Required]
        [ForeignKey("User")]
        public string UserCode { set; get; }


        /// <summary>
        /// 用户.
        /// </summary>        
        public virtual MrUser User { set; get; }






        /// <summary>
        /// 用户扩展信息
        /// </summary>
        [DataMember]
        [Column("user_exp_data")]
        [Display(Name = "用户扩展信息")]
        [StringLength(32)]
        public string UserExpData { set; get; }



    }
}
