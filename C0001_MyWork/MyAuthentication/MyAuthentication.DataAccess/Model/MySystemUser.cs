using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MyAuthentication.Model
{

    /// <summary>
    /// 系统 -- 用户  多对多 中间表.
    /// </summary>
    [Serializable]
    [Table("my_system_user")]
    public class MySystemUser
    {

        /// <summary>
        /// 系统代码
        /// </summary>
        [Column("system_code")]
        [Display(Name = "系统代码")]
        [StringLength(32)]
        [Required]
        public string SystemCode { set; get; }


        /// <summary>
        /// 系统
        /// </summary>
        public virtual MySystem System { set; get; }






        /// <summary>
        /// 用户ID
        /// </summary>
        [Column("user_id")]
        [Display(Name = "用户ID")]
        public long UserID { set; get; }


        /// <summary>
        /// 用户.
        /// </summary>
        public virtual MyUser User { set; get; }

    }
}
