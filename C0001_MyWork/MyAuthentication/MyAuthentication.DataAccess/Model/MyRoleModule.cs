using System;
using System.Collections.Generic;
using System.Text;


using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MyAuthentication.Model
{

    /// <summary>
    /// 角色 - 功能模块 关系.
    /// </summary>
    [Serializable]
    [Table("my_role_module")]
    public class MyRoleModule
    {


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





        #region 一对多的部分.  (与模块)

        /// <summary>
        /// 模块编号
        /// </summary>
        [Column("module_code")]
        [Display(Name = "模块编号")]
        [StringLength(32)]
        [Required]
        public string ModuleCode { set; get; }



        /// <summary>
        /// 功能模块.
        /// </summary>
        public virtual MyModule Module { set; get; }


        #endregion


    }
}
