using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp.Domain.Services;

namespace Tudou.Abp.Saas
{
    public interface ISaasTenantManager : IDomainService
    {
        [NotNull]
        Task<SaasTenant> CreateAsync([NotNull] string name, Guid? editionId);

        Task ChangeNameAsync([NotNull] SaasTenant tenant, [NotNull] string name);

    }
}
