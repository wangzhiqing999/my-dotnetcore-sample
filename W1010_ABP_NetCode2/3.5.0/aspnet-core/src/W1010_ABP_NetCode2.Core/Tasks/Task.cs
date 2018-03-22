using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;


namespace W1010_ABP_NetCode2.Tasks
{


    [Table("AppTasks")]
    public class Task : Entity, IHasCreationTime, IMustHaveTenant
    {
        public const int MaxTitleLength = 256;
        public const int MaxDescriptionLength = 64 * 1024; //64KB


        /// <summary>
        /// 任务标题.
        /// </summary>
        [Required]
        [MaxLength(MaxTitleLength)]
        public string Title { get; set; }


        /// <summary>
        /// 任务描述.
        /// </summary>
        [MaxLength(MaxDescriptionLength)]
        public string Description { get; set; }


        /// <summary>
        /// 创建时间.
        /// </summary>
        public DateTime CreationTime { get; set; }


        /// <summary>
        /// 任务状态.
        /// </summary>
        public TaskState State { get; set; }




        public Task()
        {
            CreationTime = Clock.Now;
            State = TaskState.Open;
        }

        public Task(string title, string description = null)
            : this()
        {
            Title = title;
            Description = description;
        }







        #region IMustHaveTenant 接口的实现.


        /// <summary>
        /// 租户的ID.
        /// </summary>
        public int TenantId { set; get; }


        #endregion


    }
}
