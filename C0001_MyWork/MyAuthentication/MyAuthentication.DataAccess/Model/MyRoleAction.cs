using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MyAuthentication.Model
{

    /// <summary>
    /// 角色 - 模块动作 关系.
    /// </summary>
    [Serializable]
    [Table("my_role_action")]
    public class MyRoleAction
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






        #region 一对多的部分.  (与模块动作)

        /// <summary>
        /// 动作代码
        /// </summary>
        [Column("action_code")]
        [Display(Name = "动作代码")]
        [StringLength(32)]
        [Required]
        public string ActionCode { set; get; }



        /// <summary>
        /// 动作
        /// </summary>        
        public MyAction Action { set; get; }


        #endregion
    }
}
