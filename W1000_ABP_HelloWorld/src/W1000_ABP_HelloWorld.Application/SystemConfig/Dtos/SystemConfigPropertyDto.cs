using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;




namespace W1000_ABP_HelloWorld.SystemConfig.Dtos
{

    [AutoMapFrom(typeof(SystemConfigProperty))]
    public class SystemConfigPropertyDto : EntityDto<long>
    {


        public string ConfigTypeCode { set; get; }



        public string PropertyName { set; get; }


        public string PropertyDataType { set; get; }


        public string PropertyDesc { set; get; }


        public int DisplayOrder { set; get; }


    }
}
