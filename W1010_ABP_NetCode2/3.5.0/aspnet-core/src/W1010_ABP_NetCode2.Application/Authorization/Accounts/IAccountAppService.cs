using System.Threading.Tasks;
using Abp.Application.Services;
using W1010_ABP_NetCode2.Authorization.Accounts.Dto;

namespace W1010_ABP_NetCode2.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
