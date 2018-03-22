using System.Threading.Tasks;
using Abp.Application.Services;
using W1010_ABP_NetCode2.Sessions.Dto;

namespace W1010_ABP_NetCode2.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
