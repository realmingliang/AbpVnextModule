using System;
using System.Threading.Tasks;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Guids;
using Tudou.Abp.Identity;
using Volo.Abp.MultiTenancy;
using Volo.Abp.PermissionManagement;
using Tudou.Abp.Identity.OrganizationUnits;

namespace Tudou.Abp.PermissionManagement.Identity
{
    public class RolePermissionManagementProvider : PermissionManagementProvider
    {
        public override string Name => RolePermissionValueProvider.ProviderName;
        protected IOrganizationUnitRoleFinder OrganizationUnitRoleFinder { get; }
        protected IUserRoleFinder UserRoleFinder { get; }

        public RolePermissionManagementProvider(
            IPermissionGrantRepository permissionGrantRepository,
            IGuidGenerator guidGenerator,
            ICurrentTenant currentTenant, 
            IUserRoleFinder userRoleFinder, IOrganizationUnitRoleFinder organizationUnitRoleFinder)
            : base(
                permissionGrantRepository,
                guidGenerator,
                currentTenant)
        {
            UserRoleFinder = userRoleFinder;
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

            if (providerName == UserPermissionValueProvider.ProviderName)
            {
                var userId = Guid.Parse(providerKey);
                var roleNames = await UserRoleFinder.GetRolesAsync(userId);

                foreach (var roleName in roleNames)
                {
                    var permissionGrant = await PermissionGrantRepository.FindAsync(name, Name, roleName);
                    if (permissionGrant != null)
                    {
                        return new PermissionValueProviderGrantInfo(true, roleName);
                    }
                }
            }
            if (providerName == OrganizationUnitPermissionValueProvider.ProviderName)
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