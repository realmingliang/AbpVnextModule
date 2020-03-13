using Microsoft.AspNetCore.Authorization;
using System;
using System.Linq;
using System.Threading.Tasks;
using Tudou.Abp.OrganizationUnit.Authorization;
using Volo.Abp.Application.Dtos;

namespace Tudou.Abp.OrganizationUnit
{
    [Authorize(OrganizationUnitPermissions.OrganizationUnit.Default)]
    public class OrganizationUnitAppService : OrganizationUnitAppServiceBase, IOrganizationUnitAppService
    {
        private readonly IOrganizationUnitRepository _organizationUnitRepository;
        private readonly IOrganizationUnitUserRepository _organizationUnitUserRepository;
        private readonly IOrganizationUnitRoleRepository _organizationUnitRoleRepository;
        private readonly OrganizationUnitManager _organizationUnitManager;
        public OrganizationUnitAppService(IOrganizationUnitRepository organizationUnitRepository,
            OrganizationUnitManager organizationUnitManager,
            IOrganizationUnitUserRepository organizationUnitUserRepository,
            IOrganizationUnitRoleRepository organizationUnitRoleRepository)
        {
            _organizationUnitRepository = organizationUnitRepository;
            _organizationUnitManager = organizationUnitManager;
            _organizationUnitUserRepository = organizationUnitUserRepository;
            _organizationUnitRoleRepository = organizationUnitRoleRepository;
        }
        [Authorize(OrganizationUnitPermissions.OrganizationUnit.Create)]
        public async Task<OrganizationUnitDto> CreateOrganizationUnitAsync(CreateOrganizationUnitInput input)
        {
            var organizationUnit = new OrganizationUnit(GuidGenerator.Create(), CurrentTenant.Id, input.DisplayName, input.ParentId);

            await _organizationUnitManager.CreateAsync(organizationUnit);
            await CurrentUnitOfWork.SaveChangesAsync();

            return ObjectMapper.Map<OrganizationUnit, OrganizationUnitDto>(organizationUnit);
        }
        [Authorize(OrganizationUnitPermissions.OrganizationUnit.Delete)]
        public async Task DeleteOrganizationUnitAsync(Guid id)
        {
            await _organizationUnitManager.DeleteAsync(id);
        }
        [Authorize(OrganizationUnitPermissions.OrganizationUnit.Default)]
        public async Task<ListResultDto<OrganizationUnitDto>> GetOrganizationUnitsAsync()
        {
            var organizationUnits = await _organizationUnitRepository.GetListAsync();
            var organizationUnitMemberCounts = (await _organizationUnitUserRepository.GetListAsync())
                 .GroupBy(x => x.OrganizationUnitId)
                .Select(groupedUsers => new
                {
                    organizationUnitId = groupedUsers.Key,
                    count = groupedUsers.Count()
                }).ToDictionary(x => x.organizationUnitId, y => y.count);
            var organizationUnitRoleCounts = (await _organizationUnitRoleRepository.GetListAsync())
                .GroupBy(x => x.OrganizationUnitId)
                .Select(groupedRoles => new
                {
                    organizationUnitId = groupedRoles.Key,
                    count = groupedRoles.Count()
                }).ToDictionary(x => x.organizationUnitId, y => y.count);

            return new ListResultDto<OrganizationUnitDto>(
                organizationUnits.Select(ou =>
                {
                    var organizationUnitDto = ObjectMapper.Map<OrganizationUnit, OrganizationUnitDto>(ou);
                    organizationUnitDto.MemberCount = organizationUnitMemberCounts.ContainsKey(ou.Id) ? organizationUnitMemberCounts[ou.Id] : 0;
                    organizationUnitDto.RoleCount = organizationUnitRoleCounts.ContainsKey(ou.Id) ? organizationUnitRoleCounts[ou.Id] : 0;
                    return organizationUnitDto;
                }).ToList());
        }
        [Authorize(OrganizationUnitPermissions.OrganizationUnit.Move)]
        public async Task<OrganizationUnitDto> MoveOrganizationUnitAsync(Guid id,MoveOrganizationUnitInput input)
        {
            await _organizationUnitManager.MoveAsync(id, input.NewParentId);

            return await CreateOrganizationUnitDto(
                await _organizationUnitRepository.GetAsync(id)
                );
        }
        [Authorize(OrganizationUnitPermissions.OrganizationUnit.Update)]
        public async Task<OrganizationUnitDto> UpdateOrganizationUnitAsync(Guid id, UpdateOrganizationUnitInput input)
        {
            var organizationUnit = await _organizationUnitRepository.GetAsync(id);

            organizationUnit.Name = input.Name;

            await _organizationUnitManager.UpdateAsync(organizationUnit);

            return await CreateOrganizationUnitDto(organizationUnit);
        }
        private async Task<OrganizationUnitDto> CreateOrganizationUnitDto(OrganizationUnit organizationUnit)
        {
            var dto = ObjectMapper.Map<OrganizationUnit, OrganizationUnitDto>(organizationUnit);
            dto.MemberCount = await _organizationUnitUserRepository.GetCountAsync(organizationUnit.Id).ConfigureAwait(false);
            dto.RoleCount = await _organizationUnitRoleRepository.GetCountAsync(organizationUnit.Id).ConfigureAwait(false);
            return dto;
        }
    }
}
