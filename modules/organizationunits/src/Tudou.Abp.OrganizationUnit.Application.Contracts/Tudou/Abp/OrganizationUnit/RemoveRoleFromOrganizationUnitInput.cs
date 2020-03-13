using System;

namespace Tudou.Abp.OrganizationUnit
{
    public class RemoveRoleFromOrganizationUnitInput
    {
        public Guid RoleId { get; set; }
        public Guid OrganizationUnitId { get; set; }
    }
}