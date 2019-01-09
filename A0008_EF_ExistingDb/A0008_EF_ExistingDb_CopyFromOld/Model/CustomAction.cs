using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.Serialization;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace A0008_EF_ExistingDb_CopyFromOld.Model
{

    /// <summary>
    /// 客户行为.
    /// </summary>
    [Table("mca_custom_action")]
    public class CustomAction
    {

        /// <summary>
        /// 流水号.
        /// </summary>
        [Column("id")]
        [Display(Name = "流水号")]
        [Key]
        public long ID { set; get; }



        #region 客户相关属性.

        /// <summary>
        /// 客户代码
        /// </summary>
        [Column("custom_id")]
        [Display(Name = "客户代码")]
        public long CustomID { set; get; }


        #endregion



        #region 一对多.


        /// <summary>
        /// 访问模块代码
        /// </summary>
        [Column("module_code")]
        [Display(Name = "访问模块代码")]
        [StringLength(32)]
        [Required]
        public string ModuleCode { set; get; }


        /// <summary>
        /// 访问模块
        /// </summary>
        public ServiceModule AccessServiceModule { set; get; }


        #endregion



        #region 访问时间相关.


        /// <summary>
        /// 进入时间.
        /// </summary>
        [Column("enter_time")]
        [Display(Name = "进入时间")]
        public DateTime EnterTime { set; get; }


        /// <summary>
        /// 最后访问时间.
        /// </summary>
        [Column("last_access_time")]
        [Display(Name = "最后访问时间")]
        public DateTime LastAccessTime { set; get; }


        /// <summary>
        /// 访问次数
        /// </summary>
        [Column("access_count")]
        [Display(Name = "访问次数")]
        public int AccessCount { set; get; }

        
        /// <summary>
        /// 访问分钟.
        /// </summary>
        [NotMapped]
        public int AccessMinutes
        {
            get
            {
                // 说明：
                // 最后访问时间与访问次数是用于 计算 用于在某个模块下的停留时间的处理.
                // 首次访问时，最后访问时间 = 进入时间；访问次数 = 1；
                // 后续访问时，最后访问时间 = 当前时间；访问次数 = 访问次数 + 1；

                var p = this.LastAccessTime - this.EnterTime;
                return Convert.ToInt32(p.TotalMinutes);
            }
        }


        #endregion




        #region 访问数据相关.



        /// <summary>
        /// 扩展数据
        /// </summary>
        [Column("exp_data")]
        [Display(Name = "扩展数据")]
        public string ExpData { set; get; }


        #endregion

    }

}
