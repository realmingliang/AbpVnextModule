using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;

namespace Tudou.Abp.Identity.OrganizationUnits
{
    [Authorize(IdentityPermissions.OrganizationUnits.Default)]
    public class OrganizationUnitAppService : IdentityAppServiceBase, IOrganizationUnitAppService
    {
        private readonly OrganizationUnitManager OrganizationUnitManager;
        private readonly IOrganizationUnitRepository OrganizationUnitRepository;
        protected IdentityRoleManager RoleManager { get; }
        protected IIdentityRoleRepository RoleRepository { get; }
        protected IdentityUserManager UserManager { get; }
        protected IIdentityUserRepository UserRepository { get; }
        public OrganizationUnitAppService(
         OrganizationUnitManager organizationUnitManager,
          IOrganizationUnitRepository organizationUnitRepository, IdentityRoleManager roleManager,
            IIdentityRoleRepository roleRepository,
             IdentityUserManager userManager,
            IIdentityUserRepository userRepository)
        {
            OrganizationUnitManager = organizationUnitManager;
            OrganizationUnitRepository = organizationUnitRepository;
            RoleManager = roleManager;
            RoleRepository = roleRepository;
            UserManager = userManager;
            UserRepository = userRepository;
        }
        [Authorize(IdentityPermissions.OrganizationUnits.Create)]
        public async Task<OrganizationUnitDto> CreateOrganizationUnit(CreateOrganizationUnitInput input)
        {
            var organizationUnit = new OrganizationUnit(CurrentTenant.Id, input.DisplayName, input.ParentId);
            await OrganizationUnitManager.CreateAsync(organizationUnit);
            await CurrentUnitOfWork.SaveChangesAsync();
            return ObjectMapper.Map<OrganizationUnit, OrganizationUnitDto>(organizationUnit);
        }
        [Authorize(IdentityPermissions.OrganizationUnits.Delete)]
        public async Task DeleteOrganizationUnit(Guid id)
        {
            await OrganizationUnitRepository.DeleteAsync(id);
        }

        public async Task<PagedResultDto<NameValueDto>> FindRoles(FindOrganizationUnitRolesInput input)
        {
            var list = await RoleRepository.GetListByOrganizationUnitIdAsync(input.OrganizationUnitId, input.MaxResultCount, input.SkipCount, input.Filter);
            var roleCount = await RoleRepository.GetCountByOrganizationUnitIdAsync(input.OrganizationUnitId, input.Filter);
            return new PagedResultDto<NameValueDto>(
                 roleCount,
                 list.Select(u =>
                     new NameValueDto(
                         u.Name,
                         u.Id
                     )
                 ).ToList()
             );

        }

        public async Task<PagedResultDto<NameValueDto>> FindUsers(FindOrganizationUnitUsersInput input)
        {
            var list = await UserRepository.GetListByOrganizationUnitIdAsync(input.OrganizationUnitId, input.MaxResultCount, input.SkipCount, input.Filter);
            var userCount = await UserRepository.GetCountByOrganizationUnitIdAsync(input.OrganizationUnitId, input.Filter);
            return new PagedResultDto<NameValueDto>(
             userCount,
             list.Select(u =>
                 new NameValueDto(
                     u.Name,
                     u.Id
                 )
             ).ToList()
         );
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
        [Authorize(IdentityPermissions.OrganizationUnits.Update)]
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
