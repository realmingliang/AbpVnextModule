using System;
using Volo.Abp.Application.Dtos;

namespace Tudou.Abp.Identity.OrganizationUnits
{
    public class FindOrganizationUnitUsersInput : PagedResultRequestDto
    {
        public string Filter { get; set; }
        public Guid OrganizationUnitId { get; set; }
    }
}