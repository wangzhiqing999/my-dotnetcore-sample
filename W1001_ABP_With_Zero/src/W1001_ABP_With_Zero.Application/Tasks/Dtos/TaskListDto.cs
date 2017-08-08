﻿using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;

namespace W1001_ABP_With_Zero.Tasks.Dtos
{

    /// <summary>
    /// 任务列表 DTO.
    /// </summary>
    [AutoMapFrom(typeof(Task))]
    public class TaskListDto : EntityDto, IHasCreationTime
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime CreationTime { get; set; }

        public TaskState State { get; set; }
    }
}