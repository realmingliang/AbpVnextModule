using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Tudou.Abp.OrganizationUnit
{
    public class OrganizationUnitUser : CreationAuditedEntity<Guid>, IMultiTenant, ISoftDelete
    {
        public OrganizationUnitUser()
        {

        }
        public OrganizationUnitUser(Guid? tenantId, [NotNull]Guid userId, [NotNull]Guid organizationUnitId)
        {
            Check.NotNull(userId, nameof(userId));
            Check.NotNull(organizationUnitId, nameof(organizationUnitId));

            TenantId = tenantId;
            UserId = userId;
            OrganizationUnitId = organizationUnitId;

        }
        public Guid? TenantId { get; protected set; }
        public virtual Guid UserId { get; set; }
        public virtual Guid OrganizationUnitId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
