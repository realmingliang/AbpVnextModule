using Tudou.Abp.Account;
using Volo.Abp.Application;
using Volo.Abp.Authorization;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;

namespace Tudou.Abp.SettingManagement
{
    [DependsOn(
             typeof(AbpAuthorizationModule),
             typeof(AbpDddApplicationModule),
             typeof(AbpPermissionManagementApplicationContractsModule),
             typeof(AbpAccountApplicationContractsModule)
             )]
    public class AbpSettingManagementApplicationContractsModule:AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {

        }
    }
}
