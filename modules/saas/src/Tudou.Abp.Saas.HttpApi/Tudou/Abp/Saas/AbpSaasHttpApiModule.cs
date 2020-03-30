using Localization.Resources.AbpUi;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.FeatureManagement;
using Volo.Abp.FeatureManagement.Localization;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;
using Tudou.Abp.Saas.Localization;

namespace Tudou.Abp.Saas
{
    [DependsOn(
        typeof(AbpSaasApplicationContractsModule),
        typeof(AbpFeatureManagementHttpApiModule),
        typeof(AbpAspNetCoreMvcModule)
        )]
    public class AbpSaasHttpApiModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
               // mvcBuilder.AddApplicationPartIfNotExists(typeof(AbpTenantManagementHttpApiModule).Assembly);
               mvcBuilder.AddApplicationPart(typeof(AbpSaasHttpApiModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Get<AbpSaasResource>()
                    .AddBaseTypes(
                        typeof(AbpFeatureManagementResource),
                        typeof(AbpUiResource));
            });
        }
    }
}