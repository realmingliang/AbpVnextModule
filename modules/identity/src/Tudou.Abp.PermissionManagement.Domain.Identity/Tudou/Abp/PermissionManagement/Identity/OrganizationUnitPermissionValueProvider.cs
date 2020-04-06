using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Security.Claims;

namespace Tudou.Abp.Identity.OrganizationUnits
{
   public class OrganizationUnitPermissionValueProvider: PermissionValueProvider
    {
        public const string ProviderName = "O";
        public override string Name => ProviderName;
        protected IOrganizationUnitRepository OrganizationUnitRepository { get; }

        public OrganizationUnitPermissionValueProvider(IPermissionStore permissionStore,
            IOrganizationUnitRepository organizationUnitRepository)
            : base(permissionStore)
        {
            OrganizationUnitRepository = organizationUnitRepository;
        }
        public override async Task<PermissionGrantResult> CheckAsync(PermissionValueCheckContext context)
        {
            var userId = context.Principal?.FindFirst(AbpClaimTypes.UserId)?.Value;
            var roleNames = await OrganizationUnitRepository.GetCurrentUserRoleNamesByOrganizationUnitAsync(Guid.Parse(userId));
            if (roleNames == null || !roleNames.Any())
            {
                return PermissionGrantResult.Undefined;
            }
            foreach (var roleName in roleNames)
            {
                if (await PermissionStore.IsGrantedAsync(context.Permission.Name, Name, roleName))
                {
                    return PermissionGrantResult.Granted;
                }
            }

            return PermissionGrantResult.Undefined;
        }

    }
}
