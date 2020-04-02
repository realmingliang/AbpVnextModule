using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Tudou.Abp.EntityFrameworkCore;
using Tudou.Abp.Identity;
using Tudou.Abp.Identity.AspNetCore;
using Tudou.Abp.Identity.EntityFrameworkCore;
using Tudou.Abp.Modularity;
using Tudou.Abp.PermissionManagement.EntityFrameworkCore;

namespace Tudou.Abp.Account
{
    [DependsOn(
        typeof(AbpIdentityAspNetCoreModule),
        typeof(AbpAccountApplicationModule),
        typeof(AbpIdentityDomainTestModule)
    )]
    public class AbpAccountApplicationTestModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var sqliteConnection = CreateDatabaseAndGetConnection();

            Configure<AbpDbContextOptions>(options =>
            {
                options.Configure(abpDbContextConfigurationContext =>
                {
                    abpDbContextConfigurationContext.DbContextOptions.UseSqlite(sqliteConnection);
                });
            });
        }

        private static SqliteConnection CreateDatabaseAndGetConnection()
        {
            var connection = new SqliteConnection("Data Source=:memory:");
            connection.Open();

            new IdentityDbContext(
                new DbContextOptionsBuilder<IdentityDbContext>().UseSqlite(connection).Options
            ).GetService<IRelationalDatabaseCreator>().CreateTables();

            new PermissionManagementDbContext(
                new DbContextOptionsBuilder<PermissionManagementDbContext>().UseSqlite(connection).Options
            ).GetService<IRelationalDatabaseCreator>().CreateTables();


            return connection;
        }
    }
}
