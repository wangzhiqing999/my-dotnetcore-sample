using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MySSO.Model
{

    /// <summary>
    /// 用户.
    /// </summary>
    [Table("sso_user")]
    public class SystemUser
    {

        /// <summary>
        /// 系统用户 ID.  (C# 端生成， 不在数据库段生成)
        /// </summary>
        [Column("user_id")]
        [Display(Name = "系统用户 ID")]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid UserID { set; get; }


        #region 一对多.

        /// <summary>
        /// 系统用户类型代码.
        /// </summary>
        [Column("user_category_code")]
        [Display(Name = "系统用户类型代码")]
        [StringLength(32)]
        public string UserCategoryCode { set; get; }


        /// <summary>
        /// 系统用户类型
        /// </summary>
        public SystemUserCategory UserCategory { set; get; }


        /// <summary>
        /// 用户类型
        /// </summary>
        [Display(Name = "用户类型")]
        [NotMapped]
        public string DisplayUserCategoryName
        {
            get
            {
                if (String.IsNullOrEmpty(this.UserCategoryCode))
                {
                    return "-";
                }

                return this.UserCategory.UserCategoryName;

            }
        }



        #endregion  一对多.


        /// <summary>
        /// 用户名
        /// </summary>
        [Column("user_name")]
        [Display(Name = "用户名")]
        [StringLength(32)]
        public string UserName { set; get; }


        /// <summary>
        /// 用户密码
        /// </summary>
        [Column("user_password")]
        [Display(Name = "用户密码")]
        [StringLength(128)]
        public string UserPassword { set; get; }





        #region  一对多


        /// <summary>
        /// 用户Token.
        /// </summary>
        public List<SystemUserToken> SystemUserTokenList { set; get; }



        #endregion


    }
}
