using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Tudou.Grace.Data;
using Volo.Abp.DependencyInjection;

namespace Tudou.Grace.EntityFrameworkCore
{
    public class EntityFrameworkCoreGraceDbSchemaMigrator
        : IGraceDbSchemaMigrator, ITransientDependency
    {
        private readonly IServiceProvider _serviceProvider;

        public EntityFrameworkCoreGraceDbSchemaMigrator(
            IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task MigrateAsync()
        {
            /* We intentionally resolving the GraceMigrationsDbContext
             * from IServiceProvider (instead of directly injecting it)
             * to properly get the connection string of the current tenant in the
             * current scope.
             */

            await _serviceProvider
                .GetRequiredService<GraceMigrationsDbContext>()
                .Database
                .MigrateAsync();
        }
    }
}