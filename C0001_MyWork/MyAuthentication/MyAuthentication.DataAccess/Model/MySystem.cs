using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using MyFramework.Model;

namespace MyAuthentication.Model
{
    /// <summary>
    /// 系统.
    /// (此类不需要管理的，因此不继承 CommonData 类)
    /// </summary>
    [Serializable]
    [Table("my_system")]
    public class MySystem
    {

        /// <summary>
        /// 系统代码
        /// </summary>
        [Column("system_code")]
        [Key]
        [Display(Name = "系统代码")]
        [StringLength(32)]
        [Required]
        public string SystemCode { set; get; }


        /// <summary>
        /// 系统名称
        /// </summary>
        [Column("system_name")]
        [Display(Name = "系统名称")]
        [StringLength(64)]
        [Required]
        public string SystemName { set; get; }




        #region 一对多 [角色]

        /// <summary>
        /// 角色.
        /// </summary>
        public virtual List<MyRole> Roles { set; get; }

        #endregion



        #region 一对多 [功能模块]

        /// <summary>
        /// 功能模块.
        /// </summary>
        public virtual List<MyModule> Modules { set; get; }

        #endregion





        #region 一对多 [系统 -- 用户].

        /// <summary>
        /// 系统 -- 用户关系.
        /// </summary>
        public virtual List<MySystemUser> SystemUsers { set; get; }

        #endregion


    }
}
