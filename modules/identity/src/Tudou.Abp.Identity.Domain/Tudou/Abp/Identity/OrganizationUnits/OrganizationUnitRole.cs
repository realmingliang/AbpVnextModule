using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Tudou.Abp.Identity.OrganizationUnits
{
    public class OrganizationUnitRole : CreationAuditedEntity, IMultiTenant
    {
        public virtual Guid? TenantId { get; protected set; }
        public virtual Guid RoleId { get; set; }
        public virtual Guid OrganizationUnitId { get; set; }

        public OrganizationUnitRole()
        {

        }
        public OrganizationUnitRole(Guid? tenantId, Guid roleId, Guid organizationUnitId)
        {
            TenantId = tenantId;
            RoleId = roleId;
            OrganizationUnitId = organizationUnitId;
        }
        public override object[] GetKeys()
        {
            return new object[] { OrganizationUnitId, RoleId };
        }
    }
}
