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
   
        public override string Name => OrganizationUnitPermissionValueProvider.ProviderName;
        public OrganizationUnitPermissionManagementPrivider(
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
