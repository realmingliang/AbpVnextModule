using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tudou.Abp.OrganizationUnit.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Identity;

namespace Tudou.Abp.OrganizationUnit
{
    [Authorize(OrganizationUnitPermissions.OrganizationUnitRole.Default)]
    public class OrganizationUnitRoleAppService : OrganizationUnitAppServiceBase, IOrganizationUnitRoleAppService
    {
        private readonly IOrganizationUnitRoleRepository _organizationUnitRoleRepository;
        private readonly OrganizationUnitManager _organizationUnitManager;
        private readonly IdentityRoleManager _roleManager;
        public OrganizationUnitRoleAppService(
            OrganizationUnitManager organizationUnitManager,
            IOrganizationUnitRoleRepository organizationUnitRoleRepository,
            IdentityRoleManager roleManager)
        {
            _organizationUnitManager = organizationUnitManager;
            _organizationUnitRoleRepository = organizationUnitRoleRepository;
            _roleManager = roleManager;
        }
        [Authorize(OrganizationUnitPermissions.OrganizationUnitRole.AddRolesToOrganizationUnit)]

        public async Task AddRolesToOrganizationUnitAsync(RolesToOrganizationUnitInput input)
        {
            foreach (var roleId in input.RoleIds)
            {
                await _organizationUnitManager.AddRoleToOrganizationUnitAsync(roleId, input.OrganizationUnitId, CurrentTenant.Id);
            }
        }
        [Authorize(OrganizationUnitPermissions.OrganizationUnitRole.FindRoles)]
        public async Task<PagedResultDto<NameValueDto>> FindRolesAsync(FindOrganizationUnitRolesInput input)
        {
            var roleIdsInOrganizationUnit = (await _organizationUnitRoleRepository.FindOrganizationUnitRolesAsync(input.OrganizationUnitId).ConfigureAwait(false))
               .Select(uou => uou.RoleId);
            var query = _roleManager.Roles
                  .Where(u => !roleIdsInOrganizationUnit.Contains(u.Id))
                  .WhereIf(
                    !input.Filter.IsNullOrWhiteSpace(),
                    u =>
                        u.NormalizedName.Contains(input.Filter) ||
                        u.Name.Contains(input.Filter)
                );
            var roleCount = await _organizationUnitRoleRepository.GetCountAsync();
            var roles = query
                .OrderBy(u => u.Name)
                .PageBy(input)
                .ToList();
            return new PagedResultDto<NameValueDto>(
                roleCount,
                roles.Select(u =>
                    new NameValueDto(
                        u.Name,
                        u.Id.ToString()
                    )
                ).ToList()
            );
        }
        [Authorize(OrganizationUnitPermissions.OrganizationUnitRole.Default)]
        public async Task<PagedResultDto<OrganizationUnitRoleListDto>> GetOrganizationUnitRolesAsync(GetOrganizationUnitRolesInput input)
        {
            var query = from ouRole in await _organizationUnitRoleRepository
                        .FindOrganizationUnitRolesAsync(input.Id, input.MaxResultCount, input.SkipCount).ConfigureAwait(false)
                        join role in _roleManager.Roles on ouRole.RoleId equals role.Id
                        where ouRole.OrganizationUnitId == input.Id
                        select new
                        {
                            ouRole,
                            role
                        };
            var totalCount = await _organizationUnitRoleRepository.GetCountAsync(input.Id);
            var items = query.ToList();

            return new PagedResultDto<OrganizationUnitRoleListDto>(
                totalCount,
                items.Select(item =>
                {
                    var organizationUnitRoleDto = ObjectMapper.Map<IdentityRole, OrganizationUnitRoleListDto>(item.role);
                    organizationUnitRoleDto.AddedTime = item.ouRole.CreationTime;
                    return organizationUnitRoleDto;
                }).ToList());
        }
        [Authorize(OrganizationUnitPermissions.OrganizationUnitRole.RemoveRoleFromOrganizationUnit)]
        public async Task RemoveRoleFromOrganizationUnitAsync(RemoveRoleFromOrganizationUnitInput input)
        {
            await _organizationUnitManager.RemoveRoleFromOrganizationUnitAsync(input.RoleId, input.OrganizationUnitId);
        }
    }
}
