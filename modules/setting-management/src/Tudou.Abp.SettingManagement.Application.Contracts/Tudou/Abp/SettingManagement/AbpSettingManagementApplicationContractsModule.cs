using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application;
using Volo.Abp.Authorization;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;

namespace Tudou.Abp.SettingManagement
{
    [DependsOn(
             typeof(AbpAuthorizationModule),
             typeof(AbpDddApplicationModule),
             typeof(AbpPermissionManagementApplicationContractsModule)
             )]
    public class AbpSettingManagementApplicationContractsModule:AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {

        }
    }
}
