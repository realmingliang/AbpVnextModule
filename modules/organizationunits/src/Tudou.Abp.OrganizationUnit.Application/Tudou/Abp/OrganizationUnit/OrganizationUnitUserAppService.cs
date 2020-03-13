using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tudou.Abp.OrganizationUnit.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Identity;

namespace Tudou.Abp.OrganizationUnit
{
    [Authorize(OrganizationUnitPermissions.OrganizationUnitUser.Default)]
    public class OrganizationUnitUserAppService : OrganizationUnitAppServiceBase, IOrganizationUnitUserAppService
    {
        private readonly IOrganizationUnitUserRepository _organizationUnitUserRepository;
        private readonly OrganizationUnitManager _organizationUnitManager;
        private readonly IdentityUserManager _userManager;
        public OrganizationUnitUserAppService(
            OrganizationUnitManager organizationUnitManager,
            IOrganizationUnitUserRepository organizationUnitUserRepository,
            IdentityUserManager userManager)
        {
            _organizationUnitManager = organizationUnitManager;
            _organizationUnitUserRepository = organizationUnitUserRepository;
            _userManager = userManager;
        }
        [Authorize(OrganizationUnitPermissions.OrganizationUnitUser.AddUsersToOrganizationUnit)]
        public async Task AddUsersToOrganizationUnitAsync(UsersToOrganizationUnitInput input)
        {
            foreach (var roleId in input.UserIds)
            {
                await _organizationUnitManager.AddUserToOrganizationUnitAsync(roleId, input.OrganizationUnitId, CurrentTenant.Id);
            }
        }
        [Authorize(OrganizationUnitPermissions.OrganizationUnitUser.FindUsers)]
        public async Task<PagedResultDto<NameValueDto>> FindUsersAsync(FindOrganizationUnitUsersInput input)
        {
            var userIdsInOrganizationUnit = (await _organizationUnitUserRepository.FindOrganizationUnitUsersAsync(input.OrganizationUnitId).ConfigureAwait(false))
               .Select(uou => uou.UserId);
            var query = _userManager.Users
                  .Where(u => !userIdsInOrganizationUnit.Contains(u.Id))
                  .WhereIf(
                    !input.Filter.IsNullOrWhiteSpace(),
                    u =>
                        u.UserName.Contains(input.Filter) ||
                        u.Name.Contains(input.Filter)
                );
            var userCount = await _organizationUnitUserRepository.GetCountAsync();
            var users = query
                .OrderBy(u => u.Name)
                .PageBy(input)
                .ToList();
            return new PagedResultDto<NameValueDto>(
                userCount,
                users.Select(u =>
                    new NameValueDto(
                        u.Name,
                        u.Id.ToString()
                    )
                ).ToList()
            );
        }
        [Authorize(OrganizationUnitPermissions.OrganizationUnitUser.Default)]
        public async Task<PagedResultDto<OrganizationUnitUserListDto>> GetOrganizationUnitUsersAsync(GetOrganizationUnitUsersInput input)
        {
            var query = from ouUser in await _organizationUnitUserRepository
                       .FindOrganizationUnitUsersAsync(input.Id, input.MaxResultCount, input.SkipCount).ConfigureAwait(false)
                        join user in _userManager.Users on ouUser.UserId equals user.Id
                        where ouUser.OrganizationUnitId == input.Id
                        select new
                        {
                            ouUser,
                            user
                        };
            var totalCount = await _organizationUnitUserRepository.GetCountAsync(input.Id);
            var items = query.ToList();

            return new PagedResultDto<OrganizationUnitUserListDto>(
                totalCount,
                items.Select(item =>
                {
                    var organizationUnitUserDto = ObjectMapper.Map<IdentityUser, OrganizationUnitUserListDto>(item.user);
                    organizationUnitUserDto.AddedTime = item.ouUser.CreationTime;
                    return organizationUnitUserDto;
                }).ToList());
        }
        [Authorize(OrganizationUnitPermissions.OrganizationUnitUser.RemoveUserFromOrganizationUnit)]
        public async Task RemoveUserFromOrganizationUnitAsync(RemoveUserFromOrganizationUnitInput input)
        {
            await _organizationUnitManager.RemoveUserFromOrganizationUnitAsync(input.UserId, input.OrganizationUnitId);
        }
    }
}
