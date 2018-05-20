using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Newtonsoft.Json;

using MyFramework.Model;

using MyAuthentication.IModel;


namespace MyWork.Model
{

    /// <summary>
    /// 账户每日报表.
    /// </summary>
    [Serializable]
    [Table("mw_account_daily_report")]
    public class AccountDailyReport
    {

        /// <summary>
        /// 报表流水.
        /// </summary>
        [Column("report_id")]
        [Display(Name = "报表流水")]
        [Key]
        public long ReportID { set; get; }






        #region 一对多关系  (发生操作的账户.)


        /// <summary>
        /// 账户ID.
        /// </summary>
        [Column("account_id")]
        [Display(Name = "账户ID")]
        public long AccountID { set; get; }



        /// <summary>
        /// 发生操作的账户.
        /// </summary>
        public virtual Account AccountData { set; get; }



        #endregion 一对多关系  (发生操作的账户.)







        /// <summary>
        /// 报表日期
        /// </summary>
        [Column("report_date")]
        [Display(Name = "报表日期")]
        public DateTime ReportDate { set; get; }





        /// <summary>
        /// 期初金额.
        /// </summary>
        [Column("beginning_money")]
        [Display(Name = "期初金额")]
        public decimal BeginningMoney { set; get; }



        /// <summary>
        /// 期末金额.
        /// </summary>
        [Column("ending_money")]
        [Display(Name = "期末金额")]
        public decimal EndingMoney { set; get; }




        /// <summary>
        /// 交易笔数.
        /// </summary>
        [Column("deal_count")]
        [Display(Name = "交易笔数")]
        public int DealCount { set; get; }



        /// <summary>
        /// 金额变化[冗余校验数据].
        /// </summary>
        [Column("money_change")]
        [Display(Name = "金额变化[冗余校验数据]")]
        public decimal MoneyChange { set; get; }






        /// <summary>
        /// 持仓市值
        /// </summary>
        [Column("position_value")]
        [Display(Name = "持仓市值")]
        public decimal PositionValue { set; get; }




        /// <summary>
        /// 总市值.
        /// </summary>
        public decimal TotalValue
        {
            get
            {
                // 返回： 期末金额 + 持仓市值.
                return this.EndingMoney + this.PositionValue;
            }
        }


    }
}
