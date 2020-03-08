using Tudou.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Tudou.Abp.AuditLogging
{
    [DependsOn(
        typeof(AbpAuditLoggingEntityFrameworkCoreTestModule)
        )]
    public class AbpAuditLoggingTestModule : AbpModule
    {

    }
}
