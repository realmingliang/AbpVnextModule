using System;
using Volo.Abp.Application.Dtos;

namespace Tudou.Abp.Identity.OrganizationUnits
{
    public class OrganizationUnitRoleListDto : EntityDto<Guid>
    {
        public string Name { get; set; }

        public DateTime AddedTime { get; set; }
    }
}