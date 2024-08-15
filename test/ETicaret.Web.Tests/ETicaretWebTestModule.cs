using Abp.AspNetCore;
using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using ETicaret.EntityFrameworkCore;
using ETicaret.Web.Startup;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace ETicaret.Web.Tests
{
    [DependsOn(
        typeof(ETicaretWebMvcModule),
        typeof(AbpAspNetCoreTestBaseModule)
    )]
    public class ETicaretWebTestModule : AbpModule
    {
        public ETicaretWebTestModule(ETicaretEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbContextRegistration = true;
        } 
        
        public override void PreInitialize()
        {
            Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(ETicaretWebTestModule).GetAssembly());
        }
        
        public override void PostInitialize()
        {
            IocManager.Resolve<ApplicationPartManager>()
                .AddApplicationPartsIfNotAddedBefore(typeof(ETicaretWebMvcModule).Assembly);
        }
    }
}