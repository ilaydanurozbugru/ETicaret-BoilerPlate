using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using ETicaret.Configuration.Dto;

namespace ETicaret.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : ETicaretAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
