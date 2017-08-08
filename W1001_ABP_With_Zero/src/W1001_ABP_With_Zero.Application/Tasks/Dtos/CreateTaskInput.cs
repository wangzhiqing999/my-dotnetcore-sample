using System;
using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;

namespace W1001_ABP_With_Zero.Tasks.Dtos
{

    /// <summary>
    /// 创建任务的输入.
    /// </summary>
    [AutoMapTo(typeof(Task))]
    public class CreateTaskInput
    {
        [Required]
        [MaxLength(Task.MaxTitleLength)]
        public string Title { get; set; }

        [MaxLength(Task.MaxDescriptionLength)]
        public string Description { get; set; }
        
    }
}