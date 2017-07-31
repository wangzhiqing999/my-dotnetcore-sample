using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;


namespace W1000_ABP_HelloWorld.SystemConfig.Dtos
{
    [AutoMapFrom(typeof(SystemConfigValue))]
    public class SystemConfigValueDto : EntityDto<long>
    {

        public string ConfigTypeCode { set; get; }


        public string ConfigCode { set; get; }


        public string ConfigName { set; get; }


        public string ConfigValue { set; get; }

    }
}
