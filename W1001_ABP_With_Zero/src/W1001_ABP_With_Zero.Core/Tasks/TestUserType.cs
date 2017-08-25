using System;
using System.Collections.Generic;
using System.Text;


using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;



namespace W1001_ABP_With_Zero.Tasks
{


    /// <summary>
    /// 测试用户类型.
    /// 
    /// 这个类的主键为 string 类型。
    /// 非自动生成， 插入时，需要自己指定数值。
    /// </summary>
    [Table("AppTestUserType")]
    public class TestUserType : Entity<string>, IPassivable
    {


        /// <summary>
        /// 最大名称长度.
        /// </summary>
        public const int MaxNameLength = 64;


        /// <summary>
        /// 名称
        /// </summary>
        [Column("user_type_name")]
        [StringLength(MaxNameLength)]
        public virtual string Name { get; set; }






        #region  IPassivable 接口的实现.


        /// <summary>
        /// 状态.
        /// </summary>
        [Column("is_active")]
        public virtual bool IsActive { set; get; }



        #endregion



    }
}
