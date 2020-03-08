using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Modularity;

namespace Tudou.Abp.AuditLogging
{
    [DependsOn(typeof(AbpAuditLoggingApplicationContractsModule)
        , typeof(AbpAspNetCoreMvcModule))]
    public class AbpAuditLoggingHttpApiModule : AbpModule
    {
    }
}
