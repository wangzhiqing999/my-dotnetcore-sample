using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;


namespace W1000_ABP_HelloWorld.SystemConfig.Dtos
{

    [AutoMapFrom(typeof(SystemConfigType))]
    public class SystemConfigTypeDto : EntityDto<string>
    {

        public string ConfigTypeName { set; get; }


        public string ConfigClassName { set; get; }


    }

}
