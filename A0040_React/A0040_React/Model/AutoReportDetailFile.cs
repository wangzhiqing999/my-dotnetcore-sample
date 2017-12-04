using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace A0040_React.Model
{

    /// <summary>
    /// 报表格式.
    /// </summary>
    public enum ExportFormatType
    {
        WordForWindows = 3,
        Excel = 4,
        PortableDocFormat = 5,
        Text = 9,
        Xml = 13,
    }





    /// <summary>
    /// 自动报表明细文件.
    /// </summary>
    [Table("auot_report_detail_file")]
    public class AutoReportDetailFile
    {


        /// <summary>
        /// 自动报表明细文件ID.
        /// </summary>
        [Column("auto_report_detail_file_id")]
        [Display(Name = "自动报表明细文件ID")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long AutoReportDetailFileID { set; get; }





        #region 一对多 (自动报表)

        /// <summary>
        /// 自动报表明细ID.
        /// </summary>
        [Column("auto_report_detail_id")]
        [Display(Name = "自动报表明细ID")]
        public long AutoReportDetailID { set; get; }


        /// <summary>
        /// 自动报表明细.
        /// </summary>
        public virtual AutoReportDetail AutoReportDetailData { set; get; }


        #endregion





        /// <summary>
        /// 报表格式
        /// </summary>
        [Column("auto_report_format")]
        [Display(Name = "报表格式")]
        [Required]
        public ExportFormatType ReportFileFormat { set; get; }


        /// <summary>
        /// 结果报表文件名.
        /// </summary>
        [Column("auto_report_file_name")]
        [Display(Name = "结果报表文件名")]
        [Required]
        [StringLength(128)]
        public string ReportFileName { set; get; }

    }
}
