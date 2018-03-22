using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using W1010_ABP_NetCode2.Configuration.Dto;

namespace W1010_ABP_NetCode2.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : W1010_ABP_NetCode2AppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
