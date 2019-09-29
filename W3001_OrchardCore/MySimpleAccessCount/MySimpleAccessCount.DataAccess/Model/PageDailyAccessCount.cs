using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace MySimpleAccessCount.Model
{

    /// <summary>
    /// 页面每日访问计数
    /// </summary>
    [Serializable]
    [Table("my_page_daily_access_count")]
    public class PageDailyAccessCount
    {
        /// <summary>
        /// 页面代码.
        /// </summary>
        [Column("page_code")]
        [StringLength(16)]
        [Display(Name = "页面代码")]
        [Required]
        public string PageCode { set; get; }


        /// <summary>
        /// 访问日期
        /// </summary>
        [Column("access_date")]
        [Display(Name = "访问日期")]
        public DateTime AccessDate { set; get; }


        /// <summary>
        /// 真实访问数量.
        /// </summary>
        [Column("real_access_count")]
        [Display(Name = "真实访问数量")]
        public int RealAccessCount { set; get; }



        #region 一对多


        /// <summary>
        /// 页面访问计数
        /// </summary>
        public PageAccessCount PageAccessCountData { set; get; }

        #endregion


    }
}
