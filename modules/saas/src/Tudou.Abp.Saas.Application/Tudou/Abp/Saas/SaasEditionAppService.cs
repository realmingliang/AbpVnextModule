using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Tudou.Abp.Saas
{
    [Authorize(SaasPermissions.Editions.Default)]
    public class SaasEditionAppService :SaasAppServiceBase, ISaasEditionAppService
    {
        protected ISaasEditionRepository EditionRepository { get; }
        protected ISaasEditionManager EditionManager { get; }


        public SaasEditionAppService(
            ISaasEditionRepository editionRepository,
            ISaasEditionManager editionManager)
        {
            EditionRepository = editionRepository;
            EditionManager = editionManager;
        }

        [Authorize(SaasPermissions.Editions.Create)]
        public async Task<SaasEditionDto> CreateAsync(SaasEditionCreateDto input)
        {
            var edition = await EditionManager.CreateAsync(input.DisplayName);
            await EditionRepository.InsertAsync(edition);
            return ObjectMapper.Map<SaasEdition, SaasEditionDto>(edition);
        }
        [Authorize(SaasPermissions.Editions.Delete)]
        public async Task DeleteAsync(Guid id)
        {
            var edition = await EditionRepository.FindAsync(id);
            if (edition == null)
            {
                return;
            }

            await EditionRepository.DeleteAsync(edition);
        }

        public async Task<SaasEditionDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<SaasEdition, SaasEditionDto>(
               await EditionRepository.GetAsync(id));
        }

        public async Task<PagedResultDto<SaasEditionDto>> GetListAsync(GetSaasEditionsInput input)
        {
            var count = await EditionRepository.GetCountAsync(input.Filter);
            var list = await EditionRepository.GetListAsync(input.Sorting, input.MaxResultCount, input.SkipCount, input.Filter);

            return new PagedResultDto<SaasEditionDto>(
                count,
                ObjectMapper.Map<List<SaasEdition>, List<SaasEditionDto>>(list)
            );
        }
        [Authorize(SaasPermissions.Editions.Update)]
        public async Task<SaasEditionDto> UpdateAsync(Guid id, SaasEditionUpdateDto input)
        {
            var edition = await EditionRepository.GetAsync(id);
            await EditionManager.ChangeNameAsync(edition, input.DisplayName);
            await EditionRepository.UpdateAsync(edition);
            return ObjectMapper.Map<SaasEdition, SaasEditionDto>(edition);
        }
    }
}
