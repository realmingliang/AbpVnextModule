using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Tudou.Abp.Identity.OrganizationUnits;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace Tudou.Abp.Identity
{
    [RemoteService]
    [Area("identity")]
    [ControllerName("OrganizationUnit")]
    [Route("api/identity/organization-unit")]
    public class OrganizationUnitController: AbpController,IOrganizationUnitAppService
    {
        protected IOrganizationUnitAppService OrganizationUnitAppService { get; }

        public OrganizationUnitController(IOrganizationUnitAppService organizationUnitAppService)
        {
            OrganizationUnitAppService = organizationUnitAppService;
        }
        [HttpGet]
        public virtual Task<ListResultDto<OrganizationUnitDto>> GetOrganizationUnits()
        {
            return OrganizationUnitAppService.GetOrganizationUnits();
        }
        [HttpPost]
        public virtual Task<OrganizationUnitDto> CreateOrganizationUnit(CreateOrganizationUnitInput input)
        {
            return OrganizationUnitAppService.CreateOrganizationUnit(input);
        }
        [HttpPut]
        [Route("{id}")]
        public virtual Task<OrganizationUnitDto> UpdateOrganizationUnit(Guid id, UpdateOrganizationUnitInput input)
        {
            return OrganizationUnitAppService.UpdateOrganizationUnit(id,input);

        }
        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteOrganizationUnit(Guid id)
        {
            return OrganizationUnitAppService.DeleteOrganizationUnit(id);
        }
        [HttpGet]
        [Route("find-roles")]
        public virtual Task<PagedResultDto<NameValueDto>> FindRoles(FindOrganizationUnitRolesInput input)
        {
            return OrganizationUnitAppService.FindRoles(input);
        }
        [HttpGet]
        [Route("find-users")]
        public virtual Task<PagedResultDto<NameValueDto>> FindUsers(FindOrganizationUnitUsersInput input)
        {
            return OrganizationUnitAppService.FindUsers(input);
        }
        [HttpPut]
        [Route("{id}/move")]
        public virtual Task<OrganizationUnitDto> MoveOrganizationUnit(Guid id, MoveOrganizationUnitInput input)
        {
            return OrganizationUnitAppService.MoveOrganizationUnit(id,input);
        }
        [HttpDelete]
        [Route("{id}/remove-user-from-organizationunit")]
        public virtual Task RemoveUserFromOrganizationUnit(Guid id, RemoveUserFromOrganizationUnitInput input)
        {
            return OrganizationUnitAppService.RemoveUserFromOrganizationUnit(id, input);
        }
        [HttpDelete]
        [Route("{id}/remove-role-from-organizationunit")]
        public virtual Task RemoveRoleFromOrganizationUnit(Guid id, RemoveRoleFromOrganizationUnitInput input)
        {
            return OrganizationUnitAppService.RemoveRoleFromOrganizationUnit(id, input);
        }
        [HttpPost]
        [Route("{id}/add-users-to-organizationunit")]
        public virtual Task AddUsersToOrganizationUnit(Guid id, UsersToOrganizationUnitInput input)
        {
            return OrganizationUnitAppService.AddUsersToOrganizationUnit(id, input);
        }
        [HttpPost]
        [Route("{id}/add-roles-to-organizationunit")]
        public virtual Task AddRolesToOrganizationUnit(Guid id, RolesToOrganizationUnitInput input)
        {
            return OrganizationUnitAppService.AddRolesToOrganizationUnit(id, input);
        }
    }
}
