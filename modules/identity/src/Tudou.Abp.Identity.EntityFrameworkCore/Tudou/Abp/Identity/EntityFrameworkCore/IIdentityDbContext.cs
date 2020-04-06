using Microsoft.EntityFrameworkCore;
using Tudou.Abp.Identity.OrganizationUnits;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Tudou.Abp.Identity.EntityFrameworkCore
{
    [ConnectionStringName(AbpIdentityDbProperties.ConnectionStringName)]
    public interface IIdentityDbContext : IEfCoreDbContext
    {
        DbSet<IdentityUser> Users { get; set; }

        DbSet<IdentityRole> Roles { get; set; }
        DbSet<IdentityUserOrganizationUnit> IdentityUserOrganizationUnits { get; set; }
        DbSet<OrganizationUnit> OrganizationUnits { get; set; }

        DbSet<OrganizationUnitRole> OrganizationUnitRoles { get; set; }
        DbSet<IdentityClaimType> ClaimTypes { get; set; }
    }
}