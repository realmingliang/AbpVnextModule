using System;

namespace Tudou.Abp.OrganizationUnit
{
    public class CreateOrganizationUnitInput
    {
        public Guid? ParentId{ get; set; }
        public string DisplayName { get; set; }
    }
}