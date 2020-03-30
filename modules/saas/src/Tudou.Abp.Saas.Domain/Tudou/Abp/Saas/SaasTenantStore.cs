using System;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.MultiTenancy;
using Volo.Abp.ObjectMapping;

namespace Tudou.Abp.Saas
{
    //TODO: This class should use caching instead of querying everytime!

    public class SaasTenantStore : ITenantStore, ITransientDependency
    {
        protected ISaasTenantRepository TenantRepository { get; }
        protected IObjectMapper<AbpSaasDomainModule> ObjectMapper { get; }
        protected ICurrentTenant CurrentTenant { get; }

        public SaasTenantStore(
            ISaasTenantRepository tenantRepository, 
            IObjectMapper<AbpSaasDomainModule> objectMapper,
            ICurrentTenant currentTenant)
        {
            TenantRepository = tenantRepository;
            ObjectMapper = objectMapper;
            CurrentTenant = currentTenant;
        }

        public virtual async Task<TenantConfiguration> FindAsync(string name)
        {
            using (CurrentTenant.Change(null)) //TODO: No need this if we can implement to define host side (or tenant-independent) entities!
            {
                var tenant = await TenantRepository.FindByNameAsync(name);
                if (tenant == null)
                {
                    return null;
                }

                return ObjectMapper.Map<SaasTenant, TenantConfiguration>(tenant);
            }
        }

        public virtual async Task<TenantConfiguration> FindAsync(Guid id)
        {
            using (CurrentTenant.Change(null)) //TODO: No need this if we can implement to define host side (or tenant-independent) entities!
            {
                var tenant = await TenantRepository.FindAsync(id);
                if (tenant == null)
                {
                    return null;
                }

                return ObjectMapper.Map<SaasTenant, TenantConfiguration>(tenant);
            }
        }

        public virtual TenantConfiguration Find(string name)
        {
            using (CurrentTenant.Change(null)) //TODO: No need this if we can implement to define host side (or tenant-independent) entities!
            {
                var tenant = TenantRepository.FindByName(name);
                if (tenant == null)
                {
                    return null;
                }

                return ObjectMapper.Map<SaasTenant, TenantConfiguration>(tenant);
            }
        }

        public virtual TenantConfiguration Find(Guid id)
        {
            using (CurrentTenant.Change(null)) //TODO: No need this if we can implement to define host side (or tenant-independent) entities!
            {
                var tenant = TenantRepository.FindById(id);
                if (tenant == null)
                {
                    return null;
                }

                return ObjectMapper.Map<SaasTenant, TenantConfiguration>(tenant);
            }
        }
    }
}
