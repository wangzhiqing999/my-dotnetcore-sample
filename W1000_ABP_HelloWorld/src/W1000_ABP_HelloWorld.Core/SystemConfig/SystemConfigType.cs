using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;



namespace W1000_ABP_HelloWorld.SystemConfig
{

    /// <summary>
    /// 系统配置类型.
    /// </summary>
    [Table("sc_system_config_type")]    
    public class SystemConfigType : Entity<string>
    {



        /// <summary>
        /// 系统配置类型代码
        /// </summary>        
        [Column("config_type_code")]
        [Display(Name = "系统配置类型代码")]
        [StringLength(32)]
        [Required]
        public override string Id { set; get; }




        /// <summary>
        /// 系统配置类型名称
        /// </summary>        
        [Column("config_type_name")]
        [Display(Name = "系统配置类型名称")]
        [StringLength(64)]
        public string ConfigTypeName { set; get; }




        /// <summary>
        /// 配置的类名.
        /// </summary>
        [Column("config_type_class_name")]
        [Display(Name = "配置的类名")]
        [StringLength(128)]
        public string ConfigClassName { set; get; }



        /// <summary>
        /// 简单的 Key-Value 关系. Key 为 String， Value 为 Object.
        /// </summary>
        public const string SimpleDictionary = "System.Collections.Generic.Dictionary`2[System.String,System.Object]";






        #region 一对多.


        /// <summary>
        /// 系统配置属性列表.
        /// </summary>
        public virtual List<SystemConfigProperty> SystemConfigPropertyList { set; get; }



        /// <summary>
        /// 系统配置值列表.
        /// </summary>
        public virtual List<SystemConfigValue> SystemConfigValueList { set; get; }


        #endregion 一对多.



    }


}
