using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Tudou.Abp.Saas.EntityFrameworkCore
{
    [DependsOn(typeof(AbpSaasDomainModule))]
    [DependsOn(typeof(AbpEntityFrameworkCoreModule))]
    public class AbpSaasEntityFrameworkCoreModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<SaasDbContext>(options =>
            {
                options.AddRepository<SaasTenant, EfCoreSaasTenantRepository>();
                options.AddRepository<SaasEdition, EfCoreSaasEditionRepository>();

            });
        }
    }
}
