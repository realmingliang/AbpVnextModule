using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

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
        protected IOrganizationUnitRoleRepository OrganizationUnitRoleRepository { get; }
        protected IdentityUserOrganizationUnitRepository UserOrganizationUnitRepository { get; }
        protected IIdentityUserRepository UserRepository { get; }
        public OrganizationUnitAppService(
         OrganizationUnitManager organizationUnitManager,
          IOrganizationUnitRepository organizationUnitRepository, IdentityRoleManager roleManager,
            IIdentityRoleRepository roleRepository,
            IOrganizationUnitRoleRepository organizationUnitRoleRepository,
             IdentityUserOrganizationUnitRepository userOrganizationUnitRepository,
             IdentityUserManager userManager,
         
            IIdentityUserRepository userRepository)
        {
            OrganizationUnitManager = organizationUnitManager;
            OrganizationUnitRepository = organizationUnitRepository;
            UserOrganizationUnitRepository = userOrganizationUnitRepository;
            RoleManager = roleManager;
            OrganizationUnitRoleRepository = organizationUnitRoleRepository;
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
        [Authorize(IdentityPermissions.OrganizationUnits.ManageRoles)]
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
        [Authorize(IdentityPermissions.OrganizationUnits.ManageMembers)]
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
        [Authorize(IdentityPermissions.OrganizationUnits.Move)]
        public async Task<OrganizationUnitDto> MoveOrganizationUnit(Guid id, MoveOrganizationUnitInput input)
        {
            await OrganizationUnitManager.MoveAsync(id, input.NewParentId);

            return await CreateOrganizationUnitDto(
                await OrganizationUnitRepository.GetAsync(id)
                );
        }
        [Authorize(IdentityPermissions.OrganizationUnits.ManageMembers)]
        public async Task RemoveUserFromOrganizationUnit(Guid id, RemoveUserFromOrganizationUnitInput input)
        {
            await UserManager.RemoveFromOrganizationUnitAsync(input.UserId, id);
        }
        [Authorize(IdentityPermissions.OrganizationUnits.ManageRoles)]
        public async Task RemoveRoleFromOrganizationUnit(Guid id, RemoveRoleFromOrganizationUnitInput input)
        {
            await RoleManager.RemoveFromOrganizationUnitAsync(input.RoleId, id);
        }
        [Authorize(IdentityPermissions.OrganizationUnits.ManageMembers)]
        public async Task AddUsersToOrganizationUnit(Guid id, UsersToOrganizationUnitInput input)
        {
            foreach (var userId in input.UserIds)
            {
                await UserManager.AddToOrganizationUnitAsync(userId, id);
            }
        }
        [Authorize(IdentityPermissions.OrganizationUnits.ManageRoles)]
        public async Task AddRolesToOrganizationUnit(Guid id, RolesToOrganizationUnitInput input)
        {
            foreach (var roleId in input.RoleIds)
            {
                await RoleManager.AddToOrganizationUnitAsync(roleId, id);
            }
        }

        public async Task<PagedResultDto<OrganizationUnitUserListDto>> GetOrganizationUnitUsers(Guid id, PagedResultRequestDto input)
        {
            var items = from ouUser in await UserOrganizationUnitRepository.GetOuUserListAsync(id, input.MaxResultCount, input.SkipCount)
                        join user in await UserRepository.GetListAsync() on ouUser.UserId equals user.Id
                        select new
                        {
                            ouUser,
                            user
                        };
            var totalCount = await UserOrganizationUnitRepository.GetOuUserCountAsync(id);
            return new PagedResultDto<OrganizationUnitUserListDto>(
              totalCount,
              items.Select(item =>
              {
                  var organizationUnitRoleDto = ObjectMapper.Map<IdentityUser, OrganizationUnitUserListDto>(item.user);
                  organizationUnitRoleDto.AddedTime = item.ouUser.CreationTime;
                  return organizationUnitRoleDto;
              }).ToList());
        }

        public async Task<PagedResultDto<OrganizationUnitRoleListDto>> GetOrganizationUnitRoles(Guid id, PagedResultRequestDto input)
        {
            var items = from ouRole in await OrganizationUnitRoleRepository.GetOuRoleListAsync(id, input.MaxResultCount, input.SkipCount)
                        join role in await RoleRepository.GetListAsync() on ouRole.RoleId equals role.Id
                        select new
                        {
                            ouRole,
                            role
                        };
            var totalCount = await OrganizationUnitRoleRepository.GetOuRoleCountAsync(id);
            return new PagedResultDto<OrganizationUnitRoleListDto>(
              totalCount,
              items.Select(item =>
              {
                  var organizationUnitRoleDto = ObjectMapper.Map<IdentityRole,OrganizationUnitRoleListDto>(item.role);
                  organizationUnitRoleDto.AddedTime = item.ouRole.CreationTime;
                  return organizationUnitRoleDto;
              }).ToList());
        }
    }
}
