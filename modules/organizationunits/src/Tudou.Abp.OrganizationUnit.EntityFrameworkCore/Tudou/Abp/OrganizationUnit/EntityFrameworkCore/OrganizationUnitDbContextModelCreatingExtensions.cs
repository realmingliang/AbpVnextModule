using System;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.Identity;

namespace Tudou.Abp.OrganizationUnit.EntityFrameworkCore
{
    public static class OrganizationUnitDbContextModelCreatingExtensions
    {
        public static void ConfigureOrganizationUnit(
            this ModelBuilder builder,
            Action<OrganizationUnitModelBuilderConfigurationOptions> optionsAction = null)
        {
            Check.NotNull(builder, nameof(builder));

            var options = new OrganizationUnitModelBuilderConfigurationOptions(
                OrganizationUnitDbProperties.DbTablePrefix,
                OrganizationUnitDbProperties.DbSchema
            );

            optionsAction?.Invoke(options);


            builder.Entity<OrganizationUnit>(b =>
            {
                //Configure table & schema name
                b.ToTable(options.TablePrefix + "OrganizationUnit", options.Schema);
                b.ConfigureFullAuditedAggregateRoot();
                b.Property(u => u.Code).IsRequired().HasMaxLength(OrganizationUnitConsts.MaxCodeLength).HasColumnName(nameof(OrganizationUnit.Code));
                b.Property(u => u.Name).IsRequired().HasMaxLength(OrganizationUnitConsts.MaxNameLength).HasColumnName(nameof(OrganizationUnit.Name));
                b.HasOne(t => t.Parent).WithMany(t=>t.Children).HasForeignKey(t => t.ParentId);
                b.HasIndex(u => u.Name);
                b.HasIndex(u => u.ParentId);
            });
            builder.Entity<OrganizationUnitRole>(b =>
            {
                b.ToTable(options.TablePrefix + "OrganizationUnitRole", options.Schema);
                b.HasOne<OrganizationUnit>().WithMany().HasForeignKey(ur => ur.OrganizationUnitId).IsRequired();
                b.HasIndex(uc => uc.OrganizationUnitId);
                b.HasIndex(uc => uc.RoleId);
            });
            builder.Entity<OrganizationUnitUser>(b =>
            {
                //Configure table & schema name
                b.ToTable(options.TablePrefix + "OrganizationUnitUser", options.Schema);
                b.HasOne<OrganizationUnit>().WithMany().HasForeignKey(ur => ur.OrganizationUnitId).IsRequired();
                b.HasIndex(uc => uc.OrganizationUnitId);
                b.HasIndex(uc => uc.UserId);
            });
        }
    }
}