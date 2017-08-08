using System.Threading.Tasks;
using Abp.Application.Services;
using W1001_ABP_With_Zero.Sessions.Dto;

namespace W1001_ABP_With_Zero.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
