using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tudou.Abp.OrganizationUnit
{
    public interface IOrganizationUnitRoleAppService : IApplicationService
    {
        Task<PagedResultDto<OrganizationUnitRoleListDto>> GetOrganizationUnitRolesAsync(GetOrganizationUnitRolesInput input);
        Task RemoveRoleFromOrganizationUnitAsync(RemoveRoleFromOrganizationUnitInput input);
        Task AddRolesToOrganizationUnitAsync(RolesToOrganizationUnitInput input);
        Task<PagedResultDto<NameValueDto>> FindRolesAsync(FindOrganizationUnitRolesInput input);
    }
}
