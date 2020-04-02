using System.Threading.Tasks;

namespace Tudou.Grace.Data
{
    public interface IGraceDbSchemaMigrator
    {
        Task MigrateAsync();
    }
}
