using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using W1001_ABP_With_Zero.Configuration.Dto;

namespace W1001_ABP_With_Zero.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : W1001_ABP_With_ZeroAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
