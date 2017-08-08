using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using W1001_ABP_With_Zero.Roles.Dto;
using W1001_ABP_With_Zero.Users.Dto;

namespace W1001_ABP_With_Zero.Users
{
    public interface IUserAppService : IAsyncCrudAppService<UserDto, long, PagedResultRequestDto, CreateUserDto, UserDto>
    {
        Task<ListResultDto<RoleDto>> GetRoles();
    }
}