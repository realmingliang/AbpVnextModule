using Volo.Abp.Auditing;
using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace Tudou.Abp.AuditLogging
{
    [DependsOn(typeof(AbpAuditingModule))]
    [DependsOn(typeof(AbpDddDomainModule))]
    [DependsOn(typeof(AbpAuditLoggingDomainSharedModule))]
    public class AbpAuditLoggingDomainModule : AbpModule
    {

    }
}
