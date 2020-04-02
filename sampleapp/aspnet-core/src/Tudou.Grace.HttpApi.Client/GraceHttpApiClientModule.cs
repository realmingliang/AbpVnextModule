using Microsoft.Extensions.DependencyInjection;
using Tudou.Abp.AuditLogging;
using Tudou.Abp.SettingManagement;
using Tudou.Abp.Account;
using Volo.Abp.FeatureManagement;
using Tudou.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Tudou.Abp.Saas;

namespace Tudou.Grace
{
    [DependsOn(
        typeof(GraceApplicationContractsModule),
        typeof(AbpAccountHttpApiClientModule),
        typeof(AbpIdentityHttpApiClientModule),
        typeof(AbpPermissionManagementHttpApiClientModule),
        typeof(AbpAuditLoggingHttpApiClientModule),
        typeof(AbpSaasHttpApiClientModule),
        typeof(AbpSettingManagementHttpApiClientModule),
        typeof(AbpFeatureManagementHttpApiClientModule)
    )]
    public class GraceHttpApiClientModule : AbpModule
    {
        public const string RemoteServiceName = "Default";

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClientProxies(
                typeof(GraceApplicationContractsModule).Assembly,
                RemoteServiceName
            );
        }
    }
}
