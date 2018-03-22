using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using W1010_ABP_NetCode2.Roles.Dto;
using W1010_ABP_NetCode2.Users.Dto;

namespace W1010_ABP_NetCode2.Users
{
    public interface IUserAppService : IAsyncCrudAppService<UserDto, long, PagedResultRequestDto, CreateUserDto, UserDto>
    {
        Task<ListResultDto<RoleDto>> GetRoles();

        Task ChangeLanguage(ChangeUserLanguageDto input);
    }
}
