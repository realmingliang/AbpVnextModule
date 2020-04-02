using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Guids;
using Volo.Abp.MultiTenancy;
using Volo.Abp.PermissionManagement;

namespace Tudou.Abp.PermissionManagement.IdentityServer
{
    public class ClientPermissionManagementProvider : PermissionManagementProvider
    {
        public override string Name => ClientPermissionValueProvider.ProviderName;

        public ClientPermissionManagementProvider(
            IPermissionGrantRepository permissionGrantRepository,
            IGuidGenerator guidGenerator,
            ICurrentTenant currentTenant)
            : base(
                permissionGrantRepository,
                guidGenerator,
                currentTenant)
        {

        }
    }
}
