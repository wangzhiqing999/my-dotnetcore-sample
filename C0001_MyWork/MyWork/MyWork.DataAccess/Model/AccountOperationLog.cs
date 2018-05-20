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
    /// 账户操作日志.
    /// </summary>
    [Serializable]
    [Table("mw_account_operation_log")]
    public class AccountOperationLog
    {

        /// <summary>
        /// 日志流水.
        /// </summary>
        [Column("log_id")]
        [Display(Name = "日志流水")]
        [Key]
        public long LogID { set; get; }





        //#region 一对多关系  (账户操作类型.)


        ///// <summary>
        ///// 操作类型代码.
        ///// </summary>
        //[Column("operation_type_code")]
        //[Display(Name = "操作类型代码")]
        //[StringLength(32)]
        //public string OperationTypeCode { set; get; }



        ///// <summary>
        ///// 操作类型.
        ///// </summary>
        //public virtual AccountOperationType OperationType { set; get; }



        //#endregion 一对多关系  (账户操作类型.)






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
        /// 结算日期
        /// </summary>
        [Column("accounting_date")]
        [Display(Name = "结算日期")]
        public DateTime AccountingDate { set; get; }




        /// <summary>
        /// 操作时间
        /// </summary>
        [Column("operation_time")]
        [Display(Name = "操作时间")]
        public DateTime OperationTime { set; get; }




        /// <summary>
        /// 操作金额.
        /// </summary>
        [Column("operation_money")]
        [Display(Name = "操作金额")]
        public decimal OperationMoney { set; get; }




        /// <summary>
        /// 操作描述.
        /// </summary>
        [Column("operation_desc")]
        [Display(Name = "操作描述")]
        public string OperationDesc { set; get; }





        /// <summary>
        /// 操作之前账户余额 [冗余校验数据]
        /// </summary>
        [Column("before_account_balance")]
        [Display(Name = "操作之前账户余额 [冗余校验数据]")]
        public decimal BeforeAccountBalance { set; get; }



        /// <summary>
        /// 操作之后账户余额 [冗余校验数据]
        /// </summary>
        [Column("after_account_balance")]
        [Display(Name = "操作之后账户余额 [冗余校验数据]")]
        public decimal AfterAccountBalance { set; get; }


    }
}
