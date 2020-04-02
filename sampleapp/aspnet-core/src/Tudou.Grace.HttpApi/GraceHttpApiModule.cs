using Tudou.Abp.AuditLogging;
using Tudou.Abp.SettingManagement;
using Tudou.Abp.Account;
using Volo.Abp.FeatureManagement;
using Tudou.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement.HttpApi;
using Tudou.Abp.Saas;


namespace Tudou.Grace
{
    [DependsOn(
        typeof(GraceApplicationContractsModule),
        typeof(AbpAccountHttpApiModule),
        typeof(AbpIdentityHttpApiModule),
        typeof(AbpAuditLoggingHttpApiModule),
        typeof(AbpPermissionManagementHttpApiModule),
        typeof(AbpSaasHttpApiModule),
        typeof(AbpSettingManagementHttpApiModule),
        typeof(AbpFeatureManagementHttpApiModule)
        )]
    public class GraceHttpApiModule : AbpModule
    {
        
    }
}
