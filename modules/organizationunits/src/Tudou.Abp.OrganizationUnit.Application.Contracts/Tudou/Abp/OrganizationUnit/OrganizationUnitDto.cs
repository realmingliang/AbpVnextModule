using System;
using Volo.Abp.Application.Dtos;

namespace Tudou.Abp.OrganizationUnit
{
    public class OrganizationUnitDto : AuditedEntityDto<Guid>
    {
        public Guid? ParentId { get; set; }

        public string Code { get; set; }
        public string Name { get; set; }
        public int MemberCount { get; set; }
        public int RoleCount { get; set; }
    }
}