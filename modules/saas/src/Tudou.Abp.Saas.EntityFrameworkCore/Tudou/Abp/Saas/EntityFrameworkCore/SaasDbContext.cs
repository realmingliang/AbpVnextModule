using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Tudou.Abp.Saas.EntityFrameworkCore
{
    [ConnectionStringName(AbpSaasDbProperties.ConnectionStringName)]
    public class SaasDbContext : AbpDbContext<SaasDbContext>, ISaasDbContext
    {
        public DbSet<SaasTenant> SaasTenants { get; set; }
        public DbSet<SaasEdition> SaasEditions { get; set; }
        public DbSet<SaasTenantConnectionString> SaasTenantConnectionStrings { get; set; }

        public SaasDbContext(DbContextOptions<SaasDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ConfigureSaasManagement();
        }
    }
}
