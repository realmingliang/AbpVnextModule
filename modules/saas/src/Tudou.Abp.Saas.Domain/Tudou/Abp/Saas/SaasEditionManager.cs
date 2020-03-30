using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace Tudou.Abp.Saas
{
    public class SaasEditionManager : DomainService, ISaasEditionManager
    {
        protected ISaasEditionRepository EditionRepository { get; }

        public SaasEditionManager(ISaasEditionRepository editionRepository)
        {
            EditionRepository = editionRepository;

        }
        public virtual async Task ChangeNameAsync(SaasEdition edition, string displayName)
        {
            Check.NotNull(edition, nameof(edition));
            Check.NotNull(displayName, nameof(displayName));
            await ValidateNameAsync(displayName, edition.Id);
            edition.SetDisplayName(displayName);
        }

        public virtual async Task<SaasEdition> CreateAsync(string displayName)
        {
            Check.NotNull(displayName, nameof(displayName));
            await ValidateNameAsync(displayName);
            return new SaasEdition(GuidGenerator.Create(), displayName);

        }

        protected virtual async Task ValidateNameAsync(string name, Guid? expectedId = null)
        {
            var edition = await EditionRepository.FindByNameAsync(name);
            if (edition != null && edition.Id != expectedId)
            {
                throw new UserFriendlyException("Duplicate edition name: " + name); 
            }
        }
    }
}
