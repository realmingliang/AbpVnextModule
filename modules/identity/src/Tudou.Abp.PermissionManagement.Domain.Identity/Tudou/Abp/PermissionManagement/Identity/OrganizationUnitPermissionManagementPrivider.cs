using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tudou.Abp.Identity.OrganizationUnits;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Guids;
using Volo.Abp.MultiTenancy;
using Volo.Abp.PermissionManagement;

namespace Tudou.Abp.PermissionManagement.Identity
{
   public class OrganizationUnitPermissionManagementPrivider : PermissionManagementProvider
    {
        public const string ProviderName = "O";
        protected IOrganizationUnitRoleFinder OrganizationUnitRoleFinder { get; }
        public override string Name => ProviderName;
        public OrganizationUnitPermissionManagementPrivider(
        IPermissionGrantRepository permissionGrantRepository,
        IGuidGenerator guidGenerator,
        ICurrentTenant currentTenant,
        IOrganizationUnitRoleFinder organizationUnitRoleFinder)
        : base(
            permissionGrantRepository,
            guidGenerator,
            currentTenant)
        {
            OrganizationUnitRoleFinder = organizationUnitRoleFinder;
        }
        public override async Task<PermissionValueProviderGrantInfo> CheckAsync(string name, string providerName, string providerKey)
        {
            if (providerName == Name)
            {
                return new PermissionValueProviderGrantInfo(
                    await PermissionGrantRepository.FindAsync(name, providerName, providerKey) != null,
                    providerKey
                );
            }
            if (providerName == RolePermissionValueProvider.ProviderName)
            {
                var roleId = Guid.Parse(providerKey);
                var roleNames = await OrganizationUnitRoleFinder.GetRolesAsync(roleId);

                foreach (var roleName in roleNames)
                {
                    var permissionGrant = await PermissionGrantRepository.FindAsync(name, Name, roleName);
                    if (permissionGrant != null)
                    {
                        return new PermissionValueProviderGrantInfo(true, roleName);
                    }
                }
            }
            return PermissionValueProviderGrantInfo.NonGranted;
        }
    }
}
