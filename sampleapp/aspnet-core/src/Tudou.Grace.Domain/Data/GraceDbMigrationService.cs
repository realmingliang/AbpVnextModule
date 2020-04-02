using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.MultiTenancy;
using Tudou.Abp.Saas;

namespace Tudou.Grace.Data
{
    public class GraceDbMigrationService : ITransientDependency
    {
        public ILogger<GraceDbMigrationService> Logger { get; set; }

        private readonly IDataSeeder _dataSeeder;
        private readonly IEnumerable<IGraceDbSchemaMigrator> _dbSchemaMigrators;
        private readonly ISaasTenantRepository _tenantRepository;
        private readonly ICurrentTenant _currentTenant;

        public GraceDbMigrationService(
            IDataSeeder dataSeeder,
            IEnumerable<IGraceDbSchemaMigrator> dbSchemaMigrators,
            ISaasTenantRepository tenantRepository,
            ICurrentTenant currentTenant)
        {
            _dataSeeder = dataSeeder;
            _dbSchemaMigrators = dbSchemaMigrators;
            _tenantRepository = tenantRepository;
            _currentTenant = currentTenant;

            Logger = NullLogger<GraceDbMigrationService>.Instance;
        }

        public async Task MigrateAsync()
        {
            Logger.LogInformation("Started database migrations...");

            await MigrateHostDatabaseAsync();

            var i = 0;
            var tenants = await _tenantRepository.GetListAsync();
            foreach (var tenant in tenants)
            {
                i++;

                using (_currentTenant.Change(tenant.Id))
                {
                    Logger.LogInformation($"Migrating {tenant.Name} database schema... ({i} of {tenants.Count})");
                    await MigrateTenantDatabasesAsync(tenant);
                    Logger.LogInformation($"Successfully completed {tenant.Name} database migrations.");
                }
            }

            Logger.LogInformation("Successfully completed database migrations.");
        }

        private async Task MigrateHostDatabaseAsync()
        {
            Logger.LogInformation("Migrating host database schema...");

            foreach (var migrator in _dbSchemaMigrators)
            {
                await migrator.MigrateAsync();
            }

            Logger.LogInformation("Executing host database seed...");
            await _dataSeeder.SeedAsync();

            Logger.LogInformation("Successfully completed host database migrations.");
        }

        private async Task MigrateTenantDatabasesAsync(SaasTenant tenant)
        {
            Logger.LogInformation($"Migrating schema for {tenant.Name} database...");

            foreach (var migrator in _dbSchemaMigrators)
            {
                await migrator.MigrateAsync();
            }

            Logger.LogInformation($"Executing {tenant.Name} tenant database seed...");
            
            await _dataSeeder.SeedAsync(tenant.Id);
        }
    }
}