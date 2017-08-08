using System.Threading.Tasks;
using W1001_ABP_With_Zero.Configuration.Dto;

namespace W1001_ABP_With_Zero.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}