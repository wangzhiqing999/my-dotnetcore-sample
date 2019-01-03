using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MySSO.Model
{


    /// <summary>
    /// 用户Token.
    /// </summary>
    [Table("sso_user_token")]
    public class SystemUserToken
    {
        /// <summary>
        /// 令牌.  (C# 端生成， 不在数据库段生成)
        /// </summary>
        [Column("token_id")]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "令牌ID")]
        public Guid TokenID { set; get; }





        #region 一对多.  与用户数据.


        /// <summary>
        /// 系统用户 ID.
        /// </summary>
        [Column("user_id")]
        [Display(Name = "系统用户 ID")]
        public Guid UserID { set; get; }


        /// <summary>
        /// 关联用户信息.
        /// </summary>
        public SystemUser SystemUserData { set; get; }


        #endregion





        /// <summary>
        /// 令牌启用时间.
        /// </summary>
        [Column("start_time")]
        [Display(Name = "令牌启用时间")]
        public DateTime StartTime { set; get; }



        /// <summary>
        /// 令牌过期时间
        /// </summary>
        [Column("expired_time")]
        [Display(Name = "令牌过期时间")]
        public DateTime ExpiredTime { set; get; }



        /// <summary>
        /// 令牌是否可用.
        /// </summary>
        [NotMapped]
        public bool IsUseable
        {
            get
            {
                if (DateTime.Now < this.StartTime)
                {
                    // 尚未开始. (理论上不会执行到这里.)
                    return false;
                }

                if (DateTime.Now > this.ExpiredTime)
                {
                    // 已经结束.
                    return false;
                }

                // 能执行到这里， 认为是有效.
                return true;
            }
        }


    }
}
