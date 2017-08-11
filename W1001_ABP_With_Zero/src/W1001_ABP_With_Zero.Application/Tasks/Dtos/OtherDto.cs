using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.MultiTenancy;

namespace W1001_ABP_With_Zero.Tasks.Dtos
{


    /// <summary>
    /// OtherDto
    /// </summary>
    [AutoMapTo(typeof(Other)), AutoMapFrom(typeof(Other))]
    public class OtherDto : EntityDto<Int64>
    {


        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        [StringLength(Other.MaxNameLength)]
        public string Name { get; set; }



        /// <summary>
        /// 有效
        /// </summary>
        public bool IsActive { get; set; }



    }
}
