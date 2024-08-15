using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace ETicaret.Controllers
{
    public abstract class ETicaretControllerBase: AbpController
    {
        protected ETicaretControllerBase()
        {
            LocalizationSourceName = ETicaretConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
