using System;

namespace Tudou.Abp.OrganizationUnit
{
    public class RolesToOrganizationUnitInput
    {
        public Guid[] RoleIds { get; set; }

        public Guid OrganizationUnitId { get; set; }
    }
}