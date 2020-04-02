using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace Tudou.Abp.Identity
{
    // TODO [RemoteService(IdentityRemoteServiceConsts.RemoteServiceName)] 
    [RemoteService]
    [Area("identity")]
    [ControllerName("ClaimType")]
    [Route("api/identity/claim-types")]
    public class IdentityClaimTypeController : AbpController, IIdentityClaimTypeAppService
    {
        protected IIdentityClaimTypeAppService ClaimTypeAppService { get; }

        public IdentityClaimTypeController(IIdentityClaimTypeAppService claimTypeAppService)
        {
            ClaimTypeAppService = claimTypeAppService;
        }

        [HttpPost]
        public virtual Task<IdentityClaimTypeDto> CreateAsync(IdentityClaimTypeCreateDto input)
        {
            return ClaimTypeAppService.CreateAsync(input);
        }

        [Route("{id}")]
        [HttpDelete]
        public virtual Task DeleteAsync(Guid id)
        {
            return ClaimTypeAppService.DeleteAsync(id);
        }


        [Route("all")]
        [HttpGet]
        public virtual Task<List<IdentityClaimTypeDto>> GetAll()
        {
            return ClaimTypeAppService.GetAll();
        }
        [Route("{id}")]
        [HttpGet]
        public virtual Task<IdentityClaimTypeDto> GetAsync(Guid id)
        {
            return ClaimTypeAppService.GetAsync(id);
        }

        [HttpGet]
        public virtual Task<PagedResultDto<IdentityClaimTypeDto>> GetListAsync(GetIdentityClaimTypesInput input)
        {
            return ClaimTypeAppService.GetListAsync(input);
        }
        [Route("{id}")]
        [HttpPut]
        public virtual Task<IdentityClaimTypeDto> UpdateAsync(Guid id, IdentityClaimTypeUpdateDto input)
        {
            return ClaimTypeAppService.UpdateAsync(id, input);
        }
    }
}
