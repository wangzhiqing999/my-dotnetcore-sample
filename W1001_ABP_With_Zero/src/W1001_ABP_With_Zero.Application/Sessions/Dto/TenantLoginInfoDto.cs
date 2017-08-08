using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using W1001_ABP_With_Zero.MultiTenancy;

namespace W1001_ABP_With_Zero.Sessions.Dto
{
    [AutoMapFrom(typeof(Tenant))]
    public class TenantLoginInfoDto : EntityDto
    {
        public string TenancyName { get; set; }

        public string Name { get; set; }
    }
}