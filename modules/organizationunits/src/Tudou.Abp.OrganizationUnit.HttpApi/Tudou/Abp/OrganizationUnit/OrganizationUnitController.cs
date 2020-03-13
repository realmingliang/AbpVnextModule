using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace Tudou.Abp.OrganizationUnit
{
    [RemoteService]
    [Area("organization-unit")]
    [ControllerName("OrganizationUnit")]
    [Route("api/organization-unit/organizationunits")]
    public class OrganizationUnitController : AbpController, IOrganizationUnitAppService
    {
        private readonly IOrganizationUnitAppService _organizationUnitAppService;

        public OrganizationUnitController(IOrganizationUnitAppService organizationUnitAppService)
        {
            _organizationUnitAppService = organizationUnitAppService;
        }
        [HttpPost]
        public async Task<OrganizationUnitDto> CreateOrganizationUnitAsync(CreateOrganizationUnitInput input)
        {
            return await _organizationUnitAppService.CreateOrganizationUnitAsync(input);
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task DeleteOrganizationUnitAsync(Guid id)
        {
            await _organizationUnitAppService.DeleteOrganizationUnitAsync(id);

        }
        [HttpGet]
        public async Task<ListResultDto<OrganizationUnitDto>> GetOrganizationUnitsAsync()
        {
           return  await _organizationUnitAppService.GetOrganizationUnitsAsync();
        }
        [HttpPost]
        [Route("move/{id}")]
        public async Task<OrganizationUnitDto> MoveOrganizationUnitAsync(Guid id, MoveOrganizationUnitInput input)
        {
            return await _organizationUnitAppService.MoveOrganizationUnitAsync(id,input);
        }
        [HttpPost]
        [Route("{id}")]
        public async Task<OrganizationUnitDto> UpdateOrganizationUnitAsync(Guid id,UpdateOrganizationUnitInput input)
        {
            return await _organizationUnitAppService.UpdateOrganizationUnitAsync(id,input);
        }
    }
}
