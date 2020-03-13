using JetBrains.Annotations;
using Volo.Abp.MongoDB;

namespace Tudou.Abp.OrganizationUnit.MongoDB
{
    public class OrganizationUnitMongoModelBuilderConfigurationOptions : AbpMongoModelBuilderConfigurationOptions
    {
        public OrganizationUnitMongoModelBuilderConfigurationOptions(
            [NotNull] string collectionPrefix = "")
            : base(collectionPrefix)
        {
        }
    }
}