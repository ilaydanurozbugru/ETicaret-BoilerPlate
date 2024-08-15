using System.Threading.Tasks;
using Abp.Application.Services;
using ETicaret.Sessions.Dto;

namespace ETicaret.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
