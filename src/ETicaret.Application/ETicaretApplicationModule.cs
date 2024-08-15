using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using ETicaret.Authorization;

namespace ETicaret
{
    [DependsOn(
        typeof(ETicaretCoreModule),
        typeof(AbpAutoMapperModule))]
    public class ETicaretApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<ETicaretAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(ETicaretApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            // Mevcut assembly'de AutoMapper profillerini yükle
            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                cfg => cfg.AddMaps(thisAssembly)
            );
        }
    }
}