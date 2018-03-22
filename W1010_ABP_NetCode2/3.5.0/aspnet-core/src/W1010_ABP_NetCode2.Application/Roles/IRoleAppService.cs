using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using W1010_ABP_NetCode2.Roles.Dto;

namespace W1010_ABP_NetCode2.Roles
{
    public interface IRoleAppService : IAsyncCrudAppService<RoleDto, int, PagedResultRequestDto, CreateRoleDto, RoleDto>
    {
        Task<ListResultDto<PermissionDto>> GetAllPermissions();
    }
}
