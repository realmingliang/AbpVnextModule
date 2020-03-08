using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Tudou.Abp.AuditLogging.EntityFrameworkCore
{
    [ConnectionStringName(AbpAuditLoggingDbProperties.ConnectionStringName)]
    public interface IAuditLoggingDbContext : IEfCoreDbContext
    {
        DbSet<AuditLog> AuditLogs { get; set; }
    }
}