using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Tudou.Abp.Identity
{
    public interface IIdentityClaimTypeAppService : ICrudAppService<IdentityClaimTypeDto, Guid, GetIdentityClaimTypesInput, IdentityClaimTypeCreateDto, IdentityClaimTypeUpdateDto>
    {
        public Task<List<IdentityClaimTypeDto>> GetAll();
    }
}
