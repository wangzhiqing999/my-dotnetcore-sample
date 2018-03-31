using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Newtonsoft.Json;


namespace MyAuthentication.Model
{

    /// <summary>
    /// 功能模块
    /// (此类不需要管理的，因此不继承 CommonData 类)
    /// </summary>
    [Serializable]
    [Table("my_module")]
    public class MyModule
    {


        /// <summary>
        /// 模块编号
        /// </summary>
        [Column("module_code")]
        [Key]
        [Display(Name = "模块编号")]
        [StringLength(32)]
        [Required]
        public string ModuleCode { set; get; }



        /// <summary>
        /// 模块名称
        /// </summary>
        [Column("module_name")]
        [Display(Name = "模块名称")]
        [StringLength(32)]
        [Required]
        public string ModuleName { set; get; }



        /// <summary>
        /// 模块附加信息
        /// </summary>
        [Column("module_expand")]
        [Display(Name = "模块附加信息")]
        [StringLength(256)]
        public string ModuleExpand { set; get; }





        #region 一对多的部分.  (与模块类型)


        /// <summary>
        /// 模块类型代码
        /// </summary>
        [Column("module_type_code")]
        [Display(Name = "模块类型代码")]
        [StringLength(32)]
        [Required]
        public string ModuleTypeCode { set; get; }


        /// <summary>
        /// 模块类型.
        /// </summary>
        [JsonIgnore]
        public MyModuleType ModuleType { set; get; }



        #endregion 一对多的部分.




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
        /// 模块所属的系统.
        /// </summary>
        [JsonIgnore]
        public MySystem System { set; get; }


        #endregion 一对多的部分.






        #region 一对多的部分.  (与动作)


        /// <summary>
        /// 模块下的 动作列表.
        /// </summary>
        [JsonIgnore]
        public virtual List<MyAction> Actions { set; get; }



        #endregion



        #region 一对多的部分.  (角色 - 功能模块关系)

        /// <summary>
        /// 角色 - 功能模块关系
        /// </summary>
        [JsonIgnore]
        public virtual List<MyRoleModule> RoleModules { set; get; }


        #endregion


    }
}
