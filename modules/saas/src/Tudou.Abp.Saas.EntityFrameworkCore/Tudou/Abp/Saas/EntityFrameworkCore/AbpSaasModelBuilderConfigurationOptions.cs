using JetBrains.Annotations;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Tudou.Abp.Saas.EntityFrameworkCore
{
    public class AbpSaasModelBuilderConfigurationOptions : AbpModelBuilderConfigurationOptions
    {
        public AbpSaasModelBuilderConfigurationOptions(
            [NotNull] string tablePrefix,
            [CanBeNull] string schema)
            : base(
                tablePrefix,
                schema)
        {

        }
    }
}