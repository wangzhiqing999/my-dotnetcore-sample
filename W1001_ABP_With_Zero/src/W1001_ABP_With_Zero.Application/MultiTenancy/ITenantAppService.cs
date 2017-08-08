using Abp.Application.Services;
using Abp.Application.Services.Dto;
using W1001_ABP_With_Zero.MultiTenancy.Dto;

namespace W1001_ABP_With_Zero.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}
