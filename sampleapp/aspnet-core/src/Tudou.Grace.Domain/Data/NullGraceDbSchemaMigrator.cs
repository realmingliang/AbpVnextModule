using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Tudou.Grace.Data
{
    /* This is used if database provider does't define
     * IGraceDbSchemaMigrator implementation.
     */
    public class NullGraceDbSchemaMigrator : IGraceDbSchemaMigrator, ITransientDependency
    {
        public Task MigrateAsync()
        {
            return Task.CompletedTask;
        }
    }
}