using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Tudou.Abp.Saas.Localization;
using Volo.Abp.Validation;
using Volo.Abp.Validation.Localization;
using Volo.Abp.VirtualFileSystem;

namespace Tudou.Abp.Saas
{
    [DependsOn(typeof(AbpValidationModule))]
    public class AbpSaasDomainSharedModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<AbpSaasDomainSharedModule>();
            });

            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<AbpSaasResource>("zh-Hans")
                    .AddBaseTypes(
                        typeof(AbpValidationResource)
                    ).AddVirtualJson("/Tudou/Abp/Saas/Localization/Resources");
            });
        }
    }
}
