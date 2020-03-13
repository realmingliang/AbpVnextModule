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
    [Route("api/organization-unit/organizationunit-users")]
    public class OrganizationUnitUserController : AbpController, IOrganizationUnitUserAppService
    {
        private readonly IOrganizationUnitUserAppService _organizationUnitUserAppService;

        public OrganizationUnitUserController(IOrganizationUnitUserAppService organizationUnitUserAppService)
        {
            _organizationUnitUserAppService = organizationUnitUserAppService;
        }
        [HttpPost]
        [Route("addusers-to-organizationunit")]
        public async Task AddUsersToOrganizationUnitAsync(UsersToOrganizationUnitInput input)
        {
            await _organizationUnitUserAppService.AddUsersToOrganizationUnitAsync(input);
        }
        [HttpGet]
        [Route("findusers")]
        public async Task<PagedResultDto<NameValueDto>> FindUsersAsync(FindOrganizationUnitUsersInput input)
        {
            return await _organizationUnitUserAppService.FindUsersAsync(input);
        }
        [HttpPost]
        public async Task<PagedResultDto<OrganizationUnitUserListDto>> GetOrganizationUnitUsersAsync(GetOrganizationUnitUsersInput input)
        {
            return await _organizationUnitUserAppService.GetOrganizationUnitUsersAsync(input);
        }
        [HttpDelete]
        [Route("removeuser-from-organizationunit")]
        public async Task RemoveUserFromOrganizationUnitAsync(RemoveUserFromOrganizationUnitInput input)
        {
            await _organizationUnitUserAppService.RemoveUserFromOrganizationUnitAsync(input);

        }
    }
}
