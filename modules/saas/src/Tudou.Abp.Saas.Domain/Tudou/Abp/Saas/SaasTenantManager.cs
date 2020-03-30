using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace Tudou.Abp.Saas
{
    public class SaasTenantManager : DomainService, ISaasTenantManager
    {
        protected ISaasTenantRepository TenantRepository { get; }

        public SaasTenantManager(ISaasTenantRepository tenantRepository)
        {
            TenantRepository = tenantRepository;

        }

        public virtual async Task<SaasTenant> CreateAsync(string name,Guid? editionId)
        {
            Check.NotNull(name, nameof(name));

            await ValidateNameAsync(name);
            return new SaasTenant(GuidGenerator.Create(), name, editionId);
        }

        public virtual async Task ChangeNameAsync(SaasTenant tenant, string name)
        {
            Check.NotNull(tenant, nameof(tenant));
            Check.NotNull(name, nameof(name));

            await ValidateNameAsync(name, tenant.Id);
            tenant.SetName(name);
        }

        protected virtual async Task ValidateNameAsync(string name, Guid? expectedId = null)
        {
            var tenant = await TenantRepository.FindByNameAsync(name);
            if (tenant != null && tenant.Id != expectedId)
            {
                throw new UserFriendlyException("Duplicate tenancy name: " + name); //TODO: A domain exception would be better..?
            }
        }

    }
}