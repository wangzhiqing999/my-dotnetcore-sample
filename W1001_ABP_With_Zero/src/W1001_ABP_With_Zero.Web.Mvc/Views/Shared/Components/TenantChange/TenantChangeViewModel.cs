using Abp.AutoMapper;
using W1001_ABP_With_Zero.Sessions.Dto;

namespace W1001_ABP_With_Zero.Web.Views.Shared.Components.TenantChange
{
    [AutoMapFrom(typeof(GetCurrentLoginInformationsOutput))]
    public class TenantChangeViewModel
    {
        public TenantLoginInfoDto Tenant { get; set; }
    }
}