using System;
using Volo.Abp;
using Volo.Abp.MongoDB;

namespace Tudou.Abp.OrganizationUnit.MongoDB
{
    public static class OrganizationUnitMongoDbContextExtensions
    {
        public static void ConfigureOrganizationUnit(
            this IMongoModelBuilder builder,
            Action<AbpMongoModelBuilderConfigurationOptions> optionsAction = null)
        {
            Check.NotNull(builder, nameof(builder));

            var options = new OrganizationUnitMongoModelBuilderConfigurationOptions(
                OrganizationUnitDbProperties.DbTablePrefix
            );

            optionsAction?.Invoke(options);
        }
    }
}