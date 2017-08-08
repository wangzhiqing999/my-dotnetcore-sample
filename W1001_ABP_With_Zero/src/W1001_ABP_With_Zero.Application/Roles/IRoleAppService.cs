using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using W1001_ABP_With_Zero.Roles.Dto;

namespace W1001_ABP_With_Zero.Roles
{
    public interface IRoleAppService : IAsyncCrudAppService<RoleDto, int, PagedResultRequestDto, CreateRoleDto, RoleDto>
    {
        Task<ListResultDto<PermissionDto>> GetAllPermissions();
    }
}
