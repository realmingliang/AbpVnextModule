using Microsoft.EntityFrameworkCore;
using Tudou.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Tudou.Abp.Identity;
using Tudou.Abp.Identity.EntityFrameworkCore;
using Tudou.Abp.IdentityServer.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Tudou.Abp.SettingManagement.EntityFrameworkCore;
using Tudou.Abp.Saas.EntityFrameworkCore;

namespace Tudou.Grace.EntityFrameworkCore
{
    /* This DbContext is only used for database migrations.
     * It is not used on runtime. See GraceDbContext for the runtime DbContext.
     * It is a unified model that includes configuration for
     * all used modules and your application.
     */
    public class GraceMigrationsDbContext : AbpDbContext<GraceMigrationsDbContext>
    {
        public GraceMigrationsDbContext(DbContextOptions<GraceMigrationsDbContext> options) 
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            /* Include modules to your migration db context */

            builder.ConfigurePermissionManagement();
            builder.ConfigureSettingManagement();
            builder.ConfigureBackgroundJobs();
            builder.ConfigureAuditLogging();
            builder.ConfigureIdentity();
            builder.ConfigureIdentityServer();
            builder.ConfigureFeatureManagement();
            builder.ConfigureSaasManagement();

            /* Configure customizations for entities from the modules included  */

            builder.Entity<IdentityUser>(b =>
            {
                b.ConfigureCustomUserProperties();
            });

            /* Configure your own tables/entities inside the ConfigureGrace method */

            builder.ConfigureGrace();
        }
    }
}