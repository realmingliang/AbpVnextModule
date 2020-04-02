using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace Tudou.Grace.EntityFrameworkCore
{
    [DependsOn(
        typeof(GraceEntityFrameworkCoreModule)
        )]
    public class GraceEntityFrameworkCoreDbMigrationsModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<GraceMigrationsDbContext>();
        }
    }
}
