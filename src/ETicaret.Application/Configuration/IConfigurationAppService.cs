using System.Threading.Tasks;
using ETicaret.Configuration.Dto;

namespace ETicaret.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
