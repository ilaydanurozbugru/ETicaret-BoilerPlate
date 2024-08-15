using System.Threading.Tasks;
using Abp.Application.Services;
using ETicaret.Authorization.Accounts.Dto;

namespace ETicaret.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
