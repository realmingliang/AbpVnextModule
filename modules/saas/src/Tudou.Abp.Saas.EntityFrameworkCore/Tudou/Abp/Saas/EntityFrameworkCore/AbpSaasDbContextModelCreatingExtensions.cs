using System;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Tudou.Abp.Saas.EntityFrameworkCore
{
    public static class AbpSaasDbContextModelCreatingExtensions
    {
        public static void ConfigureSaasManagement(
            this ModelBuilder builder,
            [CanBeNull] Action<AbpSaasModelBuilderConfigurationOptions> optionsAction = null)
        {
            Check.NotNull(builder, nameof(builder));

            var options = new AbpSaasModelBuilderConfigurationOptions(
                AbpSaasDbProperties.DbTablePrefix,
                AbpSaasDbProperties.DbSchema
            );

            optionsAction?.Invoke(options);

            builder.Entity<SaasTenant>(b =>
            {
                b.ToTable(options.TablePrefix + "SaasTenants", options.Schema);
                b.ConfigureFullAuditedAggregateRoot();
                b.Property(t => t.Name).IsRequired().HasMaxLength(SaasTenantConsts.MaxNameLength);
                b.HasMany(u => u.ConnectionStrings).WithOne().HasForeignKey(uc => uc.TenantId).IsRequired();
                b.HasOne(t => t.SaasEdition).WithMany().HasForeignKey(t => t.EditionId);
                b.HasIndex(u => u.Name);
             
            });

            builder.Entity<SaasTenantConnectionString>(b =>
            {
                b.ToTable(options.TablePrefix + "SaasTenantConnectionStrings", options.Schema);
                b.ConfigureByConvention();
                b.HasKey(x => new { x.TenantId, x.Name });
                b.Property(cs => cs.Name).IsRequired().HasMaxLength(SaasTenantConnectionStringConsts.MaxNameLength);
                b.Property(cs => cs.Value).IsRequired().HasMaxLength(SaasTenantConnectionStringConsts.MaxValueLength);
            });

            builder.Entity<SaasEdition>(b =>
            {
                b.ToTable(options.TablePrefix + "SaasEditions", options.Schema);

                b.ConfigureFullAuditedAggregateRoot();

                b.Property(t => t.DisplayName).IsRequired().HasMaxLength(SaasEditionConsts.MaxNameLength);

                b.HasIndex(u => u.DisplayName);
            });
        }
    }
}