using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;
using Volo.Abp.MongoDB;

namespace Tudou.Abp.OrganizationUnit.MongoDB
{
    [DependsOn(
        typeof(OrganizationUnitDomainModule),
        typeof(AbpMongoDbModule)
        )]
    public class OrganizationUnitMongoDbModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddMongoDbContext<OrganizationUnitMongoDbContext>(options =>
            {
                /* Add custom repositories here. Example:
                 * options.AddRepository<Question, MongoQuestionRepository>();
                 */
            });
        }
    }
}
