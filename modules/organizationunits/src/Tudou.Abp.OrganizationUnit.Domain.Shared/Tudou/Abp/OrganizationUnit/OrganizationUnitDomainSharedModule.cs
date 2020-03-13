using Volo.Abp.Modularity;
using Volo.Abp.Localization;
using Tudou.Abp.OrganizationUnit.Localization;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.Validation;
using Volo.Abp.Validation.Localization;
using Volo.Abp.VirtualFileSystem;

namespace Tudou.Abp.OrganizationUnit
{
    [DependsOn(
        typeof(AbpValidationModule)
    )]
    public class OrganizationUnitDomainSharedModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<OrganizationUnitDomainSharedModule>("Tudou.Abp.OrganizationUnit");
            });

            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<OrganizationUnitResource>("zh-Hans")
                    .AddBaseTypes(typeof(AbpValidationResource))
                    .AddVirtualJson("/Tudou/Abp/OrganizationUnit/Localization");
            });

            Configure<AbpExceptionLocalizationOptions>(options =>
            {
                options.MapCodeNamespace("Tudou.Abp.OrganizationUnit", typeof(OrganizationUnitResource));
            });
        }
    }
}
