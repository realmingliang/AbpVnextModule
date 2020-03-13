using Localization.Resources.AbpUi;
using Tudou.Abp.OrganizationUnit.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace Tudou.Abp.OrganizationUnit
{
    [DependsOn(
        typeof(OrganizationUnitApplicationContractsModule),
        typeof(AbpAspNetCoreMvcModule))]
    public class OrganizationUnitHttpApiModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(OrganizationUnitHttpApiModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Get<OrganizationUnitResource>()
                    .AddBaseTypes(typeof(AbpUiResource));
            });
        }
    }
}
