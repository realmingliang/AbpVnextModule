using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace Tudou.Abp.Saas
{
    [Controller]
    //[RemoteService(Name = EditionManagementRemoteServiceConsts.RemoteServiceName)]
    [RemoteService()]
    [Area("saas")]
    [Route("api/saas/editions")]
    public class SaasEditionController : AbpController, ISaasEditionAppService
    {
        protected ISaasEditionAppService EditionAppService { get; }

        public SaasEditionController(ISaasEditionAppService editionAppService)
        {
            EditionAppService = editionAppService;
        }
        [HttpPost]
        public Task<SaasEditionDto> CreateAsync(SaasEditionCreateDto input)
        {
            //ValidateModel();
            return EditionAppService.CreateAsync(input);
        }
        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return EditionAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<SaasEditionDto> GetAsync(Guid id)
        {
            return EditionAppService.GetAsync(id);
        }
        [HttpGet]
        public virtual Task<PagedResultDto<SaasEditionDto>> GetListAsync(GetSaasEditionsInput input)
        {
            return EditionAppService.GetListAsync(input);
        }
        [HttpPut]
        [Route("{id}")]
        public virtual Task<SaasEditionDto> UpdateAsync(Guid id, SaasEditionUpdateDto input)
        {
            return EditionAppService.UpdateAsync(id, input);
        }
    }
}
