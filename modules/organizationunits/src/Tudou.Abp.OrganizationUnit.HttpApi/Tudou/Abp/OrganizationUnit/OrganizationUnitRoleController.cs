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
    [Route("api/organization-unit/organizationunit-roles")]
    public class OrganizationUnitRoleController : AbpController, IOrganizationUnitRoleAppService
    {
        private readonly IOrganizationUnitRoleAppService _organizationUnitRoleAppService;

        public OrganizationUnitRoleController(IOrganizationUnitRoleAppService organizationUnitRoleAppService)
        {
            _organizationUnitRoleAppService = organizationUnitRoleAppService;
        }
        [HttpPost]
        [Route("addroles-to-organizationunit")]
        public async Task AddRolesToOrganizationUnitAsync(RolesToOrganizationUnitInput input)
        {
            await _organizationUnitRoleAppService.AddRolesToOrganizationUnitAsync(input);
        }
        [HttpGet]
        [Route("findroles")]
        public async Task<PagedResultDto<NameValueDto>> FindRolesAsync(FindOrganizationUnitRolesInput input)
        {
           return await _organizationUnitRoleAppService.FindRolesAsync(input);
        }
        [HttpGet]
        public async Task<PagedResultDto<OrganizationUnitRoleListDto>> GetOrganizationUnitRolesAsync(GetOrganizationUnitRolesInput input)
        {
            return await _organizationUnitRoleAppService.GetOrganizationUnitRolesAsync(input);
        }
        [HttpDelete]
        [Route("removerole-from-organizationunit")]
        public async Task RemoveRoleFromOrganizationUnitAsync(RemoveRoleFromOrganizationUnitInput input)
        {
             await _organizationUnitRoleAppService.RemoveRoleFromOrganizationUnitAsync(input);
        }
    }
}
