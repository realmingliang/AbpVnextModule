using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Tudou.Abp.Identity.OrganizationUnits
{
    public class OrganizationUnitAppService : IdentityAppServiceBase, IOrganizationUnitAppService
    {
        private readonly OrganizationUnitManager OrganizationUnitManager;
        private readonly IOrganizationUnitRepository OrganizationUnitRepository;
        public OrganizationUnitAppService(
         OrganizationUnitManager organizationUnitManager,
          IOrganizationUnitRepository organizationUnitRepository)
        {
            OrganizationUnitManager = organizationUnitManager;
            OrganizationUnitRepository = organizationUnitRepository;
        }
        public async Task<OrganizationUnitDto> CreateOrganizationUnit(CreateOrganizationUnitInput input)
        {
            var organizationUnit = new OrganizationUnit(CurrentTenant.Id, input.DisplayName, input.ParentId);
            await OrganizationUnitManager.CreateAsync(organizationUnit);
            await CurrentUnitOfWork.SaveChangesAsync();
            return ObjectMapper.Map<OrganizationUnit, OrganizationUnitDto>(organizationUnit);
        }

        public async Task DeleteOrganizationUnit(Guid id)
        {
            await OrganizationUnitRepository.DeleteAsync(id);
        }

        public async Task<ListResultDto<OrganizationUnitDto>> GetOrganizationUnits()
        {
            var organizationUnits = await OrganizationUnitRepository.GetListAsync();
            var organizationUnitMemberCounts = await OrganizationUnitRepository.GetAllMemberCountAsync();

            var organizationUnitRoleCounts = await OrganizationUnitRepository.GetAllRoleCountAsync();
            return new ListResultDto<OrganizationUnitDto>(
             organizationUnits.Select(ou =>
             {
              var organizationUnitDto = ObjectMapper.Map<OrganizationUnit, OrganizationUnitDto>(ou);
              organizationUnitDto.MemberCount = organizationUnitMemberCounts.ContainsKey(ou.Id) ? organizationUnitMemberCounts[ou.Id] : 0;
              organizationUnitDto.RoleCount = organizationUnitRoleCounts.ContainsKey(ou.Id) ? organizationUnitRoleCounts[ou.Id] : 0;
              return organizationUnitDto;
            }).ToList());
        }

        public async Task<OrganizationUnitDto> UpdateOrganizationUnit(Guid id, UpdateOrganizationUnitInput input)
        {
            var organizationUnit = await OrganizationUnitRepository.GetAsync(id);

            organizationUnit.DisplayName = input.DisplayName;

            await OrganizationUnitManager.UpdateAsync(organizationUnit);

            return await CreateOrganizationUnitDto(organizationUnit);
        }
        private async Task<OrganizationUnitDto> CreateOrganizationUnitDto(OrganizationUnit organizationUnit)
        {
            var dto = ObjectMapper.Map<OrganizationUnit, OrganizationUnitDto>(organizationUnit);
            dto.MemberCount = await OrganizationUnitRepository.GetMemberCountAsync(organizationUnit.Id);
            dto.RoleCount = await OrganizationUnitRepository.GetRoleCountAsync(organizationUnit.Id);
            return dto;
        }
    }
}
