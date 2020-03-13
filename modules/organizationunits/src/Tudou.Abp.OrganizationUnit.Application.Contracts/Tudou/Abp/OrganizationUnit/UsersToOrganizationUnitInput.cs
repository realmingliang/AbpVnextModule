using System;

namespace Tudou.Abp.OrganizationUnit
{
    public class UsersToOrganizationUnitInput
    {
        public Guid[] UserIds { get; set; }

        public Guid OrganizationUnitId { get; set; }
    }
}