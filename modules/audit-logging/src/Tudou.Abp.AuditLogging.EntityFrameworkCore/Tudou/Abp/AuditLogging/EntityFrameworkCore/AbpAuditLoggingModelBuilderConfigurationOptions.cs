using JetBrains.Annotations;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Tudou.Abp.Identity.EntityFrameworkCore
{
    public class AbpAuditLoggingModelBuilderConfigurationOptions : AbpModelBuilderConfigurationOptions
    {
        public AbpAuditLoggingModelBuilderConfigurationOptions(
            [NotNull] string tablePrefix,
            [CanBeNull] string schema)
            : base(
                tablePrefix, 
                schema)
        {

        }
    }
}