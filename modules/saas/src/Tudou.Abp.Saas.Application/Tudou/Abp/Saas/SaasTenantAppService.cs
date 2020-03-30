using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Data;

namespace Tudou.Abp.Saas
{
    [Authorize(SaasPermissions.Tenants.Default)]
    public class SaasTenantAppService : SaasAppServiceBase, ISaasTenantAppService
    {
        protected IDataSeeder DataSeeder { get; }
        protected ISaasTenantRepository TenantRepository { get; }
        protected ISaasTenantManager TenantManager { get; }

        public SaasTenantAppService(
            ISaasTenantRepository tenantRepository,
            ISaasTenantManager tenantManager,
            IDataSeeder dataSeeder)
        {
            DataSeeder = dataSeeder;
            TenantRepository = tenantRepository;
            TenantManager = tenantManager;
        }

        public virtual async Task<SaasTenantDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<SaasTenant, SaasTenantDto>(
                await TenantRepository.GetAsync(id));
        }

        public virtual async Task<PagedResultDto<SaasTenantDto>> GetListAsync(GetSaasTenantsInput input)
        {
            var count = await TenantRepository.GetCountAsync(input.Filter);
            var list = await TenantRepository.GetListAsync(input.Sorting, input.MaxResultCount, input.SkipCount, input.Filter,true);

            return new PagedResultDto<SaasTenantDto>(
                count,
                ObjectMapper.Map<List<SaasTenant>, List<SaasTenantDto>>(list)
            );
        }

        [Authorize(SaasPermissions.Tenants.Create)]
        public virtual async Task<SaasTenantDto> CreateAsync(SaasTenantCreateDto input)
        {
            var tenant = await TenantManager.CreateAsync(input.Name,input.EditionId);
            await TenantRepository.InsertAsync(tenant);

            using (CurrentTenant.Change(tenant.Id, tenant.Name))
            {
                //TODO: Handle database creation?

                await DataSeeder.SeedAsync(
                                new DataSeedContext(tenant.Id)
                                    .WithProperty("AdminEmail", input.AdminEmailAddress)
                                    .WithProperty("AdminPassword", input.AdminPassword)
                                );
            }

            return ObjectMapper.Map<SaasTenant, SaasTenantDto>(tenant);
        }

        [Authorize(SaasPermissions.Tenants.Update)]
        public virtual async Task<SaasTenantDto> UpdateAsync(Guid id, SaasTenantUpdateDto input)
        {
            var tenant = await TenantRepository.GetAsync(id);
 
            await TenantManager.ChangeNameAsync(tenant, input.Name);
            tenant.SetEdition(input.EditionId);
            await TenantRepository.UpdateAsync(tenant);
            return ObjectMapper.Map<SaasTenant, SaasTenantDto>(tenant);
        }

        [Authorize(SaasPermissions.Tenants.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            var tenant = await TenantRepository.FindAsync(id);
            if (tenant == null)
            {
                return;
            }

            await TenantRepository.DeleteAsync(tenant);
        }

        [Authorize(SaasPermissions.Tenants.ManageConnectionStrings)]
        public virtual async Task<string> GetDefaultConnectionStringAsync(Guid id)
        {
            var tenant = await TenantRepository.GetAsync(id);
            return tenant?.FindDefaultConnectionString();
        }

        [Authorize(SaasPermissions.Tenants.ManageConnectionStrings)]
        public virtual async Task UpdateDefaultConnectionStringAsync(Guid id, string defaultConnectionString)
        {
            var tenant = await TenantRepository.GetAsync(id);
            tenant.SetDefaultConnectionString(defaultConnectionString);
            await TenantRepository.UpdateAsync(tenant);
        }

        [Authorize(SaasPermissions.Tenants.ManageConnectionStrings)]
        public virtual async Task DeleteDefaultConnectionStringAsync(Guid id)
        {
            var tenant = await TenantRepository.GetAsync(id);
            tenant.RemoveDefaultConnectionString();
            await TenantRepository.UpdateAsync(tenant);
        }
    }
}
