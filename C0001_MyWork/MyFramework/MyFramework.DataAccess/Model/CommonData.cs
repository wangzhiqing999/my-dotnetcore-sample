using System;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using MyFramework.IModel;

namespace MyFramework.Model
{

    public abstract class CommonData : ILogicRemoveAble, IUpdateLogAble
    {

        /// <summary>
        /// 获取主键.
        /// </summary>
        [NotMapped]
        public abstract dynamic PrimaryKey { get; }



        #region ILogicRemoveAble 实现.

        /// <summary>
        /// 状态
        /// </summary>
        [Column("status")]
        [Display(Name = "状态")]
        [StringLength(16)]
        public string Status { set; get; }


        /// <summary>
        /// 状态是有效的.
        /// </summary>
        public const string STATUS_IS_ACTIVE = "ACTIVE";


        /// <summary>
        /// 状态是无效的.
        /// </summary>
        public const string STATUS_IS_INACTIVE = "INACTIVE";


        /// <summary>
        /// 状态是否有效.
        /// </summary>
        [NotMapped]
        [Display(Name = "数据有效")]
        public virtual bool IsActive
        {
            set
            {
                this.Status = value ? STATUS_IS_ACTIVE : STATUS_IS_INACTIVE;
            }
            get
            {
                return this.Status == STATUS_IS_ACTIVE;
            }
        }

        #endregion



        #region IUpdateLogAble 实现.


        /// <summary>
        /// 创建人
        /// </summary>
        [Column("create_user")]
        [Display(Name = "创建人")]
        [StringLength(32)]
        [Editable(false)]
        [DisplayFormat(ConvertEmptyStringToNull = true, NullDisplayText = "[未设定]")]
        public string CreateUser { set; get; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Column("create_time")]
        [Display(Name = "创建时间")]
        [Editable(false)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}")]
        public DateTime CreateTime { set; get; }




        /// <summary>
        /// 最后更新人
        /// </summary>
        [Column("last_update_user")]
        [Display(Name = "最后更新人")]
        [StringLength(32)]
        [Editable(false)]
        [DisplayFormat(ConvertEmptyStringToNull = true, NullDisplayText = "[未设定]")]
        public string LastUpdateUser { set; get; }

        /// <summary>
        /// 最后更新时间
        /// </summary>
        [Column("last_update_time")]
        [Display(Name = "最后更新时间")]
        [ConcurrencyCheck]
        [Editable(false)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}")]
        public DateTime LastUpdateTime { set; get; }




        /// <summary>
        /// 插入之前的操作.
        /// </summary>
        /// <param name="model"></param>
        public void BeforeInsertOperation(string userName)
        {
            // 插入时间.
            this.CreateTime = DateTime.Now;
            // 最后更新时间.
            this.LastUpdateTime = DateTime.Now;

            // 创建人.
            this.CreateUser = userName;
            // 最后更新人.
            this.LastUpdateUser = userName;
        }



        /// <summary>
        /// 更新之前的操作.
        /// </summary>
        /// <param name="model"></param>
        public void BeforeUpdateOperation(string userName)
        {
            // 最后更新时间.
            this.LastUpdateTime = DateTime.Now;
            // 最后更新人.
            this.LastUpdateUser = userName;
        }



        #endregion


    }
}
