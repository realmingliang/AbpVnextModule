using Tudou.Grace.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace Tudou.Grace.DbMigrator
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(GraceEntityFrameworkCoreDbMigrationsModule),
        typeof(GraceApplicationContractsModule)
        )]
    public class GraceDbMigratorModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
        }
    }
}
