using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Newtonsoft.Json;

using MyFramework.Model;

using MyAuthentication.IModel;


namespace MyAuthentication.Model
{

    /// <summary>
    /// 用户.
    /// </summary>
    [Serializable]
    [Table("my_user")]
    public class MyUser : CommonData, IOrganizationManagerAble
    {

        /// <summary>
        /// 获取主键.
        /// </summary>
        [NotMapped]
        public override dynamic PrimaryKey
        {
            get
            {
                return this.UserID;
            }
        }


        /// <summary>
        /// 用户ID
        /// </summary>
        [Column("user_id")]
        [Key]
        [Display(Name = "用户ID")]
        public long UserID { set; get; }



        #region 归属的组织.

        /// <summary>
        /// 组织ID
        /// </summary>
        [Column("organization_id")]
        [Display(Name = "组织ID")]
        public long OrganizationID { set; get; }


        /// <summary>
        /// 归属的组织
        /// </summary>
        [JsonIgnore]
        public MyOrganization Organization { set; get; }


        /// <summary>
        /// 组织名称.
        /// </summary>
        [NotMapped]
        public string DisplayOrganizationName
        {
            get
            {
                if(this.Organization == null)
                {
                    return String.Empty;
                }
                return this.Organization.OrganizationName;
            }
        }


        #endregion





        /// <summary>
        /// 用户名
        /// </summary>
        [Column("user_name")]
        [Display(Name = "用户名")]
        [StringLength(32)]
        [Required]
        public string UserName { set; get; }


        /// <summary>
        /// 登录用户代码
        /// </summary>
        [Column("login_user_code")]
        [Display(Name = "登录用户代码")]
        [StringLength(32)]
        [Required]
        public string LoginUserCode { set; get; }

        /// <summary>
        /// 用户密码
        /// </summary>
        [Column("user_password")]
        [Display(Name = "用户密码")]
        [StringLength(512)]
        public string UserPassword { set; get; }








        #region 一对多 [系统 -- 用户].

        /// <summary>
        /// 系统 -- 用户关系.
        /// </summary>
        [JsonIgnore]
        public virtual List<MySystemUser> SystemUsers { set; get; }

        #endregion




        #region 一对多的部分.  (用户-角色关系)

        /// <summary>
        /// 用户 -- 角色关系
        /// </summary>
        [JsonIgnore]
        public virtual List<MyUserRole> UserRoles { set; get; }


        #endregion


    }
}
