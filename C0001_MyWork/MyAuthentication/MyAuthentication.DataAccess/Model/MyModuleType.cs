using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Newtonsoft.Json;


namespace MyAuthentication.Model
{

    /// <summary>
    /// 模块类型
    /// (此类不需要管理的，因此不继承 CommonData 类)
    /// </summary>
    [Serializable]
    [Table("my_module_type")]
    public class MyModuleType
    {
        /// <summary>
        /// 模块类型代码
        /// </summary>
        [Column("module_type_code")]
        [Key]
        [Display(Name = "模块类型代码")]
        [StringLength(32)]
        [Required]
        public string ModuleTypeCode { set; get; }



        /// <summary>
        /// 模块类型名称
        /// </summary>
        [Column("module_type_name")]
        [Display(Name = "模块类型名称")]
        [StringLength(32)]
        public string ModuleTypeName { set; get; }




        #region 一对多的部分.  (与模块)


        /// <summary>
        /// 模块类型下的 模块列表.
        /// </summary>
        [JsonIgnore]
        public virtual List<MyModule> Modules { set; get; }



        #endregion 一对多的部分.




    }
}
