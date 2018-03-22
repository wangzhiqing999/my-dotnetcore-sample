using Abp.AutoMapper;
using W1010_ABP_NetCode2.Sessions.Dto;

namespace W1010_ABP_NetCode2.Web.Views.Shared.Components.TenantChange
{
    [AutoMapFrom(typeof(GetCurrentLoginInformationsOutput))]
    public class TenantChangeViewModel
    {
        public TenantLoginInfoDto Tenant { get; set; }
    }
}
