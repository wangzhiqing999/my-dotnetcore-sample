using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Newtonsoft.Json;

namespace MyAuthentication.Model
{

    /// <summary>
    /// 模块动作
    /// (此类不需要管理的，因此不继承 CommonData 类)
    /// </summary>
    [Serializable]
    [Table("my_action")]
    public class MyAction
    {
        /// <summary>
        /// 动作代码
        /// </summary>
        [Column("action_code")]
        [Key]
        [Display(Name = "动作代码")]
        [StringLength(32)]
        [Required]
        public string ActionCode { set; get; }


        /// <summary>
        /// 动作名称
        /// </summary>
        [Column("action_name")]
        [Display(Name = "动作名称")]
        [StringLength(32)]
        [Required]
        public string ActionName { set; get; }



        /// <summary>
        /// 动作附加信息
        /// </summary>
        [Column("action_expand")]
        [Required]
        [Display(Name = "动作附加信息")]
        [StringLength(256)]
        public string ActionExpand { set; get; }



        /// <summary>
        /// 默认可用标志
        /// </summary>
        [Column("default_useable")]
        [Display(Name = "默认可用标志")]
        public bool DefaultUseable { set; get; }





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
        [JsonIgnore]
        public virtual MyModule Module { set; get; }



        #endregion 一对多的部分.





        #region 一对多的部分.  (角色 - 模块动作关系)

        /// <summary>
        /// 角色 - 功能模块关系
        /// </summary>
        [JsonIgnore]
        public virtual List<MyRoleAction> RoleActions { set; get; }


        #endregion



    }
}
