using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Services;
using Volo.Abp.Identity;
using Volo.Abp.Threading;
using Volo.Abp.Uow;

namespace Tudou.Abp.OrganizationUnit
{
    public class OrganizationUnitManager : IDomainService
    {
        private readonly IOrganizationUnitRepository _organizationUnitRepository;
        private readonly IOrganizationUnitUserRepository _organizationUnitUserRepository;
        private readonly IOrganizationUnitRoleRepository _organizationUnitRoleRepository;
        protected CancellationToken CancellationToken => _cancellationTokenProvider.Token;
        private readonly ICancellationTokenProvider _cancellationTokenProvider;
        private readonly IdentityRoleManager _roleManager;
        private readonly IdentityUserManager _userManager;
        public OrganizationUnitManager(
            IOrganizationUnitRepository organizationUnitRepository,
          ICancellationTokenProvider cancellationTokenProvider,
              IOrganizationUnitUserRepository organizationUnitUserRepository,
              IdentityRoleManager roleManager,
              IdentityUserManager userManager,
            IOrganizationUnitRoleRepository organizationUnitRoleRepository)
        {
            _organizationUnitRepository = organizationUnitRepository;
            _cancellationTokenProvider = cancellationTokenProvider;
            _organizationUnitUserRepository = organizationUnitUserRepository;
            _roleManager = roleManager;
            _userManager = userManager;
            _organizationUnitRoleRepository = organizationUnitRoleRepository;
        }
        public virtual async Task<OrganizationUnit> GetByIdAsync(Guid id)
        {
            var organizationUnit = await _organizationUnitRepository.GetAsync(id).ConfigureAwait(false);
            if (organizationUnit == null)
            {
                throw new EntityNotFoundException(typeof(OrganizationUnit), id);
            }

            return organizationUnit;
        }
        public virtual async Task AddUserToOrganizationUnitAsync(Guid userId, Guid ouId, Guid? tenantId)
        {
            if (await IsUserInOrganizationUnitAsync(userId, ouId))
            {
                return;
            }
            await _organizationUnitUserRepository.InsertAsync(new OrganizationUnitUser(tenantId, userId, ouId));
        }
        public virtual async Task AddRoleToOrganizationUnitAsync(Guid roleId, Guid ouId, Guid? tenantId)
        {
            if (await IsRoleInOrganizationUnitAsync(roleId, ouId))
            {
                return;
            }
            await _organizationUnitRoleRepository.InsertAsync(new OrganizationUnitRole(tenantId, roleId, ouId));
        }
        public virtual async Task<bool> IsUserInOrganizationUnitAsync(Guid userId, Guid ouId)
        {
            return await _organizationUnitUserRepository.FindByOrganizationUnitIdAndUserIdAsync(ouId, userId
                   ) != null;
        }
        public virtual async Task<bool> IsRoleInOrganizationUnitAsync(Guid roleId, Guid ouId)
        {
            return await _organizationUnitRoleRepository.FindByOrganizationUnitIdAndRoleIdAsync(ouId, roleId
                   ) != null;
        }
        public virtual async Task UpdateAsync(OrganizationUnit organizationUnit)
        {
            await ValidateOrganizationUnitAsync(organizationUnit);
            await _organizationUnitRepository.UpdateAsync(organizationUnit);
        }
        public async Task CreateAsync(OrganizationUnit organizationUnit)
        {
            organizationUnit.Code = await GetNextChildCodeAsync(organizationUnit.ParentId);
            await ValidateOrganizationUnitAsync(organizationUnit);
            await _organizationUnitRepository.InsertAsync(organizationUnit);
        }
        public virtual async Task<string> GetNextChildCodeAsync(Guid? parentId)
        {
            var lastChild = await GetLastChildOrNullAsync(parentId);
            if (lastChild == null)
            {
                var parentCode = parentId != null ? await GetCodeAsync(parentId.Value) : null;
                return OrganizationUnit.AppendCode(parentCode, OrganizationUnit.CreateCode(1));
            }

            return OrganizationUnit.CalculateNextCode(lastChild.Code);
        }
        public virtual async Task<OrganizationUnit> GetLastChildOrNullAsync(Guid? parentId)
        {
            var children = await _organizationUnitRepository.GetListAsync();
            return children.OrderBy(c => c.Code).LastOrDefault();
        }
        public virtual async Task<string> GetCodeAsync(Guid id)
        {
            return (await _organizationUnitRepository.GetAsync(id)).Code;
        }
        [UnitOfWork]
        public virtual async Task MoveAsync(Guid id, Guid? parentId)
        {
            var organizationUnit = await _organizationUnitRepository.GetAsync(id);
            if (organizationUnit.ParentId == parentId)
            {
                return;
            }

            var children = await FindChildrenAsync(id, true);

            var oldCode = organizationUnit.Code;

            organizationUnit.Code = await GetNextChildCodeAsync(parentId);
            organizationUnit.ParentId = parentId;

            await ValidateOrganizationUnitAsync(organizationUnit);

            foreach (var child in children)
            {
                child.Code = OrganizationUnit.AppendCode(organizationUnit.Code, OrganizationUnit.GetRelativeCode(child.Code, oldCode));
            }
        }
        public async Task RemoveUserFromOrganizationUnitAsync(Guid userId, Guid organizationUnitId)
        {
            await RemoveUserFromOrganizationUnitAsync(
                await _userManager.GetByIdAsync(userId),
                await _organizationUnitRepository.GetAsync(organizationUnitId)
            );
        }
        public virtual async Task RemoveUserFromOrganizationUnitAsync(IdentityUser user, OrganizationUnit ou)
        {
            var entity = await _organizationUnitUserRepository.FindByOrganizationUnitIdAndUserIdAsync(ou.Id, user.Id);
            if (entity != null)
            {
                await _organizationUnitUserRepository.DeleteAsync(entity);
            }
        }
        public async Task RemoveRoleFromOrganizationUnitAsync(Guid roleId, Guid organizationUnitId)
        {
            await RemoveRoleFromOrganizationUnitAsync(
                await _roleManager.GetByIdAsync(roleId),
                await _organizationUnitRepository.GetAsync(organizationUnitId)
            );
        }
        public virtual async Task RemoveRoleFromOrganizationUnitAsync(IdentityRole role, OrganizationUnit ou)
        {
            var entity = await _organizationUnitRoleRepository.FindByOrganizationUnitIdAndRoleIdAsync(ou.Id, role.Id);
            if (entity != null)
            {
                await _organizationUnitRoleRepository.DeleteAsync(entity);
            }
        }
        protected virtual async Task ValidateOrganizationUnitAsync(OrganizationUnit organizationUnit)
        {
            var siblings = (await FindChildrenAsync(organizationUnit.ParentId))
                .Where(ou => ou.Id != organizationUnit.Id)
                .ToList();

            if (siblings.Any(ou => ou.Name == organizationUnit.Name))
            {
                throw new UserFriendlyException("组织机构名已存在:" + organizationUnit.Name);
            }
        }
        [UnitOfWork]
        public virtual async Task DeleteAsync(Guid id)
        {
            var children = await FindChildrenAsync(id, true);

            foreach (var child in children)
            {
                await _organizationUnitRepository.DeleteAsync(child);
            }

            await _organizationUnitRepository.DeleteAsync(id);
        }
        public async Task<List<OrganizationUnit>> FindChildrenAsync(Guid? parentId, bool recursive = false)
        {
            return await _organizationUnitRepository.FindChildrenAsync(parentId, recursive).ConfigureAwait(false);
        }
    }
}
