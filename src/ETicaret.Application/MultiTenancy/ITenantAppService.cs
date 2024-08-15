using Abp.Application.Services;
using ETicaret.MultiTenancy.Dto;

namespace ETicaret.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

