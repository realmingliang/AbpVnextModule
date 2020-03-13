using System;

namespace Tudou.Abp.OrganizationUnit
{
    public class RemoveUserFromOrganizationUnitInput
    {
        public Guid UserId { get; set; }
        public Guid OrganizationUnitId { get; set; }
    }
}