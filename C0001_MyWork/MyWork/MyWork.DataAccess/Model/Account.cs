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
    /// 账户.
    /// </summary>
    [Serializable]
    [Table("mw_account")]
    public class Account : CommonData, IOrganizationManagerAble
    {
        /// <summary>
        /// 获取主键.
        /// </summary>
        [NotMapped]
        public override dynamic PrimaryKey
        {
            get
            {
                return this.AccountID;
            }
        }


        /// <summary>
        /// 账户ID
        /// </summary>
        [Column("account_id")]
        [Key]
        [Display(Name = "账户ID")]
        public long AccountID { set; get; }



        /// <summary>
        /// 组织ID
        /// </summary>
        [Column("organization_id")]
        [Display(Name = "组织ID")]
        public long OrganizationID
        {
            get;
            set;
        }



        /// <summary>
        /// 账户余额
        /// </summary>
        [Column("account_balance")]
        [Display(Name = "账户余额")]
        public decimal AccountBalance { set; get; }



        /// <summary>
        /// 时间戳.
        /// </summary>
        [Timestamp]
        [Column("row_version")]
        public byte[] RowVersion { get; set; }






        #region 一对多 (操作日志)


        /// <summary>
        /// 账户操作日志.
        /// </summary>
        [JsonIgnore]
        public virtual List<AccountOperationLog> OperationLogList { set; get; }


        #endregion 一对多 (操作日志)






        #region 一对多 (日结报表)


        /// <summary>
        /// 账户操作日志.
        /// </summary>
        [JsonIgnore]
        public virtual List<AccountDailyReport> DailyReportList { set; get; }


        #endregion 一对多 (日结报表)





        #region 一对多 (交易 / 持仓)


        /// <summary>
        /// 交易
        /// </summary>
        [JsonIgnore]
        public virtual List<Trading> TradingList { set; get; }


        /// <summary>
        /// 持仓列表
        /// </summary>
        [JsonIgnore]
        public virtual List<Position> PositionList { set; get; }


        #endregion



        #region 一对多 (每日总结)


        /// <summary>
        /// 每日总结
        /// </summary>
        [JsonIgnore]
        public virtual List<DailySummary> DailySummaryList { set; get; }


        #endregion



    }
}
