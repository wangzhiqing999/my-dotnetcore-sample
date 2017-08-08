using System.Threading.Tasks;
using Abp.Application.Services;
using W1001_ABP_With_Zero.Authorization.Accounts.Dto;

namespace W1001_ABP_With_Zero.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
