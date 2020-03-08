using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application;
using Volo.Abp.Authorization;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;

namespace Tudou.Abp.AuditLogging
{
    [DependsOn(
           typeof(AbpAuthorizationModule),
           typeof(AbpDddApplicationModule),
           typeof(AbpPermissionManagementApplicationContractsModule)
           )]
    public class AbpAuditLoggingApplicationContractsModule: AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {

        }
    }
}
