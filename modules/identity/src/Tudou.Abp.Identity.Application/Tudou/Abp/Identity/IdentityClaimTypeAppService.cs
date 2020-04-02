using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Tudou.Abp.Identity
{
    public class IdentityClaimTypeAppService : IdentityAppServiceBase, IIdentityClaimTypeAppService
    {
        private readonly IdenityClaimTypeManager IdenityClaimTypeManager;
        private readonly IIdentityClaimTypeRepository IdentityClaimTypeRepository;
        public IdentityClaimTypeAppService(IdenityClaimTypeManager idenityClaimTypeManager,
            IIdentityClaimTypeRepository identityClaimTypeRepository)
        {
            IdenityClaimTypeManager = idenityClaimTypeManager;
            IdentityClaimTypeRepository = identityClaimTypeRepository;
        }
        public async Task<IdentityClaimTypeDto> CreateAsync(IdentityClaimTypeCreateDto input)
        {
            var claimType = new IdentityClaimType(GuidGenerator.Create(),
                input.Name, input.Required,false,input.Regex,input.RegexDescription,input.Description,input.ValueType);
            return ObjectMapper.Map<IdentityClaimType, IdentityClaimTypeDto>(await IdenityClaimTypeManager.CreateAsync(claimType));
        }

        public async Task DeleteAsync(Guid id)
        {
            await IdentityClaimTypeRepository.DeleteAsync(id);
        }

        public async Task<List<IdentityClaimTypeDto>> GetAll()
        {
            var list = await IdentityClaimTypeRepository.GetListAsync();
            return ObjectMapper.Map<List<IdentityClaimType>, List<IdentityClaimTypeDto>>(list);
        }

        public async Task<IdentityClaimTypeDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<IdentityClaimType, IdentityClaimTypeDto>(await IdentityClaimTypeRepository.GetAsync(id));
        }

        public async Task<PagedResultDto<IdentityClaimTypeDto>> GetListAsync(GetIdentityClaimTypesInput input)
        {
            var totalCount = await IdentityClaimTypeRepository.GetCountAsync(input.Filter);
            var list = await IdentityClaimTypeRepository.GetListAsync(input.Sorting, input.MaxResultCount, input.SkipCount, input.Filter);
            return new PagedResultDto<IdentityClaimTypeDto>(
              totalCount,
              ObjectMapper.Map<List<IdentityClaimType>, List<IdentityClaimTypeDto>>(list)
              );

        }

        public async Task<IdentityClaimTypeDto> UpdateAsync(Guid id, IdentityClaimTypeUpdateDto input)
        {
            var entity = await IdentityClaimTypeRepository.GetAsync(id);
            ObjectMapper.Map(input, entity);
            return ObjectMapper.Map<IdentityClaimType, IdentityClaimTypeDto>(await IdenityClaimTypeManager.UpdateAsync(entity));
        }
    }
}
