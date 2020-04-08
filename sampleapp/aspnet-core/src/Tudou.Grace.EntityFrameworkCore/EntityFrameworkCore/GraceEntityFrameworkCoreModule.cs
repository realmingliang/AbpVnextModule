using Microsoft.Extensions.DependencyInjection;
using Tudou.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Tudou.Abp.Identity.EntityFrameworkCore;
using Tudou.Abp.IdentityServer.EntityFrameworkCore;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Tudou.Abp.SettingManagement.EntityFrameworkCore;
using Tudou.Abp.Saas.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.SqlServer;

namespace Tudou.Grace.EntityFrameworkCore
{
    [DependsOn(
       typeof(GraceDomainModule),
       typeof(AbpIdentityEntityFrameworkCoreModule),
       typeof(AbpIdentityServerEntityFrameworkCoreModule),
       typeof(AbpPermissionManagementEntityFrameworkCoreModule),
       typeof(AbpSettingManagementEntityFrameworkCoreModule),
       typeof(AbpEntityFrameworkCoreSqlServerModule),
       typeof(AbpBackgroundJobsEntityFrameworkCoreModule),
       typeof(AbpAuditLoggingEntityFrameworkCoreModule),
       typeof(AbpSaasEntityFrameworkCoreModule),
       typeof(AbpFeatureManagementEntityFrameworkCoreModule)
       )]
    public class GraceEntityFrameworkCoreModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<GraceDbContext>(options =>
            {
                /* Remove "includeAllEntities: true" to create
                 * default repositories only for aggregate roots */
                options.AddDefaultRepositories(includeAllEntities: true);
            });

            Configure<AbpDbContextOptions>(options =>
            {
                /* The main point to change your DBMS.
                 * See also GraceMigrationsDbContextFactory for EF Core tooling. */
                options.UseSqlServer();
            });
        }
    }
}
