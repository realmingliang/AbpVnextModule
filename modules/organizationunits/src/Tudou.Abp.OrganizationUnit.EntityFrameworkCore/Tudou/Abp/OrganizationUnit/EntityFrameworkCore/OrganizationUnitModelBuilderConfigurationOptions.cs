using JetBrains.Annotations;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Tudou.Abp.OrganizationUnit.EntityFrameworkCore
{
    public class OrganizationUnitModelBuilderConfigurationOptions : AbpModelBuilderConfigurationOptions
    {
        public OrganizationUnitModelBuilderConfigurationOptions(
            [NotNull] string tablePrefix,
            [CanBeNull] string schema = null)
            : base(
                tablePrefix,
                schema)
        {

        }
    }
}