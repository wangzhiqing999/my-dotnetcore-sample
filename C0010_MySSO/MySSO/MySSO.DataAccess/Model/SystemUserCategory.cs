using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MySSO.Model
{

    /// <summary>
    /// 用户分类.
    /// </summary>
    [Table("sso_user_category")]
    public class SystemUserCategory
    {

        /// <summary>
        /// 系统用户类型代码.
        /// </summary>
        [Column("user_category_code")]
        [Display(Name = "系统用户类型代码")]
        [StringLength(32)]
        [Key]
        public string UserCategoryCode { set; get; }




        /// <summary>
        /// 系统用户类型名称.
        /// </summary>
        [Column("user_category_name")]
        [Display(Name = "系统用户类型名称")]
        [StringLength(32)]
        public string UserCategoryName { set; get; }



        #region 一对多 用户.


        /// <summary>
        /// 分类下用户列表.
        /// </summary>
        public List<SystemUser> SystemUserList { set; get; }

        #endregion 一对多.



    }
}
