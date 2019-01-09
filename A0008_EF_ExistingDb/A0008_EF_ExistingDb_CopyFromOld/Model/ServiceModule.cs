using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace A0008_EF_ExistingDb_CopyFromOld.Model
{

    /// <summary>
    /// 服务模块.
    /// </summary>
    [Table("mca_service_module")]
    public class ServiceModule
    {

        /// <summary>
        /// 模块代码
        /// </summary>
        [Column("module_code")]
        [Key]
        [Display(Name = "模块代码")]
        [StringLength(32)]
        public string ModuleCode { set; get; }



        /// <summary>
        /// 模块名称
        /// </summary>
        [Column("module_name")]
        [Display(Name = "模块名称")]
        [StringLength(64)]
        [Required]
        public string ModuleName { set; get; }




        #region 一对多.


        /// <summary>
        /// 访问本模块的客户行为.
        /// </summary>
        public List<CustomAction> CustomActionList { set; get; }

        #endregion


    }

}
