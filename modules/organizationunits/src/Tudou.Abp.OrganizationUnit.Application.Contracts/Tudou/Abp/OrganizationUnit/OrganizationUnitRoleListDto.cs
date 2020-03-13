using System;
using Volo.Abp.Application.Dtos;

namespace Tudou.Abp.OrganizationUnit
{
    public class OrganizationUnitRoleListDto:EntityDto<Guid>
    {
        public string NormalizedName { get; set; }

        public string Name { get; set; }

        public DateTime AddedTime { get; set; }


    }
}