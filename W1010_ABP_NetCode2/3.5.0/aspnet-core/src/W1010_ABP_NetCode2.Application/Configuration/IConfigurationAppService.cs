using System.Threading.Tasks;
using W1010_ABP_NetCode2.Configuration.Dto;

namespace W1010_ABP_NetCode2.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
