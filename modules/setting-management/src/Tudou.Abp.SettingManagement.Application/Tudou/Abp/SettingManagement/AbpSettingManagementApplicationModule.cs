using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;

namespace Tudou.Abp.SettingManagement
{
    [DependsOn(
         typeof(AbpSettingManagementDomainModule),
        typeof(AbpSettingManagementApplicationContractsModule),
            typeof(AbpPermissionManagementApplicationModule))]
    public class AbpSettingManagementApplicationModule:AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
        }
        
    }
}


