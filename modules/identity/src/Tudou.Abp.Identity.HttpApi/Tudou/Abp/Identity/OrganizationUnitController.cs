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
        [Route("/find-roles")]
        public Task<PagedResultDto<NameValueDto>> FindRoles(FindOrganizationUnitRolesInput input)
        {
            return OrganizationUnitAppService.FindRoles(input);
        }
        [HttpGet]
        [Route("/find-users")]
        public Task<PagedResultDto<NameValueDto>> FindUsers(FindOrganizationUnitUsersInput input)
        {
            return OrganizationUnitAppService.FindUsers(input);
        }
    }
}
