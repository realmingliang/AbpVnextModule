using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Tudou.Abp.Saas.EntityFrameworkCore
{
    [ConnectionStringName(AbpSaasDbProperties.ConnectionStringName)]
    public interface ISaasDbContext : IEfCoreDbContext
    {
        DbSet<SaasTenant> SaasTenants { get; set; }

        DbSet<SaasEdition> SaasEditions { get; set; }

        DbSet<SaasTenantConnectionString> SaasTenantConnectionStrings { get; set; }
    }
}