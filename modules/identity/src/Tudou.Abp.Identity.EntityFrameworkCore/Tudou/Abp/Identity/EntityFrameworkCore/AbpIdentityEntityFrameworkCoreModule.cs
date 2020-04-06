using Microsoft.Extensions.DependencyInjection;
using Tudou.Abp.Identity.OrganizationUnits;
using Volo.Abp.Modularity;
using Volo.Abp.Users.EntityFrameworkCore;

namespace Tudou.Abp.Identity.EntityFrameworkCore
{
    [DependsOn(
        typeof(AbpIdentityDomainModule), 
        typeof(AbpUsersEntityFrameworkCoreModule))]
    public class AbpIdentityEntityFrameworkCoreModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<IdentityDbContext>(options =>
            {
                options.AddRepository<IdentityUser, EfCoreIdentityUserRepository>();
                options.AddRepository<IdentityRole, EfCoreIdentityRoleRepository>();
                options.AddRepository<IdentityClaimType, EfCoreIdentityClaimTypeRepository>();
                options.AddRepository<OrganizationUnit, EfCoreOrganizationUnitRepository>();
                options.AddRepository<IdentityUserOrganizationUnit, EfCoreIdentityUserOrganizationUnitRepository>();
                options.AddRepository<OrganizationUnitRole, EfCoreOrganizationUnitRoleRepository>();

            });
        }
    }
}
