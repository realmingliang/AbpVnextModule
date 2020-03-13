using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace Tudou.Abp.OrganizationUnit.MongoDB
{
    [ConnectionStringName(OrganizationUnitDbProperties.ConnectionStringName)]
    public class OrganizationUnitMongoDbContext : AbpMongoDbContext, IOrganizationUnitMongoDbContext
    {
        /* Add mongo collections here. Example:
         * public IMongoCollection<Question> Questions => Collection<Question>();
         */

        protected override void CreateModel(IMongoModelBuilder modelBuilder)
        {
            base.CreateModel(modelBuilder);

            modelBuilder.ConfigureOrganizationUnit();
        }
    }
}