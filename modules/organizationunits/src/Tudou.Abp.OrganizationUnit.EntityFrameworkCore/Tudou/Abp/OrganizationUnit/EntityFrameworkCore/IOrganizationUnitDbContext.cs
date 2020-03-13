using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Tudou.Abp.OrganizationUnit.EntityFrameworkCore
{
    [ConnectionStringName(OrganizationUnitDbProperties.ConnectionStringName)]
    public interface IOrganizationUnitDbContext : IEfCoreDbContext
    {
        DbSet<OrganizationUnitRole> OrganizationUnitRoles { get; }
        DbSet<OrganizationUnitUser> OrganizationUnitUsers { get; }
        DbSet<OrganizationUnit> OrganizationUnits { get; }

    }
}