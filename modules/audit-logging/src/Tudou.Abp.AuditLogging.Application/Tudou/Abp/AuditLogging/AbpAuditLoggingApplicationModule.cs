using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;

namespace Tudou.Abp.AuditLogging
{
    [DependsOn(
           typeof(AbpAutoMapperModule),
           typeof(AbpPermissionManagementApplicationModule)
           )]
    public class AbpAuditLoggingApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<AbpAuditLoggingApplicationModule>();

            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddProfile<AbpAuditLoggingApplicationModuleAutoMapperProfile>(validate: true);
            });
        }

    }
}
