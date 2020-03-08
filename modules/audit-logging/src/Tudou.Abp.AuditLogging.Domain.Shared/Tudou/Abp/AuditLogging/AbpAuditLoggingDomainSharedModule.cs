using Tudou.Abp.AuditLogging.Localization;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;

namespace Tudou.Abp.AuditLogging
{
    public class AbpAuditLoggingDomainSharedModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources.Add<AuditLoggingResource>("en");
            });
        }
    }
}
