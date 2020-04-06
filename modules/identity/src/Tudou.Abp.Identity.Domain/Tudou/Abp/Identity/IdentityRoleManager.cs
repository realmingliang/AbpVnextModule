using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Services;
using Tudou.Abp.Identity.Localization;
using Volo.Abp;
using Volo.Abp.Threading;
using Volo.Abp.Domain.Repositories;
using Tudou.Abp.Identity.OrganizationUnits;

namespace Tudou.Abp.Identity
{
    public class IdentityRoleManager : RoleManager<IdentityRole>, IDomainService
    {
        protected override CancellationToken CancellationToken => CancellationTokenProvider.Token;

        protected IStringLocalizer<IdentityResource> Localizer { get; }
        protected ICancellationTokenProvider CancellationTokenProvider { get; }
        protected IOrganizationUnitRoleRepository OrganizationUnitRoleRepository { get; }

        public IdentityRoleManager(
            IdentityRoleStore store,
            IEnumerable<IRoleValidator<IdentityRole>> roleValidators,
            ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors,
            ILogger<IdentityRoleManager> logger,
            IOrganizationUnitRoleRepository organizationUnitRoleRepository,
            IStringLocalizer<IdentityResource> localizer,
            ICancellationTokenProvider cancellationTokenProvider)
            : base(
                  store, 
                  roleValidators, 
                  keyNormalizer, 
                  errors, 
                  logger)
        {
            Localizer = localizer;
            CancellationTokenProvider = cancellationTokenProvider;
            OrganizationUnitRoleRepository = organizationUnitRoleRepository;
        }

        public virtual async Task<IdentityRole> GetByIdAsync(Guid id)
        {
            var role = await Store.FindByIdAsync(id.ToString(), CancellationToken);
            if (role == null)
            {
                throw new EntityNotFoundException(typeof(IdentityRole), id);
            }

            return role;
        }

        public override async Task<IdentityResult> SetRoleNameAsync(IdentityRole role, string name)
        {
            if (role.IsStatic && role.Name != name)
            {
                throw new BusinessException(Localizer["Identity.StaticRoleRenamingErrorMessage"]); // TODO: localize & change exception type
            }

            return await base.SetRoleNameAsync(role, name);
        }
        public virtual async Task RemoveFromOrganizationUnitAsync(Guid roleId, Guid ouId)
        {
            await OrganizationUnitRoleRepository.DeleteAsync(uou => uou.RoleId == roleId && uou.OrganizationUnitId == ouId);
        }
        public virtual async Task AddToOrganizationUnitAsync(Guid roleId, Guid ouId)
        {
            var currentRu = await OrganizationUnitRoleRepository.FindAsync(t => t.RoleId == roleId && t.OrganizationUnitId == ouId);

            if (currentRu != null)
            {
                return;
            }
            var role = await Store.FindByIdAsync(roleId.ToString(), CancellationToken);
            await OrganizationUnitRoleRepository.InsertAsync(new OrganizationUnitRole(role.TenantId, roleId, ouId));
        }
        public override async Task<IdentityResult> DeleteAsync(IdentityRole role)
        {
            if (role.IsStatic)
            {
                throw new BusinessException(Localizer["Identity.StaticRoleDeletionErrorMessage"]); // TODO: localize & change exception type
            }

            return await base.DeleteAsync(role);
        }
    }
}
