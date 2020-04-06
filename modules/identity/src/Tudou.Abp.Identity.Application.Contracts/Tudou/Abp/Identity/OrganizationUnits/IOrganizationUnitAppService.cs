using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tudou.Abp.Identity.OrganizationUnits
{
   public interface IOrganizationUnitAppService : IApplicationService
    {
        Task<ListResultDto<OrganizationUnitDto>> GetOrganizationUnits();
        //Task<PagedResultDto<OrganizationUnitUserListDto>> GetOrganizationUnitUsers(GetOrganizationUnitUsersInput input);
        //Task<PagedResultDto<OrganizationUnitRoleListDto>> GetOrganizationUnitRoles(GetOrganizationUnitRolesInput input);
        Task<OrganizationUnitDto> CreateOrganizationUnit(CreateOrganizationUnitInput input);
        Task<OrganizationUnitDto> MoveOrganizationUnit(Guid id, MoveOrganizationUnitInput input);
        Task<OrganizationUnitDto> UpdateOrganizationUnit(Guid id,UpdateOrganizationUnitInput input);
        Task DeleteOrganizationUnit(Guid id);
        Task RemoveUserFromOrganizationUnit(Guid id, RemoveUserFromOrganizationUnitInput input);

        Task RemoveRoleFromOrganizationUnit(Guid id, RemoveRoleFromOrganizationUnitInput input);

        Task AddUsersToOrganizationUnit(Guid id,UsersToOrganizationUnitInput input);

        Task AddRolesToOrganizationUnit(Guid id,RolesToOrganizationUnitInput input);
        Task<PagedResultDto<NameValueDto>> FindRoles(FindOrganizationUnitRolesInput input);

        Task<PagedResultDto<NameValueDto>> FindUsers(FindOrganizationUnitUsersInput input);
    }
}
