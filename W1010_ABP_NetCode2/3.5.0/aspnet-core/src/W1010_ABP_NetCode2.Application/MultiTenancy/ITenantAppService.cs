using Abp.Application.Services;
using Abp.Application.Services.Dto;
using W1010_ABP_NetCode2.MultiTenancy.Dto;

namespace W1010_ABP_NetCode2.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}
