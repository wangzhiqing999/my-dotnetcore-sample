using System;
using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;

namespace W1010_ABP_NetCode2.Tasks.Dtos
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