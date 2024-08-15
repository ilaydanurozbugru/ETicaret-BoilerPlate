using Abp.AspNetCore.Mvc.Views;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Mvc.Razor.Internal;

namespace ETicaret.Web.Views
{
    public abstract class ETicaretRazorPage<TModel> : AbpRazorPage<TModel>
    {
        [RazorInject]
        public IAbpSession AbpSession { get; set; }

        protected ETicaretRazorPage()
        {
            LocalizationSourceName = ETicaretConsts.LocalizationSourceName;
        }
    }
}
