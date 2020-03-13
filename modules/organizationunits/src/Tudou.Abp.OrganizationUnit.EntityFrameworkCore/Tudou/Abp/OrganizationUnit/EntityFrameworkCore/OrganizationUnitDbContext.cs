using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Tudou.Abp.OrganizationUnit.EntityFrameworkCore
{
    [ConnectionStringName(OrganizationUnitDbProperties.ConnectionStringName)]
    public class OrganizationUnitDbContext : AbpDbContext<OrganizationUnitDbContext>, IOrganizationUnitDbContext
    {
        public DbSet<OrganizationUnitRole> OrganizationUnitRoles { get; set; }
        public DbSet<OrganizationUnitUser> OrganizationUnitUsers { get; set; }
        public DbSet<OrganizationUnit> OrganizationUnits { get; set; }

        public OrganizationUnitDbContext(DbContextOptions<OrganizationUnitDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ConfigureOrganizationUnit();
        }
    }
}