using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Tudou.Abp.OrganizationUnit.EntityFrameworkCore
{
    [DependsOn(
        typeof(OrganizationUnitDomainModule),
        typeof(AbpEntityFrameworkCoreModule)
    )]
    public class OrganizationUnitEntityFrameworkCoreModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<OrganizationUnitDbContext>(options =>
            {
                options.AddRepository<OrganizationUnit, EfCoreOrganizationUnitRepository>();
                options.AddRepository<OrganizationUnitRole, EfCoreOrganizationUnitRoleRepository>();
                options.AddRepository<OrganizationUnitUser, EfCoreOrganizationUnitUserRepository>();

            });
        }
    }
}