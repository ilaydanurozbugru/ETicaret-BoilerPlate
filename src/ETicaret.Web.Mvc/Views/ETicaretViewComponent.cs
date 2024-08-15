using Abp.AspNetCore.Mvc.ViewComponents;

namespace ETicaret.Web.Views
{
    public abstract class ETicaretViewComponent : AbpViewComponent
    {
        protected ETicaretViewComponent()
        {
            LocalizationSourceName = ETicaretConsts.LocalizationSourceName;
        }
    }
}
