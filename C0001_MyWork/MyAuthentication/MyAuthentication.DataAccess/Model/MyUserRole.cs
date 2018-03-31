using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyAuthentication.Model
{

    /// <summary>
    /// 用户 -- 角色  多对多 中间表.
    /// </summary>
    [Serializable]
    [Table("my_user_role")]
    public class MyUserRole
    {

        #region 一对多的部分.  (与用户)

        /// <summary>
        /// 用户ID
        /// </summary>
        [Column("user_id")]
        [Display(Name = "用户ID")]
        public long UserID { set; get; }




        /// <summary>
        /// 用户.
        /// </summary>        
        public MyUser User { set; get; }


        #endregion




        #region 一对多的部分.  (与角色)

        /// <summary>
		/// 角色代码
		/// </summary>
        [Column("role_code")]
        [Display(Name = "角色代码")]
        [StringLength(32)]
        [Required]
        public string RoleCode { set; get; }



        /// <summary>
        /// 角色
        /// </summary>        
        public MyRole Role { set; get; }


        #endregion

    }
}
