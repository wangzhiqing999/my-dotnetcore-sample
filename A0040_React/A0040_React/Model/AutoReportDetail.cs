using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace A0040_React.Model
{


    /// <summary>
    /// 自动报表明细.
    /// </summary>
    [Table("auot_report_detail")]
    public class AutoReportDetail
    {
        /// <summary>
        /// 自动报表明细ID.
        /// </summary>
        [Column("auto_report_detail_id")]
        [Display(Name = "自动报表明细ID")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long AutoReportDetailID { set; get; }



        #region 一对多 (自动报表)

        /// <summary>
        /// 自动报表ID.
        /// </summary>
        [Column("auto_report_master_id")]
        [Display(Name = "自动报表ID")]
        public long AutoReportMasterID { set; get; }


        /// <summary>
        /// 自动报表数据.
        /// </summary>
        public virtual AutoReportMaster AutoReportMasterData { set; get; }

        #endregion




        #region 一对多 (报表)


        /// <summary>
        /// 报表代码.
        /// </summary>
        [Column("report_id")]
        [Display(Name = "报表代码")]
        public int ReportID { set; get; }



        /// <summary>
        /// 报表数据.
        /// </summary>
        public virtual Report ReportData { set; get; }



        #endregion



        /// <summary>
        /// 报表参数
        /// </summary>
        [Column("report_parameter")]
        [Display(Name = "报表参数")]
        [StringLength(512)]
        public string ReportParameter { set; get; }



        #region 一对多(文件)

        /// <summary>
        /// 自动报表明细文件列表
        /// </summary>
        public virtual List<AutoReportDetailFile> AutoReportDetailFileList { set; get; }

        #endregion


    }
}
