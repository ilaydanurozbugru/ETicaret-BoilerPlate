using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using ETicaret.Configuration;

namespace ETicaret.Web.Host.Startup
{
    [DependsOn(
       typeof(ETicaretWebCoreModule))]
    public class ETicaretWebHostModule: AbpModule
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public ETicaretWebHostModule(IWebHostEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(ETicaretWebHostModule).GetAssembly());
        }
    }
}
