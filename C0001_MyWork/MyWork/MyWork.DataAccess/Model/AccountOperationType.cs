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
    /// 账户操作类型.
    /// </summary>
    [Serializable]
    [Table("mw_account_operation_type")]
    public class AccountOperationType
    {


        /// <summary>
        /// 操作类型代码.
        /// </summary>
        [Column("operation_type_code")]
        [Display(Name = "操作类型代码")]
        [StringLength(32)]
        [Key]
        public string OperationTypeCode { set; get; }



        /// <summary>
        /// 操作类型名称.
        /// </summary>
        [Column("operation_type_name")]
        [Display(Name = "操作类型名称")]
        [StringLength(32)]
        public string OperationTypeName { set; get; }




        /// <summary>
        /// 金额变化方向.
        /// </summary>
        public enum MoneyChangeDirection
        {
            /// <summary>
            /// 金额增加.
            /// </summary>
            Increase = 1,


            /// <summary>
            /// 金额不变.
            /// </summary>
            Unchanged = 0,


            /// <summary>
            /// 金额减少.
            /// </summary>
            Decrease = -1,
        }



        /// <summary>
        /// 金额变化方向
        /// </summary>
        [Column("change_direction")]
        [Display(Name = "金额变化方向")]
        public MoneyChangeDirection ChangeDirection { set; get; }







        #region 一对多 (操作日志)


        /// <summary>
        /// 账户操作日志.
        /// </summary>
        [JsonIgnore]
        public virtual List<AccountOperationLog> OperationLogList { set; get; }


        #endregion 一对多 (操作日志)


    }
}
