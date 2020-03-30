using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace Tudou.Abp.Saas
{
    [Controller]
    //[RemoteService(Name = TenantManagementRemoteServiceConsts.RemoteServiceName)]
    [RemoteService]
    [Area("saas")]
    [Route("api/saas/tenants")]
    public class SaasTenantController : AbpController, ISaasTenantAppService //TODO: Throws exception on validation if we inherit from Controller
    {
        protected ISaasTenantAppService TenantAppService { get; }

        public SaasTenantController(ISaasTenantAppService tenantAppService)
        {
            TenantAppService = tenantAppService;
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<SaasTenantDto> GetAsync(Guid id)
        {
            return TenantAppService.GetAsync(id);
        }

        [HttpGet]
        public virtual Task<PagedResultDto<SaasTenantDto>> GetListAsync(GetSaasTenantsInput input)
        {
            return TenantAppService.GetListAsync(input);
        }

        [HttpPost]
        public virtual Task<SaasTenantDto> CreateAsync(SaasTenantCreateDto input)
        {
            //ValidateModel();
            return TenantAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<SaasTenantDto> UpdateAsync(Guid id, SaasTenantUpdateDto input)
        {
            return TenantAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return TenantAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("{id}/default-connection-string")]
        public virtual Task<string> GetDefaultConnectionStringAsync(Guid id)
        {
            return TenantAppService.GetDefaultConnectionStringAsync(id);
        }

        [HttpPut]
        [Route("{id}/default-connection-string")]
        public virtual Task UpdateDefaultConnectionStringAsync(Guid id, string defaultConnectionString)
        {
            return TenantAppService.UpdateDefaultConnectionStringAsync(id, defaultConnectionString);
        }

        [HttpDelete]
        [Route("{id}/default-connection-string")]
        public virtual Task DeleteDefaultConnectionStringAsync(Guid id)
        {
            return TenantAppService.DeleteDefaultConnectionStringAsync(id);
        }
    }
}