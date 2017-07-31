using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;



namespace W1000_ABP_HelloWorld.SystemConfig
{
    /// <summary>
    /// 系统配置属性.
    /// </summary>
    [Table("sc_system_config_property")]
    public class SystemConfigProperty : Entity<long>
    {


        #region 一对多.

        /// <summary>
        /// 系统配置类型代码
        /// </summary>
        
        [Column("config_type_code")]
        [Display(Name = "系统配置类型代码")]
        [StringLength(32)]
        [Required]
        public string ConfigTypeCode { set; get; }


        /// <summary>
        /// 系统配置类型
        /// </summary>
        [ForeignKey(nameof(ConfigTypeCode))]
        public virtual SystemConfigType SystemConfigTypeData { set; get; }


        #endregion 一对多.



        /// <summary>
        /// 系统配置属性名称
        /// </summary>        
        [Column("property_name")]
        [Display(Name = "系统配置属性名称")]
        [StringLength(64)]
        public string PropertyName { set; get; }



        /// <summary>
        /// 系统配置属性数据类型
        /// </summary>        
        [Column("property_datatype")]
        [Display(Name = "系统配置属性数据类型")]
        [StringLength(64)]
        public string PropertyDataType { set; get; }



        /// <summary>
        /// 系统配置属性描述
        /// </summary>        
        [Column("property_desc")]
        [Display(Name = "系统配置属性描述")]
        [StringLength(64)]
        public string PropertyDesc { set; get; }



        /// <summary>
        /// 显示顺序.
        /// </summary>        
        [Column("display_order")]
        [Display(Name = "显示顺序")]
        public int DisplayOrder { set; get; }


    }


}
