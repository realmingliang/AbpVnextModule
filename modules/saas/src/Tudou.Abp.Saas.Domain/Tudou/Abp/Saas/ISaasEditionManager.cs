using JetBrains.Annotations;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Tudou.Abp.Saas
{
    public interface ISaasEditionManager: IDomainService
    {
        [NotNull]
        Task<SaasEdition> CreateAsync([NotNull] string displayName);

        Task ChangeNameAsync([NotNull] SaasEdition edition, [NotNull] string displayName);

    }
}
