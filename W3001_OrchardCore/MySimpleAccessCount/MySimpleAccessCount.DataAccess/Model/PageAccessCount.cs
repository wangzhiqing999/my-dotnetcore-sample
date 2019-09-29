using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MySimpleAccessCount.Model
{

    /// <summary>
    /// 页面访问计数.
    /// </summary>
    [Serializable]
    [Table("my_page_access_count")]
    public class PageAccessCount
    {
        /// <summary>
        /// 页面代码.
        /// </summary>
        [Key]
        [Column("page_code")]
        [StringLength(16)]
        [Display(Name = "页面代码")]
        [Required]
        public string PageCode { set; get; }


        /// <summary>
        /// 页面名称.
        /// </summary>
        [Column("page_name")]
        [StringLength(64)]
        [Display(Name = "页面名称")]
        [Required]
        public string PageName { set; get; }


        /// <summary>
        /// 初始访问数量.
        /// </summary>
        [Column("init_access_count")]
        [Display(Name = "初始访问数量")]
        public int InitAccessCount { set; get; }


        /// <summary>
        /// 真实访问数量.
        /// </summary>
        [Column("real_access_count")]
        [Display(Name = "真实访问数量")]
        public int RealAccessCount { set; get; }


        /// <summary>
        /// 访问倍数.
        /// </summary>
        [Column("access_multiple")]
        [Display(Name = "访问倍数")]
        [Range(minimum:1, maximum:100)]
        public byte AccessMultiple { set; get; }


        /// <summary>
        /// 是否存储每天的次数.
        /// </summary>
        [Column("is_save_daily_count")]
        [Display(Name = "是否存储每天的次数")]
        public bool IsSaveDailyCount { set; get; }



        /// <summary>
        /// 用于显示的访问次数.
        /// 结果 = 初始访问数量 + （真实访问数量 * 访问倍数）
        /// </summary>
        [Display(Name = "用于显示的访问次数")]
        public long DisplayAccessCount
        {
            get
            {
                return this.InitAccessCount + (this.RealAccessCount * this.AccessMultiple);
            }
        }



        #region 一对多.

        /// <summary>
        /// 页面每日访问计数
        /// </summary>
        public List<PageDailyAccessCount> PageDailyAccessCountList { set; get; }

        #endregion


    }

}
