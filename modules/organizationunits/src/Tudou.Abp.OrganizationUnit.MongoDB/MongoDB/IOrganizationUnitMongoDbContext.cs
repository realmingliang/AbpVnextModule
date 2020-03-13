using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace Tudou.Abp.OrganizationUnit.MongoDB
{
    [ConnectionStringName(OrganizationUnitDbProperties.ConnectionStringName)]
    public interface IOrganizationUnitMongoDbContext : IAbpMongoDbContext
    {
        /* Define mongo collections here. Example:
         * IMongoCollection<Question> Questions { get; }
         */
    }
}
