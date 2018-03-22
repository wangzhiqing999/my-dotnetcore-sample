using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using W1010_ABP_NetCode2.MultiTenancy;

namespace W1010_ABP_NetCode2.Sessions.Dto
{
    [AutoMapFrom(typeof(Tenant))]
    public class TenantLoginInfoDto : EntityDto
    {
        public string TenancyName { get; set; }

        public string Name { get; set; }
    }
}
