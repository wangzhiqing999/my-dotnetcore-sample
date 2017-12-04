using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace A0040_React.Model
{




    /// <summary>
    /// 自动报表类型.
    /// </summary>
    public enum CrAutoReportType
    {
        /// <summary>
        /// 每日发送.
        /// </summary>
        Day = 1,

        /// <summary>
        /// 每周发送.
        /// </summary>
        Week = 7,

        /// <summary>
        /// 每月发送.
        /// </summary>
        Month = 30,

        /// <summary>
        /// 每季度发送.
        /// </summary>
        Quarter = 90,

        /// <summary>
        /// 半年发送.
        /// </summary>
        HalfYear = 180,

        /// <summary>
        /// 每年发送.
        /// </summary>
        Year = 365,
    }





    /// <summary>
    /// 自动报表.
    /// </summary>
    [Table("auot_report_master")]
    public class AutoReportMaster
    {
        /// <summary>
        /// 自动报表ID.
        /// </summary>
        [Column("auto_report_master_id")]
        [Display(Name = "自动报表ID")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long AutoReportMasterID { set; get; }


        /// <summary>
        /// 发送报表周期.
        /// </summary>
        [Column("auto_report_type")]
        [Display(Name = "发送报表周期")]
        [Required]
        public CrAutoReportType AutoReportType { set; get; }


        /// <summary>
        /// 用于显示的报表类型.
        /// </summary>
        [NotMapped]
        public string DisplayAutoReportType
        {
            get
            {
                switch (this.AutoReportType)
                {
                    case CrAutoReportType.Day:
                        return "日报表";
                    case CrAutoReportType.Week:
                        return "周报表";
                    case CrAutoReportType.Month:
                        return "月报表";
                    case CrAutoReportType.Quarter:
                        return "季度报表";
                    case CrAutoReportType.HalfYear:
                        return "半年报表";
                    case CrAutoReportType.Year:
                        return "年度报表";
                    default:
                        // 未知格式
                        return "";
                }
            }
        }




        /// <summary>
        /// 周期内几天后处理
        /// </summary>
        [Column("auto_report_days")]
        [Display(Name = "周期内几天后处理")]
        [Range(0, 100)]
        [Required]
        public int AutoReportDays { set; get; }




        /// <summary>
        /// 自动报表处理的  时/分.
        /// </summary>
        [Column("auto_report_hour_min")]
        [Display(Name = "报表处理的时刻")]
        [Range(0, 2400)]
        [Required]
        public int ProcessHourMin { set; get; }




        /// <summary>
        /// 报表收件人.
        /// </summary>
        [Column("auto_report_mail_to")]
        [Display(Name = "报表收件人")]
        [Required]
        [StringLength(512)]
        public string ReportMailTo { set; get; }


        /// <summary>
        /// 报表抄送 (CC).
        /// </summary>
        [Column("auto_report_mail_cc")]
        [Display(Name = "报表抄送 (CC)")]
        [StringLength(512)]
        public string ReportMailCC { set; get; }



        /// <summary>
        /// 邮件主题
        /// </summary>
        [Column("auto_report_mail_subject")]
        [Display(Name = "邮件主题")]
        [Required]
        [StringLength(256)]
        public string MailSubject { set; get; }



        /// <summary>
        /// 邮件正文
        /// </summary>
        [Column("auto_report_mail_body")]
        [Display(Name = "邮件正文")]
        [Required]
        public string MailBody { set; get; }




        #region 一对多.


        /// <summary>
        /// 自动报表明细列表
        /// </summary>
        public virtual List<AutoReportDetail> AutoReportDetailList { set; get; }

        #endregion 一对多.


    }
}
