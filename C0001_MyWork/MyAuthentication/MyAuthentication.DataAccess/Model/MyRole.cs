using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using MyFramework.Model;
using Newtonsoft.Json;

namespace MyAuthentication.Model
{

    /// <summary>
    /// 角色
    /// </summary>
    [Serializable]
    [Table("my_role")]
    public class MyRole : CommonData
    {

        /// <summary>
        /// 获取主键.
        /// </summary>
        [NotMapped]
        public override dynamic PrimaryKey
        {
            get
            {
                return this.RoleCode;
            }
        }


        /// <summary>
        /// 角色代码
        /// </summary>
        [Column("role_code")]
        [Key]
        [Display(Name = "角色代码")]
        [StringLength(32)]
        [Required]
        public string RoleCode { set; get; }



        /// <summary>
        /// 角色名称
        /// </summary>
        [Column("role_name")]
        [Display(Name = "角色名称")]
        [StringLength(32)]
        [Required]
        public string RoleName { set; get; }





        #region 一对多的部分.  (与系统)

        /// <summary>
        /// 系统代码
        /// </summary>
        [Column("system_code")]
        [Display(Name = "系统代码")]
        [StringLength(32)]
        [Required]
        public string SystemCode { set; get; }


        /// <summary>
        /// 角色所属的系统.
        /// </summary>
        [JsonIgnore]
        public MySystem System { set; get; }

        #endregion 一对多的部分.





        #region 一对多的部分.  (用户-角色关系)

        /// <summary>
        /// 用户 -- 角色关系
        /// </summary>
        [JsonIgnore]
        public virtual List<MyUserRole> UserRoles { set; get; }


        #endregion





        #region 一对多的部分.  (角色 - 功能模块关系)

        /// <summary>
        /// 角色 - 功能模块关系
        /// </summary>
        [JsonIgnore]
        public virtual List<MyRoleModule> RoleModules { set; get; }


        #endregion






        #region 一对多的部分.  (角色 - 模块动作关系)

        /// <summary>
        /// 角色 - 功能模块关系
        /// </summary>
        [JsonIgnore]
        public virtual List<MyRoleAction> RoleActions { set; get; }


        #endregion



    }

}
