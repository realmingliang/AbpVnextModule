using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Tudou.Abp.OrganizationUnit
{
   public class OrganizationUnitRole : CreationAuditedEntity<Guid>, IMultiTenant,ISoftDelete
    {
        public OrganizationUnitRole()
        {
        }
        public OrganizationUnitRole(Guid? tenantId, [NotNull]Guid roleId, [NotNull]Guid organizationUnitId)
        {
            Check.NotNull(roleId, nameof(roleId));
            Check.NotNull(organizationUnitId, nameof(organizationUnitId));

            TenantId = tenantId;
            RoleId = roleId;
            OrganizationUnitId = organizationUnitId;
        }
        public Guid? TenantId { get; protected set; }

        public virtual Guid RoleId { get; set; }
        public virtual Guid OrganizationUnitId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
