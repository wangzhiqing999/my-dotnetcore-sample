using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace A0040_React.Model
{

    /// <summary>
    /// 报表.
    /// </summary>
    [Table("cr_report")]
    public class Report
    {

        /// <summary>
        /// 报表代码.
        /// </summary>
        [Column("report_id")]
        [Key]
        [Display(Name = "报表代码")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReportID { set; get; }


        /// <summary>
        /// 报表名
        /// </summary>
        [Column("report_name")]
        [Display(Name = "报表名")]
        [StringLength(64)]
        [Required]
        public string ReportName { set; get; }


        /// <summary>
        /// 报表文件名
        /// </summary>
        [Column("report_file_name")]
        [Display(Name = "报表文件名")]
        [StringLength(64)]
        [Required]
        public string ReportFileName { set; get; }



        #region 一对多.


        /// <summary>
        /// 自动报表明细列表
        /// </summary>
        public virtual List<AutoReportDetail> AutoReportDetailList { set; get; }

        #endregion 一对多.

    }


}
