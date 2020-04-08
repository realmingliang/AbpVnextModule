using System;
using Volo.Abp.Application.Dtos;

namespace Tudou.Abp.Identity.OrganizationUnits
{
    public class OrganizationUnitUserListDto : EntityDto<Guid>
    {
        public string Name { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public DateTime AddedTime { get; set; }
    }
}